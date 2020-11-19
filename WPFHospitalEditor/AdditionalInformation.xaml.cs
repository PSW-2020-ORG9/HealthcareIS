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
    /// Interaction logic for AdditionalInformation.xaml
    /// </summary>
    public partial class AdditionalInformation : Window
    {
        public AdditionalInformation(MapObject mapObject)
        {
            InitializeComponent();
            string[] contentRows = mapObject.Description.Split(";");
            this.Height = (contentRows.Length + 2) * 50 + 30;
            CreateDynamicWPFGrid(mapObject, contentRows);   
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateDynamicWPFGrid(MapObject mapObject, string[] contentRows)

        {

            // Create the Grid

            Grid DynamicGrid = new Grid();
            var bc = new BrushConverter();
            DynamicGrid.Background = (Brush)bc.ConvertFrom("#FFC6F5F8");

            createColumns(DynamicGrid);

            createRows(contentRows, DynamicGrid);

            addMapObjectName(mapObject, bc, DynamicGrid);

            createRowContent(contentRows, DynamicGrid);

            Border.Child = DynamicGrid;
        }

        private void createRowContent(string[] contentRows, Grid DynamicGrid)
        {
            List<string> labelContent = new List<string>();
            List<string> value = new List<string>();

            for (int i = 0; i < contentRows.Length; i++)
            {
                if (contentRows[i].Equals(""))
                    break;

                string[] label = contentRows[i].Split("=");
                labelContent.Add(label[0]);
                value.Add(label[1]);
            }

            insertData(DynamicGrid, labelContent, value);

            if (!contentRows.Equals(""))
            {
                addCancelButton(contentRows, DynamicGrid);
            }
            
            addOkButton(contentRows, DynamicGrid);
        }

        private void addOkButton(string[] contentRows, Grid DynamicGrid)
        {
            Button ok = new Button();
            ok.Content = "OK";
            ok.BorderThickness = new Thickness(0);
            ok.HorizontalAlignment = HorizontalAlignment.Right;
            ok.VerticalAlignment = VerticalAlignment.Center;
            ok.Background = Brushes.SkyBlue;
            ok.Width = 100;
            ok.Height = 25;
            ok.Foreground = Brushes.White;
            ok.Click += Done_Click;
            Grid.SetRow(ok, contentRows.Length + 2);
            Grid.SetColumn(ok, 2);

            DynamicGrid.Children.Add(ok);
        }

        private void addCancelButton(string[] contentRows, Grid DynamicGrid)
        {
            Button cancel = new Button();
            cancel.Content = "Poništi";
            cancel.BorderThickness = new Thickness(0);
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.VerticalAlignment = VerticalAlignment.Center;
            cancel.Background = Brushes.SkyBlue;
            cancel.Width = 100;
            cancel.Height = 25;
            cancel.Foreground = Brushes.White;
            cancel.Click += Close_Click;
            Grid.SetRow(cancel, contentRows.Length + 2);
            Grid.SetColumn(cancel, 1);

            DynamicGrid.Children.Add(cancel);
        }

        private static void insertData(Grid DynamicGrid, List<string> labelContent, List<string> value)
        {
            for (int i = 0; i < labelContent.Count; i++)
            {
                Label label = new Label();
                label.Content = labelContent[i];
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.FontSize = 20;
                Grid.SetRow(label, i + 2);
                Grid.SetColumn(label, 1);
                DynamicGrid.Children.Add(label);
               
                TextBox textBox = new TextBox();
                textBox.Text = value[i];
                textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                textBox.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(textBox, i + 2);
                Grid.SetColumn(textBox, 2);
                DynamicGrid.Children.Add(textBox);
            }
        }

        private void addMapObjectName(MapObject mapObject, BrushConverter bc, Grid DynamicGrid)
        {
            TextBox name = new TextBox();

            name.Text = mapObject.name.Text;

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

        private void createRows(string[] contentRows, Grid DynamicGrid)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(2);
            DynamicGrid.RowDefinitions.Add(gridRow1);

            for (int i = 0; i <= contentRows.Length; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(50);
                DynamicGrid.RowDefinitions.Add(gridRow);
            }
            RowDefinition gridRowCancelOK = new RowDefinition();
            gridRowCancelOK.Height = new GridLength(50);
            DynamicGrid.RowDefinitions.Add(gridRowCancelOK);
        }

        private void createColumns(Grid DynamicGrid)
        {
            ColumnDefinition borderColumn1 = new ColumnDefinition();
            borderColumn1.Width = new GridLength(15);

            ColumnDefinition labelContentColumn = new ColumnDefinition();
            labelContentColumn.Width = new GridLength(150);

            ColumnDefinition valueColumn = new ColumnDefinition();
            valueColumn.Width = new GridLength(150);

            ColumnDefinition borderColumn2 = new ColumnDefinition();
            borderColumn1.Width = new GridLength(20);

            DynamicGrid.ColumnDefinitions.Add(borderColumn1);

            DynamicGrid.ColumnDefinitions.Add(labelContentColumn);

            DynamicGrid.ColumnDefinitions.Add(valueColumn);

            DynamicGrid.ColumnDefinitions.Add(borderColumn2);
        }
    }
}
