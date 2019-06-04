using System;
using ObjCRuntime;
using Foundation;
using UIKit;

namespace SpeechSDKiOSBinding
{
    using System;
    using Foundation;
    using ObjCRuntime;
    using Speech;

    // @interface Preferences : NSObject
    [BaseType(typeof(NSObject))]
    public interface Preferences
    {
        // @property (copy, nonatomic) NSString * Locale;
        [Export("Locale")]
        string Locale { get; set; }

        // @property (copy, nonatomic) NSString * ServiceUri;
        [Export("ServiceUri")]
        string ServiceUri { get; set; }

        // @property (nonatomic, strong) NSNumber * MicrophoneTimeout;
        [Export("MicrophoneTimeout", ArgumentSemantic.Strong)]
        NSNumber MicrophoneTimeout { get; set; }

        // @property (nonatomic, strong) NSNumber * LoggingLevel;
        [Export("LoggingLevel", ArgumentSemantic.Strong)]
        NSNumber LoggingLevel { get; set; }

        // @property (copy, nonatomic) NSString * AuthenticationUri;
        [Export("AuthenticationUri")]
        string AuthenticationUri { get; set; }
    }

    // @interface AdmRecoOnlyPreferences : Preferences
    [BaseType(typeof(Preferences))]
    public interface AdmRecoOnlyPreferences
    {
        // @property (copy, nonatomic) NSString * ClientId;
        [Export("ClientId")]
        string ClientId { get; set; }

        // @property (copy, nonatomic) NSString * ClientSecret;
        [Export("ClientSecret")]
        string ClientSecret { get; set; }

        // @property (copy, nonatomic) NSString * LuisAppId;
        [Export("LuisAppId")]
        string LuisAppId { get; set; }

        // @property (copy, nonatomic) NSString * LuisSubscriptionId;
        [Export("LuisSubscriptionId")]
        string LuisSubscriptionId { get; set; }
    }

    // @interface RecognizedPhrase : NSObject
    [BaseType(typeof(NSObject))]
    public interface RecognizedPhrase
    {
        // @property (nonatomic, strong) NSString * LexicalForm;
        [Export("LexicalForm", ArgumentSemantic.Strong)]
        string LexicalForm { get; set; }

        // @property (nonatomic, strong) NSString * DisplayText;
        [Export("DisplayText", ArgumentSemantic.Strong)]
        string DisplayText { get; set; }

        // @property (nonatomic, strong) NSString * InverseTextNormalizationResult;
        [Export("InverseTextNormalizationResult", ArgumentSemantic.Strong)]
        string InverseTextNormalizationResult { get; set; }

        // @property (nonatomic, strong) NSString * MaskedInverseTextNormalizationResult;
        [Export("MaskedInverseTextNormalizationResult", ArgumentSemantic.Strong)]
        string MaskedInverseTextNormalizationResult { get; set; }

        // @property (assign, nonatomic) Confidence Confidence;
        [Export("Confidence", ArgumentSemantic.Assign)]
        Confidence Confidence { get; set; }
    }

    // @interface RecognitionResult : NSObject
    [BaseType(typeof(NSObject))]
    public interface RecognitionResult
    {
        // @property (assign, nonatomic) RecognitionStatus RecognitionStatus;
        [Export("RecognitionStatus", ArgumentSemantic.Assign)]
        RecognitionStatus RecognitionStatus { get; set; }

        // @property (nonatomic, strong) NSArray * RecognizedPhrase;
        [Export("RecognizedPhrase", ArgumentSemantic.Strong)]
        //[Verify(StronglyTypedNSArray)]
        NSObject[] RecognizedPhrase { get; set; }
    }

    // @interface IntentResult : NSObject
    [BaseType(typeof(NSObject))]
    public interface IntentResult
    {
        // @property (nonatomic, strong) NSURL * Url;
        [Export("Url", ArgumentSemantic.Strong)]
        NSUrl Url { get; set; }

        // @property (nonatomic, strong) NSDictionary * Headers;
        [Export("Headers", ArgumentSemantic.Strong)]
        NSDictionary Headers { get; set; }

        // @property (nonatomic, strong) NSString * Body;
        [Export("Body", ArgumentSemantic.Strong)]
        string Body { get; set; }
    }

    // @protocol SpeechRecognitionProtocol
    [BaseType(typeof(NSObject))]
    [Model]
    public interface SpeechRecognitionProtocol
    {
        // @required -(void)onPartialResponseReceived:(NSString *)partialResult;
        [Abstract]
        [Export("onPartialResponseReceived:")]
        void OnPartialResponseReceived(string partialResult);

        // @required -(void)onFinalResponseReceived:(RecognitionResult *)result;
        [Abstract]
        [Export("onFinalResponseReceived:")]
        void OnFinalResponseReceived(RecognitionResult result);

