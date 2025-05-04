using Common.Application;
using MediatR;
using Shop.Domain.CommentAgg;
using Shop.Domain.SupportTicketAgg;
using Shop.Domain.SupportTicketAgg.Repository;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Ticket.CreateTicket
{
    internal class CreateTicketCommandHandler : IBaseCommandHandler<CreateTicketCommand>
    {
        private readonly ISupportTicketRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly INotification _notification;

        public async Task<OperationResult> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new SupportTicketAgg(request.userId, request.title, request.message);
            _repository.Add(ticket);

            await _repository.Save();
            //var adminTokens = await _userRepository.GetAllAdminDeviceTokens();
            ////foreach (var token in adminTokens)
            ////{
            ////    await _notification.(token, "تیکت جدید", "یک تیکت جدید دریافت شد.");
            ////}
            
            ////return ticket.Id;
            return OperationResult.Success();
        }
    }
}
