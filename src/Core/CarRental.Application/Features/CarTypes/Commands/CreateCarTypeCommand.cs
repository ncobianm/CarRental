using CarRental.Application.Common.Models;
using CarRental.Application.Features.CarTypes.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Features.CarTypes.Commands
{
    public class CreateCarTypeCommand : IRequest<(Result Result, CarTypeDto CarType)>
    {
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraDayPrice { get; set; }


        public class Handler : IRequestHandler<CreateCarTypeCommand, (Result Result, CarTypeDto CarType)>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<(Result Result, CarTypeDto CarType)> Handle(CreateCarTypeCommand request, CancellationToken cancellationToken)
            {
                CarType carType = new CarType
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    BasePrice = request.BasePrice,
                    ExtraDayPrice = request.ExtraDayPrice,
                    LoyaltyPoints = request.LoyaltyPoints,
                };

                _unitOfWork.CarTypeRepository.Add(carType);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return (Result.Failure("An error has occurred while creating the car type"), null);

                return (Result.Success(), new CarTypeDto
                {
                    Id = carType.Id,
                    Name = carType.Name,
                    BasePrice = carType.BasePrice,
                    LoyaltyPoints = carType.LoyaltyPoints,
                    ExtraDayPrice = carType.ExtraDayPrice,
                });
            }
        }
    }
}
