using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypePrices.Commands
{
    public class UpdateCarTypePriceCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid CarTypeId { get; set; }
        public int? MinDay { get; set; }
        public int? MaxDay { get; set; }
        public decimal Price { get; set; }


        public class Handler : IRequestHandler<UpdateCarTypePriceCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateCarTypePriceCommand request, CancellationToken cancellationToken)
            {
                var carTypePrice = await _unitOfWork.CarTypePriceRepository.GetByIdAsync(request.Id);

                if (carTypePrice == null) return Result.Failure("Record not found");

                carTypePrice.CarTypeId = request.CarTypeId;
                carTypePrice.MinDay = request.MinDay;
                carTypePrice.MaxDay = request.MaxDay;
                carTypePrice.Price = request.Price;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while updating the car type price");

                return Result.Success();
            }
        }
    }
}
