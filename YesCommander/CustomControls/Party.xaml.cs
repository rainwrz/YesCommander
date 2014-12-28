using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Party.xaml
    /// </summary>
    public partial class Party : UserControl
    {
        public Party()
        {
            InitializeComponent();
        }
        public Party( Mission mission )
        {
            InitializeComponent();
            this.Clear();
            this.Create( mission );
        }

        public void Create( Mission mission )
        {
            Follower follower1 = mission.AssignedFollowers[ 0 ];
            this.PlaceFollower( follower1, mission, this.followerName1, this.followerIlevel1, this.followerFrozen1, this.follower1Images );
            if ( mission.AssignedFollowers.Count > 1 )
            {
                Follower follower2 = mission.AssignedFollowers[ 1 ];
                this.PlaceFollower( follower2, mission, this.followerName2, this.followerIlevel2, this.followerFrozen2, this.follower2Images );
            }
            if ( mission.AssignedFollowers.Count > 2 )
            {
                Follower follower3 = mission.AssignedFollowers[ 2 ];
                this.PlaceFollower( follower3, mission, this.followerName3, this.followerIlevel3, this.followerFrozen3, this.follower3Images );
            }
            this.PlacePartyBuff( mission );
        }

        public void PlaceFollower( Follower follower, Mission mission, TextBlock block, TextBlock ilevel, TextBlock isFrozen, StackPanel followerImages )
        {
            block.Text = follower.Name;
            ilevel.Text ="(" + follower.ItemLevel.ToString() + ")";
            if ( !follower.IsActive )
                isFrozen.Text = "(冻结)";
            if ( follower.Quolaty == 4 )
                block.Foreground = Brushes.BlueViolet;
            else if ( follower.Quolaty == 3 )
                block.Foreground = Brushes.DodgerBlue;
            else if ( follower.Quolaty == 2 )
                block.Foreground = Brushes.Lime;
            ilevel.Foreground = follower.ItemLevel >= mission.ItemLevelNeed ? Brushes.Lime : Brushes.Red;

            followerImages.Children[ 0 ].Visibility = System.Windows.Visibility.Visible;
            ( ( followerImages.Children[ 0 ] as StackPanel ).Children[ 0 ] as Image ).Source = Follower.GetImageFromAbility( follower.AbilityCollection[ 0 ] );
            if ( mission.CounterAbilitiesCollection.ContainsKey( follower.AbilityCollection[ 0 ] ) )
            {
                ImageBrush ib = new ImageBrush( new BitmapImage( new Uri(
               "pack://application:,,,/YesCommander;component/Resources/imageBorder.png", UriKind.RelativeOrAbsolute ) ) );
                ib.Stretch = Stretch.None;
                ( followerImages.Children[ 0 ] as StackPanel).Background = ib;
            }

            if ( follower.AbilityCollection.Count > 1 )
            {
                followerImages.Children[ 1 ].Visibility = System.Windows.Visibility.Visible;
                ( ( followerImages.Children[ 1 ] as StackPanel ).Children[ 0 ] as Image ).Source = Follower.GetImageFromAbility( follower.AbilityCollection[ 1 ] );
                if ( mission.CounterAbilitiesCollection.ContainsKey( follower.AbilityCollection[ 1 ] ) )
                {
                    ImageBrush ib = new ImageBrush( new BitmapImage( new Uri(
                   "pack://application:,,,/YesCommander;component/Resources/imageBorder.png", UriKind.RelativeOrAbsolute ) ) );
                    ib.Stretch = Stretch.None;
                    ( followerImages.Children[ 1 ] as StackPanel ).Background = ib;
                }
            }
            int i = 2;
            foreach ( Follower.Traits trait in follower.TraitCollection )
            {
                if ( mission.partyBuffs.Contains( trait ) )
                {
                    if( Follower.IsRaceLoverTrait(trait)&& !mission.AssignedFollowers.Exists(x=>(x.Name!=follower.Name && Follower.GetRaceMatchedByTrait(trait) == x.Race)))
                    {
                        continue;
                    }
                    followerImages.Children[ i ].Visibility = System.Windows.Visibility.Visible;
                    ( ( followerImages.Children[ i ] as StackPanel ).Children[ 0 ] as Image ).Source = Follower.GetImageFromFromTrait( trait );
                    i++;
                }
            }
        }
        public void PlacePartyBuff( Mission mission )
        {
            int i = 0;
            foreach ( Follower.Traits trait in mission.partyBuffs )
            {
                if ( trait == Follower.Traits.Unknow )
                    continue;
                ( this.partyBuffs.Children[ i ] as Image ).Source = Follower.GetImageFromFromTrait( trait );
                ( this.partyBuffs.Children[ i ] as Image ).Visibility = System.Windows.Visibility.Visible;
                i++;
            }
            this.timeNeed.Text = mission.MissionTimeCaculatedStr;
            this.timeNeed.Foreground = mission.partyBuffs.Contains( Follower.Traits.EpicMount ) ? Brushes.Lime : Brushes.White;
            double sucessChance = 100 * mission.TotalSucessChance;
            this.sucessChance.Text = sucessChance.ToString( "#,##0.##", new CultureInfo( "en-US" ) );
            this.sucessChance.Foreground = sucessChance >= 100 ? Brushes.Lime : Brushes.Red;
        }
        public void Clear()
        {
            this.followerName1.Text = string.Empty;
            this.followerIlevel1.Text = string.Empty;
            this.followerFrozen1.Text = string.Empty;
            this.followerName2.Text = string.Empty;
            this.followerIlevel2.Text = string.Empty;
            this.followerFrozen2.Text = string.Empty;
            this.followerName3.Text = string.Empty;
            this.followerIlevel3.Text = string.Empty;
            this.followerFrozen3.Text = string.Empty;
            foreach ( StackPanel panel in this.follower1Images.Children )
            {
                panel.Background = null;
                panel.Visibility = System.Windows.Visibility.Collapsed;
                ( panel.Children[ 0 ] as Image ).Source = null;
            }
            foreach ( StackPanel panel in this.follower2Images.Children )
            {
                panel.Background = null;
                panel.Visibility = System.Windows.Visibility.Collapsed;
                ( panel.Children[ 0 ] as Image ).Source = null;
            }
            foreach ( StackPanel panel in this.follower3Images.Children )
            {
                panel.Background = null;
                panel.Visibility = System.Windows.Visibility.Collapsed;
                ( panel.Children[ 0 ] as Image ).Source = null;
            }
            foreach ( Image image in this.partyBuffs.Children )
            {
                image.Source = null;
                image.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
