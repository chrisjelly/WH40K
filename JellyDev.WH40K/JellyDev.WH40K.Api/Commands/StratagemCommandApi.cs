using JellyDev.WH40K.Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;

namespace JellyDev.WH40K.Api.Commands
{
    [Route("api/v1/stratagem")]
    [ApiController]
    public class StratagemCommandApi : ControllerBase
    {
        /// <summary>
        /// Create Stratagem command service
        /// </summary>
        private readonly IAsyncCommandService<CreateStratagem> _createStratagemCmdSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createStratagemCmdSvc">Create Stratagem command service</param>
        public StratagemCommandApi(IAsyncCommandService<CreateStratagem> createStratagemCmdSvc)
        {
            _createStratagemCmdSvc = createStratagemCmdSvc;
        }

        /// <summary>
        /// Create a stratagem
        /// </summary>
        /// <param name="createStratagemCmd">The command to create the stratagem</param>
        /// <returns>Task</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateStratagem createStratagemCmd)
        {
            await _createStratagemCmdSvc.ExecuteAsync(createStratagemCmd);
            return new OkResult();
        }
    }
}
