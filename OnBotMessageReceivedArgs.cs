namespace TwitchChatOverlay
{
    class OnBotMessageReceivedArgs
    {
        public string ChannelName { get; private set; }

        public string Message { get; private set; }

        public OnBotMessageReceivedArgs(string channelName , string message)
        {
            ChannelName = channelName;
            Message = message;
        }

        public override string ToString() => $"{ChannelName} : {Message}";
    }
}
