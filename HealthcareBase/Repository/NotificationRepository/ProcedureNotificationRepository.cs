// File:    ProcedureNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:57
// Purpose: Definition of Interface ProcedureNotificationRepository

using System.Collections.Generic;
using HealthcareBase.Model.Notifications;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.NotificationRepository
{
    public interface ProcedureNotificationRepository : IWrappableRepository<ProcedureNotification, int>
    {
        IEnumerable<ProcedureNotification> GetByUser(UserAccount user);

        IEnumerable<ProcedureNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<ProcedureNotification> GetByProcedure(Procedure procedure);
    }
}