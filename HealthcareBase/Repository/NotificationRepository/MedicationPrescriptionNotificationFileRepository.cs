// File:    MedicationPrescriptionNotificationFileRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:11:59
// Purpose: Definition of Class MedicationPrescriptionNotificationFileRepository

using System.Collections.Generic;
using Model.Medication;
using Model.Notifications;
using Model.Users.UserAccounts;
using Model.Utilities;
using Repository.Generics;

namespace Repository.NotificationRepository
{
    public class MedicationPrescriptionNotificationFileRepository :
        GenericFileRepository<MedicationPrescriptionNotification, int>, MedicationPrescriptionNotificationRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public MedicationPrescriptionNotificationFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<MedicationPrescriptionNotification> GetByPrescription(MedicationPrescription prescription)
        {
            return GetMatching(notification => notification.Prescription.Equals(prescription));
        }

        public IEnumerable<MedicationPrescriptionNotification> GetByUser(UserAccount user)
        {
            return GetMatching(notification => notification.User.Equals(user));
        }

        public IEnumerable<MedicationPrescriptionNotification> GetUnreadByUser(UserAccount user)
        {
            return GetMatching(notification => !notification.Read && notification.User.Equals(user));
        }

        protected override int GenerateKey(MedicationPrescriptionNotification entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override MedicationPrescriptionNotification ParseEntity(MedicationPrescriptionNotification entity)
        {
            return entity;
        }
    }
}