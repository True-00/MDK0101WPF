using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using oop7.Models;
using oop7.Classes;
using oop7.Interfaces;
using oop7.Elements;

// Используем псевдонимы для устранения неоднозначности
using ModelUser = oop7.Models.Users;
using ModelMessage = oop7.Models.Messages;
using ElementUser = oop7.Elements.Users;
using ElementMessage = oop7.Elements.Messages;

namespace oop7
{
    public partial class MainWindow : Window
    {
        private IUsers usersContext;
        private IMessages messagesContext;

        private ModelUser selectedUser;
        private List<ModelUser> allUsers;

        private OpenFileDialog imageDialog;
        private string selectedImagePath;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            LoadUsers();
        }

        private void InitializeData()
        {
            usersContext = new UsersContext();
            messagesContext = new MessagesContext();

            imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
        }

        private void LoadUsers()
        {
            UsersPanel.Children.Clear();

            allUsers = usersContext.GetAllUsers();

            foreach (var user in allUsers)
            {
                var userControl = new ElementUser();
                userControl.Initialize(user);
                userControl.Tag = user; // Сохраняем пользователя в Tag
                userControl.UserClicked += UserControl_UserClicked;
                UsersPanel.Children.Add(userControl);
            }
        }

        private void UserControl_UserClicked(object sender, RoutedEventArgs e)
        {
            var clickedUserControl = sender as ElementUser;
            if (clickedUserControl == null) return;

            // Получаем пользователя из Tag
            selectedUser = clickedUserControl.Tag as ModelUser;

            if (selectedUser == null) return;

            // Снимаем выделение со всех
            foreach (var control in UsersPanel.Children)
            {
                if (control is ElementUser userCtrl)
                {
                    userCtrl.SetSelected(false);
                }
            }

            // Выделяем выбранного
            clickedUserControl.SetSelected(true);

            // Обновляем интерфейс
            UpdateChatInterface();
        }

        private void UpdateChatInterface()
        {
            if (selectedUser == null)
            {
                ChatTitle.Text = "Выберите пользователя";
                MessageTextBox.IsEnabled = false;
                SendButton.IsEnabled = false;
                AddImageButton.IsEnabled = false;
                MessagesPanel.Children.Clear();
                return;
            }

            ChatTitle.Text = $"Чат с {selectedUser.FullName}";
            MessageTextBox.IsEnabled = true;
            SendButton.IsEnabled = true;
            AddImageButton.IsEnabled = true;
            MessageTextBox.Focus();
            LoadMessages();
        }

        private void LoadMessages()
        {
            MessagesPanel.Children.Clear();

            if (selectedUser == null) return;

            var userMessages = messagesContext.GetMessagesByUserId(selectedUser.Id);

            foreach (var message in userMessages)
            {
                var messageControl = new ElementMessage();
                // Передаем методы как делегаты
                messageControl.Initialize(message, true, EditMessageCallback, DeleteMessageCallback);
                MessagesPanel.Children.Add(messageControl);
            }

            // Прокрутка вниз
            var scrollViewer = GetScrollViewer(MessagesPanel);
            scrollViewer?.ScrollToEnd();
        }

        private ScrollViewer GetScrollViewer(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is ScrollViewer viewer)
                    return viewer;

                var childViewer = GetScrollViewer(child);
                if (childViewer != null)
                    return childViewer;
            }
            return null;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null || string.IsNullOrWhiteSpace(MessageTextBox.Text))
                return;

            string finalImagePath = null;

            // Обработка изображения
            if (!string.IsNullOrEmpty(selectedImagePath) && System.IO.File.Exists(selectedImagePath))
            {
                try
                {
                    // Создаем папку для изображений чата
                    string chatImagesDir = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "ChatImages");

                    if (!System.IO.Directory.Exists(chatImagesDir))
                        System.IO.Directory.CreateDirectory(chatImagesDir);

                    // Копируем файл
                    string fileName = $"msg_{DateTime.Now:yyyyMMdd_HHmmss}_{System.IO.Path.GetFileName(selectedImagePath)}";
                    string destPath = System.IO.Path.Combine(chatImagesDir, fileName);
                    System.IO.File.Copy(selectedImagePath, destPath, true);

                    // Сохраняем относительный путь
                    finalImagePath = $"ChatImages/{fileName}";

                    MessageBox.Show($"Изображение сохранено: {fileName}", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            var newMessage = new ModelMessage(
                0,
                selectedUser.Id,
                MessageTextBox.Text.Trim(),
                DateTime.Now,
                finalImagePath
            );

            messagesContext.AddMessage(newMessage);
            MessageTextBox.Clear();
            selectedImagePath = null;
            LoadMessages();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null) return;

            if (imageDialog.ShowDialog() == true)
            {
                selectedImagePath = imageDialog.FileName;
                MessageBox.Show($"Изображение выбрано: {System.IO.Path.GetFileName(selectedImagePath)}",
                              "Изображение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Переименуем методы, чтобы избежать конфликта
        private void EditMessageCallback(int messageId)
        {
            var message = messagesContext.GetMessageById(messageId);
            if (message == null) return;

            // Используйте MessageBox или создайте простой TextBox
            var editDialog = new Window
            {
                Title = "Редактирование сообщения",
                Width = 300,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            var textBox = new TextBox
            {
                Text = message.Message,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true
            };

            var button = new Button
            {
                Content = "Сохранить",
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Bottom
            };

            button.Click += (s, e) =>
            {
                message.Message = textBox.Text;
                messagesContext.UpdateMessage(message);
                LoadMessages();
                editDialog.DialogResult = true;
            };

            var stackPanel = new StackPanel();
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(button);

            editDialog.Content = stackPanel;
            editDialog.ShowDialog();
        }

        private void DeleteMessageCallback(int messageId)
        {
            var result = MessageBox.Show("Удалить это сообщение?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                messagesContext.DeleteMessage(messageId);
                LoadMessages();
            }
        }
    }
}