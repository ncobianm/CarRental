using CarRental.Application.Features.CarTypes.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypes.Queries
{
    public class GetCarTypesQuery : IRequest<IEnumerable<CarTypeDto>>
    {
        public class Handler : IRequestHandler<GetCarTypesQuery, IEnumerable<CarTypeDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CarTypeDto>> Handle(GetCarTypesQuery request, CancellationToken cancellationToken)
            {
                var carTypes = await _unitOfWork.CarTypeRepository.GetAllAsync();

                return carTypes.Select(x => new CarTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    BasePrice = x.BasePrice,
                    LoyaltyPoints = x.LoyaltyPoints,
                    ExtraDayPrice = x.ExtraDayPrice,
                });
            }
        }
    }
}
