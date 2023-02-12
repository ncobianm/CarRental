using CarRental.Application.Features.CarTypePrices.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypePrices.Queries
{
    public class GetCarTypePricesQuery : IRequest<IEnumerable<CarTypePriceDto>>
    {
        public Guid? CarTypeId { get; set; }

        public class Handler : IRequestHandler<GetCarTypePricesQuery, IEnumerable<CarTypePriceDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CarTypePriceDto>> Handle(GetCarTypePricesQuery request, CancellationToken cancellationToken)
            {
                var carTypePrices = await _unitOfWork.CarTypePriceRepository.GetAllAsync();

                if (request.CarTypeId != null)
                    carTypePrices = carTypePrices.Where(x => x.CarTypeId == request.CarTypeId);

                return carTypePrices.Select(x => new CarTypePriceDto
                {
                    Id = x.Id,
                    CarTypeId = x.CarTypeId,
                    MaxDay = x.MaxDay,
                    MinDay = x.MinDay,
                    Price = x.Price
                });
            }
        }
    }
}
