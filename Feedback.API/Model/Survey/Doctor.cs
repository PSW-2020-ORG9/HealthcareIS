using Feedback.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Model.Survey
{
    public class Doctor : Entity<string>
    {
        public string Name { get; internal set; }
    }
}
