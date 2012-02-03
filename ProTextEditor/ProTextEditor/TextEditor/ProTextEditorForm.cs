using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ProTextEditor.TextEditor
{
    public partial class ProTextEditorForm : Form
    {
        private Text document;
        private Font currentFont=null;
        private Color defColor;
        public ProTextEditorForm()
        {
            InitializeComponent();
            document = new Text();
            document.TextChanged += new EventHandler(document_TextChanged);
            defColor = toolStripButton1.BackColor;
        }

        void document_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Rtf != document.InnerText)
                richTextBox1.Rtf = document.InnerText;
        }
        public DialogResult OpenFile() { return Loader.OpenFile(document); }

        public void SaveFile() { document.InnerText = richTextBox1.Rtf; Saver.SaveFile(document); }
        public void SaveFileAs() { document.InnerText = richTextBox1.Rtf; Saver.SaveFileAs(document); }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void ProTextEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!document.IsSaved)
            {
                DialogResult dr = MessageBox.Show("Сохранить файл?", "Файл не сохранен!", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.Yes: SaveFile(); break;
                    case DialogResult.No: break;
                    case DialogResult.Cancel: e.Cancel = true; break;
                }
            }
        }

        private void copyToolStripButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(richTextBox1.SelectedText);
        }

        private void cutToolStripButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.richTextBox1.SelectedText);
            this.richTextBox1.SelectedText = String.Empty;
        }

        private void pasteToolStripButton1_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();

            if (data.GetDataPresent(DataFormats.Text))
                richTextBox1.SelectedText = data.GetData(DataFormats.Text).ToString();
        }

        private void selectAllToolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void ProTextEditorForm_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = richTextBox1.Text.Length.ToString();
            foreach (var item in FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(item.Name);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.SelectedText))
            {
                currentFont = richTextBox1.SelectionFont;
                richTextBox1.SelectionFont=new Font(new FontFamily(toolStripComboBox1.SelectedItem.ToString()),currentFont.Size);
            }

        }


        public void PrintDocument()
        {
            PrintDialog printDialog = new PrintDialog();
            DialogResult dr = printDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                doc.PrintPage += new PrintPageEventHandler(CreatePage);
                doc.Print();
            }
        }

        public void PrintPreview()
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += new PrintPageEventHandler(CreatePage);
            previewDialog.Document = doc;
            previewDialog.Show();
        }

        private void CreatePage(object sender, PrintPageEventArgs ppeArgs)
        {
            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);
            ppeArgs.Graphics.DrawString(richTextBox1.Text, richTextBox1.SelectionFont, br, 20, 10);
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.SelectedText))
            {
                currentFont = richTextBox1.SelectionFont;
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily, Convert.ToSingle(toolStripComboBox2.SelectedItem.ToString()));
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.SelectedText))
            {
                currentFont = richTextBox1.SelectionFont;
                if (currentFont.Style.HasFlag(FontStyle.Bold))
                {
                    richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style ^ FontStyle.Bold);
                    toolStripButton1.BackColor = defColor;
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style | FontStyle.Bold);
                    toolStripButton1.BackColor = Color.Gold;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.SelectedText))
            {
                currentFont = richTextBox1.SelectionFont;
                if (currentFont.Style.HasFlag(FontStyle.Italic))
                {
                    richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style ^ FontStyle.Italic);
                    toolStripButton2.BackColor = defColor;
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style | FontStyle.Italic);
                    toolStripButton2.BackColor = Color.Gold;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            currentFont = richTextBox1.SelectionFont;
            if (currentFont.Style.HasFlag(FontStyle.Underline))
            {
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style ^ FontStyle.Underline);
                toolStripButton3.BackColor = defColor;
            }
            else
            {
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style | FontStyle.Underline);
                toolStripButton3.BackColor = Color.Gold;
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Style.HasFlag(FontStyle.Bold))
                toolStripButton1.BackColor= Color.Gold;
            else
                toolStripButton1.BackColor = defColor;
            if (richTextBox1.SelectionFont.Style.HasFlag(FontStyle.Italic))
                toolStripButton2.BackColor = Color.Gold;
            else
                toolStripButton2.BackColor = defColor;
            if (richTextBox1.SelectionFont.Style.HasFlag(FontStyle.Underline))
                toolStripButton3.BackColor = Color.Gold;
            else
                toolStripButton3.BackColor = defColor;
        }
        public int SearchText(string text, int paramSearch, int position)
        {
            int rez = -1;
            switch (paramSearch)
            {
                case 0:
                    rez = richTextBox1.Find(text);
                    break;
                case 1:
                    rez = richTextBox1.Find(text, position, RichTextBoxFinds.None);
                    break; 

            }
            if (rez >= 0)
                rez += text.Length;
           return rez;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainWindow).SearchText();
        }
        
        
    }
}
