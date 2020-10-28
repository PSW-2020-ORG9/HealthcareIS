// File:    HospitalizationNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:57
// Purpose: Definition of Class HospitalizationNotification

using Model.Schedule.Hospitalizations;
using System;

namespace Model.Notifications
{
    public class HospitalizationNotification : Notification
    {
        private Hospitalization hospitalization;
        private HospitalizationUpdateType updateType;

        public Hospitalization Hospitalization { get => hospitalization; set => hospitalization = value; }
        public HospitalizationUpdateType UpdateType { get => updateType; set => updateType = value; }
    }
}