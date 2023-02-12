using CarRental.Application.Common.Models;
using CarRental.Application.Features.Rentings.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Application.Services;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Features.Rentings.Commands
{
    public class CreateRentingCommand : IRequest<(Result Result, RentingDto Renting)>
    {
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public class Handler : IRequestHandler<CreateRentingCommand, (Result Result, RentingDto Renting)>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRentPriceService _rentPriceService;

            public Handler(IUnitOfWork unitOfWork, IRentPriceService rentPriceService)
            {
                _unitOfWork = unitOfWork;
                _rentPriceService = rentPriceService;
            }

            public async Task<(Result Result, RentingDto Renting)> Handle(CreateRentingCommand request, CancellationToken cancellationToken)
            {
                var numberOfDays = (int)(request.EndDate.Date - request.StartDate.Date).TotalDays;
                var price = await _rentPriceService.GetRentPrice(request.CarId, numberOfDays);

                Renting renting = new Renting
                {
                    Id = Guid.NewGuid(),
                    CarId = request.CarId,
                    CustomerId = request.CustomerId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Price = price
                };

                _unitOfWork.RentingRepository.Add(renting);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return (Result.Failure("An error has occurred while creating the renting"), null);

                return (Result.Success(), new RentingDto
                {
                    Id = renting.Id,
                    CarId = renting.CarId,
                    CustomerId = renting.CustomerId,
                    StartDate = renting.StartDate,
                    EndDate = renting.EndDate,
                    Price = price,
                });
            }
        }
    }
}
