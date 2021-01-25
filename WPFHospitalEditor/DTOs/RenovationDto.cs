using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class RenovationDto
    {
        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public TimeInterval TimeInterval { get; set; }
        string Description { get; set; }

        public SchedulingDto toSchedulingDto()
        {
            SchedulingDto schedulingDto = new SchedulingDto()
            {
                SourceRoomId = this.SourceRoomId,
                DestinationRoomId = this.DestinationRoomId,
                TimeInterval = this.TimeInterval

            };
            return schedulingDto;
        }
    }
}
