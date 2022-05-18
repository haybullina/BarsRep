using System;
using System.Diagnostics;
using System.IO;

namespace CSV_Parsing
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arrSplit = new string[] {"=", ";"};
            Stopwatch stopWatch = new Stopwatch();
            string path = @"text";
            string newPath;

            if (Directory.Exists(path))
            {
                string[] filesArr = Directory.GetFiles(path);
                
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
                                    newFile.Write($"{arrayTemp[2]}, {arrayTemp[4]}, ");
                                    string dateTime = (DateTime.Parse(arrayTemp[6]) - DateTime.Parse(arrayTemp[4]))
                                        .Seconds.ToString();
                                    newFile.Write($"{dateTime}");
                                    newFile.WriteLine();
                                }
                            }
                        }
                        stopWatch.Stop();
                    }
                    Console.WriteLine(stopWatch.Elapsed);
                }
            }
        }
    }
}