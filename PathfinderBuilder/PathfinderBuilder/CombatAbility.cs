using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderBuilder
{
    public class CombatAbility : FluffyEntity
    {
        public CombatAbility()
        {

        }

        public CombatAbility(CombatAbility ca)
        {
            Duration = ca.Duration;
            WeaponGroup = ca.WeaponGroup;
            ArmorGroup = ca.ArmorGroup;
            DamageBonus = ca.DamageBonus;
            AttackBonus = ca.AttackBonus;
            ExtraTurningPerDay = ca.ExtraTurningPerDay;
            FortBonus = ca.FortBonus;
            ReflexBonus = ca.ReflexBonus;
            WillBonus = ca.WillBonus;
            TempHpPerLevel = ca.TempHpPerLevel;
            BonusHp = ca.BonusHp;
            NaturalArmor = ca.NaturalArmor;
            BonusOnTwoWeaponFighting = ca.BonusOnTwoWeaponFighting;
            UseWisForRangedAttacks = ca.UseWisForRangedAttacks;
            UseDexForMeleeAttacks = ca.UseDexForMeleeAttacks;
            StrBonus = ca.StrBonus;
            DexBonus = ca.DexBonus;
            ConBonus = ca.ConBonus;
            IntBonus = ca.IntBonus;
            WisBonus = ca.WisBonus;
            ChaBonus = ca.ChaBonus;
            ArmorPenalty = ca.ArmorPenalty;
            IsDefensiveAbility = ca.IsDefensiveAbility;
            UnarmedDamage = ca.UnarmedDamage;
            EnergyResistance = ca.EnergyResistance;
            SkillBonuses = ca.SkillBonuses;
        }

        [Display(Name = "Duration")]
        public Duration Duration { get; set; }

        [Display(Name = "Weapon Group")]
        public WeaponGroup WeaponGroup { get; set; }

        [Display(Name = "Armor Group")]
        public ArmorGroup ArmorGroup { get; set; }

        [Display(Name = "Damage Bonus")]
        public int DamageBonus { get; set; }

        [Display(Name = "Attack Bonus")]
        public int AttackBonus { get; set; }

        [Display(Name = "Extra Turning per day")]
        public int ExtraTurningPerDay { get; set; }

        [Display(Name = "Fort Bonus")]
        public int FortBonus { get; set; }

        [Display(Name = "Ref Bonus")]
        public int ReflexBonus { get; set; }

        [Display(Name = "Will Bonus")]
        public int WillBonus { get; set; }

        [Display(Name = "Temp HP per level")]
        public int TempHpPerLevel { get; set; }

        [Display(Name = "Bonus HP")]
        public int BonusHp { get; set; }

        [Display(Name = "Natural Armor")]
        public int NaturalArmor { get; set; }

        [Display(Name = "Bonus to Two-Weapon Fighting")]
        public int BonusOnTwoWeaponFighting { get; set; }

        [Display(Name = "Zen ability")]
        public bool UseWisForRangedAttacks { get; set; }

        [Display(Name = "Finesse ability")]
        public bool UseDexForMeleeAttacks { get; set; }

        [Display(Name = "Strength Bonus")]
        public int StrBonus { get; set; }

        [Display(Name = "Dexterity Bonus")]
        public int DexBonus { get; set; }

        [Display(Name = "Constitution Bonus")]
        public int ConBonus { get; set; }

        [Display(Name = "Intelligence Bonus")]
        public int IntBonus { get; set; }

        [Display(Name = "Wisdom Bonus")]
        public int WisBonus { get; set; }

        [Display(Name = "Charisma Bonus")]
        public int ChaBonus { get; set; }

        [Display(Name = "AC Penalty")]
        public int ArmorPenalty { get; set; }

        /// <summary>
        /// Use this for abilities that are written on the AC line
        /// </summary>
        [Display(Name = "Defensive Ability")]
        public bool IsDefensiveAbility { get; set; }

        [Display(Name = "Unarmed Damage")]
        public Dice UnarmedDamage { get; set; }

        public ICollection<EnergyResistance> EnergyResistance { get; set; }

        [Display(Name = "Skill Bonuses")]
        public ICollection<SkillPointPair> SkillBonuses { get; set; }
    }
}
