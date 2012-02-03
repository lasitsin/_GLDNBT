/*
        private void Form1_Load(object sender, EventArgs e)
        {

            // Create a new SpeechRecognizer instance.
            sr = new SpeechRecognizer();
            MessageBox.Show(sr.State.ToString());
            // Create a simple grammar that recognizes "red", "green", or "blue".
            Choices colors = new Choices();
            colors.Add(new string[] { "red", "green", "blue" });

            // Create a GrammarBuilder object and append the Choices object.
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // Create the Grammar instance and load it into the speech recognizer.
            Grammar g = new Grammar(gb);
            sr.LoadGrammar(g);

            // Register a handler for the SpeechRecognized event.
            sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);
        }

        
        
        
        
         //Create a simple handler for the SpeechRecognized event.
        void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show(e.Result.Text);
        }


        SpeechRecognizer sr;

//*/