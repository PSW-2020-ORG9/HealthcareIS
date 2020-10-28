// File:    MedicationPrescriptionNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:56
// Purpose: Definition of Interface MedicationPrescriptionNotificationRepository

using Model.Medication;
using Model.Notifications;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.NotificationRepository
{
    public interface MedicationPrescriptionNotificationRepository : Repository<MedicationPrescriptionNotification, int>
    {
        IEnumerable<MedicationPrescriptionNotification> GetByUser(UserAccount user);

        IEnumerable<MedicationPrescriptionNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<MedicationPrescriptionNotification> GetByPrescription(MedicationPrescription prescription);
    }
}