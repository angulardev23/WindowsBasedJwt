using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTest.Model
{
    public class UserModel
    {
        public string Username { get; internal set; }
        public string EmailAddress { get; internal set; }
        public DateTime DateOfJoing { get; internal set; }
    }
}
