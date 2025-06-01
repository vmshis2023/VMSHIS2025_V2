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
using System.Linq;
using VNS.Properties;
using Janus.Windows.GridEX;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_capnhat_idthuockho : Form
    {
        private DataTable _mDtKhothuoc = new DataTable();
        private DataTable _mDataFull = new DataTable();
        private DataTable _mDtkho = new DataTable();
        string _kieuThuocvattu = "THUOCVT";
        string SplitterPath = "";
        public int id_kho = -1;
        public int id_thuoc = -1;
        public long id_phieu = -1;
        public long id_phieu_ctiet = -1;
        public byte loai = 1;
        public decimal so_luong = 0;
        public long id_thuockho = -1;
        public frm_capnhat_idthuockho()
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            FormClosing += frm_capnhat_idthuockho_FormClosing;
            Shown += frm_capnhat_idthuockho_Shown;
            cmdSave.Click += new EventHandler(cmdSave_Click);
        }

        void frm_capnhat_idthuockho_Shown(object sender, EventArgs e)
        {

        }

        void frm_capnhat_idthuockho_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Không xác định được id_thuockho. Vui lòng chọn trên lưới");
                    return;
                }
                //Check xem phiếu đang chọn ở trạng thái chưa xác nhận mới cho cập nhật
                if (loai != 1)///các loại phiếu xuất mới kiểm tra, còn đơn thuốc thì không cần
                {
                    TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(id_phieu);
                    if (objPhieuNhap.TrangThai == 1)
                    {
                        Utility.ShowMsg("Phiếu đã chọn đã được xác nhận nên bạn không được phép cập nhật lại id thuốc kho của bất kỳ chi tiết nào thuộc phiếu. Muốn cập nhật id thuốc kho thì cần hủy xác nhận phiếu");
                        return;
                    }
                }

                Int64 id_thuockho = Utility.Int64Dbnull(grdList.GetValue("id_thuockho"), 0);
                decimal sl = Utility.DecimaltoDbnull(grdList.GetValue("So_luong"), 0);
                int cho_xacnhan = Utility.Int32Dbnull(grdList.GetValue("choxacnhan"), 0);
                sl = sl - cho_xacnhan;
                if (sl < so_luong)
                {
                    Utility.ShowMsg(string.Format("Bạn cần chọn dòng thuốc kho có số lượng thực kê(Số lượng - chờ xác nhận) >={0}", so_luong));
                    return;
                }
                if (id_thuockho > 0)
                {
                    this.id_thuockho = id_thuockho;
                    SPs.ThuocCapnhatIdThuockho(id_kho, id_thuoc, id_phieu, id_phieu_ctiet, id_thuockho, loai).Execute();
                    Utility.ShowMsg("Cập nhật id_thuockho cho thuốc đang chọn thành công. Nhấn OK để kết thúc");
                    this.Close();
                }
                else
                {
                    Utility.ShowMsg("Không xác định được id_thuockho. Vui lòng chọn trên lưới");
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
        /// <summary>
        /// hàm thực hiện việc load lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_UpdateSoLuongTon_Load(object sender, EventArgs e)
        {
            LoadThuocTrongKho();
        }

        private void LoadThuocTrongKho()
        {
            DataTable dtData = SPs.TThuockhoGetdataCapnhat(id_kho, id_thuoc).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, dtData, true, true, "1=1", "ngay_hethan");

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

                LoadThuocTrongKho();
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                cmdSave.PerformClick();
                return;
            }

        }

        private void chkAnthuoc0_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdXemchoxacnhan_Click(object sender, EventArgs e)
        {
            try
            {
                bool isParent = this.ActiveControl != null && this.ActiveControl.Name == grdList.Name;
                frm_ThuocChoXacNhan _ThuocChoXacNhan = new frm_ThuocChoXacNhan();
                _ThuocChoXacNhan.id_kho = Utility.Int32Dbnull(id_kho, -1);
                _ThuocChoXacNhan.id_thuoc = Utility.Int32Dbnull(grdList.GetValue("id_thuoc"), -1);
                _ThuocChoXacNhan.id_ThuocKho = Utility.isValidGrid(grdList) ? Utility.Int32Dbnull(grdList.GetValue("id_thuockho"), -1) : -1;
                _ThuocChoXacNhan.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


    }
}
