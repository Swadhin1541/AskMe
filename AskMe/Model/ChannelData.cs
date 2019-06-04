using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AskMe.Model
{
    public partial class ChannelData
    {
        [JsonProperty("attachment")]
        public ChannelAttachment Attachment { get; set; }
    }

    public partial class ChannelAttachment
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }

    public partial class Payload
    {
        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("chartData")]
        public ChartData ChartData { get; set; }
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

        [JsonProperty("responseTextArray")]
        public List<string> ResponseTextArray { get; set; }

        [JsonProperty("responseData")]
        public List<ResponseDatum> ResponseData { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
    }

    public partial class ResponseDatum
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("sum")]
        public long Sum { get; set; }

        [JsonProperty("percentage")]
        public double Percentage { get; set; }
    }

    public partial class ChannelData
    {
        public static ChannelData FromJson(string json) => JsonConvert.DeserializeObject<ChannelData>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ChannelData self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
