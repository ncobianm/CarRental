using CarRental.Application.Common.Models;
using CarRental.Application.Features.Cars.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Features.Cars.Commands
{
    public class CreateCarCommand : IRequest<(Result Result, CarDto Car)>
    {
        public Guid CarTypeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }


        public class Handler : IRequestHandler<CreateCarCommand, (Result Result, CarDto Car)>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<(Result Result, CarDto Car)> Handle(CreateCarCommand request, CancellationToken cancellationToken)
            {
                Car car = new Car
                {
                    Id = Guid.NewGuid(),
                    Brand = request.Brand,
                    Model = request.Model,
                    PlateNumber = request.PlateNumber,
                    CarTypeId = request.CarTypeId
                };

                _unitOfWork.CarRepository.Add(car);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return (Result.Failure("An error has occurred while creating the car"), null);

                return (Result.Success(), new CarDto
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    PlateNumber = car.PlateNumber,
                    CarTypeId = car.CarTypeId
                });
            }
        }
    }
}
