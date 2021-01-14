﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using System.Windows.Media;
using WPFHospitalEditor.Pages;

namespace WPFHospitalEditor.Pages
{
    /// <summary>
    /// Interaction logic for BuildingPage.xaml
    /// </summary>
    public partial class BuildingPage : Page
    {
        private Dictionary<int, Floor> buildingFloors = new Dictionary<int, Floor>();
        MapObjectController mapObjectController = new MapObjectController();
        public static Canvas canvasBuilding;
        private int id;

        public BuildingPage(int id, int selectedFloor = 0)
        {
            InitializeComponent();
            ClearAll();
            this.id = id;
            PopulateBuildingFloors();
            SetFloorComboBox();
            floor.SelectedIndex = selectedFloor;
            canvasBuilding = canvas;
        }

        public Floor GetBuildingFloor(int floorNumber)
        {
            return buildingFloors[floorNumber];
        }
        private void PopulateBuildingFloors()
        {
            foreach (MapObject mapObject in mapObjectController.GetAllBuildingMapObjects(id))
            {
                int index = mapObject.MapObjectDescription.FloorNumber;
                if (!buildingFloors.ContainsKey(index))
                    buildingFloors.Add(index, new Floor());

                if (IsMapObjectSelected(mapObject.Id))
                    mapObject.rectangle.Fill = Brushes.Red;

                buildingFloors[index].AddMapObject(mapObject);
            }
        }

        private bool IsMapObjectSelected(int id)
        {
            return SearchResultDialog.selectedObjectId == id;
        }

        private void SetFloorComboBox()
        {
            for (int i = 0; i < buildingFloors.Count; i++)
                floor.Items.Add(i + ". floor");
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            ClearAll();
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), HospitalMapPage.canvasHospitalMap);
            HospitalMainWindow window = HospitalMainWindow.GetInstance(HospitalMainWindow.role);
            window.ChangePage(new HospitalMapPage());
        }

        private void FloorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAll();
            int index = floor.SelectedIndex;
            if (buildingFloors.Count == 0) return;
            CanvasService.AddObjectToCanvas(buildingFloors[index].GetAllFloorMapObjects(), canvas);
            DisplayLegend(buildingFloors[index].GetAllFloorMapObjects());
        }

        private void ClearAll()
        {
            if (legend == null) return;
            canvas.Children.Clear();
            legend.Children.Clear();
        }

        private void DisplayLegend(List<MapObject> displayedMapObjects)
        {
            if (legend == null) return;
            HashSet<MapObjectType> mapObjectTypes = FindAllMapObjectTypesOnFloor(displayedMapObjects);
            AddingRowsToGrid(mapObjectTypes);

            int index = 0;
            foreach (MapObjectType mapObjectType in mapObjectTypes)
            {
                OrganiseLegend(mapObjectType, index);
                index++;
            }
        }

        private HashSet<MapObjectType> FindAllMapObjectTypesOnFloor(List<MapObject> displayedMapObjects)
        {
            HashSet<MapObjectType> mapObjectTypes = new HashSet<MapObjectType>();
            for (int i = 0; i < displayedMapObjects.Count; i++)
                mapObjectTypes.Add(displayedMapObjects[i].MapObjectType);

            return mapObjectTypes;
        }

        private void AddingRowsToGrid(HashSet<MapObjectType> mapObjectTypes)
        {
            int numberOfRows = (mapObjectTypes.Count / legend.ColumnDefinitions.Count) + 1;
            for (int i = 0; i < numberOfRows; i++)
                legend.RowDefinitions.Add(new RowDefinition() { });
        }

        private void OrganiseLegend(MapObjectType mapObjectType, int index)
        {
            Rectangle rectangle = CreateRectangleInLegend(mapObjectType);
            TextBlock textblock = CreateTextBlockInLegend(mapObjectType);
            SettingPosition(index, rectangle, textblock);
            AddToLegend(rectangle, textblock);
        }

        private Rectangle CreateRectangleInLegend(MapObjectType mapObjectType)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = MapObjectColors.getInstance().getColor(mapObjectType);
            rectangle.Width = 25;
            rectangle.Height = 25;
            return rectangle;
        }

        private TextBlock CreateTextBlockInLegend(MapObjectType mapObjectType)
        {
            TextBlock textblock = new TextBlock();
            textblock.Text = mapObjectType.ToString() + "-  ";
            return textblock;
        }

        private void SettingPosition(int index, Rectangle rectangle, TextBlock textblock)
        {
            int row = (index / legend.ColumnDefinitions.Count);
            int column = index - row * legend.ColumnDefinitions.Count;
            rectangle.SetValue(Grid.ColumnProperty, column);
            rectangle.SetValue(Grid.RowProperty, row);
            textblock.SetValue(Grid.ColumnProperty, column);
            textblock.SetValue(Grid.RowProperty, row);
        }

        private void AddToLegend(Rectangle rectangle, TextBlock textblock)
        {
            legend.Children.Add(rectangle);
            legend.Children.Add(textblock);
        }

        private void SelectMapObject(object sender, MouseButtonEventArgs e)
        {
            int index = floor.SelectedIndex;
            MapObject chosenMapObject = CanvasService.CheckWhichObjectIsClicked(e, buildingFloors[index].GetAllFloorMapObjects(), this.canvas);
            if (chosenMapObject != null)
                OpenAdditionalInformationDialog(chosenMapObject);
        }

        private void OpenAdditionalInformationDialog(MapObject mapObject)
        {
            AdditionalInformation additionalInformation = new AdditionalInformation(mapObject, this);
            additionalInformation.ShowDialog();
        }
    }
}
