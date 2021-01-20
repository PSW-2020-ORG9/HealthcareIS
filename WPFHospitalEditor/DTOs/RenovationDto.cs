using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class RenovationDto
    {
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }
        public TimeInterval TimeInterval { get; set; }
        string Description { get; set; }

        public SchedulingDto toSchedulingDto()
        {
            SchedulingDto schedulingDto = new SchedulingDto()
            {
                FirstRoomId = this.FirstRoomId,
                SecondRoomId = this.SecondRoomId,
                TimeInterval = this.TimeInterval

            };
            return schedulingDto;
        }
    }
}
