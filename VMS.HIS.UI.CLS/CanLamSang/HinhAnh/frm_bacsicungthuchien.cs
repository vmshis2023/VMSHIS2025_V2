using Janus.Windows.GridEX;
using SubSonic;
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

namespace VNS.HIS.UI.Forms.HinhAnh
{
    public partial class frm_bacsicungthuchien : Form
    {
        DataTable dtDoctor=new DataTable();
        int id_chidinhchitiet = -1;
        public frm_bacsicungthuchien(DataTable dtDoctor,int id_chidinhchitiet)
        {
            InitializeComponent();
            this.Load += frm_bacsicungthuchien_Load;
            this.KeyDown += frm_bacsicungthuchien_KeyDown;
            this.dtDoctor = dtDoctor;
            this.id_chidinhchitiet = id_chidinhchitiet;
        }

        void frm_bacsicungthuchien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else if (e.KeyCode == Keys.S && e.Control)
            {
                cmdSave.PerformClick();
                return;
            }
        }

        void frm_bacsicungthuchien_Load(object sender, EventArgs e)
        {
            string lstDoctor=string.Join(",", (from p in dtDoctor.AsEnumerable()
                                               select p.Field<string>("ma_nhanvien")).ToList<string>().ToArray());
            DataTable dt = SPs.KcbHinhanhLayDsBScungthuchien(id_chidinhchitiet, lstDoctor).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, dt, true, true, "1=1", "ten_bacsi asc");
            foreach (GridEXRow _r in grdList.GetRows())
            {
                if (Utility.Int32Dbnull(_r.Cells["isChecked"].Value, 0) > 0)
                    _r.IsChecked = true;
                else
                    _r.IsChecked = false;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridEXRow _r in grdList.GetRows())
                {
                    string ma_bacsi = _r.Cells["ma_bacsi"].Value.ToString();
                    string noi_dung = _r.Cells["ten_nhiemvu"].Value.ToString();
                    KcbBacsicungthuchien _obj = new Select().From(KcbBacsicungthuchien.Schema).Where(KcbBacsicungthuchien.Columns.IdChidinhchitiet).IsEqualTo(id_chidinhchitiet).And(KcbBacsicungthuchien.Columns.MaBacsi).IsEqualTo(ma_bacsi).ExecuteSingle<KcbBacsicungthuchien>();
                    if (_obj == null)
                    {
                        if (_r.IsChecked)
                        {
                            _obj = new KcbBacsicungthuchien();
                            _obj.IdChidinhchitiet = id_chidinhchitiet;
                            _obj.MaBacsi = ma_bacsi;
                            _obj.NoiDung = noi_dung;
                            _obj.NgayTao = DateTime.Now;
                            _obj.Cungthuchien = false;
                            _obj.NguoiTao = globalVariables.UserName;
                            _obj.IsNew = true;
                            _obj.Save();
                        }
                    }
                    else
                    {
                        if (!_r.IsChecked)
                        {
                            new Delete().From(KcbBacsicungthuchien.Schema).Where(KcbBacsicungthuchien.Columns.IdChidinhchitiet).IsEqualTo(id_chidinhchitiet).And(KcbBacsicungthuchien.Columns.MaBacsi).IsEqualTo(ma_bacsi).Execute();
                            continue;
                        }
                        else
                        {
                            //_obj.Cungthuchien = false;
                            _obj.NoiDung = noi_dung;
                            _obj.NgaySua = DateTime.Now;
                            _obj.NguoiSua = globalVariables.UserName;
                            _obj.IsNew = false;
                            _obj.MarkOld();
                            _obj.Save();
                        }
                    }
                    
                }
                this.Close();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
