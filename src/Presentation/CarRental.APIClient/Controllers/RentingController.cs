using CarRental.Application.Features.Cars.DTO;
using CarRental.Application.Features.Rentings.Commands;
using CarRental.Application.Features.Rentings.DTO;
using CarRental.Application.Features.Rentings.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIClient.Controllers
{
    public class RentingController : AppBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetRentings(Guid? customerId, Guid? carId)
        {
            var rentings = await Mediator.Send(new GetRentingsQuery() { CardId = carId, CustomerId = customerId });
            return Ok(rentings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentingDto>> GetRenting(Guid id)
        {
            var renting = await Mediator.Send(new GetRentingByIdQuery(id));
            return Ok(renting);
        }

        [HttpGet("GetRentalPrice")]
        public async Task<ActionResult<RentalPriceDto>> GetRentalPrice(Guid carId, DateTime startDate, DateTime endDate)
        {
            var rentalPriceDto = await Mediator.Send(new GetRentalPriceQuery(carId, startDate, endDate));
            return Ok(rentalPriceDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRenting(CreateRentingCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.Renting);
        }

        [HttpPost("CreateRentingAndCustomer")]
        public async Task<ActionResult> CreateRentingAndCustomer(CreateRentingAndCustomerCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.Renting);
        }

        [HttpPost("ReturnCar")]
        public async Task<ActionResult> CreateRentingAndCustomer(ReturnCarCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.Renting);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateRenting(UpdateRentingCommand cmd)
        {
            var updateResult = await Mediator.Send(cmd);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRenting(Guid id)
        {
            var deleteResult = await Mediator.Send(new DeleteRentingCommand(id));
            if (!deleteResult.Succeeded) return BadRequest(deleteResult.Errors);

            return Ok();
        }
    }
}
