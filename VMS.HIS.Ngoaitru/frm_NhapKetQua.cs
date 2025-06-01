using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Janus.Windows.GridEX;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.UCs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_NhapKetQua : Form
    {
        public frm_NhapKetQua()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }
        public static StoredProcedure KcbLaythongtinchidinhTheosophieu(string sophieu)
        {
            var sp = new StoredProcedure("kcb_laythongtinchidinh_theosophieu", DataService.GetInstance("ORM"), "dbo");
            sp.Command.AddParameter("@sophieu", sophieu, DbType.String, null, null);
            return sp;
        }
        private void txtSoPhieu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //string sophieu = txtSoPhieu.Text;
                    //sophieu = sophieu.Insert(6, ".");
                    grdChidinh.DataSource = null;
                    DataTable dt = KcbLaythongtinchidinhTheosophieu(Utility.sDbnull(txtSoPhieu.Text)).GetDataSet().Tables[0];
                    if (dt.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Số phiếu không tồn tại! \n Mời bạn check lại với phòng khám");
                        return;
                    }
                    else
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            txtidbenhnhan.Text = Utility.sDbnull(row["id_benhnhan"], "");
                            txtmaluotkham.Text = Utility.sDbnull(row["ma_luotkham"], "");
                            txtmathebhyt.Text = Utility.sDbnull(row["mathe_bhyt"], "");
                            txthovaten.Text = Utility.sDbnull(row["ten_benhnhan"], "");
                            txtgioitinh.Text = Utility.sDbnull(row["gioi_tinh"], "");
                            txtnamsinh.Text = Utility.sDbnull(row["nam_sinh"], "");
                            txtdiachi.Text = Utility.sDbnull(row["dia_chi"], "");
                            txtphong.Text = Utility.sDbnull(row["ten_khoaphong"], "");
                            txtchandoan.Text = Utility.sDbnull(row["chandoan"], "");
                        }
                        grdChidinh.DataSource = dt;
                        txtThongbao.Text = "Chỉ định xét nghiệm bệnh nhân!";
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        private KcbKetquaCl Taomoiketquacls(GridEXRow row)
        {
            var objketqua = new KcbKetquaCl();
            objketqua.IdBenhnhan = Utility.Int64Dbnull(txtidbenhnhan.Text);
            objketqua.MaLuotkham = Utility.sDbnull(txtmaluotkham.Text);
            objketqua.MaBenhpham = Utility.sDbnull(txtSoPhieu.Text);
            objketqua.MaChidinh = Utility.sDbnull(txtSoPhieu.Text);
            objketqua.IdChidinh = Utility.Int64Dbnull(row.Cells["id_chidinh"].Value);
            objketqua.IdChitietchidinh = Utility.Int64Dbnull(row.Cells["id_chitietchidinh"].Value);
            objketqua.IdDichvu = Utility.Int32Dbnull(row.Cells["id_dichvu"].Value);
            objketqua.IdDichvuchitiet = Utility.Int32Dbnull(row.Cells["id_chitietdichvu"].Value);
            objketqua.IdLab = -1;
            objketqua.IdChitietLab = -1;
            objketqua.Barcode = Utility.sDbnull(txtSoPhieu.Text);
            objketqua.SttIn = 0;
            objketqua.KetQua = Utility.sDbnull(row.Cells["ket_qua"].Value);
            objketqua.BtNam = "";
            objketqua.BtNam = "";
            objketqua.TenDonvitinh = "";
            objketqua.TenThongso = Utility.sDbnull(row.Cells["ten_chitietdichvu"].Value);
            objketqua.TenKq ="";
            objketqua.LoaiKq = 0;
            objketqua.ChophepHienthi = 1;
            objketqua.ChophepIn = 1;
            objketqua.MotaThem = Utility.sDbnull(row.Cells["ten_chitietdichvu"].Value);
            objketqua.NguoiTao = globalVariables.UserName;
            objketqua.NgayTao = dtpNgaytraketqua.Value ;
            objketqua.NguoiSua = globalVariables.UserName;
            objketqua.NgaySua = globalVariables.SysDate;
            objketqua.TrangThai = 4;
            objketqua.NguoiXacnhan = globalVariables.UserName;
            objketqua.NgayXacnhan = globalVariables.SysDate;
            objketqua.IpMaytao = "";
            objketqua.IpMaysua = "";
            objketqua.TenMaytao = "";
            objketqua.TenMaysua = "";
            return objketqua;
        }
        private void grdChidinh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    foreach (var row in grdChidinh.GetDataRows())
                    {
                        if(Utility.sDbnull(row.Cells["ket_qua"].Value).Trim() !="")
                        {

                            SqlQuery sqlkt =
                            new Select().From(KcbKetquaCl.Schema).Where(KcbKetquaCl.Columns.IdChitietchidinh).IsEqualTo(
                                row.Cells["id_chitietchidinh"].Value).And(KcbKetquaCl.Columns.IdDichvuchitiet).IsEqualTo( row.Cells["id_chitietdichvu"].Value);
                            var objketqua = Taomoiketquacls(row);
                            if (sqlkt.GetRecordCount() > 0)
                            {
                                objketqua.IsNew = false;
                                objketqua.MarkOld();
                            }
                            else
                            {
                                objketqua.IsNew = true;
                            }
                            objketqua.Save();
                            new Update(KcbChidinhclsChitiet.Schema)
                                .Set(KcbChidinhclsChitiet.Columns.KetQua).EqualTo(Utility.sDbnull(row.Cells["ket_qua"].Value))
                                .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(4)
                                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                                    Utility.Int64Dbnull(row.Cells["id_chitietchidinh"].Value)).Execute();
                        }
                        
                    }
                    txtThongbao.Text = @"Lưu kết quả thành công!";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }

        private void frm_NhapKetQua_Load(object sender, EventArgs e)
        {
            if(globalVariables.IsAdmin)
            {
                txtidbenhnhan.Visible = true;
                dtpNgaytraketqua.Value = DateTime.Now;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            foreach (var row in grdChidinh.GetDataRows())
            {
                if (Utility.sDbnull(row.Cells["ket_qua"].Value).Trim() != "")
                {

                    SqlQuery sqlkt =
                    new Select().From(KcbKetquaCl.Schema).Where(KcbKetquaCl.Columns.IdChitietchidinh).IsEqualTo(
                        row.Cells["id_chitietchidinh"].Value).And(KcbKetquaCl.Columns.IdDichvuchitiet).IsEqualTo(row.Cells["id_chitietdichvu"].Value);
                    var objketqua = Taomoiketquacls(row);
                    if (sqlkt.GetRecordCount() > 0)
                    {
                        objketqua.IsNew = false;
                        objketqua.MarkOld();
                    }
                    else
                    {
                        objketqua.IsNew = true;
                    }
                    objketqua.Save();
                    new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.KetQua).EqualTo(Utility.sDbnull(row.Cells["ket_qua"].Value))
                        .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(4)
                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                            Utility.Int64Dbnull(row.Cells["id_chitietchidinh"].Value)).Execute();
                }

            }
            txtThongbao.Text = @"Lưu kết quả thành công!";
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.KetQua)
                .EqualTo("")
                .Where(KcbChidinhclsChitiet.Columns.MaLuotkham)
                .IsEqualTo(txtmaluotkham.Text)
                .Execute();
            new Delete().From(KcbKetquaCl.Schema)
                .Where(KcbKetquaCl.Columns.MaLuotkham)
                .IsEqualTo(txtmaluotkham.Text)
                .Execute();
            grdChidinh.DataSource = null;
            DataTable dt = KcbLaythongtinchidinhTheosophieu(Utility.sDbnull(txtSoPhieu.Text)).GetDataSet().Tables[0];
            if (dt.Rows.Count <= 0)
            {
                Utility.ShowMsg("Số phiếu không tồn tại! \n Mời bạn check lại với phòng khám");
                return;
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtidbenhnhan.Text = Utility.sDbnull(row["id_benhnhan"], "");
                    txtmaluotkham.Text = Utility.sDbnull(row["ma_luotkham"], "");
                    txtmathebhyt.Text = Utility.sDbnull(row["mathe_bhyt"], "");
                    txthovaten.Text = Utility.sDbnull(row["ten_benhnhan"], "");
                    txtgioitinh.Text = Utility.sDbnull(row["gioi_tinh"], "");
                    txtnamsinh.Text = Utility.sDbnull(row["nam_sinh"], "");
                    txtdiachi.Text = Utility.sDbnull(row["dia_chi"], "");
                    txtphong.Text = Utility.sDbnull(row["ten_khoaphong"], "");
                    txtchandoan.Text = Utility.sDbnull(row["chandoan"], "");
                }
                grdChidinh.DataSource = dt;
            }
            txtThongbao.Text = @"Xóa kết quả thành công!";

        }

    }
}
