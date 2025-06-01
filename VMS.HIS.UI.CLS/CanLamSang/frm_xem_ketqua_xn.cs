using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class frm_xem_ketqua_xn : Form
    {
        string ma_luotkham = "";
        Int64 id_benhnhan = -1;
        long id_chidinh = -1;
        string ma_chidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        byte noitru = 0;
        DataSet dsXn = new DataSet();
        bool AllowSelectionChanged = false;
        byte id_gioitinh = 0;
        public frm_xem_ketqua_xn(string ma_chidinh,long id_chidinh,string ma_luotkham,long id_benhnhan,byte id_gioitinh,byte noitru)
        {
            InitializeComponent();
            this.ma_chidinh = ma_chidinh;
            this.id_chidinh = id_chidinh;
            this.ma_luotkham = ma_luotkham;
            this.id_benhnhan = id_benhnhan;
            this.id_gioitinh = id_gioitinh;
            this.noitru = noitru;
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_xem_ketqua_xn_Load;
            this.KeyDown += frm_xem_ketqua_xn_KeyDown;
           ucThongtinnguoibenh_v31._OnEnterMe+=ucThongtinnguoibenh_v31__OnEnterMe;
           ucThongtinnguoibenh_v31.SetReadonly();
           grdChidinh.SelectionChanged += grdChidinh_SelectionChanged;
        
        }

        void grdChidinh_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChidinh) || !AllowSelectionChanged)
                {
                    dsXn.Tables[0].DefaultView.RowFilter = "1=2";
                    dsXn.Tables[0].AcceptChanges();
                }
                else
                {
                    dsXn.Tables[0].DefaultView.RowFilter = string.Format("ma_chidinh='{0}'", grdChidinh.GetValue("ma_chidinh"));
                    dsXn.Tables[0].AcceptChanges();
                }
                Utility.SetDataSourceForDataGridEx(grdKetqua, dsXn.Tables[1], true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,stt_in");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void ucThongtinnguoibenh_v31__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v31.objLuotkham != null)
            {
            }
        }

     
        void frm_xem_ketqua_xn_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2 && grdKetqua.GetDataRows().Length>0)
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
        }

        void frm_xem_ketqua_xn_Load(object sender, EventArgs e)
        {
            AllowSelectionChanged = false;
            dsXn = SPs.ClsLayKetquaXn(id_benhnhan, ma_luotkham, ma_chidinh, id_chidinh, noitru, id_gioitinh).GetDataSet();
            Utility.SetDataSourceForDataGridEx(grdChidinh, dsXn.Tables[0], true, true, "1=1", "ngay_chidinh,ma_chidinh");
            AllowSelectionChanged = true ;
            grdChidinh_SelectionChanged(grdChidinh, e);
        }

       
      

       private void cmdExit_Click(object sender, EventArgs e)
       {
           this.Close();
       }
    }
}
