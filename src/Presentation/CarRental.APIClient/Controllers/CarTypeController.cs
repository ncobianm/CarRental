using CarRental.Application.Features.CarTypes.Commands;
using CarRental.Application.Features.CarTypes.DTO;
using CarRental.Application.Features.CarTypes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIClient.Controllers
{
    public class CarTypeController : AppBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarTypeDto>>> GetCarTypes()
        {
            var carTypes = await Mediator.Send(new GetCarTypesQuery());
            return Ok(carTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarTypeDto>> GetCarType(Guid id)
        {
            var carType = await Mediator.Send(new GetCarTypeByIdQuery(id));
            return Ok(carType);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCarType(CreateCarTypeCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.CarType);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateCarType(UpdateCarTypeCommand cmd)
        {
            var updateResult = await Mediator.Send(cmd);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarType(Guid id)
        {
            var deleteResult = await Mediator.Send(new DeleteCarTypeCommand(id));
            if (!deleteResult.Succeeded) return BadRequest(deleteResult.Errors);

            return Ok();
        }
    }
}
