using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fimated
{
    public enum MenuCommand {None,Save,OpenFile,Create,SaveAs,CloseFile};
    public enum ProgramCommand {None,OpenTxt,Exit,Minim,ExitAll,OpenFM,Change}
    public class Comand_Module
    {
        private string responseText;
        public bool isOpenTextEditor = false;
        public bool isOpenFileManager = false;
        public ProgramCommand pCom = ProgramCommand.None;
        public MenuCommand mCommand = MenuCommand.None;
        public string Text;
        
        

        public Comand_Module(string str)
        {
            responseText = str;
        }

        //parse responze
        public void ParseResponse()
        {
            if (responseText.Contains("компьютер"))
            {
                if (responseText.Contains("открыть") && responseText.Contains("текстовый") && responseText.Contains("редактор"))
                {
                    pCom = ProgramCommand.OpenTxt;
                }
                if (responseText.Contains("закрыть"))
                {
                    pCom = ProgramCommand.Exit;
                }
                if (responseText.Contains("переключится"))
                {
                    pCom = ProgramCommand.Change;
                }
            }


            else
            {
                if (isOpenFileManager == true)
                {
                    Text = responseText;
                }
            }
        }
        

    }
}
