// File:    RequestNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:56
// Purpose: Definition of Interface RequestNotificationRepository

using System.Collections.Generic;
using HealthcareBase.Model.Notifications;
using HealthcareBase.Model.Requests;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.NotificationRepository
{
    public interface RequestNotificationRepository : IWrappableRepository<RequestNotification, int>
    {
        IEnumerable<RequestNotification> GetByUser(UserAccount user);

        IEnumerable<RequestNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<RequestNotification> GetByRequest(Request request);
    }
}