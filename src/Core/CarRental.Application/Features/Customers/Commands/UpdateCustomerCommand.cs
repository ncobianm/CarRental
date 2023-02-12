using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public class Handler : IRequestHandler<UpdateCustomerCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);

                if (customer == null) return Result.Failure("Record not found");

                customer.Name = request.Name;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while updating the customer");

                return Result.Success();
            }
        }
    }
}
