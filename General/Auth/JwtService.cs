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
            this._secret =
                "2fe177d2576b17e19e8a906d2ffcef87b84d8ad6cd8472917eab55a6508f5471659aacd2887d2a0c3b7be3253665391f";
        }

        public string Encode(ICryptic encryptionObject, long expirationDateTicks = -1)
        {
            JwtBuilder builder = CreateJwtBuilderWithClaims(encryptionObject);
            if (expirationDateTicks != -1) 
                AddExpirationDate(builder, expirationDateTicks);
            return builder.Encode();
        }

        public T Decode<T>(string encodedString)
        {
            JwtBuilder builder = CreateJwtBuilder().MustVerifySignature();
            try
            {
                return builder.Decode<T>(encodedString);
            }
            catch (SignatureVerificationException e)
            {
                return default;
            }
        }

        private JwtBuilder CreateJwtBuilder()
        {
            return new JwtBuilder()
                .WithAlgorithm(_algorithm)
                .WithSecret(_secret);
        }

        private JwtBuilder CreateJwtBuilderWithClaims(ICryptic encryptionObject)
        {
            JwtBuilder builder = CreateJwtBuilder();
            foreach (var attribute in encryptionObject.GetEncryptionAttributes())
            {
                builder.AddClaim(attribute.Key, attribute.Value);
            }
            return builder;
        }

        private JwtBuilder AddExpirationDate(JwtBuilder builder, long ticks)
        {
            return builder.AddClaim("exp", ticks);
        }
    }
}