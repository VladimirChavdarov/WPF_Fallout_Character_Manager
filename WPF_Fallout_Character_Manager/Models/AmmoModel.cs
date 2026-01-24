using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.Inventory.Serialization;
using WPF_Fallout_Character_Manager.Models.MVVM;
using WPF_Fallout_Character_Manager.Models.Serialization;
using WPF_Fallout_Character_Manager.Utilities;

namespace WPF_Fallout_Character_Manager.Models
{
    class AmmoModel : ModelBase, ISerializable<AmmoModelDTO>
    {
        // constructor
        public AmmoModel()
        {
            Ammos = new ObservableCollection<Ammo>();
        }
        //

        // methods
        public void FromDto(AmmoModelDTO dto, bool versionMismatch = false)
        {
            Ammos.Clear();
            foreach (AmmoDTO aDto in dto.Ammos)
            {
                Ammos.Add(new Ammo(aDto));
            }
        }

        public AmmoModelDTO ToDto()
        {
            AmmoModelDTO result = new AmmoModelDTO();

            foreach (Ammo ammo in Ammos)
            {
                var typedDto = Utils.EnsureDtoType<AmmoDTO>(ammo.ToDto());

                result.Ammos.Add(typedDto);
            }

            return result;
        }
        //

        // data
        public ObservableCollection<Ammo> Ammos { get; set; }
        //
    }
}
