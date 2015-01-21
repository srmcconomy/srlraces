using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        ChatBox richerTextBox1;
        private Race race;
        Form2 form2;
        public Form4(Race r, Form2 f2)
        {
            race = r;
            form2 = f2;
            InitializeComponent();
            richTextBox1.SelectionTabs = new int[] { 40 };
            richTextBox1.SelectionHangingIndent = 70;
            richerTextBox1 = new ChatBox();
        }
        public void notify(Message msg) {
            switch (msg.Command)
            {
                case ("JOIN"):
                    richTextBox1.SelectionColor = SystemColors.InactiveCaption;
                    richTextBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("- " + msg.User + " joined" + System.Environment.NewLine);
                    break;
                case ("PRIVMSG"):
                    if (msg.User == "RaceBot")
                    {
                        if (msg.Trail.Contains("has entered the race!"))
                        {

                        }
                    }
                    richTextBox1.SelectionColor = SystemColors.InactiveCaption;
                    richTextBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                    richTextBox1.SelectionColor = Color.Green;
                    richTextBox1.AppendText("<" + msg.User + ">");
                    richTextBox1.SelectionColor = Color.Black;
                    richTextBox1.AppendText(msg.Trail + System.Environment.NewLine);
                    break;

            }
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                richTextBox1.SelectionColor = SystemColors.InactiveCaption;
                richTextBox1.AppendText(DateTime.Now.ToString("h:mm") + "\t");
                richTextBox1.SelectionColor = Color.Gray;
                richTextBox1.AppendText("<" + Properties.Settings1.Default.UserName + ">");
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.AppendText(richTextBox3.Text + System.Environment.NewLine);
                form2.Send("PRIVMSG #srl-" + race.id + " " + richTextBox3.Text + "\r\n");
                richTextBox3.Clear();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            form2.Send("JOIN #srl-" + race.id + "\r\n");
        
            foreach (KeyValuePair<string, Entrant> kvp in race.entrants)
            {
                //Bitmap bm = new Bitmap("ttv_icon20px.png");
                Object[] objs = { kvp.Value.place, kvp.Value.displayname, kvp.Value.trueskill };
                dataGridView1.Rows.Add(objs);
                
            }


        }
        private void refresh()
        {

        }

    }
}
