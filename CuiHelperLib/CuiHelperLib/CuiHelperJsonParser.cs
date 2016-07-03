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
using Codeplex.Data; // dynamic json

namespace CuiHelperLib
{
    public class CuiHelperJsonParser
    {
        private dynamic m_Json;

        public dynamic GetRootObject()
        {
            return m_Json;
        }

        public dynamic SearchItem(string word)
        {
            foreach (KeyValuePair<string, dynamic> item in m_Json)
            {
                if (item.Key.Equals(word))
                {
                    return item.Value;
                }
            }
            return null;
        }

        public bool StartJson(string file)
        {
            string jsonText = TextFile.Read(file);
            if (jsonText == null)
            {
                return false;
            }
            m_Json = DynamicJson.Parse(@jsonText);
            return true;
        }
    }
}
