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
using System.Windows.Media.Imaging;

namespace CuiHelperLib
{
    public class CuiHelperBot
    {
        private string m_basePath;
        private CuiHelperData m_data;

        private BitmapImage MakeBitMap(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            return bitmap;
        }
        
        public void Play(string image, string serif)
        {
            if (image != null)
            {
                string path = m_basePath + image;
                BitmapImage bitmap = MakeBitMap(path);
                m_data.BotImageData = bitmap;
            }
            if (serif != null)
            {
                m_data.CommentText = serif;
            }
        }

        public CuiHelperBot(string path, CuiHelperData data)
        {
            m_basePath = path;
            m_data = data;
        }
    }
}