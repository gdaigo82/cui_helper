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
using System.Windows.Controls;

namespace CuiHelperLib
{
    public class CuiHelperBrowser
    {
        private const string BEFORE_PATH_NAME = "others\\report_before.txt";
        private const string AFTER_PATH_NAME = "others\\report_after.txt";

        private WebBrowser m_browser;
        private string m_basePath;

        public bool MakeHtmlWithTemplate(string path, string text)
        {
            string before = TextFile.Read(m_basePath + BEFORE_PATH_NAME);
            if (before == null)
            {
                return false;
            }

            string after = TextFile.Read(m_basePath + AFTER_PATH_NAME);
            if (after == null)
            {
                return false;
            }

            string html = before + text + after;

            return TextFile.Write(path, false, html);
        }
        
        public void SetURL(string url)
        {
            m_browser.Navigate(url);
        }

        public void SetHTML(string html)
        {
            m_browser.NavigateToString(html);
        }

        public CuiHelperBrowser(WebBrowser browser, string path)
        {
            m_basePath = path;
            m_browser = browser;
        }
    }
}
