using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using System.Data.OleDb;
using System.Transactions;
using Janus.Windows.GridEX;
using System.IO;
using Excel;
namespace VNS.HIS.UI.Forms.DUOC
{
    public partial class frm_CLS_Exel : Form
    {
        public bool m_blnCancel = true;
        public frm_CLS_Exel()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += new EventHandler(frm_CLS_Exel_Load);
            this.KeyDown += new KeyEventHandler(frm_CLS_Exel_KeyDown);
            cmdExport.Click += new EventHandler(cmdExport_Click);
            cmdImport.Click += new EventHandler(cmdImport_Click);
            cmdLoadExcel.Click += new EventHandler(cmdLoadExcel_Click);
        }

        void frm_CLS_Exel_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        void frm_CLS_Exel_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable m_dtObjectType = new Select().From(DmucDoituongkcb.Schema).OrderAsc(DmucDoituongkcb.Columns.SttHthi).ExecuteDataSet().Tables[0];
                grdObjectTypeList.DataSource = m_dtObjectType;
                Try2LoadHelp();
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi khởi tạo chức năng", ex);

            }
        }
        void Try2LoadHelp()
        {
            try
            {
                string filehelp = Application.StartupPath + @"\Help\Nhap_xuat_exel_thuoc.rtf";
                if (File.Exists(filehelp))
                {
                    lblNofile.SendToBack();
                    txtHelp.LoadFile(filehelp);
                }
                else
                {
                    lblNofile.BringToFront();
                    Utility.SetMsg(lblNofile, "Không tìm thấy file hướng dẫn tại địa chỉ " + filehelp, true);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi khởi tạo chức năng", ex);

            }
        }
        void cmdLoadExcel_Click(object sender, EventArgs e)
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
                 FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                 //1. Reading from a binary Excel file ('97-2003 format; *.xls)


                 if (optOffice2003.Checked) excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                 else excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                 //...
                 //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                 //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                 //...
                 //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                 excelReader.IsFirstRowAsColumnNames = true;
                 DataSet ods = excelReader.AsDataSet();
                 if (ods != null && ods.Tables.Count > 0 && ods.Tables[0].Rows.Count > 0)
                 {
                     if (!CheckColumn(ods.Tables[0]))
                     {

                         Utility.ShowMsg("File excel của bạn không đúng định dạng. Nhấn OK để hệ thống sinh file mẫu và mời bạn đọc lại hướng dẫn nạp dữ liệu từ file Excel");
                         Save2Excel(false);
                         tabControl1.SelectedTab = tabControl1.TabPages[1];
                         return;
                     }
                 }
                 else
                 {
                     Utility.ShowMsg("Không có dữ liệu danh mục thuốc từ file Excel");
                     return;
                 }
                 if (!ods.Tables[0].Columns.Contains("Error")) ods.Tables[0].Columns.Add(new DataColumn("Error", typeof(int)));
                 if (!ods.Tables[0].Columns.Contains("Success")) ods.Tables[0].Columns.Add(new DataColumn("Success", typeof(int)));
                 dtdvt = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("DONVITINH").ExecuteDataSet().Tables[0];
                 SysSystemParameter objdvt = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("MA_DVTINH_MACDINH").ExecuteSingle<SysSystemParameter>();
                 if (objdvt != null)
                     ma_dvt_macdinh = objdvt.SValue;
                 foreach (DataRow dr in ods.Tables[0].Rows)
                 {
                     dr[DmucThuoc.Columns.MaDonvitinh] = Utility.DoTrim(Utility.sDbnull(dr[DmucThuoc.Columns.MaDonvitinh], "")).ToUpper();
                     dr[DmucThuoc.Columns.MaDonvitinh] = chkAutomap.Checked ? getmadvitinh(Utility.sDbnull(dr[DmucThuoc.Columns.MaDonvitinh], "")) : Utility.sDbnull(dr[DmucThuoc.Columns.MaDonvitinh], "");
                     dr["Error"] = 0;
                     dr["Success"] = 1;
                 }
                 //List<string> lstmadvitinh = (from q in ods.Tables[0].AsEnumerable()
                 //                             select q["ma_dvitinh"].ToString()).Distinct().ToList<string>();
                 Utility.SetDataSourceForDataGridEx(grdList, ods.Tables[0], true, true, "Success=1", "ten_thuoc");
                 grdList.CheckAllRecords();

                 DataTable dtDichvu = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                 GridEXColumn _colLoaidvu = grdList.RootTable.Columns["ma_dichvu"];
                 _colLoaidvu.HasValueList = true;
                 _colLoaidvu.LimitToList = true;

                 GridEXValueListItemCollection _colIDNganHang_Collection = grdList.RootTable.Columns["ma_dichvu"].ValueList;
                 foreach (DataRow item in dtDichvu.Rows)
                 {
                     _colIDNganHang_Collection.Add(item["ma_dichvu"].ToString(), item["ten_dichvu"].ToString());
                 }

                 //...
                 ////4. DataSet - Create column names from first row
                 //excelReader.IsFirstRowAsColumnNames = true;
                 //DataSet result = excelReader.AsDataSet();

                 ////5. Data Reader methods
                 //while (excelReader.Read())
                 //{
                 //    //excelReader.GetInt32(0);
                 //}

                 //6. Free resources (IExcelDataReader is IDisposable)
             }
             catch (Exception ex)
             {
                 Utility.CatchException("Lỗi khi nạp danh mục thuốc từ file Excel", ex);
             }
            finally
            {
                excelReader.Close();
            }
        }
        private void LoadDataFromFileExcelToGrid(string Path)
        {
             Utility.SetDataSourceForDataGridEx(grdList, null, true, true, "1=1", "ten_thuoc");
            OleDbConnection odbConnection = null;
            if (optOffice2003.Checked) odbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;");
            else
                odbConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=Excel 12.0;");
            string sheetName = "";
            try
            {
                odbConnection.Open();
                DataTable dt = odbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                sheetName = dt.Rows[1]["TABLE_NAME"].ToString();
                OleDbDataAdapter oldbAdapter = new OleDbDataAdapter("Select * from ["+sheetName+"$] ", odbConnection);
                DataSet ods = new DataSet();
                oldbAdapter.Fill(ods);
                if (ods != null && ods.Tables.Count > 0 && ods.Tables[0].Rows.Count > 0)
                {
                    if (!CheckColumn(ods.Tables[0]))
                    {

                        Utility.ShowMsg("File excel của bạn không đúng định dạng. Nhấn OK để hệ thống sinh file mẫu và mời bạn đọc lại hướng dẫn nạp dữ liệu từ file Excel");
                        Save2Excel(false);
                        tabControl1.SelectedTab = tabControl1.TabPages[1];
                        return;
                    }
                }
                else
                {
                    Utility.ShowMsg("Không có dữ liệu danh mục thuốc từ file Excel");
                    return;
                }
                if (!ods.Tables[0].Columns.Contains("Error")) ods.Tables[0].Columns.Add(new DataColumn("Error", typeof(int)));
                if (!ods.Tables[0].Columns.Contains("Success")) ods.Tables[0].Columns.Add(new DataColumn("Success", typeof(int)));
                foreach (DataRow dr in ods.Tables[0].Rows)
                {
                    dr["Error"] = 0;
                    dr["Success"] = 1;
                }
                Utility.SetDataSourceForDataGridEx(grdList, ods.Tables[0], true, true, "Success=1", "ten_thuoc");
                grdList.CheckAllRecords();
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi nạp danh mục thuốc từ file Excel",ex);
            }
            finally
            {
                odbConnection.Close();
            }
        }
        bool CheckColumn(DataTable dt)
        {
            return dt.Columns.Contains(DmucThuoc.Columns.DangBaoche)
                && dt.Columns.Contains(DmucThuoc.Columns.DonGia)
                && dt.Columns.Contains(DmucThuoc.Columns.GiaBhyt)
                && dt.Columns.Contains(DmucThuoc.Columns.HamLuong)
                && dt.Columns.Contains(DmucThuoc.Columns.HangSanxuat)
                && dt.Columns.Contains(DmucThuoc.Columns.MaHoatchat)
                && dt.Columns.Contains(DmucThuoc.Columns.IdLoaithuoc)
                && dt.Columns.Contains(DmucThuoc.Columns.KieuThuocvattu)
                && dt.Columns.Contains(DmucThuoc.Columns.MaDonvitinh)
                && dt.Columns.Contains(DmucThuoc.Columns.MaThuoc)
                && dt.Columns.Contains(DmucThuoc.Columns.MotaThem)
                && dt.Columns.Contains(DmucThuoc.Columns.NoitruNgoaitru)
                && dt.Columns.Contains(DmucThuoc.Columns.NuocSanxuat)
                && dt.Columns.Contains(DmucThuoc.Columns.PhuthuDungtuyen)
                && dt.Columns.Contains(DmucThuoc.Columns.PhuthuTraituyen)
                && dt.Columns.Contains(DmucThuoc.Columns.QD31)
                && dt.Columns.Contains(DmucThuoc.Columns.SoDangky)
                && dt.Columns.Contains(DmucThuoc.Columns.TenBhyt)
                && dt.Columns.Contains(DmucThuoc.Columns.TenThuoc)
                && dt.Columns.Contains(DmucThuoc.Columns.MaTinhchat)
                && dt.Columns.Contains(DmucThuoc.Columns.TrangThai)
                && dt.Columns.Contains(DmucThuoc.Columns.TuTuc)
                && dt.Columns.Contains(DmucThuoc.Columns.TonDau)
                ;

        }
        void cmdImport_Click(object sender, EventArgs e)
        {
            if (grdList.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một thuốc để thực hiện đẩy vào hệ thống");
                return;
            }
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn thay thế dữ liệu thuốc trong hệ thống bằng dữ liệu từ file excel vừa nạp hay không?\nChú ý: Nếu bạn đồng ý thực hiện, toàn bộ dữ liệu liên quan đến thuốc sẽ bị xóa để khởi tạo lại", "Cảnh báo", true))
                if (Utility.AcceptQuestion("Toàn bộ dữ liệu biến động thuốc(Nhập-xuất-tồn) sẽ bị xóa để khởi tạo lại. Bạn có chắc chắn?", "Cảnh báo", true))
                    if (Utility.AcceptQuestion("Toàn bộ dữ liệu kê đơn thuốc, cấp phát thuốc sẽ bị xóa để khởi tạo lại. Bạn có chắc chắn", "Cảnh báo", true))
                        ImportFromExcel(chkQuanhe.Checked);
        }
        DataTable dtdvt = new DataTable();
        string ma_dvt_macdinh = "";
        string getmadvitinh(string ten_dvt)
        {
            if (Utility.DoTrim(ten_dvt).Length <= 0) return "";
            DataRow[] dr = dtdvt.Select("TEN='" + ten_dvt.TrimStart().TrimEnd().ToUpper() + "'");
            if (dr.Length > 0)
                return dr[0]["ma"].ToString();
            else
            {
                string sql = " select max(convert(int,MA)) from dmuc_chung where loai='DONVITINH' and ISNUMERIC(MA) =1";
               string ma =(Utility.Int32Dbnull( new InlineQuery().ExecuteScalar<string>(sql),0)+1).ToString();
                DmucChung obj = new DmucChung();
                obj.Ma = ma;
                obj.Ten = ten_dvt;
                obj.Loai="DONVITINH";
                obj.SttHthi=0;
                obj.TrangThai=1;
                obj.TrangthaiMacdinh=0;
                obj.NguoiTao=globalVariables.UserName;
                obj.NgayTao=globalVariables.SysDate;
                obj.IsNew=true;
                obj.Save();
                DataRow newdr = dtdvt.NewRow();
                Utility.FromObjectToDatarow(obj, ref newdr);
                dtdvt.Rows.Add(newdr);
                dtdvt.AcceptChanges();
                return  obj.Ma ;
            }
            return ma_dvt_macdinh;
        }
        string getMafromName(string[] arrWords)
        {
            string shortcut = "";
            foreach (string word in arrWords)
            {
                if (word.Trim() != "")
                    shortcut += word.Substring(0, 1);
            }
            return shortcut;
        }
         void ImportFromExcel(bool taoquanhe)
        {
            bool hasError = false;
            bool foundMissing = false;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        
                        //new Delete().From(DmucThuoc.Schema).Execute();
                        //if (taoquanhe)
                        //{
                        //    List<int> lstIdDoituongKCB = (from p in grdObjectTypeList.GetCheckedRows()
                        //                                  select Utility.Int32Dbnull(p.Cells[DmucDoituongkcb.Columns.IdDoituongKcb].Value, 0)
                        //                                     ).ToList<int>();
                        //    new Delete().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdDoituongKcb).In(lstIdDoituongKCB).Execute();
                        //}
                        SPs.ResetDuocAll().Execute();
                       
                        
                        int idx = 0;
                        List<string> lstNoitruNgoaitru = new List<string>() { "ALL", "NOI", "NGOAI" };
                        List<string> lstThuoc_VT = new List<string>() { "THUOC", "VT" };
                        progressBar1.Visible = true;
                        lblCount.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = grdList.GetCheckedRows().Length;
                        progressBar1.Value = 0;
                        foreach (GridEXRow row in grdList.GetCheckedRows())
                        {
                            idx++;
                            if (progressBar1.Value + 1 <= progressBar1.Maximum) progressBar1.Value += 1;
                            lblCount.Text = progressBar1.Value.ToString() + " / " + progressBar1.Maximum.ToString();

                            try
                            {
                                if (Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.TenThuoc].Value, "")).Length > 0)
                                {
                                    DmucThuoc _newDmucThuoc = new DmucThuoc();
                                    _newDmucThuoc.DangBaoche = Utility.sDbnull(row.Cells[DmucThuoc.Columns.DangBaoche].Value, "");
                                    _newDmucThuoc.DonGia = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.DonGia].Value, 0);
                                    _newDmucThuoc.GiaDv = _newDmucThuoc.DonGia;
                                    _newDmucThuoc.GiaBhyt = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.GiaBhyt].Value, 0);
                                    _newDmucThuoc.HamLuong = Utility.sDbnull(row.Cells[DmucThuoc.Columns.HamLuong].Value, "");
                                    _newDmucThuoc.HangSanxuat = Utility.sDbnull(row.Cells[DmucThuoc.Columns.HangSanxuat].Value, "");
                                    _newDmucThuoc.MaHoatchat = Utility.sDbnull(row.Cells[DmucThuoc.Columns.MaHoatchat].Value, "");
                                    _newDmucThuoc.IdLoaithuoc = Utility.Int16Dbnull(row.Cells[DmucThuoc.Columns.IdLoaithuoc].Value, -1);
                                    _newDmucThuoc.IdThuoc = -1;
                                    _newDmucThuoc.QD31 = "QĐ316868";
                                    _newDmucThuoc.SoDangky = "SĐK6996";
                                    string KieuThuocvattu = Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.KieuThuocvattu].Value, "THUOC"));
                                    _newDmucThuoc.KieuThuocvattu = KieuThuocvattu == "" || !lstThuoc_VT.Contains(KieuThuocvattu) ? "THUOC" : KieuThuocvattu;
                                    _newDmucThuoc.MaDonvitinh = Utility.sDbnull(row.Cells[DmucThuoc.Columns.MaDonvitinh].Value, "");
                                    string ma_thuoc = Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.MaThuoc].Value, ""));
                                    _newDmucThuoc.MaThuoc = ma_thuoc == "" ? "T" + idx.ToString() : ma_thuoc;
                                    _newDmucThuoc.MotaThem = Utility.sDbnull(row.Cells[DmucThuoc.Columns.MotaThem].Value, "");
                                    string NoitruNgoaitru = Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.NoitruNgoaitru].Value, ""));
                                    _newDmucThuoc.NoitruNgoaitru = NoitruNgoaitru == "" || !lstNoitruNgoaitru.Contains(NoitruNgoaitru) ? "ALL" : NoitruNgoaitru;
                                    _newDmucThuoc.NuocSanxuat = Utility.sDbnull(row.Cells[DmucThuoc.Columns.NuocSanxuat].Value, "");
                                    _newDmucThuoc.PhuthuDungtuyen = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.PhuthuDungtuyen].Value, 0);
                                    _newDmucThuoc.PhuthuTraituyen = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.PhuthuTraituyen].Value, 0);
                                    _newDmucThuoc.QD31 = Utility.sDbnull(row.Cells[DmucThuoc.Columns.QD31].Value, "");
                                    _newDmucThuoc.SoDangky = Utility.sDbnull(row.Cells[DmucThuoc.Columns.SoDangky].Value, "");
                                    _newDmucThuoc.TenBhyt = Utility.sDbnull(row.Cells[DmucThuoc.Columns.TenBhyt].Value, "");
                                    _newDmucThuoc.TenThuoc = Utility.sDbnull(row.Cells[DmucThuoc.Columns.TenThuoc].Value, "");
                                    _newDmucThuoc.MaTinhchat =Utility.sDbnull( row.Cells[DmucThuoc.Columns.MaTinhchat].Value);
                                    _newDmucThuoc.TonDau = Utility.Int32Dbnull(row.Cells[DmucThuoc.Columns.TonDau].Value, 0);
                                    _newDmucThuoc.TrangThai = 1;
                                    _newDmucThuoc.TuTuc = Utility.ByteDbnull(row.Cells[DmucThuoc.Columns.TuTuc].Value, 0);
                                    _newDmucThuoc.IsNew = true;
                                    _newDmucThuoc.Save();
                                    if (taoquanhe)
                                    {
                                        foreach (GridEXRow rowdoituong in grdObjectTypeList.GetCheckedRows())
                                        {
                                            DmucDoituongkcb _DmucDoituongkcb = DmucDoituongkcb.FetchByID(Utility.Int32Dbnull(rowdoituong.Cells[DmucDoituongkcb.Columns.IdDoituongKcb].Value, -1));
                                            QheDoituongThuoc _QheDoituongThuoc = new QheDoituongThuoc();

                                            _QheDoituongThuoc.IdDoituongKcb = _DmucDoituongkcb.IdDoituongKcb;
                                            _QheDoituongThuoc.IdLoaithuoc = _newDmucThuoc.IdLoaithuoc;
                                            _QheDoituongThuoc.IdThuoc = _newDmucThuoc.IdThuoc;
                                            _QheDoituongThuoc.TyleGiamgia = 0;
                                            _QheDoituongThuoc.KieuGiamgia = "%";
                                            _QheDoituongThuoc.DonGia = (THU_VIEN_CHUNG.IsBaoHiem(_DmucDoituongkcb.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(_newDmucThuoc.GiaBhyt, 0) : Utility.DecimaltoDbnull(_newDmucThuoc.DonGia, 0));
                                            _QheDoituongThuoc.PhuthuDungtuyen = (THU_VIEN_CHUNG.IsBaoHiem(_DmucDoituongkcb.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(_newDmucThuoc.PhuthuDungtuyen, 0) : 0m);
                                            _QheDoituongThuoc.PhuthuTraituyen = (THU_VIEN_CHUNG.IsBaoHiem(_DmucDoituongkcb.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(_newDmucThuoc.PhuthuTraituyen, 0) : 0m);
                                            _QheDoituongThuoc.IdLoaidoituongKcb = _DmucDoituongkcb.IdLoaidoituongKcb;

                                            _QheDoituongThuoc.MaDoituongKcb = _DmucDoituongkcb.MaDoituongKcb;
                                            _QheDoituongThuoc.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                                            _QheDoituongThuoc.NgayTao = globalVariables.SysDate;
                                            _QheDoituongThuoc.NguoiTao = globalVariables.UserName;
                                            _QheDoituongThuoc.IsNew = true;
                                            _QheDoituongThuoc.Save();
                                        }
                                    }
                                }
                                else
                                    foundMissing = true;
                            }
                            catch
                            {
                                hasError = true;
                                row.BeginEdit();
                                row.Cells["Error"].Value = 1;
                                row.Cells["Success"].Value = 0;
                                row.EndEdit();
                            }
                            finally
                            {
                                Application.DoEvents();
                            }
                        }


                    }
                    if (hasError)
                    {
                        if (Utility.AcceptQuestion("Có lỗi trong quá trình đẩy dữ liệu thuốc từ file excel vào hệ thống. Bạn có muốn chấp nhận các dữ liệu đã đẩy thành công hay không?\nChú ý: Với các dữ liệu lỗi bạn có thể liên hệ để được trợ giúp để khắc phục"))
                        {
                            Scope.Complete();
                            m_blnCancel = false;
                        }
                    }
                    else
                    {
                        Scope.Complete();
                        Utility.ShowMsg(string.Format("Đã nhập liệu thành công.{0}\nNhấn OK để kết thúc", foundMissing?"\nChú ý: Một số thuốc chưa có tên sẽ không được cập nhật vào hệ thống":""));
                        m_blnCancel = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xuất thuốc ra file Excel", ex);

            }
            finally
            {
                progressBar1.Visible = false;
                lblCount.Visible = false;
            }
        }
        void cmdExport_Click(object sender, EventArgs e)
        {
            Save2Excel(true);
        }
        void Save2Excel(bool allData)
        {
            try
            {
                SaveFileDialog _SaveFileDialog = new SaveFileDialog();
                if (_SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable dtdata = SPs.DmucLaydanhmucthuocXuatexcel(allData?1:-1).GetDataSet().Tables[0];
                    ExportExcel.exportToExcel(dtdata, _SaveFileDialog.FileName, "Danhmuc_thuoc");
                    Utility.OpenProcess(_SaveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xuất thuốc ra file Excel", ex);
            }
        }
    }
}
