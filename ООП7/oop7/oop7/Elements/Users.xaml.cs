using oop7.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace oop7.Elements
{
    public partial class Users : UserControl
    {
        public oop7.Models.Users UserData { get; private set; }

        public Users()
        {
            InitializeComponent();
        }

        public void Initialize(oop7.Models.Users user)
        {
            UserData = user;

            UserNameText.Text = user.FullName;
            UserPhoneText.Text = user.Phone;

            LoadUserImage(user.Img);
        }

        private void LoadUserImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                UserImage.Source = null;
                return;
            }

            try
            {
                // Сначала пробуем загрузить как ресурс
                var uri = new Uri(imagePath, UriKind.Relative);
                UserImage.Source = new BitmapImage(uri);
            }
            catch
            {
                try
                {
                    // Если не получилось, пробуем найти файл
                    string fullPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        imagePath.TrimStart('/'));

                    if (System.IO.File.Exists(fullPath))
                    {
                        var uri = new Uri(fullPath, UriKind.Absolute);
                        UserImage.Source = new BitmapImage(uri);
                    }
                    else
                    {
                        // Если файл не найден, создаем цветной круг
                        CreateFallbackImage();
                    }
                }
                catch
                {
                    CreateFallbackImage();
                }
            }
        }

        private void CreateFallbackImage()
        {
            // Создаем простой цветной круг
            var drawingVisual = new System.Windows.Media.DrawingVisual();
            using (var context = drawingVisual.RenderOpen())
            {
                var color = System.Windows.Media.Color.FromRgb(52, 152, 219);
                var brush = new System.Windows.Media.SolidColorBrush(color);
                context.DrawEllipse(brush, null, new System.Windows.Point(20, 20), 20, 20);
            }

            var bitmap = new RenderTargetBitmap(40, 40, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingVisual);
            UserImage.Source = bitmap;
        }

        public void SetSelected(bool isSelected)
        {
            SelectionIndicator.Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
        }

        public event RoutedEventHandler UserClicked;

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserClicked?.Invoke(this, e);
        }
    }
}