using System.Collections.Generic;

namespace Schedule.API.Auth
{
    public class UserToken : IIdentityProvider
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public long Gen { get; set; }
        public long Exp { get; set; }

        public IDictionary<string, object> GetEncryptionAttributes()
        {
            return new Dictionary<string, object>(new []
            {
                new KeyValuePair<string, object>("username", Username),
                new KeyValuePair<string, object>("gen", Gen) 
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
    }
}