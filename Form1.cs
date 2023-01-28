using System;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;


namespace homework
{
    public partial class Form1 : Form
    {

        private BinaryTree<string> bt;
        private DataTable dt;
        

        public Form1()
        {
            InitializeComponent();
            //bt = new BinaryTree<string>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bt.add(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bt.remove(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tmp = bt.search(textBox1.Text);
        }
    }
}