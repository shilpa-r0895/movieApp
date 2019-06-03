using Microsoft.AspNetCore.Mvc;
using MovieApp.Helpers;
using MovieApp.Helpers.Interfaces;
using System;

namespace MovieApp.Controllers
{
    [Route("[controller]")]
    public class ProducerController : Controller
    {
        private IPersonHelper _personHelper;

        public ProducerController(IPersonHelper personHelper)
        {
            _personHelper = personHelper;
        }

        [HttpGet]
        public IActionResult GetAllProducers()
        {
            return Ok(_personHelper.GetAllProducers());
        }

        [HttpGet("{producerId}")]
        public IActionResult GetProducer(Guid producerId)
        {
            if (producerId == null)
            {
                return BadRequest();
            }

            var Producer = _personHelper.GetPerson(producerId);

            if (Producer != null)
            {
                return Ok(Producer);
            }
            else
            {
                return NotFound(new { message = ErrorMessages.PRODUCER_NOT_FOUND });
            }
        }

        [HttpPost]
        public IActionResult AddProducer([FromBody]Model.RequestModel.AddPerson producer)
        {
            if (producer == null)
            {
                return BadRequest(new { message = "Please check input." });
            }

            var result = _personHelper.AddProducer(producer);

            if (Guid.TryParse(result, out Guid Id))
            {
                return Ok(_personHelper.GetPerson(Id));
            }
            else if (result.Equals(ErrorMessages.PRODUCER_ALREADY_EXISTS) ||
               result.Equals(ErrorMessages.NAME_EMPTY) ||
               result.Equals(ErrorMessages.SEX_INVALID) ||
               result.Equals(ErrorMessages.SEX_EMPTY) ||
               result.Equals(ErrorMessages.BIO_EMPTY) ||
               result.Equals(ErrorMessages.DOB_EMPTY) ||
               result.Equals(ErrorMessages.DOB_LIMIT))
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result });
            }
        }

        [HttpPatch]
        public IActionResult EditProducer([FromBody]Model.RequestModel.Person producer)
        {
            if (producer == null)
            {
                return BadRequest(new { message = "Please check input." });
            }

            var result = _personHelper.EditPerson(producer, PersonType.Producer);

            if (Guid.TryParse(result, out Guid Id))
            {
                return Ok(_personHelper.GetPerson(Id));
            }
            else if (result.Equals(ErrorMessages.ACTOR_NOT_FOUND) ||
              result.Equals(ErrorMessages.NAME_EMPTY) ||
              result.Equals(ErrorMessages.SEX_INVALID) ||
              result.Equals(ErrorMessages.BIO_EMPTY) ||
              result.Equals(ErrorMessages.DOB_INVALID) ||
              result.Equals(ErrorMessages.DOB_LIMIT))
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result });
            }
        }

        [HttpDelete("{producerId}")]
        public IActionResult DeleteProducer(Guid producerId)
        {
            if (producerId == Guid.Empty)
            {
                return BadRequest(new { message = "Please check input." });
            }

            var result = _personHelper.DeletePerson(producerId, PersonType.Producer);

            if (result.Equals(String.Empty))
            {
                return Ok(new { message = "Producer deleted." });
            }
            else if (result.Equals(ErrorMessages.PRODUCER_NOT_FOUND) ||
                result.Equals(ErrorMessages.PRODUCER_MAPPED))
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result });
            }
        }
    }
}
