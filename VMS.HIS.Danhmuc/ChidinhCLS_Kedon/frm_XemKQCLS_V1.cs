using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;

namespace VMS.HIS.Danhmuc.Dungchung
{
    public partial class frm_XemKQCLS_V1 : Form
    {
        KcbLuotkham objLuotkham;
        public string result;
        byte noitru = 100;
        bool hasLoaded = false;
        public frm_XemKQCLS_V1(KcbLuotkham objLuotkham,byte noitru)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.objLuotkham = objLuotkham;
            this.noitru = noitru;
            this.Load += Frm_XemKQCLS_V1_Load;
        }

        private void Frm_XemKQCLS_V1_Load(object sender, EventArgs e)
        {
            try
            {
                grdList.DataSource = SPs.ClsXemketquaClsV2(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,100).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
          
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
           if(grdList.GetCheckedRows().Count()<=0)
            {
                result = "";
                return;
            }
            result = string.Join(";",(from p in grdList.GetCheckedRows() select string.Format("{0}:{1}", Utility.sDbnull(p.Cells["ten_thongso"].Value), Utility.sDbnull(p.Cells["ket_qua"].Value))).ToArray<string>());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
