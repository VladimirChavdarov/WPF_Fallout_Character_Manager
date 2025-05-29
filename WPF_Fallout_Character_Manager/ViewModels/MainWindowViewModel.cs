using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        // All ViewModels
        public BioViewModel? BioViewModel { get; }
        public SurvivalViewModel? SurvivalViewModel { get; }
        //

        // Constructor
        public MainWindowViewModel()
        {
            BioViewModel = new BioViewModel(new Models.BioModel());
            SurvivalViewModel = new SurvivalViewModel(new Models.SurvivalModel());
        }
        //
    }
}
