using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using System.IO;
using Microsoft.VisualBasic;
using VNS.HIS.UI.Classess;
using VNS.HIS.BusRule.Classes;
//using SubSonic.Utilities;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_phieucongkhai : Form
    {
        public KcbTongketBA Tkba = new KcbTongketBA();
        public KcbLuotkham objLuotkham = null;
        public action m_enAct = action.FirstOrFinished;
        public bool CallfromParent = false;
        public bool AutoLoad = false;
        public frm_phieucongkhai()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtTuNgay.Value = dtDenNgay.Value = THU_VIEN_CHUNG.GetSysDateTime();
            ucThongtinnguoibenh_v21._OnEnterMe += ucThongtinnguoibenh_v21__OnEnterMe;
            this.KeyDown += frm_phieucongkhai_KeyDown;
            ucThongtinnguoibenh_v21.noitrungoaitru = 1;
            ucThongtinnguoibenh_v21.trangthai_noitru = 1;
            
            //ucThongtinnguoibenh_v21.SetReadonly();
        }

        void frm_phieucongkhai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                    SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cmdExit.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                cmdExcel.PerformClick();
            }
        }

        void ucThongtinnguoibenh_v21__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v21.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_v21.objLuotkham;
                LaydanhsachPhieudieutri();
            }
            else
            {
                m_dtPhieudieutri.Clear();
                m_dtPhieudieutri.AcceptChanges();
                cmdExcel.Enabled = false;
            }
        }
        KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        DataTable m_dtPhieudieutri = new DataTable();
        void LaydanhsachPhieudieutri()
        {
            try
            {
                string IdKhoanoitru = Utility.sDbnull(objLuotkham.IdKhoanoitru, "-1");

                 m_dtPhieudieutri = _KCB_THAMKHAM.NoitruTimkiemphieudieutriTheoluotkham(1, "01/01/1900", objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, "-1", -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieudieutri, m_dtPhieudieutri, false, true, "1=1", NoitruPhieudieutri.Columns.NgayDieutri + " desc");
                grdPhieudieutri.CheckAllRecords();
                cmdExcel.Enabled = grdPhieudieutri.GetDataRows().Count()>0;
                Utility.UpdateGroup(grdPhieudieutri, m_dtPhieudieutri, "id_khoanoitru", "Khoa điều trị");
                var q = from p in m_dtPhieudieutri.AsEnumerable()
                                    select Convert.ToDateTime( p["ngay_dieutri"]);
                if (q.Any())
                {
                    dtTuNgay.Value = q.Min();
                    dtDenNgay.Value = q.Max();
                }
                                
            }
            catch (Exception ex)
            {

            }
        }
        void FillData4Update()
        {
            if (Tkba != null)
            {
                dtTuNgay.Value = Convert.ToDateTime(Tkba.TuNgay);
                dtDenNgay.Value = Convert.ToDateTime(Tkba.DenNgay);
               
            }
        }

        public void ClearControl()
        {
           

        }
        private Boolean isValidData()
        {

           
            
            return true;
        }

        private void frm_phieucongkhai_Load(object sender, EventArgs e)
        {
            ucThongtinnguoibenh_v21.AutoLoad = AutoLoad;
            if (objLuotkham != null)
            {
                ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_v21.Refresh();
            }
            else
                ucThongtinnguoibenh_v21.Refresh();
        }

        private void cmdExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("PHIEUCONGKHAI_CROSSTAB", "0", true) == "1")
                InPhieuCongkhai_CrossTab();
            else
                InPhieuCongkhai();
        }
        void InPhieuCongkhai_CrossTab()
        {
            try
            {
                List<string> lstIdphieudieutri = (from p in grdPhieudieutri.GetCheckedRows()
                                                  select Utility.sDbnull(p.Cells["id_phieudieutri"].Value)
                                                ).ToList<string>();
                DataTable dtData = SPs.NoitruPhieucongkhaiLaydulieuin(dtTuNgay.Value, dtDenNgay.Value, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, string.Join(",", lstIdphieudieutri.ToArray<string>()),
                    Utility.Bool2byte(chkThuoc.Checked), Utility.Bool2byte(chkVTTH.Checked), Utility.Bool2byte(chkCLS.Checked), Utility.Bool2byte(chkGiuong.Checked)).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.SapxepthutuinPhieucongkhai(dtData, false);
                dtData.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten";



                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu công khai.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                // int colngayfrom = 24;
                // int totalDays = dtData.Columns.Count - 24;
                // dtData.Columns.AddRange(new DataColumn[]{new DataColumn("ngay1",typeof(int)),new DataColumn("ngay2",typeof(int)),new DataColumn("ngay3",typeof(int)),
                //new DataColumn("ngay4",typeof(int)),new DataColumn("ngay5",typeof(int)),new DataColumn("ngay6",typeof(int)),
                //new DataColumn("ngay7",typeof(int)),new DataColumn("ngay8",typeof(int)),new DataColumn("ngay9",typeof(int)),
                //new DataColumn("ngay10",typeof(int))});
                // foreach (DataRow dr in dtData.Rows)
                // {
                //     int i = 1;
                //     for (int j = colngayfrom; j <= colngayfrom + totalDays; j++)
                //     {
                //         if (i <= totalDays)
                //             dr[string.Format("ngay{0}", i.ToString())] = dr[j];
                //         else
                //             dr[string.Format("ngay{0}", i.ToString())] = 0;
                //         i++;
                //     }
                // }
                THU_VIEN_CHUNG.CreateXML(dtData, Application.StartupPath + @"\Xml4Reports\noitru_phieucongkhai_V1.XML");
                noitru_inphieu.InPhieu(dtData, DateTime.Now, "", true, "noitru_phieucongkhai_V1");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void InPhieuCongkhai()
        {
            try
            {
                Utility.WaitNow(this);
                Utility.SetMsg(lblMsg, "Đang tạo dữ liệu...", false);
                List<string> lstIdphieudieutri = (from p in grdPhieudieutri.GetCheckedRows()
                                                  select Utility.sDbnull(p.Cells["id_phieudieutri"].Value)
                                                ).ToList<string>();

                List<string> lstDays = (from p in grdPhieudieutri.GetCheckedRows()
                                        select Utility.sDbnull(p.Cells["sngay_dieutri"].Value)
                                                ).ToList<string>();

                DataSet dsData = SPs.NoitruPhieucongkhaiLaydulieuinV2(dtTuNgay.Value, dtDenNgay.Value, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, string.Join(",", lstIdphieudieutri.ToArray<string>()),
                    Utility.Bool2byte(chkThuoc.Checked), Utility.Bool2byte(chkVTTH.Checked), Utility.Bool2byte(chkCLS.Checked), Utility.Bool2byte(chkGiuong.Checked)).GetDataSet();
                THU_VIEN_CHUNG.SapxepthutuinPhieucongkhai(dsData.Tables[0], false);
                dsData.Tables[0].DefaultView.Sort = "id_loaithanhtoan,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_in,stt_hthi_chitiet,ten";
                if (dsData == null || dsData.Tables.Count <= 0 || dsData.Tables[0].Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu công khai.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                DataTable dtPhieuCongkhai = dsData.Tables[0].Copy();
                List<DataTable> lstData = new List<DataTable>();
                bool AutoFillEmpty = chkAutoFillEmpty.Checked;// THU_VIEN_CHUNG.Laygiatrithamsohethong("PHIEUCONGKHAI_AUTOFILL_EMPTYDAY", "1", true) == "1";
                int Cols =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("PHIEUCONGKHAI_SOCOT","10",true),0);
                DateTime mindate = Convert.ToDateTime(dtPhieuCongkhai.Rows[0]["minDate"]);
                DateTime maxdate = Convert.ToDateTime(dtPhieuCongkhai.Rows[0]["maxDate"]);
                int days = lstDays.Count;
                if (AutoFillEmpty) 
                    days = (int)Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, mindate, maxdate) + 1;
                int Pages = 0;
                Pages = (int)Math.Ceiling((decimal)days / (decimal)Cols);
                //Tính lại số ngày thực trong dữ liệu cần có
                days = Cols * Pages;
                //fill các ngày trống giữa min và max date. 
              
                if (AutoFillEmpty)
                {
                    for (int i = 0; i <= days - 1; i++)
                    {
                        DateTime newDate = mindate.AddDays(i);
                        string colName = newDate.ToString("dd/MM/yyyy");
                        if (!dtPhieuCongkhai.Columns.Contains(colName))
                        {
                            dtPhieuCongkhai.Columns.Add(colName, typeof(int));
                            //Update giá trị=0 cho các ngày giả đó
                            (from p in dtPhieuCongkhai.AsEnumerable() select p).ToList().ForEach(x => x[colName] = 0);
                        }
                    }
                }
                //Tách dữ liệu thành các bảng với các cột từ 1 đến Cols
                DateTime _mindateofPage = mindate;
                for (int i = 1; i <= Pages; i++)
                {
                    DataTable pagei = new DataTable();
                    pagei.TableName = string.Format("table_{0}", i.ToString());
                    _mindateofPage = mindate.AddDays((i-1)*Cols);
                    pagei.Columns.AddRange(new DataColumn[] { new DataColumn("id_loaithanhtoan", typeof(int)), new DataColumn("ten_loaithanhtoan", typeof(string)), new DataColumn("nhom_inphoiBHYT", typeof(string)), new DataColumn("id_loaidichvu", typeof(string)),
                    new DataColumn("ten_loaidichvu", typeof(string)), new DataColumn("stt_in", typeof(int)), new DataColumn("stt_hthi_loaidichvu", typeof(int)), new DataColumn("stt_hthi_dichvu", typeof(int)) ,
                    new DataColumn("stt_hthi_chitiet", typeof(int)), new DataColumn("kieu_thuocvattu", typeof(string)), new DataColumn("ten", typeof(string)), new DataColumn("ten_donvitinh", typeof(string)) , new DataColumn("ngay_min", typeof(DateTime)),
                     new DataColumn("id_benhnhan", typeof(long)), new DataColumn("ma_luotkham", typeof(string)), new DataColumn("ten_benhnhan", typeof(string)), new DataColumn("tuoi", typeof(int)) , new DataColumn("gioi_tinh", typeof(string)),
                     new DataColumn("dia_chi", typeof(string)), new DataColumn("nam_sinh", typeof(int)), new DataColumn("so_vaovien", typeof(long)), new DataColumn("ngay_nhapvien", typeof(DateTime)) , new DataColumn("ten_khoanoitru", typeof(string)), 
                     new DataColumn("ten_buong", typeof(string)), new DataColumn("ten_giuong", typeof(string)), new DataColumn("chan_doan", typeof(string)), new DataColumn("tong_cong", typeof(int))});
                    for (int k = 1; k <= 15; k++)
                    {
                        string datecol = string.Format("ngay_{0}", k.ToString());
                        pagei.Columns.Add(new DataColumn(datecol, typeof(int)));
                    }
                    foreach (DataRow dr in dtPhieuCongkhai.Rows)
                    {
                        DataRow newDr = pagei.NewRow();
                        newDr["ngay_min"] = _mindateofPage;
                        newDr["id_loaithanhtoan"] = dr["id_loaithanhtoan"];
                        newDr["ten_loaithanhtoan"] = dr["ten_loaithanhtoan"];
                        newDr["nhom_inphoiBHYT"] = dr["nhom_inphoiBHYT"];
                        newDr["id_loaidichvu"] = dr["id_loaidichvu"];
                        newDr["ten_loaidichvu"] = dr["ten_loaidichvu"];
                        newDr["stt_in"] = dr["stt_in"];
                        newDr["stt_hthi_loaidichvu"] = dr["stt_hthi_loaidichvu"];
                        newDr["stt_hthi_dichvu"] = dr["stt_hthi_dichvu"];
                        newDr["stt_hthi_chitiet"] = dr["stt_hthi_chitiet"];
                        newDr["kieu_thuocvattu"] = dr["kieu_thuocvattu"];
                        newDr["ten"] = dr["ten"];
                        newDr["ten_donvitinh"] = dr["ten_donvitinh"];

                        newDr["id_benhnhan"] = dr["id_benhnhan"];
                        newDr["ma_luotkham"] = dr["ma_luotkham"];
                        newDr["ten_benhnhan"] = dr["ten_benhnhan"];
                        newDr["tuoi"] = dr["tuoi"];
                        newDr["gioi_tinh"] = dr["gioi_tinh"];
                        newDr["nam_sinh"] = dr["nam_sinh"];
                        newDr["so_vaovien"] = dr["so_vaovien"];
                        newDr["ngay_nhapvien"] = dr["ngay_nhapvien"];

                        newDr["ten_khoanoitru"] = dr["ten_khoanoitru"];
                        newDr["ten_buong"] = dr["ten_buong"];
                        newDr["ten_giuong"] = dr["ten_giuong"];
                        newDr["chan_doan"] = dr["chan_doan"];

                        int tong_cong = 0;
                        //newDr["so_luong"] = dr["so_luong"];
                        for (int j = 0; j <= Cols-1; j++)
                        {
                            string datecol = string.Format("ngay_{0}", (j+1).ToString());
                            DateTime _date = _mindateofPage.AddDays(j);
                            string mappingcol = string.Format("{0}", _date.ToString("dd/MM/yyyy"));
                            if (dtPhieuCongkhai.Columns.Contains(mappingcol))
                                newDr[datecol] = dr[mappingcol];
                            else
                                newDr[datecol] = 0;
                            tong_cong += Utility.Int32Dbnull(newDr[datecol], 0);
                        }
                        newDr["tong_cong"] = tong_cong;
                        pagei.Rows.Add(newDr);
                    }
                    lstData.Add(pagei);
                }

                for (int i = 0; i <= lstData.Count - 1; i++)
                {
                    lstData[i].DefaultView.Sort = "id_loaithanhtoan,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_in,stt_hthi_chitiet,ten";
                    THU_VIEN_CHUNG.CreateXML(lstData[i],string.Format( Application.StartupPath + @"\Xml4Reports\noitru_phieucongkhai_{0}.XML",i.ToString()));
                    noitru_inphieu.InPhieuCongkhai(lstData[i], DateTime.Now, "", true, "noitru_phieucongkhai_V2");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                Utility.SetMsg(lblMsg, "...", false);
                Utility.DefaultNow(this);
            }
        }
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                Utility.SetMsg(lblMsg, "Đang tạo dữ liệu...", false);
                List<string> lstIdphieudieutri = (from p in grdPhieudieutri.GetCheckedRows()
                                                  select Utility.sDbnull(p.Cells["id_phieudieutri"].Value)
                                                ).ToList<string>();
                DataSet dsData = SPs.NoitruPhieucongkhaiLaydulieuinExcel(dtTuNgay.Value, dtDenNgay.Value, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, string.Join(",", lstIdphieudieutri.ToArray<string>()),
                    Utility.Bool2byte(chkThuoc.Checked), Utility.Bool2byte(chkVTTH.Checked), Utility.Bool2byte(chkCLS.Checked), Utility.Bool2byte(chkGiuong.Checked)).GetDataSet();
                THU_VIEN_CHUNG.SapxepthutuinPhieucongkhai(dsData.Tables[0], false);
                dsData.Tables[0].DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten";

                THU_VIEN_CHUNG.CreateXML_NOLOGO(dsData.Tables[0], Application.StartupPath + @"\Xml4Reports\noitru_phieucongkhai_Excel.XML");

                if (dsData == null || dsData.Tables.Count <= 0 || dsData.Tables[0].Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu công khai.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string tenloaiphieu = "PHIẾU CÔNG KHAI";
                string sfileName = AppDomain.CurrentDomain.BaseDirectory + "phieucongkhai\\phieucongkhai.xls";
                string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "phieucongkhai\\" + string.Format("phieucongkhai_{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                ExcelUtlity.Inphieucongkhai(dsData, "Phieucongkhai", sfileNameSave, tenloaiphieu);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                Utility.SetMsg(lblMsg, "...", false);
                Utility.DefaultNow(this);
            }
        }
    }
}
