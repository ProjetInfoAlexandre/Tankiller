using System;

namespace Tankiller
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Tankiller game = new Tankiller())
            {
                game.Run();
            }
        }
    }
#endif
}

