using System;

namespace PurgeOldFiles.CommandLine
{
    public static class Info
    {
        private static readonly string EXE_NAME = AppDomain.CurrentDomain.FriendlyName;

        public static string GetUsageInfo()
            => "Usage:" + Environment.NewLine 
               + @"More details: https://gitlab.dev.haus/Mark/PurgeOld/blob/master/readme.md" + Environment.NewLine
               + Environment.NewLine

               + "== To Do A Test Run:" + Environment.NewLine
               + "NOTE: Append --test to get a list of the files/folders THAT WOULD BE deleted WITHOUT actually deleting them, eg:" + Environment.NewLine
               + "\t" + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7  --test --created --allEmptyFolders" + Environment.NewLine

               + Environment.NewLine

               + "== Deleting Old files:" + Environment.NewLine
               + "Delete all files with a modified date older than 7 days from the folder and any empty sub-folders" + Environment.NewLine
               + "\t" + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7 --modified --allEmptyFolders" + Environment.NewLine

               + Environment.NewLine

               + "Delete all files with a created date older than 7 days from the folder and any emptied sub-folders" + Environment.NewLine
               + "\t" + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7 --created --emptiedFolders" + Environment.NewLine

               + Environment.NewLine

                + "== Deleting Empty Folders:" + Environment.NewLine
                + " -emptiedFolders \t deletes (now empty) folders that were cleared of old files" + Environment.NewLine
                + " -allEmptyFolders \t deletes all empty folders under the chosen folder, regardless of if they had old files in or not" + Environment.NewLine
                + " -noDeleteFolders \t don't delete any folders, empty or otherwise!" + Environment.NewLine

                 + Environment.NewLine

               + "Delete all files older than(modified|created)  7 days from the folder and any empty sub-folders " + Environment.NewLine
               + "AND delete any folders that don't have files in anymore" + Environment.NewLine
               + "\t" + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7  --modified --allEmptyFolders" + Environment.NewLine

                + Environment.NewLine

                + "Delete all files older than(modified|created)  7 days from the folder and it's sub-folders " + Environment.NewLine
                + "AND delete any folders that don't have files in anymore" + Environment.NewLine
                + "\t" + EXE_NAME + " \"c:\\SomeFolder\\withOldFiles\" -d 7  --modified --emptiedFolders" + Environment.NewLine;
    }

}
