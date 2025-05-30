using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.Special
{
	public class SetProductSpecialCommand: IBaseCommand
	{
		public long ProductId { get; set; }
		public bool IsSpecial { get; set; }
	}
}
