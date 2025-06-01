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

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_ChietkhauTrenHoadon : Form
    {
       public decimal TongCKChitiet=0m;
        public decimal TongtienBN=0m;
        public decimal TongCKHoadon=0m;
        public decimal TongtienCK=0m;
        public string ma_ldoCk="";
        public bool m_blnCancel = true;
        public bool ckthem = false;
        GoiDanhsach objCTKM = null;
        public frm_ChietkhauTrenHoadon(GoiDanhsach objCTKM)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.objCTKM = objCTKM;
            InitEvents();
        }
        public frm_ChietkhauTrenHoadon()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_ChietkhauTrenHoadon_FormClosing);
            this.Load += new EventHandler(frm_ChietkhauTrenHoadon_Load);
            this.KeyDown += new KeyEventHandler(frm_ChietkhauTrenHoadon_KeyDown);
            chkBoChitiet.CheckedChanged += new EventHandler(chkBoChitiet_CheckedChanged);
            txtPtramCK.TextChanged += new EventHandler(txtPtramCK_TextChanged);
            txtPtramCK.ValueChanged += new EventHandler(txtPtramCK_ValueChanged);
            txtTienChietkhau.TextChanged += new EventHandler(txtTienChietkhau_TextChanged);
            txtTienChietkhau.ValueChanged += new EventHandler(txtTienChietkhau_ValueChanged);
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            chkAll.CheckStateChanged += chkAll_CheckStateChanged;
            txtLydochietkhau._OnShowData += txtLydochietkhau__OnShowData;
            autoUudai._OnShowData += autoUudai__OnShowData;
        }

        void autoUudai__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(autoUudai.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoUudai.myCode;
                autoUudai.Init();
                autoUudai.SetCode(oldCode);
                autoUudai.Focus();
            }  
        }

        void chkAll_CheckStateChanged(object sender, EventArgs e)
        {
            txtTienChietkhau_ValueChanged(txtTienChietkhau, e);
        }

        void txtLydochietkhau__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtLydochietkhau.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtLydochietkhau.myCode;
                txtLydochietkhau.Init();
                txtLydochietkhau.SetCode(oldCode);
                txtLydochietkhau.Focus();
            } 
        }

        void cmdThemLydochietkhau_Click(object sender, EventArgs e)
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG("LYDOCHIETKHAU");
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydochietkhau.myCode;
                txtLydochietkhau.Init();
                txtLydochietkhau.SetCode(oldCode);
                txtLydochietkhau.Focus();
            } 
        }
        bool AllowChanged = true;
        void txtTienChietkhau_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowChanged) return;
            tinhlaitienckhoadon();
            
            txtTongtienchietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTienChietkhau.Text) + (chkBoChitiet.Checked ? 0m : Utility.DecimaltoDbnull(txtCKChitiet.Text, 0m)));
            txtTongtienCuoicung.Text = (TongtienBN - Utility.DecimaltoDbnull(txtTongtienchietkhau.Text)).ToString();
        }

        void txtPtramCK_ValueChanged(object sender, EventArgs e)
        {
            //if (Utility.DecimaltoDbnull(txtPtramCK.Text) > 0)
            //{
            //    txtTienChietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTiensauCK.Text) *
            //                                             Utility.DecimaltoDbnull(txtPtramCK.Text) / 100);
            //}
            //txtTongtienchietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTienChietkhau.Text) + (chkBoChitiet.Checked ? 0m : Utility.DecimaltoDbnull(txtCKChitiet.Text, 0m)));
        }

        void txtTienChietkhau_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        void chkBoChitiet_CheckedChanged(object sender, EventArgs e)
        {
            txtTienChietkhau_ValueChanged(txtTienChietkhau, e);
            //if (chkBoChitiet.Checked)
            //{
            //    txtTiensauCK.Text = TongtienBN.ToString();
            //    txtTongtienchietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTienChietkhau.Text) );
                
            //}
            //else
            //{
            //    txtTiensauCK.Text = (TongtienBN - TongCKChitiet).ToString();
            //    txtTongtienchietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTienChietkhau.Text) + (chkBoChitiet.Checked ? 0m : Utility.DecimaltoDbnull(txtCKChitiet.Text, 0m)));
               
            //}
            //txtTongtienCuoicung.Text = (TongtienBN - Utility.DecimaltoDbnull(txtTongtienchietkhau.Text)).ToString();
        }

        void txtPtramCK_TextChanged(object sender, EventArgs e)
        {
            tinhlaitienckhoadon();
            //txtTienChietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTiensauCK.Text) *
            //                                         Utility.DecimaltoDbnull(txtPtramCK.Text) / 100);

            txtTongtienchietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTienChietkhau.Text) + (chkBoChitiet.Checked ? 0m : Utility.DecimaltoDbnull(txtCKChitiet.Text, 0m)));
            txtTongtienCuoicung.Text = (TongtienBN - Utility.DecimaltoDbnull(txtTongtienchietkhau.Text)).ToString();
        }
        void tinhlaitienckhoadon()
        {
            AllowChanged = false;
            decimal ckchitiet = chkBoChitiet.Checked ? 0m : Utility.DecimaltoDbnull(txtCKChitiet.Text, 0m);
            if (chkAll.Checked)
            {
                txtTienChietkhau.Value = (txtPtramCK.Value * TongtienBN)/100;
            }
            else
            {
                txtTienChietkhau.Value =( txtPtramCK.Value * (TongtienBN - ckchitiet))/100;
            }
            AllowChanged = true;
        }
        void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void frm_ChietkhauTrenHoadon_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_ChietkhauTrenHoadon_Load(object sender, EventArgs e)
        {
            try
            {
                setProperties();
                txtLydochietkhau.Init();
                autoUudai.Init();
                txtCKChitiet.Text = TongCKChitiet.ToString();
                txtTongtienBN.Text = TongtienBN.ToString();
                txtTiensauCK.Text = (TongtienBN - TongCKChitiet).ToString();
                txtTongtienCuoicung.Text = (TongtienBN - TongCKChitiet).ToString();
                txtTongtienchietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTienChietkhau.Text) + (chkBoChitiet.Checked ? 0m : Utility.DecimaltoDbnull(txtCKChitiet.Text, 0m)));
                chkAll.Enabled = chkBoChitiet.Enabled = TongCKChitiet > 0;
                if (ckthem)
                {
                    txtPtramCK.Focus();
                    txtPtramCK.Select();
                }
                else
                {
                    txtLydochietkhau.Focus();
                    txtLydochietkhau.Select();
                }
                if(objCTKM!=null && Utility.Bool2Bool( objCTKM.KhuyenmaiTong) )
                {
                    if (objCTKM.KieuChietkhau == "%")
                        txtPtramCK.Value = objCTKM.SoTien;
                    else
                        txtTienChietkhau.Value = objCTKM.SoTien;
                    txtLydochietkhau.Text = objCTKM.TenGoi;
                }    
            }
            catch
            {
            }
            finally
            {
            }
        }
        private void setProperties()
        {
            try
            {
                foreach (Control control in this.Controls)
                {
                    if (control is EditBox)
                    {
                        var _item = new EditBox();
                        _item = ((EditBox)(control));
                        _item.Clear();
                        _item.ReadOnly = true;
                        _item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                        _item.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
               
            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }

        void frm_ChietkhauTrenHoadon_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (!isValidData()) return;
                TongCKHoadon = Utility.DecimaltoDbnull(txtTienChietkhau.Value, 0);
                TongtienCK = Utility.DecimaltoDbnull(txtTongtienchietkhau.Text, 0);
                ma_ldoCk = txtLydochietkhau.myCode;
                m_blnCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi nhấn nút Ghi:\n" + ex.Message);
                throw;
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
     
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (Utility.DecimaltoDbnull(txtPtramCK.Value, 0m) < 0 || Utility.DecimaltoDbnull(txtPtramCK.Value, 0m) > 100)
            {
                Utility.SetMsg(lblMsg, "% chiết khấu chỉ được cho phép trong khoảng từ 0-100. Mời bạn nhập lại", true);
                txtPtramCK.Focus();
                txtPtramCK.Select();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtTienChietkhau.Value, 0m) > Utility.DecimaltoDbnull(txtTongtienBN.Text, 0m))
            {
                Utility.SetMsg(lblMsg, "Tổng tiền chiết khấu không được phép vượt quá tổng tiền bệnh nhân phải trả. Mời bạn nhập lại", true);
                txtTienChietkhau.Focus();
                txtTienChietkhau.Select();
                return false;
            }
            //if (txtLydochietkhau.myCode == "-1" || Utility.DoTrim( txtLydochietkhau.myCode) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn cần nhập lý do chiết khấu", true);
            //    txtLydochietkhau.Focus();
            //    txtLydochietkhau.SelectAll();
            //    return false;
            //}
            if (TongCKChitiet > 0 && chkBoChitiet.Checked)
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy toàn bộ tiền chiết khấu chi tiết ({0}) cho các dịch vụ đang chọn thanh toán hay không?", "Xác nhận hủy chiết khấu chi tiết", true))
                {
                    chkBoChitiet.Focus();
                    return false;
                }
            }
            return true;
        }

        private void txtPtramCK_ValueChanged_1(object sender, EventArgs e)
        {

        }
       
    }
}
