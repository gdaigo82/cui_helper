/*
 * Copyright 2016 gdaigo82@gmail.com.
 * Licensed under the MIT license
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// add by gdaigo
using System.Diagnostics; // Process

namespace CuiHelperLib
{
    public class CuiHelperProcess
    {
        public static Process Exec(string name, string args, string work)
        {
            Process process = new Process();
            process.StartInfo.FileName = name;
            if (args != null)
                process.StartInfo.Arguments = args;
            if (work != null)
                process.StartInfo.WorkingDirectory = work;
            process.Start();

            return process;
        }

        public static void Close(Process process)
        {
            if (process == null)
                return;

            if (!process.HasExited)
            {
                process.Kill();
            }
        }
    }
}
