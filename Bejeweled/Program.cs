using System;

namespace Bejeweled
{
    /// <summary>
    /// The main class, Autocreated By XNA.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Display())
                game.Run();
        }
    }
}
