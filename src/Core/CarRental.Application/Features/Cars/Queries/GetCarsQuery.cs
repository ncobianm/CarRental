using CarRental.Application.Features.Cars.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries
{
    public class GetCarsQuery : IRequest<IEnumerable<CarDto>>
    {
        public class Handler : IRequestHandler<GetCarsQuery, IEnumerable<CarDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CarDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
            {
                var cars = await _unitOfWork.CarRepository.GetAllAsync();

                return cars.Select(x => new CarDto
                {
                    Id = x.Id,
                    PlateNumber = x.PlateNumber,
                    Model = x.Model,
                    CarTypeId = x.CarTypeId,
                    Brand = x.Brand
                });
            }
        }
    }
}
