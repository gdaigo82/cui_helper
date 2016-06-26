/*
 * Copyright 2016 gdaigo82@gmail.com.
 * Licensed under the MIT license
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuiHelper
{
    /*
     *   実処理を行うアプリケーションのサンプルです。
     */

    // テキストが数値かどうか判断するアプリ
    // このアプリは表として出力するサンプルです。
    class Sample1
    {
        private const string SAMPLE1_HTML_FILENAME = "sample1.html";
        private const string SAMPLE1_EXEC_IMG = "pronama_execute.png";
        private bool m_isNumber;
        private static string m_table = "<tr><th>時間</th><th>入力</th><th>判定</th></tr>";

        public void judgeText(string text)
        {
            //テキストが数値かどうか判断するだけのものです。
            //これが本来したい処理です。
            try
            {
                double value = double.Parse(text);
                m_isNumber = true;
            }
            catch (FormatException e)
            {
                DebugPrint.output("sample", e.Message);
                m_isNumber = false;
            }
        }

        //出力するHTMLを作成します。これはテーブルの例です。
        private string MakeReportHtml(string text)
        {
            //bootstrapを利用しています。
            string header = "<table class=\"table table-condensed\">";
            string footer = "</table>";
            string td = "<tr><td>" + DateTime.Now + "</td>";

            td += "<td>" + text + "</td>";
            if (m_isNumber)
            {
                td += "<td>" + "数字" + "</td></tr>";
            }
            else
            {
                td += "<td>" + "文字列" + "</td></tr>";
            }

            m_table += td;  // 前の結果に追加していきます。

            string html = header + m_table + footer;
            return html;
        }

        //TextBoxへの反応を作成します。
        private string MakeSerif(string text)
        {
            string serif = text + "は";
            if (m_isNumber)
            {
                serif += "数字だね！";
            }
            else
            {
                serif += "文字列です！";
            }
            return serif;
        }

        //GUIとの口です。
        public void main(string text, CuiApplicationResult result)
        {
            //GUIとの口です。
            //textの文字列が数字かどうかを判定。
            //それに基づいて、
            //  html（のメインデータ）
            //  セリフ
            //  出力イメージ
            //を指定します。

            judgeText(text);
            result.html = MakeReportHtml(text);
            result.htmlPath = SAMPLE1_HTML_FILENAME;
            result.serif = MakeSerif(text);
            result.imgPath = SAMPLE1_EXEC_IMG;
        }
    }

    // 与えられたファイルパスからテキストファイルかどうかを判断します。
    //   －テキストならばファイルの中身をHTML化して、戻します。
    //   －それ以外はパスの文字列をHTML化して、戻します。
    // 本サンプルは、文字列と画像をどのように出力させるかの例示です。
    class Sample2
    {
        private const string SAMPLE2_HTML_FILENAME = "sample2.html";
        private const string SAMPLE2_EXEC_IMG = "pronama_execute.png";
        private const string SAMPLE2_ERROR_IMG = "pronama_error.png";
        private bool m_isTextFile = false;

        //指定されたパスがテキストファイルなら読み込んで文字列を戻します。
        //これが本来したい処理です。
        private string readTextFile(string path)
        {
            //判定はサンプルなので、簡易なものとします
            if (path == null || path.IndexOf(".txt") < 0)
            {
                return null;
            }

            return TextFile.Read(path);
        }


        //出力するHTMLを作成します。テキストを<pre>で表示させます。
        private string MakeReportHtml(string file, string text)
        {
            if (file != null)
            {
                m_isTextFile = true;
            }

            if (m_isTextFile)
            {
                // テキストをpreタグ使ってそのまま表示させます。
                string html = "<p>内容を以下に表示します。</p>";
                html += "<pre>" + file + "</pre>";

                return html;
            }
            else
            {
                // 絵とメッセージを表示させる例です。
                // img-responsiveでサイズを自動調整します。
                string html = "<p>";
                html += "ドラッグ＆ドロップしたファイルはテキストではないようです。<br>";
                html += "テキストボックスの文字列は「" + text + "」です。";
                html += "</p>";
                html += "<img src = \"img/";
                html += SAMPLE2_ERROR_IMG;
                html += "\" alt = \"iris-chan\" class=\"img-responsive\"/>";
                return html;
            }
        }

        //TextBoxへの反応を作成します。
        private string MakeSerif(string path)
        {
            if (path != null)
            {
                return path + " を処理してみたよ。";
            }
            else
            {
                return "あれ、ファイルの指定がおかしいみたい。。";
            }
        }

        //GUIとの口です。
        public void main(string[] files, string text, CuiApplicationResult result)
        {
            //GUIとの口です。
            //textの文字列が数字かどうかを判定。
            //それに基づいて、
            //  html（のメインデータ）
            //  セリフ
            //  出力イメージ
            //を指定します。

            string textFile = readTextFile(files[0]);

            result.html = MakeReportHtml(textFile, text);
            result.htmlPath = SAMPLE2_HTML_FILENAME;
            result.serif = MakeSerif(files[0]);
            result.imgPath = SAMPLE2_EXEC_IMG;
        }
    }

    // 外部プログラムを起動する際のサンプルです。
    // ここでは電卓を起動します。
    // これを応用すると、ほかのプログラムと連動させることが出来ます。
    class Samlpe3
    {
        private const string SAMPLE3_EXEC_IMG = "pronama_execute.png";

        // 外部プログラムはこのようにして実行する事が出来ます。
        //   CuiHelperProcess.Exec(string name, string args, string work)
        // nameにはexeファイルを、引数をargsに、作業ディレクトリをworkingDirectoryに設定します。
        // 不要ならば、nullで良いです。

        void ExecuteCalc()
        {
            CuiHelperProcess.Exec("calc.exe", null, null);
        }

        //GUIとの口です。
        public void main(CuiApplicationResult result)
        {
            //GUIとの口です。
            //textの文字列が数字かどうかを判定。
            //それに基づいて、
            //  html（のメインデータ）
            //  セリフ
            //  出力イメージ
            //を指定します。

            // このサンプルでは電卓を起動するだけですので
            // HTML（出力画面）は作成せず、セリフだけ反応するようにしています。

            ExecuteCalc();
            result.html = null;
            result.htmlPath = null; // WebBrowser領域は使わない。
            result.serif = "電卓を起動したよ！";
            result.imgPath = SAMPLE3_EXEC_IMG;
        }
    }

    // ファイルダイアログを使ってファイルを選択させる際のサンプルです。
    // 選択されたファイルがjpgかpngならimgタグを使って表示するようにしています。
    class sample4
    {
        private const string SAMPLE4_HTML_FILENAME = "sample4.html";
        private const string SAMPLE4_EXEC_IMG = "pronama_execute.png";
        private const string SAMPLE4_ERROR_IMG = "pronama_error.png";

        private bool m_otherFileType;
        private string m_url;

        // ファイル選択ダイアログを出します。
        // 選択されたパスがjpgかpngならば、URLにします。
        // そうでない場合には m_otherFileType をfalseにします（結果表示のため）
        private void MakeImgURL()
        {
            string path = CuiHelperShowFileDialog.Show("jpgかpngファイルを選んで下さい。");
            if (path == null)
            {
                return;
            }

            //　sampleなので、判定条件は簡易なものです。手抜きで申し訳ない・・
            if (path.IndexOf(".jpg") < 0 && path.IndexOf(".png") < 0)
            {
                //画像じゃない
                m_otherFileType = true;
                m_url = path; // エラー説明画面で使います。
            }
            else
            {
                //画像である
                m_url = "file:\\\\\\" + path;
            }

            return;
        }

        //出力するHTMLを作成します。テキストを<pre>で表示させます。
        private string MakeReportHtml()
        {
            if (m_url != null && !m_otherFileType)
            {
                // 画像を表示させます。
                // img-responsiveでサイズを自動調整します。
                string html = "<img src = \"";
                html += m_url;
                html += "\" alt = \"image\" class=\"img-responsive\"/>";
                return html;
            }
            else
            {
                // エラー画面を作成します。
                string html = "<p>";
                if (m_otherFileType)
                {
                    html += m_url + "　は画像ではないかもしれません。";
                }
                else
                {
                    html += "ファイル選択がキャンセルされたかもしれません。";
                }
                html += "</p>";

                //エラー時の画像
                // img-responsiveでサイズを自動調整します。
                html += "<img src = \"img/";
                html += SAMPLE4_ERROR_IMG;
                html += "\" alt = \"iris-chan\" class=\"img-responsive\"/>";

                return html;
            }
        }

        //GUIとの口です。
        public void main(CuiApplicationResult result)
        {
            //GUIとの口です。
            //textの文字列が数字かどうかを判定。
            //それに基づいて、
            //  html（のメインデータ）
            //  セリフ
            //  出力イメージ
            //を指定します。

            // このサンプルでは電卓を起動するだけですので
            // HTML（出力画面）は作成せず、セリフだけ反応するようにしています。

            m_otherFileType = false;
            m_url = null;
            MakeImgURL();

            result.html = MakeReportHtml();
            result.htmlPath = SAMPLE4_HTML_FILENAME;
            result.serif = "結果はこんな感じでーす！";
            result.imgPath = SAMPLE4_EXEC_IMG;
        }
    }
}
