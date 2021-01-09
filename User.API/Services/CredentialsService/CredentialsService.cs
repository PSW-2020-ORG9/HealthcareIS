using System;
using System.Linq;
using General.Auth;
using General.Repository;
using User.API.DTOs;
using User.API.Infrastructure.Repositories.Users.UserAccounts;
using User.API.Model.Users.UserAccounts;

namespace User.API.Services.CredentialsService
{
    public class CredentialsService
    {
        private readonly RepositoryWrapper<IPatientAccountRepository> _patientAccountWrapper;
        private readonly JwtManager _jwtManager = new JwtManager();

        public CredentialsService(
            IPatientAccountRepository patientAccountRepository
        )
        {
            this._patientAccountWrapper = new RepositoryWrapper<IPatientAccountRepository>(patientAccountRepository);
        }

        public string Login(LoginCredentials credentials)
        {
            PatientAccount patientAccount = _patientAccountWrapper.Repository.GetMatching(account 
                => 
                account.Credentials.Email == credentials.Email
                && account.Credentials.Password == credentials.Password    
            ).FirstOrDefault();

            if (patientAccount != default)
            {
                return _jwtManager.Encode(MapCredentialsToUserToken(patientAccount.Credentials, "Patient"));
            }

            return null;
        }

        private UserToken MapCredentialsToUserToken(Credentials credentials, string role)
        {
            return new UserToken
            {
                Gen = DateTime.Now.Ticks,
                Exp = DateTime.Now.AddHours(1).Ticks,
                Username = credentials.Username,
                Role = role
            };
        }
    }
}