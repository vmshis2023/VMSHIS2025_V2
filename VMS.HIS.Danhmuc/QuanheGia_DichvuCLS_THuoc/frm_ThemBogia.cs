using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;

using VNS.Libs;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using System.IO;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_ThemBogia : Form
    {
        public delegate void OnCreated(int id, newaction m_enAct);
        public event OnCreated _OnCreated;
        public DmucBogiadichvu _objBogia;
        public newaction m_enAct = newaction.Insert;
        public bool blnCancel = true;
        public string KTH = "ALL";
        public frm_ThemBogia()
        {
            InitializeComponent();
            this.Load += new EventHandler(frm_ThemBogia_Load);
            this.KeyDown += new KeyEventHandler(frm_ThemBogia_KeyDown);
            dtpFrom.Value = dtpTo.Value = DateTime.Now;
        }

      

        void frm_ThemBogia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        void frm_ThemBogia_Load(object sender, EventArgs e)
        {
            if (m_enAct == newaction.Update)
            {
                if (_objBogia != null)
                {
                    txtId.Text = Utility.sDbnull(_objBogia.IdBogia);
                    txtMa.Text = Utility.sDbnull(_objBogia.MaBogia);
                    txtTenbogia.Text = Utility.sDbnull(_objBogia.TenBogia);
                    dtpFrom.Value = _objBogia.NgayBatdau;
                    dtpTo.Value = _objBogia.NgayKetthuc;
                    chkHieuluc.Checked = Utility.Byte2Bool(_objBogia.TrangThai);
                    txtMotathem.Text = Utility.sDbnull(_objBogia.MotaThem);
                }
                else
                {
                    Utility.ShowMsg("Không xác định được bộ giá bạn vừa chọn. Vui lòng nhấn OK để kết thúc");
                    return;
                }
            }
            else
            {
                txtId.Text = "Tự sinh";
                txtMa.Focus();
            }
               
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string oldInfor = "";
                if (Utility.sDbnull(txtMa.Text, "").Length <= 0)
                {
                    Utility.ShowMsg("Cần nhập mã bộ giá BHYT");
                    txtMa.Focus();
                }
                if (Utility.sDbnull(txtTenbogia.Text, "").Length <= 0)
                {
                    Utility.ShowMsg("Cần nhập tên bộ giá BHYT");
                    txtTenbogia.Focus();
                }
                if (dtpFrom.Value > dtpTo.Value)
                {
                    Utility.ShowMsg("Ngày hiệu lực đến phải >= ngày hiệu lực từ");
                    dtpTo.Focus();
                    return;
                }
                SqlQuery sql;
                //Kiểm tra xem có vi phạm phạm vi hiệu lực giữa các bộ giá
                DataTable dtCheck = SPs.QheGiadoituongClsTimdanhsachbogia(-1, "-1", "-1",new DateTime(1900,1,1)).GetDataSet().Tables[0];
                
                if (_objBogia == null)
                {
                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        DmucBogiadichvu objBogia = DmucBogiadichvu.FetchByID(Utility.Int32Dbnull(dr[DmucBogiadichvu.Columns.IdBogia]));
                        if ((dtpFrom.Value.Date >= objBogia.NgayBatdau && dtpFrom.Value.Date <= objBogia.NgayKetthuc) || (dtpTo.Value.Date >= objBogia.NgayBatdau && dtpTo.Value.Date <= objBogia.NgayKetthuc))
                        {
                            Utility.ShowMsg(string.Format("Ngày hiệu lực của Bộ giá BHYT mới: {0}-{1} không được phép nằm trong khoảng ngày hiệu lực của một trong các bộ giá cũ ({2}): {3}-{4}", dtpFrom.Text, dtpTo.Text, objBogia.TenBogia, objBogia.NgayBatdau.ToString("dd/MM/yyyy"), objBogia.NgayKetthuc.ToString("dd/MM/yyyy")));
                            return;
                        }
                        if (dtpFrom.Value.Date >= objBogia.NgayBatdau && dtpFrom.Value.Date <= objBogia.NgayKetthuc)
                        {
                            Utility.ShowMsg(string.Format("Ngày Bắt đầu của bộ giá BHYT mới: {0} không được phép nằm trong khoảng ngày hiệu lực của một trong các bộ giá cũ: {1}-{2}", dtpFrom.Text, objBogia.NgayBatdau.ToString("dd/MM/yyyy"), objBogia.NgayKetthuc.ToString("dd/MM/yyyy")));
                            return;
                        }
                        if (dtpTo.Value.Date >= objBogia.NgayBatdau && dtpTo.Value.Date <= objBogia.NgayKetthuc)
                        {
                            Utility.ShowMsg(string.Format("Ngày Kết thúc của bộ giá BHYT mới: {0} không được phép nằm trong khoảng ngày hiệu lực của một trong các bộ giá cũ({1}): {2}-{3}", dtpFrom.Text, objBogia.TenBogia, objBogia.NgayBatdau.ToString("dd/MM/yyyy"), objBogia.NgayKetthuc.ToString("dd/MM/yyyy")));
                            return;
                        }
                    }
                    sql = new Select().From(DmucBogiadichvu.Schema)
                  .Where(DmucBogiadichvu.Columns.MaBogia).IsEqualTo(Utility.DoTrim(txtMa.Text));
                    if (sql.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg(string.Format("Đã tồn tại Bộ giá có mã = {0}. Mời bạn kiểm tra lại", Utility.DoTrim(txtMa.Text)));
                        txtMa.Focus();
                        return;
                    }
                    sql = new Select().From(DmucBogiadichvu.Schema)
                  .Where(DmucBogiadichvu.Columns.TenBogia).IsEqualTo(Utility.DoTrim(txtTenbogia.Text));
                    if (sql.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg(string.Format("Đã tồn tại Bộ giá có tên = {0}. Mời bạn kiểm tra lại", Utility.DoTrim(txtTenbogia.Text)));
                        txtTenbogia.Focus();
                        return;
                    }
                    _objBogia = new DmucBogiadichvu();
                    _objBogia.NgayTao = DateTime.Now;
                    _objBogia.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        DmucBogiadichvu objBogia = DmucBogiadichvu.FetchByID(Utility.Int32Dbnull(dr[DmucBogiadichvu.Columns.IdBogia]));
                        if (objBogia.IdBogia != _objBogia.IdBogia)//Check các bộ giá khác bộ đang update
                        {
                            if ((dtpFrom.Value.Date >= objBogia.NgayBatdau && dtpFrom.Value.Date <= objBogia.NgayKetthuc) || (dtpTo.Value.Date >= objBogia.NgayBatdau && dtpTo.Value.Date <= objBogia.NgayKetthuc))
                            {
                                Utility.ShowMsg(string.Format("Ngày hiệu lực của Bộ giá BHYT đang sửa: {0}-{1} không được phép nằm trong khoảng ngày hiệu lực của một trong các bộ giá cũ ({2}): {3}-{4}", dtpFrom.Text, dtpTo.Text, objBogia.TenBogia, objBogia.NgayBatdau.ToString("dd/MM/yyyy"), objBogia.NgayKetthuc.ToString("dd/MM/yyyy")));
                                return;
                            }
                            if (dtpFrom.Value.Date >= objBogia.NgayBatdau && dtpFrom.Value.Date <= objBogia.NgayKetthuc)
                            {
                                Utility.ShowMsg(string.Format("Ngày Bắt đầu của bộ giá BHYT đang sửa: {0} không được phép nằm trong khoảng ngày hiệu lực của một trong các bộ giá cũ: {1}-{2}", dtpFrom.Text, objBogia.NgayBatdau.ToString("dd/MM/yyyy"), objBogia.NgayKetthuc.ToString("dd/MM/yyyy")));
                                return;
                            }
                            if (dtpTo.Value.Date >= objBogia.NgayBatdau && dtpTo.Value.Date <= objBogia.NgayKetthuc)
                            {
                                Utility.ShowMsg(string.Format("Ngày Kết thúc của bộ giá BHYT đang sửa: {0} không được phép nằm trong khoảng ngày hiệu lực của một trong các bộ giá cũ({1}): {2}-{3}", dtpFrom.Text, objBogia.TenBogia, objBogia.NgayBatdau.ToString("dd/MM/yyyy"), objBogia.NgayKetthuc.ToString("dd/MM/yyyy")));
                                return;
                            }
                        }
                    }

                    oldInfor = string.Format("{0} bộ giá Id={1},Mã={2}, Tên={3}, từ ngày: {4} đến ngày: {5}", m_enAct == newaction.Update ? "Cập nhật" : "Thêm mới", _objBogia.IdBogia, _objBogia.MaBogia, _objBogia.TenBogia, _objBogia.NgayBatdau, _objBogia.NgayKetthuc);
                    sql = new Select().From(DmucBogiadichvu.Schema)
                    .Where(DmucBogiadichvu.Columns.MaBogia).IsEqualTo(Utility.DoTrim(txtMa.Text))
                    .And(DmucBogiadichvu.Columns.IdBogia).IsNotEqualTo(_objBogia.IdBogia);
                    if (sql.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg(string.Format("Đã tồn tại Bộ giá có mã = {0}. Mời bạn kiểm tra lại", Utility.DoTrim(txtMa.Text)));
                        txtMa.Focus();
                        return;
                    }
                    sql = new Select().From(DmucBogiadichvu.Schema)
                    .Where(DmucBogiadichvu.Columns.TenBogia).IsEqualTo(Utility.DoTrim(txtTenbogia.Text))
                    .And(DmucBogiadichvu.Columns.IdBogia).IsNotEqualTo(_objBogia.IdBogia);
                    if (sql.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg(string.Format("Đã tồn tại Bộ giá có tên = {0}. Mời bạn kiểm tra lại", Utility.DoTrim(txtTenbogia.Text)));
                        txtTenbogia.Focus();
                        return;
                    }
                    _objBogia.MarkOld();
                    _objBogia.NgaySua = DateTime.Now;
                    _objBogia.NguoiSua = globalVariables.UserName;
                }
                _objBogia.MaBogia = Utility.sDbnull(txtMa.Text);
                _objBogia.TenBogia= Utility.sDbnull(txtTenbogia.Text);
                _objBogia.NgayBatdau = dtpFrom.Value;
                _objBogia.NgayKetthuc = dtpTo.Value;
                _objBogia.TrangThai = Utility.Bool2byte(chkHieuluc.Checked);
                _objBogia.MotaThem = Utility.sDbnull(txtMotathem.Text);
                _objBogia.Save();
                if (_OnCreated != null) _OnCreated(_objBogia.IdBogia, m_enAct);
                blnCancel = false;
                string newInfor = string.Format("{0} bộ giá Id={1},Mã={2}, Tên={3}, từ ngày: {4} đến ngày: {5}", m_enAct == newaction.Update ? "Cập nhật" : "Thêm mới", _objBogia.IdBogia, _objBogia.MaBogia, _objBogia.TenBogia, _objBogia.NgayBatdau, _objBogia.NgayKetthuc);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Thông tin cũ {0} -  Thông tin mới {1} ", oldInfor, newInfor), m_enAct, this.GetType().Assembly.ManifestModule.Name);
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

        private void CmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
