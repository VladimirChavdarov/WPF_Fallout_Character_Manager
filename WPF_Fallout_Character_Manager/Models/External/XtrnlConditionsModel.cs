using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Fallout_Character_Manager.Models.ModifierSystem.MVVM;
using WPF_Fallout_Character_Manager.Models.MVVM;

namespace WPF_Fallout_Character_Manager.Models.External
{
    class XtrnlConditionsModel : ModelBase
    {
        // constructor
        public XtrnlConditionsModel()
        {
            Conditions = new ObservableCollection<Condition>();

            var lines = File.ReadAllLines("Resources/Spreadsheets/conditions.csv");

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(';');

                // skip invalid rows
                if(parts.Length < 2 )
                    continue;

                Condition condition = new Condition(
                    name: parts[0],
                    description: parts[1]
                    );

                Conditions.Add(condition);
            }
            Console.WriteLine("Conditions uploaded");
        }
        //

        // data
        public ObservableCollection<Condition>? Conditions { get; }
        //
    }

    class Condition : ModTypeBase
    {
        // constructor
        public Condition(string name = "NewCondition", string description = "No Description", bool isReadOnly = true, Visibility descriptionVisibility = Visibility.Visible)
        {
            _name = name;
            _description = description;
            _isReadOnly = isReadOnly;
            _descriptionVisibility = descriptionVisibility;
        }
        //

        // members
        private string _name;
        public string Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => Update(ref _description, value);
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                Update(ref _isReadOnly, value);
                if(value == false)
                    DescriptionVisibility = Visibility.Visible; // automatically show the description if we enter edit mode.
            } 
        }

        private Visibility _descriptionVisibility;
        public Visibility DescriptionVisibility
        {
            get => _descriptionVisibility;
            set => Update(ref _descriptionVisibility, value);
        }
        //

        // methods
        public Condition Clone() => new Condition
        {
            Name = this.Name,
            Description = this.Description,
            IsReadOnly = this.IsReadOnly,
            DescriptionVisibility = this.DescriptionVisibility,
        };
        //
    }
}
