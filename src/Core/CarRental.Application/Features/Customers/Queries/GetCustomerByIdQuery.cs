using CarRental.Application.Features.Customers.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Application.Services;
using MediatR;

namespace CarRental.Application.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }

        public GetCustomerByIdQuery(Guid id) => Id = id;

        public class Handler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICustomerLoyaltyService _customerLoyaltyService;

            public Handler(IUnitOfWork unitOfWork, ICustomerLoyaltyService customerLoyaltyService)
            {
                _unitOfWork = unitOfWork;
                _customerLoyaltyService = customerLoyaltyService;
            }

            public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);

                if (customer == null) return null;

                var loyaltyPoints = await _customerLoyaltyService.GetCustomerLoyaltyPointsAsync(customer.Id);

                return new CustomerDto
                {
                    Id = request.Id,
                    Name = customer.Name,
                    LoyaltyPoints = loyaltyPoints
                };
            }
        }
    }
}
