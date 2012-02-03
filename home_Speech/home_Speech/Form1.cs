using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;



namespace home_Speech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            words.Add("а", "a");
            words.Add("б", "b");
            words.Add("в", "v");
            words.Add("г", "g");
            words.Add("д", "d");
            words.Add("е", "e");
            words.Add("ё", "yo");
            words.Add("ж", "zh");
            words.Add("з", "z");
            words.Add("и", "i");
            words.Add("й", "j");
            words.Add("к", "k");
            words.Add("л", "l");
            words.Add("м", "m");
            words.Add("н", "n");
            words.Add("о", "o");
            words.Add("п", "p");
            words.Add("р", "r");
            words.Add("с", "s");
            words.Add("т", "t");
            words.Add("у", "u");
            words.Add("ф", "f");
            words.Add("х", "h");
            words.Add("ц", "c");
            words.Add("ч", "ch");
            words.Add("ш", "sh");
            words.Add("щ", "sch");
            words.Add("ъ", "j");
            words.Add("ы", "y");
            words.Add("ь", "j");
            words.Add("э", "e");
            words.Add("ю", "yu");
            words.Add("я", "ya");
            words.Add("А", "A");
            words.Add("Б", "B");
            words.Add("В", "V");
            words.Add("Г", "G");
            words.Add("Д", "D");
            words.Add("Е", "E");
            words.Add("Ё", "Yo");
            words.Add("Ж", "Zh");
            words.Add("З", "Z");
            words.Add("И", "I");
            words.Add("Й", "J");
            words.Add("К", "K");
            words.Add("Л", "L");
            words.Add("М", "M");
            words.Add("Н", "N");
            words.Add("О", "O");
            words.Add("П", "P");
            words.Add("Р", "R");
            words.Add("С", "S");
            words.Add("Т", "T");
            words.Add("У", "U");
            words.Add("Ф", "F");
            words.Add("Х", "H");
            words.Add("Ц", "C");
            words.Add("Ч", "Ch");
            words.Add("Ш", "Sh");
            words.Add("Щ", "Sch");
            words.Add("Ъ", "J");
            words.Add("Ы", "Y");
            words.Add("Ь", "J");
            words.Add("Э", "E");
            words.Add("Ю", "Yu");
            words.Add("Я", "Ya");


        }

        Dictionary<string, string> words = new Dictionary<string, string>();


        private string Translit(string rus)
        {
            string source = rus;
            foreach (KeyValuePair<string, string> pair in words)
            {
                source = source.Replace(pair.Key, pair.Value);
            }

            return source;
        }






        private void button1_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer tts = new SpeechSynthesizer();
            tts.SpeakAsync(Translit(textBox1.Text));
        }









        private void button2_Click(object sender, EventArgs e)
        {
            // Create a new SpeechRecognizer instance.
            // SpeechRecognizer sr = new SpeechRecognizer();
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    sr = new SpeechRecognizer();
        //    sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);

        //    GrammarBuilder gb = new GrammarBuilder();
        //    gb.Append("Start Notepad");

        //    Grammar g = new Grammar(gb);
        //    g.Priority = 127;

        //    sr.LoadGrammar(g);
        //}

        //void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    SendKeys.Send("Start Notepad");
        //}




    }
}
