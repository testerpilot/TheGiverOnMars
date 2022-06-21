using System;

namespace TheGiverOnMars
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TheGiverOnMars())
                game.Run();
        }
    }
}
