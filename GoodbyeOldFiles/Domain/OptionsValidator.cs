using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoodbyeOldFiles.Domain
{
    public static class OptionsValidator
    {

        private static readonly List<string> _errors = new List<string>();

        public static (bool result, List<string> errors) IsValid(Options options)
        {
            _errors.Clear();

            var pathCheck = CheckPath(options.Folder);
            if(pathCheck.isValid == false)
                _errors.Add(pathCheck.message);

            if(options.DaysBefore <= 0)
                _errors.Add("Days must be a positive number");

            return (!_errors.Any(), _errors);
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