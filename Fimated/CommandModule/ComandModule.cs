using System;

namespace CommandModule
{
    public class ComandModule : EventArgs
    {
        private string _responseText;
        public bool IsOpenTextEditor = false;
        public bool IsOpenFileManager = false;
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
            }
            else if(_responseText.Contains("команда"))
            {
                if (_responseText.Contains("создать"))
                    MCommand = MenuCommand.Create;
            }


            else
            {
                if (IsOpenTextEditor == true)
                {
                    Text = _responseText;
                }
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