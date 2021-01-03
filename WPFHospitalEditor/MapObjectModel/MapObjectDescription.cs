using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.MapObjectModel
{
   public class MapObjectDescription
   {
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public String Information { get; set; }

        public MapObjectDescription(int buildingId, int floorNumber, String information)
        {
            this.BuildingId = buildingId;
            this.FloorNumber = floorNumber;
            this.Information = information;
        }

        public string[] GetInformation()
        {
            return Information.Split(";");
        }
    }
}
