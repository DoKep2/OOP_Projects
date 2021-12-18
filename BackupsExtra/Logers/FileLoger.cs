using System;
using System.IO;
using System.Runtime.Serialization;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Logers
{
    /*[DataContract]*/
    public class FileLoger : ILoger
    {
        public static readonly string LogFile = @"C:\Users\sergo\Desktop\logFile.txt";

        public FileLoger(bool prefixMode)
        {
            PrefixMode = prefixMode;
        }

        /*[DataMember]*/

        public bool PrefixMode { get; }
        public void LogInfo(string info)
        {
            StreamWriter writer = File.AppendText(LogFile);
            if (PrefixMode == true)
            {
                writer.Write($"{DateTime.Now} ");
            }

            writer.WriteLine(info);
            writer.Close();
        }
    }
}