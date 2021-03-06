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
using YesCommander.Classes;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for FollowerRow.xaml
    /// </summary>
    public partial class FollowerRow : UserControl
    {
        private Follower currentFollower;
        private List<Follower> favoriteFollowers;
        private bool isFavoriteMode = false;

        public FollowerRow()
        {
            InitializeComponent();
        }

        public FollowerRow( Follower follower, ref List<Follower> list, bool isFavorite = false )
        {
            InitializeComponent();
            this.currentFollower = follower;
            this.favoriteFollowers = list;
            this.isFavoriteMode = isFavorite;
            this.SetFollower( follower );
            if ( isFavorite )
            {
                this.isFavorit.IsChecked = true;
                this.isFavorit.IsEnabled = false;
            }
        }

        public FollowerRow( Follower follower, bool isFavorite )
        {
            InitializeComponent();
            this.isFavoriteMode = true;
            this.isFavorit.IsEnabled = false;
            this.isFavorit.IsChecked = isFavorite;
            this.currentFollower = follower;
            this.SetFollower( follower );
        }

        public void Clear()
        {
            this.textName.Text = string.Empty;
            this.textRace.Text = string.Empty;
            this.textClass.Text = string.Empty;
            this.textLevel.Text = string.Empty;
            this.textItemLevel.Text = string.Empty;
            this.textIsFrozen.Text = string.Empty;
            foreach ( Image image in this.abilities.Children )
            {
                image.Source = null;
            }
            foreach ( Image image in this.possibleAblities.Children )
            {
                image.Source = null;
            }
            foreach ( Image image in this.traits.Children )
            {
                image.Source = null;
            }
        }

        public void SetFollower( Follower follower )
        {
            this.Clear();

            this.textName.Text = follower.Name;
            if ( follower.Quolaty == 4 )
                this.textName.Foreground = Brushes.BlueViolet;
            else if ( follower.Quolaty == 3 )
                this.textName.Foreground = Brushes.DodgerBlue;
            else if ( follower.Quolaty == 2 )
                this.textName.Foreground = Brushes.Lime;

            this.textRace.Text = follower.Race.ToString();
            this.textClass.Text = Follower.GetCNStringByClass( follower.Class );
            this.textLevel.Text = follower.Level.ToString();
            this.textItemLevel.Text = follower.ItemLevel.ToString();
            if ( follower.ItemLevel >=645 )
                this.textItemLevel.Foreground = Brushes.BlueViolet;
            else if ( follower.ItemLevel >= 630 )
                this.textItemLevel.Foreground = Brushes.DodgerBlue;
            else if ( follower.ItemLevel >= 600 )
                this.textItemLevel.Foreground = Brushes.Lime;
            if ( !follower.IsActive )
                this.textIsFrozen.Text = "已冻结";

            int i=0;
            foreach ( Follower.Abilities ability in follower.AbilityCollection )
            {
                ( this.abilities.Children[ i ] as Image ).Source = Follower.GetImageFromAbility( ability );
                i++;
            }
            i = 0;
            foreach ( Follower.Traits trait in follower.TraitCollection )
            {
                ( this.traits.Children[ i ] as Image ).Source = Follower.GetImageFromFromTrait( trait );
                i++;
            }
            if ( follower.Quolaty < 4 )
            {
                List<Follower.Abilities> ablities = Follower.GetAbilityFromClass( follower.Class );
                ablities.Remove( follower.AbilityCollection[ 0 ] );
                for ( int j = 0; j < 4; j++ )
                {
                    ( this.possibleAblities.Children[ j ] as Image ).Source = Follower.GetImageFromAbility( ablities[ j ] );
                }
            }
        }

        private void isFavorit_Checked( object sender, RoutedEventArgs e )
        {
            if ( !this.isFavoriteMode )
                this.favoriteFollowers.Add( this.currentFollower );
        }

        private void isFavorit_Unchecked( object sender, RoutedEventArgs e )
        {
            if ( !this.isFavoriteMode )
            this.favoriteFollowers.Remove( this.currentFollower );
        }

    }
}
