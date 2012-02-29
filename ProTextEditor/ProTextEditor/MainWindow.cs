using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProTextEditor.TextEditor;
using CommandModule;


namespace ProTextEditor
{
    public partial class MainWindow : Form
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
       

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProTextEditorForm textForm = new ProTextEditorForm();
            textForm.MdiParent = this;
            if (textForm.OpenFile() == DialogResult.OK) 
                textForm.Show();
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProTextEditorForm textForm = new ProTextEditorForm();
            textForm.MdiParent = this;
            textForm.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Form textForm = this.ActiveMdiChild;
            if (textForm != null) (textForm as ProTextEditorForm).SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form textForm = this.ActiveMdiChild;
            if (textForm != null) (textForm as ProTextEditorForm).SaveFileAs();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void слеваНаПравоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void минимизироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] forms = this.MdiChildren;
            foreach (var item in forms)
                           item.WindowState = FormWindowState.Minimized;
        }

        private void маToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] forms = this.MdiChildren;
            foreach (var item in forms)
                item.WindowState = FormWindowState.Maximized;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                (this.ActiveMdiChild as ProTextEditorForm).PrintDocument();
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                (this.ActiveMdiChild as ProTextEditorForm).PrintPreview();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutF = new About();
            aboutF.ShowDialog();
        }
        public void SearchText()
        {
            FindForm searchF = new FindForm(this);
            searchF.Show();
        }
        //command listener

        public bool RunCommand(ComandModule _com)
        {
            if(_com.MCommand==MenuCommand.Create)
            {
                newToolStripMenuItem_Click(this,EventArgs.Empty);
                return true;
            }
            if (string.IsNullOrEmpty(_com.Text))
            {
                EnterText(_com.Text);
                return true;
            }
            return false;
        }

        private void EnterText(string txt)
        {
            if (this.ActiveMdiChild != null)
            {
                if (txt != null)
                {
                    var proTextEditorForm = this.ActiveMdiChild as ProTextEditorForm;
                    if (proTextEditorForm != null)
                        proTextEditorForm.EnterText(txt);
                }
            }
        }

        

    }
}
