using System.Collections.Generic;

namespace MSF.Service
{

    public class UserViewModel
    {
        public UserViewModel()
        {
            Roles = new List<Role>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string UserEmail { get; set; }

        public IList<Role> Roles { get; set; }

    }
}
