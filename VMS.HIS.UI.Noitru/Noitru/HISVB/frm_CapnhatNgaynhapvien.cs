using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using System.Transactions;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_CapnhatNgaynhapvien : Form
    {
        public KcbLuotkham objLuotkham;
        public bool b_Cancel = false;
        public frm_CapnhatNgaynhapvien()
        {
            InitializeComponent();
            dtNgayNhapVien.Value = THU_VIEN_CHUNG.GetSysDateTime();
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                dtNgayNhapVien.Value =objLuotkham.NgayNhapvien.HasValue? objLuotkham.NgayNhapvien.Value:DateTime.Now;
                dtngaytiepdon.Value = objLuotkham.NgayTiepdon;
                dtNgayNhapVien.Enabled = objLuotkham.TrangthaiNoitru <= TrangthaiNoitru_ChanChinhNgayNV;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InValiUpdateNgayNhapVien()) return;
                using (var scope = new TransactionScope())
                {
                    SPs.NoitruPhieunhapvienCapnhatngaynhapvien(dtNgayNhapVien.Value, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).Execute();
                    scope.Complete();
                }
                objLuotkham.NgayNhapvien = dtNgayNhapVien.Value;

                Utility.ShowMsg("Bạn cập nhập thông tin ngày nhập viện thành công", "Thông báo");
                Utility.Log(Name, globalVariables.UserName,
                           string.Format("Sửa ngày nhập viện, và ngày tiếp đón {0}", objLuotkham.MaLuotkham), newaction.Update, "UI");
                this.Close();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private bool InValiUpdateNgayNhapVien()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
                
            if (objLuotkham==null)
            {
                Utility.ShowMsg("Không tồn tại bệnh nhân này","Thông báo",MessageBoxIcon.Error);
                return false;
            }

            if (objLuotkham.TrangthaiNoitru>=3)
            {
                Utility.ShowMsg("Bệnh nhân này đã làm thủ tục ra viện, Mời bạn xem lại thông tin ", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            //Tạm rem 20240716
            //NoitruPhanbuonggiuong bg = new Select().From(NoitruPhanbuonggiuong.Schema)
            //  .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //  .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
            //  .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //  .OrderAsc(NoitruPhanbuonggiuong.Columns.NgayPhangiuong).ExecuteSingle<NoitruPhanbuonggiuong>();
            //if (dtNgayNhapVien.Value > bg.NgayPhangiuong)
            //{
            //    Utility.ShowMsg(string.Format("Ngày vào viện {0} không được sau ngày phân buồng giường lần đầu {1}.\nVui lòng kiểm tra lại thông tin", dtNgayNhapVien.Value.ToString("dd/MM/yyyy HH:mm:ss"), bg.NgayPhangiuong.Value.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo", MessageBoxIcon.Warning);
            //    dtNgayNhapVien.Focus();
            //    return false;
            //}
            NoitruPhieudieutri phieudieutri = new Select().From(NoitruPhieudieutri.Schema)
              .Where(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .OrderAsc(NoitruPhieudieutri.Columns.NgayDieutri)
              .ExecuteSingle<NoitruPhieudieutri>();
            if (phieudieutri != null)
            {

                string gioidieutri = Utility.DoTrim(phieudieutri.GioDieutri);
                int hours = Utility.Int32Dbnull(gioidieutri.Split(':')[0], 0);
                int minutes = Utility.Int32Dbnull(gioidieutri.Split(':')[1], 0);
                int seconds = Utility.Int32Dbnull(gioidieutri.Split(':')[2], 0);
                DateTime ngaydieutri = new DateTime(phieudieutri.NgayDieutri.Value.Year, phieudieutri.NgayDieutri.Value.Month, phieudieutri.NgayDieutri.Value.Day, hours, minutes, seconds);
                if (dtNgayNhapVien.Value > ngaydieutri)
                {
                    Utility.ShowMsg(string.Format("Ngày vào viện {0} không thể sau ngày lập phiếu điều trị lần đầu {1}.\nVui lòng kiểm tra lại thông tin", dtNgayNhapVien.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngaydieutri.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo", MessageBoxIcon.Warning);
                    dtNgayNhapVien.Focus();
                    return false;
                }
            }
            if (dtNgayNhapVien.Value < objLuotkham.NgayTiepdon)
            {
                Utility.ShowMsg("Ngày vào viện không thể nhỏ hơn ngày tiếp đón khám bệnh", "Thông báo", MessageBoxIcon.Warning);
                dtNgayNhapVien.Focus();
                return false;
            }

           
            return true;
        }
        int TrangthaiNoitru_ChanChinhNgayNV = 2;
        private void frm_CapnhatNgaynhapvien_Load(object sender, EventArgs e)
        {
            int TrangthaiNoitru_ChanChinhNgayNV =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("TrangthaiNoitru_ChanChinhNgayNV", "2", true),2);
            if(objLuotkham!=null)
            {
                if (objLuotkham.NgayNhapvien.HasValue)
                    dtNgayNhapVien.Value = Convert.ToDateTime(objLuotkham.NgayNhapvien.Value);
                dtngaytiepdon.Value = Convert.ToDateTime(objLuotkham.NgayTiepdon);
                dtNgayNhapVien.Enabled = objLuotkham.TrangthaiNoitru <= TrangthaiNoitru_ChanChinhNgayNV;
            }
        }
        /// <summary>
        /// hàm thực hiện việc lưu lại thông itn của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_CapnhatNgaynhapvien_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.S&&e.Control)cmdSave.PerformClick();
        }
    }
}
