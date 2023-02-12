using CarRental.Application.Common.Models;
using CarRental.Application.Features.CarTypePrices.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Features.CarTypePrices.Commands
{
    public class CreateCarTypePriceCommand : IRequest<(Result Result, CarTypePriceDto CarTypePrice)>
    {
        public Guid CarTypeId { get; set; }
        public int? MinDay { get; set; }
        public int? MaxDay { get; set; }
        public decimal Price { get; set; }

        public class Handler : IRequestHandler<CreateCarTypePriceCommand, (Result Result, CarTypePriceDto CarTypePrice)>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<(Result Result, CarTypePriceDto CarTypePrice)> Handle(CreateCarTypePriceCommand request, CancellationToken cancellationToken)
            {
                CarTypePrice carType = new CarTypePrice
                {
                    Id = Guid.NewGuid(),
                    CarTypeId = request.CarTypeId,
                    MinDay = request.MinDay,
                    MaxDay = request.MaxDay,
                    Price = request.Price
                };

                _unitOfWork.CarTypePriceRepository.Add(carType);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return (Result.Failure("An error has occurred while creating the car type price"), null);

                return (Result.Success(), new CarTypePriceDto
                {
                    Id = carType.Id,
                    CarTypeId = request.CarTypeId,
                    MinDay = request.MinDay,
                    MaxDay = request.MaxDay,
                    Price = request.Price
                });
            }
        }
    }
}
