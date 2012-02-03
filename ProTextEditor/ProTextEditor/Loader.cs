using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProTextEditor.TextEditor
{
    public static class Loader
    {
        public static DialogResult OpenFile(Text doc)
        {
            OpenFileDialog load = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                ValidateNames = true,
                Title = "Open File",
                Filter = "Text files (*.rtf)|*.rtf"
            };

            if (load.ShowDialog() == DialogResult.OK)
            {
                doc.Location = load.FileName;

                try
                {
                    TextReader textReader = new StreamReader(load.FileName, Encoding.Default, true);
                    doc.InnerText = textReader.ReadToEnd();
                    textReader.Close();

                    return DialogResult.OK;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ошибка открытия файла!/n" + exception.Message, "ProTextEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return DialogResult.Cancel;
        }
    }
}
