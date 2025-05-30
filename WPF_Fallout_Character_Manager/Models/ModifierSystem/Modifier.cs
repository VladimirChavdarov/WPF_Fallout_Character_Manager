using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem
{
    public sealed class Modifier
    {
        public Modifier()
        {
            Name = "NewModifier";
            Value = 0;
        }

        public string Name { get; set; }
        public int Value { get; set; }
    }
}
