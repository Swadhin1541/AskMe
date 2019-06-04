// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var emailChannelData = EmailChannelData.FromJson(jsonString);

namespace AskMe.Model.Email
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EmailChannelData
    {
        [JsonProperty("attachment")]
        public EmailAttachment Attachment { get; set; }
    }

    public partial class EmailAttachment
    {
        [JsonProperty("payload")]
        public Payload Payload { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Payload
    {
        [JsonProperty("eMailData")]
        public EMailData EMailData { get; set; }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }
    }

    public partial class EMailData
    {
        [JsonProperty("chartData")]
        public ChartData ChartData { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("receipients")]
        public List<string> Receipients { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }

    public partial class ChartData
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("dataExist")]
        public bool DataExist { get; set; }

        [JsonProperty("responseData")]
        public List<ResponseDatum> ResponseData { get; set; }

        [JsonProperty("responseTextArray")]
        public List<string> ResponseTextArray { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
    }

    public partial class ResponseDatum
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("percentage")]
        public double Percentage { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class EmailChannelData
    {
        public static EmailChannelData FromJson(string json) => JsonConvert.DeserializeObject<EmailChannelData>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this EmailChannelData self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
