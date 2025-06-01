using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class frm_KetnoiHisLis : Form
    {
        DataSet dsData = null;
        string ma_luotkham = "";
        Int64 id_benhnhan = -1;
        string MaBenhpham = "";
        string MaChidinh = "";
        int currRowIdx = -1;
        long id_chidinh = -1;
        int co_chitiet = -1;
        KcbLuotkham objLuotkham = null;
        bool AllowChanged = false;
        public frm_KetnoiHisLis()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_KetnoiHisLis_Load;
            this.KeyDown += frm_KetnoiHisLis_KeyDown;
            cmdSearch.Click += cmdSearch_Click;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            grdChidinh.CurrentCellChanged += grdChidinh_CurrentCellChanged;
            grdKetqua.UpdatingCell += grdKetqua_UpdatingCell;
            mnuCancelResult.Click += mnuCancelResult_Click;
            cmdConfirm.Click += cmdConfirm_Click;
            optAll.CheckedChanged += Transfer_CheckedChanged;
            optTransfered.CheckedChanged += Transfer_CheckedChanged;
            optNotTransfer.CheckedChanged += Transfer_CheckedChanged;
        }

        void Transfer_CheckedChanged(object sender, EventArgs e)
        {
            Hienthiphieuchidinh();
        }
        bool IsvalidTransferData()
        {
            if (grdChidinh.GetCheckedRows().Length <= 0)
            {
                Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn ít nhất một phiếu chỉ định để chuyển sang LIS"), false);
                return false;
            }
            List<long> lstIchidinh = (from q in grdChidinh.GetCheckedRows()
                                      select Utility.Int64Dbnull(q.Cells[KcbChidinhcl.Columns.IdChidinh].Value, 0)
                                     ).ToList<long>();
            if (cmdConfirm.Tag.ToString() == "1")
            {
                //Kiểm tra có dịch vụ trang_thai=1(chuyển cận) hay không?
                var p = from q in dsData.Tables[0].AsEnumerable()
                        where lstIchidinh.Contains(Utility.Int64Dbnull(q[KcbChidinhcl.Columns.IdChidinh], 0))
                        && Utility.Int64Dbnull(q["trang_thai"], 0) == 1
                        select q;
                if (!p.Any())
                {
                    Utility.SetMsg(lblMsg, string.Format("Các dịch vụ trong các phiếu chỉ định bạn chọn đã được chuyển sang LIS hoặc chưa được chuyển cận. Mời bạn kiểm tra lại"), false);
                    return false;
                }
            }
            else
            {
                //Kiểm tra có dịch vụ trang_thai=2(đã chuyển sang LIS) hay không?
                var p = from q in dsData.Tables[0].AsEnumerable()
                        where lstIchidinh.Contains(Utility.Int64Dbnull(q[KcbChidinhcl.Columns.IdChidinh], 0))
                        && Utility.Int64Dbnull(q["trang_thai"], 0) == 2
                        select q;
                if (!p.Any())
                {
                    Utility.SetMsg(lblMsg, string.Format("Các phiếu chỉ định bạn chọn không chứa bất kỳ dịch vụ nào ĐÃ CHUYỂN SANG LIS để HỦY BỎ. Mời bạn kiểm tra lại"), false);
                    return false;
                }
            }
            return true;
        }
        void cmdConfirm_Click(object sender, EventArgs e)
        {
            if (!IsvalidTransferData()) return;
            DataTable dt2LIS = dsData.Tables[1].Copy();
            List<string> lstIdchidinhchitiet = new List<string>();
            if (dt2LIS != null && dt2LIS.Rows.Count >= 0)
            {
                if (dt2LIS.Rows.Count == 0)
                {
                    Utility.SetMsg(lblMsg, string.Format("Các chỉ định CLS đã được chuyển hết. Mời bạn kiểm tra lại"), false);
                    return;
                }
                else
                {
                    if (cmdConfirm.Tag.ToString() == "1")
                    {
                        //Lấy các dữ liệu cần chuyển sang LIS
                        List<long> lstIchidinh = (from q in grdChidinh.GetCheckedRows()
                                                  select Utility.Int64Dbnull(q.Cells[KcbChidinhcl.Columns.IdChidinh].Value, 0)
                                        ).ToList<long>();
                        List<DataRow> lstData2Send = (from p in dsData.Tables[0].AsEnumerable()
                                                      where lstIchidinh.Contains(Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                      && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                                                      select p).ToList<DataRow>();

                        List<DataRow> lstData2Send_real = (from p in dsData.Tables[1].AsEnumerable()
                                                      where lstIchidinh.Contains(Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                      && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                                                      select p).ToList<DataRow>();
                        if (lstData2Send.Any())
                        {
                            dt2LIS = lstData2Send_real.CopyToDataTable();
                            lstIdchidinhchitiet = (from p in lstData2Send
                                                   select Utility.sDbnull(p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).Distinct().ToList<string>();

                            int result = VMS.HIS.HLC.ASTM.RocheCommunication.WriteOrderMessage(THU_VIEN_CHUNG.Laygiatrithamsohethong("ASTM_ORDERS_FOLDER", @"\\192.168.1.254\Orders", false), dt2LIS);
                            if (result == 0)//Thành công
                            {
                                SPs.HisLisCapnhatdulieuchuyensangLis(string.Join(",", lstIdchidinhchitiet.ToArray()),2,1).Execute();
                                dsData.Tables[0].AsEnumerable()
                                    .Where(c => lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trang_thai"] = 2; c1["ten_trangthai"] = "Đang thực hiện"; });
                                dsData.Tables[1].AsEnumerable()
                                   .Where(c => lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                   .ToList<DataRow>()
                                   .ForEach(c1 => { c1["trang_thai"] = 2; c1["ten_trangthai"] = "Đang thực hiện"; });
                                dsData.AcceptChanges();
                                Utility.SetMsg(lblMsg, string.Format("Các dữ liệu dịch vụ cận lâm sàng của Bệnh nhân đã được gửi thành công sang LIS"), false);
                            }
                        }
                        else
                        {
                            Utility.SetMsg(lblMsg, string.Format("Không tìm được chi tiết nào có trạng thái đã chuyển CLS(trạng thái=1) để chuyển sang LIS"), false);
                        }
                    }
                    else//Hủy chuyển sang LIS
                    {
                        //Lấy các dữ liệu cần chuyển sang LIS
                        List<long> lstIchidinh = (from q in grdChidinh.GetCheckedRows()
                                                  select Utility.Int64Dbnull(q.Cells[KcbChidinhcl.Columns.IdChidinh].Value, 0)
                                        ).ToList<long>();
                        List<DataRow> lstData2Send = (from p in dsData.Tables[0].AsEnumerable()
                                                      where lstIchidinh.Contains(Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                      && Utility.Int64Dbnull(p["trang_thai"], 0) == 2
                                                      select p).ToList<DataRow>();
                        List<DataRow> lstData2Send_real = (from p in dsData.Tables[1].AsEnumerable()
                                                           where lstIchidinh.Contains(Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                           && Utility.Int64Dbnull(p["trang_thai"], 0) == 2
                                                           select p).ToList<DataRow>();
                        if (lstData2Send.Any())
                        {
                            dt2LIS = lstData2Send_real.CopyToDataTable();
                            lstIdchidinhchitiet = (from p in lstData2Send
                                                   select Utility.sDbnull(p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).Distinct().ToList<string>();

                                SPs.HisLisCapnhatdulieuchuyensangLis(string.Join(",", lstIdchidinhchitiet.ToArray()),1,2).Execute();
                                dsData.Tables[0].AsEnumerable()
                                    .Where(c => lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trang_thai"] = 1; c1["ten_trangthai"] = "Đã chuyển cận"; });
                                dsData.Tables[1].AsEnumerable()
                                   .Where(c => lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                   .ToList<DataRow>()
                                   .ForEach(c1 => { c1["trang_thai"] = 1; c1["ten_trangthai"] = "Đã chuyển cận"; });
                                dsData.AcceptChanges();
                                Utility.SetMsg(lblMsg, string.Format("Các dữ liệu dịch vụ cận lâm sàng của Bệnh nhân đã được hủy gửi sang LIS thành công-->Chú ý báo nhân viên khoa xét nghiệm dừng không làm xét nghiệm cho các chỉ định vừa hủy"), false);
                        }
                        else
                        {
                            Utility.SetMsg(lblMsg, string.Format("Không tìm được chi tiết nào có trạng thái đã chuyển sang LIS(trạng thái=2) để hủy chuyển"), false);
                        }
                    }
                    Hienthiphieuchidinh();
                }
            }
        }

        void mnuCancelResult_Click(object sender, EventArgs e)
        {
           
        }

        //KcbChidinhclsChitiet.Trang_thai:0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
        void grdKetqua_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
           
        }

        void grdChidinh_CurrentCellChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdChidinh) || !AllowChanged)
            {
                currRowIdx = -1;
                grdKetqua.DataSource = null;
                return;
            }
            int tempRowIdx = grdChidinh.CurrentRow.RowIndex;
            if (currRowIdx == -1 || currRowIdx != tempRowIdx)
            {
                currRowIdx = tempRowIdx;
                id_chidinh = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.IdChidinh), 0);
                ma_luotkham = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaLuotkham);
                MaChidinh = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaChidinh);
                MaBenhpham = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaBenhpham);
                HienthiNhapketqua(id_chidinh);
            }
        }
        void HienthiNhapketqua(long id_chidinh)
        {
            try
            {
                DataTable dt = dsData.Tables[0].Clone();
                string rowfilter = "Id_chidinh=" + id_chidinh.ToString();
                if (optAll.Checked)
                {
                }
                else if (optTransfered.Checked)
                {
                    rowfilter += " AND  trang_thai>=2";
                }
                else//Chi hien thi cac phieu con chua du lieu chua chuyen sang LIS
                {
                    rowfilter += " AND  trang_thai=1";
                }

                DataRow[] arrDr = dsData.Tables[0].Select(rowfilter);
                if (arrDr.Length > 0)
                    dt = arrDr.CopyToDataTable();
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
            }
            catch (Exception ex)
            {


            }
        }
        void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }



        void frm_KetnoiHisLis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
                return;
            }
            
        }

        void frm_KetnoiHisLis_Load(object sender, EventArgs e)
        {
            txtMaluotkham.SelectAll();
            txtMaluotkham.Focus();
        }

        void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
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
                txtKhoaDieuTri.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenKhoanoitru]);
                txtBuong.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBuong]);
                txtGiuong.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenGiuong]);

                txtTrangthaiNgoaitru.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TrangthaiNgoaitru]) == "0" ? "Đang khám" : "Đã khám xong";
                txtTrangthaiNoitru.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenTrangthaiNoitru]);

                txtGioitinh.Text =
                    Utility.sDbnull(dr[VKcbLuotkham.Columns.GioiTinh], "");
                txtPatient_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
                txtNamSinh.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.NamSinh], "");
                txtSoBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MatheBhyt], "");
                txtTuoi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.Tuoi]);
            }
        }
        void SearchData()
        {
            try
            {
                AllowChanged = false;
                objLuotkham = null;
                ma_luotkham = "ALL";
                KcbChidinhcl objchidinh =
                    new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.MaChidinh).IsEqualTo(
                        txtMaluotkham.Text).ExecuteSingle<KcbChidinhcl>();
                ma_luotkham = objchidinh.MaLuotkham;
                //if (Utility.DoTrim(txtMaluotkham.Text) != "")
                //    ma_luotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
              //  txtMaluotkham.Text = ma_luotkham;
                objLuotkham = KcbLuotkham.FetchByID(ma_luotkham);
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu theo mã lượt khám " + ma_luotkham + ". Mời bạn nhập lại thông tin");
                    txtMaluotkham.SelectAll();
                    txtMaluotkham.Focus();
                    return;
                }
                dsData = SPs.HisLisLaydulieuchuyensangLis(dtNgaychidinh.Value.ToString("dd/MM/yyyy"), objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet();
                if (dsData == null || dsData.Tables.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu chỉ định dịch vụ theo điều kiện mã lượt khám = " + ma_luotkham + ". Mời bạn nhập lại thông tin");
                    txtMaluotkham.SelectAll();
                    txtMaluotkham.Focus();
                    return;
                }
               
                id_benhnhan = objLuotkham.IdBenhnhan;
                ma_luotkham = objLuotkham.MaLuotkham;
                DataTable dtPatientInfor = SPs.CommonLaythongtinbenhnhantheoIdMaluotkham(id_benhnhan, ma_luotkham).GetDataSet().Tables[0];
                if (dtPatientInfor.Rows.Count > 0)
                    FillPatientData(dtPatientInfor.Rows[0]);
                else
                    FillPatientData(null);
                Hienthiphieuchidinh();
                AllowChanged = true;
              

            }
            catch (Exception)
            {
                ma_luotkham = "";
            }
        }
        void Hienthiphieuchidinh()
        {
            DataTable dtPhieuChidinh = dsData.Tables[0].Clone();
            List<long> lstIDPhieu = new List<long>();
            foreach (DataRow dr in dsData.Tables[0].Rows)
            {
                long id_chidinh=Utility.Int64Dbnull(dr["id_chidinh"]);
                if (!lstIDPhieu.Contains(id_chidinh))
                {
                    lstIDPhieu.Add(id_chidinh);
                    if (optAll.Checked)
                    {
                        dtPhieuChidinh.ImportRow(dr);
                        cmdConfirm.Text = "Đẩy sang LIS(Ctrl+S)";
                        cmdConfirm.ToolTipText = "Chuyển dữ liệu Bệnh nhân & Chỉ định sang bên LIS";
                        cmdConfirm.Tag = "1";
                    }
                    else if (optTransfered.Checked)
                    {
                        DataRow[] chidinhchitiet = dsData.Tables[0].Select("Id_chidinh=" + id_chidinh.ToString() + " AND trang_thai>=2");
                        if (chidinhchitiet.Length > 0)
                            dtPhieuChidinh.ImportRow(dr);

                        cmdConfirm.Text = "Hủy chuyển LIS(Ctrl+H)";
                        cmdConfirm.ToolTipText = "Hủy chuyển các phiếu chỉ định đã được chuyển sang LIS-->Cần báo phòng xét nghiệm dừng không làm dịch vụ cho Bệnh nhân hủy nếu đang thực hiện";
                        cmdConfirm.Tag = "0";
                    }
                    else//Chi hien thi cac phieu con chua du lieu chua chuyen sang LIS
                    {
                        DataRow[] chidinhchitiet = dsData.Tables[0].Select("Id_chidinh=" + id_chidinh.ToString() + " AND trang_thai=1");
                        if (chidinhchitiet.Length > 0)
                            dtPhieuChidinh.ImportRow(dr);

                        cmdConfirm.Text = "Đẩy sang LIS(Ctrl+S)";
                        cmdConfirm.ToolTipText = "Chuyển dữ liệu Bệnh nhân & Chỉ định sang bên LIS";
                        cmdConfirm.Tag = "1";
                    }
                }
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdChidinh, dtPhieuChidinh, true, true, "1=1", "ngay_chidinh asc,id_chidinh asc");
            grdChidinh.MoveFirst();
            AllowChanged = true;
            grdChidinh_CurrentCellChanged(grdChidinh, new EventArgs());
        }
    }
    public class Phieuchidinh
    {
        public string ma_chidinh;
        public long id_chidinh;
        public DateTime ngay_chidinh;
        public string ma_khoa_chidinh;
        public long id_benhnhan;
        public string ma_luotkham;


        

    }
}
