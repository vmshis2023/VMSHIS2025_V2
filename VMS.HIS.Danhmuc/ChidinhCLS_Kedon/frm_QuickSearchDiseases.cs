using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_QuickSearchDiseases : Form
    {
        #region "Hàm thực hiện khai báo thuộc tính"
        public DataTable p_dtDiseases=new DataTable();
        private DataTable dt_DiseaseInfo=new DataTable();
        private string _rowFilter = "1=1";
        public bool m_blnCancel = false;
        public string v_DiseasesCode = "";
        public int v_Disease_Id = -1;
        public int DiseaseType_ID = -1;
        #endregion

        #region "Hàm khởi tạo đối tượng"
        public frm_QuickSearchDiseases()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
            InitEvents();
        }
        void InitEvents()
        {
            this.grdSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdSearch_KeyDown);
            this.grdSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdSearch_MouseDoubleClick);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            this.Load += new System.EventHandler(this.frm_QuickSearchDiseases_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_QuickSearchDiseases_KeyDown);
        }
        #endregion

        #region "phần khai báo sự kiện của form"
        /// <summary>
        /// đong form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        /// <summary>
        /// hàm thực hiện dùng phiims tắt của fiorm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuickSearchDiseases_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdClose.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện load thông tin của form lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuickSearchDiseases_Load(object sender, EventArgs e)
        {
            BindData();
            ModifyCommand();
            grdSearch.Focus();
            grdSearch.MoveFirst();
        }
        /// <summary>
        /// hàm thực hiện chấp nhận 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdSearch)) return;
            v_DiseasesCode = Utility.sDbnull(grdSearch.CurrentRow.Cells["ma_benh"].Value, "");
            if (string.IsNullOrEmpty(v_DiseasesCode)) return;
           // DiseaseType_ID = Utility.Int32Dbnull(grdSearch.CurrentRow.Cells["id_loaibenh"].Value.ToString(), -1);
            v_Disease_Id = Utility.Int32Dbnull(grdSearch.CurrentRow.Cells["id_benh"].Value, -1);
            m_blnCancel = true;
            this.Close();

        }
        /// <summary>
        /// hàm thực hiện sự kiện click chuột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                cmdAccept.PerformClick();
            }catch(Exception exception)
            {}
            
        }
        /// <summary>
        /// hàm thực hiện phím tát trên grdivewi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdSearch.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                    cmdAccept.PerformClick();
            }
        }
        /// <summary>
        /// hàm thực hiện bind thông tin 
        /// </summary>
        private void BindData()
        {
            dt_DiseaseInfo = globalVariables.gv_dtDmucBenh;
            if (DiseaseType_ID != -1)
            {
                _rowFilter = "id_loaibenh=" + DiseaseType_ID;
            }
            dt_DiseaseInfo.DefaultView.RowFilter = _rowFilter;
            grdSearch.DataSource = dt_DiseaseInfo.DefaultView;
        }
        private void ModifyCommand()
        {
            cmdAccept.Enabled = grdSearch.RowCount > 0;
            //cmdAccept.Enabled=grdSearch.f
        }
        #endregion

      

      

      

       
    }
}
