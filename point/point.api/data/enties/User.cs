using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace point.api.enties
{
    public class User
    {
        public int Id { get; set; }

        [Required]      
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]       
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LasName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string passWord { get; set; }

        public ICollection<Product> Productcs { get; set; }
    }
}
