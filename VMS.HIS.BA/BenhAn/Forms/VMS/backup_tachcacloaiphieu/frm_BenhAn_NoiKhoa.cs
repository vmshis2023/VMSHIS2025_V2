using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Aspose.Words;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.UI.Classess;
using VMS.HIS.Bus;
using VMS.HIS.Danhmuc.Dungchung;
using System.Transactions;

namespace VNS.HIS.UI.BA
{
    public partial class frm_BenhAn_NoiKhoa : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        public frm_BenhAn_NoiKhoa(string lstLoaiBA)
        {
            InitializeComponent();
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            chkVaovien_Capcuu.CheckedChanged += chkQLNBCapCuu_CheckedChanged;
            chkVaovien_Kkb.CheckedChanged += chkQLNBKKB_CheckedChanged;
            chkVaovien_khoadieutri.CheckedChanged += chkQLNBKhoaDieuTri_CheckedChanged;
            chkNoigioithieu_tuden.CheckedChanged += chkQLNBTuDen_CheckedChanged;
            chkNoigioithieu_coquanyte.CheckedChanged += chkQLNBCoQuanYTe_CheckedChanged;
            chkNoigioithieu_khac.CheckedChanged += chkQLNBKhac_CheckedChanged;
            chkChuyenvienTuyenduoi.CheckedChanged += chkQLNBTuyenDuoi_CheckedChanged;
            chkChuyenvienTuyentren.CheckedChanged += chkQLNBTuyenTren_CheckedChanged;
            chkRavien_ravien.CheckedChanged += chkQLNBRaVien_CheckedChanged;
            chkChuyenvienKhac.CheckedChanged += chkQLNBCK_CheckedChanged;
            chkRavien_Xinve.CheckedChanged += chkQLNBXinVe_CheckedChanged;
            chkRavien_Bove.CheckedChanged += chkQLNBBoVe_CheckedChanged;
            chkRavien_Duave.CheckedChanged += chkQLNBDuaVe_CheckedChanged;
            chkTTRVKhoi.CheckedChanged += chkTTRVKhoi_CheckedChanged;
            chkTTRVDoGiam.CheckedChanged += chkTTRVDoGiam_CheckedChanged;
            chkTTRV_Khongthaydoi.CheckedChanged += chkTTRVKhongThayDoi_CheckedChanged;
            chkTTRVNangHon.CheckedChanged += chkTTRVNangHon_CheckedChanged;
            chkTTRVTuVong.CheckedChanged += chkTTRVTuVong_CheckedChanged;
            chkTTRV_Lanhtinh.CheckedChanged += chkTTRVLanhTinh_CheckedChanged;
            chkTTRV_Nghingo.CheckedChanged += chkTTRVNghiNgo_CheckedChanged;
            chkTTRV_Actinh.CheckedChanged += chkTTRVAcTinh_CheckedChanged;
            chkttrvDoBenh.CheckedChanged += chkttrvDoBenh_CheckedChanged;
            chkttrvTrong24GioVaoVien.CheckedChanged += chkttrvTrong24GioVaoVien_CheckedChanged;
            chkttrvDoTaiBien.CheckedChanged += chkttrvDoTaiBien_CheckedChanged;
            chkttrvSau24Gio.CheckedChanged += chkttrvSau24Gio_CheckedChanged;
            chkttrvKhac.CheckedChanged += chkttrvKhac_CheckedChanged;
            ucThongtinnguoibenh_v31._OnEnterMe += ucThongtinnguoibenh_v31__OnEnterMe;
            txtIDBenhAn.KeyDown += txtIDBenhAn_KeyDown;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            ucThongtinnguoibenh_v31.trangthai_noitru = 5;
            Utility.setEnterEvent(this);
            chkDiUng.CheckedChanged += chkDiUng_CheckedChanged;
            chkMaTuy.CheckedChanged += chkMaTuy_CheckedChanged;
            chkRuouBia.CheckedChanged += chkRuouBia_CheckedChanged;
            chkThuocLa.CheckedChanged += chkThuocLa_CheckedChanged;
            chkThuocLao.CheckedChanged += chkThuocLao_CheckedChanged;
            chkKhac.CheckedChanged += chkKhac_CheckedChanged;
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;
        }

        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text =( Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
        }

        void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtKhac.Enabled = chkKhac.Checked;
            txtKhac.Focus();
        }

        void chkThuocLao_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLao.Enabled = chkThuocLao.Checked;
            txtThuocLao.Focus();
        }

        void chkThuocLa_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLa.Enabled = chkThuocLa.Checked;
            txtThuocLa.Focus();
        }

        void chkRuouBia_CheckedChanged(object sender, EventArgs e)
        {
            txtRuouBia.Enabled = chkRuouBia.Checked;
            txtRuouBia.Focus();
        }

        void chkMaTuy_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTuy.Enabled = chkMaTuy.Checked;
            txtMaTuy.Focus();
        }

        void chkDiUng_CheckedChanged(object sender, EventArgs e)
        {
            txtDiUng.Enabled = chkDiUng.Checked;
            txtDiUng.Focus();  
        }
        void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string maBA = Utility.AutoFillMaBA(txtMaBenhAn.Text);
                    txtMaBenhAn.Text = maBA;
                    if (objEmrBa != null && maBA != objEmrBa.MaBa)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo mã: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtMaBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    }
                    objEmrBa = new Select().From(EmrBa.Schema).Where(EmrBa.Columns.MaBa).IsEqualTo(Utility.DoTrim(txtMaBenhAn.Text)).ExecuteSingle<EmrBa>();
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_v31.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_v31.Refresh(true);
                        // FillData4Update();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }

        void txtIDBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (objEmrBa != null)
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo ID: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtIDBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_v31.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_v31.Refresh(true);
                        // FillData4Update();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }

        void ucThongtinnguoibenh_v31__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v31.objLuotkham != null && ucThongtinnguoibenh_v31.objBenhnhan!=null)
            {
                if (ucThongtinnguoibenh_v31.objLuotkham.TrangthaiNoitru <= 0)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} đang ở trạng thái ngoại trú nên bạn không thể thực hiện tạo BA được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_v31.objBenhnhan.TenBenhnhan, ucThongtinnguoibenh_v31.objLuotkham.MaLuotkham));
                    objLuotkham = null;
                    objBenhnhan = null;
                    objEmrBa = null;
                    objPhieuravien = null;
                    objPhieuravien = null;
                    objChandoan = null;
                    objTkba = null;
                    objtsb = null;
                    objpknk = null;
                    ClearControl();
                    ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
                    return;
                }
                objEmrBa = null;
                objPhieuravien = null;
                objPhieuravien = null;
                objChandoan = null;
                objTkba = null;
                objtsb = null;
                objpknk = null;
                objLuotkham = ucThongtinnguoibenh_v31.objLuotkham;
                objBenhnhan = ucThongtinnguoibenh_v31.objBenhnhan;
                if (!KiemTraThongTin()) return;
                ClearControl();
                FillData4Update();
                dtQLNBVaoVien.Focus();
                ModifyCommand();
            }
        }

        #region checkbox
        private void chkttrvTrong24GioVaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong24GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;

                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvDoTaiBien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoTaiBien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;

                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvSau24Gio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvSau24Gio.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;

                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvKhac.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;

            }
        }

        private void chkQLNBBoVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRavien_Bove.Checked == true)
            {
                chkRavien_ravien.Checked = false;
                chkRavien_Xinve.Checked = false;

                chkRavien_Duave.Checked = false;

            }
        }

        private void chkQLNBDuaVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRavien_Duave.Checked == true)
            {
                chkRavien_ravien.Checked = false;
                chkRavien_Xinve.Checked = false;
                chkRavien_Bove.Checked = false;


            }
        }

        private void chkTTRVKhoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhoi.Checked == true)
            {

                chkTTRVDoGiam.Checked = false;
                chkTTRV_Khongthaydoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVDoGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVDoGiam.Checked == true)
            {
                chkTTRVKhoi.Checked = false;

                chkTTRV_Khongthaydoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVKhongThayDoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRV_Khongthaydoi.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;

                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVNangHon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNangHon.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRV_Khongthaydoi.Checked = false;

                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVTuVong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVTuVong.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRV_Khongthaydoi.Checked = false;
                chkTTRVNangHon.Checked = false;



            }
        }

        private void chkTTRVLanhTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRV_Lanhtinh.Checked == true)
            {

                chkTTRV_Nghingo.Checked = false;
                chkTTRV_Actinh.Checked = false;

            }
        }

        private void chkTTRVNghiNgo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRV_Nghingo.Checked == true)
            {
                chkTTRV_Lanhtinh.Checked = false;

                chkTTRV_Actinh.Checked = false;

            }
        }

        private void chkTTRVAcTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRV_Actinh.Checked == true)
            {
                chkTTRV_Lanhtinh.Checked = false;
                chkTTRV_Nghingo.Checked = false;


            }
        }

        private void chkttrvDoBenh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoBenh.Checked == true)
            {

                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }


        private void chkQLNBCapCuu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVaovien_Capcuu.Checked == true)
            {
                chkVaovien_Kkb.Checked = false;
                chkVaovien_khoadieutri.Checked = false;


            }
        }

        private void chkQLNBKKB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVaovien_Kkb.Checked == true)
            {

                chkVaovien_khoadieutri.Checked = false;
                chkVaovien_Capcuu.Checked = false;

            }
        }

        private void chkQLNBKhoaDieuTri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVaovien_khoadieutri.Checked == true)
            {
                chkVaovien_Kkb.Checked = false;

                chkVaovien_Capcuu.Checked = false;

            }
        }

        private void chkQLNBCoQuanYTe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoigioithieu_coquanyte.Checked == true)
            {
                chkNoigioithieu_tuden.Checked = false;

                chkNoigioithieu_khac.Checked = false;


            }
        }

        private void chkQLNBTuDen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoigioithieu_tuden.Checked == true)
            {


                chkNoigioithieu_khac.Checked = false;
                chkNoigioithieu_coquanyte.Checked = false;

            }
        }

        private void chkQLNBKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoigioithieu_khac.Checked == true)
            {
                chkNoigioithieu_tuden.Checked = false;
                chkNoigioithieu_coquanyte.Checked = false;

            }
        }

        private void chkQLNBTuyenTren_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChuyenvienTuyentren.Checked == true)
            {

                chkChuyenvienTuyenduoi.Checked = false;
                chkChuyenvienKhac.Checked = false;

            }
        }

        private void chkQLNBTuyenDuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChuyenvienTuyenduoi.Checked == true)
            {
                chkChuyenvienTuyentren.Checked = false;

                chkChuyenvienKhac.Checked = false;

            }
        }

        private void chkQLNBCK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChuyenvienKhac.Checked == true)
            {
                chkChuyenvienTuyentren.Checked = false;
                chkChuyenvienTuyenduoi.Checked = false;


            }
        }

        private void chkQLNBRaVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRavien_ravien.Checked == true)
            {

                chkRavien_Xinve.Checked = false;
                chkRavien_Bove.Checked = false;
                chkRavien_Duave.Checked = false;

            }
        }

        private void chkQLNBXinVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRavien_Xinve.Checked == true)
            {
                chkRavien_ravien.Checked = false;

                chkRavien_Bove.Checked = false;
                chkRavien_Duave.Checked = false;

            }
        }
        #endregion
        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
            ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list = new Select().From(DmucBenh.Schema).Where(DmucBenh.Columns.MaBenh).In(lstICD).ExecuteAsCollection<DmucBenhCollection>();
                    //new DmucBenh().FetchByQuery(               DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_chinh_Name += _objEmrBa.TenBenh + ";";
                    ICD_chinh_Code += _objEmrBa.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_Phu_Name += _objEmrBa.TenBenh + ";";
                    ICD_Phu_Code += _objEmrBa.MaBenh + ";";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

        private void GetChanDoanNoitru()
        {
            var dtPatient = new DataTable();
            dtPatient =
                new Select("*")
                    .From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham)                    .IsEqualTo(objLuotkham.MaLuotkham)
                     .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(2)//Chẩn đoán trong quá trình điều trị nội trú.
                    .And(KcbChandoanKetluan.Columns.Noitru)                    .IsEqualTo(1)
                    .ExecuteDataSet()
                    .Tables[0];
            foreach (DataRow row in dtPatient.Rows)
            {
                ICD_Khoa_NoITru += row["mabenh_chinh"] + ";";
                Name_Khoa_NoITru += row["chandoan"] + ";";
            }
        }

        private void GetChanDoanRaVien()
        {
           
            EmrPhieuravien _phieuravien=new Select().From(EmrPhieuravien.Schema)
                .Where(EmrPhieuravien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                .And(EmrPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrPhieuravien>();
            string chandoan = "";
            string mabenh = "";
            string chandoanphu = "";
            string mabenhphu = "";
           if(_phieuravien!=null)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                GetChanDoanChinhPhu(Utility.sDbnull(_phieuravien.CdRavienMaBenhchinh, ""),
                           Utility.sDbnull(_phieuravien.CdRavienMaBenhphu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                chandoan += string.IsNullOrEmpty(_phieuravien.ChandoanRavien)
                    ? ICD_Name
                    : Utility.sDbnull(_phieuravien.ChandoanRavien);
                mabenh += ICD_Code;
                chandoanphu += ICD_Phu_Name;
                mabenhphu += ICD_Phu_Code;
               //Điền 1 số thông tin ra viện
                dtQLNBRaVien.Text = _phieuravien.QlnbNgayRavien.Value.ToString("dd/MM/yyyy");
                foreach (CheckBox cb in pnlKetquadieutriravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == _phieuravien.TtrvMaKetquadieutri)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                foreach (CheckBox cb in pnlTinhtrangravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == _phieuravien.QlnbMaTinhtrangRavien)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
            }
            txtRavien_Benhchinh.Text = chandoan;
            txtCDMaBenhChinh.Text = Utility.sDbnull(mabenh);
            txtRavien_Benhkemtheo.Text = chandoanphu;
            txtCDMaBenhKemTheo.Text = mabenhphu;

        }
        string Get_ChanDoan_KKB_CapCuu()
        {
            string _result = string.Empty;
            try
            {
                SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.Chandoan, KcbChandoanKetluan.Columns.ChandoanKemtheo, KcbChandoanKetluan.Columns.MabenhChinh, KcbChandoanKetluan.Columns.MabenhPhu)
                                            .From(KcbChandoanKetluan.Schema)
                                              .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                      //.And(KcbChandoanKetluan.Columns.KeyCode).IsEqualTo("NGOAITRU")
                                                      .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan).OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string mabenh = "";
                string tenbenhphu = "";
                string tenbenhchinh = "";
                string mabenhphu = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    string ICD_Phu_Name = "";
                    string ICD_Phu_Code = "";
                    GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MabenhChinh, ""), Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan) ? "" : Utility.sDbnull(objDiagInfo.Chandoan);
                    tenbenhchinh += ICD_Name;
                    mabenh += ICD_Code;
                    tenbenhphu += ICD_Phu_Name;
                    mabenhphu += ICD_Phu_Code;
                }
                _result =THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_SUDUNG_ICD_LAM_CHANDOANSOBO","0",true)=="1"  ? tenbenhchinh + tenbenhphu + chandoan : chandoan; //nếu dùng icd làm cdsb thì trên cdsb đã có tên bệnh rồi, ko cần cộng vào nữa
            }
            catch (Exception)
            {
                _result = string.Empty;
            }
            return _result;
        }

        //private void GetChanDoanKKB()
        //{
        //    SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.DiagInfo, KcbChandoanKetluan.Columns.DifferInfo,
        //                                    KcbChandoanKetluan.Columns.MainDiseaseId, KcbChandoanKetluan.Columns.AuxiDiseaseId).From(
        //                                        KcbChandoanKetluan.Schema)
        //                  .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
        //                  .And(KcbChandoanKetluan.Columns.KeyCode)
        //                    .IsEqualTo("NGOAITRU")
        //                  .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(txtMaBN.Text).OrderAsc(
        //                      KcbChandoanKetluan.Columns.DiagDate);
        //    var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
        //    string chandoan = "";
        //    string mabenh = "";
        //    string tenbenhphu = "";
        //    string tenbenhchinh = "";
        //    string mabenhphu = "";
        //    foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
        //    {
        //        string ICD_Name = "";
        //        string ICD_Code = "";
        //        string ICD_Phu_Name = "";
        //        string ICD_Phu_Code = "";
        //        GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MainDiseaseId, ""),
        //                   Utility.sDbnull(objDiagInfo.AuxiDiseaseId, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
        //        chandoan += string.IsNullOrEmpty(objDiagInfo.DiagInfo) ? "" : Utility.sDbnull(objDiagInfo.DiagInfo);
        //        tenbenhchinh += ICD_Name;
        //        mabenh += ICD_Code;
        //        tenbenhphu += ICD_Phu_Name;
        //        mabenhphu += ICD_Phu_Code;
        //    }
        //    txtCDKKBCapCuu.Text = tenbenhchinh + tenbenhphu + chandoan;
        //    txtCDMaKKBCapCuu.Text = Utility.sDbnull(mabenh + "" + mabenhphu);

        //}
        private void ClearControl()
        {
            //txtMaBN.Clear();
            //txtMaLanKham.Clear();
            txtMaBenhAn.Clear();
            chkRavien_Bove.Checked = false;
            chkChuyenvienKhac.Checked = false;
            chkChuyenvienTuyenduoi.Checked = false;
            chkChuyenvienTuyentren.Checked = false;
            txtChuyenvienden.Clear();
            dtQLNBRaVien.Clear();
            chkRavien_ravien.Checked = false;


            chkRavien_Xinve.Checked = false;
            chkRavien_Bove.Checked = false;
            chkRavien_Duave.Checked = false;
            txtQLNBTongSoNgayDieuTri.Clear();
            txtCDNoiChuyenDen.Clear();
            txtCDMaNoiChuyenDen.Clear();
            txtCDKKBCapCuu.Clear();
            txtCDMaKKBCapCuu.Clear();


            txtCDKhiVaoDieuTri.Clear();
            txtCDMaKhiVaoDieuTri.Clear();
            txtRavien_Benhchinh.Clear();
            txtCDMaBenhChinh.Clear();
            txtRavien_Benhkemtheo.Clear();
            txtCDMaBenhKemTheo.Clear();
            chkCDThuThuat.Checked = false;
            chkCDPhauThuat.Checked = false;
            chkCDTaiBien.Checked = false;
            chkCDBienChung.Checked = false;
            chkTTRVKhoi.Checked = false;
            chkTTRVDoGiam.Checked = false;
            chkTTRV_Khongthaydoi.Checked = false;
            chkTTRVNangHon.Checked = false;
            chkTTRVTuVong.Checked = false;

            chkTTRV_Lanhtinh.Checked = false;
            chkTTRV_Nghingo.Checked = false;
            chkTTRV_Actinh.Checked = false;

            dtpNgaytuvong.ResetText();
            chkttrvDoBenh.Checked = false;
            chkttrvTrong24GioVaoVien.Checked = false;
            chkttrvDoTaiBien.Checked = false;
            chkttrvSau24Gio.Checked = false;
            chkttrvKhac.Checked = false;
            txtTTRVNguyenNhanChinhTuVong.Clear();
            chkTTRVKhamNgiemTuThi.Checked = false;
            txtTTRVChuanDoanGiaiPhau.Clear();
            txtBenhAnLyDoNhapVien.SetDefaultItem();
            nmrVaovienngaythu.Value = 0;
            txtHoibenh_QuaTrinhBenhLy.Clear();
            txtHoibenh_TsbBanthan.Clear();
            chkDiUng.Checked = false;
            chkMaTuy.Checked = false;
            chkRuouBia.Checked = false;
            chkThuocLa.Checked = false;
            chkThuocLao.Checked = false;
            chkKhac.Checked = false;
            txtDiUng.Clear();
            txtMaTuy.Clear();
            txtRuouBia.Clear();
            txtThuocLa.Clear();
            txtThuocLao.Clear();
            txtKhac.Clear();
            txtTsbGiadinh.Clear();
            txtKhambenh_ToanThan.Clear();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtha.Clear();
            txtNhipTho.Clear();
            txtCanNang.Clear();
            txtChieuCao.Clear();
            txtBMI.Clear();
            txtKhambenh_TuanHoan.Clear();
            txtKhambenh_HoHap.Clear();
            txtKhambenh_TieuHoa.Clear();
            txtKhambenh_ThanTietNieuSinhDuc.Clear();
            txtKhambenh_ThanKinh.Clear();
            txtKhambenh_CoXuongKhop.Clear();
            txtKhambenh_TaiMuiHong.Clear();
            txtKhambenh_RangHamMat.Clear();
            txtKhambenh_Mat.Clear();
            txtKhambenh_Coquankhac.Clear();
            txtBenhAnCacXetNghiem.Clear();
            txtBenhAnTomTatBenhAn.Clear();
            txtBenhAnBenhChinh.Clear();
            txtBenhAnBenhKemTheo.Clear();
            txtBenhAnPhanBiet.Clear();
            txtBenhAnTienLuong.Clear();
            txtBenhAnHuongDieuTri.Clear();
            txtTKBAQuaTrinhBenhLy.Clear();
            txtTKBATTomTatKetQua.Clear();
            txtTKBAPhuongPhapDieuTri.Clear();
            txtTKBATinhTrangRaVien.Clear();
            txtTKBAHuongDieuTri.Clear();
            txtNguoiGiaoHoSo.Clear();
            txtNguoiNhanHoSo.Clear();
            txtBSDieuTri.Clear();
            txtB_CTScanner.Clear();
            txtB_Xquang.Clear();
            txtB_SieuAm.Clear();
            txtB_XetNghiem.Clear();
            txtB_Khac.Clear();


        }
       
        private bool KiemTraThongTin()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham==null)
            {
                Utility.ShowMsg("Bệnh nhân không tồn tại trong CSDL. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                return false;
            }
            if (Utility.sDbnull(cboLoaiBA.SelectedValue, "-1") == "-1")
            {
                Utility.ShowMsg("Cần chọn loại bệnh án");
                cboLoaiBA.Focus();
                return false;
            }
            if (Utility.sDbnull( txtBSlamBA.Text).Length>0 && txtBSlamBA.MyCode=="-1")
            {
                Utility.ShowMsg("Bác sĩ làm Bệnh án cần thuộc danh mục bác sĩ. Mời bạn chọn từ danh mục có sẵn");
                txtBSlamBA.Focus();
                return false;
            }
            if (Utility.sDbnull(txtBSDieuTri.Text).Length > 0 && txtBSDieuTri.MyCode == "-1")
            {
                Utility.ShowMsg("Bác sĩ điều trị cần thuộc danh mục bác sĩ. Mời bạn chọn từ danh mục có sẵn");
                txtBSDieuTri.Focus();
                return false;
            }
            return true;
        }
        DataTable dtDataBA = new DataTable(); 
       
        string maBA = "";
        private bool _isSuccess = false;
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraThongTin()) return;
                CreateEmr();
                //if (objEmrBa.IdBa > 0 && objEmrBa.MaBa != maBA)
                //{
                //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
                //    {
                //    }
                //}
                 EmrHosoluutru hsba =null;
                if (objEmrBa.IdBa <= 0)
                {
                    hsba = new EmrHosoluutru();
                    hsba.IdBa = objEmrBa.IdBa;
                    hsba.LoaiBa = objEmrBa.LoaiBa;
                    hsba.MaBa = objEmrBa.MaBa;
                    hsba.IdBenhnhan = objEmrBa.IdBenhnhan;
                    hsba.MaLuotkham = objEmrBa.MaLuotkham;
                    hsba.MaCoso = objEmrBa.MaCoso;
                    hsba.NgayTao = objEmrBa.NgaylamBa.Value;
                    hsba.NguoiTao = objEmrBa.NguoiTao;
                    hsba.Nam = objEmrBa.NgayTao.Value.Year;
                    hsba.TrangThai = 0;
                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objChandoan.Save();
                        objphieuvaovien.Save();
                        objPhieuravien.Save();
                        objtsb.Save();
                        objpknk.Save();
                        objTkba.Save();
                        objEmrBa.Save();
                        hsba.IdBa = objEmrBa.IdBa;
                        if (hsba != null) hsba.Save();
                        new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(txtBSDieuTri.MyID)
                                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                    .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                    .Execute();
                    }
                    scope.Complete();
                }
                txtIDBenhAn.Text = objEmrBa.IdBa.ToString();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA nội trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objBenhnhan.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Bệnh án thành công. Nhấn Ok để kết thúc");
                    cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                    if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án nội trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objBenhnhan.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                    MessageBox.Show("Đã cập nhật Bệnh án thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaLaythongtinIn(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (objEmrBa != null && _isSuccess)
                {
                    new Update(KcbLuotkham.Schema)
                        .Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(objEmrBa.MaBa)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                   // EmrThemBenhAn();
                }
              
            }
        }
        void EnableBA()
        {
            cboLoaiBA.Enabled = txtIDBenhAn.Enabled=cmdKhoitaoBA.Enabled= m_enAct == action.Insert;

        }
       
        public EmrBa objEmrBa;
        EmrPhieuvaovien objphieuvaovien;
        EmrChandoan objChandoan;
        EmrPhieuravien objPhieuravien;
        EmrTiensubenhDiung objtsb;
        EmrPhieukhamNoikhoa objpknk;
        EmrTongketBenhan objTkba;
        EmrPhieuchuyenvien objphieuchuyenvien;
        void InitData()
        {
            objEmrBa = new Select().From(EmrBa.Schema)
                .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrBa>();

            objphieuvaovien = new Select().From(EmrPhieuvaovien.Schema)
               .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteSingle<EmrPhieuvaovien>();

            objChandoan = new Select().From(EmrChandoan.Schema)
               .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteSingle<EmrChandoan>();

            objPhieuravien = new Select().From(EmrPhieuravien.Schema)
               .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteSingle<EmrPhieuravien>();

            objtsb = new Select().From(EmrTiensubenhDiung.Schema)
               .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteSingle<EmrTiensubenhDiung>();

            objpknk = new Select().From(EmrPhieukhamNoikhoa.Schema)
              .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .ExecuteSingle<EmrPhieukhamNoikhoa>();

            objTkba = new Select().From(EmrTongketBenhan.Schema)
              .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .ExecuteSingle<EmrTongketBenhan>();

            objphieuchuyenvien = new Select().From(EmrPhieuchuyenvien.Schema)
              .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .ExecuteSingle<EmrPhieuchuyenvien>();
        }
        private void  CreateEmr()
        {
            //Lấy lại các phiếu
            InitData();
            if (objEmrBa == null) objEmrBa = new EmrBa();
            if (objphieuvaovien == null) objphieuvaovien = new EmrPhieuvaovien();
            if (objPhieuravien == null) objPhieuravien = new EmrPhieuravien();
            if (objChandoan == null) objChandoan = new EmrChandoan();
            if (objtsb == null) objtsb = new EmrTiensubenhDiung();
            if (objpknk == null) objpknk = new EmrPhieukhamNoikhoa();
            if (objTkba == null) objTkba = new EmrTongketBenhan();
            if (objphieuchuyenvien == null) objphieuchuyenvien = new EmrPhieuchuyenvien();
            try
            {
                if (objphieuvaovien.IdNhapvien > 0)
                {
                    objphieuvaovien.IsLoaded = true;
                    objphieuvaovien.MarkOld();
                    objphieuvaovien.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objphieuvaovien.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objphieuvaovien.MaLuotkham = objLuotkham.MaLuotkham;
                    objphieuvaovien.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objphieuvaovien.NguoiTao = globalVariables.UserName;
                    objphieuvaovien.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                if (objPhieuravien.IdRavien > 0)
                {
                    objPhieuravien.IsLoaded = true;
                    objPhieuravien.MarkOld();
                    objPhieuravien.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objPhieuravien.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objPhieuravien.MaLuotkham = objLuotkham.MaLuotkham;
                    objPhieuravien.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objPhieuravien.NguoiTao = globalVariables.UserName;
                    objPhieuravien.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                if (objphieuchuyenvien.IdChuyenvien > 0)
                {
                    objphieuchuyenvien.IsLoaded = true;
                    objphieuchuyenvien.MarkOld();
                    objphieuchuyenvien.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objphieuchuyenvien.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objphieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                    objphieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objphieuchuyenvien.NguoiTao = globalVariables.UserName;
                    objphieuchuyenvien.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                if (objChandoan.IdChandoan > 0)
                {
                    objChandoan.IsLoaded = true;
                    objChandoan.MarkOld();
                    objChandoan.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objChandoan.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objChandoan.MaLuotkham = objLuotkham.MaLuotkham;
                    objChandoan.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objChandoan.NguoiTao = globalVariables.UserName;
                    objChandoan.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                if (objtsb.IdTsb > 0)
                {
                    objtsb.IsLoaded = true;
                    objtsb.MarkOld();
                    objtsb.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objtsb.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objtsb.MaLuotkham = objLuotkham.MaLuotkham;
                    objtsb.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objtsb.NguoiTao = globalVariables.UserName;
                    objtsb.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                if (objpknk.Id > 0)
                {
                    objpknk.IsLoaded = true;
                    objpknk.MarkOld();
                    objpknk.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objpknk.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objpknk.MaLuotkham = objLuotkham.MaLuotkham;
                    objpknk.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objpknk.NguoiTao = globalVariables.UserName;
                    objpknk.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                if (objTkba.Id > 0)
                {
                    objTkba.IsLoaded = true;
                    objTkba.MarkOld();
                    objTkba.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objTkba.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objTkba.MaLuotkham = objLuotkham.MaLuotkham;
                    objTkba.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objTkba.NguoiTao = globalVariables.UserName;
                    objTkba.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                int id = Utility.Int32Dbnull(txtIDBenhAn.Text, -1);
                if (objEmrBa.IdBa > 0)
                {
                    objEmrBa.IsLoaded = true;
                    objEmrBa.MarkOld();
                    objEmrBa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objEmrBa.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    SinhMaBenhAn();
                    objEmrBa.MaBa = Utility.sDbnull(txtMaBenhAn.Text);
                    objEmrBa.NguoiTao = globalVariables.UserName;
                    objEmrBa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                objEmrBa.NgaylamBa = dtpNgayBA.Value;
                objEmrBa.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                {
                    objEmrBa.Khoa = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_buong"], "");
                }
                else if (dtkhoanhapvien.Rows.Count > 0)
                {
                    objEmrBa.Khoa = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_buong"], "");
                }
                else
                {
                    //REM lại vì đây là khoa nhập viện hoặc khoa nhập viện có nằm giường
                    objEmrBa.Buong = ucThongtinnguoibenh_v31.txtBuong.Text;
                    objEmrBa.Khoa = ucThongtinnguoibenh_v31.txtKhoanoitru.Text;
                    objEmrBa.Giuong = ucThongtinnguoibenh_v31.txtGiuong.Text;
                }
                //objEmrBa.BenhNgoaiKhoa = Utility.sDbnull(txtBenhNgoai_Khoa.Text);
                objEmrBa.MaCoso = objLuotkham.MaCoso;
                objEmrBa.IdBenhnhan = objLuotkham.IdBenhnhan;
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;

                objphieuchuyenvien.ChuyenvienTuyentren = chkChuyenvienTuyentren.Checked;
                objphieuchuyenvien.ChuyenvienTuyenduoi = chkChuyenvienTuyenduoi.Checked;
                objphieuchuyenvien.ChuyenvienKhac = chkNoigioithieu_khac.Checked;
                objphieuchuyenvien.NoiChuyen =Utility.DoTrim( txtChuyenvienden.Text);

                //objphieuvaovien.QlnbTenKhoavaovien = lblqlbnKhoa.Text;
                //objphieuvaovien.QlnbMaKhoavaovien = lblMakhoavao.Text;
                objPhieuravien.QlnbTongsongayDieutri = Utility.Int16Dbnull(txtQLNBTongSoNgayDieuTri.Text);
                objChandoan.Chandoannoichuyenden = Utility.sDbnull(txtChuyenvienden.Text);
                objChandoan.Chandoankkbcapcu = txtCDKKBCapCuu.Text;
                objChandoan.MaChandoankkbcapcu = txtCDMaKKBCapCuu.Text;
                objChandoan.Chandoankhivaokhoadieutri = txtCDKhiVaoDieuTri.Text;
                objChandoan.MaChandoankhivaokhoadieutri = txtCDMaKhiVaoDieuTri.Text;
                objChandoan.Chandoannoichuyenden = txtCDNoiChuyenDen.Text;
                objChandoan.MaChandoannoichuyenden = txtCDMaNoiChuyenDen.Text;
                objEmrBa.CdThuthuat = Utility.Bool2Bool(chkCDThuThuat.Checked);
                objEmrBa.CdPhauthuat = Utility.Bool2Bool(chkCDPhauThuat.Checked);
                //objEmrBa.CdRvienBchinh = txtCDBenhChinh.Text;
                //objEmrBa.CdMaRvienBchinh = txtCDMaBenhChinh.Text;
                //objEmrBa.CdRvienBphu = txtCDBenhKemTheo.Text;
                //objEmrBa.CdMaRvienBphu = txtCDMaBenhKemTheo.Text;
                objEmrBa.CdTaibien = Utility.Bool2Bool(chkCDTaiBien.Checked);
                objEmrBa.CdBienchung = Utility.Bool2Bool(chkCDBienChung.Checked);

                objPhieuravien.TtrvKetquadieutriKhoi = chkTTRVKhoi.Checked;
                objPhieuravien.TtrvKetquadieutriDogiam = chkTTRVDoGiam.Checked;
                objPhieuravien.TtrvKetquadieutriKhongthaydoi = chkTTRV_Khongthaydoi.Checked;
                objPhieuravien.TtrvKetquadieutriNanghon = chkTTRVNangHon.Checked;
                objPhieuravien.TtrvKetquadieutriTuvong = chkTTRVTuVong.Checked;
                objPhieuravien.TtrvGpbLanhtinh = chkTTRV_Lanhtinh.Checked;
                objPhieuravien.TtrvGpbNghingo = chkTTRV_Nghingo.Checked;
                objPhieuravien.TtrvGpbActinh = chkTTRV_Actinh.Checked;


                objPhieuravien.TtrvThoigianTuvong = dtpNgaytuvong.Value;
                objPhieuravien.TtrvLydotuvongDobenh = chkttrvDoBenh.Checked;
                objPhieuravien.TtrvLydotuvongDotaibiendieutri = chkttrvDoTaiBien.Checked;
                objPhieuravien.TtrvLydotuvongKhac = chkttrvKhac.Checked;
                objPhieuravien.TtrvThoigiantuvongTrong24h = chkttrvTrong24GioVaoVien.Checked;
                objPhieuravien.TtrvThoigiantuvongSau24h = chkttrvSau24Gio.Checked;


                objPhieuravien.TtrvNguyennhantuvong = txtTTRVNguyenNhanChinhTuVong.Text;
                objPhieuravien.TtrvKhamnghiemtuthi = chkTTRVKhamNgiemTuThi.Checked;
                objPhieuravien.TtrvChandoangiauphaututhi = txtTTRVChuanDoanGiaiPhau.Text;
                
                objphieuvaovien.Lydovaovien = txtBenhAnLyDoNhapVien.Text;
                objphieuvaovien.Vaovienngaythu =Utility.Int16Dbnull( nmrVaovienngaythu.Value);
                
                objphieuvaovien.QuatrinhBenhly = txtHoibenh_QuaTrinhBenhLy.Text;
                objphieuvaovien.TiensuBanthan = txtHoibenh_TsbBanthan.Text;
                objtsb.TsbDiung = chkDiUng.Checked;
                objtsb.TsbMatuy = chkMaTuy.Checked;
                objtsb.TsbRuoubia = chkRuouBia.Checked ;
                objtsb.TsbThuocla = chkThuocLa.Checked;
                objtsb.TsbThuoclao = chkThuocLao.Checked;
                objtsb.TsbKhac = chkKhac.Checked;
                objtsb.TsbThoigianDiung = txtDiUng.Text;
                objtsb.TsbThoigianMatuy = txtMaTuy.Text;
                objtsb.TsbThoigianRuoubia = txtRuouBia.Text;
                objtsb.TsbThoigianThuocla = txtThuocLa.Text;
                objtsb.TsbThoigianThuoclao = txtThuocLao.Text;
                objtsb.TsbThoigianKhac = txtKhac.Text;

                objphieuvaovien.TiensuGiadinh = txtTsbGiadinh.Text;

                objpknk.NoikhoaToanthan = txtKhambenh_ToanThan.Text;
                objpknk.Mach = txtMach.Text;
                objpknk.NhietDo = txtNhietDo.Text;
                objpknk.HuyetAp = txtha.Text;
                objpknk.NhịpTho = txtNhipTho.Text;
                objpknk.CanNang = txtCanNang.Text;
                objpknk.ChieuCao = txtChieuCao.Text;
                tinhBMI();
                objpknk.Bmi = txtBMI.Text;
                objpknk.NoikhoaTuanhoan = txtKhambenh_TuanHoan.Text;
                objpknk.NoikhoaHohap = txtKhambenh_HoHap.Text;
                objpknk.NoikhoaTieuhoa = txtKhambenh_TieuHoa.Text;
                objpknk.NoikhoaThantietnieusinhduc = txtKhambenh_ThanTietNieuSinhDuc.Text;
                objpknk.NoikhoaThankinh = txtKhambenh_ThanKinh.Text;
                objpknk.NoikhoaCoxuongkhop = txtKhambenh_CoXuongKhop.Text;
                objpknk.NoikhoaTaimuihong = txtKhambenh_TaiMuiHong.Text;
                objpknk.NoikhoaRanghammat = txtKhambenh_RangHamMat.Text;
                objpknk.NoikhoaMat = txtKhambenh_Mat.Text;
                objpknk.NoikhoaKhac = txtKhambenh_Coquankhac.Text;
                
                objEmrBa.KbXetnghiemClsCanlam = txtBenhAnCacXetNghiem.Text;
                objEmrBa.KbTomtatbenhan = txtBenhAnTomTatBenhAn.Text;
                objEmrBa.CdVaokhoadieutriBenhchinh = txtBenhAnBenhChinh.Text;
                objEmrBa.CdVaokhoadieutriBenhphu = txtBenhAnBenhKemTheo.Text;
                objEmrBa.CdVaokhoadieutriPhanbiet = txtBenhAnPhanBiet.Text;
                objEmrBa.Tienluong = txtBenhAnTienLuong.Text;
                objEmrBa.Huongdieutri = txtBenhAnHuongDieuTri.Text;

                objTkba.QuatrinhbenhlyDienbienlamsang = txtTKBAQuaTrinhBenhLy.Text;
                objTkba.TomtatKqcls = txtTKBATTomTatKetQua.Text;
                objTkba.PhuongphapDieutri = txtTKBAPhuongPhapDieuTri.Text;
                objTkba.TinhtrangRavienMota = txtTKBATinhTrangRaVien.Text;
                objTkba.HuongDieutri = txtTKBAHuongDieuTri.Text;

                objTkba.NguoigiaoHoso = txtNguoiGiaoHoSo.Text;
                objTkba.NguoiNhanhoso = txtNguoiNhanHoSo.Text;
                objTkba.SotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                objTkba.SotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                objTkba.SotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                objTkba.SotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                objTkba.SotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());

            }
        }

        private void frm_BenhAn_NoiKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region "Xử lý multiline"
                if (tabpageTo1.ActiveControl != null)
                {
                    Control ctr = tabpageTo1.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo2.ActiveControl != null)
                {
                    Control ctr = tabpageTo2.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo3.ActiveControl != null)
                {
                    Control ctr = tabpageTo3.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo4.ActiveControl != null)
                {
                    Control ctr = tabpageTo4.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                #endregion
                
            }
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            //if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 0;
                ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 1;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 2;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 3;
            }
        }
        public action m_enAct = action.Insert;
        private void frm_BenhAn_NoiKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgayBA.Value = dtpNgayTKBA.Value = DateTime.Now;
                txtBSDieuTri.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                txtBSlamBA.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                DataTable dtData =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                        .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                        .And(DmucChung.Columns.Ma).In(lstLoaiBA.Split(',').ToList<string>())
                        .OrderAsc(DmucChung.Columns.SttHthi)
                        .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN", "---Chọn loại BA---", true);
                txtBenhAnLyDoNhapVien.Init();
                if (m_enAct == action.Insert)
                {
                    ucThongtinnguoibenh_v31.Refresh();
                }
                else
                {
                    objLuotkham = Utility.getKcbLuotkham(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                    objBenhnhan = Utility.getKcbBenhnhan(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                    FillData4Update();

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
            
        }
        private void GetThongtinChuyenVien()
        {

            KcbPhieuchuyenvien pcv = new Select().From(KcbPhieuchuyenvien.Schema)
                .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbPhieuchuyenvien>();
            if (pcv != null)
            {
                DmucBenhvien objBV = DmucBenhvien.FetchByID(pcv.IdBenhvienChuyenden);
                if (objBV != null)
                {
                    txtCDNoiChuyenDen.Text = objBV.TenBenhvien;
                   
                }
                chkChuyenvienKhac.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 3;
                chkChuyenvienTuyenduoi.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 2;
                chkChuyenvienTuyentren.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 1;
            }
        }

        string ICD_Khoa_NoITru = "";
        string Name_Khoa_NoITru = "";
        DataTable dtkhoachuyen = new DataTable();
        DataTable dtkhoanhapvien = new DataTable();
        DataTable dtkhoanhapvienCoGiuong = new DataTable();
        DataTable dtCacKhoa = new DataTable();
        void FillData4Update()
        {
            try
            {
                maBA = "";
                InitData();
                //Autofill Data

                dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, "-1", -1);
                dtkhoachuyen = dtCacKhoa.Clone();
                DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
                if (arrKhoachuyen.Length > 0) dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
                grdQLNBKhoa.DataSource = dtkhoachuyen;
                DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");
                
                if (arrKhoanhapvien.Length > 0)
                {
                    dtkhoanhapvien = arrKhoanhapvien.CopyToDataTable();
                    lblMakhoavao.Text = Utility.sDbnull(arrKhoanhapvien[0]["ma_khoanoitru"], "");
                    lblqlbnKhoa.Text = Utility.sDbnull(arrKhoanhapvien[0]["ten_khoanoitru"], "");
                }
                var q = from p in dtCacKhoa.AsEnumerable()
                        where Utility.Int32Dbnull(p["id_giuong"], 0) > 0
                        orderby p["ngay_vaokhoa"] ascending
                        select p;
                if (q.Any())
                    dtkhoanhapvienCoGiuong = q.CopyToDataTable();
                if (objBenhnhan.NgayNhapvien.HasValue)
                    dtQLNBVaoVien.Value = objBenhnhan.NgayNhapvien.Value;
                if (objBenhnhan.NgayRavien.HasValue)
                    dtpNgayravien.Text = objBenhnhan.NgayRavien.Value.ToString("dd/MM/yyyy HH:mm:ss");

                txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objLuotkham.SongayDieutri);
                GetChanDoanNoitru();
                GetChanDoanRaVien();
                GetThongtinChuyenVien();
                DongbothongtinTKBA();
                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                if (objPhieuravien != null)
                {
                    dtpNgayravien.Text = Utility.sDbnull(objPhieuravien.QlnbNgayRavien);
                }
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    DataRow dr = dtDataBA.Rows[0];
                    try
                    {
                        txtIDBenhAn.Text = Utility.sDbnull(objEmrBa.IdBa);
                        txtMaBenhAn.Text = Utility.sDbnull(objEmrBa.MaBa);
                        //txtBenhNgoai_Khoa.Text = Utility.sDbnull(objEmrBa.BenhNgoaiKhoa);
                        
                        string ICD_chinh_Name = "";
                        string ICD_chinh_Code = "";
                        string ICD_Phu_Name = "";
                        string ICD_Phu_Code = "";

                        GetChanDoanChinhPhu(objLuotkham.MabenhChinh,
                                            objLuotkham.MabenhPhu,
                                            ref ICD_chinh_Name,
                                            ref ICD_chinh_Code, ref ICD_Phu_Name,
                                            ref ICD_Phu_Code);

                        txtCDKKBCapCuu.Text = ICD_chinh_Name + ICD_Phu_Name;
                        txtCDMaKKBCapCuu.Text = ICD_chinh_Code + ICD_Phu_Code;

                        chkNoigioithieu_tuden.Checked = Utility.Bool2Bool(objphieuvaovien.QlnbNoigioithieuTuden);
                        chkVaovien_Kkb.Checked = Utility.Bool2Bool(objphieuvaovien.QlnbVaovienKkb);
                        chkVaovien_khoadieutri.Checked = Utility.Bool2Bool(objphieuvaovien.QlnbVaovienKhoadieutri);
                        chkNoigioithieu_coquanyte.Checked = Utility.Bool2Bool(objphieuvaovien.QlnbNoigioithieuCoquanyte);
                        chkNoigioithieu_tuden.Checked = Utility.Bool2Bool(objphieuvaovien.QlnbNoigioithieuTuden);
                        chkNoigioithieu_khac.Checked = Utility.Bool2Bool(objphieuvaovien.QlnbNoigioithieuKhac);
                        txtQLNBLanVaoVien.Text = Utility.sDbnull(objphieuvaovien.QlnbVaovienlanthu);
                        lblqlbnKhoa.Text = objphieuvaovien.QlnbTenKhoavaovien;
                        
                       
                        dtpNgayravien.Text = Utility.sDbnull(objPhieuravien.QlnbNgayRavien);// dr["QlnbRaVien"].ToString();
                        chkRavien_ravien.Checked = Utility.Int16Dbnull(objEmrBa.QlnbLydoravien) == 1;
                        chkRavien_Xinve.Checked = Utility.Int16Dbnull(objEmrBa.QlnbLydoravien) == 3;
                        chkRavien_Bove.Checked = Utility.Int16Dbnull(objEmrBa.QlnbLydoravien) == 4;
                        chkRavien_Duave.Checked = Utility.Int16Dbnull(objEmrBa.QlnbLydoravien) == 5;
                        txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objEmrBa.QlnbTongsongayDieutri);
                        
                        
                        txtRavien_Benhchinh.Text = Utility.sDbnull(objEmrBa.CdRvienBchinh);
                        txtCDMaBenhChinh.Text = Utility.sDbnull(objEmrBa.CdMaRvienBchinh);
                        txtRavien_Benhkemtheo.Text = Utility.sDbnull(objEmrBa.CdRvienBphu);
                        txtCDMaBenhKemTheo.Text = Utility.sDbnull(objEmrBa.CdMaRvienBphu);
                        chkCDThuThuat.Checked = Utility.Int16Dbnull(objEmrBa.CdThuThuat) == 1;
                        chkCDPhauThuat.Checked = Utility.Int16Dbnull(objEmrBa.CdPhauThuat) == 1;
                        chkCDTaiBien.Checked = Utility.Int16Dbnull(objEmrBa.CdTaiBien) == 1;
                        chkCDBienChung.Checked = Utility.Int16Dbnull(objEmrBa.CdBienChung) == 1;
                        chkTTRVKhoi.Checked = Utility.Int16Dbnull(objEmrBa.TtrvKquaDtri) == 1;
                        chkTTRVDoGiam.Checked = Utility.Int16Dbnull(objEmrBa.TtrvKquaDtri) == 2;
                        chkTTRV_Khongthaydoi.Checked = Utility.Int16Dbnull(objEmrBa.TtrvKquaDtri) == 3;
                        chkTTRVNangHon.Checked = Utility.Int16Dbnull(objEmrBa.TtrvKquaDtri) == 4;
                        chkTTRVTuVong.Checked = Utility.Int16Dbnull(objEmrBa.TtrvKquaDtri) == 5;
                        chkTTRV_Lanhtinh.Checked = Utility.Int16Dbnull(objEmrBa.TtrvGphauBenh) == 1;
                        chkTTRV_Nghingo.Checked = Utility.Int16Dbnull(objEmrBa.TtrvGphauBenh) == 2;
                        chkTTRV_Actinh.Checked = Utility.Int16Dbnull(objEmrBa.TtrvGphauBenh) == 3;
                        txtTTRVNgayTuVong.Text = Utility.sDbnull(objEmrBa.TtrvTVong); //Utility.sDbnull(dr["TtrvTVong"].ToString());
                        chkttrvDoBenh.Checked = Utility.Int16Dbnull(objEmrBa.TtrvLdoTvong) == 1;
                        chkttrvTrong24GioVaoVien.Checked = Utility.Int16Dbnull(objEmrBa.TtrvLdoTvong) == 2;
                        chkttrvDoTaiBien.Checked = Utility.Int16Dbnull(objEmrBa.TtrvLdoTvong) == 3;
                        chkttrvSau24Gio.Checked = Utility.Int16Dbnull(objEmrBa.TtrvLdoTvong) == 4;
                        chkttrvKhac.Checked = Utility.Int16Dbnull(objEmrBa.TtrvLdoTvong) == 5;
                        txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objEmrBa.TtrvNnhanTvong);
                        chkTTRVKhamNgiemTuThi.Checked = Utility.Int16Dbnull(objEmrBa.TtrvKhamNghiem) == 1;
                        txtTTRVChuanDoanGiaiPhau.Text = Utility.sDbnull(objEmrBa.TtrvCdoanGphau);
                        txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objEmrBa.BaLdvv);// Utility.sDbnull(dr["BaLdvv"].ToString());
                        txtBenhAnVaoNgayThu.Text = Utility.sDbnull(objEmrBa.BaNgayThu);
                        txtHoibenh_QuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.BaQtbl);// Utility.sDbnull(dr["BaQtbl"].ToString());
                        txtHoibenh_TsbBanthan.Text = Utility.sDbnull(objEmrBa.BaTsb);
                        
                        txtTsbGiadinh.Text = Utility.sDbnull(objEmrBa.BaGiaDinh);// Utility.sDbnull(dr["BaGiaDinh"].ToString());
                        txtKhambenh_ToanThan.Text = Utility.sDbnull(objEmrBa.KbToanThan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                        
                        
                        txtBenhAnCacXetNghiem.Text = Utility.sDbnull(objEmrBa.KbXetnghiemClsCanlam);
                        txtBenhAnTomTatBenhAn.Text = Utility.sDbnull(objEmrBa.KbTomtatbenhan);
                        txtBenhAnBenhChinh.Text = Utility.sDbnull(objEmrBa.CdVaokhoadieutriBenhchinh);
                        txtBenhAnBenhKemTheo.Text = Utility.sDbnull(objEmrBa.CdVaokhoadieutriBenhphu);
                        txtBenhAnPhanBiet.Text = Utility.sDbnull(objEmrBa.CdVaokhoadieutriPhanbiet);
                        txtBenhAnTienLuong.Text = Utility.sDbnull(objEmrBa.Tienluong);
                        txtBenhAnHuongDieuTri.Text = Utility.sDbnull(objEmrBa.Huongdieutri);
                       

                       
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.ToString());
                    }
                }
                
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                EnableBA();
            }
          

        }
       void FillChuyenVien()
        {
            chkChuyenvienTuyentren.Checked = Utility.Int16Dbnull(objPhieuravien.tuyen) == 1;
            chkChuyenvienTuyenduoi.Checked = Utility.Int16Dbnull(objEmrBa.QlnbChuyenvien) == 2;
            chkChuyenvienKhac.Checked = Utility.Int16Dbnull(objEmrBa.QlnbChuyenvien) == 3;
            txtChuyenvienden.Text = Utility.sDbnull(objEmrBa.QlnbChuyenden);
        }
        void FillChandoan()
        {
            txtCDNoiChuyenDen.Text = Utility.sDbnull(objChandoan.Chandoannoichuyenden);
            txtCDMaNoiChuyenDen.Text = Utility.sDbnull(objChandoan.MaChandoannoichuyenden);
            txtCDKKBCapCuu.Text = Utility.sDbnull(objChandoan.Chandoankkbcapcu);
            txtCDMaKKBCapCuu.Text = Utility.sDbnull(objChandoan.MaChandoankkbcapcu);
            txtCDKhiVaoDieuTri.Text = Utility.sDbnull(objChandoan.Chandoankhivaokhoadieutri);
            txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull(objChandoan.MaChandoankhivaokhoadieutri);
        }
        void FillPhieuVaovien()
        {
        }
        void FillPhieuRavien()
        {
        }
        void FillTongketBA()
        {
            if (objTkba != null)
            {
                
                
                txtTKBAQuaTrinhBenhLy.Text = objTkba.QuatrinhbenhlyDienbienlamsang;
                txtTKBATTomTatKetQua.Text = objTkba.TomtatKqcls;
                txtTKBAPhuongPhapDieuTri.Text = objTkba.PhuongphapDieutri;
                txtTKBATinhTrangRaVien.Text = objTkba.TinhtrangRavienMota;
                txtTKBAHuongDieuTri.Text = objTkba.HuongDieutri;

                txtNguoiGiaoHoSo.Text = Utility.sDbnull(objTkba.NguoigiaoHoso);
                txtNguoiNhanHoSo.Text = Utility.sDbnull(objTkba.NguoiNhanhoso);

                txtB_CTScanner.Text = Utility.sDbnull(objTkba.SotoCt);
                txtB_Xquang.Text = Utility.sDbnull(objTkba.SotoXquang);
                txtB_SieuAm.Text = Utility.sDbnull(objTkba.SotoSieuam);
                txtB_XetNghiem.Text = Utility.sDbnull(objTkba.SotoXetnghiem);
                txtB_Khac.Text = Utility.sDbnull(objTkba.SotoKhac);
            }
            else
            {

            }
        }
        void FillKhac()
        {
            
                //Điền các thông tin mặc định người bệnh
                //Trang 1
                m_enAct = action.Insert;

                //Trang 2
                if (objphieuvaovien != null)
                {
                    txtBenhAnLyDoNhapVien.SetCode(Utility.sDbnull(objphieuvaovien.Lydovaovien));
                    txtHoibenh_TsbBanthan.Text = Utility.sDbnull(objphieuvaovien.TiensuBanthan);
                    txtTsbGiadinh.Text = Utility.sDbnull(objphieuvaovien.TiensuGiadinh);
                    txtHoibenh_QuaTrinhBenhLy.Text = Utility.sDbnull(objphieuvaovien.QuatrinhBenhly);
                }
                //Trang 3
                //Trang 4
                //GetChanDoanKKB();
                txtCDKKBCapCuu.Text = Get_ChanDoan_KKB_CapCuu();
                txtCDMaKKBCapCuu.Text = Utility.sDbnull(objLuotkham.MabenhChinh, string.Empty);
                KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                    .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
                    .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                if (tef != null)
                {
                    txtMach.Text = Utility.sDbnull(tef.Mach);
                    txtNhietDo.Text = Utility.sDbnull(tef.Nhietdo);
                    txtha.Text = Utility.sDbnull(tef.Huyetap);
                    txtNhipTho.Text = Utility.sDbnull(tef.Nhiptho);
                    txtCanNang.Text = Utility.sDbnull(tef.Cannang);
                    txtChieuCao.Text = Utility.sDbnull(tef.Chieucao);
                    tinhBMI();
                }
            
        }

        void FillTiensubenh()
        {
            if (objtsb != null)
            {
                //Thông tin dị ứng
                chkDiUng.Checked = Utility.Int16Dbnull(objtsb.TsbDiung) == 1;
                chkMaTuy.Checked = Utility.Int16Dbnull(objtsb.TsbMatuy) == 1;
                chkRuouBia.Checked = Utility.Int16Dbnull(objtsb.TsbRuoubia) == 1;
                chkThuocLa.Checked = Utility.Int16Dbnull(objtsb.TsbThuocla) == 1;
                chkThuocLao.Checked = Utility.Int16Dbnull(objtsb.TsbThuoclao) == 1;
                chkKhac.Checked = Utility.Int16Dbnull(objtsb.TsbKhac) == 1;
                txtDiUng.Text = Utility.sDbnull(objtsb.TsbThoigianDiung);
                txtMaTuy.Text = Utility.sDbnull(objtsb.TsbThoigianMatuy);
                txtRuouBia.Text = Utility.sDbnull(objtsb.TsbThoigianRuoubia);
                txtThuocLa.Text = Utility.sDbnull(objtsb.TsbThoigianThuocla);
                txtThuocLao.Text = Utility.sDbnull(objtsb.TsbThoigianThuoclao);
                txtKhac.Text = Utility.sDbnull(objtsb.TsbKhac);
            }
        }
        void FillPhieukhamNoikhoa()
        {
            if (objpknk != null)
            {

                txtKhambenh_ToanThan.Text = objpknk.NoikhoaToanthan;
                txtKhambenh_TuanHoan.Text = objpknk.NoikhoaTuanhoan;
                txtNhietDo.Text = objpknk.NhietDo;
                txtha.Text = objpknk.NhomMau;
                txtMach.Text = objpknk.Mach;
                txtNhipTho.Text = objpknk.NhịpTho;
                txtChieuCao.Text = objpknk.ChieuCao;
                txtCanNang.Text = objpknk.CanNang;
                txtBMI.Text = objpknk.Bmi;
                txtKhambenh_HoHap.Text = objpknk.NoikhoaHohap;
                txtKhambenh_TieuHoa.Text = objpknk.NoikhoaTieuhoa;
                txtKhambenh_ThanTietNieuSinhDuc.Text = objpknk.NoikhoaThantietnieusinhduc;
                txtKhambenh_ThanKinh.Text = objpknk.NoikhoaThankinh;
                txtKhambenh_CoXuongKhop.Text = objpknk.NoikhoaCoxuongkhop;
                txtKhambenh_TaiMuiHong.Text = objpknk.NoikhoaTaimuihong;
                txtKhambenh_RangHamMat.Text = objpknk.NoikhoaRanghammat;
                txtKhambenh_Mat.Text = objpknk.NoikhoaMat;
                txtKhambenh_Coquankhac.Text = objpknk.NoikhoaKhac;
            }
            else
            {
                KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                    .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                if (tef != null)
                {
                    txtMach.Text = Utility.sDbnull(tef.Mach);
                    txtNhietDo.Text = Utility.sDbnull(tef.Nhietdo);
                    txtha.Text = Utility.sDbnull(tef.Huyetap);
                    txtNhipTho.Text = Utility.sDbnull(tef.Nhiptho);
                    txtCanNang.Text = Utility.sDbnull(tef.Cannang);
                    txtChieuCao.Text = Utility.sDbnull(tef.Chieucao);
                    tinhBMI();
                }
            }
        }
        VKcbLuotkham objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
        private void SinhMaBenhAn()
        {
            //txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn_NoiTru();
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.BaSinhMaBA(cboLoaiBA.SelectedValue.ToString(), MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput) { MaxMaBenhAN = (String)objOutput; });

            txtMaBenhAn.Text = MaxMaBenhAN;

        }
        void ModifyCommand()
        {
            tabpageTo2.Enabled = tabpageTo3.Enabled = tabpageTo4.Enabled = objLuotkham != null;
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objEmrBa!=null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    objLuotkham.MaLuotkham = THU_VIEN_CHUNG.SinhMaHoSoKhiTimKiem(objLuotkham.MaLuotkham);
            //    if (!KiemTraThongTin()) return;
            //    FillBenhAnByPatientCode();
            //}
        }
       
        private DataTable getChitietCLS()
        {
            int status = 0;
            DataTable temdt = SPs.ClsKetQuaXetNghiem(-1,"",objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, 1, status).GetDataSet().Tables[0];

            return temdt;
        }

        private void cmdXoaBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
               
                objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull( txtIDBenhAn.Text));
                if (objEmrBa == null)
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh án nào để xóa hoặc bệnh án muốn xóa không tồn tại trong hệ thống. Vui lòng gõ lại mã lượt khám để kiểm tra");
                    ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
                    return;
                }
                EmrHosoluutru hosoba = new Select().From(EmrHosoluutru.Schema)
                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                    .And(EmrHosoluutru.Columns.MaBa).IsEqualTo(objEmrBa.MaBa)
                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                    .And(EmrHosoluutru.Columns.IdBenhnhan).IsEqualTo(objEmrBa.IdBenhnhan)
                     .And(EmrHosoluutru.Columns.MaLuotkham).IsEqualTo(objEmrBa.MaLuotkham)
                    .ExecuteSingle<EmrHosoluutru>();
                if (Utility.Int32Dbnull( hosoba.TrangThai,0) == 1)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng nên không thể xóa. Muốn xóa cần quay về trạng thái mở");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 2)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng và đã gửi KHTH phê duyệt nên không thể xóa");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 3)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đã được duyệt bởi KHTH và đưa vào lưu trữ nên không thể xóa");
                    return;
                }
                if (objEmrBa != null && (Utility.Coquyen("kcb_EmrBa_xoa") || globalVariables.UserName == objEmrBa.NguoiTao))
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bệnh án ngoại khoa đi không ?", "Thông báo", true))
                    {
                        using (var Scope = new TransactionScope())
                        {
                            using (var dbScope = new SharedDbConnectionScope())
                            {
                               new Delete().From(EmrBa.Schema)
                                     .Where(EmrBa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                     .And(EmrBa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                     .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                     .Execute();
                              new Delete().From(EmrHosoluutru.Schema)
                                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                    .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                    .Execute();
                              Utility.Log("frm_BenhAn_NoiKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                       
                            Utility.ShowMsg("Bạn xóa bệnh án thành công", "Thông báo");
                            ucThongtinnguoibenh_v31.Refresh();
                            ModifyCommand();
                            
                       

                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền xóa BA(kcb_EmrBa_xoa) hoặc không phải là người tạo Bệnh án");
                    return;

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
         
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //var frm = new frm_TimKiem_BN();
            //frm.ShowDialog();
            //if (frm.b_Cancel)
            //{
            //    objLuotkham.MaLuotkham = Utility.sDbnull(frm.SoHSBA);
            //    FillBenhAnByPatientCode();
            //}
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Into1_Click(object sender, EventArgs e)
        {

            DataTable sub_dtData = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, "-1",-1);
            string reportCode = "BA_NOITRU_TO1";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            THU_VIEN_CHUNG.CreateXML(sub_dtData,  "BA_noitru_khoachuyen.XML");
            noitru_inphieu.BA_noitru_Into1(dtDataBA, sub_dtData, true, reportCode, "");
        }

        private void btnInto2_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO2";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto3_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO3";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto4_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO4";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInVoBA_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_VOBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_noitru_tongketBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void cmdUpdateBNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (objLuotkham != null)
            //{
            //    var frm = new frm_Update_BN();
            //    frm.objLuotkham = objLuotkham;
            //    frm.ShowDialog();
            //    SqlQuery sql = new Select().From<KcbLuotkham>().Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
            //    objLuotkham = sql.ExecuteSingle<KcbLuotkham>();
            //    FillBNById(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            //    FillLanKhamById(objLuotkham);
            //}
        }

        private void cmdLamMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            objEmrBa = null;
            objBenhnhan = null;
            objLuotkham = null;
            m_enAct = action.Insert;
            ucThongtinnguoibenh_v31.txtMaluotkham.Focus();
            ucThongtinnguoibenh_v31.txtMaluotkham.SelectAll();
            ModifyCommand();
        }

        bool _isCounterpart = false; //mục đích dùng để ktra xem quá tình bệnh lý ở tổng kết bệnh án đã chỉnh sửa chưa, nếu chỉnh sửa rồi thì ko cập nhật lại
        private void txtBenhAnQuaTrinhBenhLy_Enter(object sender, EventArgs e)
        {
            //_isCounterpart = txtBenhAnQuaTrinhBenhLy.Text.Trim() == txtTKBAQuaTrinhBenhLy.Text.Trim();
        }

        private void txtBenhAnQuaTrinhBenhLy_TextChanged(object sender, EventArgs e)
        {
            //if (_isCounterpart)
            //{
            //    txtTKBAQuaTrinhBenhLy.Text = txtBenhAnQuaTrinhBenhLy.Text;
            //}
        }

        private void txtBenhAnCacXetNghiem_Enter(object sender, EventArgs e)
        {
            _isCounterpart = txtBenhAnCacXetNghiem.Text.Trim() == txtTKBATTomTatKetQua.Text.Trim();
        }

        private void txtBenhAnCacXetNghiem_TextChanged(object sender, EventArgs e)
        {
            if (_isCounterpart)
            {
                txtTKBATTomTatKetQua.Text = txtBenhAnCacXetNghiem.Text;
            }
        }

        private void txtCDKhiVaoDieuTri_TextChanged(object sender, EventArgs e)
        {
            txtBenhAnBenhChinh.Text = txtCDKhiVaoDieuTri.Text;
        }

        private void txtBenhAnHuongDieuTri_Enter(object sender, EventArgs e)
        {
            //_isCounterpart = txtBenhAnHuongDieuTri.Text.Trim() == txtTKBAHuongDieuTri.Text.Trim();
        }

        private void txtBenhAnHuongDieuTri_TextChanged(object sender, EventArgs e)
        {
            //if (_isCounterpart)
            //{
            //    txtTKBAHuongDieuTri.Text = txtBenhAnHuongDieuTri.Text;
            //}
        }

        private void txtBenhAnTomTatBenhAn_Enter(object sender, EventArgs e)
        {
            _isCounterpart = txtBenhAnTomTatBenhAn.Text.Trim() == txtTKBAQuaTrinhBenhLy.Text.Trim();
        }

        private void txtBenhAnTomTatBenhAn_TextChanged(object sender, EventArgs e)
        {
            if (_isCounterpart)
            {
                txtTKBAQuaTrinhBenhLy.Text = txtBenhAnTomTatBenhAn.Text;
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ctxIn.Show(cmdPrint, new Point(0, cmdPrint.Height));

            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    pnlPrint.Visible = false;
            //}
        }

        private void cmdPrint_MouseHover(object sender, EventArgs e)
        {
            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    pnlPrint.Visible = false;
            //}
        }

        private void cmdPrint_MouseLeave(object sender, EventArgs e)
        {
           
            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    Thread.Sleep(5000);
            //    pnlPrint.Visible = false;
            //}
        }

        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }

        private void txtChieuCao_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }
        void tinhBMI()
        {
            if (txtCanNang.Text.Trim() != string.Empty && txtChieuCao.Text.Trim() != string.Empty) //2 ô có giá trị thì mới tính
            {
                if (txtCanNang.Text.Trim().All(char.IsDigit) && txtChieuCao.Text.Trim().All(char.IsDigit)) //2 ô phải là kiểu số
                {
                    if (Utility.DecimaltoDbnull(txtCanNang.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0) //2 giá trị > 0
                    {
                        decimal bmi = Utility.DecimaltoDbnull(txtCanNang.Text, 0) / (Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100 * Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100);
                        txtBMI.Text = Utility.sDbnull(Math.Round(bmi, 2));
                    }
                }
            }
        }

        private void mnuInVoBA_Click(object sender, EventArgs e)
        {
            InBA(0);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            KcbTomtatBA ttba =new Select().From(KcbTomtatBA.Schema)
                .Where(KcbTomtatBA.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbTomtatBA.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<KcbTomtatBA>();
            if (ttba == null || ttba.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(ttba);
        }

        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            InBA(1);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            InBA(2);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            InBA(3);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            InBA(4);
        }
        private void InBA(int toBA)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                    return;
                }
               
                if (objEmrBa == null || objEmrBa.IdBa <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Bệnh án nội trú trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.EmrBaLaythongtinIn(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA01_EmrBa.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                EmrPhieuravien _phieuravien = new Select().From(EmrPhieuravien.Schema)
               .Where(EmrPhieuravien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
               .And(EmrPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrPhieuravien>();
                NoitruPhieunhapvien _phieunv = new Select().From(NoitruPhieunhapvien.Schema)
               .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
               .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                dtData.TableName = "BA_NOITRU";
                Document doc;
                drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["p102"] = globalVariables.Branch_Name;
                drData["p101"] = globalVariables.ParentBranch_Name;
                drData["p132"] = _phieunv!=null?Utility.FormatDateTime_giophut_ngay_thang_nam(_phieunv.NgayNhapvien, ""):".......... giờ ....... ngày ........./........./.............";//Vào viện

                if (dtkhoanhapvien.Rows.Count > 0)
                {
                    drData["p141"] = Utility.FormatDateTime_giophut_ngay_thang_nam(Convert.ToDateTime(dtkhoanhapvien.Rows[0]["ngay_vaokhoa"]), "");//vào khoa
                    drData["p141_1"] = Utility.sDbnull(dtkhoanhapvien.Rows[0]["so_luong"], "0");
                }
                //REM lại do đã xử lý ở bước fillData trước khi ghi
                //drData["p103"] = drData["p140"];
                //if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                //{
                //    drData["p103"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                //    drData["p104"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                //}
                drData["p128"] = Utility.FormatDateTime(Utility.sDbnull(drData["p128"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["p145_1"] =_phieuravien!=null? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieuravien.NgayRavien, ""):".......... giờ ....... ngày ........./........./.............";//ra viện
                //drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                //drData["SDT_bv"] = globalVariables.Branch_Phone;
                //drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                //drData["Fax_bv"] = globalVariables.Branch_Fax;
                //drData["website_bv"] = globalVariables.Branch_Website;
                //drData["email_bv"] = globalVariables.Branch_Email;
                //drData["ten_phieu"] = "Phiếu khám thai";
                //drData["sngay_kham_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(_pkt.NgayKham, "");
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["sNgaykykinh_cuoi"] = _pkt.NgayDaukykinhcuoi.HasValue ? _pkt.NgayDaukykinhcuoi.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngaydukien_sinh"] = _pkt.NgayDukiensinh.HasValue ? _pkt.NgayDukiensinh.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                //drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");

                List<string> fieldNames = new List<string>();
                string tenToBA = "";
                if (toBA == 1) tenToBA = "BA01_EmrBa_TO1.doc";
                else if (toBA == 0) tenToBA = "BA01_EmrBa_BIA.doc";
                else if (toBA == 2) tenToBA = "BA01_EmrBa_TO2.doc";
                else if (toBA == 3) tenToBA = "BA01_EmrBa_TO3.doc";
                else if (toBA == 4) tenToBA = "BA01_EmrBa_TO4.doc";
                else tenToBA = "BA01_EmrBa.doc";
                string PathDoc = string.Format(AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\{0}", tenToBA);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "EmrBa", objLuotkham.MaLuotkham, Utility.sDbnull(objEmrBa.IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    //Nạp tiền sử sản khoa. Nhảy đến bảng số 3 trong file doc mẫu
                    try
                    {
                        if (toBA == 2 || toBA == 3 || toBA == 4)
                        {
                        }
                        else
                        {
                            int table_idx = 0;
                            string tbl_idx = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA01_EmrBa_TBLIDX.txt";
                            table_idx = Utility.Int32Dbnull(Utility.GetFirstValueFromFile(tbl_idx), 1);
                            Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[table_idx];

                            tab = tab.Rows[14].Cells[1].FirstChild as Aspose.Words.Tables.Table;//(Aspose.Words.Tables.Table)doc.GetChild(NodeType.Table, 0, true);//
                            int idx = 1;
                            foreach (DataRow dr in dtkhoachuyen.Rows)
                            {
                                Aspose.Words.Tables.Row newRow = idx == 1 ? (Aspose.Words.Tables.Row)tab.LastRow : (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);//.Clone(true);
                                newRow.RowFormat.Borders.Shadow = false;
                                newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                                newRow.Cells[0].FirstParagraph.Runs.Clear();
                                newRow.Cells[1].FirstParagraph.Runs.Clear();
                                newRow.Cells[2].FirstParagraph.Runs.Clear();

                                Run r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Size = 10d;
                                r.Font.Bold = false;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["ten_khoanoitru"], "");
                                newRow.Cells[0].FirstParagraph.AppendChild(r);
                                newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Bold = false;
                                r.Font.Size = 10d;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["ngay_vaokhoa"], "");
                                newRow.Cells[1].FirstParagraph.AppendChild(r);
                                newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Bold = false;
                                r.Font.Size = 10d;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["so_luong"], "");
                                newRow.Cells[2].FirstParagraph.AppendChild(r);
                                newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                                if (idx > 1)
                                    tab.AppendChild(newRow);
                                idx += 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            InBA(100);
        }

        private void cmdRefreshChucnangsong_Click(object sender, EventArgs e)
        {

            try
            {
                frm_XemthongtinChucnangsong _XemthongtinChucnangsong = new frm_XemthongtinChucnangsong(objLuotkham, true, 100);
                _XemthongtinChucnangsong._OnSelectMe += _XemthongtinChucnangsong__OnSelectMe;
                _XemthongtinChucnangsong.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void _XemthongtinChucnangsong__OnSelectMe(string mach, string nhietdo, string nhiptho, string huyetap, string chieucao, string cannang, string bmi, string nhommau, string SPO2)
        {
            txtMach.Text = mach;
            txtNhietDo.Text = nhietdo;
            txtNhipTho.Text = nhiptho;
            txtha.Text = huyetap;
            txtChieuCao.Text = chieucao;
            txtCanNang.Text = cannang;
            txtBMI.Text = bmi;
        }
        void DongbothongtinTKBA()
        {
            try
            {
                if (objTkba != null)
                {
                    dtpNgayTKBA.Value = objTkba.NgayTtba.Value;
                    txtTKBAQuaTrinhBenhLy.Text = objTkba.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = objTkba.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = objTkba.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = objTkba.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = objTkba.HuongDieutri;

                    txtNguoiGiaoHoSo.Text = Utility.sDbnull(objTkba.NguoigiaoHoso);
                    txtNguoiNhanHoSo.Text = Utility.sDbnull(objTkba.NguoiNhanhoso);

                    txtB_CTScanner.Text = Utility.sDbnull(objTkba.SotoCt);
                    txtB_Xquang.Text = Utility.sDbnull(objTkba.SotoXquang);
                    txtB_SieuAm.Text = Utility.sDbnull(objTkba.SotoSieuam);
                    txtB_XetNghiem.Text = Utility.sDbnull(objTkba.SotoXetnghiem);
                    txtB_Khac.Text = Utility.sDbnull(objTkba.SotoKhac);

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdSyncTTBA_Click(object sender, EventArgs e)
        {
            DongbothongtinTKBA();
        }

        private void cmdKhoitaoBA_Click(object sender, EventArgs e)
        {
            cmdSave.PerformClick();
        }
    }
}
