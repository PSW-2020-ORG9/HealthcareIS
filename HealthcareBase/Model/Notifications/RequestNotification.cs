// File:    RequestNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:42:47
// Purpose: Definition of Class RequestNotification

using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Requests;

namespace HealthcareBase.Model.Notifications
{
    public class RequestNotification : Notification
    {
        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}