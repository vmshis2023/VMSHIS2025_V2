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
    public partial class uc_grdCongkham : UserControl
    {
        public delegate void OnKeyDown(int LOAI,int id_congkham, List<int> lstSelectedID, List<int> lstLoai , string ma_congkham, string ten_congkham,string ten_phongkham);
        public event OnKeyDown _OnKeyDown;

        public delegate void OnSelectionChanged(int LOAI, int id_congkham, List<int> lstSelectedID, List<int> lstLoai, string ma_congkham, string ten_congkham, string ten_phongkham);
        public event OnSelectionChanged _OnSelectionChanged;

        public delegate void OnCellValueChanged(int LOAI,int id_congkham, List<int> lstSelectedID, List<int> lstLoai, string ma_congkham, string ten_congkham, string ten_phongkham);
        public event OnCellValueChanged _OnCellValueChanged;
        public delegate void OnPreviewKeyDown();
        public event OnPreviewKeyDown _OnPreviewKeyDown;
        public bool AllowedChanged = false;
        public int RowIndex = -1;
        public uc_grdCongkham()
        {
            InitializeComponent();
            Utility.SetVisualStyleBasic(this);
            grdList.CellValueChanged += new ColumnActionEventHandler(grdICD_CellValueChanged);
            grdList.KeyDown += new KeyEventHandler(grdICD_KeyDown);
            grdList.LostFocus += new EventHandler(grdICD_LostFocus);
            grdList.PreviewKeyDown += new PreviewKeyDownEventHandler(grdICD_PreviewKeyDown);
            grdList.SelectionChanged += new EventHandler(grdICD_SelectionChanged);
            
            //grdICD.MouseDoubleClick += new MouseEventHandler(grdICD_MouseDoubleClick);
            grdList.Click += new EventHandler(grdICD_Click);
            //grdICD.DoubleClick += new EventHandler(grdICD_DoubleClick);
        }
        public void HideColums(bool AllowedSelectPrice)
        {
           

        }
        void grdICD_DoubleClick(object sender, EventArgs e)
        {
            grdICD_KeyDown(grdList, new KeyEventArgs(Keys.Enter));
        }

        void grdICD_Click(object sender, EventArgs e)
        {
           
            if (!Utility.isValidGrid(grdList) || (grdList.CurrentColumn!=null && grdList.CurrentColumn.Key== "CHON")) return;
           
            grdICD_KeyDown(grdList, new KeyEventArgs(Keys.Enter));
        }

        void grdICD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdICD_KeyDown(grdList, new KeyEventArgs(Keys.Enter));
        }
        private void grdICD_LostFocus(object sender, EventArgs e)
        {
           // if (!txtDrug_Code.Focused) grdICD.Visible = false;
        }
        private void grdICD_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               
                if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdList) && grdList.Visible)
                {
                    List<int> lstSelectedID = new List<int>();
                    List<int> lstLoai = new List<int>();
                    
                    if (grdList.GetCheckedRows().Count() <= 0)
                    {
                        lstSelectedID = new List<int>() { Utility.Int32Dbnull(grdList.CurrentRow.Cells["id_dichvukcb"].Value) };
                        lstLoai = new List<int>() { Utility.Int32Dbnull(grdList.CurrentRow.Cells["Loai"].Value) };
                    }
                    else
                    {
                        lstSelectedID = grdList.GetCheckedRows().Select(c => Utility.Int32Dbnull(c.Cells["id_dichvukcb"].Value)).ToList<int>();
                       lstLoai = grdList.GetCheckedRows().Select(c => Utility.Int32Dbnull(c.Cells["Loai"].Value)).ToList<int>();
                    }
                   
                    RowIndex = 1;
                    _OnKeyDown(Utility.Int32Dbnull(grdList.CurrentRow.Cells["loai"].Value, ""), Utility.Int32Dbnull( grdList.CurrentRow.Cells["id_dichvukcb"].Value.ToString()), lstSelectedID, lstLoai,grdList.CurrentRow.Cells["ma_dichvukcb"].Value.ToString(), Utility.sDbnull(grdList.CurrentRow.Cells["ten_dichvukcb"].Value, ""), Utility.sDbnull(grdList.CurrentRow.Cells["ten_phongkham"].Value, ""));
                }
                if (e.KeyCode == Keys.Escape) grdList.Visible = false;
            }
            catch(Exception ex)
            {
                Utility.CatchException("_KeyDown Error", ex);
            }
        }

        private void grdICD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void grdICD_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowedChanged) return;
                if (!Utility.isValidGrid(grdList))
                {
                    RowIndex = -1;
                    _OnSelectionChanged(-1,-1,new List<int>(),new List<int>(), "","","");
                }
                else
                {
                    RowIndex = 1;
                    _OnSelectionChanged(Utility.Int32Dbnull(grdList.CurrentRow.Cells["loai"].Value, ""), Utility.Int32Dbnull(grdList.CurrentRow.Cells["id_dichvukcb"].Value.ToString()), new List<int>(), new List<int>(), grdList.CurrentRow.Cells["ma_dichvukcb"].Value.ToString(), Utility.sDbnull(grdList.CurrentRow.Cells["ten_dichvukcb"].Value, ""), Utility.sDbnull(grdList.CurrentRow.Cells["ten_phongkham"].Value, ""));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("_SelectionChanged Error", ex);
            }
        }

        private void grdICD_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                //if (!AllowedChanged) return;
                //if (Utility.isValidGrid(grdList))
                //{
                //    RowIndex = 1;
                //    _OnCellValueChanged(Utility.Int32Dbnull(grdList.CurrentRow.Cells["loai"].Value, ""), Utility.Int32Dbnull(grdList.CurrentRow.Cells["id_dichvukcb"].Value.ToString()), grdList.CurrentRow.Cells["ma_dichvukcb"].Value.ToString(), Utility.sDbnull(grdList.CurrentRow.Cells["ten_dichvukcb"].Value, ""), Utility.sDbnull(grdList.CurrentRow.Cells["ten_phongkham"].Value, ""));

                //}
            }
            catch (Exception ex)
            {
                Utility.CatchException("_CellValueChanged Error", ex);
            }
        }
    }
}
