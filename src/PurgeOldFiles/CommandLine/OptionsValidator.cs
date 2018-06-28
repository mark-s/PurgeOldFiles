using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PurgeOldFiles.CommandLine
{
    public static class OptionsValidator
    {

        public static (bool result, List<string> errors) IsValid(Options options)
        {
            var errors = new List<string>();

            var pathCheck = CheckPath(options.Folder);
            if(pathCheck.isValid == false)
                errors.Add(pathCheck.message);

            if(options.DaysBefore <= 0)
                errors.Add("Days must be a positive number");

            return (!errors.Any(), errors);
        }

        private static (bool isValid, string message) CheckPath(string path)
        {
            try
            {
                return Directory.Exists(path) == false
                    ? (false, $"Can't find path: [{path}]")
                    : (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to access path: [{path}] [{ex.Message}]");
            }

        }

    }
}