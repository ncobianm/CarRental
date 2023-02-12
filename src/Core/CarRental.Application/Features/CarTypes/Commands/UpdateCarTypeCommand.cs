using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypes.Commands
{
    public class UpdateCarTypeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraDayPrice { get; set; }


        public class Handler : IRequestHandler<UpdateCarTypeCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateCarTypeCommand request, CancellationToken cancellationToken)
            {
                var carType = await _unitOfWork.CarTypeRepository.GetByIdAsync(request.Id);

                if (carType == null) return Result.Failure("Record not found");

                carType.Name = request.Name;
                carType.BasePrice = request.BasePrice;
                carType.LoyaltyPoints = request.LoyaltyPoints;
                carType.ExtraDayPrice = request.ExtraDayPrice;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while updating the car type");

                return Result.Success();
            }
        }
    }
}
