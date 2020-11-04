// File:    NotificationService.cs
// Author:  Lana
// Created: 28 May 2020 14:08:36
// Purpose: Definition of Class NotificationService

using System;
using System.Collections.Generic;
using Model.Medication;
using Model.Notifications;
using Model.Requests;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Users.UserAccounts;
using Repository.Generics;
using Repository.NotificationRepository;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.NotificationService
{
    public class NotificationService
    {
        private readonly RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository;
        private readonly RepositoryWrapper<HospitalizationNotificationRepository> hospitalizationNotificationRepository;
        private readonly RepositoryWrapper<MedicationPrescriptionNotificationRepository> medicationPrescriptionNotificationRepository;
        private readonly RepositoryWrapper<PatientAccountRepository> patientAccountRepository;
        private readonly RepositoryWrapper<ProcedureNotificationRepository> procedureNotificationRepository;
        private readonly RepositoryWrapper<RequestNotificationRepository> requestNotificationRepository;

        public NotificationService(
            RepositoryWrapper<HospitalizationNotificationRepository> hospitalizationNotificationRepository,
            RepositoryWrapper<MedicationPrescriptionNotificationRepository> medicationPrescriptionNotificationRepository,
            RepositoryWrapper<ProcedureNotificationRepository> procedureNotificationRepository,
            RepositoryWrapper<RequestNotificationRepository> requestNotificationRepository,
            RepositoryWrapper<PatientAccountRepository> patientAccountRepository,
            RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository
            )
        {
            this.hospitalizationNotificationRepository = hospitalizationNotificationRepository;
            this.medicationPrescriptionNotificationRepository = medicationPrescriptionNotificationRepository;
            this.procedureNotificationRepository = procedureNotificationRepository;
            this.requestNotificationRepository = requestNotificationRepository;
            this.patientAccountRepository = patientAccountRepository;
            this.employeeAccountRepository = employeeAccountRepository;
        }

        public IEnumerable<Notification> GetByUser(UserAccount user)
        {
            var notifications = new List<Notification>();
            try
            {
                notifications.AddRange(hospitalizationNotificationRepository.Repository.GetByUser(user));
                notifications.AddRange(procedureNotificationRepository.Repository.GetByUser(user));
                notifications.AddRange(medicationPrescriptionNotificationRepository.Repository.GetByUser(user));
                notifications.AddRange(requestNotificationRepository.Repository.GetByUser(user));
                foreach (var notification in notifications)
                    MarkAsRead(notification);
            }
            catch (Exception)
            {
            }

            return notifications;
        }

        public IEnumerable<Notification> GetUnreadByUser(UserAccount user)
        {
            var notifications = new List<Notification>();
            try
            {
                notifications.AddRange(hospitalizationNotificationRepository.Repository.GetUnreadByUser(user));
                notifications.AddRange(procedureNotificationRepository.Repository.GetUnreadByUser(user));
                notifications.AddRange(medicationPrescriptionNotificationRepository.Repository.GetUnreadByUser(user));
                notifications.AddRange(requestNotificationRepository.Repository.GetUnreadByUser(user));
                foreach (var notification in notifications)
                    MarkAsRead(notification);
            }
            catch (Exception)
            {
            }

            return notifications;
        }

        public void Notify(HospitalizationUpdateType updateType, Hospitalization hospitalization)
        {
            try
            {
                var previousNotifications =
                    hospitalizationNotificationRepository.Repository.GetByHospitalization(hospitalization);
                foreach (var notification in previousNotifications)
                    hospitalizationNotificationRepository.Repository.Delete(notification);
            }
            catch (Exception)
            {
            }

            try
            {
                var patientAccount = patientAccountRepository.Repository.GetByPatient(hospitalization.Patient);
                var patientNotification = new HospitalizationNotification
                {
                    User = patientAccount,
                    Read = false,
                    Hospitalization = hospitalization,
                    UpdateType = updateType
                };
                hospitalizationNotificationRepository.Repository.Create(patientNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Notify(ProcedureUpdateType updateType, Procedure procedure)
        {
            try
            {
                var previousNotifications =
                    procedureNotificationRepository.Repository.GetByProcedure(procedure);
                foreach (var notification in previousNotifications)
                    procedureNotificationRepository.Repository.Delete(notification);
            }
            catch (Exception)
            {
            }

            NotifyPatient(updateType, procedure);
            NotifyDoctor(updateType, procedure);
        }

        private void NotifyPatient(ProcedureUpdateType updateType, Procedure procedure)
        {
            try
            {
                var patientAccount = patientAccountRepository.Repository.GetByPatient(procedure.Patient);
                var patientNotification = new ProcedureNotification
                {
                    User = patientAccount,
                    Read = false,
                    Procedure = procedure,
                    UpdateType = updateType
                };
                procedureNotificationRepository.Repository.Create(patientNotification);
            }
            catch (Exception)
            {
            }
        }

        private void NotifyDoctor(ProcedureUpdateType updateType, Procedure procedure)
        {
            try
            {
                var doctorAccount = employeeAccountRepository.Repository.GetByEmployee(procedure.Doctor);
                var doctorNotification = new ProcedureNotification
                {
                    User = doctorAccount,
                    Read = false,
                    Procedure = procedure,
                    UpdateType = updateType
                };
                procedureNotificationRepository.Repository.Create(doctorNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Notify(MedicationPrescription medicationPrescription)
        {
            try
            {
                var previousNotifications =
                    medicationPrescriptionNotificationRepository.Repository.GetByPrescription(medicationPrescription);
                foreach (var notification in previousNotifications)
                    medicationPrescriptionNotificationRepository.Repository.Delete(notification);
            }
            catch (Exception)
            {
            }

            try
            {
                var patientAccount = patientAccountRepository.Repository.GetByPatient(medicationPrescription.Patient);
                var patientNotification = new MedicationPrescriptionNotification
                {
                    User = patientAccount,
                    Read = false,
                    Prescription = medicationPrescription
                };
                medicationPrescriptionNotificationRepository.Repository.Create(patientNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Notify(Request request)
        {
            try
            {
                var previousNotifications =
                    requestNotificationRepository.Repository.GetByRequest(request);
                foreach (var notification in previousNotifications)
                    requestNotificationRepository.Repository.Delete(notification);
            }
            catch (Exception)
            {
            }

            try
            {
                var senderNotification = new RequestNotification
                {
                    User = request.Sender,
                    Read = false,
                    Request = request
                };
                requestNotificationRepository.Repository.Create(senderNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Delete(Notification notification)
        {
            if (notification is RequestNotification)
                requestNotificationRepository.Repository.Delete((RequestNotification) notification);
            else if (notification is ProcedureNotification)
                procedureNotificationRepository.Repository.Delete((ProcedureNotification) notification);
            else if (notification is HospitalizationNotification)
                hospitalizationNotificationRepository.Repository.Delete((HospitalizationNotification) notification);
            else if (notification is MedicationPrescriptionNotification)
                medicationPrescriptionNotificationRepository.Repository.Delete((MedicationPrescriptionNotification) notification);
        }

        private void MarkAsRead(Notification notification)
        {
            notification.Read = true;
            if (notification is RequestNotification)
                requestNotificationRepository.Repository.Update((RequestNotification) notification);
            else if (notification is ProcedureNotification)
                procedureNotificationRepository.Repository.Update((ProcedureNotification) notification);
            else if (notification is HospitalizationNotification)
                hospitalizationNotificationRepository.Repository.Update((HospitalizationNotification) notification);
            else if (notification is MedicationPrescriptionNotification)
                medicationPrescriptionNotificationRepository.Repository.Update((MedicationPrescriptionNotification) notification);
        }
    }
}