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
    class ConditionsModel : ModelBase, ISerializable<ConditionsModelDTO>
    {
        // constructor
        public ConditionsModel()
        {
            Conditions = new ObservableCollection<Condition>();
        }
        //

        // methods
        public void FromDto(ConditionsModelDTO dto, bool versionMismatch = false)
        {
            Conditions.Clear();
            foreach(var conditionDto in dto.Conditions)
            {
                Conditions.Add(new Condition(conditionDto));
            }
        }

        public ConditionsModelDTO ToDto()
        {
            ConditionsModelDTO result = new ConditionsModelDTO();
            foreach(var condition in Conditions)
            {
                result.Conditions.Add(condition.ToDto());
            }
            return result;
        }
        //

        // data
        public ObservableCollection<Condition> Conditions { get; }
        //
    }
}
