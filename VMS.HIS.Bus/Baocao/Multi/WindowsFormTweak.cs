using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VietBaIT.HandyTools
{
    public class WindowsFormTweak
    {
        public static void PerformTweak(Form form, string assemblyFullName)
        {
            VietBaIT.HandyTools.GridExColumnSorting.SetSortingMoveEvent(assemblyFullName, form);
            VietBaIT.HandyTools.GridExColumnSorting.PerformGridExColumnSorting(assemblyFullName, form);
        }
    }
}
