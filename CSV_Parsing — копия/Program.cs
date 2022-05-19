using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CSV_Parsing
{
    class Program
    {
        static Stopwatch stopwatch = new Stopwatch();
        static void Main(string[] args)
        {
            string[] arrSplit = new string[] {"=", ";"};
            string path = @"text";
            string newName = "";
            Task[] tasks;

            if (Directory.Exists(path))
            {
                string[] filesArr = Directory.GetFiles(path);
                
                tasks = new Task[filesArr.Length];
                stopwatch.Start();

                for (var i = 0; i < tasks.Length; ++i)
                {
                    Console.WriteLine(filesArr[i]);
                    newName = Console.ReadLine();
                    
                    tasks[i] = Task.Run(() =>
                    {
                        if (i >= tasks.Length)
                        {
                            i--;
                        }
                        ParsingText(filesArr[i], newName);
                    });
                }
                Task.WaitAll(tasks);
                Console.WriteLine(stopwatch.Elapsed);
            }

            static void ParsingText(string path, string newName)
            {
                stopwatch.Start();
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
                stopwatch.Stop();
            }
        }
    }
}