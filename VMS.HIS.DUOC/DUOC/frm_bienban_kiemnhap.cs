using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using System.Transactions;
using VNS.HIS.UI.DANHMUC;

namespace VMS.HIS.Duoc.DUOC
{
    public partial class frm_bienban_kiemnhap : Form
    {
        long id_phieu = -1;
        byte loai_phieu = 1;
        TBienbanKiemnhap objBienban = null;
        DataTable dtChitiet = new DataTable();
        public frm_bienban_kiemnhap(long id_phieu,byte loai_phieu)
        {
            InitializeComponent();
            this.id_phieu = id_phieu;
            this.loai_phieu = loai_phieu;
            dtp_ngaybienban.Value = globalVariables.SysDate;
            grd_hoidong.MouseDoubleClick += Grd_hoidong_MouseDoubleClick;
            txt_loaihoidong._OnShowDataV1 += _OnShowDataV1;
            txt_chucdanh._OnShowDataV1 += _OnShowDataV1;
            txt_chucvu._OnShowDataV1 += _OnShowDataV1;
            cmd_quanly_chucdanh.Click += Cmd_quanly_chucdanh_Click;
            cmd_quanly_chucvu.Click += Cmd_quanly_chucvu_Click;
            cmd_quanly_loaihoidong.Click += Cmd_quanly_loaihoidong_Click;
        }

        private void Cmd_quanly_loaihoidong_Click(object sender, EventArgs e)
        {
            _OnShowDataV1(txt_loaihoidong);
        }

        private void Cmd_quanly_chucvu_Click(object sender, EventArgs e)
        {
            _OnShowDataV1(txt_chucvu);
        }

        private void Cmd_quanly_chucdanh_Click(object sender, EventArgs e)
        {
            _OnShowDataV1(txt_chucdanh);
        }

