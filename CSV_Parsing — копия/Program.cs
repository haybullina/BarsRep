using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSV_Parsing
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arrSplit = new string[] {"=", ";"};
            Stopwatch stopwatch = new Stopwatch();
            string path = @"text";
            string newName;
            string[] newNames;
            Task[] tasks;

            if (Directory.Exists(path))
            {
                string[] filesArr = Directory.GetFiles(path);
                
                tasks = new Task[filesArr.Length];
                newNames = new string[filesArr.Length];
                for (int i = 0; i < newNames.Length; ++i)
                {
                    newNames[i] = Console.ReadLine();
                }

                stopwatch.Start();

                for (var i = 0; i < tasks.Length; ++i)
                {
                    tasks[i] = Task.Run(() =>
                    {
                        if (i < tasks.Length)
                        {
                            ParsingText(filesArr[i], newNames[i]);
                        }
                    });
                }
                Task.WaitAll(tasks);
                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);
            }

            static void ParsingText(string path, string newName)
            {
                string[] arrSplit = new string[] {"=", ";"};

                using (StreamWriter newFile = new StreamWriter($"{newName}.txt"))
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