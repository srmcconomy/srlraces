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
    public partial class Form1 : Form
    {
        private String l;
        private String l2;
        private String l3;
        private Message msg;
        private IRC irc;


        public Form1()
        {
            l = "";
            l2 = "";
            l3 = "";
            InitializeComponent();
            irc = new IRC();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            irc.Connect();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            msg = irc.GetLine();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            irc.Send("NICK mrfakeman\r\n");
            irc.Send("USER fakeman fakemans fakemen :leaguebot\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            irc.Send(textBox3.Text + "\r\n");
        }

        private void mRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (msg.Command)
            {
                case ("NOTICE"):
                    textBox2.SelectionColor = Color.Maroon;
                    textBox2.AppendText("<" + msg.User + " whispers>");
                    textBox2.SelectionColor = Color.Black;
                    textBox2.AppendText(msg.Trail + System.Environment.NewLine);
                    break;
                case ("PRIVMSG"):
                    textBox2.SelectionColor = Color.Green;
                    textBox2.AppendText("<" + msg.User + ">");
                    textBox2.SelectionColor = Color.Black;
                    textBox2.AppendText(msg.Trail + System.Environment.NewLine);
                    break;
                case ("JOIN"):
                    textBox2.SelectionColor = Color.DarkBlue;
                    textBox2.AppendText("- " + msg.User + " joined" + System.Environment.NewLine);
                    break;
                case ("QUIT"):
                    textBox2.SelectionColor = Color.DarkBlue;
                    textBox2.AppendText("- " + msg.User + " quit - " + msg.Trail + System.Environment.NewLine);
                    break;
            }
            textBox1.AppendText(msg.ToString() + System.Environment.NewLine);
            backgroundWorker1.RunWorkerAsync();

        }

    }
}
