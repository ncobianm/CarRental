using CarRental.Application.Common.Models;
using CarRental.Application.Features.Rentings.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Application.Services;
using CarRental.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Features.Rentings.Commands
{
    public class ReturnCarCommand : IRequest<(Result Result, RentingDto Renting)>
    {
        public Guid RentingId { get; set; }
        public DateTime EndDate { get; set; }

        public class Handler : IRequestHandler<ReturnCarCommand, (Result Result, RentingDto Renting)>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRentPriceService _rentPriceService;

            public Handler(IUnitOfWork unitOfWork, IRentPriceService rentPriceService)
            {
                _unitOfWork = unitOfWork;
                _rentPriceService = rentPriceService;
            }

            public async Task<(Result Result, RentingDto Renting)> Handle(ReturnCarCommand request, CancellationToken cancellationToken)
            {
                var renting = await _unitOfWork.RentingRepository.GetByIdAsync(request.RentingId);

                if (request.EndDate.Date < renting.EndDate.Date)
                    return (Result.Failure("Return date cannot be earlier than the rental date"), null);

                var surcharges = await _rentPriceService.GetSurcharges(renting.Id, request.EndDate);

                renting.RealEndDate = request.EndDate;
                renting.Surcharges = surcharges;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return (Result.Failure("An error has occurred while returning the car"), null);

                return (Result.Success(), new RentingDto
                {
                    Id = renting.Id,
                    CarId = renting.CarId,
                    CustomerId = renting.CustomerId,
                    StartDate = renting.StartDate,
                    EndDate = renting.EndDate,
                    Price = renting.Price,
                    RealEndDate = renting.RealEndDate,
                    Surcharges = renting.Surcharges
                });
            }
        }
    }
}
