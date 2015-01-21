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
    public partial class Form5 : Form
    {
        Form2 form2;
        public Form5(Form2 f2)
        {
            form2 = f2;
            InitializeComponent();
        }
        public void notify(Message msg)
        {
            richTextBox1.AppendText(msg.ToString() + System.Environment.NewLine);
        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
