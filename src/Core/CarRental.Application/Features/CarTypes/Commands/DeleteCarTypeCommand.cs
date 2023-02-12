using CarRental.Application.Common.Models;
using CarRental.Application.Interfaces.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarTypes.Commands
{
    public class DeleteCarTypeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteCarTypeCommand(Guid id) => Id = id;

        public class Handler : IRequestHandler<DeleteCarTypeCommand, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(DeleteCarTypeCommand request, CancellationToken cancellationToken)
            {
                var carType = await _unitOfWork.CarTypeRepository.GetByIdAsync(request.Id);

                if (carType == null) return Result.Failure("Record not found");

                _unitOfWork.CarTypeRepository.Remove(carType);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                    return Result.Failure("An error has occurred while deleting the car type");

                return Result.Success();
            }
        }
    }
}
