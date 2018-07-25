using System;
using System.Collections.Generic;

namespace PurgeOldFiles.CommandLine
{
    internal static class ConsoleHelpers
    {
        internal static void WriteErrors(IReadOnlyList<string> errors)
        {
            try
            {

                Console.ForegroundColor = ConsoleColor.Red;

                foreach (var error in errors)
                    Console.WriteLine(error);

            }
            finally
            {
                // let's make sure we don't leave the console text red!
                Console.ResetColor();
            }

        }

    }
}