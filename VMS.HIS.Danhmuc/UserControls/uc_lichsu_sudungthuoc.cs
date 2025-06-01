using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.Danhmuc.UserControls
{
    public partial class uc_lichsu_sudungthuoc : UserControl
    {
        public uc_lichsu_sudungthuoc()
        {
            InitializeComponent();
        }
        public void SetDataSource(DataTable dtData)
        {
            try
            {
                Utility.SetDataSourceForDataGridEx_Basic(grdDonthuoc, dtData, true, true, "", KcbDonthuocChitiet.Columns.SttIn);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
              
            }
        }
       
    }
}
