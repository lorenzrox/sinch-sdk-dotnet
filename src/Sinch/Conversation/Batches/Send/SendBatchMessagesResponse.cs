using System;
using System.Text.Json.Serialization;

namespace Sinch.Conversation.Batches.Send
{

    public sealed class SendBatchMessagesResponse
    {
        [JsonPropertyName("batch_id")]
        public string BatchId { get; set; } = default!;

        [JsonPropertyName("send_after")]
        public DateTimeOffset SendAfter { get; set; }
    }

}
