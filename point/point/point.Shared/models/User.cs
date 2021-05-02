using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace point.models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LasName { get; set; }

        public string passWord { get; set; }

        public string FullName => $"{FirstName} {LasName}";

    }
}