        // @required -(void)onError:(NSString *)errorMessage withErrorCode:(int)errorCode;
        [Abstract]
        [Export("onError:withErrorCode:")]
        void OnError(string errorMessage, int errorCode);

        // @required -(void)onMicrophoneStatus:(Boolean)recording;
        [Abstract]
        [Export("onMicrophoneStatus:")]
        void OnMicrophoneStatus(byte recording);

        // @optional -(void)onSpeakerStatus:(Boolean)speaking;
        [Export("onSpeakerStatus:")]
        void OnSpeakerStatus(byte speaking);

        // @optional -(void)onIntentReceived:(IntentResult *)intent;
        [Export("onIntentReceived:")]
        void OnIntentReceived(IntentResult intent);
    }

    // @interface SpeechAudioFormat : NSObject
    [BaseType(typeof(NSObject))]
    public interface SpeechAudioFormat
    {
        // @property (assign, nonatomic) int AverageBytesPerSecond;
        [Export("AverageBytesPerSecond")]
        int AverageBytesPerSecond { get; set; }

        // @property (assign, nonatomic) short BitsPerSample;
        [Export("BitsPerSample")]
        short BitsPerSample { get; set; }

        // @property (assign, nonatomic) short BlockAlign;
        [Export("BlockAlign")]
        short BlockAlign { get; set; }

        // @property (assign, nonatomic) short ChannelCount;
        [Export("ChannelCount")]
        short ChannelCount { get; set; }

        // @property (assign, nonatomic) AudioCompressionType EncodingFormat;
        [Export("EncodingFormat", ArgumentSemantic.Assign)]
        AudioCompressionType EncodingFormat { get; set; }

        // @property (nonatomic, strong) NSData * FormatSpecificData;
        [Export("FormatSpecificData", ArgumentSemantic.Strong)]
        NSData FormatSpecificData { get; set; }

        // @property (assign, nonatomic) int SamplesPerSecond;
        [Export("SamplesPerSecond")]
        int SamplesPerSecond { get; set; }

        // +(SpeechAudioFormat *)createSiren7Format:(int)sampleRate;
        [Static]
        [Export("createSiren7Format:")]
        SpeechAudioFormat CreateSiren7Format(int sampleRate);

        // +(SpeechAudioFormat *)create16BitPCMFormat:(int)sampleRate;
        [Static]
        [Export("create16BitPCMFormat:")]
        SpeechAudioFormat Create16BitPCMFormat(int sampleRate);
    }

    // @interface Conversation : NSObject <SpeechRecognitionProtocol>
    [BaseType(typeof(NSObject))]
    public interface Conversation : SpeechRecognitionProtocol
    {
        // -(void)initWithPrefs:(Preferences *)prefs withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Export("initWithPrefs:withProtocol:")]
        void InitWithPrefs(Preferences prefs, SpeechRecognitionProtocol @delegate);

        // -(void)createConversation;
        [Export("createConversation")]
        void CreateConversation();

        // -(OSStatus)audioStart;
        [Export("audioStart")]
        //[Verify(MethodToProperty)]
        int AudioStart { get; }

        // -(void)audioStop;
        [Export("audioStop")]
        void AudioStop();

        // -(void)sendText:(NSString *)textQuery;
        [Export("sendText:")]
        void SendText(string textQuery);

        // -(void)synthesizeText:(NSString *)text;
        [Export("synthesizeText:")]
        void SynthesizeText(string text);

        // -(void)synthesizeText:(NSString *)input withMimeType:(NSString *)mimeType;
        [Export("synthesizeText:withMimeType:")]
        void SynthesizeText(string input, string mimeType);

        // -(void)playWaveFile:(NSString *)fileName;
        [Export("playWaveFile:")]
        void PlayWaveFile(string fileName);

        // @property (copy, nonatomic) NSString * AuthenticationUri;
        [Export("AuthenticationUri")]
        string AuthenticationUri { get; set; }

        // -(void)setLiveIdToken:(NSString *)token;
        [Export("setLiveIdToken:")]
        void SetLiveIdToken(string token);

        // -(void)setLocationLatitude:(double)latitude withLongitude:(double)longitude;
        [Export("setLocationLatitude:withLongitude:")]
        void SetLocationLatitude(double latitude, double longitude);

        // -(void)reset;
        [Export("reset")]
        void Reset();
    }

    // @interface DataRecognitionClient : Conversation
    [BaseType(typeof(Conversation))]
    public interface DataRecognitionClient
    {
        // -(id)initWithSpeechRecoParams:(SpeechRecognitionMode)speechRecognitionMode withPrefs:(AdmRecoOnlyPreferences *)prefs withIntent:(_Bool)wantIntent withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Export("initWithSpeechRecoParams:withPrefs:withIntent:withProtocol:")]
        IntPtr Constructor(SpeechRecognitionMode speechRecognitionMode, AdmRecoOnlyPreferences prefs, bool wantIntent, SpeechRecognitionProtocol @delegate);

