using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class ChatBox : RichTextBox
    {
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 15)
            {
                Graphics graphic = base.CreateGraphics();
                OnPaint(new PaintEventArgs(graphic, base.ClientRectangle));
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(SystemColors.ActiveBorder);
            e.Graphics.DrawLine(pen, 35, 0, 35, this.Height);
        }
    }
}
