using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;
using Windows.Storage.Streams;

using System.Runtime.Serialization;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.Storage.FileProperties;


namespace ExecuseManagerUWP
{
    class ExcuseManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Excuse CurrentExcuse { get; set; }
        public string FileDate { get; private set; }
        private Random random = new Random();
        private IStorageFolder excuseFolder = null;
        private IStorageFile excuseFile;
        private DataContractSerializer serializer = new DataContractSerializer(typeof(Excuse));

        public ExcuseManager()
        {
            NewExcuseAsync();
        }

        public async void NewExcuseAsync()
        {
            CurrentExcuse = new Excuse();
            excuseFile = null;
            OnPropertyChanged("CurrentExcuse");
            await UpdateFileDateAsync();
        }

        public async Task<bool> ChooseNewFolderAsync()
        {
            FolderPicker folderPicker = new FolderPicker() { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };

            folderPicker.FileTypeFilter.Add(".xml");
            IStorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                excuseFolder = folder;
                return true;
            }
            MessageDialog warningDialog = new MessageDialog("No excuse folder chosen");
            await warningDialog.ShowAsync();
            return false;
        }

        public async void OpenExcuseAsync()
        {
            FileOpenPicker picker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary, CommitButtonText = "Open Excuse File" };
            picker.FileTypeFilter.Add(".xml");
            excuseFile = await picker.PickSingleFileAsync();
            if (excuseFile != null)
            {
                await ReadExcuseAsync();
            }
        }

        public async void OpenRandomExcuseAsync()
        {
            IReadOnlyList<IStorageFile> files = await excuseFolder.GetFilesAsync();
            excuseFile = files[random.Next(0, files.Count())];
            await ReadExcuseAsync();
        }

        public async Task UpdateFileDateAsync()
        {
            if (excuseFile != null)
            {
                BasicProperties basicProperties = await excuseFile.GetBasicPropertiesAsync();
                FileDate = basicProperties.DateModified.ToString();
            }
            else
            {
                FileDate = "(no file loaded)";
            }
            OnPropertyChanged("FileDate");
        }

        public async void SaveCurrentExcuseAsync()
        {
            if (CurrentExcuse == null)
            {
                await new MessageDialog("No excuse loaded").ShowAsync();
                return;
            }
                if (string.IsNullOrEmpty(CurrentExcuse.Description))
                {
                    await new MessageDialog("Curent Excuse does not have a description").ShowAsync();
                    return;
                }
            if (excuseFile == null)
            {
                    excuseFile = await excuseFolder.CreateFileAsync(CurrentExcuse.Description + ".xml", CreationCollisionOption.ReplaceExisting);
            }
            await WriteExcuseAsync();
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        public async Task ReadExcuseAsync()
        {
            // you'll write this method
        }

        public async Task WriteExcuseAsync()
        {
            // you'll write this method
            using (IRandomAccessStream stream = await excuseFile.OpenAsync(FileAccessMode.ReadWrite))
            {

            }
        }

        public async Task SaveCurrentExcuseAsAsync()
        {
            // you'll write this method
        }
    }
}
