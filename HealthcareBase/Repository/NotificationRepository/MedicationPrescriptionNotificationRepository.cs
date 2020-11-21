// File:    MedicationPrescriptionNotificationRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:56
// Purpose: Definition of Interface MedicationPrescriptionNotificationRepository

using System.Collections.Generic;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Notifications;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.NotificationRepository
{
    public interface MedicationPrescriptionNotificationRepository : IWrappableRepository<MedicationPrescriptionNotification, int>
    {
        IEnumerable<MedicationPrescriptionNotification> GetByUser(UserAccount user);

        IEnumerable<MedicationPrescriptionNotification> GetUnreadByUser(UserAccount user);

        IEnumerable<MedicationPrescriptionNotification> GetByPrescription(MedicationPrescription prescription);
    }
}