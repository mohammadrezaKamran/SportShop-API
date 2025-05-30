using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.ChangePasswordByAdmin
{
	public class ChangeUserPasswordByAdminCommand:IBaseCommand
	{
		public long UserId {  get; set; }
		public string NewPassword {  get; set; }
	}
}
