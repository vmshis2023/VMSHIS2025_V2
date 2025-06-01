using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using VietBaIT.CommonLibrary;
using SubSonic;
using VietBaIT.HISLink.DataAccessLayer;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using VietBaIT.HISLink.Report_LaoKhoa;
using Microsoft.VisualBasic;
using System.IO;
using TriState = Janus.Windows.GridEX.TriState;

namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu_NoiTru
{
    public partial class frm_PhieuPhat_TuTruc : Form
    {
        private int Distance = 488;
        private bool b_Hasloaded = false;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuXuatBN.txt"));
        private DataTable m_dtDataDonThuoc = new DataTable();
        private DataTable m_dtDataPresDetail = new DataTable();
        private HisDuocProperties HisDuocProperties;
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        public frm_PhieuPhat_TuTruc()
        {
            InitializeComponent();
            Utility.loadIconToForm(this);
            dtFromdate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now;
            splitContainer1.SplitterMoved += new SplitterEventHandler(splitContainer1_SplitterMoved);
            grdPresDetail.FormattingRow+=new RowLoadEventHandler(grdPresDetail_FormattingRow);
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            dtNgayPhatThuoc.Value = BusinessHelper.GetSysDateTime();
            CauHinh();
            cmdConfig.Visible = globalVariables.IsAdmin;
        }

        private void CauHinh()
        {
            HisDuocProperties = Utility.HisDuocProperties;
          
            cmdHuyDonThuoc.Visible = HisDuocProperties.HuyXacNhan;
            cmdXoaChiTietThuoc.Visible = HisDuocProperties.Chophepxoachitiet;
          
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdPres.RootTable.Columns[TPrescription.Columns.PresId];
            Janus.Windows.GridEX.GridEXSortKey gridExSortKey = new GridEXSortKey(gridExColumn, SortOrder.Ascending);
            grdPres.RootTable.SortKeys.Add(gridExSortKey);
            if (HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
            {
                Utility.VisiableGridEx(grdPres, TPrescription.Columns.Status, true);
                Janus.Windows.GridEX.GridEXColumn gridExColumnFormat = grdPres.RootTable.Columns[TPrescription.Columns.Status];
                Janus.Windows.GridEX.GridEXFormatCondition gridExFormatCondition = new GridEXFormatCondition(gridExColumnFormat, ConditionOperator.Equal, 1);
                gridExFormatCondition.FormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                grdPres.RootTable.FormatConditions.Add(gridExFormatCondition);
            }
            else
            {
                Utility.VisiableGridEx(grdPres, TPrescription.Columns.Status, false);
            }
            dtNgayPhatThuoc.Enabled = HisDuocProperties.ChoPhepChinhNgayDuyet;
            chkLocNgayKhiDuyet.Checked = HisDuocProperties.LocDonThuocKhiDuyet;

            //LoadLayout();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc lưu kéo ra kheo vào của cửa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

            if (!b_Hasloaded) return;
            SplitterDistance = splitContainer1.SplitterDistance;
            if (System.IO.File.Exists(FileName))
            {
                WriteSlipterContaiter();
            }
        }
        /// <summary>
        /// hàm thực hiên viecj viết thông tin của sliper khi di chuyển vào file text
        /// </summary>
        private void WriteSlipterContaiter()
        {
            SplitterDistance = splitContainer1.SplitterDistance;
            System.IO.File.WriteAllText(FileName, SplitterDistance.ToString());
        }

        private void ReadSliper()
        {
            if (System.IO.File.Exists(FileName))
            {
                SplitterDistance = Utility.Int32Dbnull(System.IO.File.ReadAllLines(FileName)[0]);
            }
            else
            {
                WriteSlipterContaiter();
            }
            splitContainer1.SplitterDistance = SplitterDistance;
        }
        private DataTable m_dtKhothuoc=new DataTable();
        private void frm_PhieuXuat_BNhan_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombox(cboObjectType, LoadDataCommon.CommonBusiness.LayThongTin_DoiTuong(),
                                       LObjectType.Columns.ObjectTypeId, LObjectType.Columns.ObjectTypeName);
            m_dtKhothuoc = LoadDataCommon.CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TUTRUC_NOITRU_THEOKHOA(globalVariables.DepartmentID);
           
            DataBinding.BindData(cboKho, m_dtKhothuoc,DKho.Columns.IdKho, DKho.Columns.TenKho);
            b_Hasloaded = true;
            //ReadSliper();
            TimKiemThongTinDonThuoc();
            ModifyCommnad();

        }
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin của đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemThongTinDonThuoc();
        }

        private void TimKiemThongTinDonThuoc()
        {
            try
            {
                int Status = -1;
                if (radChuaXacNhan.Checked) Status = 0;
                if (radDaXacNhan.Checked) Status = 1;
              
                string MaKho = "-1";
                var querykho = (from kho in m_dtKhothuoc.AsEnumerable().Cast<DataRow>()
                                let y = Utility.sDbnull(kho[DKho.Columns.IdKho])
                                select y).ToArray();
                if (querykho.Count() > 0)
                {
                    MaKho = string.Join(",", querykho);
                }

                m_dtDataDonThuoc =
                    SPs.DuocTimkiemThongtinDonthuocKhoxuatTuTruc(Utility.Int32Dbnull(txtPres_ID.Text, -1),txtPID.Text,
                                                           Utility.sDbnull(txtTenBN.Text),
                                                           Utility.Int32Dbnull(cboObjectType.SelectedValue, -1),
                                                           chkByDate.Checked
                                                               ? dtFromdate.Value
                                                               : Convert.ToDateTime("01/01/1900"),
                                                           chkByDate.Checked
                                                               ? dtToDate.Value
                                                               : BusinessHelper.GetSysDateTime(), Status,
                                                           Utility.Int32Dbnull(cboKho.SelectedValue), HisDuocProperties.KieuDuyetDonThuoc).
                        GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx(grdPres, m_dtDataDonThuoc, true, true, "1=1", "Pres_ID asc");
                RowFilterView();
                Utility.SetMessage(lblMessage, string.Format("Tổng số đơn thuốc :{0}", m_dtDataDonThuoc.Rows.Count), true);
                ModifyCommnad();
            }
            catch (Exception exception)
            {
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
               
            }
            finally
            {
                ModifyCommnad();
            }
           
        }
        /// <summary>
        /// hàm thực hiện việc lọc filter của dược
        /// </summary>
        private void RowFilterView()
        {
            if(chkLocNgayKhiDuyet.Checked)
            {
                string rowFilter = "1=1";

                if (radChuaXacNhan.Checked) rowFilter = string.Format("{0}={1}", TPrescription.Columns.Status, 0);
                if (radDaXacNhan.Checked) rowFilter = string.Format("{0}={1}", TPrescription.Columns.Status, 1);
                try
                {
                    m_dtDataDonThuoc.DefaultView.RowFilter = rowFilter;
                    m_dtDataDonThuoc.AcceptChanges();
                }
                catch (Exception)
                {

                    // throw;
                }
            }
            
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPres_ID_Click(object sender, EventArgs e)
        {

        }

        private void txtPres_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if(Utility.Int32Dbnull(txtPres_ID.Text)>0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdSearch.PerformClick();
                }
            }
           
        }
        /// <summary>
        /// hàm thực hiện việc dichuyeen thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
                {
                    lblmsg.Visible = false;
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                    int id_kho = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.StockId));
                    // dtNgayPhatThuoc.Value = Convert.ToDateTime(grdPres.GetValue(TPrescription.Columns.PresDate));
                    if (HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        var query = from kho in m_dtKhothuoc.AsEnumerable().AsEnumerable()
                                    where Utility.sDbnull(kho[DKho.Columns.IdKho]) == Utility.sDbnull(id_kho)
                                    select kho;
                        if (query.Count() > 0)
                            cboKho.SelectedValue = Utility.sDbnull(id_kho);
                        else
                        {
                            if (cboKho.Items.Count > 0)
                                cboKho.SelectedIndex = 0;
                        }

                    }

                    GetDataPresDetail();
                }
                else
                {
                    grdPresDetail.DataSource = null;
                }
              
            }
            catch (Exception)
            {
                
                
            }
            ModifyCommnad();
        }
        private void ModifyCommnad()
        {
            try
            {
                int _status = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.Status));
                cmdInDonThuoc.Enabled = grdPres.RowCount > 0 && grdPres.CurrentRow.RowType == RowType.Record;
                cmdPhatThuoc.Enabled =  grdPres.RowCount > 0 && grdPres.CurrentRow.RowType == RowType.Record;
                if (HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                {
                    if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
                    {
                        cmdHuyDonThuoc.Enabled = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.Status)) == 1;
                        cmdPhatThuoc.Enabled = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.Status)) == 0;
                        cmdXoaChiTietThuoc.Enabled = grdPresDetail.RowCount > 0 && Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.Status)) == 0;
                    }
                    else
                    {
                        cmdHuyDonThuoc.Enabled = cmdPhatThuoc.Enabled = false;
                        cmdXoaChiTietThuoc.Enabled = false;
                    }
                }
                else
                {
                     if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
                     {
                         //var query = from thuoc in grdPresDetail.GetDataRows().AsEnumerable()
                         //    let y = thuoc.RowType == RowType.Record
                         //    where
                         //        Utility.Int32Dbnull(thuoc.Cells[TPrescriptionDetail.Columns.HasComfirm]) == 1 &&
                         //        Utility.Int32Dbnull(thuoc.Cells[TPrescriptionDetail.Columns.StockId]) ==
                         //        Utility.Int32Dbnull(cboKho.SelectedValue)
                         //    select thuoc;
                         //cmdPhatThuoc.Enabled = query.Count() <= 0;
                         //cmdHuyDonThuoc.Enabled = query.Count() > 0;
                         cmdHuyDonThuoc.Enabled = _status==1;
                         cmdPhatThuoc.Enabled = !cmdHuyDonThuoc.Enabled;
                         cmdPhatThuoc.Enabled = cmdPhatThuoc.Enabled;
                         cmdXoaChiTietThuoc.Enabled = grdPresDetail.RowCount > 0;
                     }
                     else
                     {
                         cmdPhatThuoc.Enabled = cmdHuyDonThuoc.Enabled = false;
                     }
                  
                }

                
            }
            catch (Exception)
            {
                
               
            }
           

        }
        private int Pres_ID=-1;
        private void GetDataPresDetail()
        {

            int stock_id = -1;
            if(HisDuocProperties.KieuDuyetDonThuoc=="DONTHUOC")
            {
                stock_id = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.StockId));
            }
            else
            {
                stock_id = Utility.Int32Dbnull(cboKho.SelectedValue);
            }
            m_dtDataPresDetail = SPs.DuocLaythongtinDonthuocLinhthuoc(Pres_ID, stock_id).GetDataSet().Tables[0];
            if (!m_dtDataPresDetail.Columns.Contains("CHON")) m_dtDataPresDetail.Columns.Add("CHON", typeof(int));
            foreach (DataRow dr in m_dtDataPresDetail.Rows)
            {
                dr["CHON"] = 0;
            }
            m_dtDataPresDetail.AcceptChanges();
            string _rowFilter = string.Format("Pres_ID={0}", Pres_ID);
            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDataPresDetail, false, true, "1=1",
                                                 TPrescriptionDetail.Columns.ThuTuIn);
            Janus.Windows.GridEX.GridEXColumn gridExColumn =
               grdPresDetail.RootTable.Columns[TPrescriptionDetail.Columns.ThuTuIn];
            Janus.Windows.GridEX.GridEXSortKey gridExSortKey = new GridEXSortKey(gridExColumn, SortOrder.Ascending);
            grdPresDetail.RootTable.SortKeys.Add(gridExSortKey);
            // grdPresDetail.DataSource = p_dtPresDetail.DefaultView;
      
            ///thực hiện việc thay đổi khi load thông tin của thành tiền
            UpdateDataWhenChanged();
            ModifyCommnad();
        }
        private void UpdateDataWhenChanged()
        {

            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["TT"].Value = Utility.DecimaltoDbnull(gridExRow.Cells["Price"].Value, 0) *
                                                  Utility.Int32Dbnull(gridExRow.Cells["Quantity"].Value, 0);
                    gridExRow.Cells["TotalSurcharge"].Value = Utility.DecimaltoDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SurchargePrice].Value, 0) *
                                                 Utility.Int32Dbnull(gridExRow.Cells["Quantity"].Value, 0);
                    gridExRow.EndEdit();


                }
                grdPresDetail.UpdateData();
                m_dtDataPresDetail.AcceptChanges();
             

                //  sMoneyLetter.Text = _moneyByLetter.sMoneyToLetter(toolTong.Text);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                // throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phím tắt thực hiện tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuXuat_BNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F2) 
            {
                txtPID.Clear();
                txtPID.Focus();
            }
            if (e.KeyCode == Keys.F5)
            {
                grdPres_SelectionChanged(grdPres, new EventArgs());
            }
            if (e.Control && e.KeyCode == Keys.S) cmdPhatThuoc_Click(cmdPhatThuoc, new EventArgs());
        }

        private TPatientExam createPatientExam(TPrescription objPrescription)
        {
            SqlQuery sqlQuery = new Select().From(TPatientExam.Schema).Where(
                        TPatientExam.Columns.PatientCode).IsEqualTo(objPrescription.PatientCode)
                        .And(TPatientExam.Columns.PatientId).IsEqualTo(objPrescription.PatientId);
            TPatientExam objPatientExam = sqlQuery.ExecuteSingle<TPatientExam>();
            return objPatientExam;
        }

        private bool HasXacNhap = false;
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin theo đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPhatThuoc_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblmsg, "", false);
           
            if(HisDuocProperties.KieuDuyetDonThuoc=="DONTHUOC")
            {
                if (!InValiDonthuoc()) return;
            }

            try
            {
                if (!InValiKiemTraDonThuoc()) return;
                //if(Utility.AcceptQuestion("Bạn có muốn thực hiện việc xác nhận lĩnh thuốc cho bệnh nhân không\n Nếu bạn xác nhận thuốc sẽ trừ vào kho mà bạn đang chọn","thông báo",true))
                //{
                Utility.EnableButton(cmdPhatThuoc, false);
                HasXacNhap = true;
                if(!HasXacNhap)return;
                int Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId), -1);
                TPrescription objPrescription = TPrescription.FetchByID(Pres_ID);
                if(HisDuocProperties.PhatThuocTrongNgay)
                {
                    if ((objPrescription.CreatedDate.Date != BusinessHelper.GetSysDateTime().Date))
                    {
                        if(!Utility.AcceptQuestion("Đơn thuốc được kê khác ngày hiện tại, BẠN CÓ MUỐN PHÁT THUỐC(nhấn Yes)\n Nếu không phát thuốc: yêu cầu bệnh nhân hủy phơi và kê lại đơn thuốc khác(Nhấn No)!","Cảnh báo",true))
                       return;
                             

                    }
                   
                }
                DPhieuXuatBnhan objXuatBnhan = CreatePhieuXuatBenhNhan(objPrescription);
                dtNgayPhatThuoc.Value = BusinessHelper.GetSysDateTime();
                objXuatBnhan.NgayXuatBn = dtNgayPhatThuoc.Value;
                TPatientExam objPatientExam = createPatientExam(objPrescription);
                if (objPatientExam != null)
                {
                    ActionResult actionResult =
                   new VietBaIT.HISLink.Business.Duoc.XuatThuoc.XuatThuoc().LinhThuocBenhNhan(objPrescription, objPatientExam, CreatePresDetail(),
                                                                                              objXuatBnhan);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            UpdateHasConfirm();
                            HasXacNhap = false;
                            Utility.SetMsg(lblmsg, "Bạn thực hiện việc phát thuốc thành công", true);
                            Utility.EnableButton(cmdPhatThuoc, true);
                            
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình phát thuốc cho bệnh nhân", "Thông báo",MessageBoxIcon.Error);
                            break;
                        case ActionResult.PresIsConfirmed:
                            Utility.ShowMsg("Đơn thuốc này đã được cấp phát,Mời bạn chọn đơn thuốc lại hoặc nhần F5 để lấy lại dữ liệu","Thông báo",MessageBoxIcon.Warning);
                            break;
                    }
                }
               
                //}
            }
            catch (Exception)
            {

                HasXacNhap = false;
            }
            finally
            {
                ModifyCommnad();
                HasXacNhap = false;
                Utility.EnableButton(cmdPhatThuoc, true);
            }
          
        }

        private void UpdateHasConfirm()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    int id_kho = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.StockId].Value);
                    int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.DrugId].Value);
                    int SoLuongTon = BusinessHelper.SoLuongTonTrongKho(id_kho, id_thuoc);
                    gridExRow.Cells["SL_Ton"].Value = SoLuongTon;
                    gridExRow.Cells[TPrescriptionDetail.Columns.HasComfirm].Value = 1;
                    gridExRow.EndEdit();
                }
                grdPresDetail.UpdateData();
                m_dtDataPresDetail.AcceptChanges();
                var query = from donthuoc in grdPresDetail.GetDataRows().AsEnumerable()
                            where
                                Utility.Int32Dbnull(donthuoc.Cells[TPrescriptionDetail.Columns.PresId].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                    SqlQuery sqlQuery1 = new Select().From(TPrescriptionDetail.Schema)
                        .Where(TPrescriptionDetail.Columns.PresId).IsEqualTo(Pres_ID)
                        .And(TPrescriptionDetail.Columns.HasComfirm).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[TPrescription.Columns.Status].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[TPrescription.Columns.Status].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                }

            }
            catch (Exception)
            {
                
                //throw;
            }

        }
        /// <summary>
        /// hàm thực hiện việc update thông tin hủy
        /// </summary>
        private void UpdateHuyHasConfirm()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    int id_kho = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.StockId].Value);
                    int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.DrugId].Value);
                    int SoLuongTon = BusinessHelper.SoLuongTonTrongKho(id_kho, id_thuoc);
                    gridExRow.Cells["SL_Ton"].Value = SoLuongTon;
                    gridExRow.Cells[TPrescriptionDetail.Columns.HasComfirm].Value = 0;
                    gridExRow.EndEdit();
                }
                grdPresDetail.UpdateData();
                m_dtDataPresDetail.AcceptChanges();
                var query = from donthuoc in grdPresDetail.GetDataRows().AsEnumerable()
                            where
                                Utility.Int32Dbnull(donthuoc.Cells[TPrescriptionDetail.Columns.PresId].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                    SqlQuery sqlQuery1 = new Select().From(TPrescriptionDetail.Schema)
                        .Where(TPrescriptionDetail.Columns.PresId).IsEqualTo(Pres_ID)
                        .And(TPrescriptionDetail.Columns.HasComfirm).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[TPrescription.Columns.Status].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[TPrescription.Columns.Status].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }
        private bool InValiDonthuoc()
        {
            
            int Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId), -1);
            SqlQuery sqlQuery = new Select().From(TPrescription.Schema)
                .Where(TPrescription.Columns.Status).IsEqualTo(1)
                .And(TPrescription.Columns.PresId).IsEqualTo(Pres_ID);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Đơn thuốc đã phát thuốc,Mời bạn xem lại thông tin ","Thông báo",MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool InValiHuyDonthuoc()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Không tìm thấy chi tiết đơn thuốc. Bạn cần chọn ít nhất 1 đơn thuốc có chi tiết để thao tác", "Thông báo", MessageBoxIcon.Error);

                return false;
            }
            int Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId), -1);
            SqlQuery sqlQuery = new Select().From(TPrescription.Schema)
                .Where(TPrescription.Columns.Status).IsEqualTo(0)
                .And(TPrescription.Columns.PresId).IsEqualTo(Pres_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đơn thuốc chưa phát thuốc,Mời bạn xem lại thông tin ", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// /hàm thực hiện việc khởi tạo thông tin của phiếu xuất cho bệnh nhân
        /// </summary>
        /// <param name="objPrescription"></param>
        /// <returns></returns>
        private DPhieuXuatBnhan CreatePhieuXuatBenhNhan(TPrescription objPrescription)
        {
            TPatientInfo objPatientInfo = TPatientInfo.FetchByID(objPrescription.PatientId);
            TPatientExam objPatientExam = new Select().From(TPatientExam.Schema)
                .Where(TPatientExam.Columns.PatientCode).IsEqualTo(objPrescription.PatientCode)
                .And(TPatientExam.Columns.PatientId).IsEqualTo(objPrescription.PatientId).ExecuteSingle<TPatientExam>();

            DPhieuXuatBnhan objPhieuXuatBnhan=new DPhieuXuatBnhan();
            objPhieuXuatBnhan.NgayXuatBn =BusinessHelper.GetSysDateTime();
            objPhieuXuatBnhan.IdKhoaCd = Utility.Int16Dbnull(objPrescription.DepartmentId);
            objPhieuXuatBnhan.IdBsyCd = Utility.Int16Dbnull(objPrescription.AssignId);
            objPhieuXuatBnhan.IdDonThuoc = Utility.Int32Dbnull(objPrescription.PresId);
            objPhieuXuatBnhan.IdNhanVien = globalVariables.gv_StaffID;
            objPhieuXuatBnhan.HienThi = 1;
            objPhieuXuatBnhan.ChanDoan = Utility.sDbnull(objPatientExam.ChanDoanChinh);
            objPhieuXuatBnhan.MaBenh = Utility.sDbnull(objPatientExam.MBenhChinh);
            objPhieuXuatBnhan.IdDoiTuong = Utility.Int16Dbnull(objPatientExam.ObjectTypeId);
            objPhieuXuatBnhan.Gtinh = Utility.ByteDbnull(objPatientInfo.PatientSex);
            objPhieuXuatBnhan.TenBnhan = Utility.sDbnull(objPatientInfo.PatientName);
            objPhieuXuatBnhan.TenKhongDau = Utility.sDbnull(Utility.UnSignedCharacter(objPatientInfo.PatientName));
            objPhieuXuatBnhan.DiaChi = Utility.sDbnull(objPatientInfo.PatientAddr);
            objPhieuXuatBnhan.NamSinh = Utility.Int32Dbnull(objPatientInfo.YearOfBirth);
            objPhieuXuatBnhan.SoThe = Utility.sDbnull(objPatientExam.InsuranceNum);
            objPhieuXuatBnhan.NgayKeDon = objPrescription.PresDate;
            objPhieuXuatBnhan.NgayTao = DateTime.Now;
            objPhieuXuatBnhan.NguoiTao = globalVariables.UserName;
            objPhieuXuatBnhan.LoaiDthuoc = 1;
            objPhieuXuatBnhan.TrangThai = 1;
            objPhieuXuatBnhan.LoaiPhieu = (byte?) LoaiPhieu.PhieuXuatKhoBenhNhan;
           // objPhieuXuatBnhan. = (byte?) LoaiPhieu.PhieuXuatKhoBenhNhan;
            switch (HisDuocProperties.KieuDuyetDonThuoc)
            {
                case "DONTHUOC":
                    objPhieuXuatBnhan.IdKhoXuat = Utility.Int16Dbnull(objPrescription.StockId);
                    break;
                case "CHITIET":
                    objPhieuXuatBnhan.IdKhoXuat = Utility.Int16Dbnull(cboKho.SelectedValue);
                    break; 
                default:
                    objPhieuXuatBnhan.IdKhoXuat = Utility.Int16Dbnull(objPrescription.StockId);
                    break;
            }
            
            return objPhieuXuatBnhan;
        }
        private DPhieuXuatBnhanCt []CreatePhieuXuaChiTiet()
        {
            int length = 0;
            int idx = 0;
            var arrPhieuXuatCT = new DPhieuXuatBnhanCt[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    arrPhieuXuatCT[idx]=new DPhieuXuatBnhanCt();
                    arrPhieuXuatCT[idx].ChiDan =Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SDesc].Value);
                    arrPhieuXuatCT[idx].SoLuong = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.Quantity].Value);
                    arrPhieuXuatCT[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.DrugId].Value,-1);
                   // arrPhieuXuatCT[idx].PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SurchargePrice].Value);
                    arrPhieuXuatCT[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.Price].Value);
                 
                    
                    idx++;
                }
            }
            return arrPhieuXuatCT;
        }

        private void cmdInDonThuoc_Click(object sender, EventArgs e)
        {
            Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
            PrintPres(Pres_ID);
        }
        /// <summary>
        /// hàm thực hiện việc in đơn thuốc
        /// </summary>
        /// <param name="PresID"></param>
        private void PrintPres(int PresID)
        {

            DataTable v_dtData = SPs.SearchPrescriptionForPrinting(PresID).GetDataSet().Tables[0];
            if (v_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thuốc, Có thể bạn chưa lưu được thuốc, \nBạn phải lưu đơn thuốc và thực hiện in đơn thuốc", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            PresID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
            barcode.Data = Utility.sDbnull(PresID);
            byte[] Barcode = Utility.GenerateBarCode(barcode);
            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
            }
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (radA4.Checked) KhoGiay = "A4";
            string path = "";
          
            string tieude = "ĐƠN THUỐC";
            string madoituong = Utility.sDbnull(grdPres.GetValue("MA_DTUONG"));
            tieude = BusinessHelper.LayTieuDePhieuThuoc(madoituong);
            if (string.IsNullOrEmpty(tieude)) tieude = "ĐƠN THUỐC";
            VietBaIT.HISLink.Business.Reports.Implementation.InDonThuoc.PrintPres(v_dtData, KhoGiay, tieude,BusinessHelper.GetSysDateTime());
        }
        private string getPath (string PathReport)
        {
             string path = string.Format("{0}{1}", globalVariables.gv_PathReport_Duoc, PathReport);
             if (File.Exists(path))
            {
                return path;

            }
            else
            {
                Utility.ShowMsg(string.Format("Không tìm thấy File {0}", path), "Thông báo không tìm thấy File", MessageBoxIcon.Warning);
                return "";
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC SỐ LƯỢNG TỒN THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPresDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
            try
            {
                Janus.Windows.GridEX.GridEXColumn gridExColumnSL = grdPresDetail.RootTable.Columns["Quantity"];
                Janus.Windows.GridEX.GridEXColumn gridExColumnSLTon = grdPresDetail.RootTable.Columns["SL_Ton"];
                if(e.Row.RowType==RowType.Record)
                {

                    if (Utility.Int32Dbnull(e.Row.Cells[gridExColumnSL].Value) > Utility.Int32Dbnull(e.Row.Cells[gridExColumnSLTon].Value))
                    {
                        e.Row.RowStyle.BackColor = Color.Red;
                        e.Row.RowStyle.FontBold = TriState.True;
                    }
                    else
                    {
                        e.Row.RowStyle.BackColor = Color.White;
                        e.Row.RowStyle.FontBold = TriState.False;
                    }
                }
            }
            catch (Exception)
            {
                
               
            }

        }
        /// <summary>
        /// hàm thưc hiện việc kiểm tra thông tin của kho có đủ thuốc không 
        /// Nếu không đủ không cho phát thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKiemTraSoLuong_Click(object sender, EventArgs e)
        {
            if(!InValiKiemTraDonThuoc())return;
            else
            {
                Utility.ShowMsg("Bạn có thể xác nhận phiếu lĩnh thuốc của bệnh nhân\n Mời bạn phát thuốc","Thông báo",MessageBoxIcon.Information);
            }
        }

        private bool InValiKiemTraDonThuoc()
        {
            if(grdPresDetail.GetDataRows().Length<=0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi để xác nhận phát thuốc cho bệnh nhân","Thông báo",MessageBoxIcon.Error);
               
                return false;
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.DrugId].Value);
                    string Drug_name = Utility.sDbnull(gridExRow.Cells["Drug_Name"].Value);
                    int id_kho=Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.StockId].Value);
                    int so_luong=Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.Quantity].Value);
                    int SoLuongTon = BusinessHelper.SoLuongTonTrongKho(id_kho, id_thuoc);
                    if(SoLuongTon<so_luong)
                    {
                        Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc,Vì thuốc :{0} số lượng tồn hiện tại trong kho không đủ\n Mời bạn xem lại số lượng", Drug_name));
                        Utility.GonewRowJanus(grdPresDetail, TPrescriptionDetail.Columns.DrugId, id_thuoc.ToString());
                        return false;
                        break;
                    }
                }
            }
         
            var query = from thuoc in grdPresDetail.GetDataRows()
                where Utility.Int32Dbnull(thuoc.Cells[TPrescriptionDetail.Columns.HasComfirm].Value) == 1
                select thuoc;
            if (query.Any())
            {
                Utility.ShowMsg("Đơn thuốc đã có 1 thuốc được cấp phát\n Mời bạn xem lại","Thông báo",MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
       
        /// <summary>
        /// hàm thực hiện việc di chuyển thôn gtin trên đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommnad();
        }

        private void cmdExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện viecj cấu hình 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_CauHinh frm = new frm_CauHinh();
                frm.HisDuocProperties = HisDuocProperties;
                frm.ShowDialog();
                CauHinh();
                
                //grdPresDetail.SaveLayoutFile();
            }catch(Exception exception)
            {
                
            }
          
        }

        private void LoadLayout()
        {

            string layoutDir = GetLayoutDirectory() + @"\GridEXLayout.gxl";

            if (File.Exists(layoutDir))
            {

                FileStream layoutStream;

                layoutStream = new FileStream(layoutDir, FileMode.Open);

                grdPresDetail.LoadLayoutFile(layoutStream);

                layoutStream.Close();

            }

        }
        private string GetLayoutDirectory()
        {
            DirectoryInfo dInfo;
            dInfo = new DirectoryInfo(Application.ExecutablePath).Parent;

            dInfo = new DirectoryInfo(dInfo.FullName + @"\LayoutData");
            if (!dInfo.Exists) dInfo.Create();
            return dInfo.FullName;
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetDataPresDetail();
            }
            catch (Exception)
            {
                
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg("Lỗi trong quá trình lấy đơn thuốc");
                }
            }
           
        }
        /// <summary>
        /// hàm thực hiện việc hủy thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyDonThuoc_Click(object sender, EventArgs e)
        {

            if (Utility.AcceptQuestion("Bạn có muốn thực hiện hủy phát thuốc cho bệnh nhân \n Dữ liệu hủy sẽ được trả lại kho phát thuốc", "Thông báo", true))
            {
                if (HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                {
                    if (!InValiHuyDonthuoc()) return;
                }

                int Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId), -1);
                DPhieuXuatBnhan chot =
                    new Select().From(DPhieuXuatBnhan.Schema).Where(DPhieuXuatBnhan.IdDonThuocColumn).IsEqualTo(Pres_ID).And(DPhieuXuatBnhan.IdChotColumn).IsGreaterThan(0).And
                        (DPhieuXuatBnhan.NgayChotColumn).IsNotNull().ExecuteSingle<DPhieuXuatBnhan>();
                if(chot != null)
                {
                    Utility.ShowMsg("Đơn thuốc đã chốt bạn không thể hủy xác nhận", "thông báo", MessageBoxIcon.Information);
                return;
                }
                

                    
                TPrescription objPrescription = TPrescription.FetchByID(Pres_ID);
                dtNgayPhatThuoc.Value = BusinessHelper.GetSysDateTime();
                ActionResult actionResult =
                    new VietBaIT.HISLink.Business.Duoc.XuatThuoc.XuatThuoc().HuyXacNhanDonThuocBN(objPrescription, CreatePresDetail());
                switch (actionResult)
                {
                    case ActionResult.Success:
                        UpdateHuyHasConfirm();
                        Utility.ShowMsg("Bạn thực hiện việc hủy phát thuốc thành công", "thông báo", MessageBoxIcon.Information);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình hủy phát thuốc cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
        }
        private TPrescriptionDetail []CreatePresDetail()
        {
            int idx = 0;
            int length = 0;
            var query = from chitiet in grdPresDetail.GetDataRows()
                        let y = chitiet.RowType == RowType.Record
                        select y;
            length = query.Count();
            var arrDetail = new TPrescriptionDetail[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    arrDetail[idx] = new TPrescriptionDetail();
                    arrDetail[idx].PresId = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.PresId].Value);
                    arrDetail[idx].DrugId = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.DrugId].Value);
                    arrDetail[idx].PresDetailId = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.PresDetailId].Value);
                    arrDetail[idx].StockId = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.StockId].Value);
                    arrDetail[idx].Price = Utility.DecimaltoDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.Price].Value);
                    arrDetail[idx].SurchargePrice = Utility.DecimaltoDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SurchargePrice].Value);
                    arrDetail[idx].Quantity = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.Quantity].Value);
                    arrDetail[idx].SDesc = Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SDesc].Value);
                    arrDetail[idx].ChiDanThem = Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.ChiDanThem].Value);
                    arrDetail[idx].CachDung = Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.CachDung].Value);
                    arrDetail[idx].DviDung = Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.DviDung].Value);
                    arrDetail[idx].SoLuongDung = Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SoLuongDung].Value);
                    arrDetail[idx].SoLanDung = Utility.sDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.SoLanDung].Value);
                    arrDetail[idx].PaymentId = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.PaymentId].Value);
                    arrDetail[idx].IsPayment = Utility.ByteDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.IsPayment].Value);
                    arrDetail[idx].HasComfirm = Utility.ByteDbnull(gridExRow.Cells[TPrescriptionDetail.Columns.HasComfirm].Value);
                    idx++;
                }
               
            }
            return arrDetail;
        }
        private bool InValiXoaThongTin()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi để thực hiện việc xóa thông tin đơn thuốc", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(TPrescriptionDetail.Schema)
                    .Where(TPrescriptionDetail.Columns.PaymentStatus).IsEqualTo(1)
                    .And(TPrescriptionDetail.Columns.PresDetailId).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.PresDetailId].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi đã thanh toán, bạn không thể xóa thông tin ", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(TPrescriptionDetail.Schema)
                    .Where(TPrescriptionDetail.Columns.HasComfirm).IsEqualTo(1)
                    .And(TPrescriptionDetail.Columns.PresDetailId).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.PresDetailId].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bạn phải chọn những bản ghi chưa xác nhận", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                    break;
                }
            }
            return true;
        }
        private void cmdXoaPhieuNhap_Click(object sender, EventArgs e)
        {
            if (!InValiXoaThongTin()) return;
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin đơn thuốc đang chọn\n Nếu bạn chọn đơn thuốc sẽ xóa toàn bộ", "Thông báo", true))
            {
                Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                ActionResult actionResult =
                   new VietBaIT.HISLink.Business.Duoc.XuatThuoc.XuatThuoc().XoaThongTinThongTinDonThuoc(Pres_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Delete();
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                        Utility.ShowMsg("Bạn xóa toàn bộ thông tin  đơn thuốc thành công", "Thông báo", MessageBoxIcon.Information);
                        ModifyCommnad();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin đơn thuốc", "Thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
        }
        private bool InValiXoaChiTiet()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi để xác nhận phát thuốc cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);

                return false;
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.PresDetailId].Value);
                    int Hasconfim = Utility.Int32Dbnull(gridExRow.Cells[TPrescriptionDetail.Columns.HasComfirm].Value);
                    SqlQuery sqlQuery = new Select().From(TPrescriptionDetail.Schema)
                        .Where(TPrescriptionDetail.Columns.PresDetailId).IsEqualTo(PresDetail_ID)
                        .And(TPrescriptionDetail.Columns.HasComfirm).IsEqualTo(1);
                    if(sqlQuery.GetRecordCount()>0)
                    {
                        Utility.ShowMsg("Thuốc đã được cấp phát, Bạn không thể xóa thông tin ","Thông báo",MessageBoxIcon.Warning);
                        
                        return false;
                        break;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin đơn thuốc chi tiét
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaChiTietThuoc_Click(object sender, EventArgs e)
        {
            if (!InValiXoaChiTiet())return;
            if(Utility.AcceptQuestion("Bạn có muốn thực hiện xóa thông tin chi tiết của đơn thuốc","Thông báo xóa",true))
            {
                 Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                var query = from donthuoc in grdPresDetail.GetDataRows()
                            let y = donthuoc.Cells[TPrescriptionDetail.Columns.PresDetailId].Value
                            select y;
                ArrayList arrayList=new ArrayList();
                Array array = query.ToArray();
                arrayList.AddRange(array);
                ActionResult actionResult =
                    new VietBaIT.HISLink.Business.Duoc.XuatThuoc.XuatThuoc().XoaThongTinChiTietDonThuoc(arrayList,
                                                                                                        Pres_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
                        {
                            gridExRow.Delete();
                            grdPresDetail.UpdateData();
                            m_dtDataPresDetail.AcceptChanges();
                        }
                        
                        Utility.ShowMsg("Bạn xóa thông tin chi tiết đơn thuốc thành công","Thông báo",MessageBoxIcon.Information);
                        ModifyCommnad();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin chi tiết","Thông báo",MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void chkLocNgayKhiDuyet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                HisDuocProperties.LocDonThuocKhiDuyet = chkLocNgayKhiDuyet.Checked;
                Utility.SaveHisDuocConfig(HisDuocProperties);
            }catch(Exception exception)
            {
                
            }
           
        }

       

        private void txtPID_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (txtPID.Text.Trim() != "" && Utility.Int32Dbnull(txtPID.Text) > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        string patient_ID = BusinessHelper.SinhMaHoSoKhiTimKiem(txtPID.Text);
                        txtPID.Text = patient_ID;
                        int Status = -1;

                        int NoiTru = 0;

                        string MaKho = "-1";
                        var querykho = (from kho in m_dtKhothuoc.AsEnumerable().Cast<DataRow>()
                                        let y = Utility.sDbnull(kho[DKho.Columns.IdKho])
                                        select y).ToArray();
                        if (querykho.Count() > 0)
                        {
                            MaKho = string.Join(",", querykho);
                        }

                        m_dtDataDonThuoc =
                            SPs.DuocTimkiemThongtinDonthuocKhoxuat(-1, txtPID.Text,
                                                                   "",
                                                                   -1,
                                                                   Convert.ToDateTime("01/01/1900"),
                                                                   BusinessHelper.GetSysDateTime(), Status,
                                                                   Utility.Int32Dbnull(cboKho.SelectedValue), NoiTru, HisDuocProperties.KieuDuyetDonThuoc).
                                GetDataSet().Tables[0];

                        Utility.SetDataSourceForDataGridEx(grdPres, m_dtDataDonThuoc, true, true, "1=1", "Pres_ID asc");
                        RowFilterView();
                        Utility.SetMessage(lblMessage, string.Format("Tổng số đơn thuốc :{0}", m_dtDataDonThuoc.Rows.Count), true);
                        ModifyCommnad();
                    }
                    catch (Exception exception)
                    {
                        if (globalVariables.IsAdmin)
                        {
                            Utility.ShowMsg(exception.ToString());
                        }

                    }
                    finally
                    {
                        ModifyCommnad();
                    }
                }
            }
        }
       

    }
}
