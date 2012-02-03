using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProTextEditor.TextEditor
{
    
    public partial class FindForm : Form
    {
        private int rezult = 0;
        private Form parentForm = null;
        public FindForm(Form parent)
        {
            InitializeComponent();
            parentForm = parent;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProTextEditorForm textForm = (ProTextEditorForm)parentForm.ActiveMdiChild;
            if (textForm != null)
            {
                rezult = textForm.SearchText(textBox1.Text, comboBox1.SelectedIndex, rezult);
            }
            if (rezult < 0)
                MessageBox.Show("Фрагмент текста не найден", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                this.TopMost = true;
                parentForm.Focus();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
