using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using VNS.Properties;
using VNS.Libs.AppUI;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.IO;
using VMS.HIS.Danhmuc.ChidinhCLS_Kedon;
using VNS.HIS.BusRule.Goikham;
using System.Transactions;

namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_KE_DONTHUOC_MUANGOAI : Form
    {
        private readonly KCB_KEDONTHUOC _kedonthuoc = new KCB_KEDONTHUOC();
        private readonly Logger log;
        private readonly Dictionary<long, string> lstChangeData = new Dictionary<long, string>();
        public KcbDangkyKcb objkcbdangky;
        private bool APDUNG_GIATHUOC_DOITUONG = true;
        private bool _allowDrugChanged;
        private bool AllowTextChanged;
        private bool _autoFill;
        private decimal _bhytPtramTraituyennoitru;
        public CallActionKieuKeDon CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
        private bool FilterAgain;
        private bool Giathuoc_quanhe;
        private long IdDonthuoc = -1;
        public string KIEU_THUOC_VT = "THUOC";
        private string LOAIKHOTHUOC = "KHO";
        private bool Manual;
        public short ObjectType_Id = -1;
        private bool Selected;
        private string TEN_BENHPHU = "";
        public KcbChandoanKetluan _KcbCDKL;
        private ActionResult _actionResult = ActionResult.Error;
        private MoneyByLetter _moneyByLetter = new MoneyByLetter();
        private string _rowFilter = "1=1";
        private ActionResult _temp = ActionResult.Success;
        private bool blnHasLoaded;

        private long currentIdthuockho = 0L;
        public int departmentID = -1;
        private DataTable dtStockList;
        public DataTable DtIcd = new DataTable();
        public DataTable dt_ICD_PHU = new DataTable();

        public action em_Action = action.Insert;
        public CallAction em_CallAction = CallAction.FromMenu;
        public bool forced2Add = false;
        public int IdBacsikham = -1;
        private bool hasChanged;
        private bool hasMorethanOne = true;
        public int id_goidv = -1;
        public int id_kham = -1;
        private int id_thuockho = -1;
        private bool isLike = true;
        public bool isLoaded = false;
        private bool isSaved;
        private decimal m_Surcharge;
        public bool m_blnCancel = true;

        public string _ngayhenkhamlai;
        public byte donthuoctaiquay = 0;
        private bool m_blnGetDrugCodeFromList;
        private decimal m_decPrice;
        private decimal phuthu_dungtuyen=0;
        private decimal phuthu_traituyen;
        private DataTable m_dtCD_DVD = new DataTable();
        public DataTable m_dtDanhmucthuoc = new DataTable();
        public DataTable m_dtDonthuocChitiet = new DataTable();
        public DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtqheCamchidinhChungphieu = new DataTable();
        private string madoituong_gia = "DV";
        public byte noitru = 0;
        private TDmucKho objDKho;
        public delegate void OnSaveMe(long id_donthuoc,string KieuthuocVT);
        public event OnSaveMe _OnSaveMe;
        private string GUID = "0000000009999900000000000002";
        private QheDoituongThuoc objectPolicy = null;
        private QheDoituongThuoc objectPolicyTutuc = null;
        public byte KieuDonthuoc=100;
        public long id_chitietchidinh = -1;
        public int id_chitietdichvu = -1;
        public string ten_dichvu = "";
        private string rowFilter = "1=2";

        public byte trong_goi = 0;

        private int tu_tuc;

        public string v_PatientCode = "";
        public int v_Patient_ID = -1;
        public Int16 id_khoa=-1;
        bool STCT = true;
        DataTable dtGiathuocQhe = new DataTable();
        public frm_KCB_KE_DONTHUOC_MUANGOAI(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            GUID = THU_VIEN_CHUNG.GetGUID();
            Utility.SetVisualStyle(this);
            log = LogManager.GetLogger("KCB_KEDONTHUOC");
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            if (KIEU_THUOC_VT == "VT")
            {
                Text = "KÊ VẬT TƯ";
            }
            else
            {
                Text = "KÊ ĐƠN THUỐC";
            }
            base.KeyPreview = true;
            dtpCreatedDate.Value = dtNgayIn.Value = dtNgayKhamLai.Value = globalVariables.SysDate;
            InitEvents();
            CauHinh();
           // Utility.setEnterEvent(this);
            txtChanDoan.Init();
        }

        public string _Chandoan
        {
            get { return txtChanDoan.Text; }
            set { txtChanDoan._Text = value;
            txtChandoantheodon.Text = value;
            }
        }

        public string _MabenhChinh
        {
            get { return txtMaBenhChinh.Text; }
            set { txtMaBenhChinh.Text = value; }
        }

        private int ID_Goi_Dvu { get; set; }

        public string MaDoiTuong { get; set; }


        public KcbLuotkham objLuotkham { get; set; }

        public KcbDangkyKcb objCongkham { get; set; }
        public NoitruPhieudieutri objPhieudieutriNoitru { get; set; }

        public int TrongGoi { get; set; }

        private void AddBenhphu()
        {
            Func<DataRow, bool> predicate = null;
            try
            {
                try
                {
                    if ((txtMaBenhphu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) != "") &&
                        !(txtTenBenhPhu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) == ""))
                    {
                        if (predicate == null)
                        {
                            predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == txtMaBenhphu.Text;
                        }
                        if (!dt_ICD_PHU.AsEnumerable().Where(predicate).Any())
                        {
                            AddMaBenh(txtMaBenhphu.Text, TEN_BENHPHU);
                            txtMaBenhphu.ResetText();
                            txtTenBenhPhu.ResetText();
                            txtMaBenhphu.Focus();
                            txtMaBenhphu.SelectAll();
                            Selected = false;
                        }
                        else
                        {
                            txtMaBenhphu.ResetText();
                            txtTenBenhPhu.ResetText();
                            txtMaBenhphu.Focus();
                            txtMaBenhphu.SelectAll();
                        }
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
                }
            }
            finally
            {
            }
        }

        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            Func<DataRow, bool> predicate = null;
            if (
                !dt_ICD_PHU.AsEnumerable()
                    .Where(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh))
                    .Any())
            {
                DataRow row = dt_ICD_PHU.NewRow();
                row[DmucBenh.Columns.MaBenh] = MaBenh;
                if (predicate == null)
                {
                    predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh;
                }
                EnumerableRowCollection<string> source =
                    globalVariables.gv_dtDmucBenh.AsEnumerable()
                        .Where(predicate)
                        .Select(benh => Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]));
                if (source.Any())
                {
                    row[DmucBenh.Columns.TenBenh] = Utility.sDbnull(source.FirstOrDefault());
                }
                dt_ICD_PHU.Rows.Add(row);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }

     
        List<KcbDonthuocMuangoaiChitiet> lstChitietdonthuoc = new List<KcbDonthuocMuangoaiChitiet>();
             
        private void AddPreDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                setMsg(lblMsg, "", false);
                string ten_thuoc = Utility.sDbnull(txt_tenthuoc.Text, "");
                string dvt = Utility.sDbnull(txtDonvitinh.Text, "");
                decimal sl = Utility.DecimaltoDbnull(txtSoluong.Text, 0);
                string cachdung = Utility.sDbnull(txtCachDung.Text, "");
                string sang = Utility.sDbnull(txtsang.Text, "");
                string trua = Utility.sDbnull(txttrua.Text, "");
                string chieu = Utility.sDbnull(txtchieu.Text, "");
                string toi = Utility.sDbnull(txttoi.Text, "");

                if (ten_thuoc == "")
                {
                    setMsg(lblMsg, "Bạn cần nhập tên thuốc để thực hiện kê đơn", true);
                    txt_tenthuoc.Focus();
                    txt_tenthuoc.SelectAll();
                    return;
                }
                else if (dvt == "")
                {
                    setMsg(lblMsg, "Bạn cần nhập đơn vị tính", true);
                    txtDonvitinh.Focus();
                    txtDonvitinh.SelectAll();
                    return;
                }
                else if (sl <= 0)
                {
                    setMsg(lblMsg, "Số lượng " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " phải lớn hơn 0", true);
                    txtSoluong.Focus();
                    return;
                }
                else if (cachdung == "")
                {
                    setMsg(lblMsg, "Bạn cần nhập cách dùng", true);
                    txtCachDung.Focus();
                    txtCachDung.SelectAll();
                    return;
                }
                DataRow[] arrdr = m_dtDonthuocChitiet.Select(string.Format("ten_thuoc='{0}'", ten_thuoc));
                if (arrdr.Length <= 0)
                {
                    DataRow newDr = m_dtDonthuocChitiet.NewRow();
                   string privateguid = THU_VIEN_CHUNG.GetGUID();
                    newDr["guid"] = privateguid;
                    newDr["ten_thuoc"] = ten_thuoc;
                    newDr["ten_donvitinh"] = dvt;
                    newDr["so_luong"] = sl;
                    newDr["cach_dung"] = cachdung;
                    newDr["sang"] = sang;
                    newDr["trua"] = trua;
                    newDr["chieu"] = chieu;
                    newDr["toi"] = toi;
                    m_dtDonthuocChitiet.Rows.Add(newDr);
                }
                else
                {
                    arrdr[0]["ten_donvitinh"] = dvt;
                    arrdr[0]["so_luong"] = sl+Utility.DecimaltoDbnull(arrdr[0]["so_luong"]);
                    arrdr[0]["cach_dung"] = cachdung;
                    arrdr[0]["sang"] = sang;
                    arrdr[0]["trua"] = trua;
                    arrdr[0]["chieu"] = chieu;
                    arrdr[0]["toi"] = toi;

                }    

                this.txt_tenthuoc.Focus();
                this.txt_tenthuoc.SelectAll();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
              
            }
            finally
            {
              
                log.Trace("KẾT THÚC THÊM CHI TIẾT THUỐC.......................................................................................");
            }
        }
       

        private void AutoCompleteDmucChung()
        {
            try
            {
                try
                {
                    var lstLoai = new List<string> {"CDDT"};
                    DataTable source = THU_VIEN_CHUNG.LayDulieuDanhmucChung(lstLoai, true);
                    if (source != null)
                    {
                        if (!source.Columns.Contains("ShortCut"))
                        {
                            source.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                        }
                        foreach (DataRow row in source.Rows)
                        {
                            string str = "";
                            string str2 = row["TEN"].ToString().Trim() + " " +
                                          Utility.Bodau(row["TEN"].ToString().Trim());
                            str = row["MA"].ToString().Trim();
                            string[] strArray = str2.ToLower().Split(new[] {' '});
                            string str3 = "";
                            foreach (string str5 in strArray)
                            {
                                if (str5.Trim() != "")
                                {
                                    str3 = str3 + str5 + " ";
                                }
                            }
                            str = str + str3;
                            foreach (string str5 in strArray)
                            {
                                if (str5.Trim() != "")
                                {
                                    str = str + str5.Substring(0, 1);
                                }
                            }
                            row["ShortCut"] = str;
                        }
                        var list = new List<string>();
                        list =
                            source.AsEnumerable()
                                .Where(p => (p.Field<string>("LOAI").ToString() == "CDDT"))
                                .Select(
                                    p =>
                                        ("-1#" + p.Field<string>("MA").ToString() + "@" +
                                         p.Field<string>("TEN").ToString() + "@" +
                                         p.Field<string>("shortcut").ToString()))
                                .ToList<string>();
                        txtCachDung.AutoCompleteList = list;
                        txtCachDung.TextAlign = HorizontalAlignment.Center;
                        txtCachDung.CaseSensitive = false;
                        txtCachDung.MinTypedCharacters = 1;
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                AllowTextChanged = false;
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = chkSaveAndPrint.Checked;
                
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch
            {
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private void LaydanhsachBSKedon()
        {
            try
            {
            //    DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(departmentID, noitru);
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                    new List<string>
                    {
                        DmucNhanvien.Columns.IdNhanvien,
                        DmucNhanvien.Columns.MaNhanvien,
                        DmucNhanvien.Columns.TenNhanvien
                    });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_KEDON_CHONBACSI", "0", false) == "1")
                    txtBacsi.Enabled = true;
                else
                    txtBacsi.Enabled = false;
                txtBacsi.Enabled = globalVariables.IsAdmin;
            }
            catch (Exception)
            {
            }
        }


        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                cboA4.Text = (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) ? "A4" : "A5";
            }
            cboPrintPreview.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;
            cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
            chkCloseAfterSave.Checked = Utility.getUserConfigValue(chkCloseAfterSave.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSave.Checked)) == 1;
            chkSaveAndPrint.Checked = Utility.getUserConfigValue(chkSaveAndPrint.Tag.ToString(), Utility.Bool2byte(chkSaveAndPrint.Checked)) == 1;
            chkHienthithuoctheonhom.Checked = Utility.getUserConfigValue(chkHienthithuoctheonhom.Tag.ToString(), Utility.Bool2byte(chkHienthithuoctheonhom.Checked)) == 1;
            chkAskbeforeDeletedrug.Checked = Utility.getUserConfigValue(chkAskbeforeDeletedrug.Tag.ToString(), Utility.Bool2byte(chkAskbeforeDeletedrug.Checked)) == 1;
            chkNhayTab.Checked = Utility.getUserConfigValue(chkNhayTab.Tag.ToString(), Utility.Bool2byte(chkNhayTab.Checked)) == 1;
            //chkSaveAndPrint.Checked = PropertyLib._MayInProperties.InDonthuocsaukhiluu;//Thay bằng cấu hình theo user bên trên
            cmdPrintPres.Visible = Utility.getUserConfigValue(chkChophepindon.Tag.ToString(), Utility.Bool2byte(chkChophepindon.Checked)) == 1;
            //chkHienthithuoctheonhom.Checked = PropertyLib._ThamKhamProperties.Hienthinhomthuoc;
            globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKho;
            ModifyButton();
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = (cboA4.SelectedIndex == 0) ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

       
        bool autobuild = true;
        private void ChiDanThuoc()
        {
            if (!autobuild) return;
            string containGuide = GetContainGuide();
            txtChiDanDungThuoc.Text = containGuide;
        }

        private void chkAskbeforeDeletedrug_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SaveUserConfig(chkAskbeforeDeletedrug.Tag.ToString(), Utility.Bool2byte(chkAskbeforeDeletedrug.Checked));
            PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc = chkAskbeforeDeletedrug.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
        }

       

        private void chkNgayTaiKham_CheckedChanged(object sender, EventArgs e)
        {
            dtNgayKhamLai.Enabled = chkNgayTaiKham.Checked;
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.SaveUserConfig(chkSaveAndPrint.Tag.ToString(), Utility.Bool2byte(chkSaveAndPrint.Checked));
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + exception.Message);
            }
        }

        private void ClearControl(List<Control> lstNoClear)
        {
            foreach (Control control in grpkedon.Controls)
            {
                if (control is EditBox)
                {
                    if(!lstNoClear.Contains(control))
                    ((EditBox) control).Clear();
                }
                if (control is TextBox)
                {
                    if (!lstNoClear.Contains(control))
                    ((TextBox) control).Clear();
                }
                txtSoluong.Text = "";
                txtDrugID.Clear();
                txtChiDanDungThuoc.Clear();
            }
            txtDrugID.Clear();
            ModifyButton();
        }

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                cmdAddDetail.Enabled = false;
                
                if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện kê đơn thuốc", true);
                    txtBacsi.Focus();
                    return;
                }
                
                AddPreDetail();
                Manual = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                Thread.Sleep(10);
                cmdAddDetail.Enabled = true;
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            new frm_Properties(PropertyLib._ThamKhamProperties).ShowDialog();
            CauHinh();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                KcbDonthuocMuangoai donthuoc = ReadOnlyRecord<KcbDonthuocMuangoai>.FetchByID(Utility.Int32Dbnull(txt_iddonthuoc.Text));
               
                setMsg(lblMsg, "", false);
                Utility.AutoCheckGrid(grdPresDetail);
                if (grdPresDetail.GetCheckedRows().Length <= 0)
                {
                    if (Utility.isValidGrid(grdPresDetail))
                    {
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.IsChecked = true;
                        grdPresDetail.CurrentRow.EndEdit();
                    }
                    if (grdPresDetail.GetCheckedRows().Length <= 0)
                    {
                        setMsg(lblMsg, "Bạn phải chọn " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " để xóa", true);
                        grdPresDetail.Focus();
                        return;
                    }
                }

                int num;
                string s = "";
               
                string _deleteitems = "";
                if (Utility.AcceptQuestion("Bạn Có muốn xóa các " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " đang chọn hay không?", "thông báo xóa", true))
                {
                    foreach (GridEXRow row in grdPresDetail.GetCheckedRows())
                    {

                       long IdChitietdonthuoc = Utility.Int64Dbnull(row.Cells[KcbDonthuocMuangoaiChitiet.Columns.IdChitietdonthuoc].Value, -1);
                        string myGuid = Utility.sDbnull(row.Cells["guid"].Value, "-1");
                        _deleteitems = _deleteitems + Utility.sDbnull(row.Cells["ten_thuoc"].Value, "") + ",";
                        if (IdChitietdonthuoc > 0)
                        {
                            new Delete().From(KcbDonthuocMuangoaiChitiet.Schema).Where(KcbDonthuocMuangoaiChitiet.Columns.IdChitietdonthuoc).IsEqualTo(IdChitietdonthuoc).Execute();
                            this.deletefromDatatable(IdChitietdonthuoc.ToString(), false);
                        }
                        else
                        {
                           
                            this.deletefromDatatable(myGuid);
                        }
                        m_dtDonthuocChitiet.AcceptChanges();
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa thuốc khỏi đơn của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc bị xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, txtPatientName.Text, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    m_dtDonthuocChitiet.AcceptChanges();
                    m_blnCancel = false;
                  
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
              
                if (grdPresDetail.RowCount <= 0)
                {
                    em_Action = action.Insert;
                    txt_iddonthuoc.Text = "-1";
                    donthuoc = null;
                }
            }
        }
       
        private void cmdDonThuocDaKe_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }


        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong_off("KCB_THAMKHAM_TACHDONTHUOC", "0", false) == "1")
                {
                   
                    PrintPres(Utility.Int32Dbnull(txt_iddonthuoc.Text), "");
                }
                else
                {
                   
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                // throw;
            }
            
        }
       
        private void cmdSavePres_Click(object sender, EventArgs e)
        {
            try
            {

                                cmdSavePres.Enabled = false;
                if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện kê đơn thuốc", true);
                    txtBacsi.Focus();
                    return;
                }
                
                if (grdPresDetail.RowCount <= 0)
                {
                    setMsg(lblMsg, "Đơn thuốc chưa có thuốc kê. Mời bạn kiểm tra lại trước khi nhấn Lưu. Nếu không muốn kê đơn, vui lòng bấm nút Thoát", true);
                    txt_tenthuoc.Focus();
                    txt_tenthuoc.SelectAll();
                    return;
                }

                ThemDonthuoc();
                isSaved = true;
                if (chkCloseAfterSave.Checked) this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                cmdSavePres.Enabled = true;
                Manual = false;
                hasChanged = false;
            }
        }

        private void cmdUpdateChiDan_Click(object sender, EventArgs e)
        {
            UpdateChiDanThem();
        }

        private void Create_ChandoanKetluan()
        {
            if (noitru == 0)
            {
               
                _KcbCDKL = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(id_kham).ExecuteSingle<KcbChandoanKetluan>();
                if (_KcbCDKL == null)
                {
                    _KcbCDKL = new KcbChandoanKetluan();
                    _KcbCDKL.IdKham = id_kham;
                    _KcbCDKL.MaLuotkham = objLuotkham.MaLuotkham;
                    _KcbCDKL.IdBenhnhan = objLuotkham.IdBenhnhan;
                    if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                    {
                        _KcbCDKL.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                    }
                    else
                    {
                        _KcbCDKL.IdBacsikham = globalVariables.gv_intIDNhanvien;
                    }
                    _KcbCDKL.NgayTao = dtpCreatedDate.Value;
                    _KcbCDKL.NguoiTao = globalVariables.UserName;

                    _KcbCDKL.IpMaytao = globalVariables.gv_strIPAddress;
                    _KcbCDKL.TenMaytao = globalVariables.gv_strComputerName;
                    _KcbCDKL.NgayChandoan = dtpCreatedDate.Value;
                    //_KcbCDKL.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                    _KcbCDKL.Noitru = (byte)noitru;
                }

                _KcbCDKL.SoNgayhen = (Int16)Utility.DecimaltoDbnull(txtSongaydieutri.Text, 0);
                _KcbCDKL.SongayDieutri = _KcbCDKL.SoNgayhen;
               
                _KcbCDKL.NgaySua = dtpCreatedDate.Value;
                _KcbCDKL.NguoiSua = globalVariables.UserName;

                _KcbCDKL.IpMaysua = globalVariables.gv_strIPAddress;
                _KcbCDKL.TenMaysua = globalVariables.gv_strComputerName;
            }
        }

        private KcbDonthuocMuangoaiChitiet[] CreateArrayPresDetail()
        {
            _temp = ActionResult.Success;
            int index = 0;
            var chitietArray = new KcbDonthuocMuangoaiChitiet[m_dtDonthuocChitiet.DefaultView.Count];
          
            return chitietArray;
        }

        private KcbDonthuocMuangoai TaoDonthuoc()
        {

            KcbDonthuocMuangoai donthuoc = new KcbDonthuocMuangoai();


            donthuoc.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
            donthuoc.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);

            donthuoc.LoidanBacsi = Utility.sDbnull(txtLoiDanBS.Text);

           
            if (objCongkham != null)
                donthuoc.IdCongkham = objCongkham.IdKham;
            if (em_Action == action.Update)
            {
                donthuoc.IdDonthuoc = Utility.Int32Dbnull(txt_iddonthuoc.Text, -1);
                donthuoc.NguoiSua = globalVariables.UserName;
                donthuoc.NgaySua = globalVariables.SysDate;
                donthuoc.IsNew = false;
                donthuoc.MarkOld();
            }
            else
            {
                donthuoc.NguoiTao = globalVariables.UserName;
                donthuoc.NgayTao = globalVariables.SysDate;
            }

            return donthuoc;
        }
       
         private void deletefromDatatable(string id_chitietdonthuoc, bool deletebyGuid=true)
        {
           
            try
            {
                
                DataRow[] rowArray = deletebyGuid? m_dtDonthuocChitiet.Select("guid='" + id_chitietdonthuoc + "'") : m_dtDonthuocChitiet.Select("id_chitietdonthuoc=" + id_chitietdonthuoc);
                //foreach (DataRow r in rowArray)
                //    r.Delete();
                for (int i = 0; i <= (rowArray.Length - 1); i++)
                {
                    m_dtDonthuocChitiet.Rows.Remove(rowArray[i]);
                }
                m_dtDonthuocChitiet.AcceptChanges();
                //Delete from bảng tạm kê
                //new Delete().From(TTamke.Schema)
                //    .Where(TTamke.Columns.GuidKey
                //grdPresDetail.Refetch();
            }
            catch
            {
            }
        }
        private void deletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            Func<DataRow, bool> predicate = null;
            try
            {
                if (predicate == null)
                {
                    predicate =
                        q =>
                            lstIdChitietDonthuoc.Contains(
                                Utility.Int32Dbnull(q[KcbDonthuocMuangoaiChitiet.Columns.IdChitietdonthuoc]));
                }
                DataRow[] rowArray =
                    m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(predicate).ToArray<DataRow>();
                for (int i = 0; i <= (rowArray.Length - 1); i++)
                {
                    m_dtDonthuocChitiet.Rows.Remove(rowArray[i]);
                }
                m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }

        private void deletefromDatatable(List<int> lstDeleteId, int lastdetailid, decimal soluong)
        {
            Func<DataRow, bool> predicate = null;
            Func<DataRow, bool> func2 = null;
            try
            {
                int num;
                if (predicate == null)
                {
                    predicate =
                        q => Utility.Int32Dbnull(q[KcbDonthuocMuangoaiChitiet.Columns.IdChitietdonthuoc]) == lastdetailid;
                }
                DataRow[] rowArray =
                    m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(predicate).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    if (soluong <= 0)
                    {
                        m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                    }
                    else
                    {
                        rowArray[num][KcbDonthuocMuangoaiChitiet.Columns.SoLuong] = soluong;
                    }
                }
                if (func2 == null)
                {
                    func2 =
                        q =>
                            lstDeleteId.Contains(Utility.Int32Dbnull(q[KcbDonthuocMuangoaiChitiet.Columns.IdChitietdonthuoc], 0));
                }
                rowArray = m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(func2).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                }
                m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }


        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                Selected = false;
                string filterExpression = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    filterExpression = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" +
                                       tEditBox.Text + "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    filterExpression = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] source = DtIcd.Select(filterExpression);
                if (source.Length == 1)
                {
                    if (CP == 0)
                    {
                        txtMaBenhChinh.Text = "";
                        txtMaBenhChinh.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                        txtMaBenhChinh.Focus();
                    }
                    else if (CP == 1)
                    {
                        txtMaBenhphu.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                        txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                        Selected = false;
                    }
                }
                else if (source.Length > 1)
                {
                    var h_icd = new frm_DanhSach_ICD(CP)
                    {
                        dt_ICD = source.CopyToDataTable()
                    };
                    h_icd.ShowDialog();
                    if (!h_icd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = h_icd.lstSelectedRows;
                        if (CP == 0)
                        {
                            isLike = false;
                            txtMaBenhChinh.Text = "";
                            txtMaBenhChinh.Text =
                                Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            hasMorethanOne = false;
                            txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                            txtMaBenhChinh_KeyDown(txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                            Selected = false;
                        }
                        else if (CP == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                isLike = false;
                                txtMaBenhphu.Text = "";
                                txtMaBenhphu.Text =
                                    Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                hasMorethanOne = false;
                                txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                Selected = false;
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    isLike = false;
                                    txtMaBenhphu.Text = "";
                                    txtMaBenhphu.Text = Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    hasMorethanOne = false;
                                    txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                    txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                    Selected = false;
                                }
                                hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                isLike = true;
            }
        }
        string AppPath = Application.StartupPath;
        
       
        private void frm_KCB_KE_DONTHUOC_MUANGOAI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               
                if (!isSaved)
                {
                    bool AutoSave = THU_VIEN_CHUNG.Laygiatrithamsohethong("KEDONTHUOC_TUDONGLUU", "0", true) == "1";
                    long id_donthuoc = Utility.Int64Dbnull(txt_iddonthuoc.Text, -1);
                    KcbDonthuocMuangoai donthuoc = KcbDonthuocMuangoai.FetchByID(id_donthuoc);

                    var q= grdPresDetail.GetDataRows().Where(c => Utility.Int64Dbnull(c.Cells["id_chitietdonthuoc"].Value) <= 0).FirstOrDefault();
                    if (q!=null)
                    {
                        if (!AutoSave)//Hỏi nếu không phải chế độ Autosave
                        {
                            if (!Utility.AcceptQuestion(
                                    "Bạn đã thay đổi đơn " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) +
                                    " nhưng chưa lưu lại. Bạn Có muốn lưu đơn " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) +
                                    " trước khi thoát hay không?\nNhấn Yes để lưu đơn " +
                                    THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + ".\nNhấn No để không lưu đơn " +
                                    THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT), "Cảnh báo", true))
                            {
                                return;
                            }
                        }
                        cmdSavePres_Click(cmdSavePres, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
            finally
            {
              
            }
           
        }

        private void frm_KCB_KE_DONTHUOC_MUANGOAI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F11)
            {
                Utility.ShowMsg(base.ActiveControl.Name);
            }
            else if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode == Keys.P))
            {
                cmdPrintPres_Click(cmdPrintPres, new EventArgs());
            }
            else if (e.KeyCode == Keys.F2)
            {
             
            }
            else if ((e.KeyCode == Keys.A) && e.Control)
            {
                cmdAddDetail_Click(cmdAddDetail, new EventArgs());
            }
            else if (e.KeyCode == Keys.S && e.Control)
            {
                cmdSavePres_Click(cmdSavePres, new EventArgs());
            }
            else if (e.KeyCode == Keys.F3)
            {
              
            }
            else if ((e.Shift || e.Alt) && (e.KeyCode == Keys.S))
            {
                cmdLuuchidan.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Control activeCtrl=Utility.getActiveControl(this);
                    if ((activeCtrl != null && (activeCtrl.Name == txtChiDanDungThuoc.Name || activeCtrl.Name == txtLoiDanBS.Name)))
                        return;

                    //if (uiTabPage1.ActiveControl != null && uiTabPage1.ActiveControl.Name == splitContainer2.Name)
                    //    return;
                    //if (uiTabPage1.ActiveControl != null && uiTabPage1.ActiveControl.Name == splitContainer4.Name &&
                    //    (Utility.DecimaltoDbnull(txtSoluong.Text, 0) > 0))
                    if(activeCtrl.Name==txtSoluong.Name && Utility.DecimaltoDbnull(txtSoluong.Text, 0) > 0)
                    {
                        if (!_autoFill)
                        {
                            
                                cmdAddDetail_Click(cmdAddDetail, new EventArgs());
                        }
                        else
                        {
                            cmdAddDetail_Click(cmdAddDetail, new EventArgs());
                        }
                    }
                    else if (activeCtrl.Name == txtLoiDanBS.Name)
                    {
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.F5)
                {
                   
                }
                if (e.KeyCode == Keys.Escape)
                {
                    cmdExit_Click(cmdExit, new EventArgs());
                }
            }
        }
      
       
        KcbChandoanKetluan cdngoaitru = null;
        int Slton_mausac = 50;
        private void frm_KCB_KE_DONTHUOC_MUANGOAI_Load(object sender, EventArgs e)
        {
            try
            {
                string loidanbacsi = txtLoiDanBS.Text;//Để lát set lại
                               
                pnlChandoanNgoaitru.Visible = objLuotkham.TrangthaiNoitru <= 0;
               
               
                txtCachDung.LOAI_DANHMUC = KIEU_THUOC_VT == "THUOC" ? "CDDT" : "CHIDAN_KEVATTU";
                AutoloadSaveAndPrintConfig();
                LaydanhsachBSKedon();
                //LaydanhsachKhotheoBs();
                LaydanhsachMayin();
                txtCachDung.Init();
                txtLoiDanBS.Init();
                txtDonvitinh.Init();
                txtLoiDanBS._Text = loidanbacsi;
                if (grdPresDetail.DropDowns.Contains("cbo_dvt"))
                {
                    grdPresDetail.DropDowns["cbo_dvt"].DataSource = txtDonvitinh.dtData;
                }
                FillThongtinLuotkham();
                GetDataPresDetail();
                if (IdBacsikham > 0) txtBacsi.SetId(IdBacsikham);
                LoadBenh();
              
                mnuSaveAsDinhmucVTTH.Enabled = mnuChuyenDinhmucVTTHvaodon.Enabled = id_chitietchidinh > 0;
                mnuThuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
              
                MaDoiTuong = objLuotkham.MaDoituongKcb;
                SqlQuery sqlkt = null;
                
                cdngoaitru =
                       new Select().From(KcbChandoanKetluan.Schema)
                           .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                           .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                           .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                           .ExecuteSingle<KcbChandoanKetluan>();
                //REM đoạn if else vì ko cần thiết
                if (objCongkham!=null && noitru==0)//Kê đơn thuốc ngoại trú
                {
                    sqlkt = new Select().From(KcbChandoanKetluan.Schema)
                           .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                }
                else// Kê đơn thuốc nội trú
                {
                    if (objPhieudieutriNoitru != null)//Cần xem lại đoạn này xem tại sao phiếu điều trị đi kèm kcb chẩn đoán kết luận?
                        sqlkt =
                            new Select().From(KcbChandoanKetluan.Schema)
                                .Where(KcbChandoanKetluan.Columns.IdPhieudieutri)
                                .IsEqualTo(objPhieudieutriNoitru.IdPhieudieutri);
                }

                if (_KcbCDKL == null || sqlkt == null || sqlkt.GetRecordCount() <= 0)
                {
                    _KcbCDKL = new KcbChandoanKetluan();
                    _KcbCDKL.IsNew = true;
                }
                else
                {
                    _KcbCDKL.IsNew = false;
                    _KcbCDKL.MarkOld();
                }
                _KcbCDKL.Noitru = noitru;
                txtTiensudiung.Text = cdngoaitru != null ? cdngoaitru.NhanXet : _KcbCDKL.NhanXet;
              
                isLoaded = true;
                AllowTextChanged = true;
                blnHasLoaded = true;
                
                if (objLuotkham != null && Utility.sDbnull(objLuotkham.MabenhChinh, "").Length <= 0 || (objLuotkham != null && Utility.sDbnull(objLuotkham.MabenhChinh, "").Length > 0 && _KcbCDKL != null && Utility.sDbnull(_KcbCDKL.MabenhChinh, "").Length > 0))//Chỉ kiểm tra khi đã có mã bệnh chính
                {
                    //OK
                }
                else
                {
                    txtMaBenhChinh.Enabled= txtTenBenhChinh.Enabled=cmdSearchBenhChinh.Enabled= false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                //dtGiathuocQhe = new Select().From(QheDoituongThuoc.Schema).ExecuteDataSet().Tables[0];
                SetTabStop();
                if (_ngayhenkhamlai!=null && _ngayhenkhamlai != "")
                {
                    chkNgayTaiKham.Checked = true;
                    dtNgayKhamLai.Text = _ngayhenkhamlai;
                }
                txt_tenthuoc.Focus();
            }
        }

       
      

        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> paramValue = ICD_chinh.Split(new[] {','}).ToList();
                DmucBenhCollection benhs =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, paramValue));
                foreach (DmucBenh benh in benhs)
                {
                    ICD_Name = ICD_Name + benh.TenBenh + ";";
                    ICD_Code = ICD_Code + benh.MaBenh + ";";
                }
                paramValue = IDC_Phu.Split(new[] {','}).ToList();
                benhs =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, paramValue));
                foreach (DmucBenh benh in benhs)
                {
                    ICD_Name = ICD_Name + benh.TenBenh + ";";
                    ICD_Code = ICD_Code + benh.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "")
                {
                    ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                }
                if (ICD_Code.Trim() != "")
                {
                    ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
                }
            }
            catch
            {
            }
        }

      
        /// <summary>
        /// Sáng trưa chiều tối
        /// </summary>
        /// <returns></returns>
        private string GetContainGuide()
        {
            try
            {
                string yourString = "";
                //   yourString = yourString + this.txtCachDung.Text + " ";
                if (Utility.sDbnull(txtsang.Text,0)!="")
                {
                    yourString = "Sáng " + txtsang.Text.Trim() + " " + txtDonvitinh.Text;
                }
                if (Utility.sDbnull(txttrua.Text,0)!="")
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", Trưa " + txttrua.Text.Trim() + " " + txtDonvitinh.Text;
                    else
                        yourString += "Trưa " + txttrua.Text.Trim() + " " + txtDonvitinh.Text;
                }
                if (Utility.sDbnull(txtchieu.Text,0)!="")
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", Chiều " + txtchieu.Text.Trim() + " " + txtDonvitinh.Text;
                    else
                        yourString += "Chiều " + txtchieu.Text.Trim() + " " + txtDonvitinh.Text;
                }
                if (Utility.sDbnull(txttoi.Text,0)!="")
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", Tối " + txtsang.Text.Trim() + " " + txtDonvitinh.Text;
                    else yourString += "Tối " + txtsang.Text.Trim() + " " + txtDonvitinh.Text;
                }
                if (!string.IsNullOrEmpty(txtCachDung.Text))
                {
                    if (!string.IsNullOrEmpty(yourString))
                        yourString += ", " + txtCachDung.Text.Trim() + " ";// + txtDonViTinh.Text;
                    else yourString += txtCachDung.Text.Trim() + " ";// + txtDonViTinh.Text;
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
       
        private string GetDanhsachBenhphu(bool isAll)
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                if (dt_ICD_PHU.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (isAll || (objkcbdangky!=null && Utility.Int64Dbnull(row[KcbDangkyKcb.Columns.IdKham], -1) == objkcbdangky.IdKham))//Chỉ lấy mã bệnh phụ theo công khám
                        {
                            if (recordRow > 0)
                                sMaICDPHU.Append(",");
                            sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                            recordRow++;
                        }
                    }
                }
                return sMaICDPHU.ToString();
            }
            catch
            {
                return "";
            }
        }
        private string GetDanhsachBenhphu()
        {
            var builder = new StringBuilder("");
            try
            {
                int num = 0;
                if (DtIcd.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (num > 0)
                        {
                            builder.Append(",");
                        }
                        builder.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                        num++;
                    }
                }
                return builder.ToString();
            }
            catch
            {
                return "";
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        private void FillThongtinLuotkham()
        {
            if (objLuotkham != null)
            {
                txtSoBHYT.Text = Utility.sDbnull(objLuotkham.MatheBhyt);
                txtPtramBHYT.Text = (objLuotkham.TrangthaiNoitru <= 0
                    ? Utility.sDbnull(objLuotkham.PtramBhyt, "0")
                    : Utility.sDbnull(objLuotkham.PtramBhytGoc, "0")) + " %";
                txtptramdauthe.Text = Utility.sDbnull(objLuotkham.PtramBhytGoc, "0") + " %";
                txtAddress.Text = Utility.sDbnull(objLuotkham.DiaChi);
              
               objDoituongKCB=DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
                if (objDoituongKCB != null)
                {
                    Giathuoc_quanhe = Utility.ByteDbnull(objDoituongKCB.GiathuocQuanhe, 0) == 1;
                    txtObjectName.Text = Utility.sDbnull(objDoituongKCB.TenDoituongKcb);
                   
                    mnuThuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objDoituongKCB.IdLoaidoituongKcb);
                }
                
            }
        }
        KcbDonthuocMuangoai donthuoc = null;
        private void GetDataPresDetail()
        {
             
             donthuoc = ReadOnlyRecord<KcbDonthuocMuangoai>.FetchByID(Utility.Int32Dbnull(txt_iddonthuoc.Text));
            if (donthuoc != null)
            {
                IdDonthuoc = Utility.Int32Dbnull(donthuoc.IdDonthuoc);
                if (donthuoc.LoidanBacsi != "")//Để load lời dặn bác sĩ trong tình huống tạo đơn thuốc-->lưu lời dặn giấy ra viện-->sửa đơn thuốc(lời dặn đơn thuốc bị trống sẽ lấy lời dặn bên ngoài)
                    txtLoiDanBS._Text = Utility.sDbnull(donthuoc.LoidanBacsi);
            }
            else
            {
                IdDonthuoc = -1;
                txt_iddonthuoc.Text = "-1";
                if (objPhieudieutriNoitru != null)
                    dtpCreatedDate.Value = objPhieudieutriNoitru.NgayDieutri.Value.AddHours(Utility.Int32Dbnull(objPhieudieutriNoitru.GioDieutri.Split(':')[0], 0)).AddMinutes(Utility.Int32Dbnull(objPhieudieutriNoitru.GioDieutri.Split(':')[1], 0)) ;
                else
                    dtpCreatedDate.Value = globalVariables.SysDate;
            }
            m_dtDonthuocChitiet = SPs.DonthuocMuangoaiLaythongtinDexem(IdDonthuoc).GetDataSet().Tables[0];
            //CreateViewTable();//Bỏ do hiển thị theo id_thuốc kho
            if (!m_dtDonthuocChitiet.Columns.Contains("CHON"))
            {
                m_dtDonthuocChitiet.Columns.Add("CHON", typeof(int));
            }
            if (!m_dtDonthuocChitiet.Columns.Contains("guid"))
            {
                m_dtDonthuocChitiet.Columns.Add("guid", typeof(string));
            }
            if (!m_dtDonthuocChitiet.Columns.Contains("sl_org"))
            {
                m_dtDonthuocChitiet.Columns.Add("sl_org", typeof(decimal));
            }
            if (!m_dtDonthuocChitiet.Columns.Contains("id_group"))
            {
                m_dtDonthuocChitiet.Columns.Add("id_group", typeof(string));
            }
           
                Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDonthuocChitiet, false, true, "1=1","");
           
        }


        private string GetTenBenh(string MaBenh)
        {
            string str = "";
            DataRow[] rowArray =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
            if (rowArray.GetLength(0) > 0)
            {
                str = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
            }
            return str;
        }

        private string GetUnitName(string ma)
        {
            try
            {
                DmucChung chung = THU_VIEN_CHUNG.LaydoituongDmucChung("DONVITINH", ma);
                if (chung != null)
                {
                    return chung.Ten;
                }
                return "";
            }
            catch (Exception)
            {
                return "Lượt";
            }
        }

        private void grdPresDetail_CellEdited(object sender, ColumnActionEventArgs e)
        {
           // CreateViewTable();
        }

        private void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }

        private void grdPresDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                mnuDelele_Click(mnuDelele, new EventArgs());
            }
        }

        private void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyButton();
        }
       
        private void InitEvents()
        {
            base.Load += frm_KCB_KE_DONTHUOC_MUANGOAI_Load;
            base.KeyDown += frm_KCB_KE_DONTHUOC_MUANGOAI_KeyDown;
            base.FormClosing += frm_KCB_KE_DONTHUOC_MUANGOAI_FormClosing;
            grdPresDetail.KeyDown += grdPresDetail_KeyDown;
          
            grdPresDetail.CellEdited += grdPresDetail_CellEdited;
            grdPresDetail.CellUpdated += grdPresDetail_CellUpdated;
            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;
          
            txtSoluong.TextChanged += txtSoluong_TextChanged;
         
            txtCachDung._OnSelectionChanged += txtCachDung__OnSelectionChanged;
            txtCachDung.TextChanged += txtCachDung_TextChanged;
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkNgayTaiKham.CheckedChanged += chkNgayTaiKham_CheckedChanged;
            mnuDelele.Click += mnuDelele_Click;
            cmdSavePres.Click += cmdSavePres_Click;
            cmdExit.Click += cmdExit_Click;
            cmdDelete.Click += cmdDelete_Click;
            cmdDonThuocDaKe.Click += cmdDonThuocDaKe_Click;
            cmdPrintPres.Click += cmdPrintPres_Click;
            cmdAddDetail.Click += cmdAddDetail_Click;
            cmdCauHinh.Click += cmdCauHinh_Click;
           
            cboPrintPreview.SelectedIndexChanged += cboPrintPreview_SelectedIndexChanged;
            cboA4.SelectedIndexChanged += cboA4_SelectedIndexChanged;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
          
            chkAskbeforeDeletedrug.CheckedChanged += chkAskbeforeDeletedrug_CheckedChanged;
            txtMaBenhChinh.KeyDown += txtMaBenhChinh_KeyDown;
            txtMaBenhChinh.TextChanged += txtMaBenhChinh_TextChanged;
            txtMaBenhphu.GotFocus += txtMaBenhphu_GotFocus;
            txtMaBenhphu.KeyDown += txtMaBenhphu_KeyDown;
            txtMaBenhphu.TextChanged += txtMaBenhphu_TextChanged;
            mnuThuoctutuc.Click += mnuThuoctutuc_Click;
            txtCachDung._OnShowData += txtCachDung__OnShowData;
            txtCachDung._OnSaveAs += txtCachDung__OnSaveAs;
           
          
           
            chkCloseAfterSave.CheckedChanged += chkCloseAfterSave_CheckedChanged;
           
            txtSongaydieutri.LostFocus += txtSongaydieutri_LostFocus;
            txtLoiDanBS._OnShowDataV1 += _OnShowDataV1;
            txtLoiDanBS.LostFocus += txtLoiDanBS_LostFocus;
           
            txtsang.TextChanged += _Chidandungthuoc;
            txttrua.TextChanged += _Chidandungthuoc;
            txtchieu.TextChanged += _Chidandungthuoc;
            txttoi.TextChanged += _Chidandungthuoc;
           
            mnuSaveAsDinhmucVTTH.Click += mnuSaveAsDinhmucVTTH_Click;
        }

        void mnuSaveAsDinhmucVTTH_Click(object sender, EventArgs e)
        {
            try
            {
                List<TDinhmucVtth> lstDinhmucVTTH = new List<TDinhmucVtth>();
                foreach (GridEXRow _row in grdPresDetail.GetCheckedRows())
                {
                    TDinhmucVtth newItem = new TDinhmucVtth();
                    newItem = new TDinhmucVtth();
                    newItem.IsNew = true;
                    newItem.IdChitietdichvu = id_chitietdichvu;
                    newItem.IdThuoc = Utility.Int32Dbnull(_row.Cells[TDinhmucVtth.Columns.IdThuoc].Value, -1);
                    newItem.SoLuong = Utility.Int32Dbnull(_row.Cells[TDinhmucVtth.Columns.SoLuong].Value, 1);
                    newItem.NguoiTao = globalVariables.UserName;
                    newItem.NgayTao = globalVariables.SysDate;
                    lstDinhmucVTTH.Add(newItem);
                }
                if (new KCB_KEDONTHUOC().ThemDinhmucVTTH(lstDinhmucVTTH) == ActionResult.Success)
                    Utility.ShowMsg(string.Format("Tạo định mức VTTH cho dịch vụ {0} thành công", ten_dichvu));

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       
       
        void _Chidandungthuoc(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

       
        void txtLoiDanBS_LostFocus(object sender, EventArgs e)
        {
            txtLoiDanBS._Text = Utility.DoTrim(txtLoiDanBS.Text);  
        }

        void _OnShowDataV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        void txtSongaydieutri_LostFocus(object sender, EventArgs e)
        {
            dtNgayKhamLai.Value = dtpCreatedDate.Value.AddDays(Utility.Int32Dbnull(txtSongaydieutri.Text, 0));
        }

       
        void chkCloseAfterSave_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SaveUserConfig(chkCloseAfterSave.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSave.Checked));
        }




        private void ThemDonthuoc()
        {
            try
            {

                KcbDonthuocMuangoai donthuoc = TaoDonthuoc();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                    {
                        donthuoc.Save();
                        foreach (GridEXRow row in grdPresDetail.GetRows())
                        {
                            KcbDonthuocMuangoaiChitiet chitiet = KcbDonthuocMuangoaiChitiet.FetchByID(Utility.Int64Dbnull(row.Cells["id_chitietdonthuoc"].Value));
                            if (chitiet != null)
                            {
                                chitiet.MarkOld();
                                chitiet.IsNew = false;
                            }
                            else
                            {
                                chitiet = new KcbDonthuocMuangoaiChitiet();
                            }    
                            chitiet.IdDonthuoc = donthuoc.IdDonthuoc;
                            chitiet.TenThuoc = Utility.sDbnull(row.Cells["ten_thuoc"].Value);
                            chitiet.TenDonvitinh = Utility.sDbnull(row.Cells["ten_donvitinh"].Value);
                            chitiet.SoLuong = Utility.Int32Dbnull(row.Cells["so_luong"].Value);
                            chitiet.Sang = Utility.sDbnull(row.Cells["sang"].Value);
                            chitiet.Trua = Utility.sDbnull(row.Cells["trua"].Value);
                            chitiet.Chieu = Utility.sDbnull(row.Cells["chieu"].Value);
                            chitiet.Toi = Utility.sDbnull(row.Cells["toi"].Value);
                            chitiet.CachDung = Utility.sDbnull(row.Cells["cach_dung"].Value);
                            chitiet.Save();
                        }
                    }
                    scope.Complete();
                }
                _actionResult = ActionResult.Success;
                switch (_actionResult)
                {
                    case ActionResult.Error:
                        setMsg(lblMsg,
                            "Lỗi trong quá trình lưu đơn " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT), true);
                        break;

                    case ActionResult.Success:
                        txt_iddonthuoc.Text = donthuoc.IdDonthuoc.ToString();
                        em_Action = action.Update;
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới đơn thuốc cho người bệnh id={0}, mã lượt khám ={1}, id phiếu điều trị ={2}, id đơn thuốc={3} thành công", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, objPhieudieutriNoitru != null ? objPhieudieutriNoitru.IdPhieudieutri : -1, IdDonthuoc), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        setMsg(lblMsg, "Bạn thực hiện lưu đơn " + THU_VIEN_CHUNG.laytenthuoc_vattu(KIEU_THUOC_VT) + " thành công", false);
                        m_blnCancel = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                if (Manual)
                {
                    em_Action = action.Update;
                }
            }
        }

        
        private void LoadBenh()
        {
            try
            {
                AllowTextChanged = true;
                isLike = false;
                txtChanDoan._Text = Utility.sDbnull(_Chandoan);
                txtMaBenhChinh.Text = Utility.sDbnull(_MabenhChinh);
                isLike = true;
                AllowTextChanged = false;
                grd_ICD.DataSource = dt_ICD_PHU;
            }
            catch
            {
            }
        }

        private void LaydanhsachMayin()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        string item = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(item);
                    }
                }
                catch
                {
                }
                finally
                {
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
            }
        }

        private void mnuDelele_Click(object sender, EventArgs e)
        {
            cmdDelete.PerformClick();
            return;//Tạm khóa bên dưới để dùng thống nhất 1 hàm 20231007
            
        }

        private void mnuThuoctutuc_Click(object sender, EventArgs e)
        {
           
        }

        private void ModifyButton()
        {
            try
            {
                
                cmdSavePres.Enabled = grdPresDetail.RowCount > 0;
                cmdPrintPres.Enabled = grdPresDetail.RowCount > 0 && !m_dtDonthuocChitiet.AsEnumerable().Any(c=>c.Field<long>(KcbDonthuocMuangoaiChitiet.Columns.IdChitietdonthuoc)==-1);//Chỉ cho in đơn khi đã lưu
                cmdDelete.Enabled = Utility.isValidGrid(grdPresDetail) || grdPresDetail.GetCheckedRows().Count()>0;
              
                mnuThuoctutuc.Enabled = Utility.isValidGrid(grdPresDetail);
            }
            catch (Exception)
            {
            }
        }

       
      

        private void PerformAction()
        {

           
            //Kiểm tra nếu bấm sửa mà đơn không tồn tại thì quay lại chế độ insert
            if (em_Action == action.Update)
            {
                //Kiểm tra xem đã bị tổng hợp đơn thuốc chưa
                KcbDonthuocMuangoai objdt = KcbDonthuocMuangoai.FetchByID(IdDonthuoc);

                if (objdt == null)
                    em_Action = action.Insert;
            }
            ThemDonthuoc();
        }
        private void PrintPres(int presID, string forcedTitle)
        {
           
        }
        private void PrintTuvanthem(int presID, string forcedTitle, DataTable p_dtData)
        {

          
        }
        private void PrintPres(int PresID)
        {
          
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + exception.Message);
            }
        }

       

        private void setMsg(Label item, string msg, bool isError)
        {
            try
            {
                item.Text = msg;
                if (isError)
                {
                    item.ForeColor = Color.Red;
                }
                else
                {
                    item.ForeColor = Color.DarkBlue;
                }
                Application.DoEvents();
            }
            catch
            {
            }
        }


        private void txtCachDung__OnSaveAs()
        {
        }

        private void txtCachDung__OnSelectionChanged()
        {
            ChiDanThuoc();
        }

        private void txtCachDung__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtCachDung.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtCachDung.myCode;
                txtCachDung.Init();
                txtCachDung.SetCode(oldCode);
                txtCachDung.Focus();
            }
        }

        private void txtCachDung_TextChanged(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

       
        private void AutoFill_Chidandungthuoc()
        {
            try
            {
                _autoFill = false;
                var objChidan =
                    new Select().From(DmucChidanKedonthuoc.Schema)
                        .Where(DmucChidanKedonthuoc.Columns.IdThuoc)
                        .IsEqualTo(txtDrugID.Text)
                        .And(DmucChidanKedonthuoc.Columns.IdBacsi)
                        .IsEqualTo(globalVariables.gv_intIDNhanvien)
                        .ExecuteSingle<DmucChidanKedonthuoc>();
                if (objChidan != null)
                {
                    _autoFill = true;
                  
                    txtChiDanDungThuoc.Text = objChidan.ChidanThem;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
      
        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && hasMorethanOne)
            {
                DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                hasMorethanOne = false;
            }
        }

        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DataRow[] rowArray;
                    hasMorethanOne = true;
                    if (isLike)
                    {
                        rowArray =
                            globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                                 Utility.sDbnull(txtMaBenhChinh.Text, "") + "%'");
                    }
                    else
                    {
                        rowArray =
                            globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                                 Utility.sDbnull(txtMaBenhChinh.Text, "") + "'");
                    }
                    if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                    {
                        if (rowArray.GetLength(0) == 1)
                        {
                            hasMorethanOne = false;
                            txtMaBenhChinh.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                            txtTenBenhChinh.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                        }
                        else
                        {
                            txtTenBenhChinh.Text = "";
                        }
                    }
                    else
                    {
                        txtTenBenhChinh.Text = "";
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void txtMaBenhphu_GotFocus(object sender, EventArgs e)
        {
            txtMaBenhphu_TextChanged(txtMaBenhphu, e);
        }

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (hasMorethanOne)
                    {
                        DSACH_ICD(txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        AddBenhphu();
                        txtMaBenhphu.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
            DataRow[] rowArray;
            hasMorethanOne = true;
            if (isLike)
            {
                rowArray =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") + "%'");
            }
            else
            {
                rowArray =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") + "'");
            }
            if (!string.IsNullOrEmpty(txtMaBenhphu.Text))
            {
                if (rowArray.GetLength(0) == 1)
                {
                    hasMorethanOne = false;
                    txtMaBenhphu.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                    txtTenBenhPhu.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                    TEN_BENHPHU = txtTenBenhPhu.Text;
                }
                else
                {
                    txtTenBenhPhu.Text = "";
                    TEN_BENHPHU = "";
                }
            }
            else
            {
                txtMaBenhphu.Text = "";
                TEN_BENHPHU = "";
            }
        }

      
        private void txtSoluong_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Utility.DecimaltoDbnull(txtSoluong.Text, 0) > 0) && (e.KeyCode == Keys.Enter))
            {
                if (!_autoFill)
                {
                   
                }
                else
                {
                    cmdAddDetail_Click(cmdAddDetail, new EventArgs());
                }
            }
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception)
            {
            }
        }

        private void txtSolan_TextChanged(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

        private void UpdateChiDanThem()
        {
           
        }

       
       
        private void txtPatientCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPatientID_TextChanged(object sender, EventArgs e)
        {
            if (Utility.Int64Dbnull(txtPatientID.Text, -1) > 0)
            {
                DataTable dtListDonThuoc =
                    SPs.KcbLaydanhsachDonthuocOld(Utility.Int64Dbnull(txtPatientID.Text, -1),KIEU_THUOC_VT).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdListDonThuocCu,dtListDonThuoc,false,true,"","");
            }
        }

        private void grdListDonThuocCu_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
           
        }

      
        private void cboKieuKedonthuocVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AllowTextChanged || !blnHasLoaded) return;
                      SetTabStop();
        }
        void SetTabStop()
        {

            txtCachDung.TabStop = true;
            txtsang.TabStop = txttrua.TabStop = txtchieu.TabStop = txttoi.TabStop = true;
            if (chkNhayTab.Checked)
            {
                txtsang.TabStop = txttrua.TabStop = txtchieu.TabStop = txttoi.TabStop = txtCachDung.TabStop = false;
            }
        }
      
        private void chkChophepindon_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SaveUserConfig(chkHienthithuoctheonhom.Tag.ToString(), Utility.Bool2byte(chkHienthithuoctheonhom.Checked));
        }
       
        private void chkNhayTab_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SaveUserConfig(chkNhayTab.Tag.ToString(), Utility.Bool2byte(chkNhayTab.Checked));
            txtsang.TabStop = txttrua.TabStop = txtchieu.TabStop = txttoi.TabStop = txtCachDung.TabStop = !chkNhayTab.Checked;
        }

        private void cmdXemchidandungthuoccanhan_Click(object sender, EventArgs e)
        {
            frm_chidandungthuoc_bacsi _chidandungthuoc_bacsi = new frm_chidandungthuoc_bacsi();
            _chidandungthuoc_bacsi.ShowDialog();
        }

    }
}