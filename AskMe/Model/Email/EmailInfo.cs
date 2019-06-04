// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var emailInfo = EmailInfo.FromJson(jsonString);

namespace AskMe.Model.Email
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EmailInfo
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public List<string> To { get; set; }

        [JsonProperty("cc")]
        public List<string> Cc { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("htmlbody")]
        public string Htmlbody { get; set; }

        [JsonProperty("attachmentarray")]
        public List<Attachmentarray> Attachmentarray { get; set; }
    }

    public partial class Attachmentarray
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("encoding")]
        public string Encoding { get; set; }

        [JsonProperty("cid")]
        public string Cid { get; set; }
    }

    //public partial class EmailInfo
    //{
    //    public static EmailInfo FromJson(string json) => JsonConvert.DeserializeObject<EmailInfo>(json, QuickType.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this EmailInfo self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    //}

    //internal static class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters = {
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}
}
