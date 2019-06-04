using AskMe.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AskMe.Interfaces
{
    public interface ITextToSpeechService
    {
        void Speak(string text, Message message);

        void Speak(Stream stream);

        void StopSpeaking();

        void Initialize();
    }
}
