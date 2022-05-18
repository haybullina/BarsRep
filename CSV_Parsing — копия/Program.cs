using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace CSV_Parsing
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arrSplit = new string[] {"=", ";"};
            Stopwatch stopwatch = new Stopwatch();
            string path = @"text";
            string newPath;

            if (Directory.Exists(path))
            {
                string[] filesArr = Directory.GetFiles(path);
                foreach (var value in filesArr)
                {
                    newPath = Console.ReadLine();
                    ThreadPool.QueueUserWorkItem(callBack => ParsingText(value, newPath));
                }
                /*
                foreach (var value in filesArr)
                {
                    newPath = Console.ReadLine();
                    using (StreamWriter newFile = new StreamWriter($"{newPath}.txt"))
                    {
                        stopWatch.Start();
                        using (StreamReader file = new StreamReader(value))
                        {
                            while (true)
                            {
                                string temp = file.ReadLine();

                                if (temp == null) break;

                                string[] arrayTemp = temp.Split(arrSplit, StringSplitOptions.RemoveEmptyEntries);
                                if (temp.Split()[0] == "Play")
                                {
                                    string dateTime = (DateTime.Parse(arrayTemp[6]) - DateTime.Parse(arrayTemp[4]))
                                        .Seconds.ToString();
                                    newFile.Write($"{arrayTemp[2]}, {arrayTemp[4]}, {dateTime}");
                                    newFile.WriteLine();
                                }
                            }
                        }
                        stopWatch.Stop();
                        
                    }*/
                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.Elapsed);
            }

            static void ParsingText(string path, string newPath)
            {
                string[] arrSplit = new string[] {"=", ";"};
                Stopwatch stopWatch = new Stopwatch();

                using (StreamWriter newFile = new StreamWriter($"{newPath}.txt"))
                {
                    using (StreamReader file = new StreamReader(path))
                    {
                        while (true)
                        {
                            string temp = file.ReadLine();

                            if (temp == null) break;

                            string[] arrayTemp = temp.Split(arrSplit, StringSplitOptions.RemoveEmptyEntries);
                            if (temp.Split()[0] == "Play")
                            {
                                string dateTime = (DateTime.Parse(arrayTemp[6]) - DateTime.Parse(arrayTemp[4]))
                                    .Seconds.ToString();
                                newFile.Write($"{arrayTemp[2]}, {arrayTemp[4]}, {dateTime}");
                                newFile.WriteLine();
                            }
                        }
                    }
                }
            }
        }
    }
}