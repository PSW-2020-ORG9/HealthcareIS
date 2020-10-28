// File:    NotificationService.cs
// Author:  Lana
// Created: 28 May 2020 14:08:36
// Purpose: Definition of Class NotificationService

using Model.CustomExceptions;
using Model.Medication;
using Model.Notifications;
using Model.Requests;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Users.UserAccounts;
using Repository.NotificationRepository;
using Repository.UsersRepository.UserAccountsRepository;
using System;
using System.Collections.Generic;

namespace Service.NotificationService
{
    public class NotificationService
    {
        private HospitalizationNotificationRepository hospitalizationNotificationRepository;
        private MedicationPrescriptionNotificationRepository medicationPrescriptionNotificationRepository;
        private ProcedureNotificationRepository procedureNotificationRepository;
        private RequestNotificationRepository requestNotificationRepository;
        private PatientAccountRepository patientAccountRepository;
        private EmployeeAccountRepository employeeAccountRepository;

        public NotificationService(HospitalizationNotificationRepository hospitalizationNotificationRepository,
            MedicationPrescriptionNotificationRepository medicationPrescriptionNotificationRepository,
            ProcedureNotificationRepository procedureNotificationRepository,
            RequestNotificationRepository requestNotificationRepository,
            PatientAccountRepository patientAccountRepository, EmployeeAccountRepository employeeAccountRepository)
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
            List<Notification> notifications = new List<Notification>();
            try
            {
                notifications.AddRange(hospitalizationNotificationRepository.GetByUser(user));
                notifications.AddRange(procedureNotificationRepository.GetByUser(user));
                notifications.AddRange(medicationPrescriptionNotificationRepository.GetByUser(user));
                notifications.AddRange(requestNotificationRepository.GetByUser(user));
                foreach (Notification notification in notifications)
                    MarkAsRead(notification);
            }
            catch (Exception)
            {
            }
            return notifications;
        }

        public IEnumerable<Notification> GetUnreadByUser(UserAccount user)
        {
            List<Notification> notifications = new List<Notification>();
            try
            {
                notifications.AddRange(hospitalizationNotificationRepository.GetUnreadByUser(user));
                notifications.AddRange(procedureNotificationRepository.GetUnreadByUser(user));
                notifications.AddRange(medicationPrescriptionNotificationRepository.GetUnreadByUser(user));
                notifications.AddRange(requestNotificationRepository.GetUnreadByUser(user));
                foreach (Notification notification in notifications)
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
                IEnumerable<HospitalizationNotification> previousNotifications =
                    hospitalizationNotificationRepository.GetByHospitalization(hospitalization);
                foreach (HospitalizationNotification notification in previousNotifications)
                    hospitalizationNotificationRepository.Delete(notification);
            }
            catch (Exception)
            {
            }
            try
            {
                PatientAccount patientAccount = patientAccountRepository.GetByPatient(hospitalization.Patient);
                HospitalizationNotification patientNotification = new HospitalizationNotification
                {
                    User = patientAccount,
                    Read = false,
                    Hospitalization = hospitalization,
                    UpdateType = updateType
                };
                hospitalizationNotificationRepository.Create(patientNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Notify(ProcedureUpdateType updateType, Procedure procedure)
        {
            try
            {
                IEnumerable<ProcedureNotification> previousNotifications =
                    procedureNotificationRepository.GetByProcedure(procedure);
                foreach (ProcedureNotification notification in previousNotifications)
                    procedureNotificationRepository.Delete(notification);
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
                PatientAccount patientAccount = patientAccountRepository.GetByPatient(procedure.Patient);
                ProcedureNotification patientNotification = new ProcedureNotification
                {
                    User = patientAccount,
                    Read = false,
                    Procedure = procedure,
                    UpdateType = updateType
                };
                procedureNotificationRepository.Create(patientNotification);
            }
            catch (Exception)
            {

            }
        }

        private void NotifyDoctor(ProcedureUpdateType updateType, Procedure procedure)
        {
            try
            {
                EmployeeAccount doctorAccount = employeeAccountRepository.GetByEmployee(procedure.Doctor);
                ProcedureNotification doctorNotification = new ProcedureNotification
                {
                    User = doctorAccount,
                    Read = false,
                    Procedure = procedure,
                    UpdateType = updateType
                };
                procedureNotificationRepository.Create(doctorNotification);
            }
            catch (Exception)
            {

            }
        }

        public void Notify(MedicationPrescription medicationPrescription)
        {
            try
            {
                IEnumerable<MedicationPrescriptionNotification> previousNotifications =
                    medicationPrescriptionNotificationRepository.GetByPrescription(medicationPrescription);
                foreach (MedicationPrescriptionNotification notification in previousNotifications)
                    medicationPrescriptionNotificationRepository.Delete(notification);
            }
            catch (Exception)
            {
            }
            try
            {
                PatientAccount patientAccount = patientAccountRepository.GetByPatient(medicationPrescription.Patient);
                MedicationPrescriptionNotification patientNotification = new MedicationPrescriptionNotification
                {
                    User = patientAccount,
                    Read = false,
                    Prescription = medicationPrescription
                };
                medicationPrescriptionNotificationRepository.Create(patientNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Notify(Request request)
        {
            try
            {
                IEnumerable<RequestNotification> previousNotifications =
                    requestNotificationRepository.GetByRequest(request);
                foreach (RequestNotification notification in previousNotifications)
                    requestNotificationRepository.Delete(notification);
            }
            catch (Exception)
            {
            }
            try
            {
                RequestNotification senderNotification = new RequestNotification
                {
                    User = request.Sender,
                    Read = false,
                    Request = request
                };
                requestNotificationRepository.Create(senderNotification);
            }
            catch (Exception)
            {
            }
        }

        public void Delete(Notification notification)
        {
            if (notification is RequestNotification)
                requestNotificationRepository.Delete((RequestNotification)notification);
            else if (notification is ProcedureNotification)
                procedureNotificationRepository.Delete((ProcedureNotification)notification);
            else if (notification is HospitalizationNotification)
                hospitalizationNotificationRepository.Delete((HospitalizationNotification)notification);
            else if (notification is MedicationPrescriptionNotification)
                medicationPrescriptionNotificationRepository.Delete((MedicationPrescriptionNotification)notification);
        }

        private void MarkAsRead(Notification notification)
        {
            notification.Read = true;
            if (notification is RequestNotification)
                requestNotificationRepository.Update((RequestNotification)notification);
            else if (notification is ProcedureNotification)
                procedureNotificationRepository.Update((ProcedureNotification)notification);
            else if (notification is HospitalizationNotification)
                hospitalizationNotificationRepository.Update((HospitalizationNotification)notification);
            else if (notification is MedicationPrescriptionNotification)
                medicationPrescriptionNotificationRepository.Update((MedicationPrescriptionNotification)notification);
        }
    }
}