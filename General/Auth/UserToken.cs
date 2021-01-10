using System.Collections.Generic;

namespace General.Auth
{
    public class UserToken : IIdentityProvider
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public long Gen { get; set; }
        public long Exp { get; set; }
        public int UserId { get; set; }

        public IDictionary<string, object> GetEncryptionAttributes()
        {
            return new Dictionary<string, object>(new []
            {
                new KeyValuePair<string, object>("username", Username),
                new KeyValuePair<string, object>("gen", Gen),
                new KeyValuePair<string, object>("exp", Exp),
                new KeyValuePair<string, object>("role", Role), 
                new KeyValuePair<string, object>("userId", UserId), 
            });
        }

        public long GetExpirationTicks()
        {
            return Exp;
        }

        public string GetRole()
        {
            return Role;
        }

        public int GetUserId()
        {
            return UserId;
        }
    }
}