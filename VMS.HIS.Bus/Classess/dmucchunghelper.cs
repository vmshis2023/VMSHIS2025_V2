using System;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using VNS.Libs;

namespace VNS.HIS.UI.Classess
{
    
    public class dmucchunghelper
    {
        public static void ShowMe(AutoCompleteTextbox_Danhmucchung txt)
        {
            try
            {
                DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txt.LOAI_DANHMUC);
                _DMUC_DCHUNG.ShowDialog();
                if (!_DMUC_DCHUNG.m_blnCancel)
                {
                    string oldCode = txt.myCode;
                    txt.Init();
                    txt.SetCode(oldCode);
                    txt.Focus();
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
    }
}
