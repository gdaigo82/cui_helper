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
using System.Diagnostics;

namespace CuiHelper
{
    public static class DebugPrint
    {
        public static void output(string module, string text)
        {
            String output = DateTime.Now + ":" + module + ":" + text;
            Debug.WriteLine(output);
        }
    }
}
