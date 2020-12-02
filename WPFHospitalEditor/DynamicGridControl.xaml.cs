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
        private Boolean isReadOnly;

        public DynamicGridControl(String[] contentRows, Boolean isReadOnly)
        {
            InitializeComponent();
            this.contentRows = contentRows;
            this.isReadOnly = isReadOnly;
            GridControl.Children.Clear();
            createRows(contentRows);
            AddRowContent();
        }

        private void AddRowContent()
        {
            for (int i = 0; i < contentRows.Length; i++)
            {
                if (contentRows[i].Equals("")) break;
                String[] rowContent = contentRows[i].Split(AllConstants.descriptionSeparator);
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
            Grid.SetRow(label, i);
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
            Grid.SetRow(textBox, i);
            Grid.SetColumn(textBox, 2);
            if (isReadOnly)
            {
                textBox.IsReadOnly = true;
            }
            textBoxes.Add(textBox);
            GridControl.Children.Add(textBox);
        }

        private void createRows(string[] contentRows)
        {
            for (int i = 0; i < contentRows.Length; i++)
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
            StringBuilder stringBuilder = new StringBuilder("");
            for (int i = 0; i < labels.Count; i++)
            {
                stringBuilder.Append(labels[i].Content.ToString() + AllConstants.descriptionSeparator + textBoxes[i].Text + ";");
            }
            return stringBuilder.ToString();
        }
    }
}
