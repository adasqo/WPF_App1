using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        bool ifClosedWithXButton = true;
        public Form2()
        {
            InitializeComponent();
        }

        public int GetNewWidth
        {
            get
            {
                return (int)numericUpDownNewScheme.Value;
            }
        }
        public int GetNewHeight
        {
            get
            {
                return (int)numericUpDownNewScheme2.Value;
            }
        }
        public bool IfClosedWithXButton
        {
            get
            {
                return ifClosedWithXButton;
            }
        }
        private void buttonNewScheme_Click(object sender, EventArgs e)
        {
            ifClosedWithXButton = false;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
  
        }
    }
}
