using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SupportTicketAgg;
using Shop.Domain.SupportTicketAgg.Repository;
using Shop.Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.SupportTicket
{
    internal class SupportRepository : BaseRepository<SupportTicketAgg>, ISupportTicketRepository
    {
        public SupportRepository(ShopContext context) : base(context)
        {
            
        }
    }
}
