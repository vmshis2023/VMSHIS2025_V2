using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_CheckError : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        public KcbLuotkham objLuotkham;
       public long id_thanhtoan;
          public byte v_bytNoitru;
          public string lst_IDLoaithanhtoan;
          KcbThanhtoan objThanhtoan;
        public frm_CheckError(long id_thanhtoan,  byte v_bytNoitru, string lst_IDLoaithanhtoan)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.id_thanhtoan = id_thanhtoan;
            this.v_bytNoitru = v_bytNoitru;
            this.lst_IDLoaithanhtoan = lst_IDLoaithanhtoan;
        }

        private void frm_CheckError_Load(object sender, EventArgs e)
        {
            try
            {
                objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                DataTable m_dtChiPhiThanhtoan =
                     _THANHTOAN.LayThongtinChuaThanhtoan_CheckError(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, v_bytNoitru,
                         globalVariables.MA_KHOA_THIEN, objLuotkham.MaDoituongKcb, lst_IDLoaithanhtoan, id_thanhtoan);
                Utility.AddColumToDataTable(ref m_dtChiPhiThanhtoan, "colCHON", typeof(byte));
                Utility.AddColumToDataTable(ref m_dtChiPhiThanhtoan, "ck_nguongt", typeof(byte));
                m_dtChiPhiThanhtoan.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, false, true, "1=1", "");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void mnuUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrMsg = "";
                  List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                  if (Utility.DoTrim(ErrMsg).Length > 0)
                  {
                      Utility.ShowMsg(ErrMsg);
                      return;
                  }
                  if (lstItems == null)
                  {
                      Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                      return;
                  }
                  if (lstItems.Count<=0)
                  {
                      Utility.ShowMsg("Bạn cần chọn các bản ghi ở hạng mục trạng thái tồn tại =0 ");
                      return;
                  }
                  if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn tạo lại dữ liệu thanh toán chi tiết cho các bản ghi này?", "Xác nhận", true)) return;
                  ActionResult actionResult = _THANHTOAN.ThanhtoanChiphiDvuKcb_FixError(id_thanhtoan, lstItems, ref ErrMsg);
                  switch (actionResult)
                  {
                      case ActionResult.Success:
                          Utility.ShowMsg("Tạo dữ liệu thanh toán chi tiết cho các dịch vụ đang chọn vào bản ghi thanh toán đã chọn thành công");
                          break;
                      case ActionResult.Error:
                          Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                          break;
                      case ActionResult.Cancel:
                          Utility.ShowMsg(ErrMsg);
                          break;
                  }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private List<KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                DataTable dtDataCheck = new DataTable();
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (GridEXRow row in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                    if (Utility.Int32Dbnull(row.Cells["Tontai"].Value, 1) == 0)
                    {
                        KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                        newItem.IdThanhtoan = -1;
                        newItem.IdChitiet = -1;
                        newItem.TinhChiphi = 1;
                        if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                        if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                        newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.SoLuong].Value, 0);
                        //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                        newItem.BnhanChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BnhanChitra].Value, 0);
                        newItem.BhytChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BhytChitra].Value, 0);
                        newItem.DonGia = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.DonGia].Value, 0);
                        newItem.GiaGoc = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.GiaGoc].Value, 0);
                        newItem.TyleTt = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TyleTt].Value, 0);
                        newItem.PhuThu = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.PhuThu].Value, 0);
                        newItem.TinhChkhau = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TinhChkhau].Value, 0);
                        newItem.CkNguongt = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.CkNguongt].Value, 0);
                        newItem.TuTuc = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TuTuc].Value, 0);
                        newItem.IdPhieu = Utility.Int32Dbnull(row.Cells["id_phieu"].Value);
                        newItem.IdKham = Utility.Int32Dbnull(row.Cells["Id_Kham"].Value);
                        newItem.IdPhieuChitiet = Utility.Int32Dbnull(row.Cells["Id_Phieu_Chitiet"].Value, -1);
                        newItem.IdDichvu = Utility.Int16Dbnull(row.Cells["Id_dichvu"].Value, -1);
                        newItem.IdChitietdichvu = Utility.Int16Dbnull(row.Cells["Id_Chitietdichvu"].Value, -1);
                        newItem.TenChitietdichvu = Utility.sDbnull(row.Cells["Ten_Chitietdichvu"].Value, "Không xác định").Trim();
                        newItem.TenBhyt = Utility.sDbnull(row.Cells["ten_bhyt"].Value, "Không xác định").Trim();
                        newItem.DonviTinh = Utility.chuanhoachuoi(Utility.sDbnull(row.Cells["Ten_donvitinh"].Value, "Lượt"));
                        newItem.SttIn = Utility.Int16Dbnull(row.Cells["stt_in"].Value, 0);
                        newItem.IdKhoakcb = Utility.Int16Dbnull(row.Cells["id_khoakcb"].Value, -1);
                        newItem.IdPhongkham = Utility.Int16Dbnull(row.Cells["id_phongkham"].Value, -1);
                        newItem.IdBacsiChidinh = Utility.Int16Dbnull(row.Cells["id_bacsi"].Value, -1);
                        newItem.IdLoaithanhtoan = Utility.ByteDbnull(row.Cells["Id_Loaithanhtoan"].Value, -1);
                        newItem.IdLichsuDoituongKcb = Utility.Int64Dbnull(row.Cells[KcbThanhtoanChitiet.Columns.IdLichsuDoituongKcb].Value, -1);
                        newItem.MatheBhyt = Utility.sDbnull(row.Cells[KcbThanhtoanChitiet.Columns.MatheBhyt].Value, -1);
                        newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(row.Cells["Id_Loaithanhtoan"].Value, -1));
                        newItem.TienChietkhau = Math.Round(Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TienChietkhau].Value, 0m), 3);
                        newItem.TileChietkhau = Math.Round(Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TileChietkhau].Value, 0m), 3);
                        newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                        newItem.UserTao = Utility.sDbnull(row.Cells["User_tao"].Value, "UKN").Trim();
                        newItem.KieuChietkhau = "%";
                        newItem.IdThanhtoanhuy = -1;
                        newItem.TrangthaiHuy = 0;
                        newItem.TrangthaiBhyt = 0;
                        newItem.TrangthaiChuyen = 0;
                        newItem.NoiTru = v_bytNoitru;
                        newItem.NguonGoc = (byte)0;
                        newItem.NgayTao = objThanhtoan.NgayTao;
                        newItem.NguoiTao = objThanhtoan.NguoiTao;
                        lstItems.Add(newItem);
                        dtDataCheck = SPs.ThanhtoanKiemtratontaitruockhithanhtoan(newItem.IdPhieu, newItem.IdPhieuChitiet, newItem.IdLoaithanhtoan).GetDataSet().Tables[0];
                        if (dtDataCheck.Rows.Count <= 0)
                            errMsg += newItem.TenChitietdichvu + "\n";
                    }
                }
                if (errMsg.Length > 0)
                    errMsg = "Một số dịch vụ đang chọn thanh toán đã bị xóa/hủy bởi người khác. Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ không tồn tại dưới đây:\n" + errMsg;
                return lstItems;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
    }
}
