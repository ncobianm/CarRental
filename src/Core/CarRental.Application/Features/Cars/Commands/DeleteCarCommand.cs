using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.Cars.Commands
{
    public class DeleteCarCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteCarCommand(Guid id) => Id = id;

        public class Handler : IRequestHandler<DeleteCarCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
            {
                var car = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);

                if (car == null) return Result.Failure("Record not found");

                _unitOfWork.CarRepository.Remove(car);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while deleting the car");

                return Result.Success();
            }
        }
    }
}
