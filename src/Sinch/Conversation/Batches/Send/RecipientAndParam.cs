using Sinch.Conversation.Common;
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
    }

}
