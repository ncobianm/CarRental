using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Cars.Commands
{
    public class UpdateCarCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid CarTypeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }


        public class Handler : IRequestHandler<UpdateCarCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
            {
                var car = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);

                if (car == null) return Result.Failure("Record not found");

                car.CarTypeId = request.CarTypeId;
                car.Brand = request.Brand;
                car.Model = request.Model;
                car.PlateNumber = request.PlateNumber;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while updating the car");

                return Result.Success();
            }
        }
    }
}
