/*
 * Copyright 2016 gdaigo82@gmail.com.
 * Licensed under the MIT license
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//add by gdaigo
using System.ComponentModel;

namespace CuiHelper
{
    class CuiHelperComboBoxData : INotifyPropertyChanged
    {
        private string NameVal;
        public string Name
        {
            get { return NameVal; }
            set
            {
                NameVal = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string CommnadVal;
        public string Commnad
        {
            get { return CommnadVal; }
            set
            {
                CommnadVal = value;
                NotifyPropertyChanged("Commnad");
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
