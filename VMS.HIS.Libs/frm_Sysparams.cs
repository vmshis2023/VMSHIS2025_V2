using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
namespace VNS.Libs
{
    public partial class frm_Sysparams : Form
    {
        List<string> lstThamsohethongForm = new List<string>();
        string helpfile = "";
        public frm_Sysparams(List<string> lstThamsohethongForm,string helpfile)
        {
            InitializeComponent();
            this.helpfile = helpfile;
            this.lstThamsohethongForm = lstThamsohethongForm;
            this.KeyDown += frm_Sysparams_KeyDown;
            this.Load += frm_Sysparams_Load;
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.FormClosing += frm_Sysparams_FormClosing;
        }

        void frm_Sysparams_FormClosing(object sender, FormClosingEventArgs e)
        {
            globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
            globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
            LoadThamSoHeThong();
        }
        public  void LoadThamSoHeThong()
        {
            globalVariables.FORMTITLE = Laygiatrithamsohethong("FORMTITLE", false);
            globalVariables.LUONGCOBAN = Utility.DecimaltoDbnull(Laygiatrithamsohethong("BHYT_LUONGCOBAN", "83000", false), 83000);
            globalVariables.gv_strNoiDKKCBBD = Laygiatrithamsohethong("BHYT_NOIDANGKY_KCBBD", "016", false);
            globalVariables.gv_strDiadiem = Laygiatrithamsohethong("DIA_DIEM", "Hà Nội", false);
            globalVariables.gv_strNoicapBHYT = Laygiatrithamsohethong("BHYT_NOICAP_BHYT", "01", false);

            globalVariables.gv_intChophepchongiathuoc = Utility.Int32Dbnull(Laygiatrithamsohethong("CHONGIATHUOC", "0", false), 0);
            globalVariables.gv_blnApdungChedoDuyetBHYT = Laygiatrithamsohethong("BHYT_TUDONGDUYET", "1", false) == "1";

            globalVariables.gv_GiathuoctheoGiatrongKho = Laygiatrithamsohethong("GIATHUOCKHO", "1", false) == "1";
            globalVariables.ChophepNhapkhoLe = Utility.Int32Dbnull(Laygiatrithamsohethong("ChophepNhapkhoLe", "0", false));
            globalVariables.gv_strTuyenBHYT = Laygiatrithamsohethong("BHYT_TUYEN", "TW", false);
            globalVariables.TrongGio = Laygiatrithamsohethong("KCB_TRONGGIO", "0:00-23:59", false);
            globalVariables.TrongNgay = Laygiatrithamsohethong("KCB_TRONGNGAY", "2,3,4,5,6,7,CN", false);
            globalVariables.gv_intKT_TT_ChuyenCLS_DV = Utility.Int32Dbnull(Laygiatrithamsohethong("KT_TT_ChuyenCLS_DV", "0", false), 0);
            globalVariables.gv_strBHYT_MAQUYENLOI_UUTIEN = Laygiatrithamsohethong("BHYT_MAQUYENLOI_UUTIEN", "", false);
            globalVariables.gv_intKT_TT_ChuyenCLS_BHYT = Utility.Int32Dbnull(Laygiatrithamsohethong("KT_TT_ChuyenCLS_BHYT", "0", false), 0);
            globalVariables.gv_strICD_BENH_AN_NGOAI_TRU = Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "", false);

            globalVariables.gv_intSO_BENH_AN_BATDAU = Utility.Int32Dbnull(Laygiatrithamsohethong("KCB_SO_BENH_AN", "-1", false), -1);
            globalVariables.gv_strMA_BHYT_KT = Laygiatrithamsohethong("MA_BHYT_KT", "", false);
            globalVariables.gv_strMaQuyenLoiHuongBHYT100Phantram = Laygiatrithamsohethong("BHYT_MAQUYENLOI_HUONG100PHANTRAM", "1,2", false);
            globalVariables.gv_intCHARACTERCASING = Utility.Int32Dbnull(Laygiatrithamsohethong("TIEPDON_CHARACTERCASING", "0", false), 0);
            globalVariables.gv_intKIEMTRAMATHEBHYT = Utility.Int32Dbnull(Laygiatrithamsohethong("BHYT_KIEMTRAMATHE", "0", false), 0);
            globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN = Utility.Int32Dbnull(Laygiatrithamsohethong("BHYT_TUDONGCHECKTRAITUYEN", "0", false), 0);
            globalVariables.gv_strBOTENDIACHINH = Laygiatrithamsohethong("BOTENDIACHINH", "", false);

            if (globalVariablesPrivate.objNhanvien != null)
            {
                globalVariables.gv_strTenNhanvien = globalVariablesPrivate.objNhanvien.TenNhanvien;
                globalVariables.gv_intIDNhanvien = globalVariablesPrivate.objNhanvien.IdNhanvien;
            }
            else
            {
                globalVariables.gv_strTenNhanvien = globalVariables.UserName;
                globalVariables.gv_intIDNhanvien = -1;
            }
            globalVariables.gv_dtQuyenNhanvien = new Select().From(QheNhanvienQuyensudung.Schema).Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(globalVariables.gv_intIDNhanvien).ExecuteDataSet().Tables[0];
            globalVariables.gv_dtQuyenNhanvien_Dmuc = new Select().From(QheNhanvienDanhmuc.Schema).Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(globalVariables.gv_intIDNhanvien).ExecuteDataSet().Tables[0];
        }
        public  string Laygiatrithamsohethong(string paramName, bool fromDB)
        {
            try
            {
                string reval = null;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            paramName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return null;
            }
        }
        public  string Laygiatrithamsohethong(string paramName, string defaultval, bool fromDb)
        {
            try
            {
                //  fromDB = true;
                string reval = defaultval;
                if (fromDb)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            paramName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDr = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + paramName + "'");
                    if (arrDr.Length > 0) reval = Utility.sDbnull(arrDr[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        public  SysSystemParameter Laythamsohethong(string ParamName)
        {
            try
            {

                SqlQuery sqlQuery =
                    new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                        ParamName);
                SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();

                return objSystemParameter;
            }
            catch
            {
                return null;
            }
        }
        void frm_Sysparams_Load(object sender, EventArgs e)
        {
            globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
            string filter = "1=1";
            if(lstThamsohethongForm.Count>0)
                filter=string.Format("{0} in {1}", SysSystemParameter.Columns.SName, string.Join(",",lstThamsohethongForm.ToArray<string>()));
            Utility.SetDataSourceForDataGridEx_Basic(grdList, globalVariables.gv_dtSysparams, true, true, filter, SysSystemParameter.Columns.SName);
        }

        void grdList_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            
            try
            {
                if (e.Column.Key == SysSystemParameter.Columns.SValue)
                {
                    int id = Utility.Int32Dbnull(grdList.CurrentRow.Cells[SysSystemParameter.Columns.Id].Value);
                    new Update(SysSystemParameter.Schema)
                        .Set(SysSystemParameter.Columns.SValue).EqualTo(e.Value)
                        .Where(SysSystemParameter.Columns.Id).IsEqualTo(id)
                        .Execute();

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void frm_Sysparams_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F1)
                Utility.OpenHelpFile(helpfile);

        }
    }
}
