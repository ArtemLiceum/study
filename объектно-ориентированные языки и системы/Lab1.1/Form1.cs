using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st;
            st = textBox1.Text;
            
            byte[] value = Encoding.ASCII.GetBytes(st);
            if ((value[0] >= 65 && value[0] <= 90) ) 
            {
                label1.Text = "0";
            } else if (value[0] >= 97 && value[0] <= 122)
            {
                label1.Text = "1";
            } else { label1.Text = "программа корректно реагирует на символы"; }
        }
    }
}
