using Covid19Observer.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Covid19Observer.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CovidCasesController : ControllerBase
    {
        [HttpGet("top/confirmed")]
        [ProducesResponseType(200, Type = typeof(ConfirmedCasesResponse))]
        [ProducesResponseType(400)]
        public IActionResult Get(string date, int max_results)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }

            
            if (!DateTime.TryParse(date, out DateTime observationDate))
            {
                return BadRequest("Invalid Observation Date.");
            }

            if (max_results < 0)
            {
                return BadRequest("Invalid maximum number of results. Please provide a valid number.");
            }

            if (max_results == 0)
            {
                var emptyConfirmedCasesResponse = new ConfirmedCasesResponse(observationDate);
                return Ok(emptyConfirmedCasesResponse);
            }

            var service = new ConfirmedCasesService();
            var observations = service.GetConfirmedCases(observationDate, max_results)
                                    .Select(x => new CountryObservation
                                    {
                                        country = x.Country,
                                        confirmed = x.Confirmed,
                                        deaths = x.Deaths,
                                        recovered = x.Recovered
                                    }).ToList();
            
            return Ok(new ConfirmedCasesResponse(observationDate, observations));
        }
    }
}
