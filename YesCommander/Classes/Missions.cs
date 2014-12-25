using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YesCommander.Classes;
using System.Data;

namespace YesCommander.Model
{
    public class Missions
    {
        public DataTable AllMissions;
        public Dictionary<int, Mission> HighmaulMissions;
        public Dictionary<int, Mission> RingMissions;
        public Dictionary<int, Mission> OtherThreeFollowersMissions;
        public Missions()
        {
            this.AllMissions = new DataTable();
            this.HighmaulMissions = new Dictionary<int, Mission>();
            this.RingMissions = new Dictionary<int, Mission>();
            this.OtherThreeFollowersMissions = new Dictionary<int, Mission>();
            this.AllMissions = LoadData.LoadMissionFile( "missions.txt" );

            var data = from temp in this.AllMissions.AsEnumerable()
                       where
                       temp.Field<string>( "任务名" ) == "Highmaul Raid"
                       select temp;
            foreach ( DataRow row in data )
            {
                this.AddMissions( row, this.HighmaulMissions );
            }

            data = from temp in this.AllMissions.AsEnumerable()
                        where
                        temp.Field<string>( "奖励" ).Contains( "3 消魔之石 （戒指任务）" ) ||
                        temp.Field<string>( "奖励" ).Contains( "18 元素符文 （戒指任务）" )
                        select temp;
            foreach ( DataRow row in data )
            {
                this.AddMissions( row, this.RingMissions );
            }
            data = from temp in this.AllMissions.AsEnumerable()
                       where
                       temp.Field<string>( "任务名" ) != "Highmaul Raid" &&
                       temp.Field<string>( "奖励" ) != "3 消魔之石 （戒指任务）" &&
                       temp.Field<string>( "奖励" ) != "18 元素符文 （戒指任务）" &&
                       temp.Field<string>( "随从数量" ) == "3"
                       select temp;
            foreach ( DataRow row in data )
            {
                this.AddMissions( row, this.OtherThreeFollowersMissions );
            }
        }
        private void AddMissions( DataRow row, Dictionary<int, Mission> missions )
        {
            Dictionary<Follower.Abilities, int>  abilities = new Dictionary<Follower.Abilities, int>();
            if ( !string.IsNullOrEmpty( row[ "野怪入侵" ].ToString() ) )
                abilities.Add( Follower.Abilities.WildAggression, Convert.ToInt16( row[ "野怪入侵" ] ) );
            if ( !string.IsNullOrEmpty( row[ "重击" ].ToString() ) )
                abilities.Add( Follower.Abilities.MassiveStrike, Convert.ToInt16( row[ "重击" ] ) );
            if ( !string.IsNullOrEmpty( row[ "群体伤害" ].ToString() ) )
                abilities.Add( Follower.Abilities.GroupDamage, Convert.ToInt16( row[ "群体伤害" ] ) );
            if ( !string.IsNullOrEmpty( row[ "魔法减益" ].ToString() ) )
                abilities.Add( Follower.Abilities.MagicDebuff, Convert.ToInt16( row[ "魔法减益" ] ) );
            if ( !string.IsNullOrEmpty( row[ "危险区域" ].ToString() ) )
                abilities.Add( Follower.Abilities.DangerZones, Convert.ToInt16( row[ "危险区域" ] ) );
            if ( !string.IsNullOrEmpty( row[ "爪牙围攻" ].ToString() ) )
                abilities.Add( Follower.Abilities.MinionSwarms, Convert.ToInt16( row[ "爪牙围攻" ] ) );
            if ( !string.IsNullOrEmpty( row[ "强力法术" ].ToString() ) )
                abilities.Add( Follower.Abilities.PowerfulSpell, Convert.ToInt16( row[ "强力法术" ] ) );
            if ( !string.IsNullOrEmpty( row[ "致命爪牙" ].ToString() ) )
                abilities.Add( Follower.Abilities.DeadlyMinions, Convert.ToInt16( row[ "致命爪牙" ] ) );
            if ( !string.IsNullOrEmpty( row[ "限时战斗" ].ToString() ) )
                abilities.Add( Follower.Abilities.TimedBattle, Convert.ToInt16( row[ "限时战斗" ] ) );

            Follower.Traits slayerNeed;
            switch ( row[ "任务场景" ].ToString() )
            {
                case "山地": slayerNeed = Follower.Traits.Mountaineer; break;
                case "雪地": slayerNeed = Follower.Traits.ColdBlooded; break;
                case "沙漠": slayerNeed = Follower.Traits.Wastelander; break;
                case "森林": slayerNeed = Follower.Traits.Naturalist; break;
                case "地下": slayerNeed = Follower.Traits.CaveDweller; break;
                case "丛林": slayerNeed = Follower.Traits.GuerillaFighter; break;
                case "沼泽": slayerNeed = Follower.Traits.Marshwalker; break;
                case "平原": slayerNeed = Follower.Traits.Plainsrunner; break;
                case "城镇": slayerNeed = Follower.Traits.Town; break;
                case "兽人": slayerNeed = Follower.Traits.Orcslayer; break;
                case "食人魔": slayerNeed = Follower.Traits.Ogreslayer; break;
                case "野兽": slayerNeed = Follower.Traits.Beastslayer; break;
                case "恶魔": slayerNeed = Follower.Traits.Demonslayer; break;
                case "鸦人": slayerNeed = Follower.Traits.Talonslayer; break;
                case "原兽": slayerNeed = Follower.Traits.Primalslayer; break;
                case "元素之怒": slayerNeed = Follower.Traits.Furyslayer; break;
                case "畸变怪":
                case "不死族":
                    slayerNeed = Follower.Traits.Voidslayer; break;
                case "毁灭者": slayerNeed = Follower.Traits.Gronnslayer; break;
                default:
                    slayerNeed = Follower.Traits.Unknow; break;
            }

            missions.Add( Convert.ToInt16( row[ "任务ID" ] ), new Mission( row[ "任务ID" ].ToString(), row[ "任务名" ].ToString(), row[ "任务中文名" ].ToString(), Convert.ToInt16( row[ "装等要求" ] ),
                Convert.ToInt16( row[ "随从数量" ] ), abilities, slayerNeed, row[ "任务时间" ].ToString(), row[ "奖励" ].ToString(), row[ "备注" ].ToString(), ( (float)Convert.ToInt16( row[ "基础成功率" ] ) ) / 100 ) );
        }
    }
}
