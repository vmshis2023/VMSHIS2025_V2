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



namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhatThuocBN_Noitru : Form
    {
        private DataTable m_Donthuoc = new DataTable();
        bool hasLoaded = false;
        private int idPhieucapphat=-1;
        private DataTable m_Thuoc = new DataTable();
        string  kieu_thuoc_vt="THUOC";
        public frm_PhatThuocBN_Noitru(string kieu_thuoc_vt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.kieu_thuoc_vt = kieu_thuoc_vt;
            InitEvents();
        }
        public void Startup(string idcapphat)
        {
            txtIDCapPhat.Text = idcapphat;
            txtIDCapPhat_KeyDown(txtIDCapPhat, new KeyEventArgs(Keys.Enter));
        }
        void InitEvents()
        {
            this.Load += new System.EventHandler(this.frm_PhatThuocBN_Noitru_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_PhatThuocBN_Noitru_KeyDown);
            txtIDCapPhat.KeyDown+=txtIDCapPhat_KeyDown;
            grdPres.SelectionChanged += grdPres_SelectionChanged;
            grdPres.MouseDoubleClick += grdPres_MouseDoubleClick;
            cmdHuyXacNhan.Click += cmdHuyXacNhan_Click;
            cmdXacNhan.Click += cmdXacNhan_Click;
            optAll.CheckedChanged += _CheckedChanged;
            optDalinh.CheckedChanged += _CheckedChanged;
            optChualinh.CheckedChanged += _CheckedChanged;
            txtBenhnhan._OnSelectionChanged += txtBenhnhan__OnSelectionChanged;
            txtBenhnhan._OnEnterMe += txtBenhnhan__OnEnterMe;
            grdChitiet.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_THUCLINH_PHATTHUOC_BENHNHAN", "0", false) == "1";
            grdChitiet.RootTable.Columns[TPhieuCapphatChitiet.Columns.SoLuongtralai].Visible = !grdChitiet.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible;
            grdChitiet.UpdatingCell += grdChitiet_UpdatingCell;

            mnuLinhthuocCurrent.Click += new EventHandler(mnuLinhthuocCurrent_Click);
            mnuLinhthuocAll.Click += new EventHandler(mnuLinhthuocAll_Click);
            mnuHuyLinhthuocCurrent.Click += new EventHandler(mnuHuyLinhthuocCurrent_Click);
            mnuHuyLinhthuocAll.Click += new EventHandler(mnuHuyLinhthuocAll_Click);

        }

        void grdPres_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdPres_SelectionChanged(sender, new EventArgs());
        }

        void txtBenhnhan__OnEnterMe()
        {
            cmdXacNhan.Focus();   
        }
        void mnuHuyLinhthuocAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                List<int> lstNoValidData = new List<int>();
                List<int> lstID_Donthuoc = (from p in grdPres.GetDataRows().AsEnumerable()
                                            where Utility.Int32Dbnull(((DataRowView)p.DataRow)[TPhieuCapphatChitiet.Columns.DaLinh], -1) == 1
                                            select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
                if (lstID_Donthuoc.Count <= 0) return;
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn hủy bệnh nhân lĩnh thuốc cho {0} đơn thuốc đang chọn hay không?", lstID_Donthuoc.Count.ToString()), "Thông báo", true))
                {
                    frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy();
                    _NhaplydoHuy.ShowDialog();
                    if (!_NhaplydoHuy.m_blnCancel)
                    {
                        if (CapphatThuocKhoa.BenhNhanLinhThuoc(idPhieucapphat, lstID_Donthuoc, 0, ref lstNoValidData) == ActionResult.Success)
                        {
                            foreach (GridEXRow _row in grdPres.GetDataRows())
                            {
                                if (lstID_Donthuoc.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                {
                                    _row.BeginEdit();
                                    _row.Cells["da_linh"].Value = 0;
                                    if (lstNoValidData.Count > 0 && lstNoValidData.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                        _row.Cells["not_valid"].Value = 1;
                                    _row.EndEdit();
                                }
                            }
                            grdPres.Refetch();
                            Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Hủy lĩnh thuốc thành công!", false);

                        }
                        else
                            Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Hủy lĩnh thuóc không thành công!Liên hệ VMS để được trợ giúp", true);
                        mnuHuyLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=1").Length > 0 ;
                        mnuLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=0").Length > 0;
                        mnuHuyLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                        mnuLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
                        if (lstNoValidData.Count > 0)
                        {
                            Utility.ShowMsg("Chú ý: Một số đơn thuốc bạn chọn chứa thuốc trả lại đã được tổng hợp trong phiếu trả thuốc thừa nên hệ thống bỏ qua không thực hiện và đánh dấu màu đỏ. Đề nghị bạn kiểm tra lại");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void mnuHuyLinhthuocCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                List<int> lstNoValidData = new List<int>();
                List<int> lstID_Donthuoc = new List<int>() { Utility.Int32Dbnull(grdPres.GetValue("id_donthuoc"), -1) };
                if (Utility.AcceptQuestion("Bạn có chắc chắn hủy lĩnh thuốc cho Bệnh nhân đang chọn hay không?", "Thông báo", true))
                {
                    if (Trathuocthua.ThuocNoitruKiemtraThuoctralai(idPhieucapphat, Utility.Int64Dbnull(grdPres.GetValue("id_donthuoc"))))
                    {
                        Utility.ShowMsg("Đơn thuốc của bệnh nhân bạn đang chọn đã có chi tiết được tổng hợp trả thuốc thừa nên không thể đánh dấu hủy lĩnh thuốc được.\nĐề nghị bạn kiểm tra lại!");
                        return;
                    }
                    if (CapphatThuocKhoa.BenhNhanLinhThuoc(idPhieucapphat, lstID_Donthuoc, 0, ref lstNoValidData) == ActionResult.Success)
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells["da_linh"].Value = 0;
                        if (lstNoValidData.Count > 0 && lstNoValidData.Contains(Utility.Int32Dbnull(grdPres.CurrentRow.Cells["id_donthuoc"].Value, -1)))
                            grdPres.CurrentRow.Cells["not_valid"].Value = 1;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.Refetch();
                        Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Hủy lĩnh thuốc thành công!", false);
                    }
                    else
                        Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Hủy lĩnh thuốc không thành công!Liên hệ VMS để được trợ giúp", true);



                    mnuHuyLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=1").Length > 0;
                    mnuLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=0").Length > 0;
                    mnuHuyLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                    mnuLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
                    if (lstNoValidData.Count > 0)
                    {
                        Utility.ShowMsg("Chú ý: Một số đơn thuốc bạn chọn chứa thuốc trả lại đã được tổng hợp trong phiếu trả thuốc thừa nên hệ thống bỏ qua không thực hiện và đánh dấu màu đỏ. Đề nghị bạn kiểm tra lại");
                    }
                    //}
                }
            }
            catch (Exception ex)
            {

            }


        }

        void mnuLinhthuocAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                List<int> lstNoValidData = new List<int>();
                List<int> lstID_Donthuoc = (from p in grdPres.GetDataRows().AsEnumerable()
                                            where Utility.Int32Dbnull(((DataRowView)p.DataRow)["da_linh"], -1) == 0
                                            select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
                if (lstID_Donthuoc.Count <= 0) return;
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn xác nhận cho các Bệnh nhân đang chọn đã lĩnh thuốc hay không?", lstID_Donthuoc.Count.ToString()), "Thông báo", true))
                {
                    if (CapphatThuocKhoa.BenhNhanLinhThuoc(idPhieucapphat, lstID_Donthuoc, 1, ref lstNoValidData) == ActionResult.Success)
                    {
                        foreach (GridEXRow _row in grdPres.GetDataRows())
                        {
                            if (lstID_Donthuoc.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                            {
                                _row.BeginEdit();
                                _row.Cells["da_linh"].Value = 1;
                                if (lstNoValidData.Count > 0 && lstNoValidData.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                    _row.Cells["not_valid"].Value = 1;
                                _row.EndEdit();
                            }
                        }
                        grdPres.Refetch();
                        Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Xác nhận Bệnh nhân lĩnh thuốc thành công!", false);

                    }
                    else
                        Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Xác nhận Bệnh nhân lĩnh thuốc không thành công!Liên hệ VMS để được trợ giúp", true);
                    mnuHuyLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=1").Length > 0;
                    mnuLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=0").Length > 0;
                    mnuHuyLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                    mnuLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
                    if (lstNoValidData.Count > 0)
                    {
                        Utility.ShowMsg("Chú ý: Một số đơn thuốc bạn chọn chứa thuốc trả lại đã được tổng hợp trong phiếu trả thuốc thừa nên hệ thống bỏ qua không thực hiện và đánh dấu màu đỏ. Đề nghị bạn kiểm tra lại");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void mnuLinhthuocCurrent_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPres)) return;
            List<int> lstNoValidData = new List<int>();
            List<int> lstID_Donthuoc = new List<int>() { Utility.Int32Dbnull(grdPres.GetValue("id_donthuoc"), -1) };
            if (Utility.AcceptQuestion("Bạn có chắc chắn xác nhận Bệnh nhân đang chọn lĩnh thuốc hay không?", "Thông báo", true))
            {
                if(Trathuocthua.ThuocNoitruKiemtraThuoctralai(idPhieucapphat,Utility.Int64Dbnull(grdPres.GetValue("id_donthuoc"))))
                {
                    Utility.ShowMsg("Đơn thuốc của bệnh nhân bạn đang chọn đã có chi tiết được tổng hợp trả thuốc thừa nên không thể đánh dấu lĩnh thuốc được.\nĐề nghị bạn kiểm tra lại!");
                    return;
                }
                if (CapphatThuocKhoa.BenhNhanLinhThuoc(idPhieucapphat, lstID_Donthuoc, 1, ref lstNoValidData) == ActionResult.Success)
                {
                    grdPres.CurrentRow.BeginEdit();
                    grdPres.CurrentRow.Cells["da_linh"].Value = 1;
                    if (lstNoValidData.Count > 0 && lstNoValidData.Contains(Utility.Int32Dbnull(grdPres.CurrentRow.Cells["id_donthuoc"].Value, -1)))
                        grdPres.CurrentRow.Cells["not_valid"].Value = 1;
                    grdPres.CurrentRow.EndEdit();
                    grdPres.Refetch();
                    Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Xác nhận Bệnh nhân lĩnh thuốc thành công!", false);
                }
                else
                    Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Xác nhận Bệnh nhân lĩnh thuốc không thành công!Liên hệ VMS để được trợ giúp", true);
                mnuHuyLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=1").Length > 0;
                mnuLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=0").Length > 0;
                mnuHuyLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                mnuLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
                if (lstNoValidData.Count > 0)
                {
                    Utility.ShowMsg("Chú ý: Một số đơn thuốc bạn chọn chứa thuốc trả lại đã được tổng hợp trong phiếu trả thuốc thừa nên hệ thống bỏ qua không thực hiện và đánh dấu màu đỏ. Đề nghị bạn kiểm tra lại");
                }
            }
        }
        void grdChitiet_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChitiet)) return;
                int TrangthaiTralai =Utility.Int32Dbnull( Utility.getValueOfGridCell( grdChitiet,TPhieuCapphatChitiet.Columns.TrangthaiTralai),0);
                int IdPhieutralai =Utility.Int32Dbnull( Utility.getValueOfGridCell( grdChitiet,TPhieuCapphatChitiet.Columns.IdPhieutralai),0);
                long IdChitiet =Utility.Int64Dbnull( Utility.getValueOfGridCell( grdChitiet,TPhieuCapphatChitiet.Columns.IdChitiet),0);
                int soluong = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuong].Value, -1);
                int soluongtralai = 0;
                int thuclinh = soluong;
                if (TrangthaiTralai==1)
                {
                    Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                    e.Value = e.InitialValue;
                    return;
                }
                 if (TrangthaiTralai==2)
                {
                    Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa và đã trả lại kho thuốc nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                    e.Value = e.InitialValue;
                    return;
                }
                 if (IdPhieutralai>0)
                {
                    Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                    e.Value = e.InitialValue;
                    return;
                }
                int soluongsua = Utility.Int32Dbnull(e.Value, 0);
                if (soluongsua > soluong)
                {
                    Utility.ShowMsg(string.Format( "Số lượng thực lĩnh (hoặc trả lại) phải nhỏ hơn hoặc bằng số lượng kê {0}",soluong.ToString()));
                    e.Cancel = true;
                    return;
                }
                grdChitiet.CurrentRow.BeginEdit();
                if (e.Column.Key == TPhieuCapphatChitiet.Columns.SoLuongtralai)
                {
                    soluongtralai = soluongsua;
                    thuclinh = soluong - soluongsua;
                    grdChitiet.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.ThucLinh].Value = soluong - soluongsua;
                }
                else
                {
                    thuclinh = soluongsua;
                    soluongtralai = soluong - soluongsua;
                    grdChitiet.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuongtralai].Value = soluong - soluongsua;
                }
                grdChitiet.CurrentRow.EndEdit();
                grdChitiet.Refetch();
                CapphatThuocKhoa.CapnhatThuclinh(
                    IdChitiet
                    ,thuclinh
                    , soluongtralai
                    );
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void txtBenhnhan__OnSelectionChanged()
        {
            try
            {
                int _idBn = Utility.Int32Dbnull(txtBenhnhan.MyID, -1);
                string _maluotkham = Utility.sDbnull(txtBenhnhan.MyCode, "-1");
                if (_idBn > 0)
                {
                    var q = from p in grdPres.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[TPhieuCapphatChitiet.Columns.IdBenhnhan].Value, 0) == _idBn
                            && Utility.sDbnull(p.Cells[TPhieuCapphatChitiet.Columns.MaLuotkham].Value, 0) == _maluotkham
                            select p;
                    if (q.Count() > 0)
                    {
                        grdPres.MoveTo(q.First());

                    }

                }
                else
                {
                    grdPres.MoveFirst();
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
        void _CheckedChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            FilterMe(); 
        }
        void FilterMe()
        {
            try
            {
                string rowfilter = "1=1";
                if (optDalinh.Checked)
                    rowfilter = "da_linh=1";
                else if (optChualinh.Checked)
                    rowfilter = "da_linh=0";
                m_Donthuoc.DefaultView.RowFilter = rowfilter;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
            ModifyCommands();
        }
        private void frm_PhatThuocBN_Noitru_Load(object sender, EventArgs e)
        {
            ModifyCommands();
            txtIDCapPhat.Focus();
            txtIDCapPhat.SelectAll();
        }

        private void SearchData()
        {
            try
            {
                hasLoaded = false;
                txtBenhnhan.ClearMe();
                idPhieucapphat = -1;
                if (m_Donthuoc != null) m_Donthuoc.Rows.Clear();
                if (dtChitietcapphattheodon != null) dtChitietcapphattheodon.Rows.Clear();
                if (!CheckIDCapPhat())
                {
                    Utility.ShowMsg("Phiếu lĩnh thuốc không tồn tại hoặc chưa được xác nhận tại khoa Dược. Đề nghị bạn kiểm tra hoặc liên hệ lại khoa Dược");
                }
                else
                {
                    GetData();
                    
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                hasLoaded = true;
                grdPres_SelectionChanged(grdPres, new EventArgs());
                ModifyCommands();
            }
            

        }

        private bool CheckIDCapPhat()
        {
            TPhieuCapphatNoitru phieucapphat = TPhieuCapphatNoitru.FetchByID(Utility.Int32Dbnull(txtIDCapPhat.Text));
            if (phieucapphat == null) return false;
            else
            {
                if (Utility.Int32Dbnull(phieucapphat.TrangThai) == 1) return true;
                else return false;
            }
        }

        private void GetData()
        {
            
            idPhieucapphat = Utility.Int32Dbnull(txtIDCapPhat.Text);
            m_Donthuoc =
                SPs.ThuocNoitruTimkiemDonthuocTheophieutonghopthuocnoitru(idPhieucapphat, kieu_thuoc_vt, - 1).GetDataSet().
                    Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdPres,m_Donthuoc,true,true,"1=1","ten_benhnhan,tuoi,gioi_tinh");
            txtBenhnhan.Init(m_Donthuoc, new List<string>() { TPhieuCapphatChitiet.Columns.IdBenhnhan, TPhieuCapphatChitiet.Columns.MaLuotkham, KcbDanhsachBenhnhan.Columns.TenBenhnhan });
            txtBenhnhan.Focus();
        }
        private void ModifyCommands()
        {
            try
            {
                pnlFilter.Enabled = m_Donthuoc != null && m_Donthuoc.Rows.Count > 0;
                pnlAct.Enabled = Utility.isValidGrid(grdPres) && Utility.isValidGrid(grdChitiet);
                cmdXacNhan.Enabled = Utility.sDbnull(Utility.GetValueFromGridColumn(grdPres, "Da_linh"), "0") == "0";
                cmdHuyXacNhan.Enabled = !cmdXacNhan.Enabled;

                mnuHuyLinhthuocAll.Enabled = m_Donthuoc != null && m_Donthuoc.Select("da_linh=1").Length > 0;
                mnuLinhthuocAll.Enabled = m_Donthuoc != null && m_Donthuoc.Select("da_linh=0").Length > 0;
                mnuHuyLinhthuocCurrent.Enabled = m_Donthuoc != null && Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                mnuLinhthuocCurrent.Enabled = m_Donthuoc != null && Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
            }
            catch (Exception ex)
            {
                
                
            }
        }
        DataTable dtChitietcapphattheodon = null;
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!hasLoaded || !Utility.isValidGrid(grdPres)) return;
                int IdDonthuoc = Utility.Int32Dbnull(grdPres.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.IdDonthuoc].Value);
                string KieuThuocVt = Utility.sDbnull(grdPres.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.KieuThuocVt].Value);
                dtChitietcapphattheodon = SPs.ThuocNoitruLaychitietdonthuocTheophieulinhthuocnoitru(idPhieucapphat, IdDonthuoc).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdChitiet, dtChitietcapphattheodon, true, true, "1=1", "");

            }
            catch (Exception ex)
            {


            }
            finally
            {
                ModifyCommands();
            }
           
        }

        private void cmdXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                List<int> lstNoValidData = new List<int>();
                List<int> lstID_Donthuoc = (from p in grdPres.GetCheckedRows().AsEnumerable()
                                            where Utility.Int32Dbnull(((DataRowView)p.DataRow)["da_linh"], -1) == 0
                                            select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
                if (lstID_Donthuoc.Count <= 0)
                    if (Utility.isValidGrid(grdPres))
                        lstID_Donthuoc.Add(Utility.Int32Dbnull(grdPres.GetValue("id_donthuoc"),-1));
                if (lstID_Donthuoc.Count <= 0)
                {
                    Utility.ShowMsg("Bạn chưa chọn đơn thuốc nào để xác nhận lĩnh thuốc cho người bệnh. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn xác nhận cho các Bệnh nhân đang chọn đã lĩnh thuốc hay không?", lstID_Donthuoc.Count.ToString()), "Thông báo", true))
                {
                    if (CapphatThuocKhoa.BenhNhanLinhThuoc(idPhieucapphat, lstID_Donthuoc, 1, ref lstNoValidData) == ActionResult.Success)
                    {
                        foreach (GridEXRow _row in grdPres.GetDataRows())
                        {
                            if (lstID_Donthuoc.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                            {
                                _row.BeginEdit();
                                _row.Cells["da_linh"].Value = 1;
                                if (lstNoValidData.Count > 0 && lstNoValidData.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                    _row.Cells["not_valid"].Value = 1;
                                _row.EndEdit();
                            }
                        }
                        grdPres.Refetch();
                        Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Xác nhận Bệnh nhân lĩnh thuốc thành công!", false);

                    }
                    else
                        Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Xác nhận Bệnh nhân lĩnh thuốc không thành công!Liên hệ VMS để được trợ giúp", true);
                    mnuHuyLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=1").Length > 0;
                    mnuLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=0").Length > 0;
                    mnuHuyLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                    mnuLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
                    if (lstNoValidData.Count > 0)
                    {
                        Utility.ShowMsg("Chú ý: Một số đơn thuốc bạn chọn chứa thuốc trả lại đã được tổng hợp trong phiếu trả thuốc thừa nên hệ thống bỏ qua không thực hiện và đánh dấu màu đỏ. Đề nghị bạn kiểm tra lại");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            //ActionResult actionXN = CapphatThuocKhoa.BenhNhanLinhThuoc(dtChitietcapphattheodon,1);
            //switch (actionXN)
            //{
            //     case ActionResult.Success:
            //         UpdateGird(1);
            //         ModifyCommands();
            //        break;
            //     case ActionResult.Error:
            //         Utility.ShowMsg("Lỗi khi xác nhận Bệnh nhân lĩnh thuốc nội trú");
            //        break;
            //}
        }

        private void cmdHuyXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                List<int> lstNoValidData = new List<int>();
                List<int> lstID_Donthuoc = (from p in grdPres.GetDataRows().AsEnumerable()
                                            where Utility.Int32Dbnull(((DataRowView)p.DataRow)[TPhieuCapphatChitiet.Columns.DaLinh], -1) == 1
                                            select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
                if (lstID_Donthuoc.Count <= 0)
                    if (Utility.isValidGrid(grdPres))
                        lstID_Donthuoc.Add(Utility.Int32Dbnull(grdPres.GetValue("id_donthuoc"), -1));
                if (lstID_Donthuoc.Count <= 0)
                {
                    Utility.ShowMsg("Bạn chưa chọn đơn thuốc nào để hủy xác nhận lĩnh thuốc cho người bệnh. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn hủy bệnh nhân lĩnh thuốc cho {0} đơn thuốc đang chọn hay không?", lstID_Donthuoc.Count.ToString()), "Thông báo", true))
                {
                    frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy();
                    _NhaplydoHuy.ShowDialog();
                    if (!_NhaplydoHuy.m_blnCancel)
                    {
                        if (CapphatThuocKhoa.BenhNhanLinhThuoc(idPhieucapphat, lstID_Donthuoc, 0, ref lstNoValidData) == ActionResult.Success)
                        {
                            foreach (GridEXRow _row in grdPres.GetDataRows())
                            {
                                if (lstID_Donthuoc.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                {
                                    _row.BeginEdit();
                                    _row.Cells["da_linh"].Value = 0;
                                    if (lstNoValidData.Count > 0 && lstNoValidData.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                        _row.Cells["not_valid"].Value = 1;
                                    _row.EndEdit();
                                }
                            }
                            grdPres.Refetch();
                            Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Hủy lĩnh thuốc thành công!", false);

                        }
                        else
                            Utility.SetMsg(uiStatusBar2.Panels["Msg"], "Hủy lĩnh thuóc không thành công!Liên hệ VMS để được trợ giúp", true);
                        mnuHuyLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=1").Length > 0;
                        mnuLinhthuocAll.Enabled = m_Donthuoc.Select("da_linh=0").Length > 0;
                        mnuHuyLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "1";
                        mnuLinhthuocCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("da_linh"), "-1") == "0";
                        if (lstNoValidData.Count > 0)
                        {
                            Utility.ShowMsg("Chú ý: Một số đơn thuốc bạn chọn chứa thuốc trả lại đã được tổng hợp trong phiếu trả thuốc thừa nên hệ thống bỏ qua không thực hiện và đánh dấu màu đỏ. Đề nghị bạn kiểm tra lại");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            //ActionResult actionHXN = CapphatThuocKhoa.BenhNhanLinhThuoc(dtChitietcapphattheodon,  0);
            //switch (actionHXN)
            //{
            //    case ActionResult.Success:
            //        UpdateGird(0);
            //        ModifyCommands();
            //        break;
            //    case ActionResult.Error:
            //        Utility.ShowMsg("Lỗi khi hủy xác nhận Bệnh nhân lĩnh thuốc nội trú");
            //        break;
            //}
        }

        private void UpdateGird(int isdalinh)
        {
            grdPres.CurrentRow.BeginEdit();
            grdPres.CurrentRow.Cells["DA_LINH"].Value = isdalinh;
            grdPres.CurrentRow.EndEdit();
            grdPres.UpdateData();
            m_Donthuoc.AcceptChanges();
        }


        private void frm_PhatThuocBN_Noitru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdXacNhan.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdHuyXacNhan.PerformClick();
            if (e.KeyCode == Keys.F3 || (e.Control && e.KeyCode == Keys.F))
            {
                txtIDCapPhat.Focus();
                txtIDCapPhat.SelectAll();
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                txtBenhnhan.Focus();
                txtBenhnhan.SelectAll();
                return;
            }
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void txtIDCapPhat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Utility.IsNumeric(txtIDCapPhat.Text))
                    SearchData();
                else
                {
                    txtIDCapPhat.Focus();
                    txtIDCapPhat.SelectAll();
                }
            }
        }

        private void cmdXacNhan_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdHuyXacNhan_Click_1(object sender, EventArgs e)
        {

        }

      
    }
}
