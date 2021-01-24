using System.IO;

namespace WPFHospitalEditor
{
    public static class AllConstants
    {
        public const double DoorWidth = 30;
        public const double DoorHeight = 6;
        public static char Separator = Path.DirectorySeparatorChar;
        public readonly static string MAPOBJECT_PATH = $"..{Separator}..{Separator}..{Separator}..{Separator}WPFHospitalEditor{Separator}Repository{Separator}Data{Separator}AllMapObjects.json";
        public const string ConnectionUrl = "http://localhost:5000/";
        public const string DescriptionSeparator = "=";
        public const string ContentSeparator = "=";
        public const string EmptyComboBox = "None";
        public const string DayStart = " 00:00";
        public const string DayEnd = " 23:59";
        public const int RegularExaminationDepartment = 1;
        public const int StorageRoomId = 17;
        public const string AuthorizationTokenKey = "Authorization";
        public const int PatientIdForRelocation = 100;
        public const int RegularSpecialtyId = 2;

    }
}
