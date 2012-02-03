using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProTextEditor.TextEditor
{
    public static class Saver
    {
        private static void Save(Text doc)
        {
            try
            {
                TextWriter textWriter = new StreamWriter(doc.Location, false);
                textWriter.Write(doc.InnerText);
                textWriter.Close();

                doc.isSaved = true;
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения файла!", "ProTextEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SaveFileAs(Text doc)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                CheckPathExists = true,
                ValidateNames = true,
                AddExtension = true,
                Title = "Save File - MDI Sample",
                Filter = "Text files (*.rtf)|*.rtf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                doc.Location = sfd.FileName;
                Saver.Save(doc);
            }
        }

        public static void SaveFile(Text doc)
        {
            if (doc.Location != String.Empty) Save(doc);
            else SaveFileAs(doc);
        }
    }
}
