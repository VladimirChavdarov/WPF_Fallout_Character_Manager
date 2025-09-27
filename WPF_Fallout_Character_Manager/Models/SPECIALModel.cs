using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.ModifierSystem;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    public enum SPECIAL
    {
        Strength,
        Perception,
        Endurance,
        Charisma,
        Intelligence,
        Agility,
        Luck
    }

    internal sealed class SPECIALModel : ModelBase
    {
        // constructor
        public SPECIALModel()
        {
            _special = new Dictionary<SPECIAL, ModInt>
            {
                { SPECIAL.Strength,     new ModInt("Strength", 1, false, "Raw physical power. A high Strength increases your hit and damage with melee weapons and unarmed attacks. It’s also a great scare tactic against anyone. But most importantly, Strength measures your ability to handle any weapon and carry your equipment across the wasteland. All weapons have a Strength requirement, and if you fail to meet it you’ll have disadvantage to all your attack rolls with the weapon. If you have a low Strength, opt for lighter weapons. Having a low Strength will also significantly reduce your Carry Load, but a high one will allow you to carry lots of stuff.") },
                { SPECIAL.Perception,   new ModInt("Perception", 2, false, "Your senses, your insight into others, your ability to notice, and your attention to detail. Perception is also a measurement of how quick you can act under pressure. You’ll act first in combat with a high perception, and heal others easier when you know what’s important first. Damage and attack rolls with Energy Weapons are measured by Perception, as with their auto-aiming and wide beam-like capabilities, you only need to know where your target is to hit them. Perception also affects your range with all ranged weapons. A high Perception can allow you to shoot far, while a low one will decrease your range.") },
                { SPECIAL.Endurance,    new ModInt("Endurance", 3, false, "Your physical toughness, your ability to hold out. Characters with high endurance often never give up. Gritting their teeth with their power of determination to survive. This allows you to know more about the hardships of the wasteland and how to use them to your best abilities. A high Endurance gives you more hit points allowing you to survive longer. Your Endurance also affects your Healing Rate and Radiation DC. Having a low Endurance will make you more susceptible to the negative effects of radiation and even gets you closer to death in the wasteland.") },
                { SPECIAL.Charisma,     new ModInt("Charisma", 4, false, "Your force of personality, everything that makes you, you. Charisma measures your ability to understand others, while also being a master of communication. Charisma makes for great leaders and powerful negotiators. A high Charisma also increases your Party Nerve, which increases your party’s perseverance by gaining temporary stamina points and a bonus to Death Saves.") },
                { SPECIAL.Intelligence, new ModInt("Intelligence", 5, false, "Reasoning, knowledge, and memory. Intelligence measures sheer brain power and your know-how of everything. Computers, biology, cosmology, physics, equations, mechanics, engineering, and anything Scientific related is measured by Intelligence. A high Intelligence score will net you more skill points when you level up, while a lower Intelligence gives you less. A high Intelligence also allows you to craft more items like weapons, mods, and armor upgrades.") },
                { SPECIAL.Agility,      new ModInt("Agility", 6, false, "Quickness and dexterity. Agility measures your character’s speed and accuracy. Your Agility also measures your ability to dodge and stay focused in combat, granting you your SP. Your ability to move quietly and act inconspicuously is measured by Agility. Attack and damage rolls with pistols, shotguns, rifles, big guns, and SMGs are all modified by your Agility. Having a high Agility score grants you more AP, allowing you to do more in a turn during combat, while a low Agility will give you less.") },
                { SPECIAL.Luck,         new ModInt("Luck", 7, false, "Fate, Karma, and Fortune. It’s difficult to measure one’s Luck, but in the wasteland the universe itself judges individuals by their Luck. A general catch-all skill, your Luck can affect almost everything. Coming back from the dead relies on your Luck as you add your modifier to Death Saves. Finding loot, equipment, or anything in the wasteland is measured by your Luck. Having a high Luck score grants a bonus to all your Skills, and raises your chance to critically hit on all weapons.") }

            };

            foreach(var keyValue in _special)
            {
                var key = keyValue.Key;
                var value = keyValue.Value;

                value.PropertyChanged += (s, e) => OnPropertyChanged(key.ToString());
            }
        }
        //

        // helpers
        public int GetModifier(SPECIAL MainStat)
        {
            return _special[MainStat].Total - 5;
        }

        /// <summary>
        /// IMPORTANT: This function follows the rules of the TTRPG. It takes the Luck Modifier and halfs it, rounded down. If the modifier is negative, it clamps it to -1.
        /// </summary>
        /// <returns></returns>
        public int GetClampedHalfLuckModifier()
        {
            int luckModifier = Math.Clamp(GetModifier(SPECIAL.Luck), -1, 2147483647);
            if (luckModifier >= 0)
                return luckModifier /= 2;
            return -1;
        }
        //

        // Data
        private Dictionary<SPECIAL, ModInt> _special;

        public ModInt Strength
        {
            get => _special[SPECIAL.Strength];
            set => Update(_special[SPECIAL.Strength], value, v => _special[SPECIAL.Strength] = v);
        }

        public ModInt Perception
        {
            get => _special[SPECIAL.Perception];
            set => Update(_special[SPECIAL.Perception], value, v => _special[SPECIAL.Perception] = v);
        }

        public ModInt Endurance
        {
            get => _special[SPECIAL.Endurance];
            set => Update(_special[SPECIAL.Endurance], value, v => _special[SPECIAL.Endurance] = v);
        }

        public ModInt Charisma
        {
            get => _special[SPECIAL.Charisma];
            set => Update(_special[SPECIAL.Charisma], value, v => _special[SPECIAL.Charisma] = v);
        }

        public ModInt Intelligence
        {
            get => _special[SPECIAL.Intelligence];
            set => Update(_special[SPECIAL.Intelligence], value, v => _special[SPECIAL.Intelligence] = v);
        }

        public ModInt Agility
        {
            get => _special[SPECIAL.Agility];
            set => Update(_special[SPECIAL.Agility], value, v => _special[SPECIAL.Agility] = v);
        }

        public ModInt Luck
        {
            get => _special[SPECIAL.Luck];
            set => Update(_special[SPECIAL.Luck], value, v => _special[SPECIAL.Luck] = v);
        }
        //
    }
}
