using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private SRL srl;
        private Message msg;
        private IRC irc;
        private Dictionary<string, Form4> RacePages;
        Form5 f5;

        public Form2()
        {
            InitializeComponent();
            chatBox1.SelectionTabs = new int[] { 40 };
            chatBox1.SelectionHangingIndent = 70;
            RacePages = new Dictionary<string, Form4>();
            f5 = new Form5(this);

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            f5.Show();

            srl = SRL.Instance();

            irc = new IRC();
            irc.Connect();

            Form3 dialog = new Form3();
            while (Properties.Settings1.Default.UserName.Length == 0)
            {
                dialog.ShowDialog();
            }
            dialog.Dispose();
            irc.Login(Properties.Settings1.Default.UserName);
            backgroundWorker1.RunWorkerAsync();
            listBox1.BeginUpdate();
            foreach (Race r in srl.races)
            {
                listBox1.Items.Add(r.game.name);
            }
            listBox1.EndUpdate();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = srl.races[listBox1.SelectedIndex].game.name;
            label2.Text = srl.races[listBox1.SelectedIndex].goal;
            label5.Text = srl.races[listBox1.SelectedIndex].numentrants.ToString();
            label3.Text = srl.races[listBox1.SelectedIndex].statetext;
            pictureBox1.LoadAsync("http://cdn.speedrunslive.com/images/games/" + srl.races[listBox1.SelectedIndex].game.abbrev + ".jpg");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            msg = irc.GetLine();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int selectionStart = chatBox1.SelectionStart;
            int selectionLength = chatBox1.SelectionLength;
            chatBox1.SelectionStart = chatBox1.TextLength;
            chatBox1.SelectionLength = 0;
            f5.notify(msg);
            switch (msg.Command)
            {
                case ("376"):
                    irc.Send("JOIN #speedrunslive\r\n");
                    break;
                case ("NOTICE"):
                    if (msg.User == "NickServ" && msg.Trail.Contains("/msg NickServ IDENTIFY"))
                    {
                        irc.Send("PRIVMSG NickServ IDENTIFY " + Properties.Settings1.Default.Password + "\r\n");
                    }
                    if (msg.User.Length > 0)
                    {
                        chatBox1.SelectionColor = SystemColors.InactiveCaption;
                        chatBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                        chatBox1.SelectionColor = Color.Maroon;
                        chatBox1.AppendText("<" + msg.User + " whispers>");
                        chatBox1.SelectionColor = Color.Black;
                        chatBox1.AppendText(msg.Trail + System.Environment.NewLine);
                    }
                    break;
                case ("PRIVMSG"):
                    if (msg.Parameters.Count > 0 && msg.Parameters[0].Length > 5)
                    {
                        if (RacePages.ContainsKey(msg.Parameters[0].Substring(5)))
                        {
                            RacePages[msg.Parameters[0].Substring(5)].notify(msg);
                        }
                        if (msg.Parameters[0] == "#speedrunslive")
                        {
                            chatBox1.SelectionColor = SystemColors.InactiveCaption;
                            chatBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                            chatBox1.SelectionColor = Color.Green;
                            chatBox1.AppendText("<" + msg.User + ">");
                            chatBox1.SelectionColor = Color.Black;
                            chatBox1.AppendText(msg.Trail + System.Environment.NewLine);
                        }
                    }
                    break;
                case ("JOIN"):                 
                    if (RacePages.ContainsKey(msg.Trail.Substring(5)))
                    {
                        RacePages[msg.Trail.Substring(5)].notify(msg);
                    }
                    if (msg.Trail == "#speedrunslive")
                    {
                        chatBox1.SelectionColor = SystemColors.InactiveCaption;
                        chatBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                        chatBox1.SelectionColor = Color.DarkBlue;
                        chatBox1.AppendText("- " + msg.User + " joined" + System.Environment.NewLine);
                    }
                    break;
                case ("QUIT"):

                    chatBox1.SelectionColor = SystemColors.InactiveCaption;
                    chatBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                    chatBox1.SelectionColor = Color.DarkBlue;
                    chatBox1.AppendText("- " + msg.User + " quit - " + msg.Trail + System.Environment.NewLine);
                    break;
            }
            chatBox1.SelectionStart = selectionStart;
            chatBox1.SelectionLength = selectionLength;

            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(srl.races[listBox1.SelectedIndex], this);
            RacePages.Add(srl.races[listBox1.SelectedIndex].id, form4);
            form4.Show();
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                chatBox1.SelectionColor = SystemColors.InactiveCaption;
                chatBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                chatBox1.SelectionColor = Color.Gray;
                chatBox1.AppendText("<" + Properties.Settings1.Default.UserName + ">");
                chatBox1.SelectionColor = Color.Black;
                chatBox1.AppendText(richTextBox3.Text + System.Environment.NewLine);
                irc.Send("PRIVMSG #speedrunslive " + richTextBox3.Text + "\r\n");
                richTextBox3.Clear();
            }
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 dialog = new Form3();
            dialog.ShowDialog();

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        public void Send(string str)
        {
            irc.Send(str);
        }

        private void consoelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
