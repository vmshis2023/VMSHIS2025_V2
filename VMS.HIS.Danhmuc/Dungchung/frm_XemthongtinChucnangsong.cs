using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.Danhmuc.Dungchung
{
    public partial class frm_XemthongtinChucnangsong : Form
    {
        public KcbLuotkham objLuotkham = null;
        bool Doubleclick = false;
        byte noitru = 100;
        public delegate void OnSelectMe(string mach, string nhietdo,string nhiptho, string huyetap, string chieucao,string cannang,string bmi,string nhommau,string SPO2);
        public event OnSelectMe _OnSelectMe;
        public frm_XemthongtinChucnangsong(KcbLuotkham objLuotkham, bool Doubleclick, byte noitru)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.objLuotkham = objLuotkham;
            this.Doubleclick = Doubleclick;
            this.noitru = noitru;
            grdChucnangsong.MouseDoubleClick += grdChucnangsong_MouseDoubleClick;
            grdChucnangsong.KeyDown += grdChucnangsong_KeyDown;
        }

        void grdChucnangsong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                SelectMe(); 
        }

        void grdChucnangsong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectMe(); 
        }
        void SelectMe()
        {
            try
            {
                if (Utility.isValidGrid(grdChucnangsong) && _OnSelectMe != null && Doubleclick)
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn lấy thông tin chức năng sống đang chọn?", "Xác nhận chọn thông tin chức năng sống", true))
                    {
                        _OnSelectMe(Utility.sDbnull(grdChucnangsong.GetValue("mach")), Utility.sDbnull(grdChucnangsong.GetValue("nhietdo")), Utility.sDbnull(grdChucnangsong.GetValue("nhiptho")), Utility.sDbnull(grdChucnangsong.GetValue("huyetap")),
                            Utility.sDbnull(grdChucnangsong.GetValue("chieucao")), Utility.sDbnull(grdChucnangsong.GetValue("cannang")), Utility.sDbnull(grdChucnangsong.GetValue("bmi")), Utility.sDbnull(grdChucnangsong.GetValue("nhommau")), Utility.sDbnull(grdChucnangsong.GetValue("SPO2")));
                        this.Close();
                    }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void frm_XemthongtinChucnangsong_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.KcbLaythongtinChucnangsong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, noitru).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdChucnangsong, dtData, true, true, "1=1", "ngay desc");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);  
              
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SelectMe(); 

        }
    }
}