        private void _OnShowDataV1(VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        private void Grd_hoidong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Capnhat();
        }
        void Capnhat()
        {
            try
            {
                if (!Utility.isValidGrid(grd_hoidong)) return;
                Int16 id_nhanvien = Utility.Int16Dbnull(grd_hoidong.GetValue("id_nhanvien"));
                txt_IdHoiDong.Text = Utility.sDbnull(grd_hoidong.GetValue("id_hoidong"));
                txt_hoten.SetId(id_nhanvien);
                txt_chucdanh._Text = Utility.sDbnull(grd_hoidong.GetValue("chuc_danh"));
                txt_chucvu._Text = Utility.sDbnull(grd_hoidong.GetValue("chuc_vu"));
                nmr_stt.Value = Utility.DecimaltoDbnull(grd_hoidong.GetValue("stt"));
            }
            catch (Exception)
            {


            }
        }
        private void frm_bienban_kiemnhap_Load(object sender, EventArgs e)
        {
            InitData();
        }
        void InitData()
        {
            try
            {
                txt_chucdanh.Init();
                txt_chucvu.Init();
                txt_loaihoidong.Init();
                txt_hoten.Init(globalVariables.gv_dtDmucNhanvien,
                                           new List<string>
                                {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                });
                dtChitiet = new Select().From(TBienbanKiemnhapChitiet.Schema).Where(TBienbanKiemnhapChitiet.Columns.IdBienban).IsEqualTo(-1).ExecuteDataSet().Tables[0];
                objBienban = new Select().From(TBienbanKiemnhap.Schema).Where(TBienbanKiemnhap.Columns.IdPhieu).IsEqualTo(id_phieu).And(TBienbanKiemnhap.Columns.LoaiPhieu).IsEqualTo(loai_phieu).ExecuteSingle<TBienbanKiemnhap>();
                if(objBienban!=null)
                {
                    objBienban.NgaySua = DateTime.Now;
                    objBienban.NguoiSua = globalVariables.UserName;
                    txt_loaihoidong.SetCode(objBienban.LoaiHoidong);
                    dtp_ngaybienban.Value = objBienban.NgayBienban.Value;
                    dtChitiet = new Select().From(TBienbanKiemnhapChitiet.Schema).Where(TBienbanKiemnhapChitiet.Columns.IdBienban).IsEqualTo(objBienban.IdBienban).ExecuteDataSet().Tables[0];
                   
                }
                else
                {
                    objBienban = new TBienbanKiemnhap();
                    objBienban.NgayTao = DateTime.Now;
                    objBienban.NguoiTao = globalVariables.UserName;
                    objBienban.IdPhieu = id_phieu;
                    objBienban.LoaiPhieu = loai_phieu;
                }
                dtChitiet.Columns.Add(new DataColumn("uuid", typeof(string)));
                Utility.SetDataSourceForDataGridEx_Basic(grd_hoidong, dtChitiet, true, true, "1=1", "ten_nhanvien");

            }
            catch (Exception ex)
            {


            }
        }
        private void ModifyCommand()
        {
            cmd_sua.Enabled = dtChitiet.Rows.Count > 0;
        }
        bool isValidData()
        {
            errorProvider1.Clear();
            if (Utility.DoTrim(txt_loaihoidong.Text)=="")
            {
                errorProvider1.SetError(txt_loaihoidong, "Bạn nhập loại hội đồng");
                txt_loaihoidong.Focus();
                txt_loaihoidong.SelectAll();
                return false;
            }
            if (Utility.Int32Dbnull( txt_hoten.MyID)<=0)
            {
                errorProvider1.SetError(txt_hoten, "Bạn cần chọn thông tin thành viên hội đồng từ danh mục nhân viên");
                txt_hoten.Focus();
                txt_hoten.SelectAll();
                return false;
            }
            if (Utility.DoTrim( txt_chucdanh.Text )== "")
            {
                errorProvider1.SetError(txt_chucdanh, "Bạn cần nhập thông tin chức danh");
                txt_chucdanh.Focus();
                txt_chucdanh.SelectAll();
                return false;
            }
            if (Utility.DoTrim(txt_chucvu.Text) == "")
            {
                errorProvider1.SetError(txt_chucvu, "Bạn cần nhập thông tin chức vụ");
                txt_chucvu.Focus();
                txt_chucvu.SelectAll();
                return false;
            }
            return true;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objBienban.Save();
                        new Delete().From(TBienbanKiemnhapChitiet.Schema)
                            .Where(TBienbanKiemnhapChitiet.Columns.IdBienban).IsEqualTo(objBienban.IdBienban)
                            .Execute();
                        foreach (DataRow dr in dtChitiet.Rows)//Insert lại chi tiết thay vì kiểm tra thêm mới or cập nhật
                        {
                            TBienbanKiemnhapChitiet newItem = new TBienbanKiemnhapChitiet();
                            newItem.IdBienban = objBienban.IdBienban;
                            newItem.IdNhanvien = Utility.Int16Dbnull(dr["id_nhanvien"]);
                            newItem.TenNhanvien = Utility.sDbnull(dr["ten_nhanvien"]);
                            newItem.ChucDanh = Utility.sDbnull(dr["chuc_danh"]);
                            newItem.ChucVu = Utility.sDbnull(dr["chuc_vu"]);
                            newItem.Stt = Utility.ByteDbnull(dr["stt"]);
                            newItem.Save();

                        }
                    }
                    scope.Complete();
                }
                Utility.ShowMsg("Lưu biên bản thành công");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }

        private void cmd_add_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void cmd_sua_Click(object sender, EventArgs e)
        {
            Capnhat();
        }
        void Reset()
        {
            txt_IdHoiDong.Text = "-1";
            txt_hoten.SetId(-1);
            txt_chucdanh.SetCode("-1");
            txt_chucvu.SetCode("-1");
            txt_hoten.Focus();
        }
        private void cmd_luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData()) return;
                if (Utility.Int64Dbnull(txt_IdHoiDong.Text) <= 0)
                {
                    DataRow newItem = dtChitiet.NewRow();
                    newItem["id_bienban"] = -1;
                    newItem["id_nhanvien"] = Utility.Int16Dbnull(txt_hoten.MyID);
                    newItem["chuc_danh"] = txt_chucdanh.Text;
                    newItem["chuc_vu"] = txt_chucvu.Text;
                    newItem["stt"] = Utility.ByteDbnull(nmr_stt.Value);
                    newItem["id_hoidong"] = Utility.Int64Dbnull(txt_IdHoiDong.Text);
                    newItem["uuid"] = Guid.NewGuid().ToString();
                    dtChitiet.Rows.Add(newItem);
                }
                else
                {
                    DataRow item = dtChitiet.Select(string.Format("id_hoidong={0} or uuid='{1}'", txt_IdHoiDong.Text,txt_uuid.Text)).FirstOrDefault();
                    if (item != null)
                    {
                        item["id_nhanvien"] = Utility.Int16Dbnull(txt_hoten.MyID);
                        item["chuc_danh"] = txt_chucdanh.Text;
                        item["chuc_vu"] = txt_chucvu.Text;
                        item["stt"] = Utility.ByteDbnull(nmr_stt.Value);
                    }
                }
                Reset();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
