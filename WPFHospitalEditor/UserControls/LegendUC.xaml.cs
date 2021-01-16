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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.UserControls
{
    /// <summary>
    /// Interaction logic for LegendUC.xaml
    /// </summary>
    public partial class LegendUC : UserControl
    {
        public LegendUC(List<MapObject> mapObjects, int columns=4)
        {
            InitializeComponent();
            DefineColumns(columns);
            List <MapObjectType> mapObjectTypes = getUniqueTypes(mapObjects);
            FillLegend(mapObjectTypes, columns);
        }



        private void DefineColumns(int columns)
        {
            for(int i = 0; i < columns; i++) 
                Legend.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private List<MapObjectType> getUniqueTypes(List<MapObject> mapObjects)
        {
            HashSet<MapObjectType> mapObjectTypes = new HashSet<MapObjectType>();
            foreach (MapObject mo in mapObjects)
                mapObjectTypes.Add(mo.MapObjectType);

            return new List<MapObjectType>(mapObjectTypes);
        }

        private void FillLegend(List<MapObjectType> mapObjectTypes, int columns)
        {
            int row = -1;
            for(int i=0; i < mapObjectTypes.Count; i++)
            {
                
                int col = i % columns;
                if (col == 0)
                {
                    CreateGridRow();
                    row++;
                }
                InsertValueToCell(mapObjectTypes[i], row, col);
            }
        }

        private void CreateGridRow()
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(35);
            Legend.RowDefinitions.Add(rowDefinition);
        }

        private void InsertValueToCell(MapObjectType mapObjectType, int row, int column)
        {
            Grid cell = CreateLegendCell(row, column);

            InsertRectangleToCell(mapObjectType, cell);
            InsertTextToCell(mapObjectType, cell);

            Legend.Children.Add(cell);

        }

        private void InsertTextToCell(MapObjectType mapObjectType, Grid cell)
        {
            TextBlock textblock = CreateTextBlock(mapObjectType);
            textblock.SetValue(Grid.ColumnProperty, 1);
            textblock.VerticalAlignment = VerticalAlignment.Center;
            cell.Children.Add(textblock);
        }

        private void InsertRectangleToCell(MapObjectType mapObjectType, Grid cell)
        {
            Rectangle rectangle = CreateRectangle(mapObjectType);
            rectangle.SetValue(Grid.ColumnProperty, 0);
            rectangle.VerticalAlignment = VerticalAlignment.Center;
            cell.Children.Add(rectangle);
        }

        private Grid CreateLegendCell(int row,int column)
        {
            Grid grid = new Grid();
            
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(30);
            grid.ColumnDefinitions.Add(column2);
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.SetValue(Grid.ColumnProperty, column);
            grid.SetValue(Grid.RowProperty, row);

            return grid;

        }
        private TextBlock CreateTextBlock(MapObjectType mapObjectType)
        {
            TextBlock textblock = new TextBlock();
            textblock.Text = mapObjectType.ToString();
            return textblock;
        }

        private Rectangle CreateRectangle(MapObjectType mapObjectType)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = MapObjectColors.getInstance().getColor(mapObjectType);
            rectangle.Width = 25;
            rectangle.Height = 25;
            return rectangle;
        }

        
    }
}
