// File:    ProcedureNotificationFileRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:12:00
// Purpose: Definition of Class ProcedureNotificationFileRepository

using System.Collections.Generic;
using Model.Notifications;
using Model.Schedule.Procedures;
using Model.Users.UserAccounts;
using Model.Utilities;
using Repository.Generics;

namespace Repository.NotificationRepository
{
    public class ProcedureNotificationFileRepository : GenericFileRepository<ProcedureNotification, int>,
        ProcedureNotificationRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public ProcedureNotificationFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<ProcedureNotification> GetByProcedure(Procedure procedure)
        {
            return GetMatching(notification => notification.Procedure.Equals(procedure));
        }

        public IEnumerable<ProcedureNotification> GetByUser(UserAccount user)
        {
            return GetMatching(notification => notification.User.Equals(user));
        }

        public IEnumerable<ProcedureNotification> GetUnreadByUser(UserAccount user)
        {
            return GetMatching(notification => !notification.Read && notification.User.Equals(user));
        }

        protected override int GenerateKey(ProcedureNotification entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override ProcedureNotification ParseEntity(ProcedureNotification entity)
        {
            return entity;
        }
    }
}