﻿#region

using System;
using System.Windows;
using System.Windows.Media.Animation;
using LegendaryClient.Logic;
using agsXMPP;

#endregion

namespace LegendaryClient.Controls
{
    /// <summary>
    ///     Interaction logic for NotificationChatPlayer.xaml
    /// </summary>
    public partial class NotificationChatPlayer
    {
        public string PlayerName;

        public NotificationChatPlayer()
        {
            InitializeComponent();
            Blink();
        }

        public void Blink()
        {
            var fadingAnimation = new DoubleAnimation
            {
                From = 0.6,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            fadingAnimation.Completed += (eSender, eArgs) =>
            {
                fadingAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 0.6,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };
            };

            BlinkRectangle.BeginAnimation(OpacityProperty, fadingAnimation);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Client.ChatItem != null)
            {
                Client.MainGrid.Children.Remove(Client.ChatItem);
                Client.XmppConnection.MessageGrabber.Remove(new Jid((string)Client.ChatItem.Tag));
                Client.ChatItem = null;
            }

            Client.ChatListView.Items.Remove(this);
        }
    }
}