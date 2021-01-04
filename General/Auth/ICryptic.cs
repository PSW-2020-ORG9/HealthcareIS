using System.Collections.Generic;

namespace General.Auth
{
    public interface ICryptic
    {
        /// <summary>
        /// Creates an IDictionary of fields which should be used for encryption.
        /// </summary>
        /// <returns> Dictionary which holds object fields which should be used in Encryption/Decryption </returns>
        public IDictionary<string, object> GetEncryptionAttributes();
    }
}