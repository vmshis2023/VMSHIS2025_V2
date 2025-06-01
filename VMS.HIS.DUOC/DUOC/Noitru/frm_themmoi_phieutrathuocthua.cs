using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using Microsoft.VisualBasic;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.NGHIEPVU.THUOC;
using Janus.Windows.GridEX;
using VNS.Properties;
using VNS.HIS.UI.Baocao;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_phieutrathuocthua : Form
    {
        bool hasLoaded = false;
        public long idPhieutra=-1;
        private DataSet m_dsData = new DataSet();
        string  kieu_thuoc_vt="THUOC";
        public action m_enAction = action.Insert;
        public delegate void OnInsertCompleted(long idcapphat);
        public event OnInsertCompleted _OnInsertCompleted;
        public DataTable m_dtData = new DataTable();
        TPhieutrathuocthua _item = null;
        bool firstLoad = true;//Dùng cho trường hợp Update tránh báo thông tin khác khoa,kho khi mới vào form
        public frm_themmoi_phieutrathuocthua(string kieu_thuoc_vt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtpInputDate.Value = globalVariables.SysDate;
            this.kieu_thuoc_vt = kieu_thuoc_vt;
            InitEvents();
            CauHinh();
        }
        
        void InitEvents()
        {
            this.Load += new System.EventHandler(this.frm_themmoi_phieutrathuocthua_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_phieutrathuocthua_KeyDown);
            cmdCheck.Click += cmdCheck_Click;
            cmdAccept.Click += cmdAccept_Click;
            mnuViewDetail.CheckedChanged += mnuViewDetail_CheckedChanged;
            cmdSave.Click += cmdSave_Click;
            grdPres.RowCheckStateChanged += grdPres_RowCheckStateChanged;
            grdPres.ColumnHeaderClick += grdPres_ColumnHeaderClick;
            //grdPres.check
            grdChitiet.RowCheckStateChanged += grdChitiet_RowCheckStateChanged;
            cmdPrint.Click += cmdPrint_Click;
            cmdExit.Click += cmdExit_Click;
            cmdConfig.Click += cmdConfig_Click;
            chkInngaysaukhiluu.CheckedChanged += chkInngaysaukhiluu_CheckedChanged;
            mnuTonghoplai.Click += mnuTonghoplai_Click;
            lnkTonghoplai.Click += lnkTonghoplai_Click;
            txtKhoanoitru._OnEnterMe += txtKhoanoitru__OnEnterMe;
        }

        void txtKhoanoitru__OnEnterMe()
        {
            try
            {
                short IDKhoa = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
                object myid = txtKhothuoc.MyID;
                //DataRow[] arrdr = m_dtKhonhan.Select("ID_KHOAPHONG=" + IDKhoa + " or id_khoaphong_captren=" + IDKhoa);
                //DataTable _newTable = m_dtKhonhan.Clone();
                //if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();
                DataTable _newTable = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, -1,"ALL", kieu_thuoc_vt, 100, 100);// CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, IDKhoa, "ALL", KIEU_THUOC_VT, 100, 100);
                txtKhothuoc.Init(_newTable, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
                txtKhothuoc.SetId(myid);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void lnkTonghoplai_Click(object sender, EventArgs e)
        {
            GetData(1);
        }

        void mnuTonghoplai_Click(object sender, EventArgs e)
        {
            GetData(1);
        }

        void chkInngaysaukhiluu_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._TrathuocthuaProperties.Insaukhiluu = chkInngaysaukhiluu.Checked;
            PropertyLib.SaveProperty(PropertyLib._TrathuocthuaProperties);
        }

        void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties(PropertyLib._TrathuocthuaProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception exception)
            {

            }
        }
        void CauHinh()
        {
            chkInngaysaukhiluu.Checked = PropertyLib._TrathuocthuaProperties.Insaukhiluu;
        }
        void cmdPrint_Click(object sender, EventArgs e)
        {

            TPhieutrathuocthua objPhieutrathuocthua = TPhieutrathuocthua.FetchByID(idPhieutra);
            if (objPhieutrathuocthua != null)
            {
                thuoc_baocao.InPhieutrathuocthua(objPhieutrathuocthua, "PHIẾU TRẢ THUỐC THỪA", globalVariables.SysDate);
            }
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void grdChitiet_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            ModifyCommands();
        }

        void grdPres_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                foreach (GridEXRow prow in grdPres.GetDataRows())
                {
                    foreach (GridEXRow exRow in grdChitiet.GetDataRows())
                    {
                        exRow.BeginEdit();
                        if (Utility.Int32Dbnull(exRow.Cells[TPhieuCapphatChitiet.Columns.IdThuoc].Value, -1) ==
                            Utility.Int32Dbnull(prow.Cells[TPhieuCapphatChitiet.Columns.IdThuoc].Value))
                        {
                            exRow.IsChecked = prow.IsChecked;
                            //if (Utility.Int32Dbnull(exRow.Cells[TPhieuCapphatChitiet.Columns.TrangthaiTralai].Value, 0) == 0)
                            //{
                            //    exRow.IsChecked = true;
                            //}
                            //else
                            //{
                            //    exRow.IsChecked = false;
                            //}
                        }
                        exRow.EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                
               
            }
            //try
            //{
            //    grdPres.RowCheckStateChanged -= grdPres_RowCheckStateChanged;
            //    foreach (GridEXRow exRow in grdChitiet.GetDataRows())
            //    {
            //        exRow.BeginEdit();
            //        if (Utility.Int32Dbnull(exRow.Cells[TPhieuCapphatChitiet.Columns.IdThuoc].Value, -1) ==
            //            Utility.Int32Dbnull(grdPres.GetValue(TPhieuCapphatChitiet.Columns.IdThuoc)))
            //        {
            //            if (Utility.Int32Dbnull(exRow.Cells[TPhieuCapphatChitiet.Columns.TrangthaiTralai].Value, 0) == 0)
            //            {
            //                exRow.CheckState = exRow.CheckState;
            //            }
            //            else
            //            {
            //                exRow.IsChecked = false;
            //            }
            //        }
            //        exRow.EndEdit();
            //    }
            //    grdPres.RowCheckStateChanged += grdPres_RowCheckStateChanged;
            //}
            //catch (Exception)
            //{
            //    Utility.ShowMsg("Có lỗi trong quá trình xử lý thông tin");
            //}
            //finally
            //{
            //    ModifyCommands();
            //}
        }

        void grdPres_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            try
            {
                bool _datralai = false;
                foreach (GridEXRow exRow in grdChitiet.GetDataRows())
                {
                    exRow.BeginEdit();
                    if (Utility.Int32Dbnull(exRow.Cells[TPhieuCapphatChitiet.Columns.IdThuoc].Value, -1) ==
                        Utility.Int32Dbnull(grdPres.GetValue(TPhieuCapphatChitiet.Columns.IdThuoc)))
                    {
                        exRow.IsChecked = grdPres.CurrentRow.IsChecked;
                        //if (Utility.Int32Dbnull(exRow.Cells[TPhieuCapphatChitiet.Columns.TrangthaiTralai].Value, 0) == 0)
                        //{
                        //    exRow.CheckState = e.CheckState;
                        //}
                        //else
                        //{
                        //    _datralai = true;
                        //    exRow.IsChecked = false;
                        //}
                    }
                    exRow.EndEdit();
                }
                if (_datralai && m_enAction==action.Insert)
                    Utility.ShowMsg("Chú ý: Hệ thống phát hiện một số chi tiết thuốc đã được tổng hợp trả lại nên không được tổng hợp vào phiếu trả thuốc thừa đang tạo. Bạn có thể nháy chuột phải chọn mục xem chi tiết để kiểm tra lại");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }

        void cmdSave_Click(object sender, EventArgs e)
        {

            if (!IsValidData())
                return;
            List<long> lstItems = getDetails();
            TPhieutrathuocthua _item = getPhieu();
            ActionResult act = ActionResult.Success;
            if (m_enAction == action.Insert)
            {
               act= new Trathuocthua().ThemPhieutrathuocthua(_item, lstItems);
               if (act == ActionResult.Success)
               {
                 
                   idPhieutra = _item.Id;
                   cmdPrint.Enabled = true;
                   ProcessDataInsert(_item);
                   if (_OnInsertCompleted != null) _OnInsertCompleted(idPhieutra);
                   cmdPrint.Focus();
                   if (chkInngaysaukhiluu.Checked) cmdPrint_Click(cmdPrint, e);
                   
               }
              
            }
            else
            {
                act = new Trathuocthua().CappnhatPhieuTrathuocthua(_item, lstItems);
            }
            if (PropertyLib._TrathuocthuaProperties.ThoatsaukhiLuu == 0)
            {
                m_enAction = action.Update;
            }
            else if (PropertyLib._TrathuocthuaProperties.ThoatsaukhiLuu == 1)
            {
                this.Close();
            }
            else
            {
                m_enAction = action.Insert;
                idPhieutra = -1;
            }
            lnkTonghoplai.Visible = m_enAction == action.Update;
            cmdAccept.ContextMenuStrip = m_enAction == action.Update ? contextMenuStrip2 : null;
        }
        void ProcessDataInsert(TPhieutrathuocthua _item)
        {
            DataRow drNew = m_dtData.NewRow();
            drNew[TPhieutrathuocthua.Columns.NgayLapphieu] = _item.NgayLapphieu;
            drNew[TPhieutrathuocthua.Columns.Id] = _item.Id;
            drNew[TPhieutrathuocthua.Columns.IdKhonhan] = _item.IdKhonhan;
            drNew[TPhieutrathuocthua.Columns.IdKhoatra] = _item.IdKhoatra;
            drNew[TPhieutrathuocthua.Columns.NguoiLapphieu] = _item.NguoiLapphieu;
            drNew[TPhieutrathuocthua.Columns.TrangThai] = _item.TrangThai;
            drNew[TPhieutrathuocthua.Columns.NguoiTao] = _item.NguoiTao;
            drNew[TPhieutrathuocthua.Columns.NgayTao] = _item.NgayTao;
            if (m_enAction == action.Update)
            {
                drNew[TPhieutrathuocthua.Columns.NguoiSua] = _item.NguoiSua;
                drNew[TPhieutrathuocthua.Columns.NgaySua] = _item.NgaySua;
            }
            drNew["ten_khoanoitru"] = txtKhoanoitru.Text;
            drNew["ten_kho"] = txtKhothuoc.Text;
            drNew["ten_khoanoitru"] = txtKhoanoitru.Text;
            drNew["sngay_lapphieu"] = _item.NgayLapphieu.ToString("dd/MM/yyyy");
            drNew["sngay_tra"] = "";
           
            m_dtData.Rows.InsertAt(drNew, 0);
            m_dtData.AcceptChanges();

        }
        /// <summary>
        /// kiểm tra thông tin 
        /// </summary>
        /// <returns></returns>
        bool IsValidData()
        {
            try
            {
                Utility.SetMsg(uiStatusBar2.Panels["Msg"], "", true);
                if (grdChitiet.GetCheckedRows().Length <= 0)
                {
                    Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn cần chọn ít nhất một thuốc để trả lại", true);
                    return false;
                }
               
                if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
                {
                    Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn phải chọn khoa nội trú", true);
                    txtKhoanoitru.Focus();
                    return false;
                }

                if (Utility.Int32Dbnull(txtNhanvien.MyID, -1) == -1)
                {
                    Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn phải chọn người lập phiếu", true);
                    txtNhanvien.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtKhothuoc.MyID, -1) == -1)
                {
                    Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn phải chọn kho nhận lại thuốc thừa", true);
                    txtKhothuoc.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra sự hợp lệ của dữ liệu trươc khi "+(m_enAction==action.Insert?"thêm mới":"cập nhật"),ex);
                return false;
            }

        }
        TPhieutrathuocthua getPhieu()
        {
           _item = new TPhieutrathuocthua();
            if (m_enAction == action.Update) _item = TPhieutrathuocthua.FetchByID(idPhieutra);
            _item.MaPhieu = "";
            _item.NgayLapphieu = dtpInputDate.Value;
            _item.NguoiLapphieu =Utility.Int16Dbnull( txtNhanvien.MyID,-1);
            _item.IdKhoatra = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
            _item.IdKhonhan = Utility.Int16Dbnull(txtKhothuoc.MyID, -1);
           
            if (m_enAction == action.Insert)
            {
                _item.KieuThuocVt = kieu_thuoc_vt;
                _item.TrangThai = 0;
                _item.NguoiTao = globalVariables.UserName;
                _item.NgayTao = globalVariables.SysDate;
               
            }
            else
            {
                _item.NguoiSua = globalVariables.UserName;
                _item.NgaySua = globalVariables.SysDate;
            }
            return _item;
        }
        List<long> getDetails()
        {
            List<long> lstItems = new List<long>();
            foreach (GridEXRow gridExRow in grdChitiet.GetCheckedRows())
            {
                long IdCapphatChitiet = Utility.Int64Dbnull(gridExRow.Cells[TPhieuCapphatChitiet.Columns.IdChitiet].Value, -1);
                lstItems.Add(IdCapphatChitiet);
            }
            return lstItems;
        }
        void mnuViewDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (mnuViewDetail.Checked) grdChitiet.BringToFront();
            else
                grdChitiet.SendToBack();
            Application.DoEvents();
        }

        void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["Msg"], "", true);
            if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
            {
                Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn phải chọn khoa nội trú", true);
                txtKhoanoitru.Focus();
                return;
            }

            if (Utility.Int32Dbnull(txtNhanvien.MyID, -1) == -1)
            {
                Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn phải chọn người lập phiếu", true);
                txtNhanvien.Focus();
                return;
            }
            if (Utility.Int32Dbnull(txtKhothuoc.MyID, -1) == -1)
            {
                Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Bạn phải chọn kho nhận lại thuốc thừa",true);
                txtKhothuoc.Focus();
                return;
            }
            SetStatus();
        }
        void SetStatus()
        {
            if (m_enAction == action.Insert)
            {
                switch (cmdAccept.Tag.ToString())
                {
                    case "0":
                        EnabledControls(false);
                        SearchData();
                        break;
                    case "1":
                        EnabledControls(true);
                        break;
                }
            }
            else
            {
                if (_item != null)
                {
                    if (!firstLoad && (_item.IdKhoatra != Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) || _item.IdKhonhan != Utility.Int32Dbnull(txtKhothuoc.MyID, -1)))
                    {
                        if (Utility.AcceptQuestion("Bạn đang thay đổi khoa nội trú(hoặc kho nhận). Hệ thống sẽ lấy lại dữ liệu thuốc thừa theo sự thay đổi này.\nBạn đã chắc chắn?", "Xác nhận chọn lại", true))
                            return;
                        SearchData();
                    }
                    else
                    {
                        GetData(0);
                    }
                    firstLoad = true;
                }
            }
        }
        void GetData(byte tonghoplai)
        {
            DataSet dsData = DuocNoitru.ThuocNoitruLayChitietPhieutrathuocthua((int)idPhieutra, tonghoplai);
            Utility.SetDataSourceForDataGridEx(grdPres, dsData.Tables[0], false, true, "1=1", "");
            Utility.SetDataSourceForDataGridEx(grdChitiet, dsData.Tables[1], false, true, "1=1", "");
            grdPres.CheckAllRecords();
            foreach (GridEXRow exRow in grdChitiet.GetDataRows())
            {
                exRow.BeginEdit();
                exRow.IsChecked = Utility.Int32Dbnull(exRow.Cells["_checked"].Value, 0) == 1;
                exRow.EndEdit();
            }
            dtpInputDate.Value = _item.NgayLapphieu;
            txtNhanvien.SetId(Utility.Int16Dbnull(_item.NguoiLapphieu, -1));
            txtKhoanoitru.SetId(Utility.Int16Dbnull(_item.IdKhoatra, -1));
            txtKhothuoc.SetId(Utility.Int16Dbnull(_item.IdKhonhan, -1));

        }
        /// <summary>
        /// hàm thực hiện trạng thái của nút thông tin 
        /// </summary>
        /// <param name="visible"></param>
        void EnabledControls(bool visible)
        {
            dtpInputDate.Enabled = visible;
            txtNhanvien.Enabled = visible;
            txtKhoanoitru.Enabled = visible;
            txtKhothuoc.Enabled = visible;
            grpAct.Enabled = !visible;
            if (m_enAction == action.Insert)
            {
                cmdAccept.Tag = visible ? "0" : "1";
                cmdAccept.Text = visible ? "Chấp nhận" : "Chọn lại";
            }
        }
        void cmdCheck_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        private void frm_themmoi_phieutrathuocthua_Load(object sender, EventArgs e)
        {
            cmdCheck.Visible = globalVariables.IsAdmin;
            InitData();
            ResetDataByStatus();

        }
        void ResetDataByStatus()
        {
            if (m_enAction == action.Update) _item = TPhieutrathuocthua.FetchByID(idPhieutra);
            cmdAccept.Tag = m_enAction == action.Update ? "0" : "1";
            lnkTonghoplai.Visible = m_enAction == action.Update;
            cmdAccept.ContextMenuStrip = m_enAction == action.Update ? contextMenuStrip2 : null;
            SetStatus();
            ModifyCommands();
        }
        private void InitData()
        {
            try
            {
                DataTable m_dtKhoaNoitru = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);// THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                txtKhoanoitru.Init(m_dtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                txtKhoanoitru.SetId(globalVariables.idKhoatheoMay);
                txtNhanvien.Enabled = globalVariables.IsAdmin;
                DataTable dtKho = new DataTable();
                dtKho = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(-1, -1, "ALL", kieu_thuoc_vt, 100, 100);
                //if (kieu_thuoc_vt == "THUOC")
                //{
                //    dtKho = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU", "THUOC,THUOCVT", "CHANLE,LE", 100, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_TUTRUC_NOITRU(kieu_thuoc_vt);
                //}
                //else
                //{
                //    dtKho = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU", "VT,THUOCVT", "CHANLE,LE", 100, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NOITRU" });
                //}
             
                txtKhothuoc.Init(dtKho, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
                txtNhanvien.Init(THU_VIEN_CHUNG.Laydanhsachnhanvien("ALL"), new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                txtNhanvien.SetId(globalVariables.gv_intIDNhanvien);
                txtKhoanoitru.RaiseEnterEvents();
            }
            catch
            {
            }

        }
        private void SearchData()
        {
            try
            {
                hasLoaded = false;
                int id_khoanoitru = Utility.Int32Dbnull(txtKhoanoitru.MyID, -1);
                int id_kho = Utility.Int32Dbnull(txtKhothuoc.MyID, -1);
                m_dsData.Clear();
                grdPres.DataSource = null;
                grdChitiet.DataSource = null;
                m_dsData = DuocNoitru.ThuocNoitruTimkiemThuocthuatralai(id_khoanoitru, id_kho, kieu_thuoc_vt);
                Utility.SetDataSourceForDataGridEx_Basic(grdPres, m_dsData.Tables[0], true, true, "1=1", "");
                Utility.SetDataSourceForDataGridEx_Basic(grdChitiet, m_dsData.Tables[1], true, true, "1=1", "ten_thuoc");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                hasLoaded = true;
                ModifyCommands();
            }
            

        }
        private void ModifyCommands()
        {
            cmdSave.Enabled =(m_enAction==action.Update || cmdAccept.Tag.ToString()=="1") && grdChitiet.GetCheckedRows().Length > 0;
            cmdPrint.Enabled = cmdSave.Enabled;

        }
        

        private void frm_themmoi_phieutrathuocthua_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
            if (e.Control && e.KeyCode == Keys.P) cmdPrint_Click(cmdPrint, new EventArgs());
        }

        private void cmdAccept_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
