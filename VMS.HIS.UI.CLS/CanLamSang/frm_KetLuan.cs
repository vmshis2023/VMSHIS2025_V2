using System;
using System.Data;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    public partial class frm_KetLuan : Form
    {
        public frm_KetLuan()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNhapKetLuan_Click(object sender, EventArgs e)
        {
            int record=new Update(KcbChidinhclsChitiet.Schema)
                //.Set(KcbChidinhclsChitiet.Columns.KetLuan).EqualTo(txtKetLuan.Text)
                .Set(KcbChidinhclsChitiet.Columns.NguoiThuchien).EqualTo(globalVariables.UserName)
                .Set(KcbChidinhclsChitiet.Columns.IdBacsiThuchien).EqualTo(globalVariables.gv_intIDNhanvien)
                .Set(KcbChidinhclsChitiet.Columns.NgayThuchien).EqualTo(globalVariables.SysDate)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID).Execute();
            if(record>0)
            {
                DataRow[] arr = P_dtDataTable.Select("AssignDetail_ID=" + AssignDetail_ID);
                if(arr.GetLength(0)>0)
                {
                    //arr[0][KcbChidinhclsChitiet.Columns.KetLuan] = txtKetLuan.Text;
                    arr[0][KcbChidinhclsChitiet.Columns.NgayThuchien] = globalVariables.SysDate;
                    arr[0][KcbChidinhclsChitiet.Columns.NguoiThuchien] = globalVariables.UserName;
                    arr[0][KcbChidinhclsChitiet.Columns.IdBacsiThuchien] = globalVariables.gv_intIDNhanvien;
                }
                P_dtDataTable.AcceptChanges();
                Utility.ShowMsg("Bạn thực hiện thành công","Thông báo");
            }
        }

        public int AssignDetail_ID = -1;
        public DataTable P_dtDataTable=new DataTable();
        /// <summary>
        /// ham thực hiện 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KetLuan_Load(object sender, EventArgs e)
        {
            KcbChidinhclsChitiet assignDetail = KcbChidinhclsChitiet.FetchByID(AssignDetail_ID);
            if(assignDetail!=null)
            {
                //txtKetLuan.Text = Utility.sDbnull(assignDetail.KetLuan, "");
            }
        }
    }
}
