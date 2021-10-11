using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.SharedKernel
{
    /// <summary>
    /// Abstract base controller for command APIs
    /// </summary>
    public abstract class CommandApiBaseController : ControllerBase
    {
        /// <summary>
        /// Execute a command
        /// </summary>
        /// <typeparam name="TIn">Type of input</typeparam>
        /// <param name="input">The command input</param>
        /// <param name="operation">The command operation</param>
        /// <returns>API command result</returns>
        public async Task<IActionResult> ExecuteCommandAsync<TIn>(TIn input, Func<TIn, Task> operation)
        {
            try
            {
                await operation(input);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }
            
            return new OkResult();
        }
    }
}
