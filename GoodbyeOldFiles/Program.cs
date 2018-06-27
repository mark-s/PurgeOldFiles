using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using CommandLine;

namespace GoodbyeOldFiles
{
    public class Program
    {
        public static int Main(string[] args)
        {

            var options = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptionsAndReturnExitCode)
                .WithNotParsed(HandleParseError);









            return 0;
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine(CommandLineHelpers.GetUsageInfo());
        }


        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            if (opts.DeleteEmptyFolders)
                DeleteService.DeleteAndCleanEmptyFolders(opts);
            else
                DeleteService.DeleteMessy(opts);
        }
    }

}
