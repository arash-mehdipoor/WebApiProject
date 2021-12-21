using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class User
    {
        public Guid Id { get; set; }
        [StringLength(255)]
        public string UserName { get; set; }
        [StringLength(300)]
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<UserToken> UserTokens { get; set; }
    }
}
