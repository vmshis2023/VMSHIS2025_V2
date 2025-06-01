using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;

using VMS.QMS.Class;
using VNS.Properties;

namespace VMS.QMS
{
    public partial class FrmShowListData : Form
    {
        public bool UsingWS = false;
        int QMSType = 0;//0= PK,1=Phòng chức năng
        public FrmShowListData(int QMSType)
        {
            InitializeComponent();
            this.QMSType = QMSType;
            this.FormClosing += FrmShowListData_FormClosing;
            grdDathuchien.ColumnButtonClick += grdDathuchien_ColumnButtonClick;
           // globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Config\";
            //if (new ConnectionSql().ReadConfig())
            //    Utility.InitSubSonic(new ConnectionSql().KhoiTaoKetNoi(), "ORM");
            QmsPrintProperties = PropertyLib.GetQMSPrintProperties();
        }

        void FrmShowListData_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            timer1.Dispose();
            timer1 = null;
        }

        void grdDathuchien_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            Reload = true;
            if (e.Column.Key == "cmdGoilai")
            {
                
                long regId = Utility.Int64Dbnull(grdDathuchien.CurrentRow.Cells["Id"].Value, -1);
                //string PatientCode = Utility.sDbnull(grdDathuchien.CurrentRow.Cells["ma_luotkham"].Value, "");
                if (regId > 0)
                {
                    _qms.QmsPK_CapnhatTrangthai(-1, Utility.Int64Dbnull(grdDathuchien.CurrentRow.Cells["id"].Value, -1), 1, QMSType);
                    //_qms.UpdateStatusQms(PatientCode, QmsPrintProperties.MaPhongQMS, DateTime.Now, 1);
                }
                
                LoadDanhSach(QmsPrintProperties.MaPhongQMS, DateTime.Now, 100, QmsPrintProperties.MaKhoaQMS);
            }
        }
        public QMSPrintProperties QmsPrintProperties;
        private DataTable _mDtDanhSachChoKham = new DataTable();
        DataTable _dtDanhsach = new DataTable();
        QMSChucNang _qms = new QMSChucNang();
        bool Reload = false;
        private void LoadDanhSach(string maphong, DateTime ngaytao, int trangthai, string maKhoa)
        {
            try
            {
                if (QMSType == 0)
                    _dtDanhsach = _qms.QmsPK_GetData(maphong, ngaytao, trangthai, maKhoa);
                else
                    _dtDanhsach = _qms.GetListQmSbyMaPhong(maphong, QmsPrintProperties.DisplayGroup, ngaytao, trangthai, maKhoa);

                DataRow[] arrOK = _dtDanhsach.Select("trang_thai=3");//Đã xong
                DataRow[] arrDangThien = _dtDanhsach.Select("trang_thai=2");//Đang thực hiện
                DataRow[] arrDANGCHO = _dtDanhsach.Select("trang_thai=1", "So_Kham");//Mới tạo
                DataRow[] arrNHO = _dtDanhsach.Select("trang_thai=0");//Nhỡ
                if (arrOK.Length > 0)
                {
                    if (uiTab1.SelectedIndex != 2 || grdDathuchien.DataSource == null || Reload)
                        grdDathuchien.DataSource = arrOK.CopyToDataTable();
                }
                else
                    grdDathuchien.DataSource = _dtDanhsach.Clone();
                Utility.AddColumToDataTable(ref _dtDanhsach, "IsNo1", typeof(int));
                for (int j = 0; j < _dtDanhsach.Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        _dtDanhsach.Rows[j]["IsNo1"] = 1;
                    }
                    else
                    {
                        _dtDanhsach.Rows[j]["IsNo1"] = 0;
                    }
                }

                var mDtDanhSachChoKhamNext = _dtDanhsach.Clone();
                if (arrDANGCHO.Length != 0)
                {
                    mDtDanhSachChoKhamNext = arrDANGCHO.CopyToDataTable();
                    //mDtDanhSachChoKhamNext = mDtDanhSachChoKhamNext.AsEnumerable().Take(5).CopyToDataTable();
                }
                var mDtDanhSachChoKhamPass = new DataTable();
                if (arrNHO.Length != 0)
                {
                    mDtDanhSachChoKhamPass = arrNHO.CopyToDataTable();
                    //mDtDanhSachChoKhamPass = mDtDanhSachChoKhamPass.AsEnumerable().Take(5).CopyToDataTable();
                }
                //DataTable mDtDanhSachChoKhamPass = _mDtDanhSachChoKham.Select("Hien_Thi = 0").CopyToDataTable();
                string nhacnho = "";

                if (mDtDanhSachChoKhamPass.Rows.Count > 0)
                {
                    nhacnho = "Danh sách bệnh nhân nhỡ: ";
                    if (uiTab1.SelectedIndex != 1 || grdListNho.DataSource == null || Reload)
                        grdListNho.DataSource = mDtDanhSachChoKhamPass;
                    //Utility.SetDataSourceForDataGridEx(grdListNho, mDtDanhSachChoKhamPass, false, true, "1=1", "");
                    foreach (var row in mDtDanhSachChoKhamPass.AsEnumerable())
                    {
                        nhacnho = nhacnho +
                                  string.Format("{0} - {1};  ", row["So_Kham"].ToString(), row["TEN_Benhnhan"].ToString());
                    }
                }
                else
                {
                    grdListNho.DataSource = null;
                }
                if (arrDangThien.Length > 0)
                    foreach (DataRow dr in arrDangThien.CopyToDataTable().Rows)
                    {
                        DataRow dr1 = mDtDanhSachChoKhamNext.NewRow();
                        Utility.CopyData(dr, ref dr1);
                        mDtDanhSachChoKhamNext.Rows.InsertAt(dr1, 0);
                    }
                if (mDtDanhSachChoKhamNext.Rows.Count > 0)
                {
                    if (uiTab1.SelectedIndex != 0 || grdList.DataSource == null || Reload)
                        grdList.DataSource = mDtDanhSachChoKhamNext;
                }
                else
                {
                    grdList.DataSource = null;

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.ToString());
            }
            finally
            {
                Reload = false;
            }

        }
        public int ThoiGianTuDongLay = 5000;
        private void FrmShowListData_Load(object sender, EventArgs e)
        {
            timer1.Interval = ThoiGianTuDongLay;// QmsPrintProperties.ThoiGianTuDongLay;
            timer1.Enabled = true;// QmsPrintProperties.TuDongLayThongTin;
            timer1.Start();
            _qms.UsingWS = UsingWS;
            LoadDanhSach(QmsPrintProperties.MaPhongQMS, DateTime.Now, 100, QmsPrintProperties.MaKhoaQMS);
        }

        private void grdList_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            Reload = true;
            if (e.Column.Key == "cmdnho")
            {
                long regId = Utility.Int64Dbnull(grdList.CurrentRow.Cells["Id"].Value, -1);
                string PatientCode = Utility.sDbnull(grdList.CurrentRow.Cells["ma_luotkham"].Value, "");
                if (regId > 0)
                {
                    //_qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(grdList.CurrentRow.Cells["id_kham"].Value, -1), Utility.Int64Dbnull(grdList.CurrentRow.Cells["id"].Value, -1), 0);
                    _qms.QmsPK_CapnhatTrangthai(-1, Utility.Int64Dbnull(grdList.CurrentRow.Cells["id"].Value, -1), 0, QMSType);
                    //_qms.UpdateStatusQms(PatientCode, QmsPrintProperties.MaPhongQMS, DateTime.Now, 0);
                    LoadDanhSach(QmsPrintProperties.MaPhongQMS, DateTime.Now, 100, QmsPrintProperties.MaKhoaQMS);
                }
            }
            else if (e.Column.Key == "cmdHuy")
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy số QMS của bệnh nhân " + grdList.CurrentRow.Cells["TEN_BENHNHAN"].Value.ToString() + " ?", "Xác nhận", true))
                {
                    int QMS_ID = Utility.Int32Dbnull(grdList.CurrentRow.Cells["Id"].Value, -1);
                    _qms.DeleteQms(QMS_ID);
                    LoadDanhSach(QmsPrintProperties.MaPhongQMS, DateTime.Now, 100, QmsPrintProperties.MaKhoaQMS);
                }
            }
            else if (e.Column.Key == "cmdLoa")
            {

                long So_Kham = Utility.Int64Dbnull(grdList.CurrentRow.Cells["So_Kham"].Value, -1);
                string TenBenhNhan = Utility.sDbnull(grdList.CurrentRow.Cells["Ten_benhnhan"].Value, "");
                if (So_Kham > 0)
                {
                    _qms.InsertGoiLoa(Utility.sDbnull(So_Kham), QmsPrintProperties.MaPhongQMS,
                        globalVariables.gv_strMacAddress, QmsPrintProperties.MaKhoaQMS, 0,1,
                        globalVariables.UserName, globalVariables.SysDate, globalVariables.gv_strComputerName,
                        QmsPrintProperties.MaLoaGoi,
                        string.Format("{0} {1} {2} {3}", "Mời bệnh nhân", TenBenhNhan, "đến phòng ",
                            QmsPrintProperties.MaPhongQMS));
                }
            }
            
        }

        private void grdListNho_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            Reload = true;
            if (e.Column.Key == "cmdCho")
            {
               long regId = Utility.Int64Dbnull(grdListNho.CurrentRow.Cells["Id"].Value, -1);
               string PatientCode = Utility.sDbnull(grdListNho.CurrentRow.Cells["ma_luotkham"].Value, "");
                if (regId > 0)
                {
                    _qms.QmsPK_CapnhatTrangthai(-1, Utility.Int64Dbnull(grdListNho.CurrentRow.Cells["id"].Value, -1), 1, QMSType);
                    //_qms.UpdateStatusQms(PatientCode, QmsPrintProperties.MaPhongQMS, DateTime.Now, 1);
                }
                LoadDanhSach(QmsPrintProperties.MaPhongQMS, DateTime.Now, 100, QmsPrintProperties.MaKhoaQMS);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Reload) return;
            LoadDanhSach(QmsPrintProperties.MaPhongQMS, DateTime.Now, 100, QmsPrintProperties.MaKhoaQMS);
        }
    }
}
