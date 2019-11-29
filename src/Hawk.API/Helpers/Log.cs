using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawk.API.Helpers
{
    public static class Log
    {
        public static bool LogAnyErrors(this System.Collections.Generic.IEnumerable<Task<(string id, bool success, string errormessage)>> tasks, ILogger log)
        {
            if (tasks.Any(task => !task.Result.success))
            {
                log.LogError(
                    "Get repositories failed:{0}",
                    tasks.Aggregate(
                        new StringBuilder(),
                        (builder, task) => builder.AppendFormat(
                            "\r\n\tName: {0} Status: {1} Message: {2}",
                            task.Result.id,
                            task.Result.success ? "Succeded" : "Failed",
                            task.Result.errormessage
                            ),
                        builder => builder.ToString()
                        )
                    );

                return true;
            }
            return false;
        }
    }
}
