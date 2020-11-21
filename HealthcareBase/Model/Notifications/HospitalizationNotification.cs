// File:    HospitalizationNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:57
// Purpose: Definition of Class HospitalizationNotification

using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Schedule.Hospitalizations;

namespace HealthcareBase.Model.Notifications
{
    public class HospitalizationNotification : Notification
    {
        [ForeignKey("Hospitalization")]
        public int HospitalizationId { get; set; }
        public Hospitalization Hospitalization { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public HospitalizationUpdateType UpdateType { get; set; }
    }
}