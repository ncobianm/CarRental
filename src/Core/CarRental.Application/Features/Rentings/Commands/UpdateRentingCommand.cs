using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Rentings.Commands
{
    public class UpdateRentingCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public class Handler : IRequestHandler<UpdateRentingCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateRentingCommand request, CancellationToken cancellationToken)
            {
                var renting = await _unitOfWork.RentingRepository.GetByIdAsync(request.Id);

                if (renting == null) return Result.Failure("Record not found");

                renting.StartDate = request.StartDate;
                renting.EndDate = request.EndDate;
                renting.CustomerId = request.CustomerId;
                renting.CarId = request.CarId;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while updating the renting");

                return Result.Success();
            }
        }
    }
}
