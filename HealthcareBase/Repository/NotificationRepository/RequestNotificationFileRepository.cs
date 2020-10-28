// File:    RequestNotificationFileRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:59
// Purpose: Definition of Class RequestNotificationFileRepository

using Model.Notifications;
using Model.Requests;
using Model.Users.UserAccounts;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.NotificationRepository
{
    public class RequestNotificationFileRepository : GenericFileRepository<RequestNotification, int>, RequestNotificationRepository
    {
        private IntegerKeyGenerator keyGenerator;

        public RequestNotificationFileRepository(String filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<RequestNotification> GetByRequest(Request request)
        {
            return GetMatching(notification => notification.Request.Equals(request));
        }

        public IEnumerable<RequestNotification> GetByUser(UserAccount user)
        {
            return GetMatching(notification => notification.User.Equals(user));
        }

        public IEnumerable<RequestNotification> GetUnreadByUser(UserAccount user)
        {
            return GetMatching(notification => !notification.Read && notification.User.Equals(user));
        }

        protected override int GenerateKey(RequestNotification entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override RequestNotification ParseEntity(RequestNotification entity)
        {
            return entity;
        }
    }
}