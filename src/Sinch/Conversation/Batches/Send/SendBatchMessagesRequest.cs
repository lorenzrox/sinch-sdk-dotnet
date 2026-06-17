
using Sinch.Conversation.Messages.Message;
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
    }

}
