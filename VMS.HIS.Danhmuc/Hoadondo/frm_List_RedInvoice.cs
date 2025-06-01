using System;
using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
namespace VNS.HIS.UI.HOADONDO
{
    public partial class frm_List_RedInvoice : Form
    {
        private DataTable dtList;
        private DataTable dtCapPhat;
        private SqlQuery queryList;

        public frm_List_RedInvoice()
        {
            InitializeComponent();
        }

        private void frm_List_RedInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                grdList.DoubleClick += grdList_DoubleClick;

                LoadListInvoice();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void LoadListInvoice()
        {
            queryList = new Select("*,(select top 1 lm.Message_Name from hoadon_trangthai lm where lm.Message_ID = Trang_Thai and lm.Message_Type = 'MauHD') as Trang_Thai_Message," +
                                   "(case when getdate() between hoadon_mau.ngay_hethieuluc and hoadon_mau.ngay_hieuluc then 1 else 0 end) as Expired").
                From(HoadonMau.Schema).OrderDesc(HoadonMau.Columns.NgayHethieuluc);
            dtList = queryList.ExecuteDataSet().Tables[0];
            //dtList = new Select("*").From(HoadonMau.Schema).OrderDesc(HoadonMau.Columns.NgayHhluc).ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList,dtList,true,true,"","",true);
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                frm_AddRedInvoice oFrom = new frm_AddRedInvoice();
                oFrom.dtList = dtList;
                oFrom.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidData()) return;

                frm_AddRedInvoice oFrom = new frm_AddRedInvoice();
                oFrom.txtMau_HD.Text = Utility.sDbnull(grdList.GetValue(HoadonMau.Columns.MauHoadon));
                oFrom.txtKi_Hieu.Text = Utility.sDbnull(grdList.GetValue(HoadonMau.Columns.KiHieu));
                oFrom.dtList = dtList;
                oFrom.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private bool ValidData()
        {
            if (grdList.CurrentRow == null) return false;

            if (HoadonMau.CreateQuery().WHERE(HoadonMau.Columns.IdHoadonMau, Utility.Int32Dbnull(grdList.GetValue(HoadonMau.Columns.IdHoadonMau))).AND(HoadonMau.Columns.TrangThai,Comparison.GreaterThan,0).GetRecordCount() > 0)
            {
                Utility.ShowMsg("Hóa đơn đã được sử dụng. Không được sửa hoặc xóa.");
                return false;
            }

            return true;
        }

        private void frm_List_RedInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.N:
                            cmdNew.PerformClick();
                            break;
                        case Keys.E:
                            cmdEdit.PerformClick();
                            break;
                        case Keys.D:
                            cmdDelete.PerformClick();
                            break;
                    }
                }
                else
                    switch (e.KeyCode)
                    {
                        case Keys.Escape:
                            cmdThoat.PerformClick();
                            break;
                        case Keys.F4:
                            cmdPrint.PerformClick();
                            break;
                    }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            cmdEdit.PerformClick();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (!ValidData()) return;

            try
            {
                if (Utility.AcceptQuestion(string.Format("Thực hiện xóa mẫu hóa đơn {0} với ký hiệu {1}", Utility.sDbnull(grdList.GetValue(HoadonMau.Columns.MauHoadon)), Utility.sDbnull(grdList.GetValue(HoadonMau.Columns.KiHieu))), "THông báo", false))
                {
                    new Delete().From(HoadonMau.Schema).Where(HoadonMau.Columns.MauHoadon).IsEqualTo(Utility.sDbnull(grdList.GetValue(HoadonMau.Columns.MauHoadon))).
                        And(HoadonMau.Columns.KiHieu).IsEqualTo(Utility.sDbnull(grdList.GetValue(HoadonMau.Columns.KiHieu))).
                        Execute();
                    grdList.CurrentRow.Delete();
                    grdList.UpdateData();
                    dtList.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void cmdAllocate_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdList.CurrentRow == null) return;
                frm_AllocateRedInvoice oForm = new frm_AllocateRedInvoice();
                oForm.HDON_MAU_ID = Utility.Int32Dbnull(grdList.GetValue(HoadonMau.Columns.IdHoadonMau));
                oForm.dtCapPhat = dtCapPhat;
                oForm.ShowDialog();
                grdList_SelectionChanged(sender,e);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdList.CurrentRow == null) return;

                dtCapPhat =
                    new Select(
                        "*,(Select Top 1 lm.Message_Name from hoadon_trangthai lm where lm.Message_ID =  convert(nvarchar(30),Trang_Thai) and lm.Message_Type = 'CapPhatHD') as Trang_Thai_Message," +
                        "(select top 1 su.sFullName from Sys_Users su where su.PK_sUID = ma_nhanvien) as TEN_NVIEN").
                        From(HoadonCapphat.Schema).Where(HoadonCapphat.Columns.IdHoadonMau).IsEqualTo(
                            Utility.sDbnull(grdList.GetValue(HoadonCapphat.Columns.IdHoadonMau)))
                       .ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdCapPhat,dtCapPhat,true,true,"","",true);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private bool ValidCapPhat()
        {
            if (grdCapPhat.CurrentRow == null) return false;

            return true;
        }

        private void grdCapPhat_DoubleClick(object sender, EventArgs e)
        {
            SuaCapPhat();
        }

        private void grdCapPhat_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                switch (grdCapPhat.CurrentColumn.Key)
                {
                    case "btnEdit":
                        SuaCapPhat();
                        break;
                    case "btnDel":
                        XoaCapPhat();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void XoaCapPhat()
        {
            try
            {
                HoadonLog HoadonLog =
                    new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.IdCapphat).IsEqualTo(
                        grdCapPhat.GetValue(HoadonLog.Columns.IdCapphat)).ExecuteSingle<HoadonLog>();
                if(HoadonLog != null)
                {
                    Utility.ShowMsg("Đã có hóa đơn được in trong khoảng cấp phát này. Bạn không thể xóa");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Thực hiện xóa mẫu hóa đơn {0} với ký hiệu {1} từ serie {2} đến {3} của {4}",
                    Utility.sDbnull(grdList.GetValue(HoadonCapphat.Columns.MauHoadon)), Utility.sDbnull(grdList.GetValue(HoadonCapphat.Columns.KiHieu)),
                    Utility.sDbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.SerieDau)), Utility.sDbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.SerieCuoi)),
                    Utility.sDbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.MaNhanvien))), "Thông báo", false))
                {
                    new Delete().From(HoadonCapphat.Schema).Where(HoadonCapphat.Columns.IdCapphat).
                        IsEqualTo(Utility.sDbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat))).Execute();

                    if (HoadonCapphat.CreateQuery().WHERE(HoadonCapphat.Columns.IdHoadonMau, Utility.Int32Dbnull(grdList.GetValue(HoadonCapphat.Columns.IdHoadonMau))).GetRecordCount() <= 0)
                        new Update(HoadonMau.Schema).Set(HoadonMau.Columns.TrangThai).EqualTo(0).Where(
                            HoadonMau.Columns.IdHoadonMau).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(HoadonCapphat.Columns.IdHoadonMau)))
                            .Execute();
                    grdCapPhat.CurrentRow.Delete();
                    grdCapPhat.UpdateData();
                    dtCapPhat.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void SuaCapPhat()
        {
            try
            {
                if (grdCapPhat.CurrentRow == null)
                    return;
                frm_AllocateRedInvoice oForm = new frm_AllocateRedInvoice();
                oForm.HDON_MAU_ID = Utility.Int32Dbnull(grdList.GetValue(HoadonCapphat.Columns.IdHoadonMau));
                oForm.dtCapPhat = dtCapPhat;
                oForm.id_capphat = Utility.Int32Dbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat));
                oForm.ShowDialog();
                grdList_SelectionChanged(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if(Utility.Int32Dbnull(grdCapPhat.GetValue("TRANG_THAI")) >= 2)
                {
                    tsmHetHieuLuc.Visible = false;
                    tsmKichHoat.Visible = true;
                }
                else
                {
                    tsmKichHoat.Visible = false;
                    tsmHetHieuLuc.Visible = true;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình load menu");                
            }
        }

        private void tsmHetHieuLuc_Click(object sender, EventArgs e)
        {
            try
            {
                if(Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy hiệu lực của bản ghi này không ?", "Xác nhận", true))
                    return;
                int record = new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.TrangThai).EqualTo(2)
                    .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(
                        Utility.Int32Dbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat)))
                    .Execute();
                if(record > 0)
                {
                    Utility.ShowMsg("Đã cập nhật thông tin thành công.");
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình update thông tin");                
            }
        }

        private void tsmKichHoat_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn kích hoạt hiệu lực của bản ghi này không ?", "Xác nhận", true))
                    return;

                int record = new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                    .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(
                        Utility.Int32Dbnull(grdCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat)))
                    .Execute();
                if (record > 0)
                {
                    Utility.ShowMsg("Đã cập nhật thông tin thành công.");
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình update thông tin");
                
            }
        }
    }
}
