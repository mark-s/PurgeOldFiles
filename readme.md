# Purge Old Files
A small tool to delete files older than x number of days and to optionally delete any folders that have become/are empty.  
You can download the source and hit build, or the latest exe is available here: [https://gitlab.dev.haus/Mark/PurgeOld/raw/master/releases/PurgeOldFiles.zip]

**! USING THIS WILL DELETE FILES**  
**! PLEASE BE CAREFUL - MAKE BACKUPS**  
**! ENSURE YOU TEST WITH `--test` FIRST**

## Prerequisites
This requires .net 4.6 to be installed.

## Notes
`--test` will not delete anything and only show a list of what would have been deleted (watch for the two dashes `--` before test!)  
**Do this before you actually delete any files so you can check the dates etc are correct and the results are as expected**   

`-d` or `--days` is used like: `-d 7`  eg: *delete files older than 7 days*  

**You must chose one of these:**  
`--created`  checks the **creation date** of the files.   
`--modified` checks the **last modified date** of the files. 

**And chose one of these:**  
`--allEmptyFolders` will delete **all** empty folders found after deleteing the old files.  
`--emptiedFolders` will clean up **emptied** folders after deleting old files from them, and ignore any empty folders that were there and empty before we ran the program.  
`--noDeleteFolders` doesn't delete any folders - empty or otherwise!

## Examples

### Testing (DO THIS FIRST):
+ Just show me what would be deleted, but don't delete anything! [**`--test`**]  
 `PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" --test -d 7 --created --deleteEmptyFolders`

### Deleting old files and handling empty folders:

+ Delete files with a **modified date** older than 7 days and ALL empty sub-folders:  
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --modified --deleteEmptyFolders`
   
+ Delete files with a **created date** older than 7 days and ALL empty sub-folders:   
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --created --deleteEmptyFolders`

+ Delete old files and any **emptied sub-folders**:  
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --created --emptiedFolders`

+ Delete old files but **don't delete any folders**  
`PurgeOldFiles.exe "c:\SomeFolder\withOldFiles" -d 7 --created --noDeleteFolders`


## All Options


|Flag|Details|
|---|---|
|folder to clean    |*Required*. Folder to work on. Must be the first argument|
|-d, --days         |*Required*. Delete all files older then x days|
|--test             |**(Default: false)** Test run only - don't delete anything!|
|--help             |Display this help screen.|
|--version          |Display version information.|
|One of the following is *required*|
|--created          |**(Default: false)** Use file Created date|
|--modified         |**(Default: true)** Use file Modified date|
|One of the following is *required*|
|--emptiedFolders   |*Required*. **(Default: false)** Delete Empty Folders if empty after deleting old folders|
|--allEmptyFolders  |*Required*. **(Default: false)** Delete Empty Folders if empty after deleting old folders|
|--noDeleteFolders  |*Required*. **(Default: false)** Delete Empty Folders if empty after deleting old folders|


___

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

# Disclaimer

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.