﻿using Common.Query;
using Shop.Query.Products.DTOs;
using Shop.Query.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetWishList
{
    public class GetWishListQuery:IQuery<List<WishListDto>>
    {
        public GetWishListQuery(long userId)
        {
            this.userId = userId;
        }

        public long userId {  get; private set; }
    }
}
