using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using System.IO;
using System.Diagnostics;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AdditionalInformation.xaml
    /// </summary>
    public partial class AdditionalInformation : Window
    {
        const int buttonWidth = 100;
        const int buttonHeight = 25;

        private MapObject mapObject;
        private List<string> labelContent = new List<string>();
        private List<string> value = new List<string>();
        private List<Label> labels = new List<Label>();
        private List<TextBox> textBoxes = new List<TextBox>();
        private TextBox name = new TextBox();
        private int floor;
        private Building building;
        private string[] descriptionParts;

        MapObjectController mapObjectController = new MapObjectController(new MapObjectService(new MapObjectRepository(new FileRepository(AllConstants.MAPOBJECT_PATH))));

        public AdditionalInformation(MapObject mapObject, Building building, int floor)
        {
            this.floor = floor;
            this.building = building;
            InitializeComponent();
            this.mapObject = mapObject;
            descriptionParts = mapObject.Description.Split("&");
            string[] contentRows = descriptionParts[1].Split(";");
            this.Height = (contentRows.Length + 2) * 50 + 30;
            CreateDynamicWPFGrid(contentRows);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            mapObject.Description = descriptionParts[0] + "&";

            for (int i = 0; i < labels.Count; i++)
            {    
                mapObject.Description += labels[i].Content + "=" + textBoxes[i].Text + ";";             
                mapObject.Name = this.name.Text;   
            }

            updateAdditionalInformationWindow();

            int lenght = mapObject.Description.Length;
            mapObject.Description.Substring(0, lenght - 1);
            refreshMap();
            Close();

        }

        private void refreshMap()
        {
            String buildingId = building.getBuildingId(mapObject);
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            List<MapObject> objectsForRefreshing = new List<MapObject>();
            foreach(MapObject mapObjectIterate in allMapObjects)
            {
                if (!mapObjectIterate.Description.Equals(""))
                {
                    compare(objectsForRefreshing, mapObjectIterate, buildingId);                   
                }
            }
            building.canvas.Children.Clear();
            building.hospitalMap.addObjectToCanvas(objectsForRefreshing, building.canvas);
        }

        private void compare(List<MapObject> objectsForRefreshing, MapObject mapObjectIterate, String buildingId)
        {
            
            String[] firstSplit = mapObjectIterate.Description.Split("&");
            String[] secondSplit = firstSplit[0].Split("-");
            if (secondSplit[0].Equals(buildingId) && secondSplit[1].Equals(floor.ToString()))
            {
                objectsForRefreshing.Add(mapObjectIterate);
            }
        }

        private void updateAdditionalInformationWindow()
        {
            mapObjectController.update(mapObject);
        }

        private void CreateDynamicWPFGrid(string[] contentRows)
        {
            var bc = new BrushConverter();
            createColumns();
            createRows(contentRows);
            addMapObjectName(mapObject, bc);
            createRowContent(contentRows);
            Border.Child = DynamicGrid;
        }

        private void createRowContent(string[] contentRows)
        {
            for (int i = 0; i < contentRows.Length; i++)
            {
                if (contentRows[i].Equals(""))
                    break;

                string[] label = contentRows[i].Split("=");
                labelContent.Add(label[0]);
                value.Add(label[1]);
            }
            insertData();
            addCancelButton(contentRows);
            addOkButton(contentRows);
        }

        private void addOkButton(string[] contentRows)
        {
            Button ok = new Button();
            ok.Content = "OK";
            ok.BorderThickness = new Thickness(0);
            ok.HorizontalAlignment = HorizontalAlignment.Right;
            ok.VerticalAlignment = VerticalAlignment.Center;
            ok.Background = Brushes.SkyBlue;
            ok.Width = buttonWidth;
            ok.Height = buttonHeight;
            ok.Foreground = Brushes.White;
            ok.Click += Done_Click;
            Grid.SetRow(ok, contentRows.Length + 2);
            Grid.SetColumn(ok, 2);
            DynamicGrid.Children.Add(ok);
        }

        private void addCancelButton(string[] contentRows)
        {
            Button cancel = new Button();
            cancel.Content = "Cancel";
            cancel.BorderThickness = new Thickness(0);
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.VerticalAlignment = VerticalAlignment.Center;
            cancel.Background = Brushes.SkyBlue;
            cancel.Width = buttonWidth;
            cancel.Height = buttonHeight;
            cancel.Foreground = Brushes.White;
            cancel.Click += Close_Click;
            Grid.SetRow(cancel, contentRows.Length + 2);
            Grid.SetColumn(cancel, 1);
            DynamicGrid.Children.Add(cancel);
        }

        private void insertData()
        {
            for (int i = 0; i < labelContent.Count; i++)
            {
                insertLabel(i);
                insertTextBox(i);
            }
        }

        private void insertTextBox(int i)
        {
            TextBox textBox = new TextBox();
            textBox.Text = value[i];
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(textBox, i + 2);
            Grid.SetColumn(textBox, 2);
            textBoxes.Add(textBox);
            DynamicGrid.Children.Add(textBox);
        }

        private void insertLabel(int i)
        {
            Label label = new Label();
            label.Content = labelContent[i];
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 20;
            Grid.SetRow(label, i + 2);
            Grid.SetColumn(label, 1);
            labels.Add(label);
            DynamicGrid.Children.Add(label);
        }

        private void addMapObjectName(MapObject mapObject, BrushConverter bc)
        {
            name.Text = mapObject.Name;
            name.Background = (Brush)bc.ConvertFrom("#FFC6F5F8");
            name.FontSize = 18;
            name.FontWeight = FontWeights.Bold;
            name.Foreground = new SolidColorBrush(Colors.Black);
            name.VerticalAlignment = VerticalAlignment.Center;
            name.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(name, 1);
            Grid.SetColumnSpan(name, 2);
            Grid.SetColumn(name, 1);
            DynamicGrid.Children.Add(name);
        }

        private void createRows(string[] contentRows)
        {
            createOneRow(2);
            for (int i = 0; i <= contentRows.Length + 1; i++)
            {
                createOneRow(50);
            }
        }

        private void createOneRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow1);
        }

        private void createColumns()
        {
            createOneColumn(15);
            createOneColumn(150);
            createOneColumn(150);
            createOneColumn(20);
        }

        private void createOneColumn(int width)
        {
            ColumnDefinition borderColumn1 = new ColumnDefinition();
            borderColumn1.Width = new GridLength(width);
            DynamicGrid.ColumnDefinitions.Add(borderColumn1);
        }
    }
}
