using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Ticket.AddReply;
using Shop.Application.Ticket.CreateTicket;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.SupportTicket;
using Shop.Query.Products.DTOs;
using Shop.Query.Ticket.GetBtFilter;

namespace Shop.Api.Controllers
{
    public class SupportTicketController : ApiController
    {
      private readonly ISupportTicketFacade _ticketFacade;

        public SupportTicketController(ISupportTicketFacade ticketFacade)
        {
            _ticketFacade = ticketFacade;
        }

        [HttpPost]
        public async Task<ApiResult> CreateTicket(CreateTicketCommand command)
        {
         var result= await _ticketFacade.CreateTicket(command);
            return CommandResult(result);
        }

        [HttpPost("addReply")]
        public async Task<ApiResult> AddReply(AddReplyCommand command)
        {
            var result = await _ticketFacade.AddReply(command);
            return CommandResult(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<TicketFilterResult>> GetTicketByFilter([FromQuery] TicketFilterParams filterParams)
        {
            return QueryResult(await _ticketFacade.GetByFilter(filterParams));
        }
    }
}
