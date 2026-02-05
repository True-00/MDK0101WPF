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
using System.Windows.Navigation;
using System.Windows.Shapes;
using oop6.Classes;

namespace oop6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Russian russian;
        private English english;
        public MainWindow()
        {
            InitializeComponent();
            InitializeSpeakers();
        }
        private void InitializeSpeakers()
        {
            // Создаем спикеров с правильными путями к изображениям
            russian = new Russian("Александр", "Images/russian.png");
            english = new English("John", "Images/american.png");  // Используем american.png для англичанина

            // Загружаем изображения
            LoadImages();
        }
        private void LoadImages()
        {
            try
            {
                // Загружаем изображение для русского
                RussianImage.Source = new BitmapImage(
                    new Uri("Images/russian.png", UriKind.Relative));
            }
            catch
            {
                RussianImage.Source = CreateFallbackImage("РУ");
            }

            try
            {
                // Загружаем изображение для англичанина (american.png)
                EnglishImage.Source = new BitmapImage(
                    new Uri("Images/american.png", UriKind.Relative));
            }
            catch
            {
                EnglishImage.Source = CreateFallbackImage("EN");
            }
        }

        private BitmapImage CreateFallbackImage(string text)
        {
            // Создаем простое изображение-заглушку
            var visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                // Просто возвращаем null - изображение не будет отображаться
            }

            return null;
        }

        private void RussianBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            russian?.Speak(CurrentPhraseLabel);
        }

        private void EnglishBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            english?.Speak(CurrentPhraseLabel);
        }
    }
}
