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

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for followerFromDatabasexaml.xaml
    /// </summary>
    public partial class followerFromDatabasexaml : UserControl
    {
        public followerFromDatabasexaml(string nameCN, string nameEN, string nameTCN, string race, string level, int quolaty)
        {
            InitializeComponent();
            this.Clear();
            this.CN.Text = nameCN;
            this.EN.Text = nameEN;
            this.TranCN.Text = nameTCN;
            this.race.Text = race;
            this.level.Text = level;
            Brush color;
            switch ( quolaty )
            {
                case 4: color = Brushes.BlueViolet; break;
                case 3: color = Brushes.DodgerBlue; break;
                case 2:
                default:
                    color = Brushes.Lime; break;
            }
            this.CN.Foreground = this.EN.Foreground = this.TranCN.Foreground = this.race.Foreground = this.level.Foreground = color;
        }
        public void Clear()
        {
            this.CN.Text = string.Empty;
            this.EN.Text = string.Empty;
            this.TranCN.Text = string.Empty;
            this.race.Text = string.Empty;
            this.level.Text = string.Empty;
        }
    }
}