        // -(void)sendAudioFormat:(SpeechAudioFormat *)audioFormat;
        [Export("sendAudioFormat:")]
        void SendAudioFormat(SpeechAudioFormat audioFormat);

        // -(void)sendAudio:(NSData *)buffer withLength:(int)actualAudioBytesInBuffer;
        [Export("sendAudio:withLength:")]
        void SendAudio(NSData buffer, int actualAudioBytesInBuffer);

        // -(void)endAudio;
        [Export("endAudio")]
        void EndAudio();

        // -(_Bool)waitForFinalResponse:(int)timeoutInSeconds;
        [Export("waitForFinalResponse:")]
        bool WaitForFinalResponse(int timeoutInSeconds);
    }

    // @interface DataRecognitionClientWithIntent : DataRecognitionClient
    [BaseType(typeof(DataRecognitionClient))]
    public interface DataRecognitionClientWithIntent
    {
        // -(id)initWithSpeechRecoParams:(AdmRecoOnlyPreferences *)prefs withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Export("initWithSpeechRecoParams:withProtocol:")]
        IntPtr Constructor(AdmRecoOnlyPreferences prefs, SpeechRecognitionProtocol @delegate);
    }

    // @interface MicrophoneRecognitionClient : Conversation
    [BaseType(typeof(Conversation))]
    public interface MicrophoneRecognitionClient
    {
        // -(id)initWithSpeechRecoParams:(SpeechRecognitionMode)speechRecognitionMode withPrefs:(AdmRecoOnlyPreferences *)prefs withIntent:(_Bool)wantIntent withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Export("initWithSpeechRecoParams:withPrefs:withIntent:withProtocol:")]
        IntPtr Constructor(SpeechRecognitionMode speechRecognitionMode, AdmRecoOnlyPreferences prefs, bool wantIntent, SpeechRecognitionProtocol @delegate);

        // -(OSStatus)startMicAndRecognition;
        [Export("startMicAndRecognition")]
        //[Verify(MethodToProperty)]
        int StartMicAndRecognition { get; }

        // -(void)endMicAndRecognition;
        [Export("endMicAndRecognition")]
        void EndMicAndRecognition();

        // -(_Bool)waitForFinalResponse:(int)timeoutInSeconds;
        [Export("waitForFinalResponse:")]
        bool WaitForFinalResponse(int timeoutInSeconds);
    }

    // @interface MicrophoneRecognitionClientWithIntent : MicrophoneRecognitionClient
    [BaseType(typeof(MicrophoneRecognitionClient))]
    public interface MicrophoneRecognitionClientWithIntent
    {
        // -(id)initWithSpeechRecoParams:(AdmRecoOnlyPreferences *)prefs withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Export("initWithSpeechRecoParams:withProtocol:")]
        IntPtr Constructor(AdmRecoOnlyPreferences prefs, SpeechRecognitionProtocol @delegate);
    }

    // @interface SpeechRecognitionServiceFactory : NSObject
    [BaseType(typeof(NSObject))]
    public interface SpeechRecognitionServiceFactory
    {
        // +(NSString *)getAPIVersion;
        [Static]
        [Export("getAPIVersion")]
        //[Verify(MethodToProperty)]
        string APIVersion { get; }

        // +(DataRecognitionClient *)createDataClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Static]
        [Export("createDataClient:withLanguage:withKey:withProtocol:")]
        DataRecognitionClient CreateDataClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryOrSecondaryKey, SpeechRecognitionProtocol @delegate);

