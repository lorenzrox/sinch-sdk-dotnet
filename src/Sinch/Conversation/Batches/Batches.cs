using Sinch.Conversation.Batches.List;
using Sinch.Conversation.Batches.Send;
using Sinch.Core;
using Sinch.Logger;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sinch.Conversation.Batches
{

    /// <summary>
    ///     Apps are created and configured through
    ///     the <see href="https://dashboard.sinch.com/convapi/getting-started">Sinch Dashboard</see>,
    ///     are tied to the API user and come with a set of channel credentials
    ///     for each underlying connected channel.
    ///     The app has a list of conversations between itself and different contacts which share the same project.
    /// </summary>
    public interface ISinchConversationBatches
    {
        /// <summary>
        ///     You can send a message from a Conversation app to a contact associated with that app.
        ///     If the recipient is not associated with an existing contact, a new contact will be created.<br/><br/>
        ///     The message is added to the active conversation with the contact if a conversation already exists.
        ///     If no active conversation exists a new one is started automatically.<br/><br/>
        ///     You can find all of your IDs and authentication credentials on the
        ///     <see href="https://dashboard.sinch.com/settings/project-management">Sinch Customer Dashboard</see>
        /// </summary>
        /// <param name="request">A request params</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns><see cref="SendBatchMessagesRequest"/></returns>
        Task<SendBatchMessagesResponse> Send(SendBatchMessagesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     A batch can be canceled at any point.
        ///     If a batch is canceled while it's currently being delivered
        ///     some messages currently being processed might still be delivered.
        ///     The delivery report will indicate which messages were canceled and which weren't.<br /><br />
        ///     Canceling a batch scheduled in the future will result in an empty delivery report while canceling an already sent
        ///     batch would result in no change to the completed delivery report.
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="cancellationToken"></param>
        Task Cancel(string batchId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     This operation returns a specific batch that matches the provided batch ID.
        /// </summary>
        /// <param name="batchId">The batch ID you received from sending a message.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Batch</returns>
        Task<Batch> Get(string batchId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     With the list operation you can list batch messages 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Batch>> List(ListBatchesRequest request, CancellationToken cancellationToken = default);
    }

    internal sealed class Batches : ISinchConversationBatches
    {
        private readonly Uri _baseAddress;
        private readonly IHttp _http;
        private readonly ILoggerAdapter<ISinchConversationBatches>? _logger;
        private readonly string _projectId;

        public Batches(string projectId, Uri baseAddress, ILoggerAdapter<ISinchConversationBatches>? logger, IHttp http)
        {
            _projectId = projectId;
            _baseAddress = baseAddress;
            _logger = logger;
            _http = http;
        }

        public Task<SendBatchMessagesResponse> Send(SendBatchMessagesRequest request, CancellationToken cancellationToken = default)
        {
            var uri = new Uri(_baseAddress, $"v1/projects/{_projectId}/messages");
            _logger?.LogDebug("Sending a batch...");
            return _http.Send<SendBatchMessagesRequest, SendBatchMessagesResponse>(uri, HttpMethod.Post, request, cancellationToken: cancellationToken);
        }

        public Task Cancel(string batchId, CancellationToken cancellationToken = default)
        {
            var uri = new Uri(_baseAddress, $"v1/projects/{_projectId}/messages");
            _logger?.LogDebug("Cancelling batch with {id}...", batchId);
            return _http.Send<object, EmptyResponse>(uri, HttpMethod.Delete, new { batch_id = batchId }, cancellationToken);
        }

        public Task<List<Batch>> List(ListBatchesRequest request, CancellationToken cancellationToken = default)
        {
            var uri = new Uri(_baseAddress, $"v1/projects/{_projectId}/messages?{request.GetQueryString()}");
            _logger?.LogDebug("Listing batches...");
            return _http.Send<List<Batch>>(uri, HttpMethod.Get, cancellationToken);
        }

        public Task<Batch> Get(string batchId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(batchId))
                throw new ArgumentNullException(nameof(batchId), "BatchId could not be empty");

            var uri = new Uri(_baseAddress, $"v1/projects/{_projectId}/messages/{batchId}");
            _logger?.LogDebug("Getting batch...");
            return _http.Send<Batch>(uri, HttpMethod.Get, cancellationToken);
        }
    }

}
