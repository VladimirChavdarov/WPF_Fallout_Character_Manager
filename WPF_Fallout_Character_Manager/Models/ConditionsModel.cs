using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models
{
    class ConditionsModel : ModelBase
    {
        // constructor
        public ConditionsModel()
        {
            Conditions = new ObservableCollection<Condition>();
        }
        //

        // methods

        //

        // data
        public ObservableCollection<Condition> Conditions { get; }
        //
    }
}
