using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace RandomString
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly List<TextBox> _textBoxs;
        private readonly Random _rnd;

        public MainWindow()
        {
            InitializeComponent();

            _textBoxs = new List<TextBox>();
            _rnd = new Random();

            var count = int.Parse(ConfigurationManager.AppSettings["MaxTextBoxCount"]);

            for (var i = 1; i <= count; i++)
            {
                ComboBoxCount.Items.Add(i);
            }

            ComboBoxCount.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var textBoxCount = int.Parse(ComboBoxCount.SelectedValue.ToString());

            TextBoxsGrid.Children.Clear();
            _textBoxs.Clear();

            TextBoxsGrid.Height = textBoxCount*20 + 10;

            for (var i = 0; i < textBoxCount; i++)
            {
                var textBox = new TextBox
                {
                    Margin = new Thickness(10, i*20, 15, 10)
                };

                TextBoxsGrid.Children.Add(textBox);
                _textBoxs.Add(textBox);
            }
        }

        private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            var strings = new List<string>();

            foreach (var textBox in _textBoxs)
            {
                strings.Add(textBox.Text);
            }

            var result = string.Empty;

            while (strings.Count != 0)
            {
                var ind = _rnd.Next(strings.Count);

                result += strings[ind] + Environment.NewLine;

                strings.RemoveAt(ind);
            }

            TextBoxResult.Text = result;
        }
    }
}
