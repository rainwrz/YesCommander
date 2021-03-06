﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace YesCommander.Classes
{
    public class Mission
    {
        public string MissionId;
        public string MissionName;
        public string MissionNameCN;

        public int FollowersNeed;
        public int TotalCounterAbilitiesNeed;
        public int ItemLevelNeed;
        public bool IsUsingMaxiLevel;
        public Follower.Traits SlayerNeed;
        public string MissionTimeStr;
        public float MisstionTimeNeed; //hours
        public float MissionTimeCaculated;
        public string MissionTimeCaculatedStr;
        public string MissionReward;
        public string Remark;
        public Dictionary<Follower.Abilities, int> CounterAbilitiesCollection;
        public List<Follower> AssignedFollowers;

        public double SucessPerAbility;
        public double SucessPerFollower;
        public double SucessPerRaceLover;
        public double SucessPerBurstStamCombatExpSlayer;
        public double SucessPerItemLevel;
        public double TotalSucessChance;
        public float BasicSucessChange;
        public List<Follower.Traits> partyBuffs;
        public static readonly int MAXITEMLEVEL = 655;

        public Mission( string missionId, string missionName, string missionNameCN, int itemLevelNeed, int followersNeed, Dictionary<Follower.Abilities, int> abilities, Follower.Traits slayerNeed, string time, string reward, string remark, float basicSucessChange =0, bool isUsingMaxiLevel = false )
        {
            this.MissionId = missionId;
            this.MissionName = missionName;
            this.MissionNameCN = missionNameCN;
            this.ItemLevelNeed = itemLevelNeed;
            this.FollowersNeed = followersNeed;
            this.CounterAbilitiesCollection = abilities;
            this.MissionReward = reward;
            this.Remark = remark;
            this.IsUsingMaxiLevel = isUsingMaxiLevel;
            this.BasicSucessChange = basicSucessChange;

            this.TotalCounterAbilitiesNeed = 0;
            foreach ( KeyValuePair<Follower.Abilities, int> pair in abilities )
                this.TotalCounterAbilitiesNeed += pair.Value;

            this.SlayerNeed = slayerNeed;
            if ( time.Contains( "\"" ) )
                time = time.Replace( "\"", "" );
            if ( time.Contains( "," ) )
                time = time.Replace( ",", "" );
            if ( time.Contains( " " ) )
                time = time.Replace( " ", "" );
            this.MissionTimeStr = time;
            if ( time.Contains( 'h' ) )
            {
                string[] tableHead = time.Split( 'h' );
                this.MisstionTimeNeed = Convert.ToInt16( tableHead[ 0 ] );
                if ( tableHead.Length > 1 && tableHead[ 1 ].Contains( 'm' ) )
                    this.MisstionTimeNeed += ( (float) (Convert.ToInt16( tableHead[ 1 ].Replace( "m", "" ) ) )) / 60;
            }
            else
                this.MisstionTimeNeed = ( (float)( Convert.ToInt16( time.Replace( "m", "" ) ) ) ) / 60;
            this.CalculateSucess();
        }

        public Mission Copy()
        {
            return new Mission( this.MissionId, this.MissionName, this.MissionNameCN, this.ItemLevelNeed, this.FollowersNeed, this.CounterAbilitiesCollection, this.SlayerNeed, this.MissionTimeStr, this.MissionReward, this.Remark, this.BasicSucessChange, this.IsUsingMaxiLevel );
        }

        private void CalculateSucess()
        {
            //int stringLength = ( 1 / ( (float)this.TotalCounterAbilitiesNeed + 1 ) ).ToString().Length;
            //stringLength = stringLength > 6 ? 6 : stringLength;
            //string stringValue = ( 1 / ( (float)this.TotalCounterAbilitiesNeed + 1 ) ).ToString().Substring( 0, stringLength );
            //this.SucessPerAbility = Convert.ToDouble( stringValue );
            this.SucessPerAbility = 1 / ( (float)this.TotalCounterAbilitiesNeed + 1 );
            this.SucessPerFollower = this.SucessPerAbility / this.FollowersNeed;
            this.SucessPerRaceLover = this.SucessPerAbility / 2;
            this.SucessPerBurstStamCombatExpSlayer = this.SucessPerFollower;
            this.SucessPerItemLevel = this.SucessPerFollower / 30; //max at this.SucessPerFollower/2
        }

        public void AssignFollowers( List<Follower> followers )
        {
            this.AssignedFollowers = followers;
            this.TotalSucessChance = this.CalculateFinalSucess() + this.BasicSucessChange;
        }

        private double CalculateFinalSucess()
        {
            double result = 0;
            this.partyBuffs = new List<Follower.Traits>();

            if ( this.AssignedFollowers.Count == this.FollowersNeed )
                result += this.FollowersNeed * this.SucessPerFollower;
            // Abilities
            Dictionary<Follower.Abilities, int> followerRemain = new Dictionary<Follower.Abilities, int>();
            foreach ( Follower follower in this.AssignedFollowers )
            {
                foreach ( Follower.Abilities ability in follower.AbilityCollection )
                {
                    if ( followerRemain.ContainsKey( ability ) )
                        followerRemain[ ability ] += 1;
                    else
                        followerRemain.Add( ability, 1 );
                }
            }
            foreach ( KeyValuePair<Follower.Abilities, int> pair in this.CounterAbilitiesCollection )
            {
                float dancerNumber = 0;
                if ( followerRemain.ContainsKey( pair.Key ) )
                {
                    if ( followerRemain[ pair.Key ] >= pair.Value )
                    {
                        result += this.SucessPerAbility * pair.Value;
                    }
                    else
                    {
                        result += this.SucessPerAbility * followerRemain[ pair.Key ];
                        if ( pair.Key == Follower.Abilities.DangerZones )
                        {
                            int required = pair.Value - followerRemain[ pair.Key ];
                            foreach ( Follower follower in this.AssignedFollowers )
                            {
                                if ( follower.TraitCollection.Contains( Follower.Traits.Dancer ) )
                                    dancerNumber++;
                            }
                            if ( dancerNumber > 0 )
                            {
                                dancerNumber = dancerNumber > required * 2 ? required * 2: dancerNumber;
                                result += ( dancerNumber / 2 ) * this.SucessPerAbility;
                            }
                        }
                    }
                }
                else if ( pair.Key == Follower.Abilities.DangerZones )
                {
                    int required = pair.Value;
                    foreach ( Follower follower in this.AssignedFollowers )
                    {
                        if ( follower.TraitCollection.Contains( Follower.Traits.Dancer ) )
                            dancerNumber++;
                    }
                    if ( dancerNumber > 0 )
                    {
                        dancerNumber = dancerNumber > required * 2 ? required * 2 : dancerNumber;
                        result += ( dancerNumber / 2 ) * this.SucessPerAbility;
                    }
                }
                for ( int i = 0; i < dancerNumber; i++ )
                {
                    this.partyBuffs.Add( Follower.Traits.Dancer );
                }
            }

            // Traits
            int numberOfHighStamina = 0;
            int numberOfBurstOfPower = 0;
            float factorOfEpicMount = 1;
            foreach ( Follower follower in this.AssignedFollowers )
            {
                if ( follower.TraitCollection.Contains( this.SlayerNeed ) )
                {
                    result += this.SucessPerBurstStamCombatExpSlayer;
                    this.partyBuffs.Add( this.SlayerNeed );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.CombatExperience ) )
                {
                    result += this.SucessPerBurstStamCombatExpSlayer;
                    this.partyBuffs.Add( Follower.Traits.CombatExperience );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.EpicMount ) )
                {
                    factorOfEpicMount *= 2;
                    this.partyBuffs.Add( Follower.Traits.EpicMount );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.HighStamina ) )
                    numberOfHighStamina++;
                if ( follower.TraitCollection.Contains( Follower.Traits.BurstOfPower ) )
                    numberOfBurstOfPower++;
            }

            float timeNeed = this.MisstionTimeNeed / factorOfEpicMount;
            this.MissionTimeCaculated = timeNeed;
            this.MissionTimeCaculatedStr = string.Empty;
            if ( Math.Truncate( timeNeed ) != 0 )
                this.MissionTimeCaculatedStr = Math.Truncate( timeNeed ).ToString() + "小时";
            double minute = Math.Round( ( timeNeed - Math.Truncate( timeNeed ) ) * 60, 0 );
            if( minute!=0)
                this.MissionTimeCaculatedStr +=minute.ToString() + "分钟";
            if ( timeNeed > 7 )
            {
                result += this.SucessPerBurstStamCombatExpSlayer * numberOfHighStamina;
                for ( int i = 0; i < numberOfHighStamina; i++ )
                    this.partyBuffs.Add( Follower.Traits.HighStamina );
            }
            else
            {
                result += this.SucessPerBurstStamCombatExpSlayer * numberOfBurstOfPower;
                for ( int i = 0; i < numberOfBurstOfPower; i++ )
                    this.partyBuffs.Add( Follower.Traits.BurstOfPower );
            }
            // RaceLover
            if ( this.FollowersNeed == 3 )
            {
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 1 ], this.AssignedFollowers[ 2 ] );
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 1 ], this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 2 ] );
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 2 ], this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 1 ] );
            }
            else if ( this.FollowersNeed == 2 )
            {
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 1 ] );
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 1 ], this.AssignedFollowers[ 0 ] );
            }
            if ( this.ItemLevelNeed > 0 )
            {
                foreach ( Follower follower in this.AssignedFollowers )
                {
                    int followerILevel = this.IsUsingMaxiLevel ? MAXITEMLEVEL : follower.ItemLevel;
                    int ilevelIncrease = followerILevel - this.ItemLevelNeed;
                    ilevelIncrease = ilevelIncrease > 15 ? 15 : ilevelIncrease;
                    if ( ilevelIncrease > 0 )
                        result += this.SucessPerItemLevel * ilevelIncrease;
                }
            }
            return result;
        }

        private double SingleTraitRaceMatching( Follower follower1, Follower follower2, Follower follower3 = null )
        {
            double result = 0;
            foreach ( Follower.Traits trait in Follower.FilteredRaceTrait( follower1 ) )
            {
                if ( Follower.GetRaceMatchedByTrait( trait ) == follower2.Race || ( follower3 != null && Follower.GetRaceMatchedByTrait( trait ) == follower3.Race ) )
                {
                    result += this.SucessPerRaceLover;
                    this.partyBuffs.Add( trait);
                }
            }
            return result;
        }


    }
}
