using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_InBangKeVTTH_TheoGoiMo : Form
    {
      
        public KcbLuotkham objLuotkham;
       
        bool blnLoaded = false;
        public bool isCancel = true;
       
        private NLog.Logger log;
        byte v_bytNoitru = 0;//0= ngoại trú;1= nội trú
        bool AllowSeletionChanged = false;
        public DateTime pdt_InputDate = globalVariables.SysDate;
      
        public frm_InBangKeVTTH_TheoGoiMo(KcbLuotkham objLuotkham)
        {
            InitializeComponent();
            this.objLuotkham = objLuotkham;
            Utility.SetVisualStyle(this);
            grdChiDinh.SelectionChanged += grdChiDinh_SelectionChanged;
        }
        KcbChidinhclsChitiet objKcbChidinhclsChitiet = null;
        private void grdChiDinh_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChiDinh) || !AllowSeletionChanged)
                {
                    objKcbChidinhclsChitiet = null;
                    grdVTTH.DataSource = null;
                    cmd_in.Enabled= false;
                    return;
                }
                else
                {
                  
                    long id_chitietchidinh = Utility.Int64Dbnull(grdChiDinh.GetValue("id_chitietchidinh"));
                    objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(id_chitietchidinh);
                    if (objKcbChidinhclsChitiet != null)
                    {
                        objLuotkham = Utility.getKcbLuotkham(grdChiDinh.CurrentRow);

                        LayDanhsachVTTH();
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }
        DataTable dtVTTH = new DataTable();
        private void LayDanhsachVTTH()
        {
            try
            {
                dtVTTH =
                     new KCB_THAMKHAM().KcbThamkhamLayDanhsachDonThuocTheolankham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, -1l, -1l, 4, "VT", objKcbChidinhclsChitiet.IdChitietchidinh, 0).Tables[0];
                Utility.SetDataSourceForDataGridEx(grdVTTH, dtVTTH, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        private void UcThongtinnguoibenh_v21__OnEnterMe()
        {
            try
            {
                if (ucThongtinnguoibenh_v21.objLuotkham != null)
                {
                    this.objLuotkham = ucThongtinnguoibenh_v21.objLuotkham;
                    GetCls();


                }
            }
            catch (Exception ex)
            {


            }
        }
        void GetCls()
        {
            try
            {
                AllowSeletionChanged = false;
                byte ntnt = 100;
                DataTable dtCls = SPs.KcbPtttTimkiemchidinhPttt(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, ntnt).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiDinh, dtCls, true, true, "1=1", "");
                AllowSeletionChanged = true;
                grdChiDinh_SelectionChanged(grdChiDinh, new EventArgs());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }



        private void frm_InBangKeVTTH_TheoGoiMo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                GetCls();
            }
        }

        private void frm_InBangKeVTTH_TheoGoiMo_Load(object sender, EventArgs e)
        {
            try
            {

                ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_v21.Refresh(false);

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {

            }
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
