using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
		public UserRepository(ShopContext context) : base(context)
		{
		}

        public async Task<User> GetUserWithWishlist(long userId)
        {
            return await Context.Users.AsTracking()
            .Include(u => u.WishLists)
            .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}