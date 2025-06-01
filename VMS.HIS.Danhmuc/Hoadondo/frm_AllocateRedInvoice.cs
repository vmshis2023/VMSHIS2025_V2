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


namespace VNS.HIS.UI.HOADONDO
{
    public partial class frm_AllocateRedInvoice : Form
    {
        public DataTable dtCapPhat;
        public int HDON_MAU_ID;
        public int id_capphat = -1;
        private DataRow currentDr;
        private HoadonCapphat objCapPhat;
        private HoadonMau objHoaDon;

        public frm_AllocateRedInvoice()
        {
            InitializeComponent();
        }
        private void AutocompleteKieuKham()
        {
            DataTable dtStaff = new DataTable();
            try
            {
                 dtStaff = new Select(SysUser.Columns.PkSuid, string.Format("({0} + isnull(' - ' + {1},'')) as Staff_Name", SysUser.Columns.PkSuid, SysUser.Columns.SFullName)).From(SysUser.Schema).ExecuteDataSet().Tables[0];


                if (dtStaff == null) return;
                if (!dtStaff.Columns.Contains("ShortCut"))
                    dtStaff.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtStaff.Rows)
                {
                    string shortcut = "";
                    string realName = dr["Staff_Name"].ToString().Trim() + " " +
                                      Utility.Bodau(dr["Staff_Name"].ToString().Trim());
                    shortcut = dr[SysUser.Columns.PkSuid].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in dtStaff.AsEnumerable()
                            select p[SysUser.Columns.PkSuid].ToString() + "#" + p[SysUser.Columns.PkSuid].ToString() + "@" + p["Staff_Name"].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtNhanvien.AutoCompleteList = source;
                this.txtNhanvien.TextAlign = HorizontalAlignment.Center;
                this.txtNhanvien.CaseSensitive = false;
                this.txtNhanvien.MinTypedCharacters = 1;
            }
        }
        private void frm_AddRedInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSerieDauCuoi();
                LoadTrangThai();
                AutocompleteKieuKham();
                if (id_capphat > 0)
                    LoadInvoice();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void LoadSerieDauCuoi()
        {
            objHoaDon = new Select().From(HoadonMau.Schema).Where(HoadonMau.Columns.IdHoadonMau).IsEqualTo(HDON_MAU_ID).ExecuteSingle<HoadonMau>();
            txtMau_HD.Text = objHoaDon.MauHoadon;
            txtKi_Hieu.Text = objHoaDon.KiHieu;
            txtSoQuyen.Text = objHoaDon.MaQuyen;
            lblSerieDau.Text = string.Format("({0})", objHoaDon.SerieDau);
            txtSerie_Dau.MaxLength = lblSerieDau.Text.Length - 2;
            lblSerieCuoi.Text = string.Format("({0})", objHoaDon.SerieCuoi);
            txtSerie_Cuoi.MaxLength = lblSerieCuoi.Text.Length - 2;
            txtSerie_HienTai.MaxLength = lblSerieCuoi.Text.Length - 2;
        }

       

        private void LoadTrangThai()
        {
            DataTable dtTrang_Thai =
                    new Select("*").From(HoadonTrangthai.Schema).Where(HoadonTrangthai.Columns.MessageType).
                        IsEqualTo("CapPhatHD").OrderAsc(HoadonTrangthai.Columns.MessageOrder).
                        ExecuteDataSet().Tables[0];
            DataBinding.BindData(cboTrang_Thai, dtTrang_Thai, "Message_ID", "Message_Name");
            cboTrang_Thai.SelectedIndex = 0;
        }

        private void LoadInvoice()
        {
            objCapPhat = new Select().From(HoadonCapphat.Schema).Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(id_capphat).ExecuteSingle<HoadonCapphat>();
            txtSerie_Dau.Text = objCapPhat.SerieDau;
            txtSerie_Cuoi.Text = objCapPhat.SerieCuoi;
            txtSoQuyen.Text = objHoaDon.MaQuyen;
            txtSerie_HienTai.Text = objCapPhat.SerieHientai;
            cboTrang_Thai.SelectedIndex = Utility.Int32Dbnull(objCapPhat.TrangThai);
            txtNhanvien.SetCode(objCapPhat.MaNhanvien);
            currentDr = Utility.GetDataRow(dtCapPhat, new string[] { HoadonCapphat.Columns.IdCapphat }, new object[] { id_capphat });
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!ValidObj()) return;

            try
            {
                if (id_capphat <= 0 )
                {
                    HoadonCapphat obj = new HoadonCapphat();
                    obj.IdHoadonMau = HDON_MAU_ID;
                    obj.MauHoadon = txtMau_HD.Text;
                    obj.KiHieu = txtKi_Hieu.Text;
                    obj.MaQuyen = txtSoQuyen.Text;
                    obj.MaNhanvien =txtNhanvien.MyCode;
                    obj.SerieDau = txtSerie_Dau.Text;
                    obj.SerieCuoi = txtSerie_Cuoi.Text;
                    obj.SerieHientai = "";
                    obj.NgayCapphat = globalVariables.SysDate;
                    obj.TrangThai = 0;
                    obj.IsNew = true;
                    obj.Save();
                    id_capphat = Utility.Int32Dbnull(obj.IdCapphat);

                    new Update(HoadonMau.Schema).Set(HoadonMau.Columns.TrangThai).EqualTo(1).
                        Where(HoadonMau.Columns.IdHoadonMau).IsEqualTo(HDON_MAU_ID).Execute();

                    DataRow dr = dtCapPhat.NewRow();
                    ProcessData(ref dr);
                    dtCapPhat.Rows.InsertAt(dr, 0);
                    dtCapPhat.AcceptChanges();    
                }
                else
                {
                   //int  SeriMin = Utility.Int32Dbnull(HoadonLog.CreateQuery().WHERE(HoadonLog.Columns.MauHoadon,
                   //                                                        objCapPhat.MauHoadon)
                   //     .AND(HoadonLog.Columns.KiHieu, objCapPhat.KiHieu)
                   //     .AND(HoadonLog.Columns.MaQuyen,objCapPhat.MaQuyen).AND(HoadonLog.Columns.CapphatId).GetMin(HoadonLog.Columns.Serie)),0);
                    int  SeriMin = Utility.Int32Dbnull(HoadonLog.CreateQuery().WHERE(HoadonLog.Columns.IdCapphat,
                                                                            objCapPhat.IdCapphat)
                         //.AND(HoadonLog.Columns.KiHieu, objCapPhat.KiHieu)
                         //.AND(HoadonLog.Columns.MaQuyen,objCapPhat.MaQuyen).AND(HoadonLog.Columns.CapphatId)
                         .GetMin(HoadonLog.Columns.Serie),0);
                   //int SeriMax = Utility.Int32Dbnull(HoadonLog.CreateQuery().WHERE(HoadonLog.Columns.MauHoadon,
                   //                                                        objCapPhat.MauHoadon)
                   //     .AND(HoadonLog.Columns.KiHieu, objCapPhat.KiHieu)
                   //     .AND(HoadonLog.Columns.MaQuyen).GetMax(HoadonLog.Columns.Serie), 0);
                    int SeriMax = Utility.Int32Dbnull(HoadonLog.CreateQuery().WHERE(HoadonLog.Columns.IdCapphat, objCapPhat.IdCapphat)
                         //.AND(HoadonLog.Columns.KiHieu, objCapPhat.KiHieu)
                         //.AND(HoadonLog.Columns.MaQuyen)
                         .GetMax(HoadonLog.Columns.Serie), 0);
                    if (Utility.Int32Dbnull(txtSerie_Dau.Text) > SeriMin && SeriMin > 0)
                   {
                       Utility.ShowMsg("Đã có hóa đơn seri " + SeriMin + " đc in nhỏ hơn số Seri đầu : " + txtSerie_Dau.Text);
                       txtSerie_Dau.Focus();
                       return;
                   }
                    if (Utility.Int32Dbnull(txtSerie_Cuoi.Text) < SeriMax && SeriMax > 0)
                   {
                       Utility.ShowMsg("Đã có hóa đơn seri " + SeriMax + " đc in lớn hơn số Seri cuối : " + txtSerie_Dau.Text);
                       txtSerie_Cuoi.Focus();
                       return;
                   }
                    objCapPhat.MaNhanvien = txtNhanvien.MyCode;
                    objCapPhat.SerieDau = txtSerie_Dau.Text;
                    objCapPhat.SerieCuoi = txtSerie_Cuoi.Text;
                    objCapPhat.IsNew = false;
                    objCapPhat.Save();
                    ProcessData(ref currentDr);
                    currentDr.AcceptChanges();
                }

                cmdThoat.PerformClick();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void ProcessData(ref DataRow dr)
        {
            dr[HoadonCapphat.Columns.IdCapphat] = id_capphat;
            dr[HoadonCapphat.Columns.MaNhanvien] = txtNhanvien.MyCode;
            dr["TEN_NVIEN"] = txtNhanvien.Text;
            dr[HoadonCapphat.Columns.MauHoadon] = txtMau_HD.Text;
            dr[HoadonCapphat.Columns.KiHieu] = txtKi_Hieu.Text;
            dr[HoadonCapphat.Columns.MaQuyen] = txtSoQuyen.Text;
            dr[HoadonCapphat.Columns.SerieDau] = txtSerie_Dau.Text;
            dr[HoadonCapphat.Columns.SerieCuoi] = txtSerie_Cuoi.Text;
            dr[HoadonCapphat.Columns.SerieHientai] = txtSerie_HienTai.Text;
            dr[HoadonCapphat.Columns.TrangThai] = cboTrang_Thai.SelectedIndex;
            dr["TRANG_THAI_Message"] = cboTrang_Thai.Text;
        }

        private bool ValidObj()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMau_HD.Text) | string.IsNullOrEmpty(txtKi_Hieu.Text))
                {
                    Utility.ShowMsg("Mã hóa đơn và kí hiệu hóa đơn không được để trống");
                    return false;
                }

                if (string.IsNullOrEmpty(txtSerie_Dau.Text) | string.IsNullOrEmpty(txtSerie_Cuoi.Text))
                {
                    Utility.ShowMsg("Số serie hóa đơn không được để trống");
                    return false;
                }

                if (txtSerie_Dau.Text.Length != txtSerie_Cuoi.Text.Length | txtSerie_Dau.Text.Length != objHoaDon.SerieDau.Length)
                {
                    Utility.ShowMsg("Số ký tự của serie đầu và serie cuối không đúng");
                    return false;
                }

                if (string.Compare(txtSerie_Dau.Text,txtSerie_Cuoi.Text,StringComparison.InvariantCulture) > 0)
                {
                    Utility.ShowMsg("Serie đầu phải nhỏ hơn serie cuối");
                    return false;
                }

                //int c1 = string.Compare(txtSerie_Dau.Text, objHoaDon.SerieDau, StringComparison.InvariantCulture);
                //int c2 = string.Compare(txtSerie_Cuoi.Text, objHoaDon.SerieCuoi, StringComparison.InvariantCulture);

                if (string.Compare(objHoaDon.SerieDau,txtSerie_Dau.Text, StringComparison.InvariantCulture) > 0 | string.Compare(txtSerie_Cuoi.Text, objHoaDon.SerieCuoi, StringComparison.InvariantCulture) > 0)
                {
                    Utility.ShowMsg(string.Format("Serie đầu hoặc cuối cần được nhập trong khoảng {0} đến {1}", lblSerieDau.Text,txtSerie_Cuoi.Text ));
                    return false;
                }


                foreach (DataRow row in dtCapPhat.Select(HoadonCapphat.Columns.IdHoadonMau+" = " + HDON_MAU_ID))
                    if (id_capphat <= 0 || (id_capphat > 0 && id_capphat != Utility.Int32Dbnull(row[HoadonCapphat.Columns.IdCapphat])))
                    {
                        Int32 sSerie_Dau = Utility.Int32Dbnull(row[HoadonCapphat.Columns.SerieDau]);
                        Int32 sSerie_Cuoi = Utility.Int32Dbnull(row[HoadonCapphat.Columns.SerieCuoi]);
                        if ((Utility.Int32Dbnull(txtSerie_Dau.Text) >= sSerie_Dau & Utility.Int32Dbnull(txtSerie_Dau.Text) <= sSerie_Cuoi)
                            || (Utility.Int32Dbnull(txtSerie_Cuoi.Text) >= sSerie_Dau & Utility.Int32Dbnull(txtSerie_Cuoi.Text) <= sSerie_Cuoi)
                            || (Utility.Int32Dbnull(txtSerie_Dau.Text) <= sSerie_Dau & Utility.Int32Dbnull(txtSerie_Cuoi.Text) >= sSerie_Cuoi))
                        {
                            Utility.ShowMsg(string.Format("Đã có serie trong khoảng ({0} - {1}) được cấp phát", txtSerie_Dau.Text,txtSerie_Cuoi.Text));
                            return false;
                        }
                    }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void frm_AddRedInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                    return;
                }
                if (e.Control && e.KeyCode==Keys.S )
                {
                    cmdGhi_Click(cmdGhi, new EventArgs());
                    return;
                }
                if (e.Control && e.KeyCode == Keys.Escape)
                {
                    cmdThoat_Click(cmdThoat, new EventArgs());
                    return;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSerie_Dau_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtSerie_Cuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
    }
}
