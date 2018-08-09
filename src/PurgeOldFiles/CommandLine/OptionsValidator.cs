using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PurgeOldFiles.CommandLine
{
    public static class OptionsValidator
    {

        public static (bool isValid, List<string> errors) Validate(Options options)
        {
            var errors = new List<string>();

            var pathCheck = CheckPath(options.Folder);
            if (pathCheck.isValid == false)
                errors.Add(pathCheck.message);

            if (options.DaysBefore <= 0)
                errors.Add($"Days must be a positive number. (Entered: [{options.DaysBefore} ])");

            var deleteValidation = CheckFolderDeleteChoice(options);
            if (deleteValidation.isValid == false)
                errors.Add(deleteValidation.message);

            var cmValidation = CheckCreatedModifiedChoice(options);
            if (cmValidation.isValid == false)
                errors.Add(cmValidation.message);

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

        private static (bool isValid, string message) CheckFolderDeleteChoice(Options options)
        {
            var delOptions = new List<bool> {options.DeleteAllEmptyFolders,
                                                          options.DeleteEmptiedFolders,
                                                          options.DontDeleteEmptyFolders};

            // check there is one and only one folder delete option selected
            if (delOptions.Count(o => o == true) == 1)
                return (true, string.Empty);
            else
                return (false, "REQUIRED: Chose one folder delete option! [emptiedFolders | allEmptyFolders | noDeleteFolders]");
        }

        private static (bool isValid, string message) CheckCreatedModifiedChoice(Options options)
        {
            var cmOptions = new List<bool> {options.Created,options.Modified};

            // check there is one and only one option selected from created / modified
            if (cmOptions.Count(o => o == true) == 1)
                return (true, string.Empty);
            else
                return (false, "REQUIRED: Chose either: [ created | modified]");
        }
    }
}