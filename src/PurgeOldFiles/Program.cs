// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE COPYRIGHT HOLDERS OR ANYONE
// DISTRIBUTING THE SOFTWARE BE LIABLE FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF
// OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
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
                                   .WithParsed(options =>
                                   {
                                       if (Validate(options))
                                           Run(options);
                                   })
                                   .WithNotParsed(errors => Console.WriteLine(Helper.GetUsageInfo()));


#if DEBUG
            Console.ReadKey();
#endif
            return _returnCode;
        }

        private static bool Validate(Options options)
        {
            var validationCheck = OptionsValidator.IsValid(options);

            Console.ForegroundColor = ConsoleColor.Red;
            validationCheck.errors.ForEach(Console.WriteLine);
            Console.ResetColor();

            if (validationCheck.isValid == false)
                _returnCode = 1;

            return validationCheck.isValid;
        }

        private static void Run(Options options)
        {
            var config = new DeleteConfiguration(options);

            var errors = DeleteService.Delete(config);

            Console.ForegroundColor = ConsoleColor.Red;
            errors.ForEach(Console.WriteLine);
            Console.ResetColor();

            if (errors.Any())
                _returnCode = 1;
        }
    }

}
