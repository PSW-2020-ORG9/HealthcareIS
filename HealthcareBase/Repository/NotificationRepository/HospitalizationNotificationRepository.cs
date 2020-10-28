// File:    HospitalizationNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:57
// Purpose: Definition of Interface HospitalizationNotificationRepository

using Model.Notifications;
using Model.Schedule.Hospitalizations;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.NotificationRepository
{
    public interface HospitalizationNotificationRepository : Repository<HospitalizationNotification, int>
    {
        IEnumerable<HospitalizationNotification> GetByUser(UserAccount user);

        IEnumerable<HospitalizationNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<HospitalizationNotification> GetByHospitalization(Hospitalization hospitalization);
    }
}