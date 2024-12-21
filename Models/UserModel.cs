using Microsoft.AspNetCore.Mvc;

namespace AndroidCA_backend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsPaidUser {  get; set; }

    }
}
