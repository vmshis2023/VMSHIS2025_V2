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
using VNS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ChooseCapPhatNoiTru : Form
    {
        private DataTable m_dtKhoa=new DataTable();
        public string thamso = "KHOA";
        private  DataTable m_dtKhoThuoc=new DataTable();
        public DataTable p_PhieuCapPhat=new DataTable();
        public frm_ChooseCapPhatNoiTru()
        {
            InitializeComponent();
            cboDoiTuong.Enabled = globalVariables.gv_TongHopDonThuocMaDoiTuong;
            //   cboLoaiPhieu.SelectedIndex = 0;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_ChooseCapPhatNoiTru_Load(object sender, EventArgs e)
        {
            DataBinding.BindData(cboDoiTuong,LoadDataCommon.CommonBusiness.LayThongTin_DoiTuong(),LObjectType.Columns.ObjectTypeCode,LObjectType.Columns.ObjectTypeName);
            if (thamso.Equals("KHOA"))
            {
                if (globalVariables.IsAdmin)
                    m_dtKhoa = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTru();
                else
                {
                    m_dtKhoa = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTruTheoKhoa(globalVariables.DepartmentID);
                }
            }
            else
            {
                m_dtKhoa = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTruTheoKhoa(globalVariables.DepartmentID);
            }
          
            DataBinding.BindData(cboKhoa,m_dtKhoa,LDepartment.Columns.DepartmentId,LDepartment.Columns.DepartmentName);
            LoadKhoTheoVatTuThuoc();

        }

        private void LoadKhoTheoVatTuThuoc()
        {
            if (radPhieuThuoc.Checked)
                m_dtKhoThuoc = LoadDataCommon.CommonLoadDuoc.LayThongTinKhoKeNoiTruThuoc(Utility.sDbnull(cboDoiTuong.SelectedValue));
            else
            {
                m_dtKhoThuoc =
                    LoadDataCommon.CommonLoadDuoc.LayThongTinKhoKeNoiTruVatTu(Utility.sDbnull(cboDoiTuong.SelectedValue));
            }
            DataBinding.BindData(cboKho, m_dtKhoThuoc, DKho.Columns.IdKho, DKho.Columns.TenKho);
        }

        private void radPhieuThuoc_CheckedChanged(object sender, EventArgs e)
        {
            LoadKhoTheoVatTuThuoc();
        }

        private void radPhieuLinhVatTu_CheckedChanged(object sender, EventArgs e)
        {
            LoadKhoTheoVatTuThuoc();
        }

        private void cmdChon_Click(object sender, EventArgs e)
        {
            frm_AddPhieuCapPhatCT frm=new frm_AddPhieuCapPhatCT();
            frm.txtID_CAPPHAT.Text = "Tự sinh";
            frm.p_phieuCapPhatThuoc = p_PhieuCapPhat;
            frm.me_Action = action.Insert;
            if (globalVariables.gv_TongHopDonThuocMaDoiTuong)
            {
                frm.madoituong = Utility.sDbnull(cboDoiTuong.SelectedValue);
            }
            else
            {
                frm.madoituong = "TATCA";
            }
            frm.idKhoaLinh = Utility.Int32Dbnull(cboKhoa.SelectedValue);
            frm.id_khoXuat = Utility.Int32Dbnull(cboKho.SelectedValue);
            frm.loaiphieu = radPhieuThuoc.Checked ? Utility.sDbnull(radPhieuThuoc.Tag) : Utility.sDbnull(radPhieuLinhVatTu.Tag);
            frm.IsPhieuBoSung = radLinhBoSung.Checked;
            frm.ShowDialog();
        }
    }
}
