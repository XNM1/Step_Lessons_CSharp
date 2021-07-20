using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static BindingList<int> l = new BindingList<int>();
        public static BindingList<int> l2 = new BindingList<int>();
        public Form1()
        {
            InitializeComponent();
            Thread t = new Thread(add);
            t.IsBackground = true;
            listBox1.DataSource = l;
            t.Start();

            Thread t2 = new Thread(add2);
            t2.IsBackground = true;
            listBox2.DataSource = l2;
            t2.Start();
        }
        public void add() {
            for (int i = 0; i < 100; i++)
            {
                if (listBox1.InvokeRequired) listBox1.Invoke(new Action(() => l.Add(i)));
                Thread.Sleep(100);
            }
        }
        public void add2() {
            for (int i = 99; i >= 0; i--)
            {
                if (listBox2.InvokeRequired) listBox2.Invoke(new Action(() => l2.Add(i)));
                Thread.Sleep(200);
            }
        }
    }
}
