using CarRental.Application.Features.Rentings.DTO;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Rentings.Queries
{
    public class GetRentingsQuery : IRequest<IEnumerable<RentingDto>>
    {
        public Guid? CustomerId { get; set; }
        public Guid? CardId { get; set; }

        public class Handler : IRequestHandler<GetRentingsQuery, IEnumerable<RentingDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<RentingDto>> Handle(GetRentingsQuery request, CancellationToken cancellationToken)
            {
                var rentings = await _unitOfWork.RentingRepository.GetAllAsync();

                if (request.CustomerId.HasValue)
                    rentings = rentings.Where(x => x.CustomerId == request.CustomerId);

                if (request.CardId.HasValue)
                    rentings = rentings.Where(x => x.CarId == request.CardId);

                return rentings.Select(x => new RentingDto
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CustomerId = x.CustomerId,
                    Price = x.Price,
                    RealEndDate = x.RealEndDate,
                    Surcharges = x.Surcharges
                });
            }
        }
    }
}
