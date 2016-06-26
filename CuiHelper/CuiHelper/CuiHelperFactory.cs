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

namespace CuiHelper
{
    class CuiHelperFactory
    {
        // 一時的に、いずれちゃんとしたい。
        private const string SETTING_ITEM_NAME = "parameters";
        private static string m_settingFile = "setting.json";

        private CuiHelperData m_data;
        private CuiHelperBot m_bot;
        private CuiHelperBrowser m_browser;
        private CuiHelperAppManager m_appManager;
        private CuiHelperJsonParser m_jsonParser;
        private CuiApplication m_app;
        private TextBox m_inputTextBox;
        private ComboBox m_inputComboBox;
        private string m_imagePath;
        private string m_contentsPath;
        private bool m_initialized = false;

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

        public CuiHelperAppManager GetAppManager()
        {
            if (m_initialized)
            {
                return m_appManager;
            }
            else
            {
                return null;
            }
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
            if (!m_initialized)
            {
                return;
            }

            InitComboBox();
            m_appManager.Init();
        }

        public CuiHelperFactory(CuiHelperData data, WebBrowser browser, TextBox inputTextBox, ComboBox inputComboBox)
        {
            m_inputTextBox = inputTextBox;
            m_inputComboBox = inputComboBox;
            m_jsonParser = new CuiHelperJsonParser();
            bool success = settingThisApplication();
            if (!success)
            {
                return;
            }
            m_data = data;
            m_bot = new CuiHelperBot(m_imagePath, m_data);
            m_browser = new CuiHelperBrowser(browser, m_contentsPath);
            m_app = new CuiApplication(m_bot, m_browser, m_contentsPath);
            m_appManager = new CuiHelperAppManager(m_app, m_inputTextBox);
            m_initialized = true;
        }
    }
}
