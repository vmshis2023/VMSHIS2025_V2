using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
namespace  VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_MokhoaBenhnhan : Form
    {
        public frm_KCB_MokhoaBenhnhan()
        {
            InitializeComponent();
            cmdKhoa.Click += cmdKhoa_Click;
        }

        void cmdKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn Bệnh nhân cần để khóa");
                    txtMaluotkham.Focus();
                    txtMaluotkham.SelectAll();
                    return;
                }
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn khóa bệnh nhân này không ?", "Xác nhận", true))
                    return;
                objLuotkham.LastActionName = "Khóa bệnh nhân";
                objLuotkham.Locked = 1;
                objLuotkham.Save();
                cmdKhoa.Enabled = !Utility.Byte2Bool(objLuotkham.Locked);
                cmdMoKhoa.Enabled = !cmdKhoa.Enabled;
                Utility.ShowMsg("Đã khóa Bệnh nhân thành công");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }

        string ma_luotkham = "";
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                GetData();
            }
           
        }
        KcbLuotkham objLuotkham = null;
        private void GetData()
        {
            try
            {
                if (Utility.DoTrim(txtMaluotkham.Text) == "")
                {
                    objLuotkham = null;
                    FillPatientData(null);
                    return;
                }
                ma_luotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                objLuotkham = KcbLuotkham.FetchByID(ma_luotkham);
                if (objLuotkham != null)
                {
                    cmdKhoa.Enabled = !Utility.Byte2Bool(objLuotkham.Locked);
                    cmdMoKhoa.Enabled = !cmdKhoa.Enabled;
                    DataTable dtPatientInfor = SPs.CommonLaythongtinbenhnhantheoIdMaluotkham(objLuotkham.IdBenhnhan, ma_luotkham).GetDataSet().Tables[0];
                    if (dtPatientInfor.Rows.Count > 0)
                        FillPatientData(dtPatientInfor.Rows[0]);
                    else
                        FillPatientData(null);
                }
                else
                    FillPatientData(null);

            }
            catch
            {
            }
            finally
            {
            }
        }
        void FillPatientData(DataRow dr)
        {
            if (dr == null)
            {
                txtKhoaDieuTri.Clear();
                txtBuong.Clear();
                txtGiuong.Clear();

                txtTrangthaiNgoaitru.Clear();
                txtTrangthaiNoitru.Clear();

                txtGioitinh.Clear();
                txtPatient_Name.Clear();
                txtDiaChi.Clear();

                txtSoBHYT.Clear();
                txtTuoi.Clear();
            }
            else
            {
                cmdKhoa.Enabled = cmdMoKhoa.Enabled = false;
                txtKhoaDieuTri.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenKhoanoitru]);
                txtBuong.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBuong]);
                txtGiuong.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenGiuong]);

                txtTrangthaiNgoaitru.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TrangthaiNgoaitru]) == "0" ? "Đang khám" : "Đã khám xong";
                txtTrangthaiNoitru.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenTrangthaiNoitru]);

                txtGioitinh.Text =
                    Utility.sDbnull(dr[VKcbLuotkham.Columns.GioiTinh], "");
                txtPatient_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
                txtTuoi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.Tuoi], "");
                txtSoBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MatheBhyt], "");
                txtTuoi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.Tuoi]);
            }
        }
       
        private void frm_KCB_MokhoaBenhnhan_Load(object sender, EventArgs e)
        {
            FillPatientData(null);
        }

        private void cmdMoKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn Bệnh nhân cần mở khóa");
                    txtMaluotkham.Focus();
                    txtMaluotkham.SelectAll();
                    return;
                }
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn mở khóa bệnh nhân này không ?", "Xác nhận", true))
                    return;
                objLuotkham.LastActionName = "Mở khóa bệnh nhân";
                objLuotkham.Locked = 0;
                objLuotkham.Save();
                cmdKhoa.Enabled = !Utility.Byte2Bool(objLuotkham.Locked);
                cmdMoKhoa.Enabled = !cmdKhoa.Enabled;
                Utility.ShowMsg("Đã mở khóa Bệnh nhân thành công");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
    }
}
