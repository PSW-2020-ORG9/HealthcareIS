using System.Collections.Generic;
using System.Windows.Media;

namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectColors
    {
        public static Dictionary<MapObjectType, Brush> mapObjectTypesColors = new Dictionary<MapObjectType, Brush>();
        private static MapObjectColors instance = null;

        private MapObjectColors()
        {
            mapObjectTypesColors = new Dictionary<MapObjectType, Brush>();
            mapObjectTypesColors.Add(MapObjectType.Building, Brushes.SkyBlue);
            mapObjectTypesColors.Add(MapObjectType.Parking, Brushes.Gray);
            mapObjectTypesColors.Add(MapObjectType.Road, Brushes.Gray);
            mapObjectTypesColors.Add(MapObjectType.WaitingRoom, Brushes.LightGray);
            mapObjectTypesColors.Add(MapObjectType.Informations, Brushes.Green);
            mapObjectTypesColors.Add(MapObjectType.SurgeryRoom, Brushes.Orange);
            mapObjectTypesColors.Add(MapObjectType.ExaminationRoom, Brushes.Yellow);
            mapObjectTypesColors.Add(MapObjectType.Toilet, Brushes.LightBlue);
            mapObjectTypesColors.Add(MapObjectType.DentistryRoom, Brushes.Beige);
            mapObjectTypesColors.Add(MapObjectType.Canteen, Brushes.Pink);
            mapObjectTypesColors.Add(MapObjectType.Elevator, Brushes.Gray);
            mapObjectTypesColors.Add(MapObjectType.ParkingSlot, Brushes.White);
            mapObjectTypesColors.Add(MapObjectType.RecoveryRoom, Brushes.OliveDrab);
            mapObjectTypesColors.Add(MapObjectType.OnDuty, Brushes.MediumPurple);
            mapObjectTypesColors.Add(MapObjectType.NeurologyRoom, Brushes.LightSalmon);
            mapObjectTypesColors.Add(MapObjectType.DermatologyRoom, Brushes.LightGreen);
            mapObjectTypesColors.Add(MapObjectType.OphthalmologyRoom, Brushes.SteelBlue);
            mapObjectTypesColors.Add(MapObjectType.StorageRoom, Brushes.Ivory);
        }

        public static MapObjectColors getInstance()
        {
            if (instance == null)
                instance = new MapObjectColors();
            return instance;
        }

        public Brush getColor(MapObjectType mapObjectType)
        {
            return mapObjectTypesColors[mapObjectType];
        }
    }
}