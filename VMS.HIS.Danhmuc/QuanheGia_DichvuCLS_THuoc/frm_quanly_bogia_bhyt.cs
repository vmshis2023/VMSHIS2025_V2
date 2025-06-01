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
using Excel;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_quanly_bogia_bhyt : Form
    {
        private DataTable m_dtData=new DataTable();
        private DataTable m_kieuKham;
        private int Distance = 488;
        DataTable m_dtChitiet = null;
        DmucDoituongkcb objBHYT = null;
        byte loaiBogia=0;//0= nhóm chỉ định cls,1=nhóm kiểm nghiệm
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        string Bogiachidinh = "";
        string KIEUTHUOC_VT = "THUOC";
        public string KTH = "ALL";
        public frm_quanly_bogia_bhyt()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            InitEvents();
        }
        public frm_quanly_bogia_bhyt(string KIEUTHUOC_VT)
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
            grdGiaCLS.UpdatingCell += grdGiaCLS_UpdatingCell;

            this.cmdThemBogia.Click += new System.EventHandler(this.cmdThemBogia_Click);
            this.cmdSuaBogia.Click += new System.EventHandler(this.cmdSuaBogia_Click);
            this.cmdXoaBogia.Click += new System.EventHandler(this.cmdXoaBogia_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            
            this.Load += new System.EventHandler(this.frm_quanly_bogia_bhyt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_quanly_bogia_bhyt_KeyDown);
        }

        void grdGiaCLS_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (objBHYT != null)
                {
                    string colName = e.Column.Key;
                    decimal don_gia = Utility.DecimaltoDbnull(e.Value, 0);
                    decimal GiaBHYT = Utility.DecimaltoDbnull(grdGiaCLS.CurrentRow.Cells["Gia_BHYT"].Value, 0);
                    decimal GiaDV = Utility.DecimaltoDbnull(grdGiaCLS.CurrentRow.Cells["Gia_Dichvu"].Value, 0);
                    decimal GiaPhuThu = Utility.DecimaltoDbnull(grdGiaCLS.CurrentRow.Cells["Phuthu_Traituyen"].Value, 0);
                    if (e.Column.Key == "gia_bhyt")
                    {
                        colName = "don_gia";
                        GiaBHYT = don_gia;
                        if (chkAutoEdit.Checked)
                            GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                    }
                    else if (e.Column.Key == "phuthu_traituyen")
                    {
                        colName = "phuthu_traituyen";
                        GiaPhuThu = don_gia;
                    }
                    else if (e.Column.Key == "phuthu_dungtuyen")
                    {
                        colName = "phuthu_dungtuyen";
                    }
                    else if (e.Column.Key == "tyle_tt")
                    {
                        colName = "tyle_tt";
                    }
                    int num = -1;
                    long id_quanhe = Utility.Int64Dbnull(grdGiaCLS.GetValue("id_quanhe"), -1);
                    if (id_quanhe > 0)//Update
                    {
                        if (colName == "don_gia")
                        {
                            if (don_gia > 0)
                                num = new Update(QheDoituongDichvucl.Schema)
                                .Set(colName).EqualTo(don_gia)
                                .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(GiaPhuThu)
                                .Set(QheDoituongDichvucl.Columns.IdBogia).EqualTo(id_bogia)
                                .Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(id_quanhe).Execute();
                            else
                            {
                                num = new Delete().From(QheDoituongDichvucl.Schema).Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(id_quanhe).Execute();
                                grdGiaCLS.CurrentRow.BeginEdit();
                                grdGiaCLS.CurrentRow.Cells["id_quanhe"].Value = -1;
                                grdGiaCLS.CurrentRow.EndEdit();
                                grdGiaCLS.Refetch();
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa giá BHYT của dịch vụ {0},IdChitietdichvu={1} ",  grdGiaCLS.CurrentRow.Cells["ten_chitietdichvu"].Value, grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                return;

                            }
                                
                        }
                        else
                            num = new Update(QheDoituongDichvucl.Schema)
                          .Set(colName).EqualTo(don_gia)
                          .Set(QheDoituongDichvucl.Columns.IdBogia).EqualTo(id_bogia)
                          .Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(id_quanhe).Execute();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật giá {0}={1} của dịch vụ {2},IdChitietdichvu={3} ", e.Column.Key, e.Value, grdGiaCLS.CurrentRow.Cells["ten_chitietdichvu"].Value, grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                    else//Insert
                    {
                        QheDoituongDichvucl _newItem = new QheDoituongDichvucl();
                        _newItem.IdDoituongKcb = objBHYT.IdDoituongKcb;
                        _newItem.IdDichvu = Utility.Int16Dbnull(grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                        _newItem.IdChitietdichvu = Utility.Int32Dbnull(grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                        _newItem.TyleGiam = 0;
                        _newItem.KieuGiamgia = 1;
                        _newItem.TyleTt = Utility.DecimaltoDbnull(grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.TyleTt].Value, 0);
                        _newItem.MotaThem = "";
                        _newItem.DonGia = GiaBHYT;
                        _newItem.IdBogia = id_bogia;
                        _newItem.PhuthuDungtuyen = Utility.DecimaltoDbnull(grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0);
                        _newItem.IdLoaidoituongKcb = objBHYT.IdLoaidoituongKcb;
                        _newItem.PhuthuTraituyen = GiaPhuThu;
                        _newItem.MaDoituongKcb = objBHYT.MaDoituongKcb;
                        _newItem.NgayTao = globalVariables.SysDate;
                        _newItem.NguoiTao = globalVariables.UserName;
                        _newItem.MaKhoaThuchien =KTH;
                        _newItem.IsNew = true;
                        _newItem.Save();
                        id_quanhe = _newItem.IdQuanhe;

                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới giá {0}={1} của dịch vụ {2},IdChitietdichvu={3} ", e.Column.Key, e.Value, grdGiaCLS.CurrentRow.Cells["ten_chitietdichvu"].Value, grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    }
                    grdGiaCLS.CurrentRow.BeginEdit();
                    grdGiaCLS.CurrentRow.Cells["id_quanhe"].Value = id_quanhe;
                    grdGiaCLS.CurrentRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value = GiaPhuThu;
                    grdGiaCLS.CurrentRow.EndEdit();
                    grdGiaCLS.Refetch();
                }
                else
                    Utility.ShowMsg("Chưa xác định được đối tượng BHYT đầu vào");
                
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            } 
        }
       
      

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuaBogia.PerformClick();
        }
      
        void TimKiemThongTin()
        {
            try
            {
                int IdBogia = Utility.Int32Dbnull(txtID.Text, -1);
                string maBogia = Utility.DoTrim(txtMaBogia.Text);
                if (maBogia == "") maBogia = "-1";
                string tenBogia = Utility.DoTrim(txtTenBogia.Text);
                if (tenBogia == "") tenBogia = "-1";
                m_dtData =
                    SPs.QheGiadoituongClsTimdanhsachbogia(IdBogia, maBogia, tenBogia, new DateTime(1900, 1, 1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtData, true, true, "1=1", DmucBogiadichvu.Columns.TenBogia );
                if (grdList.GetDataRows().Length <= 0)
                    m_dtGiaCLS.Rows.Clear();
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
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
        private void frm_quanly_bogia_bhyt_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            objBHYT = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo("BHYT").ExecuteSingle<DmucDoituongkcb>();
            TimKiemThongTin();
            AllowTextChanged = true;
            grdList_SelectionChanged(grdList, e);
        }
        private void LoadAuCompleteThuoc()
        {
        }

        private DataTable m_dtGiaCLS=new DataTable();


        int id_bogia = -1;
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                grdGiaCLS.RootTable.Columns["act"].Visible = false;
                id_bogia = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucBogiadichvu.Columns.IdBogia), -1);
                m_dtChitiet = SPs.QheGiadichvuclsGetdatabyIdBogia(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,DmucBogiadichvu.Columns.IdBogia), -1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdGiaCLS, m_dtChitiet, true, true, "1=1", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
                Utility.SetDataSourceForDataGridEx(grdExportFile, m_dtChitiet.Copy(), true, true, "1=1", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
                //GridEXColumn stt_nhomdvu = grdGiaCLS.RootTable.Columns["stt_hthi_loaidvu"];
                //GridEXColumn stt_loaidichvu = grdGiaCLS.RootTable.Columns["stt_hthi_dichvu"];
                //GridEXColumn stt_dvu = grdGiaCLS.RootTable.Columns["stt_hthi_chitiet"];
                //Utility.SetGridEXSortKey(grdGiaCLS, stt_loaidichvu, Janus.Windows.GridEX.SortOrder.Ascending);
                //Utility.SetGridEXSortKey(grdGiaCLS, stt_dvu, Janus.Windows.GridEX.SortOrder.Ascending);
                grdGiaCLS.MoveFirst();
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
            cmdSuaBogia.Enabled =Utility.isValidGrid(grdList);
            cmdXoaBogia.Enabled = Utility.isValidGrid(grdList);
         
        }
        
        

        private void cmdThemBogia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                frm_ThemBogia _ThemBogia = new frm_ThemBogia();
                _ThemBogia._OnCreated += _ThemBogia__OnCreated;
                _ThemBogia.m_enAct = newaction.Insert;
                _ThemBogia.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
           
           
        }

        void frm__OnActionSuccess()
        {
            grdList_SelectionChanged(grdList, new EventArgs());
        }
       
        /// <summary>
        /// hàm thực hiện sửa thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaBogia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                 id_bogia = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,DmucBogiadichvu.Columns.IdBogia), -1);
                if (id_bogia <= 0)
                {
                    return;
                }
                frm_ThemBogia _ThemBogia = new frm_ThemBogia();
                _ThemBogia._OnCreated += _ThemBogia__OnCreated;
                _ThemBogia._objBogia = DmucBogiadichvu.FetchByID(id_bogia);
                _ThemBogia.m_enAct = newaction.Update;
                _ThemBogia.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
          
        }

        private void _ThemBogia__OnCreated(int id, newaction m_enAct)
        {
            try
            {
                AllowTextChanged = false;
                SPs.QheDichvuclsSaochepbogia(id_thamchieu, id, globalVariables.UserName).Execute();
                DataTable dt_temp = SPs.QheGiadoituongClsTimdanhsachbogia(id, "-1", "-1",new DateTime(1900,1,1)).GetDataSet().Tables[0];
                if (m_enAct == newaction.Delete)
                {
                    //if (DeleteMe())
                    //{
                    //    DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbBienbanhoichan.Columns.Id, grdList.GetValue(KcbBienbanhoichan.Columns.Id)));
                    //    if (arrDr.Length > 0)
                    //        m_dtData.Rows.Remove(arrDr[0]);
                    //    m_dtData.AcceptChanges();
                    //}
                }
                if (m_enAct == newaction.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == newaction.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("id_bogia=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["ma_bogia"] = dt_temp.Rows[0]["ma_bogia"];
                        arrDr[0]["ten_bogia"] = dt_temp.Rows[0]["ten_bogia"];
                        arrDr[0]["ngay_batdau"] = dt_temp.Rows[0]["ngay_batdau"];
                        arrDr[0]["ngay_ketthuc"] = dt_temp.Rows[0]["ngay_ketthuc"];
                        arrDr[0]["trang_thai"] = dt_temp.Rows[0]["trang_thai"];
                        arrDr[0]["mota_them"] = dt_temp.Rows[0]["mota_them"];
                        arrDr[0]["ngay_tao"] = dt_temp.Rows[0]["ngay_tao"];
                        arrDr[0]["nguoi_tao"] = dt_temp.Rows[0]["nguoi_tao"];
                        arrDr[0]["ngay_sua"] = dt_temp.Rows[0]["ngay_sua"];
                        arrDr[0]["nguoi_sua"] = dt_temp.Rows[0]["nguoi_sua"];
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                AllowTextChanged = true;
                Utility.GotoNewRowJanus(grdList, "id_bogia", id.ToString());
                grdList_SelectionChanged(grdList, new EventArgs());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                AllowTextChanged = true;
                id_thamchieu = -1;
                ModifyCommand();
            }
        }

        private void frm_quanly_bogia_bhyt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);      
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        
            if (e.KeyCode == Keys.N && e.Control) cmdThemBogia.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSuaBogia.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaBogia.PerformClick();
           
        }

        private ActionResult actionResult;
        /// <summary>
        /// hàm thực hiện việc xóa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaBogia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 Bộ giá cần xóa");
                    return;
                }
                 if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa Bộ giá đang chọn không ?", "Thông báo", true))
                {

                    int IdBogia = Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdList, DmucBogiadichvu.Columns.IdBogia), -1);
                    using (var scope = new TransactionScope())
                    {
                        using (var sh = new SharedDbConnectionScope())
                        {
                            new Delete().From(DmucBogiadichvu.Schema).Where(DmucBogiadichvu.Columns.IdBogia).IsEqualTo(IdBogia).Execute();
                            new Delete().From(QheDoituongDichvucl.Schema).Where(QheDoituongDichvucl.Columns.IdBogia).IsEqualTo(IdBogia).Execute();
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
        int id_thamchieu = -1;
        private void MnuClone_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                string question = string.Format("Bạn có chắc chắn muốn tạo một bộ giá mới dựa trên bộ giá {0}?", grdList.GetValue("ten_bogia"));
                if (!Utility.AcceptQuestion(question, "Xác nhận", true)) return;
                 id_thamchieu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucBogiadichvu.Columns.IdBogia), -1);
                frm_ThemBogia _ThemBogia = new frm_ThemBogia();
                _ThemBogia._OnCreated += _ThemBogia__OnCreated;
                _ThemBogia.m_enAct = newaction.Insert;
                _ThemBogia.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                
            }
        }

        private void CmdHieuluc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                string question = string.Format("Bạn có chắc chắn muốn kích hoạt hiệu lực cho bộ giá {0}. Đồng nghĩa với việc toàn bộ các bộ giá còn lại sẽ hết hiệu lực sử dụng?", grdList.GetValue("ten_bogia"));
                if (!Utility.AcceptQuestion(question, "Xác nhận", true)) return;
                int id_bogia = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucBogiadichvu.Columns.IdBogia), -1);
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                    {
                        new Update(DmucBogiadichvu.Schema).Set(DmucBogiadichvu.Columns.TrangThai).EqualTo(1).Where(DmucBogiadichvu.Columns.IdBogia).IsEqualTo(id_bogia).Execute();
                        new Update(DmucBogiadichvu.Schema).Set(DmucBogiadichvu.Columns.TrangThai).EqualTo(0).Where(DmucBogiadichvu.Columns.IdBogia).IsNotEqualTo(id_bogia).Execute();
                    }
                    scope.Complete();
                }
                (from p in m_dtData.AsEnumerable() where Utility.Int32Dbnull(p[DmucBogiadichvu.Columns.IdBogia], -1)== id_bogia select p).ToList().ForEach(x => x["trang_thai"] = 1);
                (from p in m_dtData.AsEnumerable() where Utility.Int32Dbnull(p[DmucBogiadichvu.Columns.IdBogia], -1) != id_bogia select p).ToList().ForEach(x => x["trang_thai"] = 0);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Đánh dấu bộ giá có hiệu lực duy nhất {0} thành công ", grdList.GetValue("ten_bogia")), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void ChkHienthiCogia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_dtChitiet.DefaultView.RowFilter = chkHienthiCogia.Checked ? "id_quanhe>0" : "1=1";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                string question = string.Format("Bạn có chắc chắn muốn kích hoạt hiệu lực cho bộ giá {0}?\n", grdList.GetValue("ten_bogia"));
                if (!Utility.AcceptQuestion(question, "Xác nhận", true)) return;
                int id_bogia = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucBogiadichvu.Columns.IdBogia), -1);
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                    {
                        new Update(DmucBogiadichvu.Schema).Set(DmucBogiadichvu.Columns.TrangThai).EqualTo(1).Where(DmucBogiadichvu.Columns.IdBogia).IsEqualTo(id_bogia).Execute();
                    }
                    scope.Complete();
                }
                (from p in m_dtData.AsEnumerable() where Utility.Int32Dbnull(p[DmucBogiadichvu.Columns.IdBogia], -1) == id_bogia select p).ToList().ForEach(x => x["trang_thai"] = 1);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Kích hoạt bộ giá {0} thành công ", grdList.GetValue("ten_bogia")),newaction.Restore , this.GetType().Assembly.ManifestModule.Name);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuSuspend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                string question = string.Format("Bạn có chắc chắn muốn tạm dừng hiệu lực cho bộ giá {0}?", grdList.GetValue("ten_bogia"));
                if (!Utility.AcceptQuestion(question, "Xác nhận", true)) return;
                int id_bogia = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, DmucBogiadichvu.Columns.IdBogia), -1);
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                    {
                        new Update(DmucBogiadichvu.Schema).Set(DmucBogiadichvu.Columns.TrangThai).EqualTo(0).Where(DmucBogiadichvu.Columns.IdBogia).IsEqualTo(id_bogia).Execute();
                    }
                    scope.Complete();
                }
                (from p in m_dtData.AsEnumerable() where Utility.Int32Dbnull(p[DmucBogiadichvu.Columns.IdBogia], -1) == id_bogia select p).ToList().ForEach(x => x["trang_thai"] = 0);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Tạm dừng bộ giá {0} thành công ", grdList.GetValue("ten_bogia")), newaction.Restore, this.GetType().Assembly.ManifestModule.Name);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
               
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                ////grdGiaCLS.RootTable.Groups.Clear();
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                saveFileDialog1.FileName = "dmuc_dichvu_CLS.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdExportFile;
                        gridEXExporter1.Export(s);

                    }
                   // m_dtChitiet.DataSet.WriteXml(saveFileDialog1.FileName);
                    Utility.ShowMsg("Xuất Excel thành công.\nChú ý: Bạn chỉ cần điền thông tin cho các cột : giá BHYT, giá phụ thu đúng tuyến, giá phụ thu trái tuyến, tỷ lệ thanh toán BHYT");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog _OpenFileDialog = new OpenFileDialog();
                if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadExcel(_OpenFileDialog.FileName);
                    //LoadDataFromFileExcelToGrid(_OpenFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi mở file Excel", ex);
            } 
        }
        void LoadExcel(string filePath)
        {
            IExcelDataReader excelReader = null;
            try
            {
                DataSet ods = new DataSet();
             //   using (System.Data.OleDb.OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
             //"Provider=Microsoft.ACE.OLEDB.12.0; " +
             // "data source='" + filePath + "';" +
             //    "Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\" "))
             //   {
             //       myConnection.Open();
             //       string sql="select * from [Sheet1$]";
             //       using (System.Data.OleDb.OleDbDataAdapter myImportCommand = new System.Data.OleDb.OleDbDataAdapter(sql, myConnection))
             //           myImportCommand.Fill(ods);
             //       myConnection.Close();
             //   }
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                //1. Reading from a binary Excel file ('97-2003 format; *.xls)


                if (Path.GetExtension(filePath).ToUpper() == ".XLS") excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                else excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //...
                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                excelReader.IsFirstRowAsColumnNames = true;
                ods = excelReader.AsDataSet();

                if (ods != null && ods.Tables.Count > 0 && ods.Tables[0].Rows.Count > 0)
                {
                    if (!CheckColumn(ods.Tables[0]))
                    {

                        Utility.ShowMsg("File excel của bạn không đúng định dạng. File nên xuất ra bằng tính năng Export phía trên và bắt buộc chứa các cột thông tin sau:id_chitietdichvu,gia_bhyt(giá BHYT),tyle_tt(Tỷ lệ thanh toán BHYT đối với dịch vụ),phuthu_dungtuyen(giá phụ thu đúng tuyến),phuthu_traituyen(giá phụ thu trái tuyến) ");
                        return;
                    }
                }
                else
                {
                    Utility.ShowMsg("Không có dữ liệu danh mục dịch vụ CLS từ file Excel");
                    return;
                }
                frm_xemthongtingia _xemthongtingia = new frm_xemthongtingia(ods.Tables[0]);
                if (_xemthongtingia.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
                    CapnhatGiaQhe(ods.Tables[0]);
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi nạp danh mục thuốc từ file Excel", ex);
            }
            finally
            {
               if(excelReader!=null) excelReader.Close();
            }
        }
        void CapnhatGiaQhe(DataTable dtExcelData)
        {

            if (objBHYT != null)
            {
                prgb_update.Visible = true;
                prgb_update.Maximum = grdGiaCLS.GetDataRows().Count();
                prgb_update.Minimum = 0;
                prgb_update.Step = 1;
                prgb_update.Value = 1;
                grdGiaCLS.RootTable.Columns["act"].Visible = true;
                if (!dtExcelData.Columns.Contains("id_dvu_cls")) dtExcelData.Columns.Add(new DataColumn("id_dvu_cls", typeof(decimal)));
                (from p in dtExcelData.AsEnumerable() select p).ToList().ForEach(x => x["id_dvu_cls"] = Utility.DecimaltoDbnull(x["id_chitietdichvu"],0));
                Application.DoEvents();
                foreach (GridEXRow _row in grdGiaCLS.GetDataRows())
                {
                    _row.BeginEdit();
                    try
                    {
                        if (prgb_update.Value + 1 > prgb_update.Maximum)
                            prgb_update.Value = prgb_update.Maximum;
                        else
                            prgb_update.Value += 1;
                        Application.DoEvents();

                        DataRow[] arrDr = dtExcelData.Select(string.Format("id_dvu_cls='{0}'", Utility.DecimaltoDbnull(_row.Cells["id_chitietdichvu"].Value, 0)));
                        if (arrDr.Length > 0)//==1 100%
                        {
                            decimal GiaBHYT = Utility.DecimaltoDbnull(arrDr[0]["Gia_BHYT"], 0);
                            decimal GiaDV = Utility.DecimaltoDbnull(arrDr[0]["Gia_Dichvu"], 0);
                            if (GiaBHYT > 0)
                            {
                                decimal GiaPhuThu = Utility.DecimaltoDbnull(arrDr[0]["Phuthu_Traituyen"], 0);
                                decimal GiaPhuThu_DT = Utility.DecimaltoDbnull(arrDr[0]["Phuthu_Dungtuyen"], 0);
                                decimal Tyle_TT = Utility.DecimaltoDbnull(arrDr[0]["tyle_tt"], 100);
                                int num = -1;
                                long id_quanhe = Utility.Int64Dbnull(_row.Cells["id_quanhe"].Value, -1);
                                if (chkAutoEdit.Checked && GiaPhuThu <= 0)
                                    GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                                if (id_quanhe > 0)//Update
                                {
                                    num = new Update(QheDoituongDichvucl.Schema)
                                     .Set(QheDoituongDichvucl.Columns.DonGia).EqualTo(GiaBHYT)
                                    .Set(QheDoituongDichvucl.Columns.TyleTt).EqualTo(Tyle_TT)
                                    .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(GiaPhuThu)
                                    .Set(QheDoituongDichvucl.Columns.PhuthuDungtuyen).EqualTo(GiaPhuThu_DT)
                                    .Set(QheDoituongDichvucl.Columns.IdBogia).EqualTo(id_bogia)
                                    .Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(id_quanhe).Execute();
                                    if (chkLogExcel.Checked) Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật giá BHYT từ file Excel của dịch vụ {0},don_gia={1},tyle_tt={2},Phuthu_Dungtuyen={3},Phuthu_Traituyen={4} ", _row.Cells["ten_chitietdichvu"].Value, GiaBHYT, Tyle_TT, GiaPhuThu_DT, GiaPhuThu), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                                    _row.Cells["act"].Value = "Cập nhật";
                                }
                                else//Insert
                                {
                                    QheDoituongDichvucl _newItem = new QheDoituongDichvucl();
                                    _newItem.IdDoituongKcb = objBHYT.IdDoituongKcb;
                                    _newItem.IdDichvu = Utility.Int16Dbnull(_row.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                                    _newItem.IdChitietdichvu = Utility.Int32Dbnull(_row.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                                    _newItem.TyleGiam = 0;
                                    _newItem.KieuGiamgia = 1;
                                    _newItem.TyleTt = Tyle_TT;
                                    _newItem.MotaThem = "";
                                    _newItem.DonGia = GiaBHYT;
                                    _newItem.IdBogia = id_bogia;
                                    _newItem.PhuthuDungtuyen = GiaPhuThu_DT;
                                    _newItem.IdLoaidoituongKcb = objBHYT.IdLoaidoituongKcb;
                                    _newItem.PhuthuTraituyen = GiaPhuThu;
                                    _newItem.MaDoituongKcb = objBHYT.MaDoituongKcb;
                                    _newItem.NgayTao = globalVariables.SysDate;
                                    _newItem.NguoiTao = globalVariables.UserName;
                                    _newItem.MaKhoaThuchien = KTH;
                                    _newItem.IsNew = true;
                                    _newItem.Save();
                                    id_quanhe = _newItem.IdQuanhe;
                                    _row.Cells["act"].Value = "Thêm mới";
                                    if (chkLogExcel.Checked) Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới giá BHYT từ file Excel của dịch vụ {0},don_gia={1},tyle_tt={2},Phuthu_Dungtuyen={3},Phuthu_Traituyen={4} ", _row.Cells["ten_chitietdichvu"].Value, GiaBHYT, Tyle_TT, GiaPhuThu_DT, GiaPhuThu), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                                }
                                _row.Cells["id_quanhe"].Value = id_quanhe;
                                _row.Cells["Import_OK"].Value = 1;
                                _row.Cells["gia_bhyt"].Value = GiaBHYT;
                                _row.Cells[QheDoituongDichvucl.Columns.TyleTt].Value = Tyle_TT;
                                _row.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value = GiaPhuThu;
                                _row.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value = GiaPhuThu_DT;
                                
                            }
                            else
                            {
                                _row.Cells["Import_OK"].Value = 2;
                                _row.Cells["act"].Value = "Price is Zero";
                            }
                        }
                        else
                        {
                            _row.Cells["Import_OK"].Value = 2;
                            _row.Cells["act"].Value = "Data not found";
                        }
                    }
                    catch (Exception ex)
                    {
                        _row.Cells["Import_OK"].Value = 2;
                        _row.Cells["act"].Value = ex.ToString();
                       
                    }
                    finally
                    {
                        _row.EndEdit();
                        grdGiaCLS.MoveTo(_row);
                        //Application.DoEvents();
                    }
                }
                prgb_update.Visible = false;
            }
        }
        bool CheckColumn(DataTable dt)
        {
            return dt.Columns.Contains("id_chitietdichvu")
                && dt.Columns.Contains("gia_bhyt")
                && dt.Columns.Contains("gia_dichvu")
                && dt.Columns.Contains("tyle_tt")
                && dt.Columns.Contains("phuthu_dungtuyen")
                && dt.Columns.Contains("phuthu_traituyen")
                ;

        }
    }
}
