﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppDemo.Schedulers
{
    public class Class
    {
        public static void Do1()
        {
            Debug.WriteLine("Do.........1_" + DateTime.Now);
        }

        public static void Do3()
        {
            Debug.WriteLine("Do.........3_" + DateTime.Now);
        }
    }
}