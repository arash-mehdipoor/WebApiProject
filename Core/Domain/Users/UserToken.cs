using System;

namespace Domain.Users
{
    public class UserToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime TokenExp { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExp { get; set; }

 
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
