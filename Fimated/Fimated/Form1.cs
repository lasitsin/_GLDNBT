using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProTextEditor;

namespace Fimated
{
    public partial class Form1 : Form
    {
        private Form TextEditor = null;
        private Form Fmanager = null;
        private Comand_Module command = null;
        public Form1()
        {
            InitializeComponent();
            GetResponse("компьютер открыть текстовый редактор");
            DoCommand();
        }

        public void GetResponse(string str)
        {
            command = new Comand_Module(str);
            command.ParseResponse();
        }

        public void DoCommand()
        {
            if (command != null)
            {
                if (command.pCom == ProgramCommand.OpenTxt)
                {
                    TextEditor = new MainWindow();
                    TextEditor.Show();
                }

            }
        }
    }
}
