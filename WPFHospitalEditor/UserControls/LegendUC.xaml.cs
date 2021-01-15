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
            List <MapObjectType> mapObjectTypes = getUniqueTypes(mapObjects);
            FillLegend(mapObjectTypes, columns);
        }

        private void FillLegend(List<MapObjectType> mapObjectTypes, int columns)
        {
            int row = 0;
            for(int i=0; i < mapObjectTypes.Count; i++)
            {
                int col = i % columns;
                if (col == 0)
                {
                    Legend.RowDefinitions.Add(new RowDefinition());
                    row++;
                }
                InsertValueToCell(mapObjectTypes[i], row, col);
            }
        }

        private void InsertValueToCell(MapObjectType mapObjectType, int row, int column)
        {
            DockPanel dp = CreateLegendCell(mapObjectType);
            dp.SetValue(Grid.ColumnProperty, column);
            dp.SetValue(Grid.RowProperty, row);
            Legend.Children.Add(dp);
        }

        private DockPanel CreateLegendCell(MapObjectType mapObjectType)
        {
            Rectangle rectangle = CreateRectangle(mapObjectType);
            TextBlock textblock = CreateTextBlock(mapObjectType);
            DockPanel dp = new DockPanel();
            DockPanel.SetDock(rectangle, Dock.Right);
            DockPanel.SetDock(textblock, Dock.Left);
            return dp;
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

        private List<MapObjectType> getUniqueTypes(List<MapObject> mapObjects)
        {
            HashSet<MapObjectType> mapObjectTypes = new HashSet<MapObjectType>();
            foreach (MapObject mo in mapObjects)
                mapObjectTypes.Add(mo.MapObjectType);

            return new List<MapObjectType>(mapObjectTypes);
        }
    }
}
