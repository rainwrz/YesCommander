using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YesCommander.Classes
{
    public class Follower
    {
        public string ID;
        public string Name;
        public string NameEN;
        public string NameCN;
        public string NameTCN;
        public int Quolaty;
        public int Level;
        public int ItemLevel;
        public Races Race;
        public Classes Class;
        public bool IsActive;
        public List<Abilities> AbilityCollection;
        public List<Traits> TraitCollection;
        public string ClassStr;

        public Follower(string ID, string name, int quolaty, int level, int itemLevel, string raceName, Classes classType, string classStr, int isActiveIndex, List<int> abilityIndexes, List<int> traitIndexes, string nameEN = null, string nameCN = null, string nameTCN = null )
        {
            this.ID = ID;
            this.Name = name;
            this.Quolaty = quolaty;
            this.Level = level;
            this.ItemLevel = itemLevel;
            this.Race = GetRaceByName( raceName );
            this.Class = classType;
            this.ClassStr = classStr;
            this.IsActive = isActiveIndex == 1 ? true : false;
            this.NameEN = nameEN;
            this.NameCN = nameCN;
            this.NameTCN = nameTCN;

            this.AbilityCollection = new List<Abilities>();
            if ( abilityIndexes.Count > 1 )
            {
                Abilities a1 = GetAbilityById( abilityIndexes[ 0 ] );
                Abilities a2 = GetAbilityById( abilityIndexes[ 1 ] );
                if ( Convert.ToInt16( a1 ) > Convert.ToInt16( a2 ) )
                {
                    this.AbilityCollection.Add( a2 );
                    this.AbilityCollection.Add( a1 );
                }
                else
                {
                    this.AbilityCollection.Add( a1 );
                    this.AbilityCollection.Add( a2 );
                }
            }
            else if ( abilityIndexes.Count == 1 )
                this.AbilityCollection.Add( GetAbilityById( abilityIndexes[ 0 ] ) );

            this.TraitCollection = new List<Traits>();
            foreach ( int j in traitIndexes )
                this.TraitCollection.Add( GetTratById( j ) );
        }

        public enum Abilities
        {
            DangerZones=0,
            DeadlyMinions,
            GroupDamage,
            MagicDebuff,
            MassiveStrike,
            MinionSwarms,
            PowerfulSpell,
            TimedBattle,
            WildAggression,
            Error
        }
        public static Abilities GetAbilityById( int id )
        {
            switch ( id )
            {
                case ( 1 ):
                    return Abilities.WildAggression;
                case ( 2 ):
                    return Abilities.MassiveStrike;
                case ( 3 ):
                    return Abilities.GroupDamage;
                case ( 4 ):
                    return Abilities.MagicDebuff;
                case ( 6 ):
                    return Abilities.DangerZones;
                case ( 7 ):
                    return Abilities.MinionSwarms;
                case ( 8 ):
                    return Abilities.PowerfulSpell;
                case ( 9 ):
                    return Abilities.DeadlyMinions;
                case ( 10 ):
                    return Abilities.TimedBattle;
                case ( 5 ):
                default: return Abilities.Error;
            }
        }
        public static Abilities GetAbilityFromStr( string abilityStr )
        {
            switch ( abilityStr )
            {
                case ( "Danger Zones" ):
                case ( "危险区域" ):
                case ( "危險區域" ):
                    return Abilities.DangerZones;
                case ( "Deadly Minions" ):
                    return Abilities.DeadlyMinions;
                case ( "Group Damage" ):
                    return Abilities.GroupDamage;
                case ( "Magic Debuff" ):
                    return Abilities.MagicDebuff;
                case ( "Massive Strike" ):
                    return Abilities.MassiveStrike;
                case ( "Minion Swarms" ):
                    return Abilities.MinionSwarms;
                case ( "Powerful Spell" ):
                    return Abilities.PowerfulSpell;
                case ( "Timed Battle" ):
                    return Abilities.TimedBattle;
                case ( "Wild Aggression" ):
                    return Abilities.WildAggression;
                default: return Abilities.Error;
            }
        }
        public static ImageSource GetImageFromAbility( Abilities ability )
        {
            switch ( ability )
            {
                case Abilities.DangerZones: return Follower.GetImageFromPicName( "spell_shaman_earthquake.jpg" );
                case Abilities.DeadlyMinions: return Follower.GetImageFromPicName( "achievement_boss_twinorcbrutes.jpg" );
                case Abilities.GroupDamage: return Follower.GetImageFromPicName( "spell_fire_selfdestruct.jpg" );
                case Abilities.MagicDebuff: return Follower.GetImageFromPicName( "spell_shadow_shadowwordpain.jpg" );
                case Abilities.MassiveStrike: return Follower.GetImageFromPicName( "ability_warrior_savageblow.jpg" );
                case Abilities.MinionSwarms: return Follower.GetImageFromPicName( "spell_deathknight_armyofthedead.jpg" );
                case Abilities.PowerfulSpell: return Follower.GetImageFromPicName( "spell_shadow_shadowbolt.jpg" );
                case Abilities.TimedBattle: return Follower.GetImageFromPicName( "spell_holy_borrowedtime.jpg" );
                case Abilities.WildAggression: return Follower.GetImageFromPicName( "spell_nature_reincarnation.jpg" );
                case Abilities.Error: 
                default: return null;
            }
        }
        public static ImageSource GetImageFromPicName( string picName )
        {
            BitmapImage bi = new BitmapImage( new Uri(
                "pack://application:,,,/YesCommander;component/Resources/" + picName,
                    UriKind.RelativeOrAbsolute ) );
            return bi;
        }
        public static List<Abilities> GetAbilityFromClass( Classes className )
        {
            List<Abilities> abilities = new List<Abilities>();
            switch ( className )
            {
                case Classes.FrostDeathKnight:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.TimedBattle, Abilities.WildAggression, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;
                case Classes.BloodDeathKnight:
                case Classes.UnHolyDeathKnight:
                    abilities.AddRange( new Abilities[] { Abilities.MassiveStrike, Abilities.TimedBattle, Abilities.WildAggression, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;
                    
                case Classes.FeralDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.TimedBattle, Abilities.WildAggression, Abilities.DangerZones, Abilities.MassiveStrike } );
                    break;
                case Classes.GuardianDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.WildAggression, Abilities.DangerZones, Abilities.MassiveStrike } );
                    break;
                case Classes.BalanceDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.MinionSwarms } );
                    break;
                case Classes.RestorationDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MagicDebuff, Abilities.GroupDamage, Abilities.MinionSwarms } );
                    break;

                case Classes.BeastMasterHunter:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.WildAggression, Abilities.MinionSwarms } );
                    break;
                case Classes.SurvivalHunter:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.MassiveStrike, Abilities.MinionSwarms } );
                    break;
                case Classes.MarksmanshipHunter:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;

                case Classes.ArcaneMage:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.TimedBattle, Abilities.PowerfulSpell, Abilities.DangerZones } );
                    break;
                case Classes.FireMage:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;
                case Classes.FrostMage:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MassiveStrike, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;

                case Classes.BrewmasterMonk:
                    abilities.AddRange( new Abilities[] { Abilities.DangerZones, Abilities.DeadlyMinions, Abilities.WildAggression, Abilities.MassiveStrike, Abilities.GroupDamage } );
                    break;
                case Classes.WindwalkerMonk:
                    abilities.AddRange( new Abilities[] { Abilities.DangerZones, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.WildAggression, Abilities.PowerfulSpell } );
                    break;
                case Classes.MistweaverMonk:
                    abilities.AddRange( new Abilities[] { Abilities.DangerZones, Abilities.TimedBattle, Abilities.PowerfulSpell, Abilities.MagicDebuff, Abilities.GroupDamage } );
                    break;
                    
                case Classes.RetributionPaladin:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.MassiveStrike, Abilities.MinionSwarms } );
                    break;
                case Classes.ProtectionPaladin:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.PowerfulSpell, Abilities.WildAggression, Abilities.MassiveStrike, Abilities.MagicDebuff } );
                    break;
                case Classes.HolyPaladin:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.GroupDamage, Abilities.MagicDebuff } );
                    break;

                case Classes.DisciplinePriest:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.DangerZones, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.GroupDamage } );
                    break;
                case Classes.HolyPriest:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.DangerZones, Abilities.TimedBattle, Abilities.GroupDamage, Abilities.MinionSwarms } );
                    break;
                case Classes.ShadowPriest:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.DangerZones, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MinionSwarms } );
                    break;

                case Classes.AssassinationRogue:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MassiveStrike, Abilities.TimedBattle } );
                    break;
                case Classes.CombatRogue:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MinionSwarms, Abilities.TimedBattle } );
                    break;
                case Classes.SubtletyRogue:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MassiveStrike, Abilities.MinionSwarms } );
                    break;
                    
                case Classes.ElementalShaman:
                    abilities.AddRange( new Abilities[] { Abilities.GroupDamage, Abilities.MinionSwarms, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.PowerfulSpell } );
                    break;
                case Classes.EnhancementShaman:
                    abilities.AddRange( new Abilities[] { Abilities.GroupDamage, Abilities.MinionSwarms, Abilities.TimedBattle, Abilities.DangerZones, Abilities.DeadlyMinions } );
                    break;
                case Classes.RestorationShaman:
                    abilities.AddRange( new Abilities[] { Abilities.GroupDamage, Abilities.MinionSwarms, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.MagicDebuff } );
                    break;

                case Classes.AfflictionWarlock:
                    abilities.AddRange( new Abilities[] { Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MinionSwarms, Abilities.MagicDebuff } );
                    break;
                case Classes.DemonologyWarlock:
                    abilities.AddRange( new Abilities[] { Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.TimedBattle, Abilities.MinionSwarms, Abilities.MassiveStrike } );
                    break;
                case Classes.DestructionWarlock:
                    abilities.AddRange( new Abilities[] { Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.GroupDamage, Abilities.DeadlyMinions, Abilities.TimedBattle } );
                    break;


                case Classes.ArmsWarrior:
                    abilities.AddRange( new Abilities[] { Abilities.MinionSwarms, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.WildAggression } );
                    break;
                case Classes.FuryWarrior:
                    abilities.AddRange( new Abilities[] { Abilities.MinionSwarms, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.MassiveStrike } );
                    break;
                case Classes.ProtectionWarrior:
                    abilities.AddRange( new Abilities[] { Abilities.MinionSwarms, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MassiveStrike, Abilities.WildAggression } );
                    break;

                default:
                    break;

            }
            return abilities;
        } 

        public enum Races
        {
            熊猫人=0,

            人类,
            侏儒,
            矮人,
            暗夜精灵,
            德莱尼,
            狼人,

            被遗忘者,
            牛头人,
            兽人,
            巨魔,
            血精灵,
            地精, //12

            埃匹希斯守卫,
            高等鸦人,
            鸦人流亡者,
            木精,
            豺狼人,
            机械,
            独眼魔,
            食人魔,
            锦鱼人,
            刃牙虎人,
            猢狲,

            error
        }
        public static Races GetRaceByName( string name )
        {
            Type race = typeof( Races );
            foreach ( var ra in Enum.GetValues( race ) )
            {
                if ( name == ra.ToString() )
                    return (Races)ra;
            }
            return Races.error;
        }

        public enum Traits
        {
            Unknow =-1,

            FastLearner,
            HighStamina,
            BurstOfPower,
            CombatExperience, //3

            Orcslayer, //4
            Ogreslayer,
            Beastslayer,
            Gronnslayer,
            Furyslayer,
            Primalslayer,
            Voidslayer,
            Talonslayer,
            Demonslayer,  //12

            GnomeLover, //13
            Humanist,
            Dwarvenborn,
            ChildOfTheMoon,
            AllyOfArgus,
            CanineCompanion,
            BrewAficionado,
            ChildOfDraenon,
            DeathFascination,
            Totemist,
            VoodooZealot,
            Elvenkind,
            Economist, //25

            LoneWolf,

            Naturalist, //27
            CaveDweller,
            Wastelander,
            Marshwalker,
            Mountaineer,
            ColdBlooded,
            GuerillaFighter,
            Plainsrunner, //34

            Town,

            Dancer, // 36
            EpicMount,//37

            Bodyguard, //38
            HearthstonePro,//39
            Scavenger,
            ExtraTraining, 
            Mining,//42
            Herbalism,
            Alchemy,
            Blacksmithing,
            Enchanting,
            Engineering,
            Inscription,
            Jewelcrafting,
            Leatherworking,
            Tailoring,
            Skinning,//52
            Evergreen,
            Angler
        }
        public static Traits GetTratById( int id )
        {
            switch ( id )
            {
                case 4: return Traits.Orcslayer;
                case 7: return Traits.Mountaineer;
                case 8: return Traits.ColdBlooded;
                case 9: return Traits.Wastelander;
                case 29: return Traits.FastLearner;
                case 36: return Traits.Demonslayer;
                case 37: return Traits.Beastslayer;
                case 38: return Traits.Ogreslayer;
                case 39: return Traits.Primalslayer;
                case 40: return Traits.Gronnslayer;
                case 41: return Traits.Furyslayer;
                case 42: return Traits.Voidslayer;
                case 43: return Traits.Talonslayer;
                case 44: return Traits.Naturalist;
                case 45: return Traits.CaveDweller;
                case 46: return Traits.GuerillaFighter;
                case 47: return Traits.Town;
                case 48: return Traits.Marshwalker;
                case 49: return Traits.Plainsrunner;

                case 52: return Traits.Mining;
                case 53: return Traits.Herbalism;
                case 54: return Traits.Alchemy;
                case 55: return Traits.Blacksmithing;
                case 56: return Traits.Enchanting;
                case 57: return Traits.Engineering;
                case 58: return Traits.Inscription;
                case 59: return Traits.Jewelcrafting;
                case 60: return Traits.Leatherworking;
                case 61: return Traits.Tailoring;
                case 62: return Traits.Skinning;

                case 63: return Traits.GnomeLover;
                case 64: return Traits.Humanist;
                case 65: return Traits.Dwarvenborn;
                case 66: return Traits.ChildOfTheMoon;
                case 67: return Traits.AllyOfArgus;
                case 68: return Traits.CanineCompanion;
                case 69: return Traits.BrewAficionado;
                case 70: return Traits.ChildOfDraenon;
                case 71: return Traits.DeathFascination;
                case 72: return Traits.Totemist;
                case 73: return Traits.VoodooZealot;
                case 74: return Traits.Elvenkind;
                case 75: return Traits.Economist;

                case 76: return Traits.HighStamina;
                case 77: return Traits.BurstOfPower;
                case 78: return Traits.LoneWolf;
                case 79: return Traits.Scavenger;
                case 80: return Traits.ExtraTraining;
                case 201: return Traits.CombatExperience;
                case 221: return Traits.EpicMount;
                case 227: return Traits.Angler;
                case 228: return Traits.Evergreen;
                case 231: return Traits.Bodyguard;
                case 232: return Traits.Dancer;
                case 236: return Traits.HearthstonePro;

                default: return Traits.Unknow;
            }
        }

        public static Traits GetTratByString( string trait )
        {
            switch ( trait )
            {
                case "Orcslayer": return Traits.Orcslayer;
                case "Mountaineer": return Traits.Mountaineer;
                case "ColdBlooded": return Traits.ColdBlooded;
                case "Wastelander": return Traits.Wastelander;
                case "FastLearner": return Traits.FastLearner;
                case "Demonslayer": return Traits.Demonslayer;
                case "Beastslayer": return Traits.Beastslayer;
                case "Ogreslayer": return Traits.Ogreslayer;
                case "Primalslayer": return Traits.Primalslayer;
                case "Gronnslayer": return Traits.Gronnslayer;
                case "Furyslayer": return Traits.Furyslayer;
                case "Voidslayer;": return Traits.Voidslayer;
                case "Talonslayer": return Traits.Talonslayer;
                case "Naturalist": return Traits.Naturalist;
                case "CaveDweller": return Traits.CaveDweller;
                case "GuerillaFighter": return Traits.GuerillaFighter;
                case "Town": return Traits.Town;
                case "Marshwalker": return Traits.Marshwalker;
                case "Plainsrunner": return Traits.Plainsrunner;

                case "Mining": return Traits.Mining;
                case "Herbalism": return Traits.Herbalism;
                case "Alchemy": return Traits.Alchemy;
                case "Blacksmithing": return Traits.Blacksmithing;
                case "Enchanting": return Traits.Enchanting;
                case "Engineering": return Traits.Engineering;
                case "Inscription": return Traits.Inscription;
                case "Jewelcrafting": return Traits.Jewelcrafting;
                case "Leatherworking": return Traits.Leatherworking;
                case "Tailoring": return Traits.Tailoring;
                case "Skinning": return Traits.Skinning;

                case "GnomeLover": return Traits.GnomeLover;
                case "Humanist": return Traits.Humanist;
                case "Dwarvenborn": return Traits.Dwarvenborn;
                case "ChildOfTheMoon": return Traits.ChildOfTheMoon;
                case "AllyOfArgus": return Traits.AllyOfArgus;
                case "CanineCompanion": return Traits.CanineCompanion;
                case "BrewAficionado": return Traits.BrewAficionado;
                case "ChildOfDraenon": return Traits.ChildOfDraenon;
                case "DeathFascination": return Traits.DeathFascination;
                case "Totemist": return Traits.Totemist;
                case "VoodooZealot": return Traits.VoodooZealot;
                case "Elvenkind": return Traits.Elvenkind;
                case "Economist": return Traits.Economist;

                case "HighStamina": return Traits.HighStamina;
                case "BurstOfPower": return Traits.BurstOfPower;
                case "LoneWolf": return Traits.LoneWolf;
                case "Scavenger": return Traits.Scavenger;
                case "ExtraTraining": return Traits.ExtraTraining;
                case "CombatExperience": return Traits.CombatExperience;
                case "EpicMount": return Traits.EpicMount;
                case "Angler": return Traits.Angler;
                case "Evergreen": return Traits.Evergreen;
                case "Bodyguard": return Traits.Bodyguard;
                case "Dancer": return Traits.Dancer;
                case "HearthstonePro": return Traits.HearthstonePro;

                default: return Traits.Unknow;
            }
        }

        public static List<Traits> FilteredRaceTrait( Follower follower )
        {
            List<Traits> result = new List<Traits>();
            foreach ( Traits trait in follower.TraitCollection )
            {
                if ( Follower.IsRaceLoverTrait( trait ) )
                    result.Add( trait );
            }
            return result;
        }
        public static bool IsRaceLoverTrait( Traits trait )
        {
            if ( trait == Traits.Humanist ||
                    trait == Traits.GnomeLover ||
                    trait == Traits.Humanist ||
                    trait == Traits.Dwarvenborn ||
                    trait == Traits.ChildOfTheMoon ||
                    trait == Traits.AllyOfArgus ||
                    trait == Traits.CanineCompanion ||
                    trait == Traits.BrewAficionado ||
                    trait == Traits.ChildOfDraenon ||
                    trait == Traits.DeathFascination ||
                    trait == Traits.Totemist ||
                    trait == Traits.VoodooZealot ||
                    trait == Traits.Elvenkind ||
                    trait == Traits.Economist )
                return true;
            else return false;
        }
        public static Races GetRaceMatchedByTrait( Traits trait )
        {
            switch ( trait )
            {
                case Traits.GnomeLover: return Races.侏儒;
                case Traits.Humanist: return Races.人类;
                case Traits.Dwarvenborn: return Races.矮人;
                case Traits.ChildOfTheMoon: return Races.暗夜精灵;
                case Traits.AllyOfArgus: return Races.德莱尼;
                case Traits.CanineCompanion: return Races.狼人;

                case Traits.BrewAficionado: return Races.熊猫人;

                case Traits.ChildOfDraenon: return Races.兽人;
                case Traits.DeathFascination: return Races.被遗忘者;
                case Traits.Totemist: return Races.牛头人;
                case Traits.VoodooZealot: return Races.巨魔;
                case Traits.Elvenkind: return Races.血精灵;
                case Traits.Economist: return Races.地精;
                default: return Races.error;
            }
        }
        public static ImageSource GetImageFromFromTrait( Traits trait )
        {
            if ( trait == Traits.Unknow )
                return null;
            BitmapImage bi = new BitmapImage( new Uri(
                "pack://application:,,,/YesCommander;component/Resources/" + trait.ToString()+".jpg",
                    UriKind.RelativeOrAbsolute ) );
            return bi;
        }

        public enum Classes
        {
            Unknown=-1,

            BloodDeathKnight=0,
            FrostDeathKnight,
            UnHolyDeathKnight,
            BalanceDruid,
            FeralDruid,
            GuardianDruid,
            RestorationDruid,
            BeastMasterHunter,
            MarksmanshipHunter,
            SurvivalHunter,
            ArcaneMage,
            FireMage,
            FrostMage,
            BrewmasterMonk,
            MistweaverMonk,
            WindwalkerMonk,
            HolyPaladin,
            ProtectionPaladin,
            RetributionPaladin,
            DisciplinePriest,
            HolyPriest,
            ShadowPriest,
            AssassinationRogue,
            CombatRogue,
            SubtletyRogue,
            ElementalShaman,
            EnhancementShaman,
            RestorationShaman,
            AfflictionWarlock,
            DemonologyWarlock,
            DestructionWarlock,
            ArmsWarrior,
            FuryWarrior,
            ProtectionWarrior
        }
        public static Classes GetClassBySpec( int spec )
        {
            switch ( spec )
            {
                case 2: return Classes.BloodDeathKnight;
                case 3: return Classes.FrostDeathKnight;
                case 4: return  Classes.UnHolyDeathKnight;
                case 5: return Classes.BalanceDruid;

                case 7: return Classes.FeralDruid;
                case 8: return Classes.GuardianDruid;
                case 9: return Classes.RestorationDruid;
                case 10: return Classes.BeastMasterHunter;

                case 12: return Classes.MarksmanshipHunter;
                case 13: return Classes.SurvivalHunter;
                case 14: return Classes.ArcaneMage;
                case 15: return Classes.FireMage;
                case 16: return Classes.FrostMage;
                case 17: return Classes.BrewmasterMonk;
                case 18: return Classes.MistweaverMonk;
                case 19: return Classes.WindwalkerMonk;
                case 20: return Classes.HolyPaladin;
                case 21: return Classes.ProtectionPaladin;
                case 22: return Classes.RetributionPaladin;
                case 23: return Classes.DisciplinePriest;
                case 24: return Classes.HolyPriest;
                case 25: return Classes.ShadowPriest;
                case 26: return Classes.AssassinationRogue;
                case 27: return Classes.CombatRogue;
                case 28: return Classes.SubtletyRogue;
                case 29: return Classes.ElementalShaman;
                case 30: return Classes.EnhancementShaman;
                case 31: return Classes.RestorationShaman;
                case 32: return Classes.AfflictionWarlock;
                case 33: return Classes.DemonologyWarlock;
                case 34: return Classes.DestructionWarlock;
                case 35: return Classes.ArmsWarrior;

                case 37: return Classes.FuryWarrior;
                case 38: return Classes.ProtectionWarrior;
                default: return Classes.Unknown;
            }
        }
        public static Classes GetClassByStr( string classStr, string specStr )
        {
            switch ( classStr )
            {
                case "死亡骑士":
                    switch ( specStr )
                    {
                        case "鲜血": return Classes.BloodDeathKnight;
                        case "冰霜": return Classes.FrostDeathKnight;
                        case "邪恶":
                        default: return Classes.UnHolyDeathKnight;
                    }
                case "德鲁伊":
                    switch ( specStr )
                    {
                        case "平衡": return Classes.BalanceDruid;
                        case "野性": return Classes.FeralDruid;
                        case "恢复": return Classes.RestorationDruid;
                        case "守护":
                        default: return Classes.GuardianDruid;
                    }
                case "战士":
                    switch ( specStr )
                    {
                        case "武器": return Classes.ArmsWarrior;
                        case "狂暴": return Classes.FuryWarrior;
                        case "防护":
                        default: return Classes.ProtectionWarrior;
                    }
                case "术士":
                    switch ( specStr )
                    {
                        case "恶魔": return Classes.DemonologyWarlock;
                        case "痛苦": return Classes.AfflictionWarlock;
                        case "毁灭":
                        default: return Classes.DestructionWarlock;
                    }
                case "武僧":
                    switch ( specStr )
                    {
                        case "织雾": return Classes.MistweaverMonk;
                        case "酿酒": return Classes.BrewmasterMonk;
                        case "踏风":
                        default: return Classes.WindwalkerMonk;
                    }
                case "法师":
                    switch ( specStr )
                    {
                        case "奥术": return Classes.ArcaneMage;
                        case "火焰": return Classes.FireMage;
                        case "冰霜":
                        default: return Classes.FrostMage;
                    }
                case "牧师":
                    switch ( specStr )
                    {
                        case "神圣": return Classes.HolyPriest;
                        case "戒律": return Classes.DisciplinePriest;
                        case "暗影":
                        default: return Classes.ShadowPriest;
                    }
                case "猎人":
                    switch ( specStr )
                    {
                        case "兽王": return Classes.BeastMasterHunter;
                        case "生存": return Classes.SurvivalHunter;
                        case "射击":
                        default: return Classes.MarksmanshipHunter;
                    }
                case "盗贼":
                case "潜行者":
                    switch ( specStr )
                    {
                        case "刺杀": return Classes.AssassinationRogue;
                        case "战斗": return Classes.CombatRogue;
                        case "敏锐":
                        default: return Classes.SubtletyRogue;
                    }
                case "萨满":
                    switch ( specStr )
                    {
                        case "元素": return Classes.ElementalShaman;
                        case "增强": return Classes.EnhancementShaman;
                        case "恢复":
                        default: return Classes.RestorationShaman;
                    }
                case "骑士":
                    switch ( specStr )
                    {
                        case "惩戒": return Classes.RetributionPaladin;
                        case "防护": return Classes.ProtectionPaladin;
                        case "神圣":
                        default: return Classes.HolyPaladin;
                    }
                default: return Classes.Unknown;
            }
        }
        public static Classes GetClassBySingleStr( string str )
        {
            switch ( str )
            {
                case "死亡骑士-鲜血": return Classes.BloodDeathKnight;
                case "死亡骑士-冰霜": return Classes.FrostDeathKnight;
                case "死亡骑士-邪恶": return Classes.UnHolyDeathKnight;
                case "德鲁伊-平衡": return Classes.BalanceDruid;
                case "德鲁伊-野性": return Classes.FeralDruid;
                case "德鲁伊-恢复": return Classes.RestorationDruid;
                case "德鲁伊-守护": return Classes.GuardianDruid;
                case "战士-武器": return Classes.ArmsWarrior;
                case "战士-狂暴": return Classes.FuryWarrior;
                case "战士-防护": return Classes.ProtectionWarrior;
                case "术士-恶魔": return Classes.DemonologyWarlock;
                case "术士-痛苦": return Classes.AfflictionWarlock;
                case "术士-毁灭": return Classes.DestructionWarlock;
                case "武僧-织雾": return Classes.MistweaverMonk;
                case "武僧-酿酒": return Classes.BrewmasterMonk;
                case "武僧-踏风": return Classes.WindwalkerMonk;
                case "法师-奥术": return Classes.ArcaneMage;
                case "法师-火焰": return Classes.FireMage;
                case "法师-冰霜": return Classes.FrostMage;
                case "牧师-神圣": return Classes.HolyPriest;
                case "牧师-戒律": return Classes.DisciplinePriest;
                case "牧师-暗影": return Classes.ShadowPriest;
                case "猎人-兽王": return Classes.BeastMasterHunter;
                case "猎人-生存": return Classes.SurvivalHunter;
                case "猎人-射击": return Classes.MarksmanshipHunter;
                case "潜行者-刺杀":
                case "盗贼-刺杀": return Classes.AssassinationRogue;
                case "潜行者-战斗":
                case "盗贼-战斗": return Classes.CombatRogue;
                case "潜行者-敏锐":
                case "盗贼-敏锐": return Classes.SubtletyRogue;
                case "萨满-元素": return Classes.ElementalShaman;
                case "萨满-增强": return Classes.EnhancementShaman;
                case "萨满-恢复": return Classes.RestorationShaman;
                case "骑士-惩戒": return Classes.RetributionPaladin;
                case "骑士-防护": return Classes.ProtectionPaladin;
                case "骑士-神圣": return Classes.HolyPaladin;
                default: return Classes.Unknown;
            }
        }

        public static string GetCNStringByClass( Classes c )
        {
            switch ( c )
            {
                case Classes.BloodDeathKnight: return "死亡骑士-鲜血";
                case Classes.FrostDeathKnight: return "死亡骑士-冰霜";
                case Classes.UnHolyDeathKnight: return "死亡骑士-邪恶";
                case Classes.BalanceDruid: return "德鲁伊-平衡";
                case Classes.FeralDruid: return "德鲁伊-野性";
                case Classes.RestorationDruid: return "德鲁伊-恢复";
                case Classes.GuardianDruid: return "德鲁伊-守护";
                case Classes.ArmsWarrior: return "战士-武器";
                case Classes.FuryWarrior: return "战士-狂暴";
                case Classes.ProtectionWarrior: return "战士-防护";
                case Classes.DemonologyWarlock: return "术士-恶魔";
                case Classes.AfflictionWarlock: return "术士-痛苦";
                case Classes.DestructionWarlock: return "术士-毁灭";
                case Classes.MistweaverMonk: return "武僧-织雾";
                case Classes.BrewmasterMonk: return "武僧-酿酒";
                case Classes.WindwalkerMonk: return "武僧-踏风";
                case Classes.ArcaneMage: return "法师-奥术";
                case Classes.FireMage: return "法师-火焰";
                case Classes.FrostMage: return "法师-冰霜";
                case Classes.HolyPriest: return "牧师-神圣";
                case Classes.DisciplinePriest: return "牧师-戒律";
                case Classes.ShadowPriest: return "牧师-暗影";
                case Classes.BeastMasterHunter: return "猎人-兽王";
                case Classes.SurvivalHunter: return "猎人-生存";
                case Classes.MarksmanshipHunter: return "猎人-射击";
                case Classes.AssassinationRogue: return "潜行者-刺杀";
                case Classes.CombatRogue: return "潜行者-战斗";
                case Classes.SubtletyRogue: return "潜行者-敏锐";
                case Classes.ElementalShaman: return "萨满-元素";
                case Classes.EnhancementShaman: return "萨满-增强";
                case Classes.RestorationShaman: return "萨满-恢复";
                case Classes.RetributionPaladin: return "骑士-惩戒";
                case Classes.ProtectionPaladin: return "骑士-防护";
                case Classes.HolyPaladin: return "骑士-神圣";
                default: return Classes.Unknown.ToString();
            }
        }
    }
}
