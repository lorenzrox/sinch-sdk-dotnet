using Sinch.Core;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sinch.Conversation.Batches
{

    public sealed class Batch
    {
        [JsonConverter(typeof(EnumRecordJsonConverter<StatusEnum>))]
        public record StatusEnum(string Value) : EnumRecord(Value)
        {
            public static readonly StatusEnum Ready = new("READY");
            public static readonly StatusEnum Scheduled = new("SCHEDULED");
            public static readonly StatusEnum Processed = new("PROCESSED");
            public static readonly StatusEnum Cancelled = new("CANCELLED");
        }

        [JsonPropertyName("batch_id")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("meta_data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? MetaData { get; set; }

        [JsonPropertyName("status")]
#if NET7_0_OR_GREATER
        public required StatusEnum Status { get; set; }
#else
        public StatusEnum Status { get; set; } = null!;
#endif

        [JsonPropertyName("send_after")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? SendAfter { get; set; }
    }

}
