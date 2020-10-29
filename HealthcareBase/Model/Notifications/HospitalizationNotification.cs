// File:    HospitalizationNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:57
// Purpose: Definition of Class HospitalizationNotification

using Model.Schedule.Hospitalizations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Notifications
{
    public class HospitalizationNotification : Notification
    {
        public Hospitalization Hospitalization { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public HospitalizationUpdateType UpdateType { get; set; }
    }
}