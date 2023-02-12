using CarRental.Application.Common.Models;
using CarRental.Application.Features.Customers.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Features.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<(Result Result, CustomerDto Customer)>
    {
        public string Name { get; set; }


        public class Handler : IRequestHandler<CreateCustomerCommand, (Result Result, CustomerDto Customer)>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<(Result Result, CustomerDto Customer)> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                };

                _unitOfWork.CustomerRepository.Add(customer);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return (Result.Failure("An error has occurred while creating the customer"), null);

                return (Result.Success(), new CustomerDto
                {
                    Id = customer.Id,
                    Name = request.Name,
                });
            }
        }
    }
}
