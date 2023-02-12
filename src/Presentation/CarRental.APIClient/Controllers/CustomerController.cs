using CarRental.Application.Features.Cars.DTO;
using CarRental.Application.Features.Customers.Commands;
using CarRental.Application.Features.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIClient.Controllers
{
    public class CustomerController : AppBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCustomers()
        {
            var customers = await Mediator.Send(new GetCustomersQuery());
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCustomer(Guid id)
        {
            var customer = await Mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CreateCustomerCommand cmd)
        {
            var createResult = await Mediator.Send(cmd);
            if (!createResult.Result.Succeeded) return BadRequest(createResult.Result.Errors);

            return Ok(createResult.Customer);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateCustomer(UpdateCustomerCommand cmd)
        {
            var updateResult = await Mediator.Send(cmd);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            var deleteResult = await Mediator.Send(new DeleteCustomerCommand(id));
            if (!deleteResult.Succeeded) return BadRequest(deleteResult.Errors);

            return Ok();
        }
    }
}
