using User.API.Infrastructure.Exceptions;

namespace User.API.Model.Users.UserAccounts
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Credentials() { }
        
        public Credentials(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
        
        public Credentials ChangePassword(string password)
        {
            ValidatePassword(password);
            return new Credentials(Username,password,Email);
        }

        private static void ValidatePassword(string password)
        {
            if (password == null)
                throw new BadRequestException();
            if (password.Trim().Equals(""))
                throw new BadRequestException();
        }
    }
}