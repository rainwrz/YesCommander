using System;
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
using Elysium.Controls;
using Elysium.Parameters;
using Elysium;
using YesCommander.Model;
using YesCommander.Classes;
using System.Data;
using YesCommander.CustomControls;
using System.ComponentModel;
using System.Reflection;

namespace YesCommander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
    //public partial class MainWindow : System.Windows.Window
    //{
        public Missions Missions;
        private Mission CurrentMission;
        private DataTable tableFollowers;
        private List<Follower> currentFollowers;
        private List<Follower> favoriteFollowers;
        private Dictionary<int, Mission> currentMissions;
        private MissionGrid missionGrid;
        private MissionGrid followerGrid;
        private bool isCheckboxTriggeredByself = false;
        private int maxNumberOfTeam = 10;

        public MainWindow()
        {
            InitializeComponent();
            this.maxNumberOfPartyComboBox.ItemsSource = new List<string>() { "最多显示1只队伍","最多显示10只队伍", "最多显示20只队伍", "最多显示50只队伍(降低性能)", "最多显示100条队伍(你确定？)" };
            this.maxNumberOfPartyComboBox.SelectedIndex = 1;
            this.Missions = new Missions();
            this.FillInMissions( this.Missions.HighmaulMissions );
            this.followerDetailPanel.LoadFile();
            this.radioFollowers.IsChecked = true;
            this.favoriteFollowers = new List<Follower>();
        }

        #region Events
        private void Loaded_1( object sender, RoutedEventArgs e )
        {
            this.missionWindowImage.ToolTip = new BaseToolTip( "任务列表", "打开一个新的窗口，查看所有任务详情。" );
            ToolTipService.SetInitialShowDelay( this.missionWindowImage, 0 );
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            this.about.ToolTip = new BaseToolTip( "YesCommander", "作者：梧桐哟梧桐，当前版本："
                + version.Major.ToString()+"."+version.Minor.ToString()
                + "\r\n\r\n注意：此软件仅发布在NGA。下载地址仅限百度网盘，其他途径下载均有风险。"
                + " \r\n\r\n熊猫酒仙联盟公会【月 神 之 怒】招募有识之士。共同迎战德拉诺之王。" );
            ToolTipService.SetShowDuration( this.about, 60000 );
            ToolTipService.SetInitialShowDelay( this.about, 0 );
        }

        private void RadioButton_Checked( object sender, RoutedEventArgs e )
        {
            this.followerPanel.Visibility = System.Windows.Visibility.Hidden;
            this.questPanel.Visibility = System.Windows.Visibility.Hidden;
            this.followerDetailPanel.Visibility = System.Windows.Visibility.Hidden;

            if ( this.radioFollowers.IsChecked == true )
                this.followerPanel.Visibility = Visibility.Visible;
            else if ( this.radioMissions.IsChecked == true )
            {
                this.questPanel.Visibility = System.Windows.Visibility.Visible;
                if ( missionsComboBox.SelectedIndex == -1 )
                    missionsComboBox.SelectedIndex = 0;
            }
            else if ( this.radioAllFollowers.IsChecked == true )
                this.followerDetailPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( this.missionsComboBox.SelectedIndex == -1 )
                return;
            this.ClearImages();
            this.CurrentMission = this.currentMissions.First( x => ( x.Key + " " + ( string.IsNullOrWhiteSpace( x.Value.MissionNameCN ) ? x.Value.MissionName : x.Value.MissionNameCN ) )
                == this.missionsComboBox.SelectedItem.ToString() ).Value;
            this.textItemLevel.Text = this.CurrentMission.ItemLevelNeed.ToString();
            this.textMissionTime.Text = this.CurrentMission.MissionTimeStr;
            this.textReward.Text = this.CurrentMission.MissionReward;
            this.textBasicChance.Text = ( this.CurrentMission.BasicSucessChange * 100 ).ToString();
            this.textRemark.Text = string.IsNullOrEmpty( this.CurrentMission.Remark ) ? "无" : this.CurrentMission.Remark;
            int i = 0;
            int j=0;
            foreach ( KeyValuePair<Follower.Abilities, int> pair in this.CurrentMission.CounterAbilitiesCollection )
            {
                j=pair.Value;
                while ( j > 0 )
                {
                    ( this.abilityPanel.Children[ i ] as Image ).Source = Follower.GetImageFromAbility( pair.Key );
                    ( this.abilityPanel.Children[ i ] as Image ).Visibility = System.Windows.Visibility.Visible;
                    i++;
                    j--;
                }
            }
            this.trait.Source = Follower.GetImageFromFromTrait( this.CurrentMission.SlayerNeed );
            this.titleGrid.Visibility = System.Windows.Visibility.Visible;
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }

        private void readButton_Click( object sender, RoutedEventArgs e )
        {
            LoadData.OpenFile( ref this.tableFollowers );
            if ( this.tableFollowers != null )
            {
                this.titleAllFavorite_MouseDown( this.titleAll, null );
                this.GenerateFollowers();
                if ( this.followerGrid != null )
                    this.followerGrid.Populate( this.tableFollowers );
                this.radioFollowers.Visibility = System.Windows.Visibility.Visible;
                this.radioMissions.Visibility = System.Windows.Visibility.Visible;
                this.radioAllFollowers.Visibility = System.Windows.Visibility.Visible;
                this.titleGrid2.Visibility = System.Windows.Visibility.Visible;
                this.followerImage.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void titleBlock_MouseDown( object sender, MouseButtonEventArgs e )
        {
            this.titleHighMaul.FontSize = 15;
            this.titleRing.FontSize = 15;
            this.titleElse.FontSize = 15;

            ( sender as TextBlock ).FontSize = 18;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleHighMaul":
                    this.FillInMissions( this.Missions.HighmaulMissions );
                    break;
                case "titleRing":
                    this.FillInMissions( this.Missions.RingMissions );
                    break;
                case "titleElse":
                    this.FillInMissions( this.Missions.OtherThreeFollowersMissions );
                    break;
                default: break;
            }
            this.missionsComboBox.SelectedIndex = 0;
        }

        private void missionWindow_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( this.missionGrid == null )
            {
                this.missionGrid = new MissionGrid("所有任务");
                this.missionGrid.Populate( this.Missions.AllMissions );
                this.missionGrid.Owner = this;
            }
            if ( this.missionGrid.Visibility == System.Windows.Visibility.Visible )
                this.missionGrid.Visibility = System.Windows.Visibility.Hidden;
            else
            {
                this.missionGrid.Show();
            }
         }

        private void followerWindow_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( this.followerGrid == null )
            {
                this.followerGrid = new MissionGrid("随从列表");
                this.followerGrid.Populate( this.tableFollowers );
                this.followerGrid.Owner = this;
            }
            if ( this.followerGrid.Visibility == System.Windows.Visibility.Visible )
                this.followerGrid.Visibility = System.Windows.Visibility.Hidden;
            else
            {
                this.followerGrid.Show();
            }
         }

        private void titleBlock_MouseEnter( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock )
                ( sender as TextBlock ).Foreground = Brushes.White;
            this.Cursor = Cursors.Hand;
        }

        private void titleBlock_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock )
                ( sender as TextBlock ).Foreground = (Brush)new BrushConverter().ConvertFromString( "#ffe8ce" );
            this.Cursor = Cursors.Arrow;
        }

        private void checkboxMaxiLevel_Checked( object sender, RoutedEventArgs e )
        {
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }

        private void checkboxMaxiLevel_Unchecked( object sender, RoutedEventArgs e )
        {
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }

        private void Window_Closing( object sender, CancelEventArgs e )
        {
            if ( this.missionGrid != null )
            {
                this.missionGrid.ReadyToClose = true;
                this.missionGrid.Close();
            }
            if ( this.followerGrid != null )
            {
                this.followerGrid.ReadyToClose = true;
                this.followerGrid.Close();
            }
        }

        private void tag_MouseEnter( object sender, MouseEventArgs e )
        {
            if ( ( sender as Image ).Name == this.about.Name )
                this.Cursor = Cursors.Help;
            else
                this.Cursor = Cursors.Hand;
        }

        private void tag_MouseLeave( object sender, MouseEventArgs e )
        {
            this.Cursor = Cursors.Arrow;
        }

        private void checkboxUsingFavorite_Checked( object sender, RoutedEventArgs e )
        {
            if ( this.isCheckboxTriggeredByself )
                return;

            if ( this.checkboxUsingFavorite.IsChecked == true )
            {
                if ( this.favoriteFollowers.Count < 3 )
                {
                    MessageBox.Show( "偏好随从少于3个，请选择至少3个偏好随从。", "错误", MessageBoxButton.OK, MessageBoxImage.Warning );
                    this.isCheckboxTriggeredByself = true;
                    this.checkboxUsingFavorite.IsChecked = false;
                    this.isCheckboxTriggeredByself = false;
                }
                else
                    this.PlaceParties( true );
            }
            else
            {
                this.PlaceParties( false );
            }

        }

        private void titleAllFavorite_MouseDown( object sender, MouseButtonEventArgs e )
        {
            this.titleFavorite.FontSize = 18;
            this.titleAll.FontSize = 18;
            this.allScroll.Visibility = System.Windows.Visibility.Hidden;
            this.favoriteScroll.Visibility = System.Windows.Visibility.Hidden;

            ( sender as TextBlock ).FontSize = 22;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleFavorite":
                    {
                        this.favoriteScroll.Visibility = System.Windows.Visibility.Visible;
                        this.favoriteRows.Children.Clear();
                        this.AddEpicFollowers( true );
                        this.AddFollowers( true );
                    }
                    break;
                case "titleAll":
                    this.allScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                default: break;
            }
        }

        private void maxNumberOfPartyComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( !this.IsLoaded )
                return;
            switch ( this.maxNumberOfPartyComboBox.SelectedIndex )
            {
                case 0: this.maxNumberOfTeam = 1; break;
                default:
                case 1: this.maxNumberOfTeam = 10; break;
                case 2: this.maxNumberOfTeam = 20; break;
                case 3: this.maxNumberOfTeam = 50; break;
                case 4: this.maxNumberOfTeam = 100; break;
            }
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }
        #endregion //Events

        #region Methods

        private void FillInMissions( Dictionary<int, Mission> missions )
        {
            List<string> items = new List<string>();
            this.currentMissions = missions;
            foreach ( KeyValuePair<int, Mission> pair in missions )
            {
                items.Add( pair.Key + " " + ( string.IsNullOrWhiteSpace( pair.Value.MissionNameCN ) ? pair.Value.MissionName : pair.Value.MissionNameCN ) );
            }
            this.missionsComboBox.ItemsSource = items;
        }

        private void ClearImages()
        {
            foreach ( Image image in this.abilityPanel.Children )
            {
                image.Source = null;
                image.Visibility = System.Windows.Visibility.Collapsed;
            }
            this.trait.Source = null;
        }

        private void PlaceParties( bool isUsingFavorite )
        {
            List<Mission> missions;
            if ( this.CurrentMission.FollowersNeed == 3 )
                missions = this.AssignMissionForThreeFollowers( this.CurrentMission, isUsingFavorite );
            else if ( this.CurrentMission.FollowersNeed == 2 )
                missions = this.AssignMissionForTwoFollowers( this.CurrentMission );
            else if ( this.CurrentMission.FollowersNeed == 1 )
                missions = this.AssignMissionForOneFollowers( this.CurrentMission );
            else
                return;

            foreach ( Party party in this.partyPanel.Children )
            {
                party.Clear();
            }
            this.partyPanel.Children.Clear();
            var data = from temp in missions.AsEnumerable()
                       //where temp.TotalSucessChance > 0.8
                       select temp;
            data = data.OrderByDescending( x => x.TotalSucessChance ).ThenBy( x => x.MissionTimeCaculated );
            int i = 0;
            foreach ( Mission mission in data )
            {
                if ( i >= this.maxNumberOfTeam )
                    break;
                this.partyPanel.Children.Add( new Party( mission ) );
                //( this.partyPanel.Children[ i ] as Party ).Create( mission );
                i++;
            }
        }

        private void GenerateFollowers()
        {
            this.currentFollowers = new List<Follower>();
            this.favoriteFollowers = new List<Follower>();
            List<int> abilities;
            List<int> traits;
            foreach ( DataRow row in this.tableFollowers.Rows )
            {
                abilities = new List<int>();
                abilities.Add( Convert.ToInt16( row[ "技能ID1" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) == 4 )
                    abilities.Add( Convert.ToInt16( row[ "技能ID2" ] ) );
                traits = new List<int>();
                traits.Add( Convert.ToInt16( row[ "特长ID1" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) > 2 )
                    traits.Add( Convert.ToInt16( row[ "特长ID2" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) > 3 )
                    traits.Add( Convert.ToInt16( row[ "特长ID3" ] ) );

                this.currentFollowers.Add( new Follower( row[ "姓名" ].ToString(), Convert.ToInt16( row[ "品质" ] ), Convert.ToInt16( row[ "等级" ] ), Convert.ToInt16( row[ "装等" ] ),
                    row[ "种族" ].ToString(), Follower.GetClassBySpec( Convert.ToInt16( row[ "职业ID" ] ) ), row[ "职业" ].ToString(), Convert.ToInt16( row[ "激活" ] ), abilities, traits ) );
            }

            this.followerRows.Children.Clear();
            this.AddEpicFollowers();
            this.AddFollowers();
        }

        private void AddFollowers( bool isFavorite=false )
        {
            if ( isFavorite )
            {
                if ( this.favoriteFollowers.Count == 0 )
                    return;
                foreach ( Follower follower in this.favoriteFollowers.FindAll( x => x.Quolaty < 4 ).OrderBy( x => x.AbilityCollection[ 0 ] ) )
                {
                    this.favoriteRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers, true ) );
                }
            }
            else
            {
                foreach ( Follower follower in this.currentFollowers.FindAll( x => x.Quolaty < 4 ).OrderBy( x => x.AbilityCollection[ 0 ] ) )
                {
                    this.followerRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers ) );
                }
            }
        }

        private void AddEpicFollowers( bool isFavorite = false )
        {
            if ( isFavorite )
            {
                if ( this.favoriteFollowers.Count == 0 )
                    return;
                for ( int i = 0; i < 9; i++ )
                {
                    Follower.Abilities ability = (Follower.Abilities)i;
                    foreach ( Follower follower in this.favoriteFollowers.FindAll( x => ( x.Quolaty == 4 && x.AbilityCollection[ 0 ] == ability ) ).OrderBy( x => x.AbilityCollection[ 1 ] ) )
                    {
                        this.favoriteRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers, true ) );
                    }
                }
            }
            else for ( int i = 0; i < 9; i++ )
            {
                Follower.Abilities ability = (Follower.Abilities)i;
                foreach ( Follower follower in this.currentFollowers.FindAll( x => ( x.Quolaty == 4 && x.AbilityCollection[ 0 ] == ability ) ).OrderBy( x => x.AbilityCollection[ 1 ] ) )
                {
                    this.followerRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers ) );
                }
            }
        }

        private List<Mission> AssignMissionForThreeFollowers( Mission mission, bool isUsingFaverite = false )
        {
            List<Mission> missions = new List<Mission>();
            List<Follower> list = isUsingFaverite ? this.favoriteFollowers : this.currentFollowers;
            if ( list.Count >= 3 )
            {
                for ( int i = 0; i < list.Count; i++ )
                {
                    for ( int j = 0; j < list.Count; j++ )
                    {
                        if ( j <= i )
                            continue;
                        for ( int k = 0; k < list.Count; k++ )
                        {
                            if ( k <= j )
                                continue;
                            else
                            {
                                Mission newMission = mission.Copy();
                                newMission.IsUsingMaxiLevel = this.checkboxMaxiLevel.IsChecked == true;
                                newMission.AssignFollowers( new List<Follower>() { list[ i ], list[ j ], list[ k ] } );
                                missions.Add( newMission );
                            }
                        }
                    }
                }
            }
            return missions;
        }

        private List<Mission> AssignMissionForTwoFollowers( Mission mission )
        {
            List<Follower> followers;
            List<Mission> missions = new List<Mission>();
            for ( int j = 0; j < this.currentFollowers.Count; j++ )
            {
                for ( int k = 0; k < this.currentFollowers.Count; k++ )
                {
                    if ( k <= j )
                        continue;
                    else
                    {
                        Mission newMission = mission.Copy();
                        followers = new List<Follower>();
                        followers.Add( this.currentFollowers[ j ] );
                        followers.Add( this.currentFollowers[ k ] );
                        newMission.AssignFollowers( followers );
                        missions.Add( newMission );
                    }
                }
            }
            return missions;
        }

        private List<Mission> AssignMissionForOneFollowers( Mission mission )
        {
            List<Follower> followers;
            List<Mission> missions = new List<Mission>();
            for ( int k = 0; k < this.currentFollowers.Count; k++ )
            {
                Mission newMission = mission.Copy();
                followers = new List<Follower>();
                followers.Add( this.currentFollowers[ k ] );
                newMission.AssignFollowers( followers );
                missions.Add( newMission );
            }
            return missions;
        }

        public static void ShowDialog( string str )
        {
            MessageBox.Show( "str" );
        }
        #endregion //Methods
    }
}
