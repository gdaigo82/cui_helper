/*
 * Copyright 2016 gdaigo82@gmail.com.
 * Licensed under the MIT license
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuiHelperLib
{
    public interface CuiHelperAppInterface
    {
        //以下の関数処理を実装する事で、アプリケーションが実現出来ます。
        //実際は CuiHelperApplication.cs を見てもらってもよろしいでしょうか・・
        int PrepareDragAndDrop(string[] files, string text);
        void DragAndDrop(string[] files, string text);
        int PrepareTextBoxEvent(string text);
        void TextBoxEvent(string text);
        int PrepareComboBoxEvent(string command, string text);
        void ComboBoxEvent(string command, string text);
        CuiHelperComboBoxData[] GetComboBoxData();
        void Init(CuiHelperBot bot, CuiHelperBrowser browser, string contentsPath);
    }
}
