using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder
{
    public class Spell : FluffyEntity
    {
        public Spell()
        {
            School = MagicSchool.None;
        }

        [Display(Name = "Range")]
        public string Range { get; set; }

        [Display(Name = "Target")]
        public string Target { get; set; }

        [Display(Name = "Has Spell Resistance?")]
        public bool SpellResistance { get; set; }

        [Display(Name = "Half Damage Reflex")]
        public bool HalfDamageReflex { get; set; }

        [Display(Name = "Half Damage Fortitude")]
        public bool HalfDamageFort { get; set; }

        [Display(Name = "Will Save")]
        public bool WillSave { get; set; }

        [Display(Name = "Reflex Save")]
        public bool ReflexSave { get; set; }

        [Display(Name = "Fortitude Save")]
        public bool FortSave { get; set; }

        [Display(Name = "Duration")]
        public Duration Duration { get; set; }

        [Display(Name = "Kill Spell")]
        public bool KillSpell { get; set; }

        [Display(Name = "Max HD for effect")]
        [Range(0, 100)]
        public int MaxHitDice { get; set; }

        [Display(Name = "School")]
        public MagicSchool School { get; set; }

        [Display(Name = "Support Mark (0-100)")]
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100")]
        public int SupportMark { get; set; }

        [Display(Name = "Absolute Damange Mark (0-100)")]
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100")]
        public int DamageMark { get; set; }

        [Display(Name = "Area Damage Mark (0-100)")]
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100")]
        public int AoEMark { get; set; }

        [Display(Name = "Adventuring Mark (0-100)")]
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100")]
        public int AdventureMark { get; set; }

        [Display(Name = "Buff Mark (0-100)")]
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100")]
        public int BuffMark { get; set; }

        [Display(Name = "Plot Device-ioum Mark (0-100)")]
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100")]
        public int PlotMark { get; set; }

        [Display(Name = "Components")]
        public virtual ICollection<SpellComponents> Components { get; set; }

        [Display(Name = "Levels")]
        public virtual ICollection<Tuple<string,int>> Levels { get; set; }

        [Display(Name = "Damage")]
        public virtual ICollection<DieAmountPair> Damage { get; set; }

        [Display(Name = "Healing")]
        public virtual ICollection<DieAmountPair> Healing { get; set; }

        [Display(Name = "Friendly Buffs")]
        public virtual ICollection<CombatAbility> Buffs { get; set; }

        [Display(Name = "Hostile Debuffs")]
        public virtual ICollection<CombatAbility> Debuffs { get; set; }
    }
}
