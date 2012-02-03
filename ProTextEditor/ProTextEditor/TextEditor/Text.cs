using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProTextEditor.TextEditor
{
    public class Text
    {
        public event EventHandler TextChanged;

        private String location = "";
        public String Location
        {
            get { return location; }
            set { location = value; }
        }

        public Boolean isSaved = true;
        public Boolean IsSaved
        {
            get { return isSaved; }
        }

        private String text = "";
        public String InnerText
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    isSaved = false;
                    if (TextChanged != null) TextChanged(this, EventArgs.Empty);
                }
            }
        }
    }
}
