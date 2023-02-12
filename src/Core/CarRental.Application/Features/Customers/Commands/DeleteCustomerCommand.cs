using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteCustomerCommand(Guid id) => Id = id;

        public class Handler : IRequestHandler<DeleteCustomerCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);

                if (customer == null) return Result.Failure("Record not found");

                _unitOfWork.CustomerRepository.Remove(customer);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while deleting the customer");

                return Result.Success();
            }
        }
    }
}