        // +(DataRecognitionClient *)createDataClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate __attribute__((deprecated("Use alternative method using just the primary key.")));
        [Static]
        [Export("createDataClient:withLanguage:withPrimaryKey:withSecondaryKey:withProtocol:")]
        DataRecognitionClient CreateDataClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryKey, string secondaryKey, SpeechRecognitionProtocol @delegate);

        // +(DataRecognitionClient *)createDataClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createDataClient:withLanguage:withKey:withProtocol:withUrl:")]
        DataRecognitionClient CreateDataClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryOrSecondaryKey, SpeechRecognitionProtocol @delegate, string url);

        // +(DataRecognitionClient *)createDataClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createDataClient:withLanguage:withPrimaryKey:withSecondaryKey:withProtocol:withUrl:")]
        DataRecognitionClient CreateDataClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryKey, string secondaryKey, SpeechRecognitionProtocol @delegate, string url);

        // +(DataRecognitionClientWithIntent *)createDataClientWithIntent:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Static]
        [Export("createDataClientWithIntent:withKey:withLUISAppID:withLUISSecret:withProtocol:")]
        DataRecognitionClientWithIntent CreateDataClientWithIntent(string language, string primaryOrSecondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate);

        // +(DataRecognitionClientWithIntent *)createDataClientWithIntent:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate __attribute__((deprecated("Use alternative method using just the primary key.")));
        [Static]
        [Export("createDataClientWithIntent:withPrimaryKey:withSecondaryKey:withLUISAppID:withLUISSecret:withProtocol:")]
        DataRecognitionClientWithIntent CreateDataClientWithIntent(string language, string primaryKey, string secondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate);

        // +(DataRecognitionClientWithIntent *)createDataClientWithIntent:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createDataClientWithIntent:withKey:withLUISAppID:withLUISSecret:withProtocol:withUrl:")]
        DataRecognitionClientWithIntent CreateDataClientWithIntent(string language, string primaryOrSecondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate, string url);

        // +(DataRecognitionClientWithIntent *)createDataClientWithIntent:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createDataClientWithIntent:withPrimaryKey:withSecondaryKey:withLUISAppID:withLUISSecret:withProtocol:withUrl:")]
        DataRecognitionClientWithIntent CreateDataClientWithIntent(string language, string primaryKey, string secondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate, string url);

        // +(MicrophoneRecognitionClient *)createMicrophoneClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Static]
        [Export("createMicrophoneClient:withLanguage:withKey:withProtocol:")]
        MicrophoneRecognitionClient CreateMicrophoneClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryOrSecondaryKey, SpeechRecognitionProtocol @delegate);

        // +(MicrophoneRecognitionClient *)createMicrophoneClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate __attribute__((deprecated("Use alternative method using just the primary key.")));
        [Static]
        [Export("createMicrophoneClient:withLanguage:withPrimaryKey:withSecondaryKey:withProtocol:")]
        MicrophoneRecognitionClient CreateMicrophoneClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryKey, string secondaryKey, SpeechRecognitionProtocol @delegate);

        // +(MicrophoneRecognitionClient *)createMicrophoneClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createMicrophoneClient:withLanguage:withKey:withProtocol:withUrl:")]
        MicrophoneRecognitionClient CreateMicrophoneClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryOrSecondaryKey, SpeechRecognitionProtocol @delegate, string url);

        // +(MicrophoneRecognitionClient *)createMicrophoneClient:(SpeechRecognitionMode)speechRecognitionMode withLanguage:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createMicrophoneClient:withLanguage:withPrimaryKey:withSecondaryKey:withProtocol:withUrl:")]
        MicrophoneRecognitionClient CreateMicrophoneClient(SpeechRecognitionMode speechRecognitionMode, string language, string primaryKey, string secondaryKey, SpeechRecognitionProtocol @delegate, string url);

        // +(MicrophoneRecognitionClientWithIntent *)createMicrophoneClientWithIntent:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate;
        [Static]
        [Export("createMicrophoneClientWithIntent:withKey:withLUISAppID:withLUISSecret:withProtocol:")]
        MicrophoneRecognitionClientWithIntent CreateMicrophoneClientWithIntent(string language, string primaryOrSecondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate);

        // +(MicrophoneRecognitionClientWithIntent *)createMicrophoneClientWithIntent:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate __attribute__((deprecated("Use alternative method using just the primary key.")));
        [Static]
        [Export("createMicrophoneClientWithIntent:withPrimaryKey:withSecondaryKey:withLUISAppID:withLUISSecret:withProtocol:")]
        MicrophoneRecognitionClientWithIntent CreateMicrophoneClientWithIntent(string language, string primaryKey, string secondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate);

        // +(MicrophoneRecognitionClientWithIntent *)createMicrophoneClientWithIntent:(NSString *)language withKey:(NSString *)primaryOrSecondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createMicrophoneClientWithIntent:withKey:withLUISAppID:withLUISSecret:withProtocol:withUrl:")]
        MicrophoneRecognitionClientWithIntent CreateMicrophoneClientWithIntent(string language, string primaryOrSecondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate, string url);

        // +(MicrophoneRecognitionClientWithIntent *)createMicrophoneClientWithIntent:(NSString *)language withPrimaryKey:(NSString *)primaryKey withSecondaryKey:(NSString *)secondaryKey withLUISAppID:(NSString *)luisAppID withLUISSecret:(NSString *)luisSubscriptionID withProtocol:(id<SpeechRecognitionProtocol>)delegate withUrl:(NSString *)url;
        [Static]
        [Export("createMicrophoneClientWithIntent:withPrimaryKey:withSecondaryKey:withLUISAppID:withLUISSecret:withProtocol:withUrl:")]
        MicrophoneRecognitionClientWithIntent CreateMicrophoneClientWithIntent(string language, string primaryKey, string secondaryKey, string luisAppID, string luisSubscriptionID, SpeechRecognitionProtocol @delegate, string url);
    }

}

