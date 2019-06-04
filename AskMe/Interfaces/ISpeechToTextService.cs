using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Interfaces
{
    public interface ISpeechToTextService
    {
        Task StartRecording();

        void StopRecording();

        Task Initialize();
    }
}
