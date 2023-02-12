using CarRental.Application.Features.Rentings.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Rentings.Queries
{
    public class GetRentingByIdQuery : IRequest<RentingDto>
    {
        public Guid Id { get; set; }

        public GetRentingByIdQuery(Guid id) => Id = id;

        public class Handler : IRequestHandler<GetRentingByIdQuery, RentingDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<RentingDto> Handle(GetRentingByIdQuery request, CancellationToken cancellationToken)
            {
                var renting = await _unitOfWork.RentingRepository.GetByIdAsync(request.Id);

                if (renting == null) return null;

                return new RentingDto
                {
                    Id = request.Id,
                    CarId = renting.CarId,
                    CustomerId = renting.CustomerId,
                    EndDate = renting.EndDate,
                    StartDate = renting.StartDate,
                    Price = renting.Price,
                    RealEndDate = renting.RealEndDate,
                    Surcharges = renting.Surcharges,
                };
            }
        }
    }
}
