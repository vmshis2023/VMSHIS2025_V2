using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using System.IO;
using VNS.HIS.UI.Forms.HinhAnh;
using VMS.HIS.Danhmuc;
namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_chonvungksat : Form
    {
        List<string> lstId = new List<string>();
        public int Id = -1;
        public string ten = "";
        public bool Hthi_Chon = false;
        public string vungks = "";
        public string ten_dvu = "";
        public frm_chonvungksat(List<string> lstId)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.lstId = lstId;
            this.Load += frm_chonvungksat_Load;
            this.FormClosing += frm_chonvungksat_FormClosing;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            this.KeyDown += frm_chonvungksat_KeyDown;
            
        }

        void frm_chonvungksat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Accept();
            else if (e.KeyCode == Keys.Escape) this.Close();
        }

        void frm_chonvungksat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK) return;
            if (Hthi_Chon)
            {
                
                    if (grdList.GetCheckedRows().Count() > 0)
                    {
                        var query = (from chk in grdList.GetCheckedRows()
                                     let x = Utility.sDbnull(chk.Cells[DmucVungkhaosat.Columns.Id].Value)
                                     select x).ToArray();
                        if (query != null && query.Count() > 0)
                        {
                            vungks = string.Join(",", query);
                        }
                    }
                    else
                    {
                        vungks = Utility.GetValueFromGridColumn(grdList, DmucVungkhaosat.Columns.Id);
                    }
                    if (Utility.DoTrim(vungks) != "")
                        Utility.ShowMsg(string.Format("Đã gắn vùng khảo sát thành công cho dịch vụ {0}", ten_dvu));
            }
        }
        void Accept()
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == Janus.Windows.GridEX.RowType.Record)
            {
                Id = Utility.Int32Dbnull(grdList.CurrentRow.Cells["ID"].Value, -1);
                ten = Utility.sDbnull(grdList.CurrentRow.Cells["ten_vungkhaosat"].Value, "");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Accept();
        }
        DataTable _dtVungKS = null;
        void LoadData()
        {
            
            string serviceCode = "ALL";
            serviceCode =Util.getMaKieuDichVu();
            if (lstId.Count <= 0 || (lstId.Count == 1 && lstId[0].TrimEnd().TrimStart() == ""))
            {
                if (serviceCode == "ALL" || globalVariables.IsAdmin)
                    _dtVungKS = new Select().From(DmucVungkhaosat.Schema.Name)
                        .Where(DmucVungkhaosat.Columns.TrangThai).IsEqualTo(1)
                    .ExecuteDataSet().Tables[0];
                else
                    _dtVungKS = new Select().From(DmucVungkhaosat.Schema.Name)
                        .Where(DmucVungkhaosat.Columns.MaLoaidvu).In(serviceCode.Split(',').ToList<string>())
                        .And(DmucVungkhaosat.Columns.TrangThai).IsEqualTo(1)
                        .ExecuteDataSet().Tables[0];
                grdList.DataSource = _dtVungKS;
            }
            else
            {
                grdList.DataSource = new Select().From(DmucVungkhaosat.Schema).Where(DmucVungkhaosat.Columns.Id).In(lstId).ExecuteDataSet().Tables[0];
            }
        }
        void frm_chonvungksat_Load(object sender, EventArgs e)
        {
            Utility.grdExVisiableColName(grdList, "colChon", Hthi_Chon);
            LoadData();
        }

        private void cmdInsertVKS_Click(object sender, EventArgs e)
        {
            var obj = new DmucVungkhaosat { NgayTao = DateTime.Now };
            var f = new frm_themmoi_vungkhaosat("ALL") { m_enAct = action.Insert, Table = _dtVungKS, Obj = obj };
            f.ShowDialog();
        }
    }
    
}
