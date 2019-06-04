namespace AskMe.Utility
{
    public static class Constants
    {
        public static readonly string BingSpeechAuthenticationURL = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
        public static readonly string BingSpeechApiKey = "5c0181a199a642a2b1f73f17a16c2a26";
        public static readonly string BingSpeechRecognitionEndpoint = "https://speech.platform.bing.com/speech/recognition/";
        public static readonly string AudioContentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";

        public static readonly string CRISApiKey = "9f4a8af470a2450ab82cf2b35745d76c";
        public static readonly string CRISEndPoint = "https://westus.stt.speech.microsoft.com/ws/cris/speech/recognize?cid=af488b61-0ea2-41b1-bc15-491321e16825";
        public static readonly string CRISAuthenticationURL = "https://westus.api.cognitive.microsoft.com/sts/v1.0/issueToken";
        public static readonly string CRISDictationEndPoint = "https://westus.stt.speech.microsoft.com/ws/cris/speech/recognize/continuous?cid=af488b61-0ea2-41b1-bc15-491321e16825";

        public static readonly string CRISPSEndPoint = "https://westus.stt.speech.microsoft.com/ws/cris/speech/recognize/continuous?cid=7f8083c7-106e-4a3f-a492-7da40f66ce12";
        public static readonly string CRISPSEndPoint_IN = "https://westus.stt.speech.microsoft.com/ws/cris/speech/recognize/continuous?cid=ca24f4bd-c39c-47ab-b3f4-feb2546ec322";
        public static readonly string CRISPSKey = "f96f47f62495424d8229713429609235";

        public static readonly string CRISTTSEndPoint = "https://westus.tts.speech.microsoft.com/cognitiveservices/v1";
        public static readonly string VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)";
        public static readonly string CRISTTSKey = "5a3673eb87454ef79dc37a017d906a8a";



        public static readonly string EmailAPI = "http://52.191.213.173:3200/mail";

        public const string DefaultLocale = "en-US";
        public const string IndianLocale = "en-IN";

        public static readonly string BingSpellCheckApiKey = "f536faa8abab4fd68b13b0c6fd343e20";
        public static readonly string BingSpellCheckEndpoint = "https://api.cognitive.microsoft.com/bing/v7.0/SpellCheck/";

        public static readonly string AudioFilename = "query.wav";

        public static readonly string DirectLineSecret = "NkekgtUt380.cwA.U84.ecEXe039uD1PrEGwN8I2xIVk_GRhNrlWr_DuoTeAaAk";
        public static readonly string BotEndPoint = "https://ilink-innolabs.com/sasa/api/messages";
        public static readonly string BotId = "sasa";

        public static readonly string AdaptiveCardType = "application/vnd.microsoft.card.adaptive";
        public static readonly string HeroCardType = "application/vnd.microsoft.card.hero";

        public static readonly string ErrorString = "Check your internet connection and try again!!!";

        public static string Username = "User";
        public static string Email = string.Empty;
    }
}
