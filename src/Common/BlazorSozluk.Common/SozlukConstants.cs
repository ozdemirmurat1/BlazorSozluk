namespace BlazorSozluk.Common
{
    public class SozlukConstants
    {
        public const string RabbitMQHost = "amqps://ixjpkcqy:J9KOIqvsoOosDs9coneY0vPiE5kAp0Um@rat.rmq2.cloudamqp.com/ixjpkcqy";
        public const string DefaultExchangeType = "direct";

        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        public const string FavExchangeName = "FavExchangeName";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    }
}
