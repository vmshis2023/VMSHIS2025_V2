using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using VNS.Properties;
using VNS.HIS.UI.NGOAITRU;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_Dsach_Congkham : Form
    {
        public bool mv_blnCancel = true;
        KcbChidinhcl objChidinh = null;
        public frm_Dsach_Congkham(KcbChidinhcl objChidinh)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.objChidinh = objChidinh;
            InitEvents();
        }

        private void InitEvents()
        {
            Load += frm_Dsach_Congkham_Load;
            KeyDown += frm_Dsach_Congkham_KeyDown;
            cmdClose.Click += cmdClose_Click;
          
            grdRegExam.MouseDoubleClick += GrdRegExam_MouseDoubleClick;
        }

        private void GrdRegExam_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
              
            }
            catch (Exception)
            {

            }
        }

        private void frm_Dsach_Congkham_Load(object sender, EventArgs e)
        {
            try
            {
              
                LayDanhsachCongkham();
                
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void LayDanhsachCongkham()
        {
            
          DataTable  m_dtDangkyPhongkham = new KCB_DANGKY().LayDsachCongkhamDadangki(objChidinh != null? objChidinh.MaLuotkham:"", objChidinh != null? objChidinh.IdBenhnhan:-1, 0);
            Utility.SetDataSourceForDataGridEx(grdRegExam, m_dtDangkyPhongkham, false, true, "", "stt_tt37");
           

        }
        
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

       
       
        private void frm_Dsach_Congkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
          
            else if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        private DataTable _mDtDanhsachDichvuKcb = new DataTable();
       

        private void optChange_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam))
                {
                    Utility.ShowMsg("Bạn cần chọn công khám trước khi thực hiện chuyển phiếu chỉ định vào");
                    return;
                }
                SPs.KcbChuyenChiDinhKhongQuaKhamVaoCongKham(objChidinh.IdChidinh, Utility.Int64Dbnull(grdRegExam.GetValue("id_kham"))).Execute();
                Utility.ShowMsg("Đã chuyển Phiếu chỉ định không qua khám vào phòng khám thành công.");
                mv_blnCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}