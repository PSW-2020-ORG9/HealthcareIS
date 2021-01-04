using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;

namespace General.Auth
{
    public class JwtService
    {
        private readonly IJwtAlgorithm _algorithm;
        private readonly string _secret;

        public JwtService()
        {
            this._algorithm = new HMACSHA256Algorithm();
            this._secret = "910xlAOsic*s9cAs;1!@4xXasWEsxcsspdoAoxiusdu82iuaskjxmasdkwdi";
        }

        public string Encode(ICryptic encryptionObject)
        {
            return CreateJwtBuilderWithClaims(encryptionObject)
                .WithAlgorithm(_algorithm)
                .WithSecret(_secret)
                .Encode();
        }

        public T Decode<T>(string encodedString)
        {
            JwtBuilder builder = new JwtBuilder();
            builder
                .WithAlgorithm(_algorithm)
                .WithSecret(_secret)
                .MustVerifySignature();
            try
            {
                return builder.Decode<T>(encodedString);
            }
            catch (SignatureVerificationException e)
            {
                return default;
            }
        }

        private JwtBuilder CreateJwtBuilderWithClaims(ICryptic encryptionObject)
        {
            JwtBuilder builder = new JwtBuilder();
            foreach (var attribute in encryptionObject.GetEncryptionAttributes())
            {
                builder.AddClaim(attribute.Key, attribute.Value);
            }
            return builder;
        }
    }
}