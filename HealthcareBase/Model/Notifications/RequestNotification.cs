// File:    RequestNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:42:47
// Purpose: Definition of Class RequestNotification

using Model.Requests;
using System;

namespace Model.Notifications
{
    public class RequestNotification : Notification
    {
        private Model.Requests.Request request;

        public Request Request { get => request; set => request = value; }
    }
}