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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : Window
    {
        List<Button> advanceSearchButtons = new List<Button>();
        int firstContentRowNumber = 2;

        public SearchResultDialog()
        {
            this.Height = (HospitalMap.searchResult.Count + 1) * 50 + 10;
            InitializeComponent();
            CreateDynamicGrid();
        }

        private void CreateDynamicGrid()
        {
            createRows();
            createRowContent();
            Border.Child = DynamicGrid;
        }

        private void createRowContent()
        {
            foreach (MapObject mapObject in HospitalMap.searchResult)
            {
                if (mapObject.MapObjectType.Equals(MapObjectType.Building))
                {
                    createBuildingRowData(mapObject);
                }
                else
                {
                    createRowData(mapObject);
                }
            }
        }

        

        private int createRowData(MapObject mapObject)
        {
            addLabels(mapObject);

            addAdvancedSearchButton();

            addSeparator();

            firstContentRowNumber++;

            return firstContentRowNumber;
        }

        private void addLabels(MapObject mapObject)
        {
            for(int i = 1; i <= 3; i++)
            {
                Label label = new Label();

                adjustLabelProperties(mapObject, label, i);

                Grid.SetRow(label, firstContentRowNumber);
                Grid.SetColumn(label, i);

                DynamicGrid.Children.Add(label);
            }
        }

        private void adjustLabelProperties(MapObject mapObject, Label label, int i)
        {
            switch(i)
            {
                case 1:
                    {
                        label.Content = mapObject.Name;
                        label.HorizontalAlignment = HorizontalAlignment.Center;
                        label.VerticalAlignment = VerticalAlignment.Center;
                    }
                    break;
                case 2:
                    {
                        label.Content = Building.findBuilding(mapObject);
                        label.HorizontalAlignment = HorizontalAlignment.Center;
                        label.VerticalAlignment = VerticalAlignment.Center;
                    }
                    break;
                case 3:
                    {
                        label.Content = Building.findFloor(mapObject);
                        label.HorizontalAlignment = HorizontalAlignment.Center;
                        label.VerticalAlignment = VerticalAlignment.Center;
                    }
                    break;
                default:
                    break;
            }
        }

        private void addNameLabel(MapObject mapObject)
        {
            Label name = new Label();

            adjustNameLabelProperties(mapObject, name);

            Grid.SetRow(name, firstContentRowNumber);
            Grid.SetColumn(name, 1);

            DynamicGrid.Children.Add(name);
        }

        private static void adjustNameLabelProperties(MapObject mapObject, Label name)
        {
            name.Content = mapObject.Name;
            name.HorizontalAlignment = HorizontalAlignment.Center;
            name.VerticalAlignment = VerticalAlignment.Center;
        }

        private void addAdvancedSearchButton()
        {
            Button advancedSearch = new Button();

            adjustAdvancedSearchButtonProperties(advancedSearch);

            advanceSearchButtons.Add(advancedSearch);

            //advancedSearch.Click += Advanced_Search_Click(mapObject, i);

            Grid.SetRow(advancedSearch, firstContentRowNumber);
            Grid.SetColumn(advancedSearch, 4);

            DynamicGrid.Children.Add(advancedSearch);
        }

        private static void adjustAdvancedSearchButtonProperties(Button advancedSearch)
        {
            advancedSearch.HorizontalAlignment = HorizontalAlignment.Center;
            advancedSearch.VerticalAlignment = VerticalAlignment.Center;
            advancedSearch.Content = "+";
            advancedSearch.FontSize = 35;
            advancedSearch.Foreground = Brushes.White;
            advancedSearch.Background = Brushes.SkyBlue;
            advancedSearch.FontWeight = FontWeights.UltraBold;
            advancedSearch.Width = 35;
            advancedSearch.Height = 35;
            advancedSearch.HorizontalContentAlignment = HorizontalAlignment.Center;
            advancedSearch.VerticalContentAlignment = VerticalAlignment.Top;
            advancedSearch.BorderThickness = new Thickness(0);
            advancedSearch.Padding = new Thickness(0, -10, 0, 0);
        }

        private void addSeparator()
        {
            Separator separator = new Separator();

            separator.VerticalAlignment = VerticalAlignment.Bottom;

            Grid.SetRow(separator, firstContentRowNumber);
            Grid.SetColumn(separator, 1);
            Grid.SetColumnSpan(separator, 4);

            DynamicGrid.Children.Add(separator);
        }

        private int createBuildingRowData(MapObject mapObject)
        {
            addNameLabel(mapObject);

            addAdvancedSearchButton();

            addSeparator();

            firstContentRowNumber++;

            return firstContentRowNumber;
        }

        private void createOneRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow1);
        }

        private void createRows()
        {
            for (int i = 0; i < HospitalMap.searchResult.Count; i++)
            {
                createOneRow(50);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            HospitalMap.searchResult.Clear();
            Close();
        }

        /*private void Advanced_Search_Click(MapObject mapObject, int i)
        {
            button.Click += new EventHandler(
                (sender, e) => button_Click(sender, e, grid, i)
            );
        }*/
    }
}
