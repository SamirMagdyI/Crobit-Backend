using System;

namespace AG.DTO
{
    public class AuthenticationResponse{
        public bool status { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }


}
}
