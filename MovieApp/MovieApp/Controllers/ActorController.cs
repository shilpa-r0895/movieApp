using Microsoft.AspNetCore.Mvc;
using MovieApp.Helpers;
using MovieApp.Helpers.Interfaces;
using System;

namespace MovieApp.Controllers
{
    [Route("[controller]")]
    public class ActorController : Controller
    {
        private IPersonHelper _personHelper;

        public ActorController(IPersonHelper personHelper)
        {
            _personHelper = personHelper;
        }

        [HttpGet]
        public IActionResult GetAllActors()
        {
            return Ok(_personHelper.GetAllActors());
        }

        [HttpGet("{actorId}")]
        public IActionResult GetActor(Guid actorId)
        {
            if (actorId == null)
            {
                return BadRequest(new { message = "Please check input." });
            }

            var actor = _personHelper.GetPerson(actorId);

            if (actor != null)
            {
                return Ok(actor);
            }
            else
            {
                return NotFound(new { message = ErrorMessages.ACTOR_NOT_FOUND });
            }
        }

        [HttpPost]
        public IActionResult AddActor([FromBody]Model.RequestModel.AddPerson actor)
        {
            if (actor == null)
            {
                return BadRequest(new { message = "Please check input." });
            }
            var result = _personHelper.AddActor(actor);

            if (Guid.TryParse(result, out Guid Id))
            {
                return Ok(_personHelper.GetPerson(Id));
            }
            else if (result.Equals(ErrorMessages.ACTOR_ALREADY_EXISTS) ||
               result.Equals(ErrorMessages.NAME_EMPTY) ||
               result.Equals(ErrorMessages.SEX_INVALID) ||
               result.Equals(ErrorMessages.SEX_EMPTY) ||
               result.Equals(ErrorMessages.BIO_EMPTY) ||
               result.Equals(ErrorMessages.DOB_EMPTY) ||
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

        [HttpPatch]
        public IActionResult EditActor([FromBody]Model.RequestModel.Person actor)
        {
            if (actor == null)
            {
                return BadRequest(new { message = "Please check input." });
            }

            var result = _personHelper.EditPerson(actor, PersonType.Actor);

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

        [HttpDelete("{actorId}")]
        public IActionResult DeleteActor(Guid actorId)
        {
            if (actorId == Guid.Empty)
            {
                return BadRequest(new { message = "Please check input." });
            }

            var result = _personHelper.DeletePerson(actorId, PersonType.Actor);

            if (result.Equals(String.Empty))
            {
                return Ok(new { message = "Actor deleted." });
            }
            else if (result.Equals(ErrorMessages.ACTOR_NOT_FOUND) ||
                result.Equals(ErrorMessages.ACTOR_MAPPED))
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
