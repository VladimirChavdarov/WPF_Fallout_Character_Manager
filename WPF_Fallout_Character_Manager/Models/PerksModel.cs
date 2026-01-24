using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;

namespace WPF_Fallout_Character_Manager.Models
{
    class PerksModel : ModelBase, ISerializable<PerksModelDTO>
    {
        public PerksModel()
        {
            Traits = new ObservableCollection<Trait>();
            Perks = new ObservableCollection<Perk>();
        }

        public ObservableCollection<Trait> Traits { get; set; }
        public ObservableCollection<Perk> Perks { get; set; }

        // methods
        public void FromDto(PerksModelDTO dto, bool versionMismatch = false)
        {
            Traits.Clear();
            Perks.Clear();

            foreach(var dtoTrait in dto.Traits)
            {
                Traits.Add(new Trait(dtoTrait));
            }
            foreach(var dtoPerk in dto.Perks)
            {
                Perks.Add(new Perk(dtoPerk));
            }
        }

        public PerksModelDTO ToDto()
        {
            PerksModelDTO result = new PerksModelDTO();
            foreach (var trait in Traits)
            {
                result.Traits.Add(trait);
            }
            foreach (var perk in Perks)
            {
                result.Perks.Add(perk);
            }

            return result;
        }
        //
    }
}
