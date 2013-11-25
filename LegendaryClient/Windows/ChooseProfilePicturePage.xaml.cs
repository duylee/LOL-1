﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LegendaryClient.Logic;
using PVPNetConnect.RiotObjects.Platform.Catalog.Icon;
using PVPNetConnect.RiotObjects.Platform.Statistics;
using PVPNetConnect.RiotObjects.Platform.Summoner;
using PVPNetConnect.RiotObjects.Platform.Summoner.Icon;

namespace LegendaryClient.Windows
{
    /// <summary>
    /// Interaction logic for ChooseProfilePicturePage.xaml
    /// </summary>
    public partial class ChooseProfilePicturePage : Page
    {
        public ChooseProfilePicturePage()
        {
            InitializeComponent();
            GetIcons();
        }

        private async void GetIcons()
        {
            SummonerIconInventoryDTO PlayerIcons = await Client.PVPNet.GetSummonerIconInventory(Client.LoginPacket.AllSummonerData.Summoner.SumId);
            foreach (Icon ic in PlayerIcons.SummonerIcons)
            {
                ListViewItem item = new ListViewItem();
                Image champImage = new Image();
                champImage.Height = 64;
                champImage.Width = 64;
                champImage.Margin = new Thickness(5, 5, 5, 5);
                var uriSource = new Uri(Path.Combine(Client.ExecutingDirectory, "Assets", "profileicon", ic.IconId + ".png"), UriKind.Absolute);
                champImage.Source = new BitmapImage(uriSource);
                item.Content = champImage;
                item.Tag = ic.IconId;
                SummonerIconListView.Items.Add(item);
            }
            for (int i = 0; i < 29; i++)
            {
                ListViewItem item = new ListViewItem();
                Image champImage = new Image();
                champImage.Height = 64;
                champImage.Width = 64;
                champImage.Margin = new Thickness(5, 5, 5, 5);
                var uriSource = new Uri(Path.Combine(Client.ExecutingDirectory, "Assets", "profileicon", i + ".png"), UriKind.Absolute);
                champImage.Source = new BitmapImage(uriSource);
                item.Content = champImage;
                item.Tag = i;
                SummonerIconListView.Items.Add(item);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Client.OverlayContainer.Visibility = Visibility.Hidden;
        }

        private async void SetButton_Click(object sender, RoutedEventArgs e)
        {
            if (SummonerIconListView.SelectedItem != null)
            {
                ListViewItem m = (ListViewItem)SummonerIconListView.SelectedItem;
                int SummonerIcon = Convert.ToInt32(m.Tag);
                await Client.PVPNet.UpdateProfileIconId(SummonerIcon);
                Client.LoginPacket.AllSummonerData.Summoner.ProfileIconId = SummonerIcon;
                Client.SetChatHover();
                var uriSource = new Uri(Path.Combine(Client.ExecutingDirectory, "Assets", "profileicon", SummonerIcon + ".png"), UriKind.RelativeOrAbsolute);
                Client.MainPageProfileImage.Source = new BitmapImage(uriSource);
            }
            Client.OverlayContainer.Visibility = Visibility.Hidden;
        }
    }
}
