using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GoogleSpeech
{
    public class GoogleVoice
    {      

      public static String GoogleSpeechRequest(String flacName, int sampleRate)
      {       
        WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v1/recognize?xjerr=1&client=chromium&lang=ru-RU");        
        request.Method = "POST";
        
        byte[] byteArray = File.ReadAllBytes(flacName);

        // Set the ContentType property of the WebRequest.
        request.ContentType = "audio/x-flac; rate=" + sampleRate; //"16000";        
        request.ContentLength = byteArray.Length;

        // Get the request stream.
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length);
        
        dataStream.Close();

        // Get the response.
        WebResponse response = request.GetResponse();
        
        dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.
        string responseFromServer = reader.ReadToEnd();

        // Clean up the streams.
        reader.Close();
        dataStream.Close();
        response.Close();

        return responseFromServer;
      }

      public static String GoogleSpeechRequest(String wavName, String flacName)
      {
        int sampleRate = SoundTools.Wav2Flac(wavName, flacName);
        return GoogleSpeechRequest(flacName, sampleRate);          
      }
    }
}
