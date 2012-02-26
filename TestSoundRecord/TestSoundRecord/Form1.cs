using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using GoogleSpeech;

using NAudio.Wave;
//using AudioInterface;

namespace TestSoundRecord
{
    public partial class Form1 : Form
    {
        // WaveIn Streams for recording
        WaveIn waveInStream;
        WaveFileWriter writer;
        string outputFilename;

        public Form1()
        {
            InitializeComponent();
            txtPath.Text = Settings.Instance.wavName;
        }

        private delegate void DoWorkDelegate(String filePath);
        private delegate void AddDelegate(String log);
        private delegate void ReportOnProgressDelegate(int progress, string msg);

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                outputFilename = Settings.Instance.wavName;

                waveInStream = new WaveIn(8000, 2);
                writer = new WaveFileWriter(outputFilename, waveInStream.WaveFormat);

                waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
                waveInStream.StartRecording();


                // Just controling the objects on the screen.
                button1.Enabled = false;
                button2.Enabled = true;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message,ex.Source, MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            waveInStream.StopRecording();
            waveInStream.Dispose();
            waveInStream = null;
            writer.Close();
            writer = null;
           
            button1.Enabled = true;
            button2.Enabled = false;

            SendFileRun(Settings.Instance.wavName);
        }
        private void SendFileRun(String filePath)
        {
            // Disable the button
            DisableStartButtons();
            // Create delegate and make async call
            DoWorkDelegate worker = new DoWorkDelegate(SendFile);
            worker.BeginInvoke(filePath, new AsyncCallback(DoWorkComplete), worker);
        }
        private void DisableStartButtons()
        {
            button1.Enabled = false;
        }


        private void SendFile(String filePath)
        {
            try
            {
                ReportOnProgress(10, "Идет запрос");
                String responseFromServer = GoogleVoice.GoogleSpeechRequest(filePath, Settings.Instance.tmpName);


                AddLog(responseFromServer + System.Environment.NewLine);
                JSon.RecognitionResult result = JSon.Parse(responseFromServer);
                if (result.hypotheses.Length > 0)
                {
                    JSon.RecognizedItem item = result.hypotheses.First();
                    //listBox1.Items.Add(String.Format("{0:0.000} ; {1}", item.confidence, item.utterance));
                    AddToList(String.Format("{0:0.000} ; {1}", item.confidence, item.utterance));
                }
                else
                {
                    AddToList(String.Format("None"));
                    //listBox1.Items.Add(String.Format("None"));
                }

                ReportOnProgress(100, "Запрос успешно выполнен");
            }
            catch (Exception e)
            {
                ReportOnProgress(100, "Ошибка запроса: " + e.Message);
                AddLog(e.ToString());
            }

        }
        private void ReportOnProgress(int progress, string msg)
        {
          if (InvokeRequired)
          {
            Invoke(new ReportOnProgressDelegate(ReportOnProgress),
              new object[] { progress, msg });
            return;
            ;
          }
                    
            this.Text = Settings.Instance.AppTitle + " —  " + msg;                      
        }
        private void AddToList(String item)
        {
            if (InvokeRequired)
            {
                Invoke(new AddDelegate(AddToList), new object[] { item });
                return;
            }
            listBox1.Items.Add(item);
        }

        private void AddLog(string log)
        {
            if (InvokeRequired)
            {
                Invoke(new AddDelegate(AddLog), new object[] { log });
                return;
            }

            txtLog.Text += log;
        }

        private void DoWorkComplete(IAsyncResult workID)
        {
            EnableStartButtons();
            DoWorkDelegate worker = workID.AsyncState as DoWorkDelegate;
            worker.EndInvoke(workID);
            //ReportOnProgress(100, "Запрос произведен");
        }

        private void EnableStartButtons()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(EnableStartButtons));
            }
            else
            {
                button1.Enabled = true;
            }

        }

        void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);

        }

        
    }
}
