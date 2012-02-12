/* 
 * This code was origionaly written by Mark Heath - MixDiffStream.cs; contained in the MixDiffDemo application from NAudio. 
 * The following code is licenced under the MS-PL.
 * 
 * Additional modifications writen by Sebastian Gray.
*/

using System;
using System.Collections.Generic;
using System.Text;

using NAudio.Wave;


namespace AudioInterface
{
    public class AudioSample : WaveStream
    {
        // General Sample Settings (Info)
        string _fileName = "";
        bool _loop;
        long _pausePosition = -1;
        bool _pauseLoop;
                
        // Sample WaveStream Settings
        //WaveOffsetStream offsetStream;
        WaveChannel32 channelStream;
        bool muted;
        float volume;

        int bytesPerSample;

        // SampleArray to be store the reveresed array
        byte[] reversedSample;

        bool _sampleReversed = false; 

        public AudioSample(string fileName)
        {
            _fileName = fileName;
            WaveFileReader reader = new WaveFileReader(fileName);
            //offsetStream = new WaveOffsetStream(reader);
            //channelStream = new WaveChannel32(offsetStream);
            channelStream = new WaveChannel32(reader);
            muted = false;
            volume = 1.0f;



            #region ReverseStuff

            //Code to reverse
            NAudioBufferReverse nbr = new NAudioBufferReverse();
            reversedSample = new byte[(int)channelStream.Length];
            channelStream.Read(reversedSample, 0, (int)channelStream.Length);
            bytesPerSample = (channelStream.WaveFormat.BitsPerSample / 8) * channelStream.WaveFormat.Channels;
            reversedSample = nbr.reverseSample(reversedSample, (int)channelStream.Length, bytesPerSample);

            #endregion


        }







        public override WaveFormat WaveFormat
        {
            get { return channelStream.WaveFormat; }
        }

        public override long Length
        {
            get { return channelStream.Length; }
        }

        public override long Position
        {
            get
            {
                return channelStream.Position;
            }
            set
            {
                channelStream.Position = value;
            }
        }


        public void SetReverse (bool Reverse)
        {
            _sampleReversed = Reverse;
        }



        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_sampleReversed)
            {

                System.IO.MemoryStream stream0;
                byte[] streamBuffer0;

                streamBuffer0 = new byte[(int)channelStream.Length];
                stream0 = new System.IO.MemoryStream(streamBuffer0);
                stream0.Write(reversedSample, 0, (int)channelStream.Length);

                //need to understand why this is a more reliable offset
                offset = (int)channelStream.Position;


                // Have to work out our own number. The only time this number should be 
                // different is when we hit the end of the stream but we always need to
                // report that we read the same amount. Missing data is filled in with 
                // silence
                int outCount = count;

                // This try is probably not required any more ;-)
                try
                {
                    // Find out if we are trying to read more data than is available in the buffer
                    if (offset + count > streamBuffer0.Length)
                    {
                        // If we are then reduce the read amount
                        count = count - ((offset + count) - streamBuffer0.Length);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        // Individually copy the samples into the buffer for reading by the overriden method
                        buffer[i] = streamBuffer0[i + offset];
                    }
                }
                finally { }

                // Setting this position lets us keep track of how much has been played back.
                // There is no other offset used to track this information
                channelStream.Position = channelStream.Position + count;

                // Regardless of how much is read the count expected by the calling method is
                // the same number as was origionaly provided to the Read method
                return outCount;




                //byte[] tempBuffer = new byte[sourceLengthBytes];

                //// Fill the temp Buffer - with a complete set of data
                //channelStream.Read(tempBuffer, 0, sourceLengthBytes);

                //// Reverse the buffer, we want to move complete samples to the other end of the stream
                //for (int i = 0; i < sourceLengthBytes; i + bytesPerSample)
                //{


                //}












                //int read = 0;
                //while (read < count)
                ////{
                ////    int required = count - read;

                //    //for (int i = 1; i < count; i++)
                //    {
                //        //int readThisTime = channelStream.Read(buffer, (count - i) + (offset - read), 1);
                //        int readThisTime = channelStream.Read(buffer, 0, count);
                //        read += readThisTime;
                //    }

                ////    int readThisTime = channelStream.Read(buffer, offset + read, required);
                ////    if (readThisTime < required)
                ////    {
                ////        channelStream.Position = 0;
                ////    }

                ////    if (channelStream.Position >= channelStream.Length)
                ////    {
                ////        channelStream.Position = 0;
                ////    }
                ////    read += readThisTime;
                ////}
                //return read;

            }
            else
            {
                // Normal read code, sample has not been set to loop
                return channelStream.Read(buffer, offset, count);
            }
        }

        







        #region NotUsed


        public bool Mute
        {
            get
            {
                return muted;
            }
            set
            {
                muted = value;
                if (muted)
                {
                    channelStream.Volume = 0.0f;
                }
                else
                {
                    // reset the volume                
                    Volume = Volume;
                }
            }
        }


        public override bool HasData(int count)
        {
            return channelStream.HasData(count);
        }

        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                if (!Mute)
                {
                    channelStream.Volume = volume;
                }
            }
        }

        // We are not using the delay stream..
        //public TimeSpan PreDelay
        //{
        //    get { return offsetStream.StartTime; }
        //    set { offsetStream.StartTime = value; }
        //}

        //public TimeSpan Offset
        //{
        //    get { return offsetStream.SourceOffset; }
        //    set { offsetStream.SourceOffset = value; }
        //}

        protected override void Dispose(bool disposing)
        {
            if (channelStream != null)
            {
                channelStream.Dispose();
            }

            base.Dispose(disposing);
        }

        public override int BlockAlign
        {
            get
            {
                return channelStream.BlockAlign;
            }
        }


        // General Sample Settings (Info)

        /// <summary>
        /// FileName of the loaded sample
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Loop determines wether the sample will play in a loop - takes imediate effect and cna be turned on and off while a sample is playing.
        /// </summary>
        public void SetLoop(bool Loop)
        {
            _loop = Loop;
        }

        public void Pause()
        {
            // Store the current stream settings
            _pausePosition = Position;
            _pauseLoop = _loop;

            // Ensure the sample is temporairly not looped and set the position to the end of the stream
            _loop = false;
            Position = Length;

            // Set the loop status back, so that any further modifications of the loop status are observed
            _loop = _pauseLoop;
        }

        public void Resume()
        {
            // Ensure that the sample had actuall been paused and that we are not just jumping to a random position
            if (_pausePosition >= 0)
            {
                // Set the position of the stream back to where it was paused
                Position = _pausePosition;

                // Set the pause position to negative so that we know the sample is not currently paused
                _pausePosition = -1;
            }
        }

        public void SetPan(float pan)
        {
            channelStream.Pan = pan;
        }


        #endregion


    }
}
