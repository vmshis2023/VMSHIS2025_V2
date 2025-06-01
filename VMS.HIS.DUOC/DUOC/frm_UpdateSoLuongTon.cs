using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.Forms.DUOC;
using VNS.Libs;

using VNS.Properties;
using Janus.Windows.GridEX;
namespace VNS.HIS.UI.THUOC
{
    public partial class FrmUpdateSoLuongTon : Form
    {
        private DataTable _mDtKhothuoc = new DataTable();
        private DataTable _mDataFull = new DataTable();
        private DataTable _mDtkho = new DataTable();
        private HisDuocProperties HisDuocProperties;
        bool _hasLoaded = false;
        string _kieuThuocvattu = "THUOCVT";
        public FrmUpdateSoLuongTon()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            grdDieuchinh.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(grdDieuchinh_UpdatingCell);
            grdDieuchinh.SelectionChanged += new EventHandler(grdDieuchinh_SelectionChanged);
            grdList.UpdatingCell += grdList_UpdatingCell;
            
            grdKho.UpdatingCell += grdKho_UpdatingCell;
            cmdUp.Click += new EventHandler(cmdUp_Click);
            cmdDown.Click += new EventHandler(cmdDown_Click);
            lnkNgayhethan.Click += new EventHandler(lnkNgayhethan_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);
            optFIFO.CheckedChanged += new EventHandler(_CheckedChanged);
            optLIFO.CheckedChanged += new EventHandler(_CheckedChanged);
            optUutien.CheckedChanged += new EventHandler(_CheckedChanged);
            optExpireDate.CheckedChanged += new EventHandler(_CheckedChanged);
            cmdRefresh.Click += cmdRefresh_Click;
            Cauhinh();
        }

        void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadThuocTrongKho();
                pnlDieuchinh.Height = grdList.GetDataRows().Length > 0 ? 300 : 0;
            }
            catch (Exception)
            {

                //throw;
            }
        }

        void grdKho_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == TThuockho.Columns.ChophepKedon)
                {
                    int idKho = Utility.Int32Dbnull(grdKho.CurrentRow.Cells[TThuockho.Columns.IdKho].Value);
                    int IdThuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TThuockho.Columns.IdThuoc].Value);
                    int idthuockho =
                        Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    SPs.ThuocCapnhattrangthaikedon(idthuockho, idKho, (byte)e.Value).Execute();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == TThuockho.Columns.ChophepKetutruc)
                {
                    int idKho = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TThuockho.Columns.IdKho].Value);
                    int IdThuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TThuockho.Columns.IdThuoc].Value);

                    new Update(TThuockho.Schema)
                        .Set(TThuockho.Columns.ChophepKetutruc).EqualTo(e.Value)
                        .Where(TThuockho.Columns.IdThuoc).IsEqualTo(IdThuoc)
                        .And(TThuockho.Columns.IdKho).IsEqualTo(idKho)
                        .Execute();

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }

        void _CheckedChanged(object sender, EventArgs e)
        {
            if (!_hasLoaded) return;
            try
            {
                string _value = "STT";
                if (optFIFO.Checked)
                    _value = "FIFO";
                if (optLIFO.Checked)
                    _value = "LIFO";
                if (optExpireDate.Checked)
                    _value = "EXP";
                if (optUutien.Checked)
                    _value = "STT";
                THU_VIEN_CHUNG.Capnhatgiatrithamsohethong("THUOC_KIEUXUATTHUOC", _value);
                ChangeOutType();
            }
            catch
            {
            }

        }


        void SetOutType()
        {
            try
            {
                string _value = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_KIEUXUATTHUOC", "STT", true);
                switch (_value)
                {
                    case "FIFO":
                        optFIFO.Checked = true;
                        break;
                    case "LIFO":
                        optLIFO.Checked = true;
                        break;
                    case "EXP":
                        optExpireDate.Checked = true;
                        break;
                    case "STT":
                        optUutien.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }
        void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._ThuocProperties);
            if (_Properties.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Cauhinh();
        }
        void Cauhinh()
        {
            if (PropertyLib._ThuocProperties != null)
            {
                grdDieuchinh.RootTable.Columns[TThuockho.Columns.SoLuong].EditType = PropertyLib._ThuocProperties.Chophepsuasoluong ? EditType.TextBox : EditType.NoEdit;
                grdDieuchinh.RootTable.Columns[TThuockho.Columns.GiaBan].EditType = PropertyLib._ThuocProperties.Chophepsuagiaban ? EditType.TextBox : EditType.NoEdit;
            }
        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataRow drv in _mDataFull.GetChanges().Rows)
                {
                    int IdThuockho = Utility.Int32Dbnull(drv[TThuockho.Columns.IdThuockho]);
                    int SttBan = Utility.Int32Dbnull(drv[TThuockho.Columns.SttBan]);
                    int SoLuong = Utility.Int32Dbnull(drv[TThuockho.Columns.SoLuong]);
                    decimal GiaBan = Utility.DecimaltoDbnull(drv[TThuockho.Columns.GiaBan]);
                    new Update(TThuockho.Schema)
                       .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                       .Set(TThuockho.Columns.SoLuong).EqualTo(SoLuong)
                       .Set(TThuockho.Columns.GiaBan).EqualTo(GiaBan)
                       .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                       .Execute();
                }
            }
            catch
            {
            }

        }

        void grdDieuchinh_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdDieuchinh))
            {
                cmdUp.Enabled = false;
                cmdDown.Enabled = false;
                return;
            }
            bool canDown = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position < grdDieuchinh.RowCount - 1;
            bool canUp = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position > 0;
            cmdUp.Enabled = canUp;
            cmdDown.Enabled = canDown;
        }

        void lnkNgayhethan_Click(object sender, EventArgs e)
        {

        }

        void cmdDown_Click(object sender, EventArgs e)
        {
            try
            {
                int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                grdDieuchinh.MoveNext();
                int sttBan = grdDieuchinh.CurrentRow.Position + 1;
                if (chkAutoupdate.Checked)
                {
                    new Update(TThuockho.Schema)
                       .Set(TThuockho.Columns.SttBan).EqualTo(sttBan)
                       .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                       .Execute();
                }
                updateData(IdThuockho.ToString(), sttBan);
                bool canDown = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position < grdDieuchinh.RowCount - 1;
                bool canUp = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position > 0;
                cmdUp.Enabled = canUp;
                cmdDown.Enabled = canDown;
            }
            catch (Exception ex)
            {
            }
        }
        void updateData(string idthuockho, int SttBan)
        {
            try
            {
                DataRow[] arrDR = _mDataFull.Select(TThuockho.Columns.IdThuockho + "=" + idthuockho);
                if (arrDR.Length > 0)
                    arrDR[0][TThuockho.Columns.SttBan] = SttBan;
                _mDataFull.AcceptChanges();
            }
            catch
            {
            }
        }
        void cmdUp_Click(object sender, EventArgs e)
        {
            try
            {
                int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                grdDieuchinh.MovePrevious();
                int SttBan = grdDieuchinh.CurrentRow.Position + 1;
                if (chkAutoupdate.Checked)
                {
                    new Update(TThuockho.Schema)
                        .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                        .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                        .Execute();
                }
                updateData(IdThuockho.ToString(), SttBan);
                bool canDown = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position < grdDieuchinh.RowCount - 1;
                bool canUp = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position > 0;
                cmdUp.Enabled = canUp;
                cmdDown.Enabled = canDown;
            }
            catch (Exception ex)
            {
            }
        }

        void grdDieuchinh_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key.ToUpper() == TThuockho.Columns.SttBan.ToUpper())
                {
                    int SttBan = Utility.Int32Dbnull(e.Value);
                    int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    if (chkAutoupdate.Checked)
                    {
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                            .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                            .Execute();
                    }
                    _mDataFull.AcceptChanges();
                    grdDieuchinh.Refetch();
                    Utility.GotoNewRowJanus(grdDieuchinh, TThuockho.Columns.IdThuockho, IdThuockho.ToString());
                }
                if (e.Column.Key.ToUpper() == TThuockho.Columns.SoLuong.ToUpper())
                {
                    if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn sửa số lượng tồn kho?", "Cảnh báo", true))
                    {
                        e.Cancel = true;
                        grdDieuchinh.Refetch();
                        return;
                    }
                    int SoLuong = Utility.Int32Dbnull(e.Value);
                    int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    string tenthuoc = Utility.sDbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.TenThuoc].Value);

                    if (chkAutoupdate.Checked)
                    {
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.SoLuong).EqualTo(SoLuong)
                            .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                            .Execute();
                    }
                    _mDataFull.AcceptChanges();
                    grdDieuchinh.Refetch();
                    Utility.GotoNewRowJanus(grdDieuchinh, TThuockho.Columns.IdThuockho, IdThuockho.ToString());
                    Utility.Log(this.Name, globalVariables.UserName,
                                       string.Format(
                                           "Sửa số lượng tồn của thuốc: {0} thành số lượng là {1} tại kho {2} bởi {3}",
                                           tenthuoc, SoLuong, txtkho.Text, globalVariables.UserName), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
                if (e.Column.Key.ToUpper() == TThuockho.Columns.GiaBan.ToUpper())
                {
                    decimal GiaBan = Utility.DecimaltoDbnull(e.Value);
                    int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    string tenthuoc = Utility.sDbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.TenThuoc].Value);

                    if (chkAutoupdate.Checked)
                    {
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.GiaBan).EqualTo(GiaBan)
                            .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                            .Execute();
                    }
                    _mDataFull.AcceptChanges();
                    grdDieuchinh.Refetch();
                    Utility.GotoNewRowJanus(grdDieuchinh, TThuockho.Columns.IdThuockho, IdThuockho.ToString());
                    Utility.Log(this.Name, globalVariables.UserName,
                                       string.Format(
                                           "Sửa giá bán của thuốc: {0} thành giá bán là {1} tại kho {2} bởi {3}",
                                           tenthuoc, GiaBan, txtkho.Text, globalVariables.UserName), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        void grdList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    pnlDieuchinh.Height = 0;
                    _mDtkho.Rows.Clear();
                }
                else
                {
                    pnlDieuchinh.Height = 300;
                    object idthuoc = Utility.getValueOfGridCell(grdList, TThuockho.Columns.IdThuoc);
                    if (idthuoc != null)
                    {
                        _mDtkho = SPs.ThuocKhochuathuoc(Utility.Int32Dbnull(idthuoc, 0), _kieuThuocvattu, globalVariables.gv_intIDNhanvien).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx(grdKho, _mDtkho, true, true, "1=1", TDmucKho.Columns.TenKho);
                        _mDataFull.DefaultView.RowFilter = TThuockho.Columns.IdThuoc + "=" + idthuoc.ToString();
                    }
                    else
                    {
                        _mDtkho.Rows.Clear();
                        _mDataFull.DefaultView.RowFilter = "1=2";
                    }
                }


            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện viecj thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CauHinh()
        {
            try
            {
                cmdCauHinh.Visible = globalVariables.IsAdmin;
                grdList.RootTable.Columns[TThuockho.Columns.SoLuong].Selectable = HisDuocProperties.ChoPhepSuaSLuongTon || globalVariables.IsAdmin;
                if (PropertyLib._HisDuocProperties.KieuThuocVattu == "VT")
                {
                    grdList.RootTable.Columns[TThuockho.Columns.NgayHethan].Selectable = true;
                    grdList.RootTable.Columns[TThuockho.Columns.GiaNhap].Selectable = true;
                    grdList.RootTable.Columns[TThuockho.Columns.GiaBan].Selectable = true;
                }

                cmdSave.Visible = HisDuocProperties.ChoPhepSuaSLuongTon || globalVariables.IsAdmin;
                timer1.Enabled = HisDuocProperties.Tudonglaysolieu > 0;
                timer1.Interval = Convert.ToInt32(HisDuocProperties.Tudonglaysolieu * 1000);
                timer1.Start();
            }
            catch
            {
            }
        }
        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadThuocTrongKho();
                pnlDieuchinh.Height = grdList.GetDataRows().Length > 0 ? 300 : 0;
            }
            catch (Exception)
            {

                //throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc load lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_UpdateSoLuongTon_Load(object sender, EventArgs e)
        {
            //DataBinding.BindDataCombobox(cboKho, CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA(), TDmucKho.Columns.IdKho,
            //                     TDmucKho.Columns.TenKho, "--Chọn kho thuốc--", true);
            txtkho.Init(CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA(),
                                          new List<string>()
                                              {TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho});
            SetOutType();
            LoadThuocTrongKho();
            CauHinh();
            _hasLoaded = true;
        }

        private void LoadThuocTrongKho()
        {
            TDmucKho _kho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtkho.MyID, -1));
            if (_kho != null)
            {
                _kieuThuocvattu = _kho.KhoThuocVt;
                if (_kieuThuocvattu != "THUOC" || _kieuThuocvattu != "VT")
                    _kieuThuocvattu = "ALL";
                DataSet ds = SPs.ThuocLaythongtinthuoctontrongkhoTheokho(Utility.Int32Dbnull(txtkho.MyID, -1)).GetDataSet();
                _mDtKhothuoc = ds.Tables[0];
                _mDataFull = ds.Tables[1];
                Utility.SetDataSourceForDataGridEx(grdList, _mDtKhothuoc, true, true, "1=1", "TEN_THUOC");
                string Orderby = TThuockho.Columns.SttBan;
                Utility.SetDataSourceForDataGridEx(grdDieuchinh, _mDataFull, true, true, "1=2", getOrderOut());

                if (grdList.GetDataRows().Length > 0)
                {
                    grdList.MoveFirst();
                    grdList_CurrentCellChanged(grdList, new EventArgs());
                }
            }
            else
            {
                _mDtKhothuoc.Rows.Clear();
                _mDataFull.Rows.Clear();
                _mDtkho.Rows.Clear();
            }
        }
        string getOrderOut()
        {
            string Orderby = TThuockho.Columns.SttBan + " ," + TThuockho.Columns.NgayHethan;
            if (optFIFO.Checked)
                Orderby = TThuockho.Columns.NgayNhap + " ," + TThuockho.Columns.NgayHethan;
            if (optLIFO.Checked)
                Orderby = TThuockho.Columns.NgayNhap + " desc," + TThuockho.Columns.NgayHethan;
            if (optExpireDate.Checked)
                Orderby = TThuockho.Columns.NgayHethan + " ," + TThuockho.Columns.NgayNhap;
            if (optUutien.Checked)
                Orderby = TThuockho.Columns.SttBan + " ," + TThuockho.Columns.NgayHethan;
            return Orderby;
        }
        void ChangeOutType()
        {

            _mDataFull.DefaultView.Sort = getOrderOut();
        }
        private void grdList_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {

        }



        private void frm_UpdateSoLuongTon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cmdExit.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                 
                return;
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                cmdSave.PerformClick();
                return;
            }
            if (e.Shift && e.Control && e.Alt && e.KeyCode == Keys.U)
            {
                if (grdDieuchinh.RootTable.Columns["so_luong"].EditType != EditType.TextBox)
                    grdDieuchinh.RootTable.Columns["so_luong"].EditType = EditType.TextBox;
                else
                    grdDieuchinh.RootTable.Columns["so_luong"].EditType = EditType.NoEdit;
                return;
            }
        }





        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (HisDuocProperties.Tudonglaysolieu > 0 && !chkTamdung.Checked)
                    LoadThuocTrongKho();
            }
            catch
            {
            }
        }

        private void grdDieuchinh_CurrentCellChanged(object sender, EventArgs e)
        {
            //object idthuockho = Utility.getValueOfGridCell(grdDieuchinh, TThuockho.Columns.IdThuockho);
            //if (idthuockho != null)
            //{
            //    m_dtkho = SPs.ThuocKhochuathuoc(Utility.Int32Dbnull(idthuockho, 0), kieu_thuocvattu, globalVariables.gv_intIDNhanvien).GetDataSet().Tables[0];
            //    Utility.SetDataSourceForDataGridEx(grdKho, m_dtkho, true, true, "1=1", TDmucKho.Columns.TenKho);
            //    m_dataFull.DefaultView.RowFilter = TThuockho.Columns.IdThuoc + "=" + idthuockho.ToString();
            //}
            //else
            //{
            //    m_dtkho.Rows.Clear();
            //    m_dataFull.DefaultView.RowFilter = "1=2";
            //}
        }
        string KIEU_THUOC_VT = "THUOC";
        private void cmdInTonKho_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable m_dtReport = BAOCAO_THUOC.ThuocBaocaoInTonKhoThuoc(Utility.Int32Dbnull(txtkho.MyID), 
                 Utility.Int32Dbnull(-1), Utility.Int32Dbnull(-1), -1, KIEU_THUOC_VT);
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocaothuocton_theokho.xml");
                //Truyền dữ liệu vào datagrid-view
                //Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, false, true, "1=1", "");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                //Add stt vào datatable
                Utility.AddColumToDataTable(ref m_dtReport, "STT", typeof(Int32));
                int idx = 1;
                foreach (DataRow drv in m_dtReport.Rows)
                {
                    drv["STT"] = idx;
                    idx++;
                }
                m_dtReport.AcceptChanges();
                //Add logo vào datatable
                Utility.UpdateLogotoDatatable(ref m_dtReport);

               

                //Lấy chuỗi condition truyền vào biến ?FromDateToDate trên crpt
                string Condition = string.Format("Thuộc kho :{0}", string.IsNullOrEmpty(txtkho.MyText));
              
                //Lấy tên người tạo báo cáo và gọi crpt
                string StaffName = globalVariables.gv_strTenNhanvien;
                string tieude = "", reportname = "";
                string _reportCode = "thuoc_baocao_intonkhothuoc";
                var crpt = Utility.GetReport(_reportCode, ref tieude, ref reportname);
                if (crpt == null) return;

                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                frmPrintPreview objForm = new frmPrintPreview("BÁO CÁO THUỐC TỒN KHO", crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(m_dtReport);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = _reportCode;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt,"ReportCondition", Condition);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt,"ngay_in", dtNgayInPhieu.Value.ToString("dd/MM/yyyy"));
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message
                    );
            }
        }

        private void cmdUpdateIdThuocKho_Click(object sender, EventArgs e)
        {
            var frm = new FrmUpdateByIdThuocKho();
            frm.IdThuockho = Utility.Int64Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value,-1);
            frm.Ngayhethan = Convert.ToDateTime(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.NgayHethan].Value);
            frm.Ngaynhap = Convert.ToDateTime(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.NgayNhap].Value);
            frm.Gianhap = Utility.DecimaltoDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
            frm.Giadv = Utility.DecimaltoDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.GiaBan].Value, 0);
            frm.Giabhyt = Utility.DecimaltoDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
            frm.Solo = Utility.sDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.SoLo].Value, "");
            frm.IdThuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, -1);
            frm.phuthu_dt = Utility.DecimaltoDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.PhuthuDungtuyen].Value, 0);
            frm.phuthu_tt = Utility.DecimaltoDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.PhuthuTraituyen].Value, 0);
            frm.TenThuoc = Utility.sDbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.TenThuoc].Value, "");
            frm.sodangky = Utility.sDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.SoDky].Value, "");
            frm.stt_thau = Utility.sDbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.SoQdinhthau].Value, "");
            frm.ShowDialog();
        }

        private void txtkho__OnSelectionChanged()
        {
            //try
            //{
            //    LoadThuocTrongKho();
            //    pnlDieuchinh.Height = grdList.GetDataRows().Length > 0 ? 300 : 0;
            //}
            //catch (Exception)
            //{

            //    //throw;
            //}
        }
        
    }
}
