// File:    NotificationService.cs
// Author:  Lana
// Created: 28 May 2020 14:08:36
// Purpose: Definition of Class NotificationService

using System.Collections.Generic;
using System.Linq;
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

        private readonly RepositoryWrapper<MedicationPrescriptionNotificationRepository>
            medicationPrescriptionNotificationRepository;

        private readonly RepositoryWrapper<PatientAccountRepository> patientAccountRepository;
        private readonly RepositoryWrapper<ProcedureNotificationRepository> procedureNotificationRepository;
        private readonly RepositoryWrapper<RequestNotificationRepository> requestNotificationRepository;

        public NotificationService(
            HospitalizationNotificationRepository hospitalizationNotificationRepository,
            MedicationPrescriptionNotificationRepository medicationPrescriptionNotificationRepository,
            ProcedureNotificationRepository procedureNotificationRepository,
            RequestNotificationRepository requestNotificationRepository,
            PatientAccountRepository patientAccountRepository,
            EmployeeAccountRepository employeeAccountRepository
        )
        {
            this.hospitalizationNotificationRepository =
                new RepositoryWrapper<HospitalizationNotificationRepository>(hospitalizationNotificationRepository);
            this.medicationPrescriptionNotificationRepository =
                new RepositoryWrapper<MedicationPrescriptionNotificationRepository>(
                    medicationPrescriptionNotificationRepository);
            this.procedureNotificationRepository =
                new RepositoryWrapper<ProcedureNotificationRepository>(procedureNotificationRepository);
            this.requestNotificationRepository =
                new RepositoryWrapper<RequestNotificationRepository>(requestNotificationRepository);
            this.patientAccountRepository = new RepositoryWrapper<PatientAccountRepository>(patientAccountRepository);
            this.employeeAccountRepository =
                new RepositoryWrapper<EmployeeAccountRepository>(employeeAccountRepository);
        }

        public IEnumerable<Notification> GetByUser(UserAccount user)
        {
            var notifications = new List<Notification>();

            notifications.AddRange(hospitalizationNotificationRepository.Repository.GetByUser(user));
            notifications.AddRange(procedureNotificationRepository.Repository.GetByUser(user));
            notifications.AddRange(medicationPrescriptionNotificationRepository.Repository.GetByUser(user));
            notifications.AddRange(requestNotificationRepository.Repository.GetByUser(user));
            notifications.ForEach(MarkAsRead);

            return notifications;
        }

        public IEnumerable<Notification> GetUnreadByUser(UserAccount user)
        {
            var notifications = new List<Notification>();

            notifications.AddRange(hospitalizationNotificationRepository.Repository.GetUnreadByUser(user));
            notifications.AddRange(procedureNotificationRepository.Repository.GetUnreadByUser(user));
            notifications.AddRange(medicationPrescriptionNotificationRepository.Repository.GetUnreadByUser(user));
            notifications.AddRange(requestNotificationRepository.Repository.GetUnreadByUser(user));
            notifications.ForEach(MarkAsRead);

            return notifications;
        }

        public void Notify(HospitalizationUpdateType updateType, Hospitalization hospitalization)
        {
            var previousNotifications =
                hospitalizationNotificationRepository.Repository.GetByHospitalization(hospitalization);
            previousNotifications.ToList().ForEach(notification =>
                hospitalizationNotificationRepository.Repository.Delete(notification));
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

        public void Notify(ProcedureUpdateType updateType, Procedure procedure)
        {
            var previousNotifications =
                procedureNotificationRepository.Repository.GetByProcedure(procedure);
            previousNotifications.ToList().ForEach(notification =>
                procedureNotificationRepository.Repository.Delete(notification));

            NotifyPatient(updateType, procedure);
            NotifyDoctor(updateType, procedure);
        }

        private void NotifyPatient(ProcedureUpdateType updateType, Procedure procedure)
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

        private void NotifyDoctor(ProcedureUpdateType updateType, Procedure procedure)
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

        public void Notify(MedicationPrescription medicationPrescription)
        {
            var previousNotifications =
                medicationPrescriptionNotificationRepository.Repository.GetByPrescription(medicationPrescription);
            previousNotifications.ToList().ForEach(notification =>
                medicationPrescriptionNotificationRepository.Repository.Delete(notification));
            var patientAccount = patientAccountRepository.Repository.GetByPatient(medicationPrescription.Patient);
            var patientNotification = new MedicationPrescriptionNotification
            {
                User = patientAccount,
                Read = false,
                Prescription = medicationPrescription
            };
            medicationPrescriptionNotificationRepository.Repository.Create(patientNotification);
        }

        public void Notify(Request request)
        {
            var previousNotifications =
                requestNotificationRepository.Repository.GetByRequest(request);
            previousNotifications.ToList().ForEach(notification =>
                requestNotificationRepository.Repository.Delete(notification));
            var senderNotification = new RequestNotification
            {
                User = request.Sender,
                Read = false,
                Request = request
            };
            requestNotificationRepository.Repository.Create(senderNotification);
        }

        public void Delete(Notification notification)
        {
            switch (notification)
            {
                case RequestNotification requestNotification:
                    requestNotificationRepository.Repository.Delete(requestNotification);
                    break;
                case ProcedureNotification procedureNotification:
                    procedureNotificationRepository.Repository.Delete(procedureNotification);
                    break;
                case HospitalizationNotification hospitalizationNotification:
                    hospitalizationNotificationRepository.Repository.Delete(hospitalizationNotification);
                    break;
                case MedicationPrescriptionNotification prescriptionNotification:
                    medicationPrescriptionNotificationRepository.Repository.Delete(prescriptionNotification);
                    break;
            }
        }

        private void MarkAsRead(Notification notification)
        {
            notification.Read = true;
            switch (notification)
            {
                case RequestNotification requestNotification:
                    requestNotificationRepository.Repository.Update(requestNotification);
                    break;
                case ProcedureNotification procedureNotification:
                    procedureNotificationRepository.Repository.Update(procedureNotification);
                    break;
                case HospitalizationNotification hospitalizationNotification:
                    hospitalizationNotificationRepository.Repository.Update(hospitalizationNotification);
                    break;
                case MedicationPrescriptionNotification prescriptionNotification:
                    medicationPrescriptionNotificationRepository.Repository.Update(prescriptionNotification);
                    break;
            }
        }
    }
}