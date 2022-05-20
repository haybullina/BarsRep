using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Parsing
{
    class Program
    {
        static Stopwatch stopwatch = new Stopwatch();
        static int tempName = 0;
        static StringBuilder builder = new StringBuilder();
        static object locker = new();

        static void Main(string[] args)
        {
            string path = @"text";
            Task[] tasks;
            stopwatch.Start();

            if (Directory.Exists(path))
            {
                string[] filesArr = Directory.GetFiles(path);

                tasks = new Task[filesArr.Length];

                foreach (var file in filesArr)
                {
                    tasks = new[] {Task.Run(() => { ParsingText(file); })};
                }

                Task.WaitAll(tasks);

                builder.Append(File.ReadAllText($"new\\{tempName++}.txt"));
                File.WriteAllText("new\\fileOutput.txt", builder.ToString());

                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);
            }
        }

        static void ParsingText(string path)
        {
            //stopwatch.Start();
            lock (locker)
            {
                string[] arrSplit = new string[] {"=", ";"};
                using (StreamWriter newFile = new StreamWriter($"new\\{tempName++}.txt"))
                {
                    using (StreamReader file = new StreamReader(path))
                    {
                        while (true)
                        {
                            string temp = file.ReadLine();

                            if (temp == null) break;

                            string[] arrayTemp = temp.Split(arrSplit, StringSplitOptions.RemoveEmptyEntries);

                            if (temp.StartsWith("Play"))
                            {
                                string dateTime = (DateTime.Parse(arrayTemp[6]) - DateTime.Parse(arrayTemp[4]))
                                    .Seconds.ToString();
                                newFile.WriteLine($"{arrayTemp[2]}, {arrayTemp[4]}, {dateTime}");
                            }
                        }
                    }
                    
                }
                //stopwatch.Stop();
            }
        }
    }
}