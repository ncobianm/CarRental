using CarRental.Application.Features.Rentings.DTO;
using CarRental.Application.Services;
using MediatR;

namespace CarRental.Application.Features.Rentings.Queries
{
    public class GetRentalPriceQuery : IRequest<RentalPriceDto>
    {
        public Guid CardId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetRentalPriceQuery(Guid cardId, DateTime startDate, DateTime endDate)
        {
            CardId = cardId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public class Handler : IRequestHandler<GetRentalPriceQuery, RentalPriceDto>
        {
            private readonly IRentPriceService _rentPriceService;

            public Handler(IRentPriceService rentPriceService)
            {
                _rentPriceService = rentPriceService;
            }

            public async Task<RentalPriceDto> Handle(GetRentalPriceQuery request, CancellationToken cancellationToken)
            {
                var numberOfDays = (int)(request.EndDate.Date - request.StartDate.Date).TotalDays;
                var price = await _rentPriceService.GetRentPrice(request.CardId, numberOfDays);

                return new RentalPriceDto
                {
                    Price = price
                };
            }
        }
    }
}
