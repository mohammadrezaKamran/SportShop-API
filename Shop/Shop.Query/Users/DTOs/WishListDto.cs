using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.DTOs
{
    public class WishListDto:BaseDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Slug {  get; set; }
        public decimal Price { get; set; }
        public int StockQuantity {  get; set; }
        public ProductStatus Status { get; set; }
    }
}
