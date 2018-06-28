using System;
using System.Collections.Generic;
using CommandLine;
using PurgeOldFiles.Domain;

namespace PurgeOldFiles
{
    public class Program
    {
        private static int _returnCode;

        public static int Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args)
               .WithParsed(ValidateAndRun)
               .WithNotParsed(errors => Console.WriteLine(CommandLineHelpers.GetUsageInfo()));

            return _returnCode;
        }

        private static void ValidateAndRun(Options options)
        {
            // validate the args
            var validationCheck = OptionsValidator.IsValid(options);
            if (validationCheck.result == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                {
                    validationCheck.errors.ForEach(Console.WriteLine);
                }
                Console.ResetColor();

                _returnCode = 1;
                return;
            }

            // go!

            List<string> errors;
            if (options.DeleteEmptyFolders)
                errors = DeleteService.DeleteAndCleanEmptyFolders(options);
            else
                errors =DeleteService.DeleteButLeaveEmptyFolders(options);

            Console.ForegroundColor = ConsoleColor.Red;
            {
                errors.ForEach(Console.WriteLine);
            }
            Console.ResetColor();

        }
    }

}
