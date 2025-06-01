using QMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VNS.QMS
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
            //Application.Run(new frm_QMSTiepdon_Layso_V1());
            //Application.Run(new frm_QMSPCN_Layso());
            Application.Run(new QMS_Tiepdon_BVMHN2());
        }
    }
}
