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
using System.IO;

namespace CuiHelper
{
    public static class TextFile
    {
        public static string Read(string path)
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(
                    @path, 
                    System.Text.Encoding.GetEncoding("shift_jis"));
                string text = reader.ReadToEnd();
                reader.Close();
                return text;
            }
            catch (Exception exception)
            {
                DebugPrint.output("file", exception.Message);
                return null;
            }
        }

        public static bool Write(string path, bool append, string text)
        {
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(
                    @path,
                    append,
                    System.Text.Encoding.GetEncoding("utf-8"));
                writer.Write(text);
                writer.Close();
                return true;
            }
            catch (Exception exception)
            {
                DebugPrint.output("file", exception.Message);
                return false;
            }
        }

    }
}
