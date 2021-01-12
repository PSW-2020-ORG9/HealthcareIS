using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace User.API.Model.ESProjection
{
    public class Projection
    {

        [Key]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
