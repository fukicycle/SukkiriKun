﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SukkiriKun
{
    public interface NotifyChanged
    {
        void ItemClicked();
        void ThrowException(string message);
        void ItemEdit(ShortCutItem item);
    }
}
