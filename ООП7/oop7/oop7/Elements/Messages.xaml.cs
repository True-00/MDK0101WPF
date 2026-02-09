using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace oop7.Elements
{
    public partial class Messages : UserControl
    {
        private oop7.Models.Messages _message;
        private Action<int> _editCallback;
        private Action<int> _deleteCallback;

        public Messages()
        {
            InitializeComponent();
        }

        public void Initialize(oop7.Models.Messages message, bool showEditControls = false,
                              Action<int> editCallback = null, Action<int> deleteCallback = null)
        {
            _message = message;
            _editCallback = editCallback;
            _deleteCallback = deleteCallback;

            MessageHeader.Text = $"{message.Date:dd.MM.yyyy HH:mm}";
            MessageText.Text = message.Message;

            // Загружаем изображение
            if (!string.IsNullOrEmpty(message.ImagePath))
            {
                try
                {
                    var uri = new Uri(message.ImagePath, UriKind.RelativeOrAbsolute);
                    MessageImage.Source = new BitmapImage(uri);
                    MessageImage.Visibility = Visibility.Visible;
                }
                catch
                {
                    MessageImage.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageImage.Visibility = Visibility.Collapsed;
            }

            EditPanel.Visibility = showEditControls ? Visibility.Visible : Visibility.Collapsed;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _editCallback?.Invoke(_message.Id);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _deleteCallback?.Invoke(_message.Id);
        }
        private void LoadMessageImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                MessageImage.Visibility = Visibility.Collapsed;
                return;
            }

            try
            {
                // Пробуем загрузить как ресурс
                var uri = new Uri(imagePath, UriKind.Relative);
                MessageImage.Source = new BitmapImage(uri);
                MessageImage.Visibility = Visibility.Visible;
            }
            catch
            {
                try
                {
                    // Пробуем абсолютный путь
                    string fullPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        imagePath);

                    if (System.IO.File.Exists(fullPath))
                    {
                        var uri = new Uri(fullPath, UriKind.Absolute);
                        MessageImage.Source = new BitmapImage(uri);
                        MessageImage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageImage.Visibility = Visibility.Collapsed;
                    }
                }
                catch
                {
                    MessageImage.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}