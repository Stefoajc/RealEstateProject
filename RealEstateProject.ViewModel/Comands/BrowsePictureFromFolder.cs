using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;

namespace RealEstateProject.ViewModel.Comands
{
    public class BrowsePictureFromFolder : ViewModelBase
    {
        public RelayCommand BrowseCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public string SelectedItem { get; set; }

        private ObservableCollection<string> imageList;

        public ObservableCollection<string> ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }

        public void Browse()
        {

            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();




            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "All Files (*.*)|*.*|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a ListBox of image items 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                ImageList.Add(filename);
            }
        }
        public void Delete()
        {
            ImageList.Remove(SelectedItem);
        }

        public BrowsePictureFromFolder()
        {
            ImageList = new ObservableCollection<string>();
            BrowseCommand = new RelayCommand(Browse);         // Using action syntax
            DeleteCommand = new RelayCommand(() => Delete()); // Using lambda syntax
        }
    }
}
