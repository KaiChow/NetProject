using Admin.Model.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service.Jwt
{
    public interface ICustomJWTService
    {
        string GetToken(UserRes user);
    }
}
