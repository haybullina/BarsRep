using System;

namespace BarsRep
{
    public class Keyboard
    {
        public event EventHandler<char> OnKeyPressed;

        public void Run()
        {
            char ch;
            while ((ch = Console.ReadKey().KeyChar) != 'c')
            {
                OnKeyPressed?.Invoke(this, ch);
            }
        }
    }
}