using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Noitru
{
    public partial class frm_lichsubuonggiuong_nguoibenh : Form
    {
        public string mabenhvien = "";
        public int idBenhvien = -1;
        public bool mv_blnCancel=true;
        public long Id_Bg = 0;
        public KcbLuotkham objLK = null;
        public frm_lichsubuonggiuong_nguoibenh(KcbLuotkham objLK)
        {
            InitializeComponent();
            this.objLK = objLK;
           
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.KeyDown += new KeyEventHandler(frm_lichsubuonggiuong_nguoibenh_KeyDown);
            this.Load += new EventHandler(frm_lichsubuonggiuong_nguoibenh_Load);
            grdBuongGiuong.MouseDoubleClick += GrdBuongGiuong_MouseDoubleClick;
        }

        private void GrdBuongGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Chon();
        }

        void frm_lichsubuonggiuong_nguoibenh_Load(object sender, EventArgs e)
        {
            LayLichsuBuongGiuong();
        }
        void LayLichsuBuongGiuong()
        {
            try
            {
                // objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)));
                //Lấy tất cả lịch sử buồng giường
                DataTable m_dtBG =
                      new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLK.MaLuotkham, objLK.IdBenhnhan, "-1", -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBG, false, true, "1=1",
                    NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Loi :" + ex.Message);
            }
            finally
            {
               
            }
        }
       

        void frm_lichsubuonggiuong_nguoibenh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
          
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdChon_Click(object sender, EventArgs e)
        {

            Chon();
        }
        void Chon()
        {
            if (!Utility.isValidGrid(grdBuongGiuong))
            {
                return;
            }
            Id_Bg = Utility.Int64Dbnull(grdBuongGiuong.GetValue("Id"));
            mv_blnCancel = false;
            this.Close();
        }
    }
}
