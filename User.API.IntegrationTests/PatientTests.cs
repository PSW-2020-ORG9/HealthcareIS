using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using User.API.Model.Generalities;
using User.API.Model.Users.UserAccounts.Registration;
using Xunit;

namespace User.API.IntegrationTests
{
    public class PatientTests
        : IClassFixture<UserApiFactory<TestStartup>>
    {
        private readonly UserApiFactory<TestStartup> _factory;
        public PatientTests(UserApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Finds_patient()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/patient/1");
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Registers_patient()
        {
            var client = _factory.CreateClient();
            var content = JsonContent.Create(new PatientRegistrationDTO
            {
                Name = "Pera",
                Surname = "Peric",
                Address = "",
                CityOfBirthId = 1,
                CityOfResidenceId = 1,
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "peraperic@email.com",
                InsuranceNumber = "123",
                Jmbg = "1234567891011",
                Gender = Gender.Male,
                MaritalStatus = MaritalStatus.Single,
                MiddleName = "Mika",
                Username = "peraperic",
                Password = "pera1",
                TelephoneNumber = "0600000000"
            });
            var response = await client.PostAsync("user/patient/register", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Activates_patient()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/patient/activate/00000000-0000-0000-0000-000000000000");
            var requestUri = response.RequestMessage.RequestUri;
            Assert.Equal("http://localhost:8080/#/successfully-registered",requestUri.ToString());
        }

        [Fact]
        public async void Finds_patient_account()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/patient/account/1");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Gets_all_patients()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("user/patient");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }

        [Fact]
        public async void Finds_patient_accounts_by_ids()
        {
            var client = _factory.CreateClient();
            List<int> accountIds = new List<int>();
            accountIds.Add(1);
            var content = JsonContent.Create(accountIds);
            var response = await client.PostAsync("user/patient/accounts", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("\"id\":1", responseString);
        }
    }
}
