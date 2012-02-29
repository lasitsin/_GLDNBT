using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProTextEditor;
using CommandModule;

namespace Fimated
{
    public partial class Form1 : Form
    {
        private MainWindow _textEditor = null;
        private Form _fmanager = null;
        private ComandModule _command = null;

        //list of events
        public event EventHandler saveFile;
        public event EventHandler enterTxt;
        public event EventHandler closeMainProgramm;
        
        public Form1()
        {
            InitializeComponent();
            _command = new ComandModule();
        }

        public void GetResponse(string str)
        {
            _command.GetResponse(str);
        }

        public void DoCommand()
        {
            if (_command != null)
            {
                if (_command.PCom == ProgramCommand.OpenTxt)
                {
                    _textEditor = new MainWindow();
                    _textEditor.Show();
                    _command.IsOpenTextEditor = true;
                }
                else
                {
                    if(_textEditor!=null)
                    if (!(_textEditor.RunCommand(_command)))
                        MessageBox.Show("Команда не выполнена!!");
                }
            }
            if (_command != null) 
                _command.ClearAllCommands();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetResponse(textBox1.Text);
            DoCommand();
        }

    }
}
