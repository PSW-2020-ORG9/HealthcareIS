using General;
using System;
using User.API.Infrastructure.Exceptions;

namespace User.API.Model.Users.UserAccounts
{
    public abstract class UserAccount : Entity<int>
    {
        public Credentials Credentials { get; set; }
        public string Role { get; set; }
        
        public string AvatarUrl { get; set; }
        
        public bool IsActivated { get; set; }
        public Guid UserGuid { get; set; }
        
        public void ActivateAccount()
        {
            if(IsActivated) throw new ValidationException("Account is already activated.");
            IsActivated = true;
        }
    }
}