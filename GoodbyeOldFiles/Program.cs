using System;
using System.Collections.Generic;
using CommandLine;
using GoodbyeOldFiles.Domain;

namespace GoodbyeOldFiles
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
                validationCheck.errors.ForEach(Console.WriteLine);
                _returnCode = 1;
                return;
            }

            // go!

            List<string> errors;
            if (options.DeleteEmptyFolders)
                errors = DeleteService.DeleteAndCleanEmptyFolders(options);
            else
                errors =DeleteService.DeleteButLeaveEmptyFolders(options);

            errors.ForEach(Console.WriteLine);

        }
    }

}
