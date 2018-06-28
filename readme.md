# Purge Old Files

A small tool to delete files older than x number of days and to optionally delete any folders that have become empty.
You can download the source and hit build, or the latest exe is available here: XXXXXXX

####  !!! USING THIS WILL DELETE FILES! PLEASE BE CAREFUL - MAKE BACKUPS !!!
#### !!! ENSURE YOU TEST WITH `--test` FIRST !!!
---
### Prerequisites

This requires .net 4.6 to be installed.

## Examples

__NOTE:__ Append `--test` to get a list of the files/folders THAT WOULD BE deleted WITHOUT actually deleting them,
   `PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --created --test`

To delete all files with a _modified date_ older than 7 days from the folder and it's sub-folders:
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --modified`
   
To delete all files with a _created date_ older than 7 days from the folder and it's sub-folders
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --created`

To delete all files older than(modified|created)  7 days from the folder and it's sub-folders
**AND** delete any folders that don't have files in anymore
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7  [--modified|--created] --deleteEmptyFolders`


## Arguments


|Flag|Details|
|---|---|
|folder to clean (pos. 0)    |*Required*. Folder to work on.|
|-d, --days                  |*Required*. Delete all files older then x days|
|--created                   |**(Default: false)** Use file Created date|
|--modified                  |**(Default: true)** Use file Modified date|
|--deleteemptyfolders        |**(Default: false)** Delete Empty Folders if empty after deleting old folders|
|--test                      |**(Default: false)** Test run only - don't delete anything!|
|--help                      |Display this help screen.|
|--version                   |Display version information.|

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

