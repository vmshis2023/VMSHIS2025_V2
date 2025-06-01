using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VNS.HandyTools
{
    public class WindowsFormTweak
    {
        public static void PerformTweak(Form form, string assemblyFullName)
        {
            VNS.HandyTools.GridExColumnSorting.SetSortingMoveEvent(assemblyFullName, form);
            VNS.HandyTools.GridExColumnSorting.PerformGridExColumnSorting(assemblyFullName, form);
        }
    }
}
