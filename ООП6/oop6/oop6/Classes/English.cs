using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace oop6.Classes
{
    internal class English : Human
    {
        private List<Phrase> phrases;
        private int currentIndex = 0;

        public English(string name, string img) : base(name, img)
        {
            phrases = new List<Phrase>
            {
                new Phrase("Hello", "Voices/helloEN.mp3"),        // Тот же файл, но английская фраза
                new Phrase("How are you?", "Voices/HowAreYouEN.mp3"),
                new Phrase("Have you seen Avatar 3?", "Voices/avatarEN.mp3")
            };
        }
        public override void Speak(Label phraseLabel)
        {
            if (phrases == null || phrases.Count == 0) return;

            var phrase = phrases[currentIndex];
            phraseLabel.Content = phrase.Text;

            try
            {
                string fullPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    phrase.AudioPath);

                var player = new MediaPlayer();
                player.Open(new Uri(fullPath, UriKind.Absolute));
                player.Play();

                phraseLabel.Content = phrase.Text + " 🔊";
            }
            catch (Exception ex)
            {
                phraseLabel.Content = phrase.Text + " (audio error)";
            }

            currentIndex = (currentIndex + 1) % phrases.Count;
        }
    }
}
