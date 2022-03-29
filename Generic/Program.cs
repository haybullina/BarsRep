using System;

namespace Generic
{
    class Program
    {
        static void Main(string[] args)
        {
            var intLogger = new LocalFileLogger<int>("logs.txt");
            intLogger.LogInfo("intLogger LogInfo");
            intLogger.LogWarning("intLogger LogWarning");
            intLogger.LogError("intLogger LogError", new NullReferenceException());
            
            var strLogger = new LocalFileLogger<string>("logs.txt");
            strLogger.LogInfo("string");
            strLogger.LogWarning("string");
            strLogger.LogError("string", new NullReferenceException());

            var boolLogger = new LocalFileLogger<bool>("logs.txt");
            boolLogger.LogInfo("boolLogger LogInfo");
            boolLogger.LogWarning("boolLogger LogWarning");
            boolLogger.LogError("boolLogger LogError", new NullReferenceException());
        }
    }
}
