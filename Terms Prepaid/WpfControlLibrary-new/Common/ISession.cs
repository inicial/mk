﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface ISession
    {
        bool IsSuperviser { get; }
        int UsKey { get; }
    }
}
