using Common.Application;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Status
{
    public class ChangeOrderStatusCommandHandler : IBaseCommandHandler<ChangeOrderStatusCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderId);
            if (order == null)
                return OperationResult.NotFound();
            order.ChangeStatus(request.Status);

            await _orderRepository.Save();
            return OperationResult.Success();
        }
    }
}
