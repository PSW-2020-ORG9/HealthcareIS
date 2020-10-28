// File:    ProcedureNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:57
// Purpose: Definition of Interface ProcedureNotificationRepository

using Model.Notifications;
using Model.Schedule.Procedures;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.NotificationRepository
{
    public interface ProcedureNotificationRepository : Repository<ProcedureNotification, int>
    {
        IEnumerable<ProcedureNotification> GetByUser(UserAccount user);

        IEnumerable<ProcedureNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<ProcedureNotification> GetByProcedure(Procedure procedure);
    }
}