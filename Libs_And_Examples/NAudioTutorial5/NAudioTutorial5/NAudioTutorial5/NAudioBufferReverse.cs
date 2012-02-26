using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using NAudio.Wave;

namespace AudioInterface
{
    class NAudioBufferReverse
    {

        // The Temp Streams
        private System.IO.MemoryStream stream0;
        private byte[] streamBuffer0;
        private System.IO.MemoryStream stream1;
        private byte[] streamBuffer1;

        // Length of the buffer
        private int numOfBytes;


        public byte[] reverseSample(byte[] sampleToReverse, int SourceLengthBytes, int BytesPerSample)//, int Channels)
        {


            numOfBytes = SourceLengthBytes;
            //int bytesPerSample = (BitsPerSample * Channels) / 8;

            int bytesPerSample = BytesPerSample;

            CreateStreamBuffers();
            CreateStreams();

            // The alternatve location; starts at the end and works to
            // the begining
            int b = 0;

            // Read the complete stream in to a memory stream
            //dsInterface.aSound[sampleToReverse].Read(0, stream0, numOfBytes, Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);
            stream0.Write(sampleToReverse, 0, numOfBytes);

            //Prime the loop by 'reducing' the numOfBytes by the first increment for the first sample 
            numOfBytes = numOfBytes - bytesPerSample;

            // Used for the imbeded loop to move the complete sample
            int q = 0;

            // Moves through the stream based on each sample
            for (int i = 0; i < numOfBytes - bytesPerSample; i = i + bytesPerSample)
            {
                // Location of streamBuffer1 position, in the reversal process
                // Effectively a mirroing process; b will equal i (or be out by one if its an equal buffer)
                // when the middle of the buffer is reached.
                b = numOfBytes - bytesPerSample - i;

                // Copies the 'sample' in whole to the opposite end of streamBuffer1
                for (q = 0; q <= bytesPerSample; q++)
                {
                    streamBuffer1[b + q] = streamBuffer0[i + q];
                }
            }

            // Writes back the reversed stream to the origional sample buffer
            //dsInterface.aSound[sampleToReverse].Write(0, stream1, numOfBytes, Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);

            stream1.Read(sampleToReverse, 0, numOfBytes);

            return sampleToReverse;
            //return true;
        }


        private void CreateStreamBuffers()
        {
            streamBuffer0 = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; i++)
                streamBuffer0[i] = 0;



            streamBuffer1 = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; i++)
                streamBuffer1[i] = 0;

        }

        private void CreateStreams()
        {
            stream0 = new MemoryStream(streamBuffer0);
            stream1 = new MemoryStream(streamBuffer1);
        }



    }
}
