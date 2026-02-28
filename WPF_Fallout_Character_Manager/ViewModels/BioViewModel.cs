using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPF_Fallout_Character_Manager.Models;
using WPF_Fallout_Character_Manager.Utilities;
using WPF_Fallout_Character_Manager.ViewModels.MVVM;

namespace WPF_Fallout_Character_Manager.ViewModels
{
    internal class BioViewModel : ViewModelBase
    {
        // local variables
        private BioModel? _bioModel;
        private BitmapImage _image;
        //

        public BioModel? BioModel
        {
            get { return _bioModel; }
            set
            {
                _bioModel = value;
                OnPropertyChanged("BioModel");
            }
        }

        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        // commands
        private RelayCommand _loadImageCommand;
        public RelayCommand LoadImageCommand
        {
            get
            {
                return _loadImageCommand ??
                    (
                    _loadImageCommand = new RelayCommand(obj =>
                    {
                        var dialog = new Microsoft.Win32.OpenFileDialog();
                        dialog.Title = "Select an Avatar";
                        dialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
                        if(dialog.ShowDialog() == true)
                        {
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(dialog.FileName);
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                            Image = bitmap;
                            BioModel.ImageSource = dialog.FileName;
                        }
                    }));
            }
        }

        public RelayCommand FlipKarmaCapCommand { get; private set; }
        private void FlipKarmaCap(object obj) // TODO: this doesn't get called
        {
            if(obj is KarmaCap karmaCap)
            {
                karmaCap.IsActive = !karmaCap.IsActive;
            }
        }

        public RelayCommand AddKarmaCapCommand { get; private set; }
        private void AddKarmaCap(object _ = null)
        {
            BioModel.KarmaCaps.Add(new KarmaCap(true));
        }

        public RelayCommand RemoveKarmaCapCommand { get; private set; }
        private void RemoveKarmaCap(object _ = null)
        {
            if(BioModel.KarmaCaps.Count > 0)
            {
                BioModel.KarmaCaps.Remove(BioModel.KarmaCaps.Last());
            }
        }
        //

        // constructor
        public BioViewModel(BioModel? bioModel)
        {
            _bioModel = bioModel;

            UpdateImage();

            FlipKarmaCapCommand = new RelayCommand(FlipKarmaCap);
            AddKarmaCapCommand = new RelayCommand(AddKarmaCap);
            RemoveKarmaCapCommand = new RelayCommand(RemoveKarmaCap);
        }
        //

        // methods
        public void UpdateImage()
        {
            if (!string.IsNullOrEmpty(_bioModel?.ImageSource))
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(_bioModel.ImageSource, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    Image = bitmap;
                }
                catch (Exception ex)
                {
                    // Optional: log or handle image load failure
                    Console.WriteLine($"Failed to load image: {ex.Message}");
                }
            }
        }
        //
    }
}
