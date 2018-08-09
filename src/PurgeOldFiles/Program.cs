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
        private const int SUCCESS = 0;
        private const int FAILURE = 1;

        private static int _returnCode = SUCCESS;

        public static int Main(string[] args)
        {

            // If no args, don't display errors - just show the usage info
            if (args == null || args.Any() == false)
            {
                Console.WriteLine(Info.GetUsageInfo());
                _returnCode = FAILURE;
            }
            else
            {
                Parser.Default.ParseArguments<Options>(args)
                                       .WithParsed(options =>
                                                   {
                                                       if (Validate(options))
                                                           Run(options);
                                                   })
                                       .WithNotParsed(errors =>
                                                    {
                                                        Console.WriteLine(Info.GetUsageInfo());
                                                        _returnCode = FAILURE;
                                                    });
            }

#if DEBUG
            Console.ReadKey();
#endif

            return _returnCode;
        }

        private static bool Validate(Options options)
        {
            var validationResult = OptionsValidator.Validate(options);

            if (validationResult.isValid == false)
            {
                ConsoleHelpers.WriteErrors(validationResult.errors);
                _returnCode = FAILURE;
            }

            return validationResult.isValid;
        }

        private static void Run(Options options)
        {
            // 'parse' the commandline options
            var config = new DeleteConfiguration(options);

            // Run the Delete/Purge
            var errors = DeleteService.Delete(config);

            if (errors.Any())
            {
                ConsoleHelpers.WriteErrors(errors);
                _returnCode = FAILURE;
            }

        }


    }
}
