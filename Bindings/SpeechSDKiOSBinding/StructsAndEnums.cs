using System;
using ObjCRuntime;
using Foundation;
using UIKit;

namespace SpeechSDKiOSBinding
{
    using System;
    using ObjCRuntime;

    [Native]
    public enum SpeechRecognitionMode : ulong
    {
        ShortPhrase,
        LongDictation
    }

    [Native]
    public enum SpeechLoggingLevel : long
    {
        Disabled = 0,
        FullAudio = 1,
        Limited = 2,
        Obfuscated = 3
    }

    [Native]
    public enum RecognitionStatus : long
    {
        None = 0,
        Intermediate = 100,
        RecognitionSuccess = 200,
        NoMatch = 301,
        InitialSilenceTimeout = 303,
        BabbleTimeout = 304,
        HotWordMaximumTime = 305,
        Cancelled = 201,
        RecognitionError = 500,
        DictationEndSilenceTimeout = 610,
        EndOfDictation = 612
    }

    [Native]
    public enum Confidence : long
    {
        None = -2,
        Low = -1,
        Normal = 0,
        High = 1
    }

    public enum SpeechClientStatus
    {
        SecurityFailed = (int)-1910505471,
        LoginFailed = (int)-1910505470,
        Timeout = (int)-1910505469,
        ConnectionFailed = (int)-1910505468,
        NameNotFound = (int)-1910505467,
        InvalidService = (int)-1910505466,
        InvalidProxy = (int)-1910505465,
        BadResponse = (int)-1910505464,
        InternalError = (int)-1910505463,
        AuthenticationError = (int)-1910505462,
        AuthenticationExpired = (int)-1910505461,
        LimitsExceeded = (int)-1910505460,
        AudioOutputFailed = (int)-1910439935,
        MicrophoneInUse = (int)-1910439933,
        MicrophoneUnavailable = (int)-1910439934,
        MicrophoneStatusUnknown = (int)-1910439932,
        InvalidArgument = (int)-2147024809
    }

    [Native]
    public enum AudioCompressionType : ulong
    {
        Pcm = 1,
        Siren7 = 654
    }
}