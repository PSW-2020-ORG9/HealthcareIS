using System;
using System.Collections.Generic;
using System.Text;
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
        private List<Label> labels = new List<Label>();
        private List<TextBox> textBoxes = new List<TextBox>();

        public DynamicGridControl(String[] contentRows, Boolean isReadOnly)
        {
            InitializeComponent();
            this.contentRows = contentRows;
            InitializeGridControl(contentRows.Length);
            AddRowContent(isReadOnly);
        }


        public String GetAllContent()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            for (int i = 0; i < labels.Count; i++)
                stringBuilder.Append(labels[i].Content.ToString() + AllConstants.DescriptionSeparator + textBoxes[i].Text + ";");

            return stringBuilder.ToString();
        }

        private void InitializeGridControl(int rows)
        {
            GridControl.Children.Clear();
            CreateEmptyRows(rows);
        }

        private void CreateEmptyRows(int rows)
        {
            for (int i = 0; i < rows; i++)
                CreateRow(50);
        }

        private void CreateRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            GridControl.RowDefinitions.Add(gridRow1);
        }

        private void AddRowContent(bool isReadOnly)
        {
            for (int i = 0; i < contentRows.Length; i++)
            {
                if (contentRows[i].Equals("")) break;
                String[] rowContent = contentRows[i].Split(AllConstants.DescriptionSeparator);
                InsertLabel(rowContent[0], i);
                InsertTextBox(rowContent[1], i, isReadOnly);
            }
        }

        private void InsertLabel(String labelContent, int row)
        {
            Label label = new Label();
            label.Content = labelContent;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.FontSize = 20;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 1);
            labels.Add(label);
            GridControl.Children.Add(label);
        }

        private void InsertTextBox(String textBoxContent, int row, bool isReadOnly)
        {
            TextBox textBox = new TextBox();
            textBox.Text = textBoxContent;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 2);
            if (isReadOnly)
                textBox.IsReadOnly = true;
            
            textBoxes.Add(textBox);
            GridControl.Children.Add(textBox);
        }
    }
}
