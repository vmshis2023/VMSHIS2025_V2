using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_KhoitaoBA : Form
    {
        KcbLuotkham objLuotkham;
        string ten_nguoibenh = "";
        public frm_KhoitaoBA(KcbLuotkham objLuotkham, string ten_nguoibenh)
        {
            InitializeComponent();
           
            this.objLuotkham = objLuotkham;
            this.ten_nguoibenh = ten_nguoibenh;
            this.Shown += Frm_TiensuSanphukhoa_Shown;
            this.KeyDown += frm_KhoitaoBA_KeyDown;
            this.Load += Frm_KhoitaoBA_Load;
        }

        private void Frm_KhoitaoBA_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgayBA.Value = DateTime.Now;
                txtBSlamBA.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                DataTable dtData =
                   new Select().From(DmucChung.Schema)
                       .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                       .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                       .OrderAsc(DmucChung.Columns.SttHthi)
                       .ExecuteDataSet().Tables[0];
                if (dtData.Rows.Count > 1)
                {
                    DataRow dr = dtData.NewRow();
                    dr[DmucChung.Columns.Ten] = "---Chọn loại BA---";
                    dr[DmucChung.Columns.Ma] = "-1";

                    dtData.Rows.InsertAt(dr, 0);
                }
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN");
                //fill trạng thái của người bệnh
                EmrBa objEmrBa = EmrBa.FetchByID(objLuotkham.IdBa);
                if(objEmrBa != null)
                {
                    cboLoaiBA.SelectedValue = objEmrBa.LoaiBa;
                    dtpNgayBA.Value = objEmrBa.NgaylamBa.Value;
                    txtIDBenhAn.Text = Utility.sDbnull(objEmrBa.IdBa);
                    txtMaBenhAn.Text = Utility.sDbnull(objEmrBa.MaBa);
                    txtBSlamBA.SetId(objEmrBa.IdBacsiLamBA);
                    lblStatus.Text =string.Format( "Người bệnh {0} đã được khởi tạo Bệnh án",ten_nguoibenh);
                    cmdExit.Focus();
                }
                else
                {
                    cmdSave.Enabled = true;
                    cmdSave.Focus();
                }    
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frm_KhoitaoBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ProcessTabKey(true);
        }

        private void Frm_TiensuSanphukhoa_Shown(object sender, EventArgs e)
        {
          
            
        }

       
       

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        EmrBa objEmrBa = new EmrBa();
                        SinhMaBenhAn();
                        objEmrBa.MaBa = Utility.sDbnull(txtMaBenhAn.Text);
                        objEmrBa.NguoiTao = globalVariables.UserName;
                        objEmrBa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        objEmrBa.NgaylamBa = dtpNgayBA.Value;
                        objEmrBa.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                        objEmrBa.MabacsiLamBA = txtBSlamBA.MyCode;
                        objEmrBa.IdBacsiLamBA = Utility.Int16Dbnull(txtBSlamBA.MyID);
                        objEmrBa.TenbacsiLamBA = txtBSlamBA.Text;
                        objEmrBa.Save();
                        new Update(KcbLuotkham.Schema)
                                   .Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(objEmrBa.IdBacsiDieutri)
                                  .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                                  .Set(KcbLuotkham.Columns.LoaiBenhAn).EqualTo(objEmrBa.LoaiBa)
                                        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                        .Execute();
                        EmrHosoluutru hsba = new EmrHosoluutru();
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
                        if (hsba != null)
                        {
                            hsba.IdBa = objEmrBa.IdBa;
                            hsba.Save();
                        }
                    }
                    scope.Complete();
                }
                //Thực hiện hàm refresh EMR
                int num = 0;
                StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "", 1, num);
               if( Utility.AcceptQuestion(string.Format("Khởi tạo Bệnh án {0} cho người bệnh {1} thành công.\nBạn có thể xem thông tin các giấy tờ, phiếu khám của người bệnh đã phát sinh trước đó trên hồ sơ Bệnh án Điện tử EMR.\nCác phiếu và giấy tờ phát sinh sau khi khởi tạo Bệnh án sẽ tự động được gắn vào các gáy của hồ sơ EMR. Bạn có muốn xem thông tin EMR để kiểm tra luôn các hồ sơ, giấy tờ của người bệnh ngay bây giờ không?", cboLoaiBA.Text, ten_nguoibenh)))
                {
                    frm_Emr _Emr = new frm_Emr();
                    _Emr.isAutoLoad = true;
                    _Emr.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                    _Emr.ShowDialog();
                }    
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void SinhMaBenhAn()
        {
            //txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn_NoiTru();
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.EmrBaSinhMaBA(cboLoaiBA.SelectedValue.ToString(), MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate (object objOutput) { MaxMaBenhAN = (String)objOutput; });

            txtMaBenhAn.Text = MaxMaBenhAN;

        }

    }
}
