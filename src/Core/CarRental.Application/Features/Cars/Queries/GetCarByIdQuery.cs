using CarRental.Application.Features.Cars.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries
{
    public class GetCarByIdQuery : IRequest<CarDto>
    {
        public Guid Id { get; set; }

        public GetCarByIdQuery(Guid id) => Id = id;

        public class Handler : IRequestHandler<GetCarByIdQuery, CarDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
            {
                var car = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);

                if (car == null) return null;

                return new CarDto
                {
                    Id = request.Id,
                    Brand = car.Brand,
                    CarTypeId = car.CarTypeId,
                    Model = car.Model,
                    PlateNumber = car.PlateNumber
                };
            }
        }
    }
}
