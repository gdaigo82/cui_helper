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
using System.Windows;
using Codeplex.Data; // dynamic json
using System.Windows.Input;

namespace CuiHelperLib
{
    public class CuiHelperFactory
    {
        private const string SETTING_ITEM_NAME = "parameters";
        private static string m_settingFile;

        private CuiHelperData m_data;
        private CuiHelperBot m_bot;
        private CuiHelperBrowser m_browser;
        private CuiHelperAppManager m_appManager;
        private CuiHelperJsonParser m_jsonParser;
        private CuiHelperAppInterface m_app;
        private TextBox m_inputTextBox;
        private ComboBox m_inputComboBox;
        private string m_imagePath;
        private string m_contentsPath;

        private bool settingThisApplication()
        {
            bool success = m_jsonParser.StartJson(m_settingFile);
            if (!success)
            {
                MessageBox.Show("設定ファイルが読めません！", "ERROR");
                return false;
            }

            dynamic item = m_jsonParser.SearchItem(SETTING_ITEM_NAME);
            if (item == null)
            {
                MessageBox.Show("設定ファイルが正しくありません！", "ERROR");
                return false;
            }

            m_imagePath = item.ImagePath;
            m_contentsPath = item.ContentsPath;
            DebugPrint.output("factory", "ImagePath=" + item.ImagePath);
            DebugPrint.output("factory", "ContentsPath=" + item.ContentsPath);
            return true;
        }

        private void InitComboBox()
        {
            CuiHelperComboBoxData[] comboBoxData = m_appManager.GetComboBoxData();
            m_inputComboBox.ItemsSource = comboBoxData;
            m_inputComboBox.DisplayMemberPath = "Name";
            m_inputComboBox.SelectedValuePath = "Command";
        }

        public void Start()
        {
            m_appManager.Init(m_bot, m_browser, m_contentsPath);
            InitComboBox();
        }

        public void WindowDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && m_appManager != null)
            {
                m_appManager.DragAndDropEvent(files);
            }
        }

        public void WindowPreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        public void OnComboBoxEvent()
        {
            CuiHelperComboBoxData data = (CuiHelperComboBoxData)m_inputComboBox.SelectedItem;
            if (data == null)
            {
                return;
            }
            m_appManager.ButtonEvent(data.Commnad);
        }

        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                m_appManager.TextBoxEvent(m_inputTextBox.Text);
            }
        }

        public void SetDataContext(Window window)
        {
            window.DataContext = m_data;
        }

        public CuiHelperData GetBindingDataClass()
        {
            return m_data;
        }

        public CuiHelperFactory(string path, CuiHelperAppInterface app, WebBrowser browser, TextBox inputTextBox, ComboBox inputComboBox)
        {
            m_settingFile = path;
            m_inputTextBox = inputTextBox;
            m_inputComboBox = inputComboBox;
            m_jsonParser = new CuiHelperJsonParser();
            bool success = settingThisApplication();
            if (!success)
            {
                return;
            }
            m_data = new CuiHelperData();
            m_bot = new CuiHelperBot(m_imagePath, m_data);
            m_browser = new CuiHelperBrowser(browser, m_contentsPath);
            m_app = app;
            m_appManager = new CuiHelperAppManager(m_app, m_inputTextBox);
        }
    }
}
