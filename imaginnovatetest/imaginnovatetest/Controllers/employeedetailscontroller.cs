using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using imaginnovatetest.Models.DTO;
using imaginnovatetest.Interfaces;
using Microsoft.Extensions.Logging;

namespace imaginnovatetest.Controllers
{
    [Route("api/employeedetils")]
    [ApiController]
    public class employeedetailscontroller : ControllerBase
    {
        private readonly Iemployeedetails _iemployeedetails;
        private readonly ILogger<employeedetailscontroller> _logger;
       public employeedetailscontroller(Iemployeedetails iemployeedetails, ILogger<employeedetailscontroller> logger)
        {
            _iemployeedetails = iemployeedetails;
            _logger = logger;
        }

        /// <summary>
        /// End point for save employee details
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveemployee")]
        public async Task<IActionResult> SaveempDetails(employeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string msg = await _iemployeedetails.Saveemployee(employeeDto);
                if (msg == null)
                {
                    return NotFound();
                }
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("getemptaxdetails")]
        public async Task<IActionResult> GetEmptaxdetails(financialyearDto financialyearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var  emptaxdetails = await _iemployeedetails.GetEmptaxdetails(financialyearDto);
                if (emptaxdetails == null)
                {
                    return NotFound();
                }
                return Ok(emptaxdetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

    }
}
