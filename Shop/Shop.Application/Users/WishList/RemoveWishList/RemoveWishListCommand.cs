﻿using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.WishList.RemoveWishList
{
    public class RemoveWishListCommand : IBaseCommand
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }

    }
}
