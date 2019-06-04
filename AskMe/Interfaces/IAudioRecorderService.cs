using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Interfaces
{
    public interface IAudioRecorderService
    {
        void StartRecording();
        void StopRecording();
    }
}
