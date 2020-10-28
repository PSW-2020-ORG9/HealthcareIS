// File:    RequestNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:56
// Purpose: Definition of Interface RequestNotificationRepository

using Model.Notifications;
using Model.Requests;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.NotificationRepository
{
    public interface RequestNotificationRepository : Repository<RequestNotification, int>
    {
        IEnumerable<RequestNotification> GetByUser(UserAccount user);

        IEnumerable<RequestNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<RequestNotification> GetByRequest(Request request);
    }
}