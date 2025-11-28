using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.Forms.Noitru;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Aspose.Words;
using VMS.HIS.Bus.Emr;
using VMS.HIS.UI.EMR;
using Janus.Windows.GridEX;
using VNS.HIS.UI.NGOAITRU;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_DanhsachNguoibenhPTTT : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        KcbChidinhclsChitiet objChitiet;
        KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        DataTable _mDtKhoanoitru;
        long v_id_chitietchidinh;
        string noitru = "2";
        string SplitterPath = "";
        public frm_DanhsachNguoibenhPTTT(string noitru)
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            this.noitru = noitru;
            cmdScanFinger.Visible = true;
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_DanhsachNguoibenhPTTT_Load;
            KeyDown += frm_DanhsachNguoibenhPTTT_KeyDown;
            grdList.SelectionChanged += GrdList_SelectionChanged;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            grdList.CellValueChanged += GrdList_CellValueChanged;
            grdVTTH.SelectionChanged += grdVTTH_SelectionChanged;
            this.Shown += Frm_DanhsachNguoibenhPTTT_Shown;
            this.FormClosing += Frm_DanhsachNguoibenhPTTT_FormClosing;
        }
        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count >= 1)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];

                }
            }
            catch (Exception)
            {

            }
        }
        private void Frm_DanhsachNguoibenhPTTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
        }

        private void Frm_DanhsachNguoibenhPTTT_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(!Utility.isValidGrid(grdList))
                {
                    objChitiet = null;
                    grdVTTH.DataSource = null;
                    cmd_themdon_vtth.Enabled = cmd_suadon_vtth.Enabled = cmd_xoa_vtth.Enabled =cmd_indon_vtth.Enabled= false;
                    return;
                }
                else
                {
                    long id_chitietchidinh = Utility.Int64Dbnull(grdList.GetValue("id_chitietchidinh"));
                    objChitiet = KcbChidinhclsChitiet.FetchByID(id_chitietchidinh);
                    if(objChitiet!=null)
                    {
                        objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                      
                        LayDanhsachVTTH();
                    }    
                   
                }    
            }
            catch (Exception ex)
            {

               
            }
            finally
            {
                ModifyCommmands();
            }
        }

        void grdVTTH_SelectionChanged(object sender, EventArgs e)
        {
            RowVTTH = Utility.findthelastChild(grdVTTH.CurrentRow);
            ModifyCommmands();
        }
        #region Kê VTTH
        private void mnuKeVTTH_Click(object sender, EventArgs e)
        {

        }
        int Pres_ID = -1;
        GridEXRow RowVTTH = null;

        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();

        private void ThemMoiDonVTTH()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.KieuDonthuoc = 4;
                frm.objLuotkham = objLuotkham;
                frm._KcbCDKL = null;
                frm._MabenhChinh = "";
                frm.id_chitietchidinh = objChitiet.IdChitietchidinh;
                frm.id_chitietdichvu = objChitiet.IdChitietdichvu;
                frm.ten_dichvu = Utility.sDbnull(grdList.GetValue("ten_dichvu"));
                frm._Chandoan = "";
                frm.DtIcd = null;
                frm.dt_ICD_PHU = null;
                frm.id_kham = -1;
                frm.objCongkham = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(grdList.GetValue("dien_thoai"));
                frm.txtPatientName.Text = Utility.sDbnull(grdList.GetValue("ten_benhnhan"));
                frm.txtYearBirth.Text = Utility.sDbnull(grdList.GetValue("nam_sinh"));
                frm.txtSex.Text = Utility.sDbnull(grdList.GetValue("gioi_tinh"));
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = "";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {

                    LayDanhsachVTTH();
                    Utility.GotoNewRowJanus(grdVTTH, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
                frm.Dispose();
                frm = null;
                GC.Collect();
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
                ModifyCommmands();

            }
        }
       
        void ModifyCommmands()
        {
            cmd_themdon_vtth.Enabled = objLuotkham != null && objChitiet != null;
            cmd_suadon_vtth.Enabled = cmd_xoa_vtth.Enabled = cmd_indon_vtth.Enabled = cmdWords.Enabled = Utility.isValidGrid(grdVTTH) && objLuotkham != null && objChitiet != null;
        }
        /// <summary>
        /// Kiểm tra xem đã được tổng hợp cấp phát hoặc đã duyệt cấp phát hay chưa
        /// </summary>
        /// <param name="pres_id"></param>
        /// <returns></returns>
        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }

        private void UpdateDonVTTH()
        {
            try
            {
                if (grdVTTH.RowCount > 0)//grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (objLuotkham != null)
                    {


                        if (Donthuoc_DangXacnhan(Pres_ID))
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát thuốc tại phòng Dược");
                            return;
                        }

                        ////Tạm hủy phía dưới vì đơn VTTH sẽ ko được thanh toán
                        //var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        //    .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        //    .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        //    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        //if (v_collect.Count > 0)
                        //{
                        //    Utility.ShowMsg(
                        //        "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                        //    return;
                        //}
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            var frm = new frm_KCB_KE_DONTHUOC("VT");
                            frm.em_Action = action.Update;
                            frm._KcbCDKL = null;
                            frm._MabenhChinh = "";
                            frm.id_chitietchidinh = objChitiet.IdChitietchidinh;
                            frm.id_chitietdichvu = objChitiet.IdChitietdichvu;
                            frm.ten_dichvu = Utility.sDbnull(grdList.GetValue("ten_dichvu"));
                            frm._Chandoan = "";
                            frm.DtIcd = null;
                            frm.dt_ICD_PHU = null;
                            frm.noitru = 0;
                            frm.objLuotkham = objLuotkham;
                            frm.id_kham = -1;
                            frm.objCongkham = null;
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan, "-1");
                            frm.txtSoDT.Text = Utility.sDbnull(grdList.GetValue("dien_thoai"));
                            frm.txtPatientName.Text = Utility.sDbnull(grdList.GetValue("ten_benhnhan"));
                            frm.txtYearBirth.Text = Utility.sDbnull(grdList.GetValue("nam_sinh"));
                            frm.txtSex.Text = Utility.sDbnull(grdList.GetValue("gioi_tinh"));
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.dtNgayKhamLai.MinDate = globalVariables.SysDate;
                            frm._ngayhenkhamlai = "";

                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                LayDanhsachVTTH();
                                Utility.GotoNewRowJanus(grdVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc, Utility.sDbnull(frm.txtPres_ID.Text));
                            }
                            frm.Dispose();
                            frm = null;
                            GC.Collect();
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }
        void CapnhatDonVTTH(KcbChidinhclsChitiet objChitiet, long id_donthuoc)
        {
            var frm = new frm_KCB_KE_DONTHUOC("VT");
            frm.em_Action = action.Update;
            frm._KcbCDKL = null;
            frm._MabenhChinh = "";
            frm.id_chitietchidinh = objChitiet.IdChitietchidinh;
            frm.id_chitietdichvu = objChitiet.IdChitietdichvu;
            frm.ten_dichvu = grdList.GetValue("ten_dichvu").ToString();
            frm._Chandoan = "";
            frm.DtIcd = null;
            frm.dt_ICD_PHU = null;
            frm.noitru = 0;
            frm.objLuotkham = objLuotkham;
            frm.id_kham = -1;
            frm.objCongkham = null;
            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
            frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
            frm.txtPres_ID.Text = Utility.sDbnull(id_donthuoc);
            frm.dtNgayKhamLai.MinDate = globalVariables.SysDate;
            frm._ngayhenkhamlai = "";

            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
            GC.Collect();
        }
     
        private void ThemMoiDonVTTH(KcbChidinhclsChitiet objChitiet)
        {
            try
            {

                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.KieuDonthuoc = 4;
                frm.objLuotkham = objLuotkham;
                frm._KcbCDKL = null;
                frm._MabenhChinh = "";
                frm.id_chitietchidinh = objChitiet.IdChitietchidinh;
                frm.id_chitietdichvu = objChitiet.IdChitietdichvu;
                frm.ten_dichvu = grdList.GetValue("ten_dichvu").ToString();
                frm._Chandoan = "";
                frm.DtIcd = null;
                frm.dt_ICD_PHU = null;
                frm.id_kham = -1;
                frm.objCongkham = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan, "-1");
                frm.txtSoDT.Text = objBenhnhan.DienThoai;
                frm.txtPatientName.Text = objBenhnhan.TenBenhnhan;
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = "";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                frm.Dispose();
                frm = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

            }
        }
        DataTable dtVTTH = new DataTable();
        private void LayDanhsachVTTH()
        {
            try
            {
                dtVTTH =
                     new KCB_THAMKHAM().KcbThamkhamLayDanhsachDonThuocTheolankham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, -1l, -1l, 4, "VT", objChitiet.IdChitietchidinh, 0).Tables[0];
                Utility.SetDataSourceForDataGridEx(grdVTTH, dtVTTH, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        private void PerformActionDeletePres()
        {
            string s = "";
            var lstIdchitiet = new List<int>();
            Utility.AutoCheckGrid(grdVTTH);
            if (grdVTTH.GetCheckedRows().Count() <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết VTTH để xóa");
                return;
            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdVTTH.UpdateData();
            }
            if (lstIdchitiet.Count <= 0) return;
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            DataRow[] rows =
                         dtVTTH.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa đơn VTTH của bệnh nhân ID={0}, PID={1}, Tên={2}, DS VTTH xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham,  Utility.sDbnull(grdList.GetValue("ten_benhnhan")), _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            DeletefromDatatable(lstIdchitiet);
            dtVTTH.AcceptChanges();
        }
        private List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                dtVTTH.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                      KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                      + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }
        private void DeletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in dtVTTH.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    dtVTTH.Rows.Remove(p[i]);
                dtVTTH.AcceptChanges();
            }
            catch
            {
            }
        }
        private bool KiemtraThuocTruockhixoa()
        {
            bool b_Cancel = false;
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các VTTH đang chọn hay không?", "Xác nhận xóa", true)) return false;
            Utility.AutoCheckGrid(grdVTTH);
            if (grdVTTH.GetCheckedRows().Count() <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin VTTH ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_xoa_donthuoc_vtth") || globalVariables.IsAdmin ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các VTTH bạn chọn xóa, có một số VTTH được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa(hoặc cần được cấp quyền quyen_xoa_donthuoc_vtth)");
                    return false;
                }
            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int vIdChitietdonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    KcbDonthuocChitiet kcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(vIdChitietdonthuoc);
                    if (kcbDonthuocChitiet != null && (Utility.Byte2Bool(kcbDonthuocChitiet.TrangthaiThanhtoan) ||
                         Utility.Byte2Bool(kcbDonthuocChitiet.TrangThai)))
                    {
                        b_Cancel = true;
                        break;
                    }
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Một số VTTH bạn chọn đã thanh toán hoặc đã phát VTTH cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            return true;
        }
        #endregion
        private void GrdList_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                string colName = e.Column.Key;
              int num=  new Update(KcbPhieupttt.Schema).Set(colName).EqualTo(grdList.GetValue(colName)).Where(KcbPhieupttt.Columns.IdPhieu).IsEqualTo(grdList.GetValue(KcbPhieupttt.Columns.IdPhieu)).Execute();
            }
            catch (Exception)
            {

               
            }
           
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdUpdate.PerformClick();
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

        private void frm_DanhsachNguoibenhPTTT_Load(object sender, EventArgs e)
        {
            
            InitData();
            
            TimKiemThongTin();
            ModifyCommand();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            autoLoaiPTTT.Init();

        }
        
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdUpdate.Enabled = cmdPrint.Enabled = cmdXoa.Enabled =isValid;
            //cmdDelete.Enabled = cmdTrichBBHC.Enabled = false;
           
        }

        private void TimKiemThongTin()
        {
            try
            {
                DateTime tungay = chkByDate.Checked ? dtFromDate.Value : new DateTime(1900, 1, 1);
                DateTime denngay = chkByDate.Checked ? dtToDate.Value : new DateTime(1900, 1, 1);
                string ma_luotkham = (Utility.DoTrim(txtMaluotkham.Text));
                string ten_benhnhan = (Utility.DoTrim(txtTennguoibenh.Text));
                string ma_phieupttt = Utility.DoTrim(txtmaBBHC.Text);
                int idkhoadieutri = Utility.Int32Dbnull(autoKhoa.MyID, "-1");
                string loaipttt = autoLoaiPTTT.MyCode;
                if (ma_luotkham.Length > 0)
                {
                    tungay = denngay = new DateTime(1900, 1, 1);
                    ten_benhnhan = "";
                    ma_phieupttt = "";
                    loaipttt = "-1";
                }
                byte ntnt = noitru == "0" ? (byte)0 : (noitru == "1" ? (byte)1 : (byte)100);
                m_dtData = SPs.KcbPtttTimkiemdanhsachnguoibenhlamPttt(tungay, denngay, ma_phieupttt, idkhoadieutri, ma_luotkham, ten_benhnhan, loaipttt, ntnt).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_pttt,ten_benhnhan");
                ModifyCommand();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
      
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtMaluotkham.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtMaluotkham.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = MaLuotkham;
                txtMaluotkham.Select(txtMaluotkham.Text.Length, txtMaluotkham.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_DanhsachNguoibenhPTTT_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdInsert.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdate.PerformClick();
           // if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_V2", "0", true) == "0")
            {
                frm_PhieuPTTT _PhieuPTTT = new frm_PhieuPTTT(noitru);
                _PhieuPTTT.m_enAct = action.Insert;
                _PhieuPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Focus();
                _PhieuPTTT.ShowDialog();
            }
            else
            {
                frm_PhieuPTTT_V2 _PhieuPTTT = new frm_PhieuPTTT_V2(noitru);
                _PhieuPTTT.m_enAct = action.Insert;
                _PhieuPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Focus();
                _PhieuPTTT.ShowDialog();
            }    
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("kcb_phieupttt_sua") || globalVariables.UserName == grdList.GetValue("nguoi_tao").ToString())
            {
            }
            else
            {
                Utility.ShowMsg(string.Format("Phiếu PTTT bạn đang chọn do người dùng {0} tạo nên bạn không có quyền sửa phiếu. Muốn sửa phiếu của người khác bạn phải là Admin,Super Admin hoặc có quyền (kcb_phieupttt_sua).\nLiên hệ IT Bệnh viện để được hỗ trợ", grdList.GetValue("nguoi_tao").ToString()));
                return;

            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_V2", "0", true) == "0")
            {
                frm_PhieuPTTT _PhieuPTTT = new frm_PhieuPTTT(noitru);
                _PhieuPTTT.objpttt = KcbPhieupttt.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieu")));
                _PhieuPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));

                _PhieuPTTT.m_enAct = action.Update;
                _PhieuPTTT.ShowDialog();
            }
            else
            {
                frm_PhieuPTTT_V2 _PhieuPTTT = new frm_PhieuPTTT_V2(noitru);
                _PhieuPTTT.objpttt = KcbPhieupttt.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieu")));
                _PhieuPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));

                _PhieuPTTT.m_enAct = action.Update;
                _PhieuPTTT.ShowDialog();
            }    
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("kcb_phieupttt_xoa") || globalVariables.UserName == grdList.GetValue("nguoi_tao").ToString())
                {
                }
                else
                {
                    Utility.ShowMsg(string.Format("Phiếu PTTT bạn đang chọn do người dùng {0} tạo nên bạn không có quyền xóa phiếu. Muốn xóa phiếu của người khác bạn phải là Admin,Super Admin hoặc có quyền (kcb_phieupttt_xoa).\nLiên hệ IT Bệnh viện để được hỗ trợ", grdList.GetValue("nguoi_tao").ToString()));
                    return;
                }
            

               
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString());
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu PTTT trên lưới trước khi thực hiện xóa phiếu PTTT");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu PTTT với mã {0} của người bệnh {1} hay không?", grdList.GetValue(KcbBienbanhoichan.Columns.MaBbhc).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận hủy chuyển viện", true))
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            new Delete().From(KcbBienbanhoichan.Schema).Where(KcbBienbanhoichan.Columns.Id).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(KcbBienbanhoichan.Columns.Id), -1)).Execute();
                        }
                        scope.Complete();
                        Utility.ShowMsg(string.Format("Xóa phiếu PTTT cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbBienbanhoichan.Columns.Id, grdList.GetValue(KcbBienbanhoichan.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();

                    }
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
               ExcelUtlity.ExportGridEx(grdList);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            //try
            //{
            //    Utility.WaitNow(this);
            //    string ma_luotkham = grdList.GetValue(KcbBienbanhoichan.Columns.MaLuotkham).ToString();
            //    long id_phieu = Utility.Int64Dbnull(grdList.GetValue(KcbBienbanhoichan.Columns.IdPhieu));
            //    DataTable dtData =
            //                     SPs.KcbThamkhamPhieuchuyenvien(id_phieu, ma_luotkham).GetDataSet().Tables[0];

            //    if (dtData.Rows.Count <= 0)
            //    {
            //        Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    THU_VIEN_CHUNG.CreateXML(dtData, "thamkham_phieuchuyenvien.XML");
            //    Utility.UpdateLogotoDatatable(ref dtData);
            //    string StaffName = globalVariables.gv_strTenNhanvien;
            //    if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

            //    string tieude = "", reportname = "";
            //    ReportDocument crpt = Utility.GetReport("thamkham_phieuchuyenvien", ref tieude, ref reportname);
            //    if (crpt == null) return;
            //    try
            //    {

            //        frmPrintPreview objForm = new frmPrintPreview("PHIẾU CHUYỂN TUYẾN", crpt, true, dtData.Rows.Count <= 0 ? false : true);
            //        crpt.SetDataSource(dtData);

            //        objForm.mv_sReportFileName = Path.GetFileName(reportname);
            //        objForm.mv_sReportCode = "thamkham_phieuchuyenvien";
            //        Utility.SetParameterValue(crpt, "StaffName", StaffName);
            //        Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            //        Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            //        Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
            //        Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
            //        Utility.SetParameterValue(crpt, "sTitleReport", tieude);
            //        Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(dtpNgayin.Value));
            //        Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            //        Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
            //        objForm.crptViewer.ReportSource = crpt;
            //        objForm.ShowDialog();

            //    }
            //    catch (Exception ex)
            //    {
            //        Utility.CatchException(ex);
            //    }
            //    finally
            //    {
            //        Utility.DefaultNow(this);
            //        GC.Collect();
            //        Utility.FreeMemory(crpt);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTennguoibenh.Clear();
            txtmaBBHC.Clear();
            //autohinhthuchc.SetCode("-1");
            autoLoaiPTTT.SetCode("-1");
            txtmaBBHC.Focus();

        }

        private void cmdScanFinger_Click(object sender, EventArgs e)
        {
            RegisterFinger();
        }
        internal static IntPtr hWnd;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void SendMessageW(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string className, string windowName);
        internal static Process process;

        void RegisterFinger()
        {
            try
            {
                string patientID = Utility.sDbnull(grdList.CurrentRow.Cells["id_benhnhan"].Value, "");
                if (Utility.Int32Dbnull(patientID, -1) > 0)
                {
                    List<string> _list = new List<string>();
                    _list.Add(patientID.ToString());
                    _list.Add(0.ToString());
                    string sPatientInforFile = Application.StartupPath + @"\IVF_FR\PatientInfor.txt";
                    string appName = Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe";
                    if (File.Exists(sPatientInforFile))
                    {
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    else
                    {
                        File.CreateText(sPatientInforFile);
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    Utility.KillProcess(appName);
                    Thread.Sleep(100);
                    process = Process.Start(Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe");
                    if (process != null) process.WaitForExit();
                    WaitForSingleObject(process.Handle, 0xffffffff);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        EmrDocuments emrdoc = new EmrDocuments();
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Coquyen("noitru_phieupttt_xoa") || globalVariables.UserName == grdList.GetValue("nguoi_tao"))
                {
                }
                else
                {
                    Utility.thongbaokhongcoquyen("noitru_phieupttt_xoa", "xóa phiếu phẫu thuật thủ thuật");
                    return;
                }
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn một phiếu PTTT trên danh sách để xóa. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin phiếu PTTT đang chọn không ?", "Thông báo", true))
                {
                    long IdPhieu = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                    int banghi = new Delete().From<KcbPhieupttt>()
                         .Where(KcbPhieupttt.Columns.IdPhieu)
                         .IsEqualTo(IdPhieu)
                         .Execute();
                    emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUPTTT, "");//Xóa tất cả các phiếu liên quan đến phiếu này
                    //emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUPTTT, "PHIEU_CAMKET_PTTT");
                    //emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUPTTT, "PHIEU_CHUNGNHAN_PTTT");
                    //emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUPTTT, "PHIEU_PTTT_NOITRU");
                    //emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUPTTT, "PHIEU_TUONGTRINH_PTTT");
                    if (banghi > 0)
                    {
                        Utility.ShowMsg("Bạn xóa thông tin phiếu PTTT thành công", "Thông báo");
                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                        m_dtData.Rows.Remove(dr);
                        m_dtData.AcceptChanges();

                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        string getFileIn(string ma_loaidvu, string loai_phieu, string ten_file_macdinh)
        {
            List<string> lst_file_in = THU_VIEN_CHUNG.Laygiatrithamsohethong(loai_phieu, ten_file_macdinh, true).Split('@').ToList<string>();
            if (ma_loaidvu == "PTTT" || ma_loaidvu == "PHAUTHUAT" || ma_loaidvu == "PT")
                return lst_file_in[0];
            return lst_file_in[1];//Thủ thuật

        }
        private void mnuInPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                string ma_loaidvu = Utility.sDbnull(grdList.GetValue("ma_dichvu"), "PTTT");
                long ID_PHIEUPTTT = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                KcbPhieupttt objpttt = KcbPhieupttt.FetchByID(ID_PHIEUPTTT);
                DataTable dtData = SPs.KcbPtttInphieu(ID_PHIEUPTTT).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                List<string> fieldNames = new List<string>();
                string file_in = getFileIn(ma_loaidvu, "PHIEU_PTTT_NOITRU", "PHIEU_PTTT_NOITRU.doc");
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + string.Format("Doc\\{0}", file_in);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport(file_in, ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), file_in, grdList.GetValue("ma_luotkham").ToString(), Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    doc.MailMerge.Execute(drData);
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuInchungnhanPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKiemtra = Utility.ExecuteSql(string.Format("select 1 from noitru_phieuravien where id_benhnhan={0} and ma_luotkham='{1}'", Utility.Int64Dbnull(grdList.GetValue("id_benhnhan")), Utility.sDbnull(grdList.GetValue("ma_luotkham"))), CommandType.Text).Tables[0];
                if (dtKiemtra != null && dtKiemtra.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Người bệnh chưa làm giấy ra viện nên thông tin tình trạng ra viện trên phiếu chứng nhận chưa có. Vui lòng kiểm tra lại");
                }

                long ID_PHIEUPTTT = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                KcbPhieupttt objpttt = KcbPhieupttt.FetchByID(ID_PHIEUPTTT);
                NoitruPhieuravien objRV= new Select().From(NoitruPhieuravien.Schema)
                .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();

                DataTable dtData = SPs.KcbPtttInphieu(ID_PHIEUPTTT).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU", "GIẤY CHỨNG NHẬN PHẪU THUẬT-THỦ THUẬT", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdList.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["sngay_ravien"] = objRV != null ? Utility.FormatDateTime_gio_ngay_thang_nam(objRV.NgayRavien, "") : "Ngày........tháng.........năm..........";
                List<string> fieldNames = new List<string>();
                string file_in = getFileIn(ma_loaidvu, "PHIEU_CHUNGNHAN_PTTT", "PHIEU_CHUNGNHAN_PTTT.doc");
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory +string.Format( "Doc\\{0}", file_in);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport(file_in, ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), file_in, grdList.GetValue("ma_luotkham").ToString(), Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\PHIEU_PTTT_CHECKED_FIELDS.txt";
                    List<string> lstcheckboxfields = new List<string>();
                    lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    doc.MailMerge.Execute(drData);
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuInCamketPTTT_Click(object sender, EventArgs e)
        {
            try
            {

                long ID_PHIEUPTTT = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                KcbPhieupttt objpttt = KcbPhieupttt.FetchByID(ID_PHIEUPTTT);
                DataTable dtData = SPs.KcbPtttInphieu(ID_PHIEUPTTT).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_CAMKET", "GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC@GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdList.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                List<string> fieldNames = new List<string>();
                string file_in = getFileIn(ma_loaidvu, "PHIEU_CHUNGNHAN_PTTT", "PHIEU_CAMKET_PTTT.doc");
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory +string.Format( "Doc\\{0}", file_in);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport(file_in, ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), file_in, grdList.GetValue("ma_luotkham").ToString(), Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    doc.MailMerge.Execute(drData);
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdKhamtienme_Click(object sender, EventArgs e)
        {
            frm_Phieukhamtienme _Phieukhamtienme = new frm_Phieukhamtienme();
            _Phieukhamtienme.m_enAct = action.Update;
            _Phieukhamtienme.objLuotkham = Utility.isValidGrid(grdList) ? Utility.getKcbLuotkham(grdList.CurrentRow) : null;
            _Phieukhamtienme.ShowDialog();
        }

        private void mnuPhieutuongtrinhPTTT_Click(object sender, EventArgs e)
        {
            try
            {

                long ID_PHIEUPTTT = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                KcbPhieupttt objpttt = KcbPhieupttt.FetchByID(ID_PHIEUPTTT);
                DataTable dtData = SPs.KcbPtttInphieu(ID_PHIEUPTTT).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_TUONGTRINH", "PHIẾU TƯỜNG TRÌNH PHẪU THUẬT@PHIẾU TƯỜNG TRÌNH THỦ THUẬT", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdList.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                List<string> fieldNames = new List<string>();
                string file_in = getFileIn(ma_loaidvu, "PHIEU_TUONGTRINH_PTTT", "PHIEU_TUONGTRINH_PTTT.doc");
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory +string.Format( "Doc\\{0}", file_in);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport(file_in, ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), file_in, grdList.GetValue("ma_luotkham").ToString(), Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));

                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                             w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                             h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("PTTTsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;
                        w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                        h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage,w,h);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    doc.MailMerge.Execute(drData);
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            frm_PhieuPTTT _PhieuPTTT = new frm_PhieuPTTT(noitru);
            _PhieuPTTT.objpttt = KcbPhieupttt.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieu")));
            _PhieuPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
            _PhieuPTTT.ucThongtinnguoibenh_doc_v11.Refresh();
            _PhieuPTTT.m_enAct = action.View;
            _PhieuPTTT.ShowDialog();
        }

        private void cmd_kedon_vtth_Click(object sender, EventArgs e)
        {
            try
            {
                v_id_chitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList, "id_chitietchidinh"), -1);
                objChitiet = KcbChidinhclsChitiet.FetchByID(v_id_chitietchidinh);
                if (objChitiet == null)
                {
                    Utility.ShowMsg("Không lấy được dữ liệu ca chụp đang chọn. Có thể đã bị xóa trong lúc bạn đang mở chức năng và chưa thực hiện. Vui lòng nhấn lại nút tìm kiếm để làm mới lại dữ liệu danh sách các ca chụp.");
                    return;
                }
                if (Utility.ByteDbnull(objChitiet.TrangthaiHuy, 0) > 0)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn đã hủy hoặc trả lại tiền nên không thể thao tác Kê VTTH. Vui lòng kiểm tra lại");
                    return;
                }
                KcbLuotkham objluotkham = Utility.getKcbLuotkham(objChitiet.IdBenhnhan, objChitiet.MaLuotkham);
                KcbChidinhcl objChidinh = KcbChidinhcl.FetchByID(objChitiet.IdChidinh);
                if (objluotkham != null && objChidinh != null && objChidinh.Noitru == 0 && objluotkham.TrangthaiCapcuu == 0 && objChitiet.TrangThai <= 0)
                {
                    ///////Tạm khoa các dòng dưới đây 25-09-29
                    ////Lấy tiền tạm ứng ngoại trú
                    //decimal tstamung = noitru_TamungHoanung.LaySoTienTamUng(objluotkham.MaLuotkham, Utility.Int64Dbnull(objluotkham.IdBenhnhan), 0);
                    //if (tstamung <= 0)//Nếu ko có tạm ứng thì chỉ thanh toán mới được phép thực hiện
                    //{
                    //    if (objChitiet.TrangthaiThanhtoan <= 0)
                    //    {
                    //        Utility.ShowMsg("Dịch vụ bạn chọn thuộc ngoại trú và chưa được thanh toán/ghi nợ nên không thể thực hiện nhập VTTH");
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    //Kiểm tra tiền tạm ứng có > tiền dịch vụ hay không
                    //    decimal TongChiPhi = KCB_CHIDINH_CANLAMSANG.LayTongSoTienChuaThanhToan(objluotkham.MaLuotkham, objluotkham.IdBenhnhan, Utility.Int32Dbnull(objluotkham.Noitru));
                    //    if (tstamung - TongChiPhi < 0)
                    //    {
                    //        string sTU = String.Format(Utility.FormatDecimal(), tstamung);
                    //        string sTCP = String.Format(Utility.FormatDecimal(), TongChiPhi);
                    //        string sChenhlech = String.Format(Utility.FormatDecimal(), tstamung - TongChiPhi);
                    //        Utility.ShowMsg(string.Format("Tổng tạm ứng: {0} đồng\nTổng chi phí={1} đồng\nTổng chênh lệch=Tổng tạm ứng - Tổng chi phí={2} đồng\n Người bệnh cần nộp thêm tiền tạm ứng ít nhất {3} đồng trước khi thực hiện kê VTTH cho dịch vụ", sTU, sTCP, sChenhlech, sChenhlech));
                    //        return;
                    //    }
                    //}
                }
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objChitiet.IdBenhnhan);
                objLuotkham = KcbLuotkham.FetchByID(objChitiet.MaLuotkham);
                //Kiểm tra xem có đơn VTTH theo chỉ định đang chọn hay chưa
                KcbDonthuoc objdonthuoc = null;
                if (!isNew) objdonthuoc = KcbDonthuoc.FetchByID(Utility.Int64Dbnull(grdVTTH.GetValue("id_donthuoc")));// new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.KieuDonthuoc).IsEqualTo(4).And(KcbDonthuoc.Columns.IdChitietchidinh).IsEqualTo(objChitiet.IdChitietchidinh).ExecuteSingle<KcbDonthuoc>();
                if (objdonthuoc != null || !isNew)
                {
                    if (!IsValid_UpdateDonthuoc(objdonthuoc.IdDonthuoc, "thuốc"))
                    {
                        return;
                    }
                    if (Utility.Coquyen("quyen_suadonthuoc") || objdonthuoc.NguoiTao == globalVariables.UserName)
                    {
                        CapnhatDonVTTH(objChitiet, objdonthuoc.IdDonthuoc);
                    }
                    else
                    {
                        Utility.ShowMsg("Đơn VTTH đang chọn sửa được tạo bởi bác sĩ khác hoặc bạn không được gán quyền sửa(quyen_suadonthuoc). Vui lòng kiểm tra lại");
                        return;
                    }
                }
                else
                {
                    ThemMoiDonVTTH(objChitiet);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                GrdList_SelectionChanged(grdList, e);
            }
        }
        private bool IsValid_UpdateDonthuoc(long id_donthuoc, string thuoc_vt)
        {
            TPhieuCapphatChitiet _capphat = new Select().From(TPhieuCapphatChitiet.Schema).Where(TPhieuCapphatChitiet.Columns.IdDonthuoc).IsEqualTo(id_donthuoc)
                .ExecuteSingle<TPhieuCapphatChitiet>();
            if (_capphat != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " đã được tổng hợp lĩnh " + thuoc_vt + " nên bạn không được phép sửa. Đề nghị kiểm tra lại");
                return false;
            }
            KcbDonthuoc _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(id_donthuoc)
                .And(KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị kiểm tra lại");
                return false;
            }

            return true;
        }
        bool isNew = false;
        private void cmd_themdon_vtth_Click(object sender, EventArgs e)
        {
            isNew = true;
            cmd_kedon_vtth.PerformClick();
        }

        private void cmd_suadon_vtth_Click(object sender, EventArgs e)
        {
            isNew = false;
            cmd_kedon_vtth.PerformClick();
        }

        private void cmd_xoa_vtth_Click(object sender, EventArgs e)
        {
            if (RowVTTH != null)
            {
                Pres_ID = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc), -1);
                if (!IsValid_UpdateDonthuoc(Pres_ID, "vật tư"))
                {
                    return;
                }
            }
            if (!KiemtraThuocTruockhixoa()) return;
            PerformActionDeletePres();
            ModifyCommmands();
        }

        private void mnu_TinhPhi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdVTTH))
                {
                    
                    return;
                }
                
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn đánh dấu các Thuốc-VTTH đang chọn là Tính phí(Người bệnh PHẢI TRẢ TIỀN cho các Thuốc-VTTH đang chọn) hay không?", "Xác nhận", true))
                {
                    return;
                }
                Utility.AutoCheckGrid(grdVTTH);
                string ten_thuoc_vtth = "";
                foreach (GridEXRow row in grdVTTH.GetCheckedRows())
                {
                    if (Utility.Coquyen("donthuoc_tinhphi_haophi") || Utility.sDbnull(row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                    {
                       //Tiếp tục thực hiện
                    }
                    else
                        continue;
                    ten_thuoc_vtth = string.Format("{0}:{1}", Utility.sDbnull(row.Cells["id_thuoc"].Value), Utility.sDbnull(row.Cells["ten_thuoc"].Value));
                    KcbDonthuocChitiet objVTTH = KcbDonthuocChitiet.FetchByID(Utility.Int64Dbnull(row.Cells["id_chitietdonthuoc"].Value));
                    if (objVTTH != null)
                    {
                        if (!Utility.Byte2Bool(objVTTH.TrangthaiThanhtoan))
                        {
                            new Update(KcbDonthuocChitiet.Schema)
                               .Set(KcbDonthuocChitiet.Columns.TinhChiphi).EqualTo(1)
                                .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.IdDangky).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objVTTH.IdChitietdonthuoc)
                                .Execute();
                            row.BeginEdit();
                            row.Cells["tinh_chiphi"].Value = 1;
                            row.EndEdit();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Đánh dấu Tính phí Thuốc-VTTH: {0} của bệnh nhân ID={1}, PID={2}, Tên={3} thành công", ten_thuoc_vtth, objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, Utility.sDbnull(grdList.GetValue("ten_benhnhan"))), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void mnu_HaoPhi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdVTTH))
                {
                    return;
                }
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn đánh dấu các Thuốc-VTTH đang chọn là hao phí(Người bệnh KHÔNG PHẢI TRẢ TIỀN cho các Thuốc-VTTH đang chọn) hay không?", "Xác nhận", true))
                {
                    return;
                }
                Utility.AutoCheckGrid(grdVTTH);
                string ten_thuoc_vtth = "";
                foreach (GridEXRow row in grdVTTH.GetCheckedRows())
                {
                    if (Utility.Coquyen("donthuoc_tinhphi_haophi") || Utility.sDbnull(row.Cells["nguoi_tao"].Value) == globalVariables.UserName)
                    {
                        //Tiếp tục thực hiện
                    }
                    else
                        continue;
                    ten_thuoc_vtth = string.Format("{0}:{1}", Utility.sDbnull(row.Cells["id_thuoc"].Value), Utility.sDbnull(row.Cells["ten_thuoc"].Value));
                    KcbDonthuocChitiet objVTTH = KcbDonthuocChitiet.FetchByID(Utility.Int64Dbnull(row.Cells["id_chitietdonthuoc"].Value));
                    if (objVTTH != null)
                    {
                        if (!Utility.Byte2Bool(objVTTH.TrangthaiThanhtoan))
                        {
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TinhChiphi).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objVTTH.IdChitietdonthuoc)
                                .Execute();
                            row.BeginEdit();
                            row.Cells["tinh_chiphi"].Value = 0;
                            row.EndEdit();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Đánh dấu Hao phí Thuốc-VTTH: {0} của bệnh nhân ID={1}, PID={2}, Tên={3} thành công", ten_thuoc_vtth, objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, Utility.sDbnull(grdList.GetValue("ten_benhnhan"))), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void mnuInchungnhanPTTTCoBacSy_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKiemtra = Utility.ExecuteSql(string.Format("select 1 from noitru_phieuravien where id_benhnhan={0} and ma_luotkham='{1}'", Utility.Int64Dbnull(grdList.GetValue("id_benhnhan")), Utility.sDbnull(grdList.GetValue("ma_luotkham"))), CommandType.Text).Tables[0];
                if (dtKiemtra != null && dtKiemtra.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Người bệnh chưa làm giấy ra viện nên thông tin tình trạng ra viện trên phiếu chứng nhận chưa có. Vui lòng kiểm tra lại");
                }

                long ID_PHIEUPTTT = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                KcbPhieupttt objpttt = KcbPhieupttt.FetchByID(ID_PHIEUPTTT);
                NoitruPhieuravien objRV = new Select().From(NoitruPhieuravien.Schema)
                .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();

                DataTable dtData = SPs.KcbPtttInphieu(ID_PHIEUPTTT).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU", "GIẤY CHỨNG NHẬN PHẪU THUẬT-THỦ THUẬT", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdList.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                drData["sngay_pttt"] = Utility.FormatDateTime(Utility.sDbnull(drData["sngay_pttt"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["sngay_ravien"] = objRV != null ? Utility.FormatDateTime_gio_ngay_thang_nam(objRV.NgayRavien, "") : "Ngày........tháng.........năm..........";
                List<string> fieldNames = new List<string>();
                string file_in = getFileIn(ma_loaidvu, "PHIEU_CHUNGNHAN_PTTT_BS", "PHIEU_CHUNGNHAN_PTTT_BS.doc");
                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + string.Format("Doc\\{0}", file_in);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport(file_in, ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), file_in, grdList.GetValue("ma_luotkham").ToString(), Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
                    string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\PHIEU_PTTT_CHECKED_FIELDS.txt";
                    List<string> lstcheckboxfields = new List<string>();
                    lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    doc.MailMerge.Execute(drData);
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
