﻿using System;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Newtonsoft.Json;

namespace General.Auth
{
    public class JwtManager
    {
        private readonly IJwtAlgorithm _algorithm;

        private const string _secret = "2fe177d2576b17e19e8a906d2ffcef87b84d8ad6cd8472917eab55a6508f5471659aacd2887d2a0c3b7be3253665391f";
        public static readonly string AuthorizationTokenKey = "Authorization";
        
        public JwtManager()
        {
            this._algorithm = new HMACSHA256Algorithm();
        }

        /// <summary>
        /// Encrypts the given Identity object into a string.
        /// </summary>
        /// <param name="encryptionObject"></param>
        /// <returns>Encrypted JWT string representation of the given object.</returns>
        public string Encode(IIdentityProvider encryptionObject)
        {
            JwtBuilder builder = CreateJwtBuilderWithClaims(encryptionObject);
            return builder.Encode();
        }

        /// <summary>
        /// Attempts to decrypt and deserialize the given encoded string into an object of type T.
        /// </summary>
        /// <param name="encodedString"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>On success, returns instance of T. On failure, returns default.</returns>
        public T Decode<T>(string encodedString) where T : IIdentityProvider
        {
            JwtBuilder builder = CreateJwtBuilder().MustVerifySignature();
            try
            {
                T obj = builder.Decode<T>(encodedString);
                if (obj.GetExpirationTicks() < DateTime.Now.Ticks)
                    throw new JwtExpiredException();
                return obj;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("JWT verification failed.");
                return default;
            }
            catch (JsonReaderException)
            {
                return default;
            }
            catch (JwtExpiredException)
            {
                Console.WriteLine("Token expired.");
                return default;
            }
        }

        private class JwtExpiredException : Exception {}
        
        private JwtBuilder CreateJwtBuilder()
        {
            return new JwtBuilder()
                .WithAlgorithm(_algorithm)
                .WithSecret(_secret);
        }

        private JwtBuilder CreateJwtBuilderWithClaims(IIdentityProvider encryptionObject)
        {
            JwtBuilder builder = CreateJwtBuilder();
            foreach (var attribute in encryptionObject.GetEncryptionAttributes())
            {
                builder.AddClaim(attribute.Key, attribute.Value);
            }

            return builder;
        }
    }
}