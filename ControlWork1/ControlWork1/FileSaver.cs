using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlWork1
{
    public partial class FileSaver : Form
    {
        static public string name;
        static public string type;
        public FileSaver()
        {
            InitializeComponent();
        }

        private void FileSaver_Load(object sender, EventArgs e)
        {
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text + comboBox1.Text;
            type = comboBox1.Text;
        }
    }
}
