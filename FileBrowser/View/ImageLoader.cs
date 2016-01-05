using FileBrowser.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FileBrowser.View
{
    public class ImageLoader
    {
        //private BackgroundWorker _newTask;
        //private BackgroundWorker _oldTask;
        public List<string> imagePaths { get; set; }
        private FileItemsViewModel _fileItemsViewModel;
        private Dispatcher _dispatcher;
        //private bool _isBusy = false;
        private Thread _loadingThread;

        public ImageLoader(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void LoadImagesAsync(FileItemsViewModel fileItemsViewModel)
        {
            if (_loadingThread != null && _loadingThread.IsAlive)
            {
                _loadingThread.Abort();
            }
            _loadingThread = new Thread(new ThreadStart(DoLoadImagesAsync));
            _loadingThread.Start();
            _fileItemsViewModel = fileItemsViewModel;     
            // Load place holder
            CreateDefaultIcon(FileItemType.Drive);
            CreateDefaultIcon(FileItemType.Directory);
            CreateDefaultIcon(FileItemType.File);  

            // if (_oldTask != null) 
            // {
            //     _oldTask.CancelAsync();
            // }
            // _newTask = new BackgroundWorker();
            // _newTask.WorkerSupportsCancellation = true;
            // _newTask.WorkerReportsProgress = true;
            // _newTask.DoWork += new DoWorkEventHandler(DoLoadImagesAsync);                 
            // _newTask.RunWorkerAsync();
        }

        public void EndLoadingImages()
        {
            if (_loadingThread.IsAlive)
            {
                _loadingThread.Abort();
            }
        }

        private void CreateDefaultIcon(FileItemType type)
        {
            var iconPath = GetDefaultIconPath(type);
            var items = _fileItemsViewModel.DirectoriesAndFiles.Where(item => item.Type == type);
            BitmapFrame bf = null;
            var thumb = CreateThumb(iconPath, bf);
            foreach (var item in items)
            {                  
                item.Icon = thumb;
            }
        }

        private void DoLoadImagesAsync()
        {
            var fileItems = _fileItemsViewModel.DirectoriesAndFiles.Where(item => item.Type == FileItemType.File);
            foreach (var item in fileItems)
            {
                string imagePath = item.FullName;
                BitmapFrame bf = null;
                try
                {
                    bf = CreateThumb(imagePath, bf);
                }
                catch
                {
                    bf = CreateThumb(GetDefaultIconPath(item.Type), bf);
                }
                if (bf != null)
                {
                    _dispatcher.BeginInvoke(new Action(() =>
                    {
                        item.Icon = bf;
                    }));
                }
            }
        }

        // private void DoLoadImagesAsync(object sender, DoWorkEventArgs args)
        // {
        //     _isBusy = true;
        //     _oldTask = _newTask;
        //     var fileItems = _fileItemsViewModel.DirectoriesAndFiles.Where(item => item.Type == FileItemType.File);
        //     foreach (var item in fileItems)
        //     {
        //         if (_oldTask != null && _oldTask.CancellationPending)
        //         {
        //             args.Cancel = true;
        //             _oldTask.Dispose();
        //             _oldTask = null;
        //             return;
        //         }
        //         string imagePath = item.FullName;
        //         BitmapFrame bf = null;
        //         try
        //         {
        //             bf = CreateThumb(imagePath, bf);                   
        //         }
        //         catch 
        //         {
        //             bf = CreateThumb(GetDefaultIconPath(item.Type), bf);                        
        //         }
        //         if (bf != null)
        //         {
        //             _dispatcher.BeginInvoke(new Action(() =>
        //             {
        //                 item.Icon = bf;
        //             }));
        //         }                             
        //     }
        //     _oldTask.Dispose();
        //     _oldTask = null;
        // }

        private static BitmapFrame CreateThumb(string imagePath, BitmapFrame bf)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Open))
            {
                using (System.Drawing.Image drawingImage = System.Drawing.Image.FromStream(fileStream))
                {
                    int width = (int)(drawingImage.Width / (double)drawingImage.Height * 80);
                    using (System.Drawing.Image thumbImage =
                    drawingImage.GetThumbnailImage(width, 80, () => { return true; }, IntPtr.Zero))
                    {
                        MemoryStream ms = new MemoryStream();
                        thumbImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bf = BitmapFrame.Create(ms);
                    }
                }
            }
            return bf;
        }

        private static string GetDefaultIconPath(FileItemType type)
        {
            string imagePath = string.Empty;
            switch (type)
            {
                case FileItemType.Drive:
                    imagePath = @"E:\GitHub Project\Everything\FileBrowser\Resources\Driver.png";
                    break;
                case FileItemType.Directory:
                    imagePath = @"E:\GitHub Project\Everything\FileBrowser\Resources\Folder.png";
                    break;
                case FileItemType.File:
                    imagePath = @"E:\GitHub Project\Everything\FileBrowser\Resources\File.png";
                    break;
            }
            return imagePath;
        }
    }
}
