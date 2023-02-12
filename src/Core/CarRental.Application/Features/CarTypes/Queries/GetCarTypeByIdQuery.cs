using CarRental.Application.Features.CarTypes.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypes.Queries
{
    public class GetCarTypeByIdQuery : IRequest<CarTypeDto>
    {
        public Guid Id { get; set; }

        public GetCarTypeByIdQuery(Guid id) => Id = id;

        public class Handler : IRequestHandler<GetCarTypeByIdQuery, CarTypeDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<CarTypeDto> Handle(GetCarTypeByIdQuery request, CancellationToken cancellationToken)
            {
                var carType = await _unitOfWork.CarTypeRepository.GetByIdAsync(request.Id);

                if (carType == null) return null;

                return new CarTypeDto
                {
                    Id = request.Id,
                    Name = carType.Name,
                    BasePrice = carType.BasePrice,
                    LoyaltyPoints = carType.LoyaltyPoints,
                    ExtraDayPrice = carType.ExtraDayPrice,
                };
            }
        }
    }
}
