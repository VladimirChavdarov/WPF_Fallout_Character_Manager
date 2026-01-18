using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.ModifierSystem.Serialization
{
    public class ModValueDTO<T>
    {
        public LabeledValue<T> BaseValueObject { get; set; }
        public bool IsBaseValueReadOnly { get; set; }
        public List<LabeledValue<T>> Modifiers { get; set; }
    }
}
