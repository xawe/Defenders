using System;

namespace Defenders
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DefendersGame())
                game.Run();
        }
    }
}
