using Sinch.Core;
using System.Collections.Generic;

namespace Sinch.Conversation.Batches.List
{
    public sealed class ListBatchesRequest
    {
#if NET7_0_OR_GREATER
        public required string MetadataKey { get; set; }
#else
        public string MetadataKey { get; set; } = null!;
#endif

#if NET7_0_OR_GREATER
        public required string MetadataValue { get; set; }
#else
        public string MetadataValue { get; set; } = null!;
#endif

        internal string GetQueryString()
        {
            var list = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("metadataKey", MetadataKey),
                new KeyValuePair<string, string>("metadataValue", MetadataValue)
            };
            return StringUtils.ToQueryString(list);
        }
    }
}
