using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NAudio.Wave;
using AudioInterface;

namespace NAudioTutorial5
{
    public partial class NAudioTutorial5 : Form
    {
        //Declarations required for audio out and mixing
        private IWavePlayer waveOutDevice;
        private WaveMixerStream32 mixer;

        // The Sample array we will load our Audio Samples in to
        private AudioSample Sample;

        // WaveIn Streams for recording
        WaveIn waveInStream;
        WaveFileWriter writer;
        string outputFilename;


        public NAudioTutorial5()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup Audio via NAudio. Defaults to using Asio for Audio Output.
        /// </summary>
        public void SetupAudio()
        {
            //Setup the Mixer
            mixer = new WaveMixerStream32();
            mixer.AutoStop = false;

            if (waveOutDevice == null)
            {
                //waveOutDevice = new AsioOut();

                waveOutDevice = new WaveOut(0, 300, false);
                
                waveOutDevice.Init(mixer);
                waveOutDevice.Play();
            }
        }

        private void NAudioTutorial5_Load(object sender, EventArgs e)
        {
            SetupAudio();
        }

        private void cmbOpen_Click(object sender, EventArgs e)
        {
            // prompt for file load
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV Files (*.wav)|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Sample = new AudioSample(openFileDialog.FileName);

                //byte[] tempSample = new byte[(int)Sample.Length];
                //Sample.Read(tempSample, 0, (int)Sample.Length);

                // Need to dispose the waveOutDevice - if it is created before the sample has been added
                // to the mixer then sample playback is undertaken at twice the speed for some reason
                waveOutDevice.Dispose();
                // Add the stream to the mixer
                mixer.AddInputStream(Sample);
                //Re-initalise the waveOutDevice and play back sound
                waveOutDevice.Init(mixer);
                waveOutDevice.Play();

                // The stop is required because when an InputStream is added, if it is too long it will start playing because we do not turn off the mixer.
                // This is effectively just a work around by making sure that we move the playback position to the end of the stream to aviod this issue.
                Sample.Position = Sample.Length;
            }
        }

        private void cmbPlay_Click(object sender, EventArgs e)
        {
            Sample.SetReverse(chkReverse.Checked);
            Sample.Position = 0;
        }

        private void cmbRecord_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select output file:";
            saveFileDialog.Filter = "WAV Files (*.wav)|*.wav";
            saveFileDialog.FileName = outputFilename;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputFilename = saveFileDialog.FileName;

                waveInStream = new WaveIn(44100,2);
                writer = new WaveFileWriter(outputFilename, waveInStream.WaveFormat);

                waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
                waveInStream.StartRecording();
                
       
                // Just controling the objects on the screen.
                cmbRecord.Enabled = false;
                cmbStop.Enabled = true;
            }
        }


        void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);

        }

        private void cmbStop_Click(object sender, EventArgs e)
        {
            waveInStream.StopRecording();
            waveInStream.Dispose();
            waveInStream = null;
            writer.Close();
            writer = null;

            cmbRecord.Enabled = true;
            cmbStop.Enabled = false;
        }

        private void cmbRecordDirect_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select output file:";
            saveFileDialog.Filter = "WAV Files (*.wav)|*.wav";
            saveFileDialog.FileName = outputFilename;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputFilename = saveFileDialog.FileName;
                mixer.StreamMixToDisk(outputFilename);
                mixer.StartStreamingToDisk();
                cmbRecordDirect.Enabled = false;
                cmbStopDirect.Enabled = true;
                cmbPauseDirect.Enabled = true;
            }
        }

        private void cmbStopDirect_Click(object sender, EventArgs e)
        {
            mixer.StopStreamingToDisk();
            cmbRecordDirect.Enabled = true;
            cmbStopDirect.Enabled = false;
            cmbPauseDirect.Enabled = false;
        }

        private void cmbPauseDirect_Click(object sender, EventArgs e)
        {
            if (cmbPauseDirect.Text == "Pause")
            {
                cmbPauseDirect.Text = "Resume";
                mixer.PauseStreamingToDisk();
            }
            else
            {
                cmbPauseDirect.Text = "Pause";
                mixer.ResumeStreamingToDisk();
            }
        }


    }
}
