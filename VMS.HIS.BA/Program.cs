using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VMS.HIS.BA.Forms;

namespace VMS.HIS.BA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmHoSoCayThuocTranhThai());
        }
    }
}
