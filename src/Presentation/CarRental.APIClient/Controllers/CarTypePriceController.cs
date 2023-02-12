using CarRental.Application.Features.CarTypePrices.Commands;
using CarRental.Application.Features.CarTypePrices.DTO;
using CarRental.Application.Features.CarTypePrices.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIClient.Controllers
{
    public class CarTypePriceController : AppBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarTypePriceDto>>> GetCarTypes(Guid? carTypeId)
        {
            var carTypes = await Mediator.Send(new GetCarTypePricesQuery() { CarTypeId = carTypeId });
            return Ok(carTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarTypePriceDto>> GetCarTypePrice(Guid id)
        {
            var carType = await Mediator.Send(new GetCarTypePriceByIdQuery(id));
            return Ok(carType);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCarType(CreateCarTypePriceCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.CarTypePrice);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateCarType(UpdateCarTypePriceCommand cmd)
        {
            var updateResult = await Mediator.Send(cmd);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarType(Guid id)
        {
            var deleteResult = await Mediator.Send(new DeleteCarTypePriceCommand(id));
            if (!deleteResult.Succeeded) return BadRequest(deleteResult.Errors);

            return Ok();
        }
    }
}
