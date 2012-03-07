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
using ProTextEditor;
using CommandModule;
using exam_FManager;

using NAudio.Wave;
//using AudioInterface;

namespace TestSoundRecord
{
    public partial class Form1 : Form
    {
        //for command module
        private MainWindow _textEditor = null;
        private exam_FManager.Form1 _fmanager = null;
        private ComandModule _command = null;

        // WaveIn Streams for recording
        WaveIn waveInStream;
        WaveFileWriter writer;
        WaveFileWriter writer2;
        string outputFilename;
        string outputFilename2;

        private bool rec1 = true;
        private bool rec2 = false;
        


        public Form1()
        {
            InitializeComponent();
            txtPath.Text = Settings.Instance.wavName;
            _command = new ComandModule();
        }

        private delegate void DoWorkDelegate(String filePath);
        private delegate void AddDelegate(String log);
        private delegate void ReportOnProgressDelegate(int progress, string msg);
        private delegate void ContinueDelegate();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                outputFilename = Settings.Instance.wavName;
                outputFilename2 = Settings.Instance.wavName2;


                numericUpDown1.Value = 0;
                waveInStream = new WaveIn(8000, 2);

                if (rec1)
                {
                    loggy.Text += "создаем писателя" + System.Environment.NewLine;
                    writer = new WaveFileWriter(outputFilename, waveInStream.WaveFormat);
                    
                }
                else if(rec2)
                {
                    loggy.Text += "создаем писателя2" + System.Environment.NewLine;
                    writer2 = new WaveFileWriter(outputFilename2, waveInStream.WaveFormat);
                }


                loggy.Text += "создаем событие на DataAvailable" + System.Environment.NewLine;
                waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
                
                loggy.Text += "стартуем запись" + System.Environment.NewLine;
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
            rec1 = !rec1;
            rec2 = !rec2;
            loggy.Text += System.Environment.NewLine + "останавливаем запись" + System.Environment.NewLine;
            waveInStream.StopRecording();
            
            loggy.Text += "уничтожаем поток" + System.Environment.NewLine;
            waveInStream.Dispose();
            loggy.Text += "обнуляем поток" + System.Environment.NewLine;
            waveInStream = null;

            if (writer != null)
            {
                loggy.Text += "закрываем писателя" + System.Environment.NewLine;
                writer.Close();
                loggy.Text += "обнуляем писателя" + System.Environment.NewLine;
                writer = null;
            }
            else if (writer2 != null)
            {
                loggy.Text += "закрываем писателя 2" + System.Environment.NewLine;
                writer2.Close();
                loggy.Text += "обнуляем писателя 2" + System.Environment.NewLine;
                writer2 = null;
            }          
            button1.Enabled = true;
            button2.Enabled = false;

            ContinueRec();
            if (!rec1)
            {
                loggy.Text += "отправляем файл 1 в ГУГЛ" + System.Environment.NewLine;
                SendFileRun(Settings.Instance.wavName);
            }
            else if (!rec2)
            {
                loggy.Text += "отправляем файл 2 в ГУГЛ" + System.Environment.NewLine;
                SendFileRun(Settings.Instance.wavName2);
            }
            
        }


        private void SendFileRun(String filePath)
        {
            // Disable the button
            DisableStartButtons();
            // Create delegate and make async call
            loggy.Text += "создаем делегат и асинхронно отправляем файл" + System.Environment.NewLine;
            DoWorkDelegate worker = new DoWorkDelegate(SendFile);
            loggy.Text += "начинаем инвок" + System.Environment.NewLine;
            worker.BeginInvoke(filePath, new AsyncCallback(DoWorkComplete), worker);
        }
        private void SendFile(String filePath)
        {
            try
            {
                ReportOnProgress(10, "Идет запрос");
                String responseFromServer = null;
                if (!rec1)
                {
                    //loggy.Text += "идет запрос" + System.Environment.NewLine;
                    responseFromServer = GoogleVoice.GoogleSpeechRequest(filePath, Settings.Instance.tmpName);
                }
                else if (!rec2)
                {
                    responseFromServer = GoogleVoice.GoogleSpeechRequest(filePath, Settings.Instance.tmpName2);
                }

                //loggy.Text += "выводим лог" + System.Environment.NewLine;
                AddLog(responseFromServer + System.Environment.NewLine);
                JSon.RecognitionResult result = JSon.Parse(responseFromServer);
                if (result.hypotheses.Length > 0)
                {
                    //loggy.Text += "выводим результат" + System.Environment.NewLine;
                    JSon.RecognizedItem item = result.hypotheses.First();
                    //listBox1.Items.Add(String.Format("{0:0.000} ; {1}", item.confidence, item.utterance));
                    AddToList(String.Format("{0:0.000} ; {1}", item.confidence, item.utterance));
                    AddToOut(String.Format("{0}",item.utterance));
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
        private void DoWorkComplete(IAsyncResult workID)
        {
            //loggy.Text += "ворк комплит";
            EnableStartButtons();
            DoWorkDelegate worker = workID.AsyncState as DoWorkDelegate;
            worker.EndInvoke(workID);
            ReportOnProgress(100, "Запрос произведен");
            //ContinueRec();
        }


        private void DisableStartButtons()
        {
            button1.Enabled = false;
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

        private void ContinueRec()
    {
        if (InvokeRequired)
        {
            Invoke(new ContinueDelegate(ContinueRec));
            return;
        }
        if (checkBox1.Checked)
        {
            button1.PerformClick(); 
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
        private void AddToOut(string utterance)
        {
            if (InvokeRequired)
            {
                Invoke(new AddDelegate(AddToOut), new object[] { utterance });
                return;
            }
            textOut.Text += " " + utterance;
            GetResponse(utterance);
            DoCommand();
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



        void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {

            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;

            loggy.Text += ".";
            numericUpDown1.Value++;
            textBox2.Text = buffer.Length.ToString();
            textBox3.Text = bytesRecorded.ToString();
            textBox4.Text = buffer.GetUpperBound(0).ToString();
            textBox5.Text = buffer.GetLowerBound(0).ToString();
            for (int i = 0; i < buffer.Length; i+=200)
            {
                textBox7.Text = buffer[i].ToString();
                progressBar1.Value = buffer[i]; 
            }

            //loggy.Text += "!!! пишем данные буфера в писатель" + System.Environment.NewLine;
            if (rec1)
            {
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);
                int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
                textBox6.Text = secondsRecorded.ToString();
            }
            else if(rec2)
            {
                writer2.WriteData(e.Buffer, 0, e.BytesRecorded);
                int secondsRecorded = (int)(writer2.Length / writer2.WaveFormat.AverageBytesPerSecond);
                textBox6.Text = secondsRecorded.ToString();
            }
            if ((numericUpDown1.Value % numericUpDown2.Value) == 0 && checkBox1.Checked)
            {
                button2.PerformClick();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                button2.PerformClick();
            }
        }

        //Parse sesponse and command
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
                else if (_command.PCom == ProgramCommand.OpenFm)
                {
                    _fmanager = new exam_FManager.Form1();
                    _fmanager.Show();
                    _command.IsOpenFileManager = true;
                }
                else if (_textEditor != null)
                {
                    if (!(_textEditor.RunCommand(_command)))
                        MessageBox.Show("Команда не выполнена!!");
                }
                else if (_fmanager != null)
                {
                    if (!(_fmanager.RunCommand(_command)))
                    {
                        MessageBox.Show("Команда не выполнена!!");
                    }

                }

            }
            if (_command != null)
                _command.ClearAllCommands();
        }

        
    }
}
