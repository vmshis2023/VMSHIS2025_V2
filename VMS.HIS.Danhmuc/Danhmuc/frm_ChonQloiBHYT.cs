using System;
using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_ChonQloiBHYT : Form
    {
        public DataTable m_dtqhe=new DataTable();
        public decimal Original_Price = 0;
        public int DetailService = -1;
        public string MaGoiDV = "";
        public bool m_blnCancel = true;
        public enObjectType _enObjectType = enObjectType.DichvuCLS;
        public string MaKhoaTHIEN = "";
        string ma_doituongbhyt = "";
        public frm_ChonQloiBHYT(string ma_doituongbhyt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.ma_doituongbhyt = ma_doituongbhyt;
            cmdAccept.Click+=new EventHandler(cmdAccept_Click);
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            lblTitle.Text = "CHỌN QUYỀN LỢI BHYT CHO MÃ ĐẦU THẺ" + ma_doituongbhyt.ToUpper();
        }
        /// <summary>
        /// hàm thực hiện đóng Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frm_ChonQloiBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdClose.PerformClick();
            if(e.Control && e.KeyCode==Keys.S)cmdAccept.PerformClick();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "" , true);
            if (!isValidData()) return;
            SaveData();
                    
            
        }
        bool isValidData()
        {
            try
            {
                bool noSelect = true;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    noSelect = false;
                   int ptram_bhyt = Utility.Int32Dbnull(gridExRow.Cells[QheDautheQloiBhyt.Columns.PhantramBhyt].Value, -1);
                   string ma_qloi = Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value, "");
                   if (ptram_bhyt < 0)
                   {
                       Utility.SetMsg(lblMsg, "Bạn cần nhập phần trăm BHYT cho mã quyền lợi ="+ma_qloi, true);
                       return false;
                   }
                }
                if (noSelect)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một mã quyền lợi trước khi lưu");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SaveData()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    DataRow newDr = m_dtqhe.NewRow();
                    newDr[QheDautheQloiBhyt.Columns.MaDoituongbhyt] = this.ma_doituongbhyt;
                    newDr[QheDautheQloiBhyt.Columns.MaQloi] = Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value, "");
                    newDr[QheDautheQloiBhyt.Columns.PhantramBhyt] = Utility.Int32Dbnull(gridExRow.Cells[QheDautheQloiBhyt.Columns.PhantramBhyt].Value, 0);
                    m_dtqhe.Rows.Add(newDr);

                    QheDautheQloiBhyt _QheDautheQloiBhyt = new QheDautheQloiBhyt();
                    _QheDautheQloiBhyt.MaDoituongbhyt = this.ma_doituongbhyt;
                    _QheDautheQloiBhyt.MaQloi = Utility.ByteDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value, "");
                    _QheDautheQloiBhyt.PhantramBhyt = Utility.Int16Dbnull(gridExRow.Cells[QheDautheQloiBhyt.Columns.PhantramBhyt].Value, 0);
                    _QheDautheQloiBhyt.IsNew = true;
                    _QheDautheQloiBhyt.Save();
                    m_blnCancel = false;
                } 
                m_dtqhe.AcceptChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        private DataTable m_dtObjectType=new DataTable();
       
        private void LoadData()
        {
            try
            {

                m_dtObjectType = THU_VIEN_CHUNG.LayDulieuDanhmucChung("MAQUYENLOIBHYT", true);
                if (!m_dtObjectType.Columns.Contains(QheDautheQloiBhyt.Columns.PhantramBhyt))
                {
                    Utility.AddColumToDataTable(ref m_dtObjectType, QheDautheQloiBhyt.Columns.PhantramBhyt, typeof(Int16));
                }
               
                foreach (DataRowView drv in m_dtqhe.DefaultView)
                {
                    DataRow[] arrDr = m_dtObjectType.Select(DmucChung.Columns.Ma + "='" + Utility.sDbnull(drv[QheDautheQloiBhyt.Columns.MaQloi], "-1") + "'");
                    if (arrDr.GetLength(0) > 0)
                    {
                        m_dtObjectType.Rows.Remove(arrDr[0]);
                        m_dtObjectType.AcceptChanges();
                    }
                }
                grdList.DataSource = m_dtObjectType;
            }
            catch
            {
            }
            
        }
      
        private void frm_ChonQloiBHYT_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void chkCheckAllorNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckAllorNone.Checked)
                grdList.CheckAllRecords();
            else
                grdList.UnCheckAllRecords();
        }
    }
}
