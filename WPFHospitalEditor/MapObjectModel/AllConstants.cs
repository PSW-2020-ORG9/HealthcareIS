using System.IO;

namespace WPFHospitalEditor.MapObjectModel
{

    public static class AllConstants
    {
        public const double doorWidth = 30;
        public const double doorHeight = 6;
        public static char separator = Path.DirectorySeparatorChar;
        public readonly static string MAPOBJECT_PATH = $"..{separator}..{separator}..{separator}Repository{separator}Data{separator}AllMapObjects.json";
    }
}