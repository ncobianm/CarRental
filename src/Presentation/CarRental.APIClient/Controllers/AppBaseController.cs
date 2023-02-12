using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppBaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
