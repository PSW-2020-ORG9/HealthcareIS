// File:    HospitalizationNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:57
// Purpose: Definition of Interface HospitalizationNotificationRepository

using System.Collections.Generic;
using Model.Notifications;
using Model.Schedule.Hospitalizations;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Repository.NotificationRepository
{
    public interface HospitalizationNotificationRepository : IWrappableRepository<HospitalizationNotification, int>
    {
        IEnumerable<HospitalizationNotification> GetByUser(UserAccount user);

        IEnumerable<HospitalizationNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<HospitalizationNotification> GetByHospitalization(Hospitalization hospitalization);
    }
}