using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Microsoft.VisualBasic;
using VNS.HIS.UCs;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using VNS.HIS.UI.Classess;
using Aspose.Words;
using System.Diagnostics;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.Danhmuc.Dungchung;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_ThemBbanhoichan : Form
    {
        public action m_enAct = action.FirstOrFinished;
        public KcbBienbanhoichan bbhc = new KcbBienbanhoichan();
        DataTable dtbsthamgia = new DataTable();
        public bool CallfromParent = false;
        KcbLuotkham objLuotkham = null;
        public DataTable dtbsphauthuat = new DataTable();
        public DataTable dtbsgayme = new DataTable();
        public DataTable dtbsphauthuatphu = new DataTable();
        public frm_ThemBbanhoichan()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtpNgayhoichan.Value = DateTime.Now;
            dtbsthamgia = globalVariables.gv_dtDmucNhanvien.Clone();
            grdBacsithamgia.DataSource = dtbsthamgia;
            InitEvents();
            dtbsgayme = globalVariables.gv_dtDmucNhanvien.Clone();
            dtbsphauthuat = globalVariables.gv_dtDmucNhanvien.Clone();
            dtbsphauthuatphu = globalVariables.gv_dtDmucNhanvien.Clone();
            //ucThongtinnguoibenh_doc_v61.SetReadonly();
            SetReadOnly();

        }
        void SetReadOnly()
        {
            try
            {
                bool isReadOnly = THU_VIEN_CHUNG.Laygiatrithamsohethong("BIENBANHOICHAN_CHUCNANGSONG_READONLY", "1", true) == "1";
                foreach (Control ctr in grpChucNangSong.Controls)
                    if (ctr.GetType().Equals(typeof(EditBox)) || ctr.GetType().Equals(typeof(AutoCompleteTextbox_Danhmucchung)))
                        ((EditBox)ctr).ReadOnly = isReadOnly;
                isReadOnly = THU_VIEN_CHUNG.Laygiatrithamsohethong("BIENBANHOICHAN_KQXN_READONLY", "1", true) == "1";
                foreach (Control ctr in pnlKQXN.Controls)
                    if (ctr.GetType().Equals(typeof(EditBox)) || ctr.GetType().Equals(typeof(AutoCompleteTextbox_Danhmucchung)))
                        ((EditBox)ctr).ReadOnly = isReadOnly;
                isReadOnly = THU_VIEN_CHUNG.Laygiatrithamsohethong("BIENBANHOICHAN_KQCDHA_READONLY", "1", true) == "1";
                txtXQ.ReadOnly = txtSA.ReadOnly = txtDientim.ReadOnly = isReadOnly;
            }
            catch (Exception ex)
            {
                
               Utility.CatchException(ex);
            }
        }
        void InitEvents()
        {
            ucThongtinnguoibenh_doc_v61._OnEnterMe += ucThongtinnguoibenh_doc_v61__OnEnterMe;
            this.KeyDown += frm_ThemBbanhoichan_KeyDown;
            FormClosing += frm_ThemBbanhoichan_FormClosing;
            autoBacsithamgia._OnEnterMe += autoBacsithamgia__OnEnterMe;
            autoLydohc._OnShowDataV1 += __OnShowDataV1;
            autohinhthuchc._OnShowDataV1 += __OnShowDataV1;
            autoBSPhauthuat._OnEnterMe += autoBSPhauthuat__OnEnterMe;
            autoLydohc._OnSaveAsV1 += autoLydohc__OnSaveAsV1;
            autoBSGayme._OnEnterMe += autoBSGayme__OnEnterMe;
            autoBSphu._OnEnterMe += autoBSphu__OnEnterMe;
            grd_bsgm.ColumnButtonClick += grd_bsgm_ColumnButtonClick;
            grd_bspt.ColumnButtonClick += grd_bspt_ColumnButtonClick;
            grd_bsphauthuatphu.ColumnButtonClick += grd_bsphauthuatphu_ColumnButtonClick;
        }

        void autoLydohc__OnSaveAsV1(AutoCompleteTextbox_Danhmucchung obj)
        {
            if (Utility.DoTrim(obj.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, obj.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }   
        }
        void grd_bsphauthuatphu_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ phẫu thuật phụ: {0} không?", grd_bsphauthuatphu.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bsphauthuatphu.CurrentRow.Delete();
                        dtbsphauthuatphu.AcceptChanges();
                        grd_bsphauthuatphu.Refetch();
                        grd_bsphauthuatphu.AutoSizeColumns();

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

            }
        }

        void grd_bspt_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ phẫu thuật : {0} không?", grd_bspt.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bspt.CurrentRow.Delete();
                        dtbsphauthuat.AcceptChanges();
                        grd_bspt.Refetch();
                        grd_bspt.AutoSizeColumns();

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

            }
        }

        void grd_bsgm_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ gây mê: {0} không?", grd_bsgm.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bsgm.CurrentRow.Delete();
                        dtbsgayme.AcceptChanges();
                        grd_bsgm.Refetch();
                        grd_bsgm.AutoSizeColumns();

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

            }
        }
        void autoBSphu__OnEnterMe()
        {
            if (autoBSphu.MyID != "-1")
            {
                AddBacsi(dtbsphauthuatphu, grd_bsphauthuatphu, autoBSphu);
                autoBSphu.Focus();
                autoBSphu.SelectAll();
            }
        }
        void autoBSGayme__OnEnterMe()
        {
            if (autoBSGayme.MyID != "-1")
            {
                AddBacsi(dtbsgayme, grd_bsgm, autoBSGayme);
                autoBSGayme.Focus();
                autoBSGayme.SelectAll();
            }
        }

        void autoBSPhauthuat__OnEnterMe()
        {
            if (autoBSPhauthuat.MyID != "-1")
            {
                AddBacsi(dtbsphauthuat, grd_bspt, autoBSPhauthuat);
                autoBSPhauthuat.Focus();
                autoBSPhauthuat.SelectAll();
            }
        }
        private void AddBacsi(DataTable dtData, GridEX grdList, AutoCompleteTextbox auto)
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from benh in dtData.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucNhanvien.Columns.MaNhanvien]) == auto.MyCode
                                                         && Utility.sDbnull(benh[DmucNhanvien.Columns.IdNhanvien]) == auto.MyID
                                                         select benh;
                if (!query.Any())
                {
                    DataRow drv = dtData.NewRow();
                    drv[DmucNhanvien.Columns.IdNhanvien] = auto.MyID;
                    drv[DmucNhanvien.Columns.MaNhanvien] = auto.MyCode;
                    drv[DmucNhanvien.Columns.TenNhanvien] = auto.Text;
                    dtData.Rows.Add(drv);
                    dtData.AcceptChanges();
                    grdList.AutoSizeColumns();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
            }
        }
        void frm_ThemBbanhoichan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }



        void __OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }

        }
        void autoBacsithamgia__OnEnterMe()
        {
            if (Utility.DoTrim(autoBacsithamgia.Text).Length > 0)
            {
                AddBacsi();
                autoBacsithamgia.Focus();
                autoBacsithamgia.SelectAll();
            }
        }
        private void AddBacsi()
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from benh in dtbsthamgia.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucNhanvien.Columns.MaNhanvien]) == autoBacsithamgia.MyCode
                                                         && Utility.sDbnull(benh[DmucNhanvien.Columns.TenNhanvien]) == autoBacsithamgia.Text
                                                         select benh;
                if (!query.Any())
                {
                    DataRow drv = dtbsthamgia.NewRow();
                    drv[DmucNhanvien.Columns.MaNhanvien] = autoBacsithamgia.MyCode;
                    drv[DmucNhanvien.Columns.TenNhanvien] = autoBacsithamgia.Text;
                    dtbsthamgia.Rows.Add(drv);
                    dtbsthamgia.AcceptChanges();
                    grdBacsithamgia.AutoSizeColumns();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
            }
        }


        void frm_ThemBbanhoichan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control activeCtrl = Utility.getActiveControl(this);
                if ((activeCtrl != null && (activeCtrl.Name == autoBSPhauthuat.Name || activeCtrl.Name == autoBSGayme.Name || activeCtrl.Name == autoBSphu.Name || activeCtrl.Name == autoBacsithamgia.Name)))
                    return;
                else
                {
                    if (activeCtrl.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = activeCtrl as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (activeCtrl.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = activeCtrl as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    //else if (activeCtrl.Name == autoBacsithamgia.Name)
                    //    if (Utility.DoTrim(autoBacsithamgia.Text).Length > 0)
                    //        return;
                    //    else
                    //        SendKeys.Send("{TAB}");
                    else if (activeCtrl.Name == txtNhommau.Name)
                    {
                        uiTabInfor.SelectedIndex = 1;
                        txtCT.Focus();
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                //if (tabPage1.ActiveControl != null)
                //{
                //    Control ctr = tabPage1.ActiveControl;
                //    if (ctr.GetType().Equals(typeof(EditBox)))
                //    {
                //        EditBox box = ctr as EditBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.GetType().Equals(typeof(TextBox)))
                //    {
                //        TextBox box = ctr as TextBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.Name == autoBacsithamgia.Name)
                //        if (Utility.DoTrim(autoBacsithamgia.Text).Length > 0)
                //            return;
                //        else
                //            SendKeys.Send("{TAB}");
                //    else if (ctr.Name == txtChanDoanTuyenDuoiKKB.Name)
                //    {
                //        uiTabInfor.SelectedIndex = 1;
                //        txtTomTatDienBienBenh.Focus();
                //    }
                //    else
                //        SendKeys.Send("{TAB}");

                //}
                //else if (tabPage2.ActiveControl != null)
                //{
                //    Control ctr = tabPage2.ActiveControl;
                //    if (ctr.GetType().Equals(typeof(EditBox)))
                //    {
                //        EditBox box = ctr as EditBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }
                //    else if (ctr.GetType().Equals(typeof(TextBox)))
                //    {
                //        TextBox box = ctr as TextBox;
                //        if (box.Multiline)
                //        {
                //            return;
                //        }
                //        else
                //            SendKeys.Send("{TAB}");
                //    }

                //}
                //else
                //    SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cmdExit.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                cmdThemMoiBN.PerformClick();
            }

        }

        void ucThongtinnguoibenh_doc_v61__OnEnterMe()
        {
            if (ucThongtinnguoibenh_doc_v61.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_doc_v61.objLuotkham;
                
                bbhc = new Select().From(KcbBienbanhoichan.Schema)
                    .Where(KcbBienbanhoichan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbBienbanhoichan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<KcbBienbanhoichan>();
                if (bbhc != null) if(m_enAct!=action.View) m_enAct = action.Update;
                FillData4Update();
            }
        }

        string getBacsithamgia()
        {
            var q = from p in dtbsthamgia.AsEnumerable()
                    select Utility.sDbnull(p["ten_nhanvien"], "");
            return string.Join(",", q.ToArray<string>());
        }
        void FillBacsithamgia(string dataString)
        {
            dtbsthamgia.Clear();
            if (!string.IsNullOrEmpty(dataString))
            {
                string[] rows = dataString.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dtbsthamgia.NewRow();
                        newDr[DmucNhanvien.Columns.MaNhanvien] = "";
                        newDr[DmucNhanvien.Columns.TenNhanvien] = row;// LaytenNvien(row);
                        dtbsthamgia.Rows.Add(newDr);
                        dtbsthamgia.AcceptChanges();
                    }
                }
                grdBacsithamgia.DataSource = dtbsthamgia;
            }
        }
        private string LaytenNvien(string id_nhanvien)
        {
            try
            {
                string TenNhanvien = "";
                DataRow[] arrMaBenh =
                    globalVariables.gv_dtDmucNhanvien.Select(string.Format(DmucNhanvien.Columns.IdNhanvien + "={0}", id_nhanvien));
                if (arrMaBenh.GetLength(0) > 0) TenNhanvien = Utility.sDbnull(arrMaBenh[0][DmucNhanvien.Columns.TenNhanvien], "");
                return TenNhanvien;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        //private string LaytenNvien(string MaNhanvien)
        //{
        //    string TenNhanvien = "";
        //    DataRow[] arrMaBenh =
        //        globalVariables.gv_dtDmucNhanvien.Select(string.Format(DmucNhanvien.Columns.MaNhanvien + "='{0}'", MaNhanvien));
        //    if (arrMaBenh.GetLength(0) > 0) TenNhanvien = Utility.sDbnull(arrMaBenh[0][DmucNhanvien.Columns.TenNhanvien], "");
        //    return TenNhanvien;
        //}
        void FillData4Update()
        {
            txtNhommau.Text = ucThongtinnguoibenh_doc_v61.objBenhnhan != null ? ucThongtinnguoibenh_doc_v61.objBenhnhan.NhomMau : "";
            if (bbhc != null)
            {
                txtstt_rec.Text = Utility.sDbnull(bbhc.MaBbhc, "");
                txtID.Text = bbhc.Id.ToString();

                txtBacsidexuat.SetId(bbhc.BacsiDexuat);
                dtpNgayhoichan.Value = Convert.ToDateTime(bbhc.NgayHoichan);
                //  dtNgayCapHoChieu.Value = Convert.ToDateTime(bbhc.NgayCapHoChieu);
                FillBacsithamgia(bbhc.BacsiThamgia);
                FillBacsiPttt(bbhc.IdbacsiGayme, dtbsgayme, grd_bsgm);
                FillBacsiPttt(bbhc.IdbacsiPttt, dtbsphauthuat, grd_bspt);
                FillBacsiPttt(bbhc.IdbacsiPtttPhu, dtbsphauthuatphu, grd_bsphauthuatphu);
                txtHopTai._Text = bbhc.HopTai;
                autoChutoa._Text = bbhc.ChuToa;
                autoThuki._Text = bbhc.ThuKy;
                txtYeuCauHoiChan.Text = bbhc.YeucauHoichan;
                autoLydohc.SetCode(bbhc.MaLydoHc);
                autohinhthuchc.SetCode(bbhc.MaHinhthucHc);
                //txtNoiCapHoChieu.Text = bbhc.NoiCap;
                //txtNoiLamViec.Text = bbhc.NoiLamViec;
                txtTsb_ngoaikhoa.Text = bbhc.TsbNgoaikhoa;
                txtTsb_sankhoa.Text = bbhc.TsbSankhoa;
                txtTinhTrangLucVaoVien.Text = bbhc.TrangthaiVaovien;
                txtChanDoanTuyenDuoiKKB.Text = bbhc.ChanDoan;
                txtTomTatDienBienBenh.Text = bbhc.DienbienBenh;
                txtChanDoan.Text = bbhc.ChandoanNguyennhanTienluong;
                txtPhuongPhapDieuTri.Text = bbhc.Pphapdieutri;
                txtChamSoc.Text = bbhc.ChamSoc;
                txtKetLuanChanDoan.Text = bbhc.KetluanChandoan;
                txtKetLuanHuongXuLyTiep.Text = bbhc.Huongxuly;
                txtKetLuanTienLuong.Text = bbhc.KetluanTienluong;
                txtChanDoan.Text = bbhc.ChandoanSaukham;
                txtNguyenNhan.Text = bbhc.NguyenNhan;
                txtTienLuong.Text = bbhc.TienLuong;
                txtQuaTrinhChamSoc.Text = bbhc.QuatrinhChamsoc;
                txtQuaTrinhDieuTri.Text = bbhc.QuatrinhDieutri;
                //Các trường mới theo mẫu chị Hường yc
                txtTsb_noikhoa.Text = bbhc.TsbNoikhoa;
                txttsb_khac.Text = bbhc.TsbKhac;

                txtMach.Text = bbhc.Mach;
                txtha.Text = bbhc.Ha;
                txtNhietDo.Text = bbhc.NhietDo;
                txtChieucao.Text = bbhc.Cao;
                txtCannang.Text = bbhc.CanNang;
                txtBmi.Text = bbhc.Bmi;
                txtNhommau.Text = bbhc.Nhommau;
                txtToanthan.Text = bbhc.ToanThan;

                txtToanthan.Text = bbhc.ToanThan;
                txtTrieuchungconang.Text = bbhc.TrieuchungConang;
                txtTrieuchungthucthe.Text = bbhc.TrieuchungThucthe;

                optkhamtimmach_binhthuong.Checked = bbhc.Timach.Value == 0;
                optkhamtimmach_batthuong.Checked = bbhc.Timach.Value == 1;
                optkhamtimmach_khac.Checked = bbhc.Timach.Value == 2;
                txtkhamtimmach_khac.Text = bbhc.TimmachKhac;
                optkhamhohap_binhthuong.Checked = bbhc.Hohap.Value == 0;
                optkhamhohap_COPD.Checked = bbhc.Hohap.Value == 1;
                optkhamhohap_khac.Checked = bbhc.Hohap.Value == 2;
                txtkhamhohap_khac.Text = bbhc.HohapKhac;

                txtHCT.Text = bbhc.XnHct;
                txtHC.Text = bbhc.XnHc;
                txtBC.Text = bbhc.XnBc;
                txtTC.Text = bbhc.XnTieucau;
                txtTqr.Text = bbhc.XnTqr;
                txtTckr.Text = bbhc.XnTckr;
                txtRh.Text = bbhc.XnRh;
                txtHiv.Text = bbhc.XnHiv;
                txtHCV.Text = bbhc.XnHcv;
                txtHbsag.Text = bbhc.XnHbsAg;
                txtQuicktest.Text = bbhc.XnQuicktest;
                txtGlucose.Text = bbhc.XnGlucose;
                txtUre.Text = bbhc.XnUre;
                txtCreatinin.Text = bbhc.XnCreatinin;
                txtAst.Text = bbhc.XnAst;
                txtALT.Text = bbhc.XnAlt;
                txtNuoctieu.Text = bbhc.XnNuoctieu;

                txtXQ.Text = bbhc.CdhaXq;
                txtSA.Text = bbhc.CdhaSa;
                txtDientim.Text = bbhc.CdhaDientim;
                txtCT.Text = bbhc.CdhaCt;
                txtMRI.Text = bbhc.CdhaMri;
                txtCDHAKhac.Text = bbhc.CdhaKhac;

                txtChandoan_new.Text = bbhc.ChanDoan;
                txtBenhlykemtheo.Text = bbhc.BenhlyKemtheo;
                optSach.Checked = bbhc.PhanloaiVetmo.Value == 0;
                optSachnhiem.Checked = bbhc.PhanloaiVetmo.Value == 1;
                optNhiem.Checked = bbhc.PhanloaiVetmo.Value == 2;
                optBan.Checked = bbhc.PhanloaiVetmo.Value == 3;
                txtKhangsinhdukien.Text = bbhc.KhangsinhDukien;
                txtPhuongPhapPT._Text = bbhc.PhuongphapPttt;
                txtPhuongPhapVoCam._Text = bbhc.PhuongphapVocam;
                if (bbhc.DukienthoigianPttt.HasValue)
                    dtpNgayPTTT.Value = bbhc.DukienthoigianPttt.Value;
                else
                    dtpNgayPTTT.NullValue = DBNull.Value;
                txtChuanbichuyenbiet.Text = bbhc.ChuanbiChuyenbiet;
                txtTuthenguoibenh.Text = bbhc.TutheNguoibenh;
                txtDutrumau.Text = bbhc.DutruMau;
                txtBacsidexuat.Focus();
            }
            else
                AutoLoadData(2);
        }
        void FillBacsiPttt(string dataString, DataTable dtData, GridEX grdlist)
        {
            dtData.Clear();
            if (!string.IsNullOrEmpty(dataString) && dtData.Columns.Count > 0)
            {
                string[] rows = dataString.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dtData.NewRow();
                        newDr[DmucNhanvien.Columns.IdNhanvien] = Utility.Int16Dbnull(row, -1);
                        newDr[DmucNhanvien.Columns.TenNhanvien] = LaytenNvien(Utility.sDbnull(row, -1));
                        dtData.Rows.Add(newDr);
                        dtData.AcceptChanges();
                    }
                }
                grdlist.DataSource = dtData;
            }
        }
        public void ClearControl()
        {
            txtstt_rec.Text = string.Empty;
            txtBacsidexuat.SetCode("-1");
            //txtBSThamGia.Text = string.Empty; 
            txtHopTai._Text = string.Empty;
            autoChutoa.SetCode("-1");
            autoThuki.SetCode("-1");
            dtbsthamgia.Clear();
            txtYeuCauHoiChan.Text = string.Empty;
            txtChanDoanTuyenDuoiKKB.Text = string.Empty;
            txtTinhTrangLucVaoVien.Text = string.Empty; ;
            txtTsb_ngoaikhoa.Text = string.Empty;
            txtTsb_sankhoa.Text = string.Empty;
            txtTomTatDienBienBenh.Text = string.Empty;
            txtChanDoan.Text = string.Empty;
            txtPhuongPhapDieuTri.Text = string.Empty;
            txtChamSoc.Text = string.Empty; ;
            txtKetLuanChanDoan.Text = string.Empty;
            txtChanDoan.Text = string.Empty;
            txtTienLuong.Text = string.Empty;
            txtNguyenNhan.Text = string.Empty;
            txtQuaTrinhChamSoc.Text = string.Empty;
            txtQuaTrinhDieuTri.Text = string.Empty;
            txtKetLuanChanDoan.Text = string.Empty;
            txtKetLuanHuongXuLyTiep.Text = string.Empty;
            txtKetLuanTienLuong.Text = string.Empty;


        }
        private void ModifyCommand()
        {
            cmdIn.Enabled = m_enAct == action.Update;
            if (m_enAct == action.View)
                cmdThemMoiBN.Enabled = cmdSave.Enabled =cmdRefreshCDHA.Enabled=cmdRefreshChucnangsong.Enabled=cmdRefreshXN.Enabled= false;
        }


        private void btnPrintTrichBBHC_Click(object sender, EventArgs e)
        {
            //ReportDocument crpt = new ReportDocument();
            //string path = Utility.sDbnull(SystemReports.GetPathReport("TRICHBIENBANHOICHAN"));
            //if (File.Exists(path))
            //{
            //    crpt.Load(path);
            //}
            //else
            //{
            //    Utility.ShowMsg("Không tìm thấy file\n Mời bạn liên hệ với quản trị để update thêm file report", "Thông báo", MessageBoxIcon.Error);
            //}
            //DataSet dt = SPs.KcbLaythongtinBienbanhoichanIn(txtstt_rec.Text).GetDataSet();
            //DataTable db = dt.Tables[0];
            //if (dt != null && dt.Tables.Count > 0)
            //{
            //    dt.Tables[0].TableName = "TRICHBIENBANHOICHAN";
            //}
            ////dt.WriteXmlSchema("D:\\dsBienBanKiemThaoTuVong.xsd");
            //THU_VIEN_CHUNG.CreateXml(dt, "TRICHBIENBANHOICHAN.xml");
            //Utility.UpdateLogotoDatatable(ref db);

            //var objForm = new frmPrintPreview("TRÍCH BIÊN BẢN HỘI CHẨN", crpt, true, true);

            //crpt.SetDataSource(dt);
            //objForm.crptViewer.ReportSource = crpt;
            //objForm.crptTrinhKyName = Path.GetFileName(path);
            //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
            //                                                     "                                                                  "
            //                                                         .Replace("#$X$#",
            //                                                             Strings.Chr(34) + "&Chr(13)&" +
            //                                                             Strings.Chr(34)) + Strings.Chr(34);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name.ToUpper());
            //crpt.SetParameterValue("BranchName", globalVariables.Branch_Name.ToUpper());
            //crpt.SetParameterValue("BranchAddress", globalVariables.Branch_Address);
            //crpt.SetParameterValue("ReportTitle", "TRÍCH BIÊN BẢN HỘI CHẨN");
            //crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(DateTime.Now));
            //crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            //objForm.ShowDialog();
            //crpt.Close();
            //crpt.Dispose();
            //objForm.Dispose();
        }
        public static void InBienbanHoichan(DataTable mDtReport, string sTitleReport)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            string tieude = "", reportname = "";
            ReportDocument reportDocument = Utility.GetReport("TRICHBIENBANHOICHAN", ref tieude, ref reportname);
            if (reportDocument == null) return;
            ReportDocument crpt = reportDocument;
            THU_VIEN_CHUNG.CreateXML(mDtReport, "bbanhoichan.XML");
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "TRICHBIENBANHOICHAN";
                crpt.SetDataSource(mDtReport);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "HotLine", globalVariables.Branch_Fax);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                //if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                //{
                //    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                //    objForm.ShowDialog();
                //}
                //else
                //{
                //    objForm.addTrinhKy_OnFormLoad();
                //    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                //    crpt.PrintToPrinter(1, false, 0, 0);
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        private void btnPrinKcbBienbanhoichan_Click(object sender, EventArgs e)
        {
            //ReportDocument crpt = new ReportDocument();
            //string path = Utility.sDbnull(SystemReports.GetPathReport("SOHOICHAN"));
            //if (File.Exists(path))
            //{
            //    crpt.Load(path);
            //}
            //else
            //{
            //    Utility.ShowMsg("Không tìm thấy file\n Mời bạn liên hệ với quản trị để update thêm file report", "Thông báo", MessageBoxIcon.Error);
            //}
            //DataSet dt = SPs.SpSoHoiChan(txtstt_rec.Text).GetDataSet();
            //DataTable db = dt.Tables[0];
            //Utility.UpdateLogotoDatatable(ref db);
            //if (dt != null && dt.Tables.Count > 0)
            //{
            //    dt.Tables[0].TableName = "SOHOICHAN";
            //}
            ////dt.WriteXmlSchema("D:\\dsBienBanKiemThaoTuVong.xsd");
            //THU_VIEN_CHUNG.CreateXml(dt, "SOHOICHAN.xml");
            //var objForm = new frmPrintPreview("SỔ HỘI CHẨN", crpt, true, true);
            //crpt.SetDataSource(dt);
            //objForm.crptViewer.ReportSource = crpt;
            //objForm.crptTrinhKyName = Path.GetFileName(path);
            //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
            //                                                     "                                                                  "
            //                                                         .Replace("#$X$#",
            //                                                             Strings.Chr(34) + "&Chr(13)&" +
            //                                                             Strings.Chr(34)) + Strings.Chr(34);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name.ToUpper());
            //crpt.SetParameterValue("BranchName", globalVariables.Branch_Name.ToUpper());
            //crpt.SetParameterValue("BranchAddress", globalVariables.Branch_Address);
            //crpt.SetParameterValue("ReportTitle", "SỔ HỘI CHẨN");
            //crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(DateTime.Now));
            //crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            //objForm.ShowDialog();
            //crpt.Close();
            //crpt.Dispose();
            //objForm.Dispose();
        }


        private Boolean isValidData()
        {
            errorProvider1.Clear();
            //if (txtBacsidexuat.MyID == "-1")
            //{
            //    Utility.ShowMsg("Bạn phải nhập bác sĩ đề xuất hội chẩn");
            //    uiTabInfor.SelectedIndex = 0;
            //    errorProvider1.SetError(txtBacsidexuat, "Nhập thông tin");
            //    txtBacsidexuat.Focus();
            //    txtBacsidexuat.SelectAll();
            //    return false;
            //}
            //if (dtbsthamgia.Rows.Count <= 0)
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập ít nhất một bác sĩ tham gia hội chẩn");
            //    errorProvider1.SetError(autoBacsithamgia, "Nhập thông tin");
            //    autoBacsithamgia.Focus();
            //    autoBacsithamgia.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(txtHopTai.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập địa điểm họp hội chẩn");
            //    errorProvider1.SetError(txtHopTai, "Nhập thông tin");
            //    txtHopTai.Focus();
            //    txtHopTai.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autoChutoa.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập chủ tọa hội chẩn");
            //    errorProvider1.SetError(autoBacsithamgia, "Nhập thông tin");
            //    autoChutoa.Focus();
            //    autoChutoa.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autoThuki.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập thư kí hội chẩn");
            //    errorProvider1.SetError(autoThuki, "Nhập thông tin");
            //    autoThuki.Focus();
            //    autoThuki.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(txtYeuCauHoiChan.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập yêu cầu hội chẩn");
            //    errorProvider1.SetError(txtYeuCauHoiChan, "Nhập thông tin");
            //    txtYeuCauHoiChan.Focus();
            //    txtYeuCauHoiChan.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autoLydohc.MyCode) == "-1")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập lý do hội chẩn");
            //    errorProvider1.SetError(autoLydohc, "Nhập thông tin");
            //    autoLydohc.Focus();
            //    autoLydohc.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autohinhthuchc.MyCode) == "-1")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập hình thức hội chẩn");
            //    errorProvider1.SetError(autohinhthuchc, "Nhập thông tin");
            //    autohinhthuchc.Focus();
            //    autohinhthuchc.SelectAll();
            //    return false;
            //}
            return true;
        }


        void LoadUserConfigs()
        {
            try
            {
                chkPreview.Checked = Utility.getUserConfigValue(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked)) == 1;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked));

            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void frm_ThemBbanhoichan_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { autoLydohc.LOAI_DANHMUC, autohinhthuchc.LOAI_DANHMUC,txtPhuongPhapPT.LOAI_DANHMUC,txtPhuongPhapVoCam.LOAI_DANHMUC }, true);
            autoLydohc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydohc.LOAI_DANHMUC));
            autohinhthuchc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autohinhthuchc.LOAI_DANHMUC));
            txtPhuongPhapPT.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongPhapPT.LOAI_DANHMUC));
            txtPhuongPhapVoCam.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongPhapVoCam.LOAI_DANHMUC));
            Utility.SetDataSourceForDataGridEx(grd_bspt, dtbsphauthuat, false, true, "", "");
            Utility.SetDataSourceForDataGridEx(grd_bsgm, dtbsgayme, false, true, "", "");
            Utility.SetDataSourceForDataGridEx(grd_bsphauthuatphu, dtbsphauthuatphu, false, true, "", "");

            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtBacsidexuat.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            txtHopTai.Init(globalVariables.gv_dtDmucPhongban,
                             new List<string>
                                  {
                                      DmucKhoaphong.Columns.IdKhoaphong,
                                      DmucKhoaphong.Columns.MaKhoaphong,
                                      DmucKhoaphong.Columns.TenKhoaphong
                                  });
            autoChutoa.Init(txtBacsidexuat.AutoCompleteSource, txtBacsidexuat.defaultItem);
            autoThuki.Init(txtBacsidexuat.AutoCompleteSource, txtBacsidexuat.defaultItem);
            autoBacsithamgia.Init(txtBacsidexuat.AutoCompleteSource, txtBacsidexuat.defaultItem);
            autoBSPhauthuat.Init(txtBacsidexuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            autoBSGayme.Init(txtBacsidexuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            autoBSphu.Init(txtBacsidexuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            if (bbhc != null && m_enAct == action.Update)
            {
                FillData4Update();
            }
            else
            {
                ucThongtinnguoibenh_doc_v61.Refresh();
               
            }
            ModifyCommand();
        }
        void AutoLoadData(byte _loadtype)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Mời bạn chọn người bệnh cần hội chẩn");
                    ucThongtinnguoibenh_doc_v61.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_doc_v61.txtMaluotkham.SelectAll();
                    return;
                }
                List<string> lstAvailable = new List<string>() { "XQ", "SA", "DT", "CT", "MRI" };
                DataSet ds = new noitru_nhapvien().KcbLaythongtinKetquacls(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan), (byte)0);
                DataTable dtketquaXN = ds.Tables[0];
                DataTable dtketquaCDHA = ds.Tables[1];
                string[] querykq = null;
                if (_loadtype != 1)
                {
                    foreach (Control txt in pnlKQXN.Controls)
                    {
                        if (txt.GetType().Equals(typeof(EditBox)))
                        {
                            List<string> lstTags=Utility.sDbnull(txt.Tag, "UKN").ToUpper().Split('@').ToList<string>();
                            bool isAll = false;
                            string commonTag = lstTags[0];
                            string getTag = "";
                            if (lstTags.Count == 2)
                                getTag = lstTags[1];
                            else
                            {
                                getTag = commonTag;
                                isAll = true;
                            }
                            List<string> lstGetTags = getTag.Split(',').ToList<string>();
                            querykq = (from kq in dtketquaXN.AsEnumerable()
                                       let y = Utility.sDbnull(kq["co_chitiet"], "") == "1" && isAll ? Utility.sDbnull(kq["ten_ketqua"]) : Utility.sDbnull(kq["Ket_Qua"])
                                       where Utility.sDbnull(kq["mota_them_chitiet"], "").ToUpper() == commonTag
                                       && (isAll || lstGetTags.Contains(Utility.sDbnull(kq["ten_thongso"], "").ToUpper()))
                                       select y).ToArray();
                            txt.Text = string.Join("; ", querykq);
                        }
                    }
                }
                if (_loadtype != 0)
                {
                     querykq = (from kq in dtketquaCDHA.AsEnumerable()
                                        let y = string.Format("{0}:{1}", Utility.sDbnull(kq["ten_chitietdichvu"]), Utility.sDbnull(kq["ket_qua"]))
                                        where Utility.sDbnull(kq["ma_dichvu"]) == "XQ"
                                        select y).ToArray();
                    string ketquaCDHA = string.Join("; ", querykq);
                    txtXQ.Text = ketquaCDHA;
                    querykq = (from kq in dtketquaCDHA.AsEnumerable()
                               let y = string.Format("{0}:{1}", Utility.sDbnull(kq["ten_chitietdichvu"]), Utility.sDbnull(kq["ket_qua"]))
                               where Utility.sDbnull(kq["ma_dichvu"]) == "SA"
                               select y).ToArray();
                    ketquaCDHA = string.Join("; ", querykq);
                    txtSA.Text = ketquaCDHA;
                    querykq = (from kq in dtketquaCDHA.AsEnumerable()
                               let y = string.Format("{0}:{1}", Utility.sDbnull(kq["ten_chitietdichvu"]), Utility.sDbnull(kq["ket_qua"]))
                               where Utility.sDbnull(kq["ma_dichvu"]) == "DT"
                               select y).ToArray();
                    ketquaCDHA = string.Join("; ", querykq);
                    txtDientim.Text = ketquaCDHA;
                    querykq = (from kq in dtketquaCDHA.AsEnumerable()
                               let y = string.Format("{0}:{1}", Utility.sDbnull(kq["ten_chitietdichvu"]), Utility.sDbnull(kq["ket_qua"]))
                               where Utility.sDbnull(kq["ma_dichvu"]) == "CT"
                               select y).ToArray();
                    ketquaCDHA = string.Join("; ", querykq);
                    txtCT.Text = ketquaCDHA;
                    querykq = (from kq in dtketquaCDHA.AsEnumerable()
                               let y = string.Format("{0}:{1}", Utility.sDbnull(kq["ten_chitietdichvu"]), Utility.sDbnull(kq["ket_qua"]))
                               where Utility.sDbnull(kq["ma_dichvu"]) == "MRI"
                               select y).ToArray();
                    ketquaCDHA = string.Join("; ", querykq);
                    txtMRI.Text = ketquaCDHA;
                    querykq = (from kq in dtketquaCDHA.AsEnumerable()
                               let y = string.Format("{0}:{1}", Utility.sDbnull(kq["ten_chitietdichvu"]), Utility.sDbnull(kq["ket_qua"]))
                               where !lstAvailable.Contains(Utility.sDbnull(kq["ma_dichvu"]))
                               select y).ToArray();
                    ketquaCDHA = string.Join("; ", querykq);
                    txtCDHAKhac.Text = ketquaCDHA;
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string LaysoBBHC()
        {
            string ma_bbhc = "";
            StoredProcedure sp = SPs.SpGetMaBBHC(DateTime.Now.Year, ma_bbhc);
            sp.Execute();
            return Utility.sDbnull(sp.OutputValues[0], "-1");
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData()) return;
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu biên bản hội chẩn?", "Thông báo", true)) return;
                if (bbhc == null) bbhc = new KcbBienbanhoichan();
                if (bbhc.Id <= 0)
                {
                    bbhc.IsNew = true;
                    bbhc.MaBbhc = LaysoBBHC();

                    bbhc.NguoiTao = globalVariables.UserName;
                    bbhc.NgayTao = globalVariables.SysDate;
                    bbhc.MacMaytao = globalVariables.gv_strMacAddress;
                    bbhc.IpMaytao = globalVariables.gv_strIPAddress;
                }
                else
                {
                    bbhc.MarkOld();
                    bbhc.IsNew = false;

                    bbhc.NguoiSua = globalVariables.UserName;
                    bbhc.NgaySua = globalVariables.SysDate;
                    bbhc.MacMaysua = globalVariables.gv_strMacAddress;
                    bbhc.IpMaysua = globalVariables.gv_strIPAddress;
                }
                bbhc.IdBenhnhan = objLuotkham.IdBenhnhan;
                bbhc.MaLuotkham = objLuotkham.MaLuotkham;
                bbhc.MaLydoHc = autoLydohc.MyCode;
                bbhc.MaHinhthucHc = autohinhthuchc.MyCode;
                bbhc.BacsiDexuat = Utility.Int32Dbnull(txtBacsidexuat.MyID, -1);
                bbhc.NgayHoichan = dtpNgayhoichan.Value;
                bbhc.BacsiThamgia = getBacsithamgia();
                bbhc.HopTai = txtHopTai.Text;
                bbhc.ChuToa = autoChutoa.Text;
                bbhc.ThuKy = autoThuki.Text;
                bbhc.YeucauHoichan = txtYeuCauHoiChan.Text;
                bbhc.TsbNgoaikhoa =Utility.sDbnull( txtTsb_ngoaikhoa.Text);
                bbhc.TsbSankhoa = Utility.sDbnull(txtTsb_sankhoa.Text);
                bbhc.TrangthaiVaovien = txtTinhTrangLucVaoVien.Text;
                bbhc.ChanDoan = txtChanDoanTuyenDuoiKKB.Text;
                bbhc.DienbienBenh = Utility.sDbnull(txtTomTatDienBienBenh.Text);
                bbhc.ChandoanNguyennhanTienluong = txtChanDoan.Text;
                bbhc.Pphapdieutri = txtPhuongPhapDieuTri.Text;
                bbhc.ChamSoc = txtChamSoc.Text;

                bbhc.KetLuan = txtKetLuanChanDoan.Text;
                bbhc.KetluanChandoan = txtKetLuanChanDoan.Text;
                bbhc.Huongxuly = txtKetLuanHuongXuLyTiep.Text;
                bbhc.KetluanTienluong = txtKetLuanTienLuong.Text;

                bbhc.QuatrinhChamsoc = txtQuaTrinhChamSoc.Text;
                bbhc.QuatrinhDieutri = txtQuaTrinhDieuTri.Text;

                bbhc.TienLuong = txtTienLuong.Text;
                bbhc.NguyenNhan = txtNguyenNhan.Text;
                bbhc.ChandoanSaukham = txtChanDoan.Text;
                bbhc.MaBbhc = txtstt_rec.Text;
                //trường mới
                bbhc.TsbNoikhoa = Utility.sDbnull(txtTsb_noikhoa.Text);
                bbhc.TsbKhac = Utility.sDbnull(txttsb_khac.Text);

                bbhc.Mach = txtMach.Text;
                bbhc.Ha = txtha.Text;
                bbhc.NhietDo = txtNhietDo.Text;
                bbhc.Cao = txtChieucao.Text;
                bbhc.CanNang = txtCannang.Text;
                bbhc.Bmi = txtBmi.Text;
                bbhc.Nhommau = txtNhommau.Text;
                bbhc.ToanThan = txtToanthan.Text;

                bbhc.ToanThan = Utility.sDbnull(txtToanthan.Text);
                bbhc.TrieuchungConang = Utility.sDbnull(txtTrieuchungconang.Text);
                bbhc.TrieuchungThucthe = txtTrieuchungthucthe.Text;
                bbhc.Timach = (byte)(optkhamtimmach_binhthuong.Checked ? 0 : (optkhamtimmach_batthuong.Checked ? 1 : 2));
                bbhc.TimmachKhac = txtkhamtimmach_khac.Text;
                bbhc.Hohap = (byte)(optkhamhohap_binhthuong.Checked ? 0 : (optkhamhohap_COPD.Checked ? 1 : 2));
                bbhc.HohapKhac = txtkhamhohap_khac.Text;

                bbhc.XnHct = txtHCT.Text;
                bbhc.XnHc = txtHC.Text;
                bbhc.XnBc = txtBC.Text;
                bbhc.XnTieucau = txtTC.Text;
                bbhc.XnTqr = txtTqr.Text;
                bbhc.XnTckr = txtTckr.Text;
                bbhc.XnRh = txtRh.Text;
                bbhc.XnHiv = txtHiv.Text;
                bbhc.XnHcv = txtHCV.Text;
                bbhc.XnHbsAg = txtHbsag.Text;
                bbhc.XnQuicktest = txtQuicktest.Text;
                bbhc.XnGlucose = txtGlucose.Text;
                bbhc.XnUre = txtUre.Text;
                bbhc.XnCreatinin = txtCreatinin.Text;
                bbhc.XnAst = txtAst.Text;
                bbhc.XnAlt = txtALT.Text;
                bbhc.XnNuoctieu = txtNuoctieu.Text;

                bbhc.CdhaXq = txtXQ.Text;
                bbhc.CdhaSa = txtSA.Text;
                bbhc.CdhaDientim = txtDientim.Text;
                bbhc.CdhaCt = txtCT.Text;
                bbhc.CdhaMri = txtMRI.Text;
                bbhc.CdhaKhac = txtCDHAKhac.Text;

                bbhc.ChanDoan = txtChandoan_new.Text;
                bbhc.BenhlyKemtheo = txtBenhlykemtheo.Text;
                bbhc.PhanloaiVetmo = (byte)(optSach.Checked ? 0 : (optSachnhiem.Checked ? 1 : (optNhiem.Checked ? 2 : 3)));
                bbhc.KhangsinhDukien = txtKhangsinhdukien.Text;
                bbhc.PhuongphapPttt = txtPhuongPhapPT.Text;
                bbhc.PhuongphapVocam = txtPhuongPhapVoCam.Text;
                bbhc.DukienthoigianPttt = dtpNgayPTTT.Value;
                bbhc.ChuanbiChuyenbiet = txtChuanbichuyenbiet.Text;
                bbhc.TutheNguoibenh = txtTuthenguoibenh.Text;
                bbhc.DutruMau = txtDutrumau.Text;
                bbhc.IdbacsiGayme = getBacsithamgia(dtbsgayme);
                bbhc.IdbacsiPttt = getBacsithamgia(dtbsphauthuat);
                bbhc.IdbacsiPtttPhu = getBacsithamgia(dtbsphauthuatphu);
                bbhc.Save();
                if (Utility.sDbnull(bbhc.Nhommau).Length > 0)
                    new Update(KcbDanhsachBenhnhan.Schema).Set(KcbDanhsachBenhnhan.Columns.NhomMau).EqualTo(bbhc.Nhommau).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(bbhc.IdBenhnhan).Execute();
                txtID.Text = bbhc.Id.ToString();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới biên bản hội chẩn bệnh nhân: {0}-{1} thành công", bbhc.MaLuotkham, ucThongtinnguoibenh_doc_v61.txtTenBN.Text), bbhc.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã thêm mới biên bản hội chẩn thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật sổ hội chẩn bệnh nhân: {0}-{1} thành công", bbhc.MaLuotkham, ucThongtinnguoibenh_doc_v61.txtTenBN.Text), bbhc.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã Cập nhật biên bản hội chẩn thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
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
        string getBacsithamgia(DataTable dtData)
        {
            var q = from p in dtData.AsEnumerable()
                    select Utility.sDbnull(p["id_nhanvien"], "");
            return string.Join(",", q.ToArray<string>());
        }
        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            if (m_enAct == action.Insert)
            {
                if (!Utility.AcceptQuestion("Bạn đang ở trạng thái thêm mới biên bản hội chẩn và có thể đã nhập một số thông tin. Nếu nhấn thêm mới các thông tin mới nhập có thể bị xóa.\nBạn có chắc chắn muốn làm lại từ đầu không?", "Xác nhận", true))
                {
                    return;
                }
            }
            m_enAct = action.Insert;
            cmdIn.Enabled = m_enAct == action.Update;
            ClearControl();
            ucThongtinnguoibenh_doc_v61.txtMaluotkham.Focus();
            ucThongtinnguoibenh_doc_v61.txtMaluotkham.SelectAll();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {

            try
            {
                if (bbhc == null || bbhc.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo biên bản hội chẩn trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.KcbLaythongtinBienbanhoichanIn(bbhc.Id).GetDataSet().Tables[0];

                List<string> lstAddedFields = new List<string>() { "khamtimmach_binhthuong", "khamtimmach_batthuong", "khamtimmach_khac",
                "khamhohap_binhthuong", "khamhohap_copd", "khamhohap_khac",
                "phanloaivetmo_sach","phanloaivetmo_sachnhiem",  
                "phanloaivetmo_nhiem", "phanloaivetmo_ban"};

                dtData.TableName = "kcb_bienbanhoichan";
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = "BIÊN BẢN HỘI CHẨN";
                drData["sngay_hoichan_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(bbhc.NgayHoichan,"");
                drData["sngay_hoichan"] = Utility.FormatDateTime(bbhc.NgayHoichan);
                drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien,"");
                drData["sngay_dukienpttt"] = Utility.FormatDateTime_giophut_ngay_thang_nam(bbhc.DukienthoigianPttt.Value,"");
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("khamtimmach_binhthuong", bbhc.Timach.Value==0?"1":"0");
                dicMF.Add("khamtimmach_batthuong", bbhc.Timach.Value == 1 ? "1" : "0");
                dicMF.Add("khamtimmach_khac", bbhc.Timach.Value == 2 ? "1" : "0");
                dicMF.Add("khamhohap_binhthuong", bbhc.Hohap.Value == 0 ? "1" : "0");
                dicMF.Add("khamhohap_copd", bbhc.Hohap.Value == 1 ? "1" : "0");
                dicMF.Add("khamhohap_khac", bbhc.Hohap.Value == 2 ? "1" : "0");
                dicMF.Add("phanloaivetmo_sach", bbhc.PhanloaiVetmo.Value == 0 ? "1" : "0");
                dicMF.Add("phanloaivetmo_sachnhiem", bbhc.PhanloaiVetmo.Value == 1 ? "1" : "0");
                dicMF.Add("phanloaivetmo_nhiem", bbhc.PhanloaiVetmo.Value == 2 ? "1" : "0");
                dicMF.Add("phanloaivetmo_ban", bbhc.PhanloaiVetmo.Value == 3 ? "1" : "0");
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\BIENBAN_HOICHAN.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("BIENBAN_HOICHAN", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Biên bản hội chẩn tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "BIENBAN_HOICHAN", objLuotkham.MaLuotkham, Utility.sDbnull(bbhc.Id), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
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
            //try
            //    {
            //    DataTable dtData = SPs.KcbLaythongtinBienbanhoichanIn(bbhc.Id).GetDataSet().Tables[0];
            //    dtData.TableName = "kcb_bienbanhoichan";
            //    THU_VIEN_CHUNG.CreateXML(dtData, "kcb_bienbanhoichan.xml");
            //    if (dtData == null || dtData.Rows.Count <= 0)
            //    {
            //        Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    noitru_inphieu.InBienbanHoichan(dtData, DateTime.Now, chkPreview.Checked, "noitru_bienbanhoichan");
            //}
            //catch (Exception ex)
            //{

            //    Utility.CatchException(ex);
            //}
        }

        private void cmdRefreshCDHA_Click(object sender, EventArgs e)
        {
            AutoLoadData(1);
        }

        private void cmdRefreshXN_Click(object sender, EventArgs e)
        {
            AutoLoadData(0);
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
            txtha.Text = huyetap;
            txtChieucao.Text = chieucao;
            txtCannang.Text = cannang;
            txtBmi.Text = bmi;
            txtNhommau.Text = nhommau;
        }

        private void grpChucNangSong_Enter(object sender, EventArgs e)
        {

        }

        private void cmdViewKQCLS_Click(object sender, EventArgs e)
        {
            frm_XemKQCLS _XemKQCLS = new frm_XemKQCLS(objLuotkham, 100);
            _XemKQCLS.ShowDialog();
        }
        
    }
}
