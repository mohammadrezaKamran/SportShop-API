using Common.Application;
using Shop.Application.SiteEntities.Banners.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.DeleteUser
{
    public record DeleteUserCommand(long id) : IBaseCommand;

}
