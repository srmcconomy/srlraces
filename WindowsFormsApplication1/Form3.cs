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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings1.Default.UserName;
            textBox2.Text = Properties.Settings1.Default.Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings1.Default.UserName = textBox1.Text;
            Properties.Settings1.Default.Password = textBox2.Text;
            Properties.Settings1.Default.Save();
        }
    }
}
