using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.ViewModels.Serialization
{
    public static class ChangeTracker
    {
        private static bool DataChanged { get; set; } = false;

        public static void SetDirty()
        {
            DataChanged = true;
        }

        public static void Reset()
        {
            DataChanged = false;
        }

        public static bool IsDirty() { return DataChanged; }
    }
}
