using hi_teacher_app_backend.DTOs;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services
{
    public interface IUsersService<T>
    {
        UserDTO Authenticate(string username, string password);
        void Register(RegisterDTO model, string scheme, string host,string pathBase);
    }
}
