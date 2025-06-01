using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.UCs;
using Janus.Windows.EditControls;
using VNS.HIS.UI.DANHMUC;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_KCB_Lapphieuhen_Tiemchung : Form
    {
        DataSet dsData = new DataSet();
        public KcbLuotkham objLuotkham = null;
        public KcbDanhsachBenhnhan objBN = null;
        bool AllowedChangedEvents = false;
        bool quyencapnhatketqua = false;
        bool quyen_chonmui_hentiemchung = false;
        public frm_KCB_Lapphieuhen_Tiemchung()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_KCB_Lapphieuhen_Tiemchung_Load;
            this.KeyDown += frm_KCB_Lapphieuhen_Tiemchung_KeyDown;
            chkMuithu1.CheckedChanged += _CheckedChanged;
            chkMuithu2.CheckedChanged += _CheckedChanged;
            chkMuithu3.CheckedChanged += _CheckedChanged;
            chkMuithu4.CheckedChanged += _CheckedChanged;
            chkMuithu5.CheckedChanged += _CheckedChanged;
            cmdSave.Click += cmdSave_Click;
            cmdExit.Click += cmdExit_Click;
            cmdPrint.Click += cmdPrint_Click;

            grdList.SelectionChanged += grdList_SelectionChanged;
            txtDichvuTiemchung._OnEnterMe += txtDichvuTiemchung__OnEnterMe;
            txtNguoitiem1._OnEnterMe += txtNguoitiem1__OnEnterMe;
            txtNguoitiem2._OnEnterMe += txtNguoitiem2__OnEnterMe;
            txtNguoitiem3._OnEnterMe += txtNguoitiem3__OnEnterMe;
            txtNguoitiem4._OnEnterMe += txtNguoitiem4__OnEnterMe;
            txtNguoitiem5._OnEnterMe += txtNguoitiem5__OnEnterMe;
            dtpNgayhen1.ValueChanged += datetime_ValueChanged;
            dtpNgayhen2.ValueChanged += datetime_ValueChanged;
            dtpNgayhen3.ValueChanged += datetime_ValueChanged;
            dtpNgayhen4.ValueChanged += datetime_ValueChanged;
            dtpNgayhen5.ValueChanged += datetime_ValueChanged;

            dtpNgaytiem1.ValueChanged += datetime_ValueChanged;
            dtpNgaytiem2.ValueChanged += datetime_ValueChanged;
            dtpNgaytiem3.ValueChanged += datetime_ValueChanged;
            dtpNgaytiem4.ValueChanged += datetime_ValueChanged;
            dtpNgaytiem5.ValueChanged += datetime_ValueChanged;

            mnuDelete.Click += mnuDelete_Click;
            txtDichvuTiemchung._OnShowData += txtDichvuTiemchung__OnShowData;
            chkMuihen.CheckedChanged += chkMuihen_CheckedChanged;

            cmdDatiem1.Click += Datiem_Click;
            cmdDatiem2.Click += Datiem_Click;
            cmdDatiem3.Click += Datiem_Click;
            cmdDatiem4.Click += Datiem_Click;
            cmdDatiem5.Click += Datiem_Click;

            cmdChuatiem1.Click += Chuatiem_Click;
            cmdChuatiem2.Click += Chuatiem_Click;
            cmdChuatiem3.Click += Chuatiem_Click;
            cmdChuatiem4.Click += Chuatiem_Click;
            cmdChuatiem5.Click += Chuatiem_Click;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;


        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string fullCode = Utility.AutoFullPatientCode(Utility.DoTrim(txtMaluotkham.Text));
                objLuotkham = KcbLuotkham.FetchByID(fullCode);
                txtMaluotkham.Text = fullCode;
                if (objLuotkham == null)
                {
                    this.Text = "Mời bạn nhập lại mã lượt khám đúng để tìm Bệnh nhân cần hẹn tiêm chủng";
                    dsData.Clear();
                    txtNguoitiem1.SetId(-1);
                    txtNguoitiem2.SetId(-1);
                    txtNguoitiem3.SetId(-1);
                    txtNguoitiem4.SetId(-1);
                    txtNguoitiem5.SetId(-1);
                    chkMuithu1.Checked = chkMuithu2.Checked = chkMuithu3.Checked = chkMuithu4.Checked = chkMuithu5.Checked = false;
                    dtpNgayhen1.Value = dtpNgayhen2.Value = dtpNgayhen3.Value = dtpNgayhen4.Value = dtpNgayhen5.Value
                        = dtpNgaytiem1.Value = dtpNgaytiem2.Value = dtpNgaytiem3.Value = dtpNgaytiem4.Value = dtpNgaytiem5.Value = globalVariables.SysDate;
                    cmdDatiem1.Enabled = cmdDatiem2.Enabled = cmdDatiem3.Enabled = cmdDatiem4.Enabled = cmdDatiem5.Enabled
                        = cmdChuatiem1.Enabled = cmdChuatiem2.Enabled = cmdChuatiem3.Enabled = cmdChuatiem4.Enabled = cmdChuatiem5.Enabled = false;
                    txtMaluotkham.SelectAll();
                    txtMaluotkham.Focus();
                }
                else
                {
                    objBN = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                    this.Text = string.Format("Lập phiếu hẹn tiêm chủng cho Bệnh nhân {0} - {1}",objLuotkham.MaLuotkham,objBN.TenBenhnhan);
                    Getdata();
                }
                chkMuithu1.Enabled = chkMuithu2.Enabled = chkMuithu3.Enabled = chkMuithu4.Enabled = chkMuithu5.Enabled = Utility.isValidGrid(grdList) && quyen_chonmui_hentiemchung;
            }
        }

        void Chuatiem_Click(object sender, EventArgs e)
        {
            UIButton cmd = sender as UIButton;
            if (cmd.Name == cmdChuatiem1.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=1 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem1.Enabled = false;
                    cmdDatiem1.Enabled = !cmdChuatiem1.Enabled;
                    dtpNgayhen1.Enabled = true;
                }
                else
                {
                    cmdChuatiem1.Enabled = cmdDatiem1.Enabled = false;
                }
            }
            else if (cmd.Name == cmdChuatiem2.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=2 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem2.Enabled = false;
                    cmdDatiem2.Enabled = !cmdChuatiem2.Enabled;
                    dtpNgayhen2.Enabled = true;
                }
                else
                {
                    cmdChuatiem2.Enabled = cmdDatiem2.Enabled = false;
                }
            }
            else if (cmd.Name == cmdChuatiem3.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=3 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem3.Enabled = false;
                    cmdDatiem3.Enabled = !cmdChuatiem3.Enabled;
                    dtpNgayhen3.Enabled = true;
                }
                else
                {
                    cmdChuatiem3.Enabled = cmdDatiem3.Enabled = false;
                }
            }
            else if (cmd.Name == cmdChuatiem4.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=4 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem4.Enabled = false;
                    cmdDatiem4.Enabled = !cmdChuatiem4.Enabled;
                    dtpNgayhen4.Enabled = true;
                }
                else
                {
                    cmdChuatiem4.Enabled = cmdDatiem1.Enabled = false;
                }
            }
            else if (cmd.Name == cmdChuatiem5.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=5 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem5.Enabled = false;
                    cmdDatiem5.Enabled = !cmdChuatiem5.Enabled;
                    dtpNgayhen5.Enabled = true;
                }
                else
                {
                    cmdChuatiem5.Enabled = cmdDatiem5.Enabled = false;
                }
            }
        }

        void Datiem_Click(object sender, EventArgs e)
        {
            UIButton cmd = sender as UIButton;
            if (cmd.Name == cmdDatiem1.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=1 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 1;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem1.Enabled = true;
                    cmdDatiem1.Enabled = !cmdChuatiem1.Enabled;
                    dtpNgayhen1.Enabled = false;
                }
                else
                {
                    cmdChuatiem1.Enabled = cmdDatiem1.Enabled = false;
                }
            }
            else if (cmd.Name == cmdDatiem2.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=2 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 1;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem2.Enabled = true;
                    cmdDatiem2.Enabled = !cmdChuatiem2.Enabled;
                    dtpNgayhen2.Enabled = false;
                }
                else
                {
                    cmdChuatiem2.Enabled = cmdDatiem2.Enabled = false;
                }
            }
            else if (cmd.Name == cmdDatiem3.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=3 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 1;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem3.Enabled = true;
                    cmdDatiem3.Enabled = !cmdChuatiem3.Enabled;
                    dtpNgayhen3.Enabled = false;
                }
                else
                {
                    cmdChuatiem3.Enabled = cmdDatiem3.Enabled = false;
                }
            }
            else if (cmd.Name == cmdDatiem4.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=4 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 1;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem4.Enabled = true;
                    cmdDatiem4.Enabled = !cmdChuatiem4.Enabled;
                    dtpNgayhen4.Enabled = false;
                }
                else
                {
                    cmdChuatiem4.Enabled = cmdDatiem4.Enabled = false;
                }
            }
            else if (cmd.Name == cmdDatiem5.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=5 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 1;
                    dsData.Tables[1].AcceptChanges();
                    cmdChuatiem5.Enabled = true;
                    cmdDatiem5.Enabled = !cmdChuatiem5.Enabled;
                    dtpNgayhen5.Enabled = false;
                }
                else
                {
                    cmdChuatiem5.Enabled = cmdDatiem5.Enabled = false;
                }
            }
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            LoadDetails();
        }

        void chkMuihen_CheckedChanged(object sender, EventArgs e)
        {
            txtSongay.Enabled = chkMuihen.Checked;
            int songay = (int)Utility.DecimaltoDbnull(txtSongay.Text, 0);
            if (chkMuihen.Checked)
            {
                AllowedChangedEvents = false;
                dtpNgayhen2.Value = dtpNgayhen1.Value.AddDays(songay);
                dtpNgayhen3.Value = dtpNgayhen2.Value.AddDays(songay);
                dtpNgayhen4.Value = dtpNgayhen3.Value.AddDays(songay);
                dtpNgayhen5.Value = dtpNgayhen4.Value.AddDays(songay);
                AllowedChangedEvents = true;
            }
        }

        void txtDichvuTiemchung__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDichvuTiemchung.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDichvuTiemchung.myCode;
                txtDichvuTiemchung.Init();
                txtDichvuTiemchung.SetCode(oldCode);
                txtDichvuTiemchung.Focus();
            }      
        }

        void mnuDelete_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || !Utility.isValidGrid(grdList)) return;
            string ErrMsg = "";
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu hẹn tiêm chủng cho dịch vụ {0} hay không?", Utility.getValueOfGridCell(grdList,"ten_loaidvu_tiemchung")),"Xác nhận xóa",true))
            {
                long id=Utility.Int64Dbnull(Utility.getValueOfGridCell(grdList, KcbPhieuhenTiemchung.Columns.Id), -1);
                ActionResult act = KCB_TIEMCHUNG.Xoaphieuhen(id, ref ErrMsg);
                if (act == ActionResult.Success)
                {
                    //Xóa khỏi bảng
                    DataRow[] arrDr = dsData.Tables[0].Select(KcbPhieuhenTiemchung.Columns.Id + "=" + id.ToString());
                    if (arrDr.Length > 0)
                        dsData.Tables[0].Rows.Remove(arrDr[0]);
                    arrDr = dsData.Tables[1].Select(KcbPhieuhenTiemchung.Columns.Id + "=" + id.ToString());
                    int count = arrDr.Length;
                    int idx=0;
                _continue:
                        foreach (DataRow dr in dsData.Tables[1].Rows)
                        {
                            if (dr[KcbPhieuhenTiemchung.Columns.Id].ToString() == id.ToString())
                            {
                                idx++;
                                dsData.Tables[1].Rows.Remove(dr);
                                dsData.Tables[1].AcceptChanges();
                            }
                            if (idx < count) goto _continue;
                            else
                                break;
                        }
                chkMuithu1.Enabled = chkMuithu2.Enabled = chkMuithu3.Enabled = chkMuithu4.Enabled = chkMuithu5.Enabled = Utility.isValidGrid(grdList) && quyen_chonmui_hentiemchung;
                }
            }
        }

        void datetime_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowedChangedEvents || objLuotkham == null || !Utility.isValidGrid(grdList)) return;
            DateTimePicker dtp = sender as DateTimePicker;
            if (dtp.Name == dtpNgayhen1.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=1 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtp.Value;
                    arrDr[0]["sngay_hen"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgayhen2.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=2 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtp.Value;
                    arrDr[0]["sngay_hen"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgayhen3.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=3 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtp.Value;
                    arrDr[0]["sngay_hen"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgayhen4.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=4 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtp.Value;
                    arrDr[0]["sngay_hen"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgayhen5.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=5 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtp.Value;
                    arrDr[0]["sngay_hen"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgaytiem1.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=1 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = dtp.Value;
                    arrDr[0]["sngay_tiem"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgaytiem2.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=2 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = dtp.Value;
                    arrDr[0]["sngay_tiem"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgaytiem3.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=3 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = dtp.Value;
                    arrDr[0]["sngay_tiem"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgaytiem4.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=4 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = dtp.Value;
                    arrDr[0]["sngay_tiem"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
            else if (dtp.Name == dtpNgaytiem5.Name)
            {
                DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=5 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = dtp.Value;
                    arrDr[0]["sngay_tiem"] = dtp.Text;
                    dsData.Tables[1].AcceptChanges();
                }
            }
        }

        void txtNguoitiem1__OnEnterMe()
        {
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=1 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
            if (objLuotkham != null && Utility.Int32Dbnull(txtNguoitiem1.MyID,-1)>0 && arrDr.Length > 0)
            {
                arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = txtNguoitiem1.MyID;
                dsData.Tables[1].AcceptChanges();
            }
        }
        void txtNguoitiem2__OnEnterMe()
        {
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=2 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
            if (objLuotkham != null && Utility.Int32Dbnull(txtNguoitiem2.MyID, -1) > 0 && arrDr.Length > 0)
            {
                arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = txtNguoitiem2.MyID;
                dsData.Tables[1].AcceptChanges();
            }
        }
        void txtNguoitiem3__OnEnterMe()
        {
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=3 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
            if (objLuotkham != null && Utility.Int32Dbnull(txtNguoitiem3.MyID, -1) > 0 && arrDr.Length > 0)
            {
                arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = txtNguoitiem3.MyID;
                dsData.Tables[1].AcceptChanges();
            }
        }
        void txtNguoitiem4__OnEnterMe()
        {
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=4 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
            if (objLuotkham != null && Utility.Int32Dbnull(txtNguoitiem4.MyID, -1) > 0 && arrDr.Length > 0)
            {
                arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = txtNguoitiem4.MyID;
                dsData.Tables[1].AcceptChanges();
            }
        }
        void txtNguoitiem5__OnEnterMe()
        {
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=5 AND ma_loaidvu_tiemchung='" + txtDichvuTiemchung.myCode + "'");
            if (objLuotkham != null && Utility.Int32Dbnull(txtNguoitiem5.MyID, -1) > 0 && arrDr.Length > 0)
            {
                arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = txtNguoitiem5.MyID;
                dsData.Tables[1].AcceptChanges();
            }
        }
       

        void txtDichvuTiemchung__OnEnterMe()
        {
            if (objLuotkham == null || txtDichvuTiemchung.myCode == "-1") return;
            if (dsData.Tables[0].Select(KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung + "='" + txtDichvuTiemchung.myCode + "'").Length > 0)
            {
                Utility.GotoNewRowJanus(grdList, KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung, txtDichvuTiemchung.myCode);
                LoadDetails();
            }
            else
            {
                KcbPhieuhenTiemchung newItems = new KcbPhieuhenTiemchung();
                newItems.IdBenhnhan = objLuotkham.IdBenhnhan;
                newItems.MaLuotkham = objLuotkham.MaLuotkham;
                newItems.MaLoaidvuTiemchung = txtDichvuTiemchung.myCode;
                newItems.NgayTao = globalVariables.SysDate;
                newItems.NguoiTao = globalVariables.UserName;
                newItems.IsNew = true;
                newItems.Save();
                DataRow newDr = dsData.Tables[0].NewRow();
                newDr[KcbPhieuhenTiemchung.Columns.Id] = newItems.Id;
                newDr[KcbPhieuhenTiemchung.Columns.IdBenhnhan] = objLuotkham.IdBenhnhan;
                newDr[KcbPhieuhenTiemchung.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                newDr[KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung] = txtDichvuTiemchung.myCode   ;
                newDr["ten_loaidvu_tiemchung"] = txtDichvuTiemchung.Text;
                newDr[KcbPhieuhenTiemchung.Columns.NgayTao] =globalVariables.SysDate;
                newDr[KcbPhieuhenTiemchung.Columns.NguoiTao] = globalVariables.UserName;
                dsData.Tables[0].Rows.Add(newDr);
                dsData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung, txtDichvuTiemchung.myCode);
                LoadDetails();
            }
            chkMuithu1.Enabled = chkMuithu2.Enabled = chkMuithu3.Enabled = chkMuithu4.Enabled = chkMuithu5.Enabled = Utility.isValidGrid(grdList) && quyen_chonmui_hentiemchung;
        }
        void LoadDetails()
        {
            try
            {
                 if (!Utility.isValidGrid(grdList)) return;
                string id = Utility.GetValueFromGridColumn(grdList, KcbPhieuhenTiemchungChitiet.Columns.Id);
                chkMuithu1.Enabled = chkMuithu2.Enabled = chkMuithu3.Enabled = chkMuithu4.Enabled = chkMuithu5.Enabled = Utility.isValidGrid(grdList) && quyen_chonmui_hentiemchung;
                txtDichvuTiemchung.SetCode( Utility.GetValueFromGridColumn(grdList, KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung));
                DataRow[] arrDr = dsData.Tables[1].Select("id='" + id + "'");
                if (arrDr.Length > 0)
                {
                    var q = from p in arrDr.AsEnumerable()
                            where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "1"
                            select p;
                    if (q.Any())
                    {
                        chkMuithu1.Checked = true;
                        dtpNgayhen1.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_hen"]);
                        txtNguoitiem1.SetId(q.FirstOrDefault()["nguoi_tiem"]);
                        dtpNgaytiem1.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_tiem"]);
                        cmdDatiem1.Enabled = q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai].ToString() == "0";
                        cmdChuatiem1.Enabled = !cmdDatiem1.Enabled;
                    }
                    else
                    {
                        chkMuithu1.Checked = false ;
                        dtpNgayhen1.Value = globalVariables.SysDate;
                        txtNguoitiem1.SetId(-1);
                        dtpNgaytiem1.Value = globalVariables.SysDate;
                        cmdDatiem1.Enabled = cmdChuatiem1.Enabled = false;
                        
                    }
                    dtpNgayhen1.Enabled = !q.Any() || Utility.ByteDbnull(q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai])<=0;
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "2"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu2.Checked = true;
                        dtpNgayhen2.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_hen"]);
                        txtNguoitiem2.SetId(q.FirstOrDefault()["nguoi_tiem"]);
                        dtpNgaytiem2.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_tiem"]);
                        cmdDatiem2.Enabled = q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai].ToString() == "0";
                        cmdChuatiem2.Enabled = !cmdDatiem2.Enabled;
                    }
                    else
                    {
                        chkMuithu2.Checked = false;
                        dtpNgayhen2.Value = globalVariables.SysDate;
                        txtNguoitiem2.SetId(-1);
                        dtpNgaytiem2.Value = globalVariables.SysDate;
                        cmdDatiem2.Enabled = cmdChuatiem2.Enabled = false;

                    }
                    dtpNgayhen2.Enabled = !q.Any() || Utility.ByteDbnull(q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai])<=0;
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "3"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu3.Checked = true;
                        dtpNgayhen3.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_hen"]);
                        txtNguoitiem3.SetId(q.FirstOrDefault()["nguoi_tiem"]);
                        dtpNgaytiem3.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_tiem"]);
                        cmdDatiem3.Enabled = q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai].ToString() == "0";
                        cmdChuatiem3.Enabled = !cmdDatiem3.Enabled;
                    }
                    else
                    {
                        chkMuithu3.Checked = false;
                        dtpNgayhen3.Value = globalVariables.SysDate;
                        txtNguoitiem3.SetId(-1);
                        dtpNgaytiem3.Value = globalVariables.SysDate;
                        cmdDatiem3.Enabled = cmdChuatiem2.Enabled = false;

                    }
                    dtpNgayhen3.Enabled = !q.Any() || Utility.ByteDbnull(q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai])<=0;
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "4"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu4.Checked = true;
                        dtpNgayhen4.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_hen"]);
                        txtNguoitiem4.SetId(q.FirstOrDefault()["nguoi_tiem"]);
                        dtpNgaytiem4.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_tiem"]);
                        cmdDatiem4.Enabled = q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai].ToString() == "0";
                        cmdChuatiem4.Enabled = !cmdDatiem4.Enabled;
                    }
                    else
                    {
                        chkMuithu4.Checked = false;
                        dtpNgayhen4.Value = globalVariables.SysDate;
                        txtNguoitiem4.SetId(-1);
                        dtpNgaytiem4.Value = globalVariables.SysDate;
                        cmdDatiem4.Enabled = cmdChuatiem4.Enabled = false;

                    }
                    dtpNgayhen4.Enabled = !q.Any() || Utility.ByteDbnull(q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai])<=0;
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "5"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu5.Checked = true;
                        dtpNgayhen5.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_hen"]);
                        txtNguoitiem5.SetId(q.FirstOrDefault()["nguoi_tiem"]);
                        dtpNgaytiem5.Text = Utility.sDbnull(q.FirstOrDefault()["sngay_tiem"]);
                        cmdDatiem5.Enabled = q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai].ToString() == "0";
                        cmdChuatiem5.Enabled = !cmdDatiem5.Enabled;
                    }
                    else
                    {
                        chkMuithu5.Checked = false;
                        dtpNgayhen5.Value = globalVariables.SysDate;
                        txtNguoitiem5.SetId(-1);
                        dtpNgaytiem5.Value = globalVariables.SysDate;
                        cmdDatiem5.Enabled = cmdChuatiem5.Enabled = false;

                    }
                    dtpNgayhen5.Enabled = !q.Any() || Utility.ByteDbnull(q.FirstOrDefault()[KcbPhieuhenTiemchungChitiet.Columns.TrangThai])<=0;
                }
                else
                {
                    ResetView();

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        void ResetView()
        {
            chkMuithu1.Checked = false;
            chkMuithu2.Checked = false;
            chkMuithu3.Checked = false;
            chkMuithu4.Checked = false;
            chkMuithu5.Checked = false;

            dtpNgayhen1.Value = dtpNgayhen2.Value = dtpNgayhen3.Value = dtpNgayhen4.Value = dtpNgayhen5.Value = globalVariables.SysDate;
            cmdDatiem1.Enabled = cmdChuatiem1.Enabled = txtNguoitiem1.Enabled = dtpNgaytiem1.Enabled
                   = cmdDatiem2.Enabled = cmdChuatiem2.Enabled = txtNguoitiem2.Enabled = dtpNgaytiem2.Enabled
                   = cmdDatiem3.Enabled = cmdChuatiem3.Enabled = txtNguoitiem3.Enabled = dtpNgaytiem3.Enabled
                   = cmdDatiem4.Enabled = cmdChuatiem4.Enabled = txtNguoitiem4.Enabled = dtpNgaytiem4.Enabled
                   = cmdDatiem5.Enabled = cmdChuatiem5.Enabled = txtNguoitiem5.Enabled = dtpNgaytiem5.Enabled = false;
        }
        

        void cmdPrint_Click(object sender, EventArgs e)
        {
            
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "",false);
            bool notAnyChecked=!chkMuithu1.Checked && !chkMuithu2.Checked &&!chkMuithu3.Checked &&!chkMuithu4.Checked &&!chkMuithu5.Checked ;
            if (notAnyChecked)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một mũi hẹn tiêm chủng. Nếu muốn xóa dịch vụ hẹn tiêm chủng, bạn nháy chuột phải vào lưới danh sách và chọn mục Xóa");
                return;
            }
            if (objLuotkham == null || !Utility.isValidGrid(grdList)
                || notAnyChecked) return;
            //var q=from p in  dsData.Tables[0].AsEnumerable()
            //      select p[KcbPhieuhenTiemchung.Columns.Id];
            //var q=from p in dsData.Tables[0].AsEnumerable()
            //      where
            List<KcbPhieuhenTiemchungChitiet> lstItems = new List<KcbPhieuhenTiemchungChitiet>();
            List<long> lstDeleID = new List<long>();
            foreach (DataRow dr in dsData.Tables[1].Rows)
            {
                if (dr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")//Insert
                {
                    KcbPhieuhenTiemchungChitiet newItem = new KcbPhieuhenTiemchungChitiet();
                    newItem.IsNew = true;
                    newItem.Id = Utility.Int64Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.Id]);
                    newItem.MuiThu = Utility.ByteDbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu]);
                    newItem.NgayHen =(DateTime) dr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen];
                    newItem.NguoiTiem = Utility.Int16Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem]);
                    if (dr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] == DBNull.Value)
                        newItem.NgayTiem = null;
                    else
                        newItem.NgayTiem = (DateTime)dr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem];
                    newItem.TrangThai = Utility.ByteDbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai]);
                    newItem.GhiChu = "";
                    lstItems.Add(newItem);
                }
                else
                {
                    if (dr["del"].ToString() == "1")//Delete
                    {
                        lstDeleID.Add((Int64)dr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet]);
                    }
                    else//Update
                    {
                        KcbPhieuhenTiemchungChitiet newItem = KcbPhieuhenTiemchungChitiet.FetchByID(dr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString());
                        newItem.IsNew = false;
                        newItem.Id = Utility.Int64Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.Id]);
                        newItem.MuiThu = Utility.ByteDbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu]);
                        newItem.NgayHen = (DateTime)dr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen];
                        newItem.NguoiTiem = Utility.Int16Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem]);
                        if (dr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] == DBNull.Value)
                            newItem.NgayTiem = null;
                        else
                            newItem.NgayTiem = (DateTime)dr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem];
                        newItem.TrangThai = Utility.ByteDbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai]);
                        newItem.GhiChu = "";
                        lstItems.Add(newItem);
                    }
                }
            }
            string ErrMsg = "";
            ActionResult act = KCB_TIEMCHUNG.CapnhatPhieuhen(lstItems, lstDeleID, ref ErrMsg);
          if (act != ActionResult.Success)
          {
              Utility.SetMsg(lblMsg, "Lỗi khi cập nhật phiếu hẹn tiêm chủng!", true);
              Utility.ShowMsg(ErrMsg);
          }
          else
          {
              int count = lstDeleID.Count;
              int idx = 0;
          _continue:
              foreach (DataRow dr in dsData.Tables[1].Rows)
              {
                  if (lstDeleID.Contains(Utility.Int64Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet], -1)))
                  {
                      idx++;
                      dsData.Tables[1].Rows.Remove(dr);
                      dsData.Tables[1].AcceptChanges();
                  }
                  if (idx < count) goto _continue;
                  else
                      break;
              }

              foreach (DataRow dr in dsData.Tables[1].Rows)
              {
                  var q = from p in lstItems
                          where p.Id == Utility.Int32Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.Id], -1)
                          && p.MuiThu == Utility.Int32Dbnull(dr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], -1)
                          select p;
                  if (q.Any())
                  {
                      dr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet] = q.FirstOrDefault().IdChitiet;
                  }
              }
              Utility.SetMsg(lblMsg, "Cập nhật phiếu hẹn tiêm chủng thành công!",false);
          }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _CheckedChanged(object sender, EventArgs e)
        {
            
            if (!AllowedChangedEvents || !Utility.isValidGrid(grdList)) return;
            UICheckBox chk = sender as UICheckBox;
            int _tag=Utility.Int32Dbnull(chk.Tag,-1);
            int id_phieuhen=Utility.Int32Dbnull( Utility.getValueOfGridCell(grdList,KcbPhieuhenTiemchung.Columns.Id),-1);
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=" + _tag.ToString() + " AND ma_loaidvu_tiemchung='"+txtDichvuTiemchung.myCode+"'");
            switch (_tag)
            {
                case 1:
                    if (arrDr.Length > 0)//Có dữ liệu mũi tương ứng
                    {
                        if (!chk.Checked)//bỏ mũi
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                                dsData.Tables[1].Rows.Remove(arrDr[0]);
                                cmdChuatiem1.Enabled = cmdDatiem1.Enabled=false;
                            }
                            else
                            {
                                if (Utility.Int32Dbnull( arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai],0) >0)
                                {
                                    Utility.ShowMsg("Mũi 1 đã được đánh dấu hoặc xác nhận trạng thái đã tiêm nên bạn không thể bỏ mũi. Muốn bỏ mũi thì đánh dấu là chưa tiêm");
                                    chk.Checked = true;
                                    return;
                                }
                                else
                                    arrDr[0]["del"] = 1;
                            }
                        }
                        else//Hẹn mũi-->
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")//99.99% ko bao giờ vào nhánh này
                            {
                            }
                            else
                            {
                                arrDr[0]["del"] = 0;//Khôi phục mũi đánh dấu  xóa thành ko xóa-->Sẽ update
                            }
                            dsData.Tables[1].AcceptChanges();
                            dtpNgaytiem1.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                            txtNguoitiem1.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                            dtpNgaytiem1.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                            cmdDatiem1.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                            cmdChuatiem1.Enabled = !cmdDatiem1.Enabled;
                        }
                        
                    }
                    else//Chưa có mũi nào ứng với dịch vụ tiêm chủng đang chọn-->thêm mới vào DataTable
                    {
                        dtpNgayhen1.Enabled =chk.Enabled && chk.Checked;
                        if (chk.Checked)//Add to DataTable
                        {
                            DataRow newDr = dsData.Tables[1].NewRow();
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.Id] = id_phieuhen;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu] = 1;
                            newDr["ma_loaidvu_tiemchung"] = txtDichvuTiemchung.myCode;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtpNgayhen1.Value;
                            newDr["sngay_hen"] = dtpNgayhen1.Text;
                            newDr["sngay_tiem"] = "";
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = DBNull.Value ;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.GhiChu] = "";
                            dsData.Tables[1].Rows.Add(newDr);
                            dsData.AcceptChanges();
                            cmdDatiem1.Enabled = true;
                            cmdChuatiem1.Enabled = !cmdDatiem1.Enabled;
                        }
                    }

                    break;
                case 2:
                    if (arrDr.Length > 0)//Có dữ liệu mũi tương ứng
                    {
                        if (!chk.Checked)//bỏ mũi
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                                dsData.Tables[1].Rows.Remove(arrDr[0]);
                                cmdChuatiem2.Enabled = cmdDatiem2.Enabled = false;
                            }
                            else
                            {
                                if (Utility.Int32Dbnull(arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai], 0) > 0)
                                {
                                    Utility.ShowMsg("Mũi 2 đã được đánh dấu hoặc xác nhận trạng thái đã tiêm nên bạn không thể bỏ mũi. Muốn bỏ mũi thì đánh dấu là chưa tiêm");
                                    chk.Checked = true;
                                    return;
                                }
                                else
                                    arrDr[0]["del"] = 1;
                            }
                        }
                        else//Hẹn mũi-->
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                            }
                            else
                            {
                                arrDr[0]["del"] = 0;//Khôi phục mũi đánh dấu  xóa thành ko xóa-->Sẽ update
                            }
                            dsData.Tables[1].AcceptChanges();
                            dtpNgaytiem2.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                            txtNguoitiem2.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                            dtpNgaytiem2.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                            cmdDatiem2.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                            cmdChuatiem2.Enabled = !cmdDatiem2.Enabled;
                        }
                       
                    }
                    else//Chưa có mũi nào ứng với dịch vụ tiêm chủng đang chọn-->thêm mới vào DataTable
                    {
                        dtpNgayhen2.Enabled = chk.Enabled && chk.Checked;
                        if (chk.Checked)//Add to DataTable
                        {
                            DataRow newDr = dsData.Tables[1].NewRow();
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.Id] = id_phieuhen;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu] = 2;
                            newDr["ma_loaidvu_tiemchung"] = txtDichvuTiemchung.myCode;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtpNgayhen2.Value;
                            newDr["sngay_hen"] = dtpNgayhen2.Text;
                            newDr["sngay_tiem"] = "";
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = DBNull.Value;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.GhiChu] = "";
                            dsData.Tables[1].Rows.Add(newDr);
                            dsData.AcceptChanges();
                            cmdDatiem2.Enabled = true;
                            cmdChuatiem2.Enabled = !cmdDatiem2.Enabled;
                        }
                    }
                    break;
                case 3:
                    if (arrDr.Length > 0)//Có dữ liệu mũi tương ứng
                    {
                        if (!chk.Checked)//bỏ mũi
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                                dsData.Tables[1].Rows.Remove(arrDr[0]);
                                cmdChuatiem3.Enabled = cmdDatiem3.Enabled = false;
                            }
                            else
                            {
                                if (Utility.Int32Dbnull(arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai], 0) > 0)
                                {
                                    Utility.ShowMsg("Mũi 3 đã được đánh dấu hoặc xác nhận trạng thái đã tiêm nên bạn không thể bỏ mũi. Muốn bỏ mũi thì đánh dấu là chưa tiêm");
                                    chk.Checked = true;
                                    return;
                                }
                                else
                                    arrDr[0]["del"] = 1;
                            }
                        }
                        else//Hẹn mũi-->
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                            }
                            else
                            {
                                arrDr[0]["del"] = 0;//Khôi phục mũi đánh dấu  xóa thành ko xóa-->Sẽ update
                            }
                            dsData.Tables[1].AcceptChanges();
                            dtpNgaytiem3.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                            txtNguoitiem3.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                            dtpNgaytiem3.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                            cmdDatiem3.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                            cmdChuatiem3.Enabled = !cmdDatiem3.Enabled;
                        }
                       
                    }
                    else//Chưa có mũi nào ứng với dịch vụ tiêm chủng đang chọn-->thêm mới vào DataTable
                    {
                        dtpNgayhen3.Enabled = chk.Enabled && chk.Checked;
                        if (chk.Checked)//Add to DataTable
                        {
                            DataRow newDr = dsData.Tables[1].NewRow();
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.Id] = id_phieuhen;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu] = 3;
                            newDr["ma_loaidvu_tiemchung"] = txtDichvuTiemchung.myCode;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtpNgayhen3.Value;
                            newDr["sngay_hen"] = dtpNgayhen3.Text;
                            newDr["sngay_tiem"] = "";
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = DBNull.Value;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.GhiChu] = "";
                            dsData.Tables[1].Rows.Add(newDr);
                            dsData.AcceptChanges();
                            cmdDatiem3.Enabled = true;
                            cmdChuatiem3.Enabled = !cmdDatiem3.Enabled;
                        }
                    }

                    break;
                case 4:
                    if (arrDr.Length > 0)//Có dữ liệu mũi tương ứng
                    {
                        if (!chk.Checked)//bỏ mũi
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                                dsData.Tables[1].Rows.Remove(arrDr[0]);
                                cmdChuatiem4.Enabled = cmdDatiem4.Enabled = false;
                            }
                            else
                            {
                                if (Utility.Int32Dbnull(arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai], 0) > 0)
                                {
                                    Utility.ShowMsg("Mũi 4 đã được đánh dấu hoặc xác nhận trạng thái đã tiêm nên bạn không thể bỏ mũi. Muốn bỏ mũi thì đánh dấu là chưa tiêm");
                                    chk.Checked = true;
                                    return;
                                }
                                else
                                    arrDr[0]["del"] = 1;
                            }
                        }
                        else//Hẹn mũi-->
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                            }
                            else
                            {
                                arrDr[0]["del"] = 0;//Khôi phục mũi đánh dấu  xóa thành ko xóa-->Sẽ update
                            }
                            dsData.Tables[1].AcceptChanges();
                            dtpNgaytiem4.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                            txtNguoitiem4.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                            dtpNgaytiem4.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                            cmdDatiem4.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                            cmdChuatiem4.Enabled = !cmdDatiem4.Enabled;
                        }
                       
                    }
                    else//Chưa có mũi nào ứng với dịch vụ tiêm chủng đang chọn-->thêm mới vào DataTable
                    {
                        dtpNgayhen4.Enabled = chk.Enabled && chk.Checked;
                        if (chk.Checked)//Add to DataTable
                        {
                            DataRow newDr = dsData.Tables[1].NewRow();
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.Id] = id_phieuhen;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu] = 4;
                            newDr["ma_loaidvu_tiemchung"] = txtDichvuTiemchung.myCode;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtpNgayhen4.Value;
                            newDr["sngay_hen"] = dtpNgayhen4.Text;
                            newDr["sngay_tiem"] = "";
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = DBNull.Value;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.GhiChu] = "";
                            dsData.Tables[1].Rows.Add(newDr);
                            dsData.AcceptChanges();
                            cmdDatiem4.Enabled = true;
                            cmdChuatiem4.Enabled = !cmdDatiem4.Enabled;
                        }
                    }

                    break;
                case 5:
                    if (arrDr.Length > 0)//Có dữ liệu mũi tương ứng
                    {
                        if (!chk.Checked)//bỏ mũi
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                                dsData.Tables[1].Rows.Remove(arrDr[0]);
                                cmdChuatiem5.Enabled = cmdDatiem5.Enabled = false;
                            }
                            else
                            {
                                if (Utility.Int32Dbnull(arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.TrangThai], 0) > 0)
                                {
                                    Utility.ShowMsg("Mũi 5 đã được đánh dấu hoặc xác nhận trạng thái đã tiêm nên bạn không thể bỏ mũi. Muốn bỏ mũi thì đánh dấu là chưa tiêm");
                                    chk.Checked = true;
                                    return;
                                }
                                else
                                    arrDr[0]["del"] = 1;
                            }
                        }
                        else//Hẹn mũi-->
                        {
                            if (arrDr[0][KcbPhieuhenTiemchungChitiet.Columns.IdChitiet].ToString() == "-1")
                            {
                            }
                            else
                            {
                                arrDr[0]["del"] = 0;//Khôi phục mũi đánh dấu  xóa thành ko xóa-->Sẽ update
                            }
                            dsData.Tables[1].AcceptChanges();
                            dtpNgaytiem5.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                            txtNguoitiem5.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                            dtpNgaytiem5.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                            cmdDatiem5.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                            cmdChuatiem5.Enabled = !cmdDatiem5.Enabled;
                        }
                       
                    }
                    else//Chưa có mũi nào ứng với dịch vụ tiêm chủng đang chọn-->thêm mới vào DataTable
                    {
                        dtpNgayhen5.Enabled = chk.Enabled && chk.Checked;
                        if (chk.Checked)//Add to DataTable
                        {
                            DataRow newDr = dsData.Tables[1].NewRow();
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.IdChitiet] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.Id] = id_phieuhen;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.MuiThu] = 5;
                            newDr["ma_loaidvu_tiemchung"] = txtDichvuTiemchung.myCode;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayHen] = dtpNgayhen5.Value;
                            newDr["sngay_hen"] = dtpNgayhen5.Text;
                            newDr["sngay_tiem"] = "";
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NguoiTiem] = -1;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.NgayTiem] = DBNull.Value;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.TrangThai] = 0;
                            newDr[KcbPhieuhenTiemchungChitiet.Columns.GhiChu] = "";
                            dsData.Tables[1].Rows.Add(newDr);
                            dsData.AcceptChanges();
                            cmdDatiem5.Enabled = true;
                            cmdChuatiem5.Enabled = !cmdDatiem5.Enabled;
                        }
                    }

                    break;
                default:
                    break;
            }

        }

        void frm_KCB_Lapphieuhen_Tiemchung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F3)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F4)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
        }
       
        void frm_KCB_Lapphieuhen_Tiemchung_Load(object sender, EventArgs e)
        {
            try
            {
                 quyencapnhatketqua = Utility.Coquyen("quyen_capnhat_ketqua_hentiemchung");
                 quyen_chonmui_hentiemchung = Utility.Coquyen("quyen_chonmui_hentiemchung");
                cmdDatiem1.Visible = cmdChuatiem1.Visible = lblNguoitiem1.Visible = lblNgaytiem1.Visible
                    = cmdDatiem2.Visible = cmdChuatiem2.Visible = lblNguoitiem2.Visible = lblNgaytiem2.Visible
                    = cmdDatiem3.Visible = cmdChuatiem3.Visible = lblNguoitiem3.Visible = lblNgaytiem3.Visible
                    = cmdDatiem4.Visible = cmdChuatiem4.Visible = lblNguoitiem4.Visible = lblNgaytiem4.Visible
                    = cmdDatiem5.Visible = cmdChuatiem5.Visible = lblNguoitiem5.Visible = lblNgaytiem5.Visible
                    = quyencapnhatketqua;
                
                InitView();
                Getdata();
                chkMuithu1.Enabled = chkMuithu2.Enabled = chkMuithu3.Enabled = chkMuithu4.Enabled = chkMuithu5.Enabled = Utility.isValidGrid(grdList) && quyen_chonmui_hentiemchung;

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                AllowedChangedEvents = true;
            }
        }
        void InitView()
        {
            txtDichvuTiemchung.Init();
            DataTable Nguoitiem = THU_VIEN_CHUNG.Laydanhsachnhanvien("NGUOITIEM");
            txtNguoitiem1.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem2.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem3.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem4.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem5.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
        }
        void Getdata()
        {
            if (objLuotkham != null && objBN == null)
                objBN = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
            if (objLuotkham != null && objBN != null)
            {
                this.Text = string.Format("Lập phiếu hẹn tiêm chủng cho Bệnh nhân {0} - {1}", objLuotkham.MaLuotkham, objBN.TenBenhnhan);
                txtMaluotkham.Text = objLuotkham.MaLuotkham;
                dsData = KCB_TIEMCHUNG.KcbTiemchungPhieuhen(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
                Utility.SetDataSourceForDataGridEx(grdList, dsData.Tables[0], false, true, "", "STT_HTHI,ten_loaidvu_tiemchung");
                grdList.MoveFirst();
            }
        }

        private void grdList_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }
    }
}
