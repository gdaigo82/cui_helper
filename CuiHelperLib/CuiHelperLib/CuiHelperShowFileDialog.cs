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
using System.Windows.Forms;

namespace CuiHelperLib
{
    public class CuiHelperShowFileDialog
    {
        public static string Show(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = title;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
