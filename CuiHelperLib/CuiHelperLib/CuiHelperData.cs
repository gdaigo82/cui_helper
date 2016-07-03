/*
 * Copyright 2016 gdaigo82@gmail.com.
 * Licensed under the MIT license
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// add by gdaigo
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace CuiHelperLib
{
    public class CuiHelperData : INotifyPropertyChanged
    {
        private BitmapImage BotImageDataValue;
        public BitmapImage BotImageData
        {
            get { return this.BotImageDataValue; }
            set
            {
                if (value != this.BotImageDataValue)
                {
                    this.BotImageDataValue = value;
                    NotifyPropertyChanged("BotImageData");
                }
            }
        }

        private string CommentTextValue;
        public string CommentText
        {
            get { return this.CommentTextValue; }
            set
            {
                if (value != this.CommentTextValue)
                {
                    this.CommentTextValue = value;
                    NotifyPropertyChanged("CommentText");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

}
