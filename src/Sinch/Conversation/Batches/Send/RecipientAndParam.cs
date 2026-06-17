using Sinch.Conversation.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sinch.Conversation.Batches.Send
{

    public sealed class RecipientAndParam
    {
        [JsonPropertyName("recipient")]
#if NET7_0_OR_GREATER
        public required IRecipient Recipient { get; set; }
#else
        public IRecipient Recipient { get; set; } = null!;
#endif

        [JsonPropertyName("parameters")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? Parameters { get; set; }

        [JsonPropertyName("message_metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? MessageMetadata { get; set; }

        [JsonPropertyName("conversation_metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? ConversationMetadata { get; set; }
    }

}
