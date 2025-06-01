using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.DANHMUC;
using Excel;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_DoichieuGiaDvuKCB_Truockhichuyendoi : Form
    {
        bool AllowSelectionChanged_Old = false;
        bool AllowSelectionChanged_New = false;
        private DataTable dtData=new DataTable();
        KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
        string ma_doituongkcb_moi = "DV";
        public frm_DoichieuGiaDvuKCB_Truockhichuyendoi(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan, string ma_doituongkcb_moi)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            this.objLuotkham = objLuotkham;
            this.objBenhnhan = objBenhnhan;
            this.ma_doituongkcb_moi = ma_doituongkcb_moi;
            InitEvents();
        }
        public frm_DoichieuGiaDvuKCB_Truockhichuyendoi(DataTable dtData)
        {
            InitializeComponent();
          
            Utility.SetVisualStyle(this);
            this.dtData = dtData;
            InitEvents();
        }
        void InitEvents()
        {
            cmdCancel.Click += cmdCancel_Click;
            cmdSave.Click += cmdSave_Click;
            this.Load+=frm_DoichieuGiaDvuKCB_Truockhichuyendoi_Load;
            this.Shown += Frm_DoichieuGiaDvuKCB_Truockhichuyendoi_Shown;
            grd_NewList.SelectionChanged += Grd_NewList_SelectionChanged;
            grd_OldList.SelectionChanged += Grd_OldList_SelectionChanged;
        }

        private void Frm_DoichieuGiaDvuKCB_Truockhichuyendoi_Shown(object sender, EventArgs e)
        {
            LoadMe();
        }

        private void Grd_OldList_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowSelectionChanged_Old) return;
            AllowSelectionChanged_New = false;
            Utility.GotoNewRowJanus(grd_NewList, "privatekey", Utility.sDbnull(grd_OldList.GetValue("privatekey")));
            AllowSelectionChanged_New = true;
        }

        private void Grd_NewList_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowSelectionChanged_New) return;
            AllowSelectionChanged_Old = false;
            Utility.GotoNewRowJanus(grd_OldList, "privatekey", Utility.sDbnull( grd_NewList.GetValue("privatekey")));
            AllowSelectionChanged_Old = true;
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_DoichieuGiaDvuKCB_Truockhichuyendoi_Load(object sender, EventArgs e)
        {
        }
       void LoadMe()
        {
            try
            {

                DataSet dsData = SPs.KcbLaythongtinGiaDvuChuyendoituongKCB(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet();
                DataTable dtGia = dsData.Tables[0];
                DataTable dtData = dsData.Tables[1]; 
                int idx = 0;
                foreach(DataRow dr in dtData.Rows)
                {
                    byte trangthai_thanhtoan = Utility.ByteDbnull(dr["trangthai_thanhtoan"]);
                    if (trangthai_thanhtoan == 1) continue;
                    dr[KcbLuotkham.Columns.PtramBhyt] = objLuotkham.PtramBhyt;
                    dr["privatekey"] = idx;
                    //Lấy giá của dịch vụ theo đối tượng mới
                    int id_loaithanhtoan = Utility.Int32Dbnull(dr["id_loaithanhtoan"]);
                    int id_chitietdichvu = Utility.Int32Dbnull(dr["id_chitietdichvu"]);
                    DataRow[] arrDr = dtGia.Select(string.Format("id_loaithanhtoan={0} and id_dichvu={1} and ma_doituongkcb='{2}'", id_loaithanhtoan, id_chitietdichvu, ma_doituongkcb_moi));
                    if(arrDr.Length>0)
                    {
                        dr["don_gia"] = Utility.DecimaltoDbnull(arrDr[0]["don_gia"]);
                        dr["phu_thu"] =Utility.Byte2Bool( objLuotkham.DungTuyen)?Utility.DecimaltoDbnull( arrDr[0]["phuthu_dungtuyen"]): Utility.DecimaltoDbnull(arrDr[0]["phuthu_traituyen"]);
                        THU_VIEN_CHUNG.Bhyt_PhantichGia(objLuotkham, dr);
                    }    
                }    
                Utility.SetDataSourceForDataGridEx(grd_OldList, dtData, false, true, "1=1", "");
                Utility.SetDataSourceForDataGridEx(grd_NewList, dtData.Copy(), false, true, "1=1", "");
            }
            catch (Exception ex)
            {

               
            }
        }
    }
}
