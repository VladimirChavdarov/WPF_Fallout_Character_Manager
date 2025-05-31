using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        // All Models
        public BioModel BioModel { get; } = new BioModel();
        public SPECIALModel SPECIALModel { get; } = new SPECIALModel();
        public SurvivalModel SurvivalModel { get; } = new SurvivalModel();
        //

        // All ViewModels
        public BioViewModel? BioViewModel { get; }
        public SurvivalViewModel? SurvivalViewModel { get; }
        public SPECIALViewModel? SPECIALViewModel { get; }
        //

        // Constructor
        public MainWindowViewModel()
        {
            BioViewModel = new BioViewModel(BioModel);
            SPECIALViewModel = new SPECIALViewModel(SPECIALModel);
            SurvivalViewModel = new SurvivalViewModel(SurvivalModel, SPECIALModel);
        }
        //
    }
}
