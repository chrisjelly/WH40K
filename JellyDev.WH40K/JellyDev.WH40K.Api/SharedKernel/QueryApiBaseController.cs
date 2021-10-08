using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.SharedKernel
{
    /// <summary>
    /// Abstract base controller for query APIs
    /// </summary>
    public class QueryApiBaseController : ControllerBase
    {
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <typeparam name="TIn">Type of input</typeparam>
        /// <typeparam name="TOut">Type of ouput</typeparam>
        /// <param name="input">The query input</param>
        /// <param name="operation">The query operation</param>
        /// <returns>Task</returns>
        public async Task<IActionResult> ExecuteQueryAsync<TIn, TOut>(TIn input, Func<TIn, Task<TOut>> operation)
        {
            TOut results = default(TOut);
            try
            {
                results = await operation(input);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }

            return new OkObjectResult(results);
        }
    }
}
