using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Collections.Generic;
using VNS.HIS.UI.Classess;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_canhbaodongtamung : Form
    {
        private NLog.Logger log;
        public frm_canhbaodongtamung()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            log = LogManager.GetCurrentClassLogger();
            dtToDate.Value = dtFromDate.Value = THU_VIEN_CHUNG.GetSysDateTime();
            cboCondition.SelectedIndex = 0;
            txtSotientu.TextChanged += txtSotientu_TextChanged;
            txtSotienden.TextChanged += txtSotienden_TextChanged;
        }

        void txtSotienden_TextChanged(object sender, EventArgs e)
        {
            getDieukienchenhlech();
        }

        void txtSotientu_TextChanged(object sender, EventArgs e)
        {
            getDieukienchenhlech();
        }

        //public frm_canhbaodongtamung(string sArg)
        //{
        //    InitializeComponent();
        //    this.thamso = sArg;
        //    log = LogManager.GetCurrentClassLogger();
        //    dtToDate.Value = dtFromDate.Value = THU_VIEN_CHUNG.GetSysDateTime();
        //    cboCondition.SelectedIndex = 0;
        //}
        
        private void frm_canhbaodongtamung_Load(object sender, EventArgs e)
        {
            InitData();
            SearchthongTin();
            ModifyCommand();
        }
        private void InitData()
        {
            try
            {
                DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName,Utility.Bool2byte( globalVariables.IsAdmin),(byte)1);
                txtKhoanoitru.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                if (Utility.Coquyen("tamung_canhbao_chonkhoa"))
                    txtKhoanoitru.Enabled = true;
                else
                {
                   
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh khoi tao khoa noi tru =" + exception);
            }
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiesm thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if ((Utility.sDbnull( cboCondition.SelectedValue,"") == "0" || Utility.sDbnull( cboCondition.SelectedValue,"") == "1") && Utility.sDbnull(txtSotientu.Text,"").Length<=0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin số tiền chênh lệnh giữa Tổng tạm ứng - Tổng chi phí");
                txtSotienden.Focus();
                return;
            }
            if (Utility.sDbnull(cboCondition.SelectedValue, "") == "2" && Utility.sDbnull(txtSotientu.Text, "").Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin số tiền chênh lệnh từ (Tổng tạm ứng - Tổng chi phí)");
                txtSotienden.Focus();
                return;
            }
            if (Utility.sDbnull(cboCondition.SelectedValue, "") == "2" && Utility.sDbnull(txtSotienden.Text, "").Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin số tiền chênh lệnh đến (Tổng tạm ứng - Tổng chi phí)");
                txtSotienden.Focus();
                return;
            }
            SearchthongTin();
        }

        private DataTable m_dtDataSearch = new DataTable();
        private void ModifyCommand()
        {
            cmdIn.Enabled = grdList.RowCount > 0;
        }
        private DataTable m_dtCanhBaoGoiDv=new DataTable();
        private void SearchthongTin()
        {
            int Status = -1;
            byte noitru=100;
            if(optNoitru.Checked) noitru=1;
            if(optNgoaitru.Checked) noitru=0;
            m_dtDataSearch =
                SPs.NoitruCanhbaoTamung(chkCreateDate.Checked ? dtFromDate.Value
                                                           : Convert.ToDateTime("01/01/1900"),
                                                           chkCreateDate.Checked ? dtToDate.Value : Convert.ToDateTime("01/01/1900"), txtPatientCode.Text, -1, txtTenBN.Text,
                                                           Utility.Bool2byte(chkTinhngoaitru.Checked),Utility.Int32Dbnull( txtKhoanoitru.MyID,-1),Utility.sDbnull( cboCondition.SelectedValue,"-1")
                                                           , Utility.DecimaltoDbnull(txtSotientu.Text), Utility.DecimaltoDbnull(txtSotienden.Text), noitru).GetDataSet().Tables[0];
         
            Utility.SetDataSourceForDataGridEx(grdList,m_dtDataSearch,true,true,"1=1","");
            ModifyCommand();
        }
        string getDieukienchenhlech()
        {
            if (Utility.sDbnull(cboCondition.SelectedValue, "") == "-1") return "Tất cả";
            else if (Utility.sDbnull(cboCondition.SelectedValue, "") == "0") return string.Format("Tổng tiền tạm ứng -Tổng chi phí >={0}", txtSotientu.Text);
            else if (Utility.sDbnull(cboCondition.SelectedValue, "") == "1") return string.Format("Tổng tiền tạm ứng -Tổng chi phí <={0}", txtSotientu.Text);
            else return string.Format("Tổng tiền tạm ứng -Tổng chi phí trong khoảng:{0}-{1}",txtSotientu.Text,txtSotienden.Text);

        }
        private void cmdINDANHSACH_Click(object sender, EventArgs e)
        {
            try
            {


                THU_VIEN_CHUNG.CreateXML(m_dtDataSearch, "noitru_canhbaotientamung.xml");
                if (m_dtDataSearch == null || m_dtDataSearch.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để in", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string Condition = string.Format("{0} - Khoa điều trị : {1} - Chênh lệch :{2}", chkCreateDate.Checked ? string.Format("") : "Từ ngày đến ngày: Tất cả", txtKhoanoitru.MyID == "-1" ? "Tất cả" : txtKhoanoitru.Text, getDieukienchenhlech());
                                       
                noitru_inphieu.InPhieu(m_dtDataSearch, DateTime.Now, Condition,true, "noitru_canhbaotientamung");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_canhbaodongtamung_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.F3)cmdSearch.PerformClick();
            if(e.KeyCode==Keys.F4)cmdIn.PerformClick();
        }

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void uiTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void uiButton2_Click(object sender, EventArgs e)
        {
            string TNDN = string.Format("Từ ngày {0} đến ngày {1}", dtFromDate.Text, dtToDate.Text);
            if(m_dtCanhBaoGoiDv.Rows.Count<=0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào","Thông báo",MessageBoxIcon.Warning);
                return;
            }
          //  VietBaIT.HISLink.Reports_TAMPHUC.INPHIEU_CLASS.INPHIEU.TAMPHUC_INDANHSACH_CANHBAO_GOI_DVU(m_dtCanhBaoGoiDv, txtTIEUDE_GOIDV.Text, TNDN);
        }

        private void mnuInTach_Click(object sender, EventArgs e)
        {
            //string PatientCode = Utility.sDbnull(grdList.CurrentRow.Cells[TPatientExam.Columns.PatientCode].Value);
            ////string PatientName = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.PatientName].Value);
            ////string PatientAddr = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.PatientAddr].Value);
         
            ////string Year_Of_Birth = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.YearOfBirth].Value);
            ////string PatientSex = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.PatientSex].Value);


            //DataTable  m_dtInTachCanhBao =
            //  SPs.TamphucTimkiemDanhsachBnhanCanhbao(string.Empty,
            //                                         chkCreateDate.Checked
            //                                             ? dtFromDate.Value
            //                                             : Convert.ToDateTime("01/01/1900"),
            //                                         chkCreateDate.Checked
            //                                             ? dtToDate.Value
            //                                             : BusinessHelper.GetSysDateTime(),
            //                                             Utility.Int32Dbnull(cboKhoa.SelectedValue, -1), Utility.sDbnull(cboCondition.SelectedValue),
            //                                                              Utility.DecimaltoDbnull(txtSoTien.Text, 0), Utility.sDbnull(PatientCode,"-1")).GetDataSet().
            //      Tables[0];
            //VietBaIT.HISLink.Business.Reports.Implementation.BC_Inphieu_KyHoanQui.InTachCanhBaoKyQui(m_dtInTachCanhBao);

        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", "Cảnh báo tạm ứng chi phí điều trị nội trú");
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    var fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
            }
        }

        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSotienden.Enabled = Utility.sDbnull(cboCondition.SelectedValue, "-1") == "2";
            Utility.SetMsg(lblDiengiaiChenhlech, getDieukienchenhlech(), false);
        }
    }
}
