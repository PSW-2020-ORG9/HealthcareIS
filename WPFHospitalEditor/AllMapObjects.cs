using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
{
    class AllMapObjects
    {
        public static List<MapObject> allOuterMapObjects = new List<MapObject>();
        public static List<MapObject> allFirstBuildingFirstFloorObjects = new List<MapObject>();
        public static List<MapObject> allFirstBuildingSecondFloorObjects = new List<MapObject>();
        public static List<MapObject> allSecondBuildingFirstFloorObjects = new List<MapObject>();
        public static List<MapObject> allSecondBuildingSecondFloorObjects = new List<MapObject>();
        public AllMapObjects()
        {

            MapObject road1 = new MapObject(3, new MapObjectMetrics(new MapObjectCoordinates(0.0, 20.0), new MapObjectDimensions(900.0, 20.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject road2 = new MapObject(4, new MapObjectMetrics(new MapObjectCoordinates(440.0, 0.0), new MapObjectDimensions(20.0, 550.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject road5 = new MapObject(5, new MapObjectMetrics(new MapObjectCoordinates(50.0, 330.0), new MapObjectDimensions(500.0, 20.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject road4 = new MapObject(6, new MapObjectMetrics(new MapObjectCoordinates(50.0, 460.0), new MapObjectDimensions(500, 20.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject building1 = new MapObject(1, new MapObjectMetrics(new MapObjectCoordinates(20.0, 60.0), new MapObjectDimensions(380.0, 220.0)), MapObjectType.Building, "Zgrada1", new MapObjectDoor(MapObjectDoorOrientation.Right), "");
            MapObject building2 = new MapObject(2, new MapObjectMetrics(new MapObjectCoordinates(500.0, 60.0), new MapObjectDimensions(300.0, 220.0)), MapObjectType.Building, "Zgrada2", new MapObjectDoor(MapObjectDoorOrientation.Left), "");
            MapObject parking1 = new MapObject(7, new MapObjectMetrics(new MapObjectCoordinates(20.0, 300.0), new MapObjectDimensions(380.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject parking2 = new MapObject(8, new MapObjectMetrics(new MapObjectCoordinates(20.0, 430.0), new MapObjectDimensions(380.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject parking3 = new MapObject(9, new MapObjectMetrics(new MapObjectCoordinates(500.0, 300.0), new MapObjectDimensions(300.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject parking4 = new MapObject(10, new MapObjectMetrics(new MapObjectCoordinates(500.0, 430.0), new MapObjectDimensions(300.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");

            allOuterMapObjects.Add(road1);
            allOuterMapObjects.Add(road2);
            allOuterMapObjects.Add(road5);
            allOuterMapObjects.Add(road4);
            allOuterMapObjects.Add(building1);
            allOuterMapObjects.Add(building2);
            allOuterMapObjects.Add(parking1);
            allOuterMapObjects.Add(parking2);
            allOuterMapObjects.Add(parking3);
            allOuterMapObjects.Add(parking4);

            for (int i = 0; i < 7; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(20 + (45.5 * (1 + i)) - 1 + 2 * i, 300.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
            for (int i = 0; i < 7; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(20 + (45.5 * (1 + i)) - 1 + 2 * i, 430.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
            for (int i = 0; i < 5; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(800 - (45.5 * (1 + i)) + 1 - 2 * i, 300.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
            for (int i = 0; i < 5; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(800 - (45.5 * (1 + i)) + 1 - 2 * i, 430.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }


            MapObject elevator1 = new MapObject(11, new MapObjectMetrics(new MapObjectCoordinates(750.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, "Lift", new MapObjectDoor(MapObjectDoorOrientation.Left), "Maksimalno mogu stati 4 osobe");
            MapObject infos1 = new MapObject(12, new MapObjectMetrics(new MapObjectCoordinates(0.0, 150.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.Informations, "Informacije1", new MapObjectDoor(MapObjectDoorOrientation.Right), "07:00 - 00:00");
            MapObject toilet1 = new MapObject(13, new MapObjectMetrics(new MapObjectCoordinates(0.0, 300.0), new MapObjectDimensions(100.0, 100.0)), MapObjectType.Toilet, "Toalet", new MapObjectDoor(MapObjectDoorOrientation.Right), "");
            MapObject hall1 = new MapObject(14, new MapObjectMetrics(new MapObjectCoordinates(130.0, 130.0), new MapObjectDimensions(600.0, 140.0)), MapObjectType.WaitingRoom, "Cekaonica", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject regular1 = new MapObject(15, new MapObjectMetrics(new MapObjectCoordinates(0.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.ExaminationRoom, "Opsti pregled1", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 21:00,2,13,10,4");
            MapObject regular2 = new MapObject(16, new MapObjectMetrics(new MapObjectCoordinates(140.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.ExaminationRoom, "Opsti pregled2", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 21:00,1,9,5,6");
            MapObject regular3 = new MapObject(17, new MapObjectMetrics(new MapObjectCoordinates(280.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.ExaminationRoom, "Opsti pregled3", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 21:00,3,5,7,8");
            MapObject op1 = new MapObject(18, new MapObjectMetrics(new MapObjectCoordinates(460.0, 0.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.SurgeryRoom, "Operaciona sala1", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 19:00,50,100,1,1");
            MapObject op2 = new MapObject(19, new MapObjectMetrics(new MapObjectCoordinates(650.0, 0.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.SurgeryRoom, "Operaciona sala2", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 19:00,50,100,1,1");
            MapObject dentistRoom1 = new MapObject(20, new MapObjectMetrics(new MapObjectCoordinates(220.0, 280.0), new MapObjectDimensions(150.0, 120.0)), MapObjectType.DentistryRoom, "Stomatoloska ordinacija1", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 19:00,100,100,3,20");
            MapObject dentistRoom2 = new MapObject(21, new MapObjectMetrics(new MapObjectCoordinates(370.0, 280.0), new MapObjectDimensions(150.0, 120.0)), MapObjectType.DentistryRoom, "Stomatoloska ordinacija2", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 19:00,100,100,3,20");
            MapObject canteen1 = new MapObject(22, new MapObjectMetrics(new MapObjectCoordinates(600.0, 280.0), new MapObjectDimensions(200.0, 120.0)), MapObjectType.Canteen, "Kantina", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 18:00,50,100,10,40");

            MapObject elevator2 = new MapObject(23, new MapObjectMetrics(new MapObjectCoordinates(750.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, "Lift", new MapObjectDoor(MapObjectDoorOrientation.Left), "Maksimalno mogu stati 4 osobe");
            MapObject infos2 = new MapObject(24, new MapObjectMetrics(new MapObjectCoordinates(0.0, 150.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.Informations, "Informacije2", new MapObjectDoor(MapObjectDoorOrientation.Right), "07:00 - 00:00");
            MapObject toilet2 = new MapObject(25, new MapObjectMetrics(new MapObjectCoordinates(0.0, 300.0), new MapObjectDimensions(100.0, 100.0)), MapObjectType.Toilet, "Toalet", new MapObjectDoor(MapObjectDoorOrientation.Right), "");
            MapObject recoveryRoom1 = new MapObject(26, new MapObjectMetrics(new MapObjectCoordinates(0.0, 0.0), new MapObjectDimensions(220.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak1", new MapObjectDoor(MapObjectDoorOrientation.Down), "20,7");
            MapObject onDuty1 = new MapObject(27, new MapObjectMetrics(new MapObjectCoordinates(250.0, 0.0), new MapObjectDimensions(120.0, 120.0)), MapObjectType.OnDuty, "Dezurno osoblje", new MapObjectDoor(MapObjectDoorOrientation.Down), "00:00 - 24:00");
            MapObject recoveryRoom2 = new MapObject(28, new MapObjectMetrics(new MapObjectCoordinates(400.0, 0.0), new MapObjectDimensions(220.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak2", new MapObjectDoor(MapObjectDoorOrientation.Down), "20,7");
            MapObject recoveryRoom3 = new MapObject(29, new MapObjectMetrics(new MapObjectCoordinates(650.0, 0.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak3", new MapObjectDoor(MapObjectDoorOrientation.Down), "20,7");
            MapObject recoveryRoom4 = new MapObject(30, new MapObjectMetrics(new MapObjectCoordinates(150.0, 280.0), new MapObjectDimensions(120.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak4", new MapObjectDoor(MapObjectDoorOrientation.Up), "20,7");
            MapObject recoveryRoom5 = new MapObject(31, new MapObjectMetrics(new MapObjectCoordinates(300.0, 280.0), new MapObjectDimensions(120.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak5", new MapObjectDoor(MapObjectDoorOrientation.Up), "20,7");
            MapObject onDuty2 = new MapObject(32, new MapObjectMetrics(new MapObjectCoordinates(450.0, 280.0), new MapObjectDimensions(120.0, 120.0)), MapObjectType.OnDuty, "Dezurno osoblje", new MapObjectDoor(MapObjectDoorOrientation.Up), "00:00 - 24:00");
            MapObject recoveryRoom6 = new MapObject(33, new MapObjectMetrics(new MapObjectCoordinates(600.0, 280.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak6", new MapObjectDoor(MapObjectDoorOrientation.Up), "20,7");


            allFirstBuildingFirstFloorObjects.Add(infos1);
            allFirstBuildingFirstFloorObjects.Add(regular1);
            allFirstBuildingFirstFloorObjects.Add(regular2);
            allFirstBuildingFirstFloorObjects.Add(regular3);
            allFirstBuildingFirstFloorObjects.Add(op1);
            allFirstBuildingFirstFloorObjects.Add(op2);
            allFirstBuildingFirstFloorObjects.Add(toilet1);
            allFirstBuildingFirstFloorObjects.Add(dentistRoom1);
            allFirstBuildingFirstFloorObjects.Add(dentistRoom2);
            allFirstBuildingFirstFloorObjects.Add(canteen1);
            allFirstBuildingFirstFloorObjects.Add(elevator1);

            allFirstBuildingSecondFloorObjects.Add(infos2);
            allFirstBuildingSecondFloorObjects.Add(toilet2);
            allFirstBuildingSecondFloorObjects.Add(elevator2);
            allFirstBuildingSecondFloorObjects.Add(recoveryRoom1);
            allFirstBuildingSecondFloorObjects.Add(onDuty1);
            allFirstBuildingSecondFloorObjects.Add(recoveryRoom2);
            allFirstBuildingSecondFloorObjects.Add(recoveryRoom3);
            allFirstBuildingSecondFloorObjects.Add(recoveryRoom4);
            allFirstBuildingSecondFloorObjects.Add(recoveryRoom5);
            allFirstBuildingSecondFloorObjects.Add(onDuty2);
            allFirstBuildingSecondFloorObjects.Add(recoveryRoom6);


            MapObject elevator3 = new MapObject(34, new MapObjectMetrics(new MapObjectCoordinates(570.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, "Lift", new MapObjectDoor(MapObjectDoorOrientation.Left), "Maksimalno mogu stati 4 osobe");
            MapObject infos3 = new MapObject(35, new MapObjectMetrics(new MapObjectCoordinates(0.0, 150.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.Informations, "Informacije3", new MapObjectDoor(MapObjectDoorOrientation.Right), "07:00 - 00:00");
            MapObject toilet3 = new MapObject(36, new MapObjectMetrics(new MapObjectCoordinates(0.0, 300.0), new MapObjectDimensions(100.0, 100.0)), MapObjectType.Toilet, "Toalet", new MapObjectDoor(MapObjectDoorOrientation.Right), "");
            MapObject regular4 = new MapObject(37, new MapObjectMetrics(new MapObjectCoordinates(0.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.ExaminationRoom, "Opsti pregled3", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 21:00,2,3,5,6");
            MapObject regular5 = new MapObject(38, new MapObjectMetrics(new MapObjectCoordinates(140.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.ExaminationRoom, "Opsti pregled4", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 21:00,2,3,5,6");
            MapObject onDuty3 = new MapObject(39, new MapObjectMetrics(new MapObjectCoordinates(310.0, 0.0), new MapObjectDimensions(120.0, 120.0)), MapObjectType.OnDuty, "Dezurno osoblje", new MapObjectDoor(MapObjectDoorOrientation.Down), "00:00 - 24:00");
            MapObject dentistRoom3 = new MapObject(40, new MapObjectMetrics(new MapObjectCoordinates(460.0, 0.0), new MapObjectDimensions(180.0, 120.0)), MapObjectType.DentistryRoom, "Stomatoloska ordinacija3", new MapObjectDoor(MapObjectDoorOrientation.Down), "09:00 - 19:00,100,100,3,20");
            MapObject neurology1 = new MapObject(41, new MapObjectMetrics(new MapObjectCoordinates(180.0, 280.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.NeurologyRoom, "Neurologija1", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 21:00,2,3,5,6");
            MapObject dermatology1 = new MapObject(42, new MapObjectMetrics(new MapObjectCoordinates(340.0, 280.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.DermatologyRoom, "Dermatologija1", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 21:00,2,3,5,6");
            MapObject ophthalmology1 = new MapObject(43, new MapObjectMetrics(new MapObjectCoordinates(500.0, 280.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.OphthalmologyRoom, "Oftamologija1", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 21:00,2,3,5,6");

            MapObject elevator4 = new MapObject(44, new MapObjectMetrics(new MapObjectCoordinates(570.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, "Lift", new MapObjectDoor(MapObjectDoorOrientation.Left), "Maksimalno mogu stati 4 osobe");
            MapObject infos4 = new MapObject(45, new MapObjectMetrics(new MapObjectCoordinates(0.0, 150.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.Informations, "Informacije4", new MapObjectDoor(MapObjectDoorOrientation.Right), "07:00 - 00:00");
            MapObject toilet4 = new MapObject(46, new MapObjectMetrics(new MapObjectCoordinates(0.0, 300.0), new MapObjectDimensions(100.0, 100.0)), MapObjectType.Toilet, "Toalet", new MapObjectDoor(MapObjectDoorOrientation.Right), "");
            MapObject recoveryRoom7 = new MapObject(47, new MapObjectMetrics(new MapObjectCoordinates(0.0, 0.0), new MapObjectDimensions(220.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak7", new MapObjectDoor(MapObjectDoorOrientation.Down), "20,7");
            MapObject recoveryRoom8 = new MapObject(48, new MapObjectMetrics(new MapObjectCoordinates(220.0, 0.0), new MapObjectDimensions(220.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak8", new MapObjectDoor(MapObjectDoorOrientation.Down), "20,7");
            MapObject onDuty4 = new MapObject(49, new MapObjectMetrics(new MapObjectCoordinates(470.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.OnDuty, "Dezurno osoblje", new MapObjectDoor(MapObjectDoorOrientation.Down), "00:00 - 24:00");
            MapObject canteen2 = new MapObject(50, new MapObjectMetrics(new MapObjectCoordinates(220.0, 280.0), new MapObjectDimensions(200.0, 120.0)), MapObjectType.Canteen, "Kantina", new MapObjectDoor(MapObjectDoorOrientation.Up), "09:00 - 18:00,50,100,10,40");
            MapObject recoveryRoom9 = new MapObject(51, new MapObjectMetrics(new MapObjectCoordinates(420.0, 280.0), new MapObjectDimensions(220.0, 120.0)), MapObjectType.RecoveryRoom, "Sala za oporavak9", new MapObjectDoor(MapObjectDoorOrientation.Up), "20,7");


            allSecondBuildingFirstFloorObjects.Add(elevator3);
            allSecondBuildingFirstFloorObjects.Add(infos3);
            allSecondBuildingFirstFloorObjects.Add(toilet3);
            allSecondBuildingFirstFloorObjects.Add(regular4);
            allSecondBuildingFirstFloorObjects.Add(regular5);
            allSecondBuildingFirstFloorObjects.Add(onDuty3);
            allSecondBuildingFirstFloorObjects.Add(dentistRoom3);
            allSecondBuildingFirstFloorObjects.Add(neurology1);
            allSecondBuildingFirstFloorObjects.Add(dermatology1);
            allSecondBuildingFirstFloorObjects.Add(ophthalmology1);

            allSecondBuildingSecondFloorObjects.Add(elevator4);
            allSecondBuildingSecondFloorObjects.Add(infos4);
            allSecondBuildingSecondFloorObjects.Add(toilet4);
            allSecondBuildingSecondFloorObjects.Add(recoveryRoom7);
            allSecondBuildingSecondFloorObjects.Add(recoveryRoom8);
            allSecondBuildingSecondFloorObjects.Add(onDuty4);
            allSecondBuildingSecondFloorObjects.Add(canteen2);
            allSecondBuildingSecondFloorObjects.Add(recoveryRoom9);
        }
    }
}
