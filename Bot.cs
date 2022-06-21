using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchChatOverlay
{
    internal class Bot
    {
        ConnectionCredentials _credentials = new(Cred.ChanelName,Cred.Token);
        TwitchClient _client;
        private readonly SynchronizationContext syncContext;
        public event EventHandler<OnBotMessageReceivedArgs> OnMessageReceivedEvent;

        public Bot()
            => syncContext = AsyncOperationManager.SynchronizationContext;

        internal void Connect()
        {
            _client = new();
            _client.Initialize(_credentials , Cred.ChanelName);
            _client.Connect();
            _client.OnMessageReceived += Client_OnMessageReceived;

#if DEBUG
            _client.OnLog += Client_OnLog;
#endif
        }

        private void Client_OnMessageReceived(object sender , OnMessageReceivedArgs e)
        {
            var args = new OnBotMessageReceivedArgs(e.ChatMessage.Channel , e.ChatMessage.Message);
            syncContext.Post(OnMessageReceived, args);
        }

        private void OnMessageReceived(object args) => OnMessageReceivedEvent?.Invoke(this , args as OnBotMessageReceivedArgs);

#if DEBUG
        private void Client_OnLog(object sender , OnLogArgs e) => Debug.WriteLine(e.Data);
#endif

    }
}