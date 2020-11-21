// File:    HospitalizationNotificationFileRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:12:00
// Purpose: Definition of Class HospitalizationNotificationFileRepository

using System.Collections.Generic;
using HealthcareBase.Model.Notifications;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.NotificationRepository
{
    public class HospitalizationNotificationFileRepository : GenericFileRepository<HospitalizationNotification, int>,
        HospitalizationNotificationRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public HospitalizationNotificationFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<HospitalizationNotification> GetByHospitalization(Hospitalization hospitalization)
        {
            return GetMatching(notification => notification.Hospitalization.Equals(hospitalization));
        }

        public IEnumerable<HospitalizationNotification> GetByUser(UserAccount user)
        {
            return GetMatching(notification => notification.User.Equals(user));
        }

        public IEnumerable<HospitalizationNotification> GetUnreadByUser(UserAccount user)
        {
            return GetMatching(notification => !notification.Read && notification.User.Equals(user));
        }

        protected override int GenerateKey(HospitalizationNotification entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override HospitalizationNotification ParseEntity(HospitalizationNotification entity)
        {
            return entity;
        }
    }
}