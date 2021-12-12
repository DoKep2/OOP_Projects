using System;
using System.IO;
using System.Runtime.Serialization;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Logers
{
    /*[DataContract]*/
    public class ConsoleLoger : ILoger
    {
        public ConsoleLoger(bool prefixMode)
        {
            PrefixMode = prefixMode;
        }

        /*[DataMember]*/
        public bool PrefixMode { get; }
        public void LogInfo(string info)
        {
            if (PrefixMode)
            {
                Console.Write($"{DateTime.Now} ");
            }

            Console.WriteLine(info);
        }
    }
}