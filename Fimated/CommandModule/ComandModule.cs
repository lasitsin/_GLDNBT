using System;

namespace CommandModule
{
    public class ComandModule : EventArgs
    {
        private string _responseText;
        public bool IsOpenTextEditor = false;
        public bool IsOpenFileManager = false;
        public bool EnterText = false;
        public bool Redact = false;
        public ProgramCommand PCom = ProgramCommand.None;
        public MenuCommand MCommand = MenuCommand.None;
        public string Text;


        public ComandModule()
        {

        }

        public void GetResponse(string str)
        {
            _responseText = str;
            ParseResponse();
        }

        //parse responze
        private void ParseResponse()
        {
            if (_responseText.Contains("компьютер"))
            {
                if (_responseText.Contains("открыть") && _responseText.Contains("текстовый") && _responseText.Contains("редактор"))
                {
                    PCom = ProgramCommand.OpenTxt;
                }
                if (_responseText.Contains("закрыть"))
                {
                    PCom = ProgramCommand.Exit;
                }
                if (_responseText.Contains("переключится"))
                {
                    PCom = ProgramCommand.Change;
                }
                if (_responseText.Contains("открыть") && _responseText.Contains("файловый") && _responseText.Contains("менеджер"))
                {
                    PCom = ProgramCommand.OpenFm;
                }
                if (_responseText.Contains("закрыть") && _responseText.Contains("файловый") && _responseText.Contains("менеджер"))
                {
                    PCom = ProgramCommand.ExitFm;
                }
            }
            else 
            {
                if (_responseText.Contains("выключить") && _responseText.Contains("набор") && _responseText.Contains("текста"))
                    MCommand = MenuCommand.NotEnterTxt;
                else if (IsOpenTextEditor == true && EnterText==true)
                {
                    Text = _responseText;
                }
                else if (_responseText.Contains("создать"))
                    MCommand = MenuCommand.Create;
                else if (_responseText.Contains("сохранить"))
                    MCommand = MenuCommand.Save;
                else if (_responseText.Contains("сохранить") && _responseText.Contains("как"))
                    MCommand = MenuCommand.SaveAs;
                else if (_responseText.Contains("печать"))
                    MCommand = MenuCommand.Print;
                else if (_responseText.Contains("просмотр"))
                    MCommand = MenuCommand.PreView;
                else if ( _responseText.Contains("набор") && _responseText.Contains("текста"))
                    MCommand = MenuCommand.EnterTxt;
                
            }

        }
        public void ClearAllCommands()
        {
            PCom = ProgramCommand.None;
            MCommand = MenuCommand.None;
            Text = "";
        }


    }
}