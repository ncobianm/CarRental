using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypePrices.Commands
{
    public class DeleteCarTypePriceCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteCarTypePriceCommand(Guid id) => Id = id;

        public class Handler : IRequestHandler<DeleteCarTypePriceCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(DeleteCarTypePriceCommand request, CancellationToken cancellationToken)
            {
                var carTypePrice = await _unitOfWork.CarTypePriceRepository.GetByIdAsync(request.Id);

                if (carTypePrice == null) return Result.Failure("Record not found");

                _unitOfWork.CarTypePriceRepository.Remove(carTypePrice);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while deleting the car type price");

                return Result.Success();
            }
        }
    }
}
