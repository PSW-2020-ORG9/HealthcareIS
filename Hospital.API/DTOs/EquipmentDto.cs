﻿using System;

namespace Hospital.API.DTOs
{
    public class EquipmentDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }

    }
}
