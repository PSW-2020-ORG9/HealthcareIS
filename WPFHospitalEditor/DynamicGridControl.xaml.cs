using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for DynamicGridControl.xaml
    /// </summary>
    public partial class DynamicGridControl : UserControl
    {
        private String[] contentRows;
        private String separator;
        private List<Label> labels = new List<Label>();
        private List<TextBox> textBoxes = new List<TextBox>();
        private Role role;
        private Boolean isEquipmentOrMedicine;

        public DynamicGridControl(String[] contentRows, String separator, Role role, Boolean isEquipmentOrMedicine)
        {
            InitializeComponent();
            this.contentRows = contentRows;
            this.separator = separator;
            this.role = role;
            this.isEquipmentOrMedicine = isEquipmentOrMedicine;
            GridControl.Children.Clear();
            createRows(contentRows);
            AddRowContent();
        }

        private void AddRowContent()
        {
            for (int i = 0; i < contentRows.Length; i++)
            {
                if (contentRows[i].Equals("")) break;
                String[] rowContent = contentRows[i].Split(separator);
                insertLabel(rowContent[0], i);
                insertTextBox(rowContent[1], i);
            }
        }

        private void insertLabel(String labelContent, int i)
        {
            Label label = new Label();
            label.Content = labelContent;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 20;
            Grid.SetRow(label, i + 2);
            Grid.SetColumn(label, 1);
            labels.Add(label);
            GridControl.Children.Add(label);
        }

        private void insertTextBox(String textBoxContent, int i)
        {
            TextBox textBox = new TextBox();
            textBox.Text = textBoxContent;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(textBox, i + 2);
            Grid.SetColumn(textBox, 2);
            if (role.Equals(Role.Patient) || isEquipmentOrMedicine == true)
            {
                textBox.IsReadOnly = true;
            }
            textBoxes.Add(textBox);
            GridControl.Children.Add(textBox);
        }

        private void createRows(string[] contentRows)
        {
            createOneRow(2);
            for (int i = 0; i < contentRows.Length + 2; i++)
            {
                createOneRow(50);
            }
        }

        private void createOneRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            GridControl.RowDefinitions.Add(gridRow1);
        }

        public String GetAllContent()
        {
            String result = "";
            for (int i = 0; i < labels.Count; i++)
            {
                result += labels[i].Content.ToString() + separator + textBoxes[i].Text + ";";
            }
            return result;
        }
    }
}
