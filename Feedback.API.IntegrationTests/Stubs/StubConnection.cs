﻿using General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.API.IntegrationTests.Stubs
{
    class StubConnection : IConnection
    {
        public T Get<T>(string pathParam = "") where T : class 
            => (T)Activator.CreateInstance(typeof(T));
        public T Post<T>(object obj) where T : class 
            => (T)Activator.CreateInstance(typeof(T));
    }
}
