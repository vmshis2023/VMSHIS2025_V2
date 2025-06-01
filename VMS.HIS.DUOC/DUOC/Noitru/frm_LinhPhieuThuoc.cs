using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.UI.QMS;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.BENH_AN;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using VNS.Libs.AppUI;
using VNS.UCs;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_LinhPhieuThuoc : Form
    {
        public string madoituong { get; set; }
        public int id_khoXuat { get; set; }
        public bool IsPhieuBoSung { get; set; }
        public int idKhoaLinh { get; set; }
        public string loaiphieu { get; set; }
        public frm_LinhPhieuThuoc()
        {
            InitializeComponent();

        }

        private void frm_CapNhapPhatThuoc_Load(object sender, EventArgs e)
        {
            getData();
        }
        private void getData()
        {
            TPhieuCapphatNoitru objPhieuCapphat = TPhieuCapphatNoitru.FetchByID(Utility.Int32Dbnull(txtID_CAPPHAT.Text));
            if (objPhieuCapphat != null)
            {
                txtID_CAPPHAT.Text = Utility.sDbnull(objPhieuCapphat.IdCapphat);
                dtNgayCapPhat.Value = dtNgayCapPhat.Value;
                txtID_KHOA_LINH.Text = Utility.sDbnull(objPhieuCapphat.IdKhoaLinh);
                idKhoaLinh = Utility.Int32Dbnull(objPhieuCapphat.IdKhoaLinh);
                DmucKhoaphong objLDepartment = DmucKhoaphong.FetchByID(objPhieuCapphat.IdKhoaLinh);
                if (objLDepartment != null)
                {
                    txtTen_KHOA_LINH.Text = Utility.sDbnull(objLDepartment.TenKhoaphong);
                }
                //chkIsBoSung.Checked = Convert.ToBoolean(objPhieuCapphat.LinhBSung);

                IsPhieuBoSung =Utility.Byte2Bool(objPhieuCapphat.LoaiPhieu);
                radLinhBoSung.Checked = IsPhieuBoSung;
                loaiphieu = Utility.sDbnull(objPhieuCapphat.LoaiPhieu);
                radThuoc.Checked = loaiphieu == "THUOC";
                radLinhVTYT.Checked = loaiphieu == "VT";
                txtID_NVIEN.Text = Utility.sDbnull(objPhieuCapphat.IdNhanviencapphat);
                DmucNhanvien objStaff = DmucNhanvien.FetchByID(objPhieuCapphat.IdNhanviencapphat);
                if (objStaff != null)
                {
                    txtTen_NVIEN.Text = Utility.sDbnull(objStaff.TenNhanvien);
                }
                txtId_KhoXuat.Text = Utility.sDbnull(objPhieuCapphat.IdKhoXuat);
                id_khoXuat = Utility.Int32Dbnull(objPhieuCapphat.IdKhoXuat);
                DKho objKho = DKho.FetchByID(objPhieuCapphat.IdKhoXuat);
                if (objKho != null)
                {
                    txtTenKho.Text = Utility.sDbnull(objKho.TenKho);
                }
                madoituong = Utility.sDbnull(objPhieuCapphat.MaDoiTuong);
                txtMaDoiTuong.Text = Utility.sDbnull(madoituong);
                SqlQuery sqlQuery = new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(madoituong);
                LObjectType objectType = sqlQuery.ExecuteSingle<LObjectType>();
                if (objectType != null) txtObjectType_Name.Text = Utility.sDbnull(objectType.ObjectTypeName);
                txtMOTA_THEM.Text = Utility.sDbnull(objPhieuCapphat.MotaThem);
            }
            else
            {
                dtNgayCapPhat.Value = BusinessHelper.GetSysDateTime();
                txtID_KHOA_LINH.Text = Utility.sDbnull(idKhoaLinh);
                LDepartment objLDepartment = LDepartment.FetchByID(idKhoaLinh);
                if (objLDepartment != null)
                {
                    txtTen_KHOA_LINH.Text = Utility.sDbnull(objLDepartment.DepartmentName);
                }
                txtID_NVIEN.Text = Utility.sDbnull(globalVariables.gv_StaffID);
                LStaff objStaff = LStaff.FetchByID(globalVariables.gv_StaffID);
                if (objStaff != null)
                {
                    txtTen_NVIEN.Text = Utility.sDbnull(objStaff.StaffName);
                }

                txtId_KhoXuat.Text = Utility.sDbnull(id_khoXuat);
                DKho objKho = DKho.FetchByID(id_khoXuat);
                if (objKho != null)
                {
                    txtTenKho.Text = Utility.sDbnull(objKho.TenKho);
                }
                //loaiphieu = Utility.sDbnull(objPhieuCapphat.LoaiPhieu);
                radThuoc.Checked = loaiphieu == "THUOC";
                radLinhVTYT.Checked = loaiphieu == "VT";
                radLinhBoSung.Checked = IsPhieuBoSung;
                // IsPhieuBoSung = Convert.ToBoolean(objPhieuCapphat.LinhBSung);
                txtMaDoiTuong.Text = Utility.sDbnull(madoituong);
                SqlQuery sqlQuery = new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(madoituong);
                LObjectType objectType = sqlQuery.ExecuteSingle<LObjectType>();
                if (objectType != null) txtObjectType_Name.Text = Utility.sDbnull(objectType.ObjectTypeName);
            }
            LoadPhieuDonThuoc();
        }
        private DataTable m_dtDonThuoc=new DataTable();
        private void LoadPhieuDonThuoc()
        {
            m_dtDonThuoc =
                SPs.DuocTimDonThuocDeChiaBn(Utility.Int32Dbnull(txtID_CAPPHAT.Text,-100)).GetDataSet().Tables[0];

                

            Utility.SetDataSourceForDataGridEx(grdPres, m_dtDonThuoc, true, true, "IsChon=0", "");
            //Modifycommand();
        }
        /// <summary>
        /// hàm thực hiện việc phát thuốc cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPresDetail_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
           
        }
        private DataTable m_dtPhieuCapPhatLichSuCT = new DataTable();
        private DataSet ds = new DataSet();
        /// <summary>
        /// hàm thực hiện việc load thông tin cấp phát chi tiết
        /// </summary>
        private void LoadPhieuCapPhatCT()
        {
            if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
            {
                int idcapphat = Utility.Int32Dbnull(grdPres.GetValue(TPhieuCapphatNoitru.Columns.IdCapphat));
                int Id_donThuoc = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                m_dtPhieuCapPhatLichSuCT = SPs.DuocCapPhatPhieuCapPhatCT(Id_donThuoc, idcapphat).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtPhieuCapPhatLichSuCT,true,true,"1=1","");


            }
            else
            {
                grdPresDetail. DataSource = null;
                //grdChiTietCapPhat.DataSource = null;
            }
        }
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            LoadPhieuCapPhatCT();
        }
        /// <summary>
        /// hàm thực hiện viecj phát thuốc hết cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPhatHet_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow grdRow in grdPresDetail.GetDataRows())
            {
                grdRow.BeginEdit();
                grdRow.Cells[TPhieuCapphatNoitruCt.Columns.DaLinh].Value = 1;
                grdRow.EndEdit();
            }
        }

        private void grdPresDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void cmdLuuLai_Click(object sender, EventArgs e)
        {
         
            foreach (Janus.Windows.GridEX.GridEXRow grdExRow in grdPresDetail.GetDataRows())
            {
                int IdCapPhatCt = Utility.Int32Dbnull(grdExRow.Cells[TPhieuCapphatNoitruCt.Columns.IdCapPhatCt].Value);
                int presdetail_id = Utility.Int32Dbnull(grdExRow.Cells[TPhieuCapphatNoitruCt.Columns.IdDonthuocCtiet].Value);
                int soluong = Utility.Int32Dbnull(grdExRow.Cells[TPhieuCapphatNoitruCt.Columns.SoLuong].Value);
                int DaLinh = Utility.Int32Dbnull(grdExRow.Cells[TPhieuCapphatNoitruCt.Columns.DaLinh].Value);
               // int value = Utility.Int32Dbnull(e.Value);
                StoredProcedure sp = SPs.DuocUpdateLinhSoLuongThuoc(IdCapPhatCt, presdetail_id, DaLinh);
                sp.Execute();
                var query = from thuoc in m_dtPhieuCapPhatLichSuCT.AsEnumerable()
                    where
                        Utility.Int32Dbnull(TPhieuCapphatNoitruCt.Columns.IdCapPhatCt) ==
                        Utility.Int32Dbnull(grdExRow.Cells[TPhieuCapphatNoitruCt.Columns.IdCapPhatCt].Value)
                    select thuoc;
                if (query.Any())
                {
                    var firstrow = query.FirstOrDefault();
                    if (firstrow != null)
                    {
                        firstrow[TPrescriptionDetail.Columns.SluongLinh] = soluong == 1 ? soluong : 0;
                        firstrow.AcceptChanges();
                    }
                }
                grdPresDetail.UpdateData();
            }
              

           
        }

        private void chkChuaPhat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string _rowFilter = "1=1";
                if (chkChuaPhat.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", TPhieuCapphatNoitruCt.Columns.DaLinh, false);

                }
                m_dtPhieuCapPhatLichSuCT.DefaultView.RowFilter = _rowFilter;
            }
            catch (Exception)
            {
                
                //throw;
            }
          
        }

        private void chkDaLinh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string _rowFilter = "1=1";
                if (chkChuaPhat.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", TPhieuCapphatNoitruCt.Columns.DaLinh, true);

                }
                m_dtPhieuCapPhatLichSuCT.DefaultView.RowFilter = _rowFilter;
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
}
