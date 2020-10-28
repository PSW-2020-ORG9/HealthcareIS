// File:    RequestNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:42:47
// Purpose: Definition of Class RequestNotification

using Model.Requests;

namespace Model.Notifications
{
    public class RequestNotification : Notification
    {
        public Request Request { get; set; }
    }
}