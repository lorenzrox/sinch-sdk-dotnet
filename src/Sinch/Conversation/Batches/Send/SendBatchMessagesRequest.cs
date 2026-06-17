
using Sinch.Conversation.Messages.Message;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sinch.Conversation.Batches.Send
{

    public sealed class SendBatchMessagesRequest
    {
        [JsonPropertyName("app_id")]
        public string AppId { get; set; } = default!;

        [JsonPropertyName("message")]
        public AppMessage Message { get; set; } = default!;

        [JsonPropertyName("recipient_and_params")]
        public List<RecipientAndParam> RecipientAndParams { get; set; } = new();

        [JsonPropertyName("processing_strategy")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProcessingStrategy? ProcessingStrategy { get; set; }

        [JsonPropertyName("batch_metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? BatchMetadata { get; set; }

        [JsonPropertyName("message_metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? MessageMetadata { get; set; }

        [JsonPropertyName("conversation_metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? ConversationMetadata { get; set; }

        [JsonPropertyName("send_after")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? SendAfter { get; set; }

        [JsonPropertyName("callback_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CallbackUrl { get; set; }

        [JsonPropertyName("channel_priority_order")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ConversationChannel>? ChannelPriorityOrder { get; set; }

        [JsonPropertyName("ttl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Ttl { get; set; }

        [JsonPropertyName("channel_properties")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? ChannelProperties { get; set; }

        [JsonPropertyName("correlation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorrelationId { get; set; }

    }

}
