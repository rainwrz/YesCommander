using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using YesCommander.Classes;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for AllFollowersByClass.xaml
    /// </summary>
    public partial class AllFollowersByClass : UserControl
    {
        public DataTable allFollowersAli;
        public DataTable allFollowersHrd;
        public List<Follower> listAli;
        public List<Follower> listHrd;
        public AllFollowersByClass()
        {
            InitializeComponent();

            this.classComboBox.ItemsSource = new List<string> { 
                "死亡骑士-鲜血", "死亡骑士-冰霜", "死亡骑士-邪恶", 
                "德鲁伊-平衡", "德鲁伊-野性", "德鲁伊-恢复", "德鲁伊-守护",
                "战士-武器", "战士-狂暴", "战士-防护", 
                "术士-恶魔", "术士-痛苦", "术士-毁灭", 
                "武僧-织雾", "武僧-酿酒", "武僧-踏风", 
                "法师-奥术", "法师-火焰", "法师-冰霜", 
                "牧师-神圣", "牧师-戒律", "牧师-暗影", 
                "猎人-兽王", "猎人-生存", "猎人-射击", 
                "盗贼-刺杀", "盗贼-战斗", "盗贼-敏锐", 
                "萨满-元素", "萨满-增强", "萨满-恢复", 
                "骑士-惩戒", "骑士-防护", "骑士-神圣" 
            };
        }
        public void LoadFile()
        {
            this.allFollowersAli = LoadData.LoadMissionFile( "ALI.txt" );
            this.allFollowersHrd = LoadData.LoadMissionFile( "HRD.txt" );
            this.listAli = new List<Follower>();
            foreach ( DataRow row in this.allFollowersAli.Rows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                this.listAli.Add( new Follower( row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, new List<int>(), new List<int>(),
                    row[ "英文名字" ].ToString(), row[ "简体名字" ].ToString(), row[ "繁体名字" ].ToString() ) );
            }
            this.listHrd = new List<Follower>();
            foreach ( DataRow row in this.allFollowersHrd.Rows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                this.listHrd.Add( new Follower( row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, new List<int>(), new List<int>(),
                    row[ "英文名字" ].ToString(), row[ "简体名字" ].ToString(), row[ "繁体名字" ].ToString() ) );
            }
            this.classComboBox.SelectedIndex = 0;
        }
        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            Follower.Classes currentClass = Follower.GetClassBySingleStr( this.classComboBox.SelectedItem.ToString() );

            this.aliPanel.Children.Clear();
            foreach ( Follower follower in this.listAli.FindAll( x => x.Class == currentClass ) )
            {
                this.aliPanel.Children.Add( new followerFromDatabasexaml( follower.NameCN, follower.NameEN, follower.NameTCN, follower.Race.ToString(), follower.Level.ToString(), follower.Quolaty ) );
            }
            this.hrdPanel.Children.Clear();
            foreach ( Follower follower in this.listHrd.FindAll( x => x.Class == currentClass ) )
            {
                this.hrdPanel.Children.Add( new followerFromDatabasexaml( follower.NameCN, follower.NameEN, follower.NameTCN, follower.Race.ToString(), follower.Level.ToString(), follower.Quolaty ) );
            }

            foreach ( Image image in this.abilityPanel.Children )
            {
                image.Source = null;
            }
            for ( int i = 0; i < 5; i++ )
            {
                ( this.abilityPanel.Children[ i ] as Image ).Source = Follower.GetImageFromAbility( Follower.GetAbilityFromClass( currentClass )[ i ] );
            }

        }
        private void titleBlock_MouseDown( object sender, MouseButtonEventArgs e )
        {
            this.titleAli.FontSize = 18;
            this.titleHrd.FontSize = 18;
            this.aliScroll.Visibility = System.Windows.Visibility.Hidden;
            this.hrdScroll.Visibility = System.Windows.Visibility.Hidden;

            ( sender as TextBlock ).FontSize = 22;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleAli":
                    this.aliScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "titleHrd":
                    this.hrdScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                default: break;
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
                ( sender as TextBlock ).Foreground = ( sender as TextBlock ).Name == "titleAli" ? Brushes.DodgerBlue : Brushes.Red;
            this.Cursor = Cursors.Arrow;
        }
    }
}
