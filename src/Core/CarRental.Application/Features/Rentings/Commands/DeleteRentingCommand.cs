using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Rentings.Commands
{
    public class DeleteRentingCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteRentingCommand(Guid id) => Id = id;

        public class Handler : IRequestHandler<DeleteRentingCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(DeleteRentingCommand request, CancellationToken cancellationToken)
            {
                var renting = await _unitOfWork.RentingRepository.GetByIdAsync(request.Id);

                if (renting == null) return Result.Failure("Record not found");

                _unitOfWork.RentingRepository.Remove(renting);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while deleting the renting");

                return Result.Success();
            }
        }
    }
}
