﻿using System;

namespace GoodbyeOldFiles
{
    public static class CommandLineHelpers
    {
        const string EXE_NAME = "DelOldFiles.exe";

        public static string GetUsageInfo()
            => "Usage:"
               + Environment.NewLine
               + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7 --modified" + Environment.NewLine
               + "   Will delete all files with a modified date older than 7 days from the folder and it's sub-folders and delete empty folders"
               + Environment.NewLine
               + Environment.NewLine
               + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7 --created" + Environment.NewLine
               + "   Will delete all files with a created date older than 7 days from the folder and it's sub-folders and delete empty folders"
               + Environment.NewLine
               + Environment.NewLine
               + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7  [--modified|--created] --deleteEmptyFolders" + Environment.NewLine
               + "   Will delete all files older than(modified|created)  7 days from the folder and it's sub-folders " + Environment.NewLine
               + "   AND delete any folders that don't have files in anymore"
               + Environment.NewLine
               + Environment.NewLine
               + "NOTE: Append --test to get a list of the files/folders THAT WOULD BE deleted WITHOUT actually deleting them," +Environment.NewLine
               + "   " + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7 --created --test" + Environment.NewLine;


    }
}
