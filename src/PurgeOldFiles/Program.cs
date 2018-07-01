// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE COPYRIGHT HOLDERS OR ANYONE
// DISTRIBUTING THE SOFTWARE BE LIABLE FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF
// OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using PurgeOldFiles.CommandLine;
using PurgeOldFiles.Domain;

namespace PurgeOldFiles
{
    public class Program
    {
        private static int _returnCode; // 0 - success, 1 - error

        public static int Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args)
                                   .WithParsed(options =>{Validate(options);
                                                                           Run(options);})
                                   .WithNotParsed(errors => Console.WriteLine(Helper.GetUsageInfo()));

            Console.ReadKey();

            return _returnCode;
        }

        private static void Validate(Options options)
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
            }
        }

        private static void Run(Options options)
        {
            List<string> errors = new List<string>();

            if (options.DeleteEmptiedFolders)
                errors = DeleteService.DeleteAndCleanEmptiedFolders(options);

            if (options.DeleteAllEmptyFolders)
                errors = DeleteService.DeleteAndCleanAllEmptyFolders(options);

            if (options.DeleteEmptiedFolders == false)
                errors = DeleteService.DeleteButLeaveEmptyFolders(options);

            

            Console.ForegroundColor = ConsoleColor.Red;
            {
                errors.ForEach(Console.WriteLine);
            }
            Console.ResetColor();

            if (errors.Any())
                _returnCode = 1;

        }
    }

}
