using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace oop6.Classes
{
    public class Russian : Human
    {
        private List<Phrase> phrases;
        private int currentIndex = 0;

        public Russian(string name, string img) : base(name, img)
        {
            phrases = new List<Phrase>
            {
                new Phrase("Привет", "Voices/hello.mp3"),       
                new Phrase("Как дела?", "Voices/HowAreYou.mp3"),
                new Phrase("Ты уже смотрел Аватар 3?", "Voices/avatar.mp3")
            };
        }
        public override void Speak(Label phraseLabel)
        {
            if (phrases == null || phrases.Count == 0) return;

            var phrase = phrases[currentIndex];
            phraseLabel.Content = phrase.Text;

            try
            {
                // Создаем полный путь к файлу
                string fullPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    phrase.AudioPath);

                var player = new MediaPlayer();
                player.Open(new Uri(fullPath, UriKind.Absolute));
                player.Play();

                // Отображаем, что звук играет
                phraseLabel.Content = phrase.Text + " 🔊";
            }
            catch (Exception ex)
            {
                phraseLabel.Content = phrase.Text + " (ошибка звука)";
            }

            // Переходим к следующей фразе
            currentIndex = (currentIndex + 1) % phrases.Count;
        }
    }
}
