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

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class Frm_CapNhat_Thongtin_Thue : Form
    {
        private string _patientCode = "";
        private int _patientId = -1;
        public Frm_CapNhat_Thongtin_Thue(string patientCode, int patientID)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            _patientCode = patientCode;
            _patientId = patientID;
        }
        public Frm_CapNhat_Thongtin_Thue()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }

        private void txtmalankham_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Frm_CapNhat_Thongtin_Thue_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_patientCode))
            {
                txtmalankham.Text = _patientCode;
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                  .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(_patientCode)
                  .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(_patientId));
                var objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objPatientExam != null)
                {
                    txtmabenhnhan.Text = _patientId.ToString();
                    KcbDanhsachBenhnhan objPatientInfo = KcbDanhsachBenhnhan.FetchByID(_patientId);
                    if (objPatientInfo != null)
                    {
                        txthovaten.Text = Utility.sDbnull(objPatientInfo.TenBenhnhan);
                        txtnamsinh.Text = Utility.sDbnull(objPatientInfo.NamSinh.ToString());
                        txtgioitinh.Text = objPatientInfo.GioiTinh;
                        txtdiachi.Text = Utility.sDbnull(objPatientInfo.DiaChi);
                    }
                    txtmasothue.Text = Utility.sDbnull(objPatientExam.ThueMaso,"");
                    txttencongty.Text = Utility.sDbnull(objPatientExam.ThueCongty, "");
                    txtsotaikhoan.Text = Utility.sDbnull(objPatientExam.ThueSotaikhoan, "");
                    txtThueDiaChi.Text = Utility.sDbnull(objPatientExam.ThueDiachi, "");
                }
            }
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cmdCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.ThueMaso).EqualTo(txtmasothue.Text.Trim())
                    .Set(KcbLuotkham.Columns.ThueCongty).EqualTo(txttencongty.Text.Trim())
                    .Set(KcbLuotkham.Columns.ThueSotaikhoan).EqualTo(txtsotaikhoan.Text.Trim())
                    .Set(KcbLuotkham.Columns.ThueDiachi).EqualTo(txtThueDiaChi.Text.Trim())
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(_patientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(_patientId).Execute();
                Utility.ShowMsg("Cập nhật thành công Thông tin Thuế cho người bệnh");
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thành công Thông tin Thuế cho người bệnh: {0}, mã bệnh nhân : {1}", _patientCode, _patientId), newaction.Update, "UI");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
}
