namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class VoteService
    {
        private readonly HttpClient _client;

        public VoteService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await _client.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote error");
        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await _client.PostAsync($"/api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote error");
        }


    }
}
