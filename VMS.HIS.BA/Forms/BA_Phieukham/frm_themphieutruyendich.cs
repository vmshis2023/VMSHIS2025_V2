using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.EditControls;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_themphieutruyendich : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
        KcbLuotkham objLuotkham;
        public bool mv_blnCallFromMenu = true;
        public bool IsChanged = false;

        public action m_enAct = action.Insert;
        public string ma="";
        public string ten = "";
        public bool m_blnCancel = true;
        string _name = "";
        public NoitruPhieudichtruyen objPhieu = null;
        public frm_themphieutruyendich()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += UcThongtinnguoibenh_emr_basic1__OnEnterMe;
           
            InitEvents();
        }

        private void UcThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            this.objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
            if(objLuotkham!=null)
            {
                DataTable dtKhoaphong = SPs.CommonLaydanhsachKhoadieutriCuanguoibenh(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_khoanoitru, dtKhoaphong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
            }    
           if(m_enAct==action.Insert) LaythongtinChandoan();

        }

        public frm_themphieutruyendich(string loaidanhmuc,string title1,string title2,string _name,string ngaythuchien)
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           
            this.Text = title1;
          
            this._name = _name;
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_themphieutruyendich_FormClosing);
            this.Load += new EventHandler(frm_themphieutruyendich_Load);
            this.KeyDown += new KeyEventHandler(frm_themphieutruyendich_KeyDown);
          
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
        }

        void txtDmucchung__OnShowData()
        {
           
        }

        void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void frm_themphieutruyendich_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_themphieutruyendich_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
               
                if (m_enAct == action.Update)
                {
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objPhieu.MaLuotkham;
                    ucThongtinnguoibenh_emr_basic1.Refresh();
                    SetData4Update();
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
        void SetData4Update()
        {
            if (objPhieu != null)
            {
                txtId.Text = objPhieu.IdPhieu.ToString();
                cbo_khoanoitru.SelectedValue =objPhieu.IdKhoadieutri;
                txtBuong.Text = objPhieu.Buong;
                txtGiuong.Text = objPhieu.Giuong;
                txt_chandoan.Text = objPhieu.ChanDoan;
            }
        }
        void LaythongtinChandoan()
        {
            try
            {
                string chan_doan = "";

                Utility.GetChandoanHienThiFormDieuTriNoitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, DateTime.Now.AddYears(100), ref chan_doan, false);
                txt_chandoan.Text = chan_doan;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        void frm_themphieutruyendich_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (!isValidData()) return;
                if (m_enAct == action.Insert)
                {
                    objPhieu = new NoitruPhieudichtruyen();
                    objPhieu.NgayTao = globalVariables.SysDate;
                    objPhieu.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    objPhieu.NgaySua = globalVariables.SysDate;
                    objPhieu.NguoiSua = globalVariables.UserName;
                }
                objPhieu.IdBenhnhan = objLuotkham.IdBenhnhan;
                objPhieu.MaLuotkham = objLuotkham.MaLuotkham;
                objPhieu.IdKhoadieutri =Utility.Int32Dbnull( cbo_khoanoitru.SelectedValue);
                objPhieu.Khoa = cbo_khoanoitru.Text;
                objPhieu.Buong = txtBuong.Text;
                objPhieu.Giuong = txtGiuong.Text;
                objPhieu.ChanDoan = txt_chandoan.Text;
                objPhieu.Save();
                if (_OnCreated != null) _OnCreated(objPhieu.IdPhieu, m_enAct);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                m_blnCancel = false;
                if (chkContine.Checked && m_enAct==action.Insert)
                {
                   
                    txtBuong.Clear();
                    txtGiuong.Clear();
                    txt_chandoan.Clear();
                    cbo_khoanoitru.Focus();
                }
                else
                    this.Close();
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi Lưu phiếu truyền dịch:\n" + ex.Message);
               
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
     
        private bool isValidData()
        {
            errorProvider1.Clear();
            string Msg = "";
            Utility.SetMsg(lblMsg, "", true);
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham == null)
            {
                Msg = "Bạn phải chọn Người bệnh cần lập phiếu truyền dịch";
                errorProvider1.SetError(ucThongtinnguoibenh_emr_basic1.txtMaluotkham, Msg);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                return false;
            }    
           
            if (Utility.Int32Dbnull( cbo_khoanoitru.SelectedValue,-1)<=0)
            {
                Msg = "Bạn phải chọn Khoa thực hiện truyền dịch";
                errorProvider1.SetError(cbo_khoanoitru, Msg);
                cbo_khoanoitru.Focus();
                return false;
            }
            return true;
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
