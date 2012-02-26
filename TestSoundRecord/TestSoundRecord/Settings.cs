using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSoundRecord
{
    class Settings
    {
        public String wavName = @"my1.wav";
        public String tmpName = @"tmp.flac";


        public String AppTitle
        {
            get { return "Google Voice Search Test"; }

        }

        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Settings();

                return _instance;
            }

        }
    }
}
