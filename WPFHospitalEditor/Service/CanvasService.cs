using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Service
{
    public static class CanvasService
    {
        public static void addObjectToCanvas(List<MapObject> objectsToShow, Canvas canvas)
        {
            for (int i = 0; i < objectsToShow.Count; i++)
            {
                canvas.Children.Add(objectsToShow[i].rectangle);
                canvas.Children.Add(objectsToShow[i].nameOnMap);
                canvas.Children.Add(objectsToShow[i].MapObjectDoor.rectangle);
                Canvas.SetLeft(objectsToShow[i].rectangle, objectsToShow[i].MapObjectMetrics.MapObjectCoordinates.X);
                Canvas.SetTop(objectsToShow[i].rectangle, objectsToShow[i].MapObjectMetrics.MapObjectCoordinates.Y);
            }
        }
        public static MapObject checkWhichObjectIsClicked(MouseButtonEventArgs e, List<MapObject> allMapObjectsShowed, Canvas canvas)
        {
            for (int i = 0; i < allMapObjectsShowed.Count; i++)
            {
                if (checkIfPointIsInRectangle(e, allMapObjectsShowed[i], canvas))
                {
                    return allMapObjectsShowed[i];
                }
            }
            return null;
        }

        private static Boolean checkIfPointIsInRectangle(MouseButtonEventArgs e, MapObject mapObject, Canvas canvas)
        {
            return (e.GetPosition(canvas).X > mapObject.MapObjectMetrics.MapObjectCoordinates.X
                    && e.GetPosition(canvas).X < mapObject.MapObjectMetrics.MapObjectCoordinates.X + mapObject.MapObjectMetrics.MapObjectDimensions.Width
                    && e.GetPosition(canvas).Y > mapObject.MapObjectMetrics.MapObjectCoordinates.Y
                    && e.GetPosition(canvas).Y < mapObject.MapObjectMetrics.MapObjectCoordinates.Y + mapObject.MapObjectMetrics.MapObjectDimensions.Height);
        }
    }
}
