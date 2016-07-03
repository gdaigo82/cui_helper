using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//gdaigo add
using CuiHelperLib;

namespace CuiHelperApp
{
    /*
     *   ここで作成したアプリケーションとGUIとの関係を記述します。
     *   ここを書き換えることで、任意の処理を行うイメージです。
     *   但し、実際には別のクラスで処理を行い、本コードを利用する方が
     *   楽ではないかと思います。
     */

    // 後、exeと同じフォルダにあるsetting.jsonの設定もお願いします。

    // 本サンプルでは実処理は別クラスで生成し、
    // 各アプリがこのクラスのメンバ変数を設定する作りにしています。

    // これは応答内容を一括でまとめたものです。Sample側のアプリにこれを
    // 設定してもらうような作りになっています。
    public class CuiApplicationResult
    {
        public string html;     // 結果をHTMLにしたもの。
        public string htmlPath; // HTMLファイルに残すのでそのファイル名
        public string serif;    // 右下のTextBoxに表示するもの 
        public string imgPath;  // 左下に出力するイメージファイル
    }

    class CuiApplication :  CuiHelperAppInterface
    {
        private const int DELAY_MSEC_FOR_EVENT = 1000;
        private const string NORMAL_IMG = "pronama_normal.png";

        private const string COMMAND_GO_HOME = "GO_HOME";
        private const string COMMAND_CALC = "CALC";
        private const string COMMAND_IMAGE = "IMAGE";

        private CuiHelperComboBoxData[] m_ComboBoxData;
        private CuiHelperBot m_bot;
        private CuiHelperBrowser m_browser;
        private string m_contentsPath;

        //ここでComboBoxで表示するデータの定義を行います。
        private void MakeComboBoxData()
        {
            //CuiHelperComboBoxData[]の生成
            m_ComboBoxData = new[] {
                new CuiHelperComboBoxData { Name = "Goto Home", Commnad = COMMAND_GO_HOME },
                new CuiHelperComboBoxData { Name = "Calc", Commnad = COMMAND_CALC },
                new CuiHelperComboBoxData { Name = "View jpg/png file", Commnad = COMMAND_IMAGE },
            };
        }

        // 初期イメージを表示します。
        private void MakeInitView(string serif)
        {
            string url = m_contentsPath + "index.html";
            m_browser.SetURL(@url);
            m_bot.Play(NORMAL_IMG, serif);
        }

        // アプリから戻されたCuiApplicationResultを元に出力します。
        private void MakeReport(CuiApplicationResult result)
        {
            if (result.htmlPath != null)
            {
                string url = m_contentsPath + result.htmlPath;
                if (m_browser.MakeHtmlWithTemplate(url, result.html) == true)
                {
                    m_browser.SetURL(url);
                }
            }
            m_bot.Play(result.imgPath, result.serif);
        }

        // ドラッグ＆ドロップの前処理。
        public int PrepareDragAndDrop(string[] files, string text)
        {
            m_bot.Play("pronama_drop.png", "ドラッグ＆ドロップキター！");
            return DELAY_MSEC_FOR_EVENT;
        }

        // ドラッグ＆ドロップの本処理
        public void DragAndDrop(string[] files, string text)
        {
            Sample2 sample = new Sample2();
            CuiApplicationResult result = new CuiApplicationResult();

            sample.main(files, text, result);
            MakeReport(result);
        }

        // テキスト内容を変更されたというイベントの前処理。
        public int PrepareTextBoxEvent(string text)
        {
            string serif = "お。テキストからだ。\r\n";
            serif += "えーと「" + text + "」…と。";

            m_bot.Play("pronama_textbox.png", serif);
            return DELAY_MSEC_FOR_EVENT;
        }

        // テキスト内容を変更されたというイベントの本処理。
        public void TextBoxEvent(string text)
        {
            Sample1 sample = new Sample1();
            CuiApplicationResult result = new CuiApplicationResult();

            sample.main(text, result);
            MakeReport(result);
        }

        // ComboBoxの選択項目実行を行うイベントの前処理。
        public int PrepareComboBoxEvent(string command, string text)
        {
            m_bot.Play("pronama_listbox.png", "ボタンを押しましたねー！");
            if (command.Equals(COMMAND_IMAGE))
            {
                return 0; // ダイアログを出す処理なので、直ちに移行させる。
            }
            else
            {
                return DELAY_MSEC_FOR_EVENT;
            }
        }

        // ComboBoxの選択項目実行を行うイベントの本処理。
        public void ComboBoxEvent(string command, string text)
        {
            CuiApplicationResult result = new CuiApplicationResult();

            if (command.Equals(COMMAND_GO_HOME))
            {
                // 初期画面に戻します。
                MakeInitView("最初の画面に戻したよ。");
            }
            else if (command.Equals(COMMAND_CALC))
            {
                Samlpe3 sample = new Samlpe3();
                sample.main(result);
                MakeReport(result);
            }
            else if (command.Equals(COMMAND_IMAGE))
            {
                sample4 sample = new sample4();
                sample.main(result);
                MakeReport(result);
            }
            else
            {
                m_bot.Play("pronama_execute.png", command + "," + text + "は何だろう？");
            }
        }

        // 生成したCuiHelperComboBoxDataを渡します。
        public CuiHelperComboBoxData[] GetComboBoxData()
        {
            return m_ComboBoxData;
        }

        // リソースの初期化です。
        public void Init(CuiHelperBot bot, CuiHelperBrowser browser, string contentsPath)
        {
            m_bot = bot;
            m_browser = browser;
            m_contentsPath = contentsPath;
            MakeComboBoxData();
            MakeInitView("始めましょー！");
        }
    }
}
