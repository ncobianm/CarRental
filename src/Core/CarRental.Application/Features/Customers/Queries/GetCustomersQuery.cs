using CarRental.Application.Features.Customers.DTO;
using CarRental.Application.Interfaces.Repositories;
using CarRental.Application.Services;
using MediatR;

namespace CarRental.Application.Features.Customers.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        public class Handler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICustomerLoyaltyService _customerLoyaltyService;

            public Handler(IUnitOfWork unitOfWork, ICustomerLoyaltyService customerLoyaltyService)
            {
                _unitOfWork = unitOfWork;
                _customerLoyaltyService = customerLoyaltyService;
            }

            public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();

                var customersDto = customers.Select(async x => new CustomerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    LoyaltyPoints = await _customerLoyaltyService.GetCustomerLoyaltyPointsAsync(x.Id)
                });

                return await Task.WhenAll(customersDto);
            }
        }
    }
}
