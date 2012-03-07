using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommandModule;

namespace exam_FManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openToolStripMenuItem.PerformClick();
            this.MdiChildren[0].WindowState = FormWindowState.Maximized;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.menuStrip1.AllowMerge = false;
            this.optionsToolStripMenuItem.Checked = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.MdiParent = this;
            if (this.MdiChildren.Count() > 1)
            {
                fm2.Text = fm2.Text + " (" + this.MdiChildren.Count().ToString() + ")";
            }
            fm2.Show();
        }



        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = this.MdiChildren.Count();
            for (int i = 0; i < n; i++)
            {
                this.MdiChildren[i].WindowState = FormWindowState.Maximized;
            }
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = this.MdiChildren.Count();
            for (int i = 0; i < n; i++)
            {
                this.MdiChildren[i].WindowState = FormWindowState.Minimized;
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = this.MdiChildren.Count();
            for (int i = n-1; i >= 0; i--)
            {
                this.MdiChildren[i].Close();
            }
        }



        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuIt = (ToolStripMenuItem)sender;
            if (menuIt.Checked)
            {
                this.menuStrip1.AllowMerge = false;
            }
            else
            {
                this.menuStrip1.AllowMerge = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }





        public bool RunCommand(ComandModule _com)
        {
            if (_com.PCom == ProgramCommand.ExitFm)
            {
                this.Close();
                return true;
            }
            return false;
        }


    }
}
