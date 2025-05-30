using Common.Application;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.WishList.RemoveWishList
{
    internal class RemoveWishListCommandHandler : IBaseCommandHandler<RemoveWishListCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveWishListCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(RemoveWishListCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetUserWithWishlist(request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.RemoveFromWishlist(request.ProductId);

            await _userRepository.Save();
            return OperationResult.Success();
        }
    }
}
