using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using Janus.Windows.GridEX;
namespace  VNS.HIS.UI.NGOAITRU
{
    public partial class frm_DanhSach_ICD : Form
    {
        public int IcdCHINHorPHU = -1;
        public DataTable dt_ICD;
        public int CP = 0;
        public List< GridEXRow> lstSelectedRows=new List<GridEXRow>();
        public bool has_Cancel = true;
        public string mabenh { get; set; }
        public frm_DanhSach_ICD(int CP)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.CP = CP;
            grd_List.RootTable.Columns["colCHON"].Visible = this.CP == 1;
            grd_List.KeyDown += new KeyEventHandler(grd_List_KeyDown);
            this.grd_List.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grd_List_KeyPress);
            grd_List.MouseDoubleClick += grd_List_MouseDoubleClick;
            this.Load += new System.EventHandler(this.frm_DanhSach_ICD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_DanhSach_ICD_KeyDown);
            cmdSavePres.Click += cmdSavePres_Click;
            cmdExit.Click += cmdExit_Click;
          
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            has_Cancel = true;
            this.Close();
        }

        void cmdSavePres_Click(object sender, EventArgs e)
        {
            Accept();
        }

        void grd_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Accept();
        }
        void Accept()
        {
            lstSelectedRows = grd_List.GetCheckedRows().ToList<GridEXRow>();
            if (lstSelectedRows.Count <= 0)
                lstSelectedRows.Add(grd_List.CurrentRow);
            has_Cancel = false;
            Close();
        }
        void grd_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.CP == 1 && e.KeyCode == Keys.Space && grd_List.CurrentColumn != null && Utility.sDbnull(grd_List.CurrentColumn.Key, "") != "colCHON")
            {
                grd_List.CurrentRow.IsChecked = !grd_List.CurrentRow.IsChecked;
            }
            
        }
        private void frm_DanhSach_ICD_Load(object sender, EventArgs e)
        {
            lnkAll.Visible = dt_ICD.Rows.Count < globalVariables.gv_dtDmucBenh.Rows.Count;
            grd_List.DataSource = dt_ICD;
            if(!string.IsNullOrEmpty(mabenh))
            {
                var query = from benh in grd_List.GetDataRows()
                            where Utility.sDbnull(benh.Cells["FirstChar"].Value).Contains(mabenh)
                                  || Utility.sDbnull(benh.Cells["FirstChar"].Value).EndsWith(mabenh)
                                  || Utility.sDbnull(benh.Cells["FirstChar"].Value).StartsWith(mabenh)
                            select Utility.sDbnull(benh.Cells[DmucBenh.Columns.MaBenh].Value);
                if(query.Any())
                {
                    Utility.GotoNewRowJanus(grd_List,DmucBenh.Columns.MaBenh,query.FirstOrDefault());
                }

            }
          
                       
        }

        private void frm_DanhSach_ICD_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter ||(e.Control && e.KeyCode==Keys.A)) && grd_List.RowCount>0)
            {
                Accept();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                has_Cancel = true;
                Close();
            }
        }

       

        private void grd_List_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsLetter(e.KeyChar))
            //{
                //if (Char.IsLetterOrDigit(e.KeyChar))
                //{
                //        mabenh = Utility.sDbnull(e.KeyChar);

                //        var querymabenh = from benh in grd_List.GetDataRows()
                //                    where Utility.sDbnull(benh.Cells[DmucBenh.Columns.MaBenh].Value).Contains(mabenh)
                //                          || Utility.sDbnull(benh.Cells[DmucBenh.Columns.MaBenh].Value).EndsWith(mabenh)
                //                          || Utility.sDbnull(benh.Cells[DmucBenh.Columns.MaBenh].Value).StartsWith(mabenh)
                //                    select Utility.sDbnull(benh.Cells[DmucBenh.Columns.MaBenh].Value);

                //        if (querymabenh.Any())
                //        {
                //            Utility.GotoNewRowJanus(grd_List, DmucBenh.Columns.MaBenh, querymabenh.FirstOrDefault());
                //        }
                   
                //        var query = from benh in grd_List.GetDataRows()
                //                    where Utility.sDbnull(benh.Cells[DmucBenh.Columns.TenBenh].Value).Contains(mabenh)
                //                          || Utility.sDbnull(benh.Cells[DmucBenh.Columns.TenBenh].Value).EndsWith(mabenh)
                //                          || Utility.sDbnull(benh.Cells[DmucBenh.Columns.TenBenh].Value).StartsWith(mabenh)
                //                    select Utility.sDbnull(benh.Cells[DmucBenh.Columns.MaBenh].Value);
                //        if (query.Any())
                //        {
                //            Utility.GotoNewRowJanus(grd_List, DmucBenh.Columns.MaBenh, query.FirstOrDefault());
                //        }
                //    Janus.Windows.GridEX.GridEXColumn gridExColumn =
                //        grd_List.RootTable.Columns[DmucBenh.Columns.TenBenh];
                //    Janus.Windows.GridEX.GridEXFilterCondition gridExFilterCondition = new GridEXFilterCondition(gridExColumn, ConditionOperator.Contains, mabenh);
                //    grd_List.Find(gridExFilterCondition,1,-1);

                //}
            //}
               
        }

        private void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grd_List.DataSource = globalVariables.gv_dtDmucBenh;
        }

    }
}
