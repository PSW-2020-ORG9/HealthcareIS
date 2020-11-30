﻿using System.IO;

namespace WPFHospitalEditor
{

    public static class AllConstants
    {
        public const double doorWidth = 30;
        public const double doorHeight = 6;
        public static char separator = Path.DirectorySeparatorChar;
        public readonly static string MAPOBJECT_PATH = $"..{separator}..{separator}..{separator}..{separator}WPFHospitalEditor{separator}Repository{separator}Data{separator}AllMapObjects.json";
        public const int additionalInformationsbuttonWidth = 100;
        public const int additionalInformationsbuttonHeight = 25;
        public const string connectionUrl = "http://localhost:5290/";
    }
}