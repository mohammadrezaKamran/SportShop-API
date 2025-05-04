using Common.Application;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.WishList.AddToWishList
{
    public class AddToWishListCommandHandler : IBaseCommandHandler<AddToWishListCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddToWishListCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(AddToWishListCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetTracking(request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.AddToWishlist(request.ProductId);

            await _userRepository.Save();
            return OperationResult.Success();
        }
    }
}
