namespace BlazorSozluk.Common
{
    public class SozlukConstants
    {
        public const string RabbitMQHost = "amqps://ixjpkcqy:J9KOIqvsoOosDs9coneY0vPiE5kAp0Um@rat.rmq2.cloudamqp.com/ixjpkcqy";
        public const string DefaultExchangeType = "direct";

        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        public const string FavExchangeName = "FavExchange";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueueName";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";

        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
        public const string VoteExchangeName = "VoteExchange";
    }
}
