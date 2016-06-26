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

namespace CuiHelper
{
    public enum CuiHelperRequest { None, DragAndDrop, TextBox, Button }

    class CuiHelperAppManager
    {
        private string[] m_files;
        private string m_text;
        private CuiHelperAppInterface m_app;
        private CuiHelperRequest m_LatestRequst;
        private TextBox m_inputTextBox;
        private string m_Command;
        private System.Windows.Threading.DispatcherTimer timer;

        private void timerEvent(object sender, EventArgs e)
        {
            timer.Stop();
            switch (m_LatestRequst)
            {
                case CuiHelperRequest.DragAndDrop:
                    m_app.DragAndDrop(m_files, m_inputTextBox.Text);
                    break;

                case CuiHelperRequest.TextBox:
                    m_app.TextBoxEvent(m_text);
                    m_inputTextBox.Text = "";
                    break;

                case CuiHelperRequest.Button:
                    m_app.ButtonEvent(m_Command, m_inputTextBox.Text);
                    break;

                default:
                    DebugPrint.output("EVENT", "timer: undefined request!");
                    break;
            }
        }

        private void StartTimer(int msec)
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds((double)msec);
            timer.Tick += timerEvent;
            timer.Start();
        }
        
        public void TextBoxEvent(string text)
        {
            m_LatestRequst = CuiHelperRequest.TextBox;
            m_text = text;
            int msec = m_app.PrepareTextBoxEvent(m_text);
            if (msec == 0)
            {
                m_app.TextBoxEvent(m_text);
                return;
            }
            StartTimer(msec);
        }

        public void DragAndDropEvent(string[] files)
        {
            m_LatestRequst = CuiHelperRequest.DragAndDrop;
            m_files = files;
            int msec = m_app.PrepareDragAndDrop(m_files, m_inputTextBox.Text);
            if (msec == 0)
            {
                m_app.DragAndDrop(m_files, m_inputTextBox.Text);
                return;
            }
            StartTimer(msec);
        }

        public void ButtonEvent(string command)
        {
            m_LatestRequst = CuiHelperRequest.Button;
            m_Command = command;
            int msec = m_app.PrepareButtonEvent(m_Command, m_inputTextBox.Text);
            if (msec == 0)
            {
                m_app.ButtonEvent(m_Command, m_inputTextBox.Text);
                return;
            }
            StartTimer(msec);
        }

        public CuiHelperComboBoxData[] GetComboBoxData()
        {
            return m_app.GetComboBoxData();
        }

        public void Init()
        {
            m_app.Init();
        }
        
        public CuiHelperAppManager(CuiApplication app, TextBox inputTextBox)
        {
            m_inputTextBox = inputTextBox;
            m_app = app;
        }
    }
}
