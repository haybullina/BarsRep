using System;


namespace BarsRep
{
    public class KeyboardSubscriber
    {
        private Keyboard kb = new Keyboard();

        public KeyboardSubscriber()
        {
            kb.OnKeyPressed += PrintChar;
            
            kb.Run();
        }

        private void PrintChar(Object sender, char ch)
        {
            Console.WriteLine($"\n{ch}");
        }
    } 
}