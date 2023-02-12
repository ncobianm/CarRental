using CarRental.Application.Features.Cars.Commands;
using CarRental.Application.Features.Cars.DTO;
using CarRental.Application.Features.Cars.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIClient.Controllers
{
    public class CarController : AppBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            var cars = await Mediator.Send(new GetCarsQuery());
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(Guid id)
        {
            var car = await Mediator.Send(new GetCarByIdQuery(id));
            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCar(CreateCarCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.Car);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateCar(UpdateCarCommand cmd)
        {
            var updateResult = await Mediator.Send(cmd);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            var deleteResult = await Mediator.Send(new DeleteCarCommand(id));
            if (!deleteResult.Succeeded) return BadRequest(deleteResult.Errors);

            return Ok();
        }
    }
}
