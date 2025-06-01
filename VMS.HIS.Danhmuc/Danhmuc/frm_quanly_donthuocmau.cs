using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.DANHMUC;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_quanly_donthuocmau : Form
    {
        private DataTable m_dtData=new DataTable();
        private DataTable m_kieuKham;
        private int Distance = 488;
        DataTable m_dtChitiet = null;
        byte loainhom=0;//0= nhóm chỉ định cls,1=nhóm kiểm nghiệm
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        string nhomchidinh = "";
        string KIEUTHUOC_VT = "THUOC";
        public frm_quanly_donthuocmau()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            InitEvents();
        }
        public frm_quanly_donthuocmau(string KIEUTHUOC_VT)
        {
            InitializeComponent();
            this.KIEUTHUOC_VT = KIEUTHUOC_VT;
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            InitEvents();
        }
        void InitEvents()
        {
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            grdAssignDetail.UpdatingCell += grdAssignDetail_UpdatingCell;

            this.cmdThemnhom.Click += new System.EventHandler(this.cmdThemnhom_Click);
            this.cmdSuaNhom.Click += new System.EventHandler(this.cmdSuaNhom_Click);
            this.cmdXoaNhom.Click += new System.EventHandler(this.cmdXoaNhom_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            
            this.Load += new System.EventHandler(this.frm_quanly_donthuocmau_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_quanly_donthuocmau_KeyDown);
            txtLoainhom._OnShowData += txtLoainhom__OnShowData;
            mnuDelete.Click += mnuDelete_Click;
        }

        void grdAssignDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "sang" || e.Column.Key == "trua" || e.Column.Key == "chieu" || e.Column.Key == "toi")
                {
                    string chidan = GetContainGuide(Utility.sDbnull(grdAssignDetail.GetValue("sang"), ""), Utility.sDbnull(grdAssignDetail.GetValue("trua"), ""), Utility.sDbnull(grdAssignDetail.GetValue("chieu"), ""), Utility.sDbnull(grdAssignDetail.GetValue("toi"), ""), Utility.sDbnull(grdAssignDetail.GetValue("cach_dung"), ""), Utility.sDbnull(grdAssignDetail.GetValue("dvt"), ""));
                    //DmucDonthuocmauChitiet objchitiet = DmucDonthuocmauChitiet.FetchByID(Utility.Int32Dbnull(grdAssignDetail.GetValue("id_chitiet"), -1));
                    int num = new Update(DmucDonthuocmauChitiet.Schema)
                    .Set(DmucDonthuocmauChitiet.Columns.Sang).EqualTo(Utility.DecimaltoDbnull(grdAssignDetail.GetValue("sang"), 0))
                    .Set(DmucDonthuocmauChitiet.Columns.Trua).EqualTo(Utility.DecimaltoDbnull(grdAssignDetail.GetValue("trua"), 0))
                    .Set(DmucDonthuocmauChitiet.Columns.Chieu).EqualTo(Utility.DecimaltoDbnull(grdAssignDetail.GetValue("chieu"), 0))
                    .Set(DmucDonthuocmauChitiet.Columns.Toi).EqualTo(Utility.DecimaltoDbnull(grdAssignDetail.GetValue("toi"), 0))
                        .Set(DmucDonthuocmauChitiet.Columns.ChidanThem).EqualTo(chidan)
                        .Where(DmucDonthuocmauChitiet.Columns.IdChitiet).IsEqualTo(Utility.Int32Dbnull(grdAssignDetail.GetValue("id_chitiet"), -1))
                        .Execute();
                    if (num > 0)
                    {
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells["chidan_them"].Value = chidan;
                        grdAssignDetail.CurrentRow.EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
               
            }
        }
        private string GetContainGuide( string sang,string trua,string chieu,string toi,string cachdung,string dvt)
        {
            try
            {
                string yourString = "";
                //   yourString = yourString + this.txtCachDung.Text + " ";
                if (!string.IsNullOrEmpty(sang))
                {
                    yourString = "Sáng " + sang.Trim() + " " + dvt;
                }
                if (!string.IsNullOrEmpty(trua))
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", Trưa " + trua.Trim() + " " + dvt;
                    else
                        yourString += "Trưa " + trua.Trim() + " " + dvt;
                }
                if (!string.IsNullOrEmpty(chieu))
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", Chiều " + chieu.Trim() + " " + dvt;
                    else
                        yourString += "Chiều " + chieu.Trim() + " " + dvt;
                }
                if (!string.IsNullOrEmpty(toi))
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", Tối " + toi.Trim() + " " + dvt;
                    else yourString += "Tối " + toi.Trim() + " " + dvt;
                }
                if (!string.IsNullOrEmpty(cachdung))
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", " + cachdung.Trim() + " " + dvt;
                    else yourString += cachdung.Trim() + " " + dvt;
                }
                //if (!string.IsNullOrEmpty(this.txtChiDanThem.Text))
                //{
                //    yourString = yourString + ". " + this.txtChiDanThem.Text;
                //}
                return Utility.ReplaceString(yourString);
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }
        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS_Selected()) return;
                long idChitiet = Utility.Int64Dbnull(grdAssignDetail.CurrentRow.Cells[DmucDonthuocmauChitiet.Columns.IdChitiet].Value, -1);
                new Delete().From(DmucDonthuocmauChitiet.Schema)
                    .Where(DmucDonthuocmauChitiet.Columns.IdChitiet)
                    .IsEqualTo(idChitiet)
                    .Execute();
                //_KCB_CHIDINH_CANLAMSANG.XoaCLSChitietKhoinhom(idChitiet);
                grdAssignDetail.CurrentRow.Delete();
                grdAssignDetail.UpdateData();
                grdAssignDetail.Refresh();
                m_dtChitiet.AcceptChanges();
                ModifyCommand();
            }
            catch
            {
            }
        }
        private bool IsValidDataXoaCLS_Selected()
        {
            if (!Utility.isValidGrid(grdAssignDetail))
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một thuốc cần xóa khỏi đơn mẫu",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            if (!globalVariables.IsAdmin)
            {

                int idChitietId = Utility.Int32Dbnull(
                    grdAssignDetail.CurrentRow.Cells[DmucDonthuocmauChitiet.Columns.IdChitiet].Value, -1);
                SqlQuery sqlQuery = new Select().From(DmucDonthuocmauChitiet.Schema)
                    .Where(DmucDonthuocmauChitiet.Columns.IdChitiet).IsEqualTo(idChitietId)
                    .And(DmucDonthuocmauChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg(
                        "Trong các thuốc bạn chọn xóa, có một số thuốc được thêm bởi Bác sĩ khác nên bạn không được phép xóa. " +
                        "Mời bạn chọn lại chỉ các  thuốc do chính bạn thêm vào nhóm để thực hiện xóa");
                    return false;
                }
            }

            return true;
        }
        void txtLoainhom__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLoainhom.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLoainhom.myCode;
                txtLoainhom.Init();
                txtLoainhom.SetCode(oldCode);
                txtLoainhom.Focus();
            }
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuaNhom.PerformClick();
        }
      
        void TimKiemThongTin()
        {
            try
            {
                int IdNhom = Utility.Int32Dbnull(txtID.Text, -1);
                string manhom = Utility.DoTrim(txtManhom.Text);
                if (manhom == "") manhom = "-1";
                string tennhom = Utility.DoTrim(txtTennhom.Text);
                if (tennhom == "") tennhom = "-1";
                string MaLoainhom = Utility.DoTrim(txtLoainhom.myCode);
                if (MaLoainhom == "") MaLoainhom = "-1";
                m_dtData =
                    SPs.DmucTimkiemDonthuocmau(IdNhom, manhom, tennhom,MaLoainhom, KIEUTHUOC_VT,
                        Utility.Int32Dbnull(txtthuoc.MyID, -1), globalVariables.UserName).GetDataSet().Tables[0];
                //m_dtData = _KCB_CHIDINH_CANLAMSANG.DmucTimkiemNhomchidinhCls(IdNhom, tennhom, manhom, MaLoainhom, loainhom, Utility.Int32Dbnull(txtthuoc.MyID, -1), globalVariables.UserName);

                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtData, true, true, "1=1", DmucDonthuocmau.Columns.TenDonthuoc );
                if (grdList.GetDataRows().Length <= 0)
                    m_dataDataRegExam.Rows.Clear();
                UpdateGroup();
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtData.AsEnumerable().GroupBy(x => x.Field<string>("ten_loaithuoc"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_loaithuoc"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_loaithuoc"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool AllowTextChanged;
      
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_quanly_donthuocmau_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            txtLoainhom.Init();
            bool gridView =
               Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KEDONTHUOC_SUDUNGLUOI", "0", true), 0) ==
               1;
            if (!gridView)
            {
                gridView = PropertyLib._AppProperties.GridView;
            }
            txtthuoc.GridView = gridView;
            LoadAuCompleteThuoc();
            //DataTable mDtChitietchidinh = SPs.thuocla(-1, -1).GetDataSet().Tables[0];
            //txtthuoc.Init(mDtChitietchidinh,
            //    new List<string>()
            //    {
            //        VDmucDichvuclsChitiet.Columns.IdChitietdichvu,
            //        VDmucDichvuclsChitiet.Columns.MaChitietdichvu,
            //        VDmucDichvuclsChitiet.Columns.TenChitietdichvu
            //    });
            AllowTextChanged = true;
            TimKiemThongTin();
        }
        private void LoadAuCompleteThuoc()
        {
            txtthuoc.dtData = CommonLoadDuoc.LayThongTinThuoc(KIEUTHUOC_VT);
            txtthuoc.ChangeDataSource();
        }

        private DataTable m_dataDataRegExam=new DataTable();



        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                 m_dtChitiet = SPs.DmucLaychitietDonthuocmau(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,DmucDonthuocmau.Columns.Id), -1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChitiet, false, true, "1=1",
                                                   "stt_hienthi," + DmucThuoc.Columns.TenThuoc);
                grdAssignDetail.MoveFirst();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu CLS chi tiết\n" + ex.Message);
            }
           

        }
        /// <summary>
        /// hàm thực hiện việc cáu hình trạng thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            cmdSuaNhom.Enabled =Utility.isValidGrid(grdList);
            cmdXoaNhom.Enabled = Utility.isValidGrid(grdList);
         
        }
        
        

        private void cmdThemnhom_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_donthuocmau_2023 frm = new frm_themmoi_donthuocmau_2023(KIEUTHUOC_VT);
                frm.m_eAction = action.Insert;
                frm.m_dtNhom = m_dtData;
                frm.grdList = grdList;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
               
            }
            catch (Exception exception)
            {
                
                
            }
            finally
            {
               // CauHinh();
            }
           
        }

        void frm__OnActionSuccess()
        {
            UpdateGroup();
            grdList_SelectionChanged(grdList, new EventArgs());
        }
       
        /// <summary>
        /// hàm thực hiện sửa thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaNhom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 đơn thuốc mẫu để sửa đổi");
                    return;
                }

                frm_themmoi_donthuocmau_2023 frm = new frm_themmoi_donthuocmau_2023(KIEUTHUOC_VT);
                frm.txtId.Text =Utility.Int32Dbnull( Utility.GetValueFromGridColumn(grdList, DmucDonthuocmau.Columns.Id),-1).ToString();
                frm.m_eAction = action.Update;
                frm.m_dtNhom = m_dtData;
                frm.grdList = grdList;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
               
              
            }
            catch (Exception)
            {


            }
            finally
            {
                //CauHinh();
            }
          
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin khám 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 đơn thuốc mẫu để xóa");
                return;
            }
            if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa đơn thuốc mẫu đang chọn không ?", "Thông báo", true))
            {
                int IdNhom = Utility.Int32Dbnull(
                    Utility.GetValueFromGridColumn(grdList, DmucDonthuocmau.Columns.Id), -1);
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Delete().From(DmucDonthuocmau.Schema).Where(DmucDonthuocmau.Columns.Id).IsEqualTo(IdNhom).Execute();
                        new Delete().From(DmucDonthuocmauChitiet.Schema).Where(DmucDonthuocmauChitiet.Columns.IdNhom).IsEqualTo(IdNhom).Execute();
                    }
                    scope.Complete();
                }
                //_KCB_CHIDINH_CANLAMSANG.Xoanhom(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucNhomcanlamsang.Columns.Id), -1));
            }
        }
        private void frm_quanly_donthuocmau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);      
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        
            if (e.KeyCode == Keys.N && e.Control) cmdThemnhom.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSuaNhom.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaNhom.PerformClick();
           
        }

        private ActionResult actionResult;
        /// <summary>
        /// hàm thực hiện việc xóa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaNhom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 đơn thuốc mẫu cần xóa");
                    return;
                }
                 if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa đơn thuốc mẫu đang chọn không ?", "Thông báo", true))
                {

                    int IdNhom = Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdList, DmucDonthuocmau.Columns.Id), -1);
                    using (var scope = new TransactionScope())
                    {
                        using (var sh = new SharedDbConnectionScope())
                        {
                            new Delete().From(DmucDonthuocmau.Schema).Where(DmucDonthuocmau.Columns.Id).IsEqualTo(IdNhom).Execute();
                            new Delete().From(DmucDonthuocmauChitiet.Schema).Where(DmucDonthuocmauChitiet.Columns.IdNhom).IsEqualTo(IdNhom).Execute();
                        }
                        scope.Complete();
                        actionResult = ActionResult.Success;
                    }
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            try
                            {
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Delete();
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                grdList_SelectionChanged(grdList, e);

                            }
                            catch
                            {

                            }
                            m_dtData.AcceptChanges();
                            UpdateGroup();
                            break;
                        case ActionResult.Exception:
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }
                ModifyCommand();
            }
            catch
            {
            }
            finally
            {
               
            }
        }
       
      
        /// <summary>
        /// hàm thực hiện viec lọc thông in trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void grdList_AddingRecord(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

       

      
       

        private void cmdCauhinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
        }

      

        private void mnuShared_Click_1(object sender, EventArgs e)
        {
            try
            {
                string nhomchidinhcls = string.Join(",", (from p in grdList.GetCheckedRows()
                                                          select Utility.sDbnull(p.Cells["ten_donthuoc"].Value, "")).ToArray<string>());
                List<int> lstID = (from p in grdList.GetCheckedRows()
                                   select Utility.Int32Dbnull(p.Cells["Id"].Value, "")).ToList<int>();
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chia sẻ các mẫu đơn thuốc dưới đây cho các bác sĩ khác không?:\n {0}?", nhomchidinhcls), "Xác nhận", true))
                {
                    Utility.ShowMsg("Bạn vừa hủy thao tác chia sẻ mẫu đơn thuốc cho các bác sĩ khác. Nhấn OK để kết thúc");
                    return;
                }

                int numofa = new Update(DmucDonthuocmau.Schema).Set(DmucDonthuocmau.Columns.SharedX).EqualTo(0).Where(DmucDonthuocmau.Columns.Id).In(lstID).Execute();
                if (numofa > 0)
                {
                    (from p in m_dtData.AsEnumerable() where lstID.Contains(Utility.Int32Dbnull(p["id"], -1)) select p).ToList().ForEach(x => x["Shared"] = 1);
                    Utility.ShowMsg(string.Format("Đã chia sẻ các mẫu đơn thuốc dưới đây cho các bác sĩ khác thành công :{0}", nhomchidinhcls));
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuUnshared_Click(object sender, EventArgs e)
        {
            try
            {
                string nhomchidinhcls = string.Join(",", (from p in grdList.GetCheckedRows()
                                                          select Utility.sDbnull(p.Cells["ten_donthuoc"].Value, "")).ToArray<string>());
                List<int> lstID = (from p in grdList.GetCheckedRows()
                                   select Utility.Int32Dbnull(p.Cells["Id"].Value, "")).ToList<int>();
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn Hủy chia sẻ các mẫu đơn thuốc dưới đây cho các bác sĩ khác không?:\n {0}?", nhomchidinhcls), "Xác nhận", true))
                {
                    Utility.ShowMsg("Bạn vừa hủy thao tác Hủy chia sẻ mẫu đơn thuốc cho các bác sĩ khác. Nhấn OK để kết thúc");
                    return;
                }

                int numofa = new Update(DmucDonthuocmau.Schema).Set(DmucDonthuocmau.Columns.SharedX).EqualTo(0).Where(DmucDonthuocmau.Columns.Id).In(lstID).Execute();
                if (numofa > 0)
                {
                    (from p in m_dtData.AsEnumerable() where lstID.Contains(Utility.Int32Dbnull(p["id"], -1)) select p).ToList().ForEach(x => x["Shared"] = 0);
                    Utility.ShowMsg(string.Format("Đã hủy chia sẻ các mẫu đơn thuốc dưới đây cho các bác sĩ khác thành công :{0}", nhomchidinhcls));
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdThemnhom_Click_1(object sender, EventArgs e)
        {

        }

       
       
       
      
      
       
    }
}
