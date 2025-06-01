using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.DANHMUC;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_quanlynhomchidinh_cls : Form
    {
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private DataTable m_dtExamTypeRelationList = new DataTable();
        private DataTable m_dtData=new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChiDinhCLS = new DataTable();
        private int Distance = 488;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath,string.Format("SplitterDistanceTiepDonf.txt"));
        private bool m_blnHasloaded = false;
        DataTable m_dtChitiet = null;
        byte loainhom=0;//0= nhóm chỉ định cls,1=nhóm kiểm nghiệm
        public bool isUsed4Select = false;
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        string nhomchidinh = "";
        public frm_quanlynhomchidinh_cls(string nhomchidinh)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            this.nhomchidinh = nhomchidinh; //"-GOI,-TIEN,-CHIPHITHEM";
            this.loainhom = loainhom;
            InitEvents();
        }
        void InitEvents()
        {
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
           

            this.cmdThemnhom.Click += new System.EventHandler(this.cmdThemnhom_Click);
            this.cmdSuaNhom.Click += new System.EventHandler(this.cmdSuaNhom_Click);
            this.cmdXoaNhom.Click += new System.EventHandler(this.cmdXoaNhom_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            
            this.Load += new System.EventHandler(this.frm_quanlynhomchidinh_cls_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_quanlynhomchidinh_cls_KeyDown);
            txtLoainhom._OnShowData += txtLoainhom__OnShowData;
            mnuDelete.Click += mnuDelete_Click;
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS_Selected()) return;
                long IdChitiet =
                    Utility.Int64Dbnull(grdAssignDetail.CurrentRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);

                _KCB_CHIDINH_CANLAMSANG.XoaCLSChitietKhoinhom(IdChitiet);
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
            if (grdList.GetValue("Nguoi_tao").ToString() == globalVariables.UserName || globalVariables.IsAdmin)
            {
            }
            else
            {
                Utility.ShowMsg("Bạn không có quyền sửa thông tin nhóm dịch vụ của người khác tạo. Bạn phải có quyền của Admin mới được phép sửa");
                return false;
            }
            if (!Utility.isValidGrid(grdAssignDetail))
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một chi tiết cận lâm sàng cần xóa khỏi nhóm",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            int IdChitiet_ID = -1;
            SqlQuery sqlQuery = null;
            if (!globalVariables.IsAdmin)
            {

                IdChitiet_ID =
                    Utility.Int32Dbnull(
                        grdAssignDetail.CurrentRow.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitiet].Value, -1);
                sqlQuery = new Select().From(DmucNhomcanlamsangChitiet.Schema)
                    .Where(DmucNhomcanlamsangChitiet.Columns.IdChitiet).IsEqualTo(IdChitiet_ID)
                   .And(DmucNhomcanlamsangChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Trong các dịch vụ cận lâm sàng bạn chọn xóa, có một số dịch vụ được thêm bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các dịch vụ do chính bạn thêm vào nhóm để thực hiện xóa");
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
                m_dtData = _KCB_CHIDINH_CANLAMSANG.DmucTimkiemNhomchidinhCls(IdNhom, tennhom, manhom, MaLoainhom, loainhom, Utility.Int32Dbnull(txtDmucDichvuCLS.MyID, -1), globalVariables.UserName,Utility.Bool2byte( globalVariables.IsAdmin));
                Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", DmucNhomcanlamsang.Columns.TenNhom );
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
                var counts = m_dtData.AsEnumerable().GroupBy(x => x.Field<string>("ten_loainhom"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_loainhom"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_loainhom"];
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
        private void frm_quanlynhomchidinh_cls_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            txtLoainhom.Init();
            DataTable m_dtChitietchidinh = SPs.DmucLaydanhmucDichvuclsChitiet(-1, -1).GetDataSet().Tables[0];
            txtDmucDichvuCLS.Init(m_dtChitietchidinh,
                new List<string>()
                {
                    VDmucDichvuclsChitiet.Columns.IdChitietdichvu,
                    VDmucDichvuclsChitiet.Columns.MaChitietdichvu,
                    VDmucDichvuclsChitiet.Columns.TenChitietdichvu
                });
            AllowTextChanged = true;
            TimKiemThongTin();
            SetisUsed4Select();
            m_blnHasloaded = true;
        }
       
        void SetisUsed4Select()
        {
            int orgHeigh = grbInfor.Height;
            trbActions.Visible = !isUsed4Select;
            grbInfor.Height = isUsed4Select ? 0 : orgHeigh;
            grdList.SelectionMode = isUsed4Select ? Janus.Windows.GridEX.SelectionMode.MultipleSelection : Janus.Windows.GridEX.SelectionMode.SingleSelection;
        }    
        private DataTable m_dataDataRegExam=new DataTable();



        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                 m_dtChitiet = _KCB_CHIDINH_CANLAMSANG.DmucLaychitietNhomchidinhCls(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,DmucNhomcanlamsang.Columns.Id), -1));
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChitiet, false, true, "1=1",
                                                   "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
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
                frm_themmoi_nhomcls frm = new frm_themmoi_nhomcls(nhomchidinh);
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
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 nhóm chỉ định cận lâm sàng để sửa đổi");
                    return;
                }

                frm_themmoi_nhomcls frm = new frm_themmoi_nhomcls(nhomchidinh);
                frm.txtId.Text =Utility.Int32Dbnull( Utility.GetValueFromGridColumn(grdList, DmucNhomcanlamsang.Columns.Id),-1).ToString();
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
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 nhóm chỉ định cần xóa");
                return;
            }
            if (grdList.GetValue("Nguoi_tao").ToString() == globalVariables.UserName || globalVariables.IsAdmin)
            {
            }
            else
            {
                Utility.ShowMsg("Bạn không có quyền xóa nhóm dịch vụ của người khác tạo. Bạn phải có quyền của Admin mới được phép xóa");
                return;
            }
            if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa nhóm chỉ định cận lâm sàng đang chọn không ?", "Thông báo", true))
            {
                _KCB_CHIDINH_CANLAMSANG.Xoanhom(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucNhomcanlamsang.Columns.Id), -1));
            }
        }
        private void frm_quanlynhomchidinh_cls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);      
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        
            if (e.KeyCode == Keys.N && e.Control) cmdThemnhom.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSuaNhom.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaNhom.PerformClick();
           
        }
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
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 nhóm chỉ định cần xóa");
                    return;
                }
                if (grdList.GetValue("Nguoi_tao").ToString() == globalVariables.UserName || globalVariables.IsAdmin)
                {
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền xóa nhóm dịch vụ của người khác tạo. Bạn phải có quyền của Admin mới được phép xóa");
                    return;
                }

                 if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa nhóm chỉ định cận lâm sàng đang chọn không ?", "Thông báo", true))
                {
                    ActionResult actionResult = _KCB_CHIDINH_CANLAMSANG.Xoanhom(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucNhomcanlamsang.Columns.Id), -1));
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

        private void mnuShared_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdList.GetValue("Nguoi_tao").ToString() == globalVariables.UserName || globalVariables.IsAdmin)
                {
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền chia sẻ nhóm dịch vụ của người khác tạo. Bạn phải có quyền của Admin mới được phép chia sẻ");
                    return;
                }


                string nhomchidinhcls = string.Join(",", (from p in grdList.GetCheckedRows()
                                                       select Utility.sDbnull(p.Cells["Ten_Nhom"].Value, "")).ToArray<string>());
                List<int> lstID = (from p in grdList.GetCheckedRows()
                                   select Utility.Int32Dbnull(p.Cells["Id"].Value, "")).ToList<int>();
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chia sẻ các mẫu chỉ định CLS dưới đây cho các bác sĩ khác không?:\n {0}?", nhomchidinhcls), "Xác nhận", true))
                {
                    Utility.ShowMsg("Bạn vừa hủy thao tác chia sẻ mẫu chỉ định CLS cho các bác sĩ khác. Nhấn OK để kết thúc");
                    return;
                }

                int numofa = new Update(DmucNhomcanlamsang.Schema).Set(DmucNhomcanlamsang.Columns.SharedX).EqualTo(0).Where(DmucNhomcanlamsang.Columns.Id).In(lstID).Execute();
                if (numofa > 0)
                {
                    (from p in m_dtChiDinhCLS.AsEnumerable() where lstID.Contains(Utility.Int32Dbnull(p["id"], -1)) select p).ToList().ForEach(x => x["Shared"] = 1);
                    Utility.ShowMsg(string.Format("Đã chia sẻ các mẫu chỉ định CLS dưới đây cho các bác sĩ khác thành công :{0}", nhomchidinhcls));
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
                if (grdList.GetValue("Nguoi_tao").ToString() == globalVariables.UserName || globalVariables.IsAdmin)
                {
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền hủy chia sẻ nhóm dịch vụ của người khác tạo. Bạn phải có quyền của Admin mới được phép hủy chia sẻ");
                    return;
                }
                string nhomchidinhcls = string.Join(",", (from p in grdList.GetCheckedRows()
                                                          select Utility.sDbnull(p.Cells["Ten_Nhom"].Value, "")).ToArray<string>());
                List<int> lstID = (from p in grdList.GetCheckedRows()
                                   select Utility.Int32Dbnull(p.Cells["Id"].Value, "")).ToList<int>();
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn Hủy chia sẻ các mẫu chỉ định CLS dưới đây cho các bác sĩ khác không?:\n {0}?", nhomchidinhcls), "Xác nhận", true))
                {
                    Utility.ShowMsg("Bạn vừa hủy thao tác Hủy chia sẻ mẫu chỉ định CLS cho các bác sĩ khác. Nhấn OK để kết thúc");
                    return;
                }

                int numofa = new Update(DmucNhomcanlamsang.Schema).Set(DmucNhomcanlamsang.Columns.SharedX).EqualTo(0).Where(DmucNhomcanlamsang.Columns.Id).In(lstID).Execute();
                if (numofa > 0)
                {
                    (from p in m_dtChiDinhCLS.AsEnumerable() where lstID.Contains(Utility.Int32Dbnull(p["id"], -1)) select p).ToList().ForEach(x => x["Shared"] = 0);
                    Utility.ShowMsg(string.Format("Đã hủy chia sẻ các mẫu chỉ định CLS dưới đây cho các bác sĩ khác thành công :{0}", nhomchidinhcls));
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

       
       
       
      
      
       
    }
}
