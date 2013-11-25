﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LegendaryClient.Logic;

namespace LegendaryClient.Windows.Profile
{
    /// <summary>
    /// Interaction logic for Masteries.xaml
    /// </summary>
    public partial class Masteries : Page
    {
        public Masteries()
        {
            InitializeComponent();
            int i = 1;
            foreach (var MasteryPage in Client.LoginPacket.AllSummonerData.MasteryBook.BookPages)
            {
                Button b = new Button();
                b.Content = i++;
                b.Width = 28;
                b.Margin = new Thickness(2, 0, 2, 0);
                MasteryPageListView.Items.Add(b);
            }
        }
    }
}
