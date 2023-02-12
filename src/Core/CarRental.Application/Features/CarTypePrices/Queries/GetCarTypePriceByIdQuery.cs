using CarRental.Application.Features.CarTypePrices.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypePrices.Queries
{
    public class GetCarTypePriceByIdQuery : IRequest<CarTypePriceDto>
    {
        public Guid Id { get; set; }

        public GetCarTypePriceByIdQuery(Guid id) => Id = id;

        public class Handler : IRequestHandler<GetCarTypePriceByIdQuery, CarTypePriceDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CarTypePriceDto> Handle(GetCarTypePriceByIdQuery request, CancellationToken cancellationToken)
            {
                var carTypePrice = await _unitOfWork.CarTypePriceRepository.GetByIdAsync(request.Id);

                if (carTypePrice == null) return null;

                return new CarTypePriceDto
                {
                    Id = request.Id,
                    Price = carTypePrice.Price,
                    MinDay = carTypePrice.MinDay,
                    MaxDay = carTypePrice.MaxDay,
                    CarTypeId = carTypePrice.CarTypeId
                };
            }
        }
    }
}
