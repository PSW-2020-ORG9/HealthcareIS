using HealthcareBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class Equipment : Window
    {
        Dictionary<String,EquipmentDto> allEquipment = new Dictionary<string, EquipmentDto>();
        private List<string> labelContent = new List<string>();
        private List<string> value = new List<string>();
        private List<Label> labels = new List<Label>();
        private List<TextBox> textBoxes = new List<TextBox>();

        public Equipment(Dictionary<String, EquipmentDto> allEquipment)
        {
            InitializeComponent();
            this.allEquipment = allEquipment;
            this.Height = (allEquipment.Count() + 2) * 50 + 30;
            ModifyDynamicWPFGrid();
        }

        private void ModifyDynamicWPFGrid()
        {
            createColumns();
            createRows(allEquipment.Count());
            createRowContent(allEquipment.Count());
            Border.Child = DynamicGrid;
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

        private void createRows(int rowNumbers)
        {
            createOneRow(2);
            for (int i = 0; i <= rowNumbers + 1; i++)
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

        private void createRowContent(int rowNumbers)
        {
                for (int i = 0; i < rowNumbers; i++)
                {
                    if (allEquipment.Count() == 0) break;
                    setRowContent(i);
                }
            
            insertData();
            addCancelButton(rowNumbers);
        }

        private void setRowContent(int row)
        {
            labelContent.Add(allEquipment.ElementAt(row).Value.Name);
            value.Add(allEquipment.ElementAt(row).Value.Quantity.ToString());
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
            textBox.IsReadOnly = true;
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

        private void addCancelButton(int rowNumbers)
        {
            Button cancel = new Button();
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.Click += Close_Click;
            Grid.SetRow(cancel, rowNumbers + 2);
            Grid.SetColumn(cancel, 1);
            setButtonsCommonAttributes(cancel, "Cancel");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void setButtonsCommonAttributes(Button button, string name)
        {
            button.BorderThickness = new Thickness(0);
            button.Content = name;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = Brushes.SkyBlue;
            button.Width = AllConstants.additionalInformationsbuttonWidth;
            button.Height = AllConstants.additionalInformationsbuttonHeight;
            button.Foreground = Brushes.White;
            DynamicGrid.Children.Add(button);

        }

    }
}
