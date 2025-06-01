using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;

namespace VNS.HIS.UCs
{
    public partial class ucGrid : UserControl
    {
        public delegate void OnKeyDown(string ID,int id_thuockho,string _name, string ngayhethan);
        public event OnKeyDown _OnKeyDown;

        public delegate void OnSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc);
        public event OnSelectionChanged _OnSelectionChanged;

        public delegate void OnCellValueChanged(string _name, int id_thuockho, string dongia, string donvi);
        public event OnCellValueChanged _OnCellValueChanged;
        public delegate void OnPreviewKeyDown();
        public event OnPreviewKeyDown _OnPreviewKeyDown;
        public bool AllowedChanged = false;
        public int RowIndex = -1;
        public ucGrid()
        {
            InitializeComponent();
            Utility.SetVisualStyleBasic(this);
            grdListDrug.CellValueChanged += new ColumnActionEventHandler(grdListDrug_CellValueChanged);
            grdListDrug.KeyDown += new KeyEventHandler(grdListDrug_KeyDown);
            grdListDrug.LostFocus += new EventHandler(grdListDrug_LostFocus);
            grdListDrug.PreviewKeyDown += new PreviewKeyDownEventHandler(grdListDrug_PreviewKeyDown);
            grdListDrug.SelectionChanged += new EventHandler(grdListDrug_SelectionChanged);
            
            //grdListDrug.MouseDoubleClick += new MouseEventHandler(grdListDrug_MouseDoubleClick);
            grdListDrug.Click += new EventHandler(grdListDrug_Click);
            //grdListDrug.DoubleClick += new EventHandler(grdListDrug_DoubleClick);
        }
        public void HideColums(bool AllowedSelectPrice)
        {
            grdListDrug.RootTable.Columns["GIA_BAN"].Visible = AllowedSelectPrice;
            grdListDrug.RootTable.Columns["PHU_THU"].Visible = AllowedSelectPrice;

        }
        void grdListDrug_DoubleClick(object sender, EventArgs e)
        {
            grdListDrug_KeyDown(grdListDrug, new KeyEventArgs(Keys.Enter));
        }

        void grdListDrug_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdListDrug)) return;
            grdListDrug_KeyDown(grdListDrug, new KeyEventArgs(Keys.Enter));
        }

        void grdListDrug_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdListDrug_KeyDown(grdListDrug, new KeyEventArgs(Keys.Enter));
        }
        private void grdListDrug_LostFocus(object sender, EventArgs e)
        {
           // if (!txtDrug_Code.Focused) grdListDrug.Visible = false;
        }
        private void grdListDrug_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdListDrug))
                {
                    RowIndex = 1;
                    _OnKeyDown(grdListDrug.CurrentRow.Cells["id_thuoc"].Value.ToString(),Utility.Int32Dbnull( grdListDrug.CurrentRow.Cells["id_thuockho"].Value,-1), grdListDrug.GetValue("ten_thuoc").ToString(), "");// grdListDrug.CurrentRow.Cells["NGAY_HETHAN"].Value.ToString());

                    //grdListDrug.Visible = false;
                    //TudongloadsotayBS();
                    //txtQuantity.Focus();
                    //txtQuantity.SelectAll();
                }
                // if(grdListDrug.rowf)
                if (e.KeyCode == Keys.Escape) grdListDrug.Visible = false;
            }
            catch(Exception ex)
            {
                Utility.CatchException("_KeyDown Error", ex);
            }
        }

        private void grdListDrug_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void grdListDrug_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowedChanged) return;
                if (!Utility.isValidGrid(grdListDrug))
                {
                    RowIndex = -1;
                    _OnSelectionChanged("",-1, "", "", "", 0);
                }
                else
                {
                    RowIndex = 1;
                        _OnSelectionChanged(Utility.sDbnull(grdListDrug.GetValue("id_thuoc"), "-1"), Utility.Int32Dbnull(grdListDrug.CurrentRow.Cells["id_thuockho"].Value, -1),
                        Utility.sDbnull(grdListDrug.GetValue("ten_thuoc"), "0"),
                        Utility.sDbnull(grdListDrug.GetValue("GIA_BAN"), "0"),
                        Utility.sDbnull(grdListDrug.GetValue("phu_thu"), "0"),
                        Utility.Int32Dbnull(grdListDrug.GetValue("tu_tuc"), 0)
                        );
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException("_SelectionChanged Error", ex);
            }
        }

        private void grdListDrug_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                if (!AllowedChanged) return;
                if (Utility.isValidGrid(grdListDrug))
                {
                    RowIndex = 1;
                    _OnCellValueChanged(Utility.sDbnull(grdListDrug.GetValue("ten_thuoc"), ""), Utility.Int32Dbnull(grdListDrug.CurrentRow.Cells["id_thuockho"].Value, -1), Utility.sDbnull(grdListDrug.GetValue("gia_ban"), "0"), Utility.sDbnull(grdListDrug.GetValue("ten_donvitinh"), "0"));
                 
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("_CellValueChanged Error", ex);
            }
        }
    }
}
