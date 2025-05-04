using Common.Application;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CommentAgg;
using Shop.Domain.SupportTicketAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Ticket.AddReply
{
    internal class AddReplyCommandHandler : IBaseCommandHandler<AddReplyCommand>
    {
        private readonly ISupportTicketRepository _repository;

        public AddReplyCommandHandler(ISupportTicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddReplyCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetAsync(request.ticketId);
            if(ticket == null)
                return OperationResult.NotFound();

            ticket.AddReply(request.adminId,request.message);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
