using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GoodbyeOldFiles
{
    public static class CommandLineHelpers
    {
        const string EXE_NAME = "DelOldFiles.exe";
        const string EXPECTED_DATE_FORMAT = "yyyy-mm-dd";
        private const int SUCCESS = 0;
        private const int FAILURE = 1;


        public static (bool isValid, int returnValue, string message) ValidateAllArguments(string[] args)
        {
            // Check arg count
            if (args.Any() == false || args.Length < 2 || args.Length > 5)
                return (false, FAILURE, GetUsageInfo());

            // check path exists (Assume access permissions ok)
            var path = args[0];
            try
            {
                if (Directory.Exists(path) == false)
                    return (false, FAILURE, "Can't find: " + path);
            }
            catch (Exception ex)
            {
                return (false, FAILURE, $"Failed to access path: [{path}] [{ex.Message}]");
            }

            // check date format
            var date = args[1];
            var parseDateResponse = TryParseDate(date);
            if (parseDateResponse.isSuccess == false)
                return (false, FAILURE, parseDateResponse.errorMessage);

            // check created / modified
            var dateCondition = args[2];
            var dateConditionResponse = TryParseDateContition(dateCondition);
            if (dateConditionResponse.isSuccess == false && IsTestFlag(args[2]))
                return (false, FAILURE, dateConditionResponse.errorMessage);

            // check the delete empty folders expected parameter is as expected
            if (args.Length == 4 && string.Equals(args[3], "deleteEmptyFolders", StringComparison.InvariantCultureIgnoreCase) == false)
                return (false, FAILURE, GetUsageInfo());

            return (true, SUCCESS, string.Empty);
        }

        private static (bool isSuccess, DateCondition parsedDate, string errorMessage) TryParseDateContition(string dateCondition)
        {
            if (string.Equals(dateCondition, "modified", StringComparison.InvariantCultureIgnoreCase) == false ||
                string.Equals(dateCondition, "created", StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return (false, DateCondition.ERROR, "Use 'created' or 'modified'");
            }

            return string.Equals(dateCondition, "modified", StringComparison.InvariantCultureIgnoreCase)
                ? (true, DateCondition.Modified, string.Empty)
                : (true, DateCondition.Created, string.Empty);
        }

        private static (bool isSuccess, DateTime parsedDate, string errorMessage) TryParseDate(string date)
        {
            try
            {
                var parsed = DateTime.ParseExact(date, EXPECTED_DATE_FORMAT, CultureInfo.InvariantCulture);
                return (true, parsed, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, DateTime.MaxValue, ex.Message);
            }
        }

        private static bool IsTestFlag(string argument)
        => string.Equals(argument, "test", StringComparison.InvariantCultureIgnoreCase) == false;

        public static string GetUsageInfo()
            => "Usage:"
               + Environment.NewLine
               + EXE_NAME + " -f \"c:\\SomeFolder\\withOldFiles\" -d 7 --modified" + Environment.NewLine
               + "   Will delete all files with a modified date older than 7 days from the folder and it's sub-folders and delete empty folders"
               + Environment.NewLine
               + Environment.NewLine
               + EXE_NAME + " -f \"c:\\SomeFolder\\withOldFiles\" -d 7 --created" + Environment.NewLine
               + "   Will delete all files with a created date older than 7 days from the folder and it's sub-folders and delete empty folders"
               + Environment.NewLine
               + Environment.NewLine
               + EXE_NAME + " -f \"c:\\SomeFolder\\withOldFiles\" -d 7  [--modified|--created] --deleteEmptyFolders" + Environment.NewLine
               + "   Will delete all files older than(modified|created)  7 days from the folder and it's sub-folders " + Environment.NewLine
               + "   AND delete any folders that don't have files in anymore"
               + Environment.NewLine
               + Environment.NewLine
               + "NOTE: Append --test to get a list of the files/folders THAT WOULD BE deleted WITHOUT actually deleting them," +Environment.NewLine
               + "   " + EXE_NAME + " -f \"c:\\SomeFolder\\withOldFiles\" -d 7 --created --test" + Environment.NewLine;


    }
}
