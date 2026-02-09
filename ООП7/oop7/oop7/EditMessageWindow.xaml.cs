using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace oop7
{
    /// <summary>
    /// Логика взаимодействия для EditMessageWindow.xaml
    /// </summary>
    public partial class EditMessageWindow : Window
    {
        public string MessageText { get; private set; }
        public string ImagePath { get; private set; }

        public EditMessageWindow(string currentMessage, string currentImagePath)
        {
            InitializeComponent();
            MessageTextBox.Text = currentMessage;
            ImagePath = currentImagePath;
            UpdateImageInfo();
        }

        private void UpdateImageInfo()
        {
            if (!string.IsNullOrEmpty(ImagePath))
            {
                ImageInfoText.Text = $"Изображение: {System.IO.Path.GetFileName(ImagePath)}";
            }
            else
            {
                ImageInfoText.Text = "Изображение не прикреплено";
            }
        }

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
                UpdateImageInfo();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageText = MessageTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
