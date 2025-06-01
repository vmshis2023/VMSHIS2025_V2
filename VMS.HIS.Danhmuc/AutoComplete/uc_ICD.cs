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
    public partial class uc_ICD : UserControl
    {
        public delegate void OnKeyDown(Int16 id_benh, string ma_benh, string ten_benh);
        public event OnKeyDown _OnKeyDown;

        public delegate void OnSelectionChanged(Int16 id_benh, string ma_benh,  string ten_benh);
        public event OnSelectionChanged _OnSelectionChanged;

        public delegate void OnCellValueChanged(Int16 id_benh, string ma_benh, string ten_benh);
        public event OnCellValueChanged _OnCellValueChanged;
        public delegate void OnPreviewKeyDown();
        public event OnPreviewKeyDown _OnPreviewKeyDown;
        public bool AllowedChanged = false;
        public int RowIndex = -1;
        public uc_ICD()
        {
            InitializeComponent();
            Utility.SetVisualStyleBasic(this);
            grdICD.CellValueChanged += new ColumnActionEventHandler(grdICD_CellValueChanged);
            grdICD.KeyDown += new KeyEventHandler(grdICD_KeyDown);
            grdICD.LostFocus += new EventHandler(grdICD_LostFocus);
            grdICD.PreviewKeyDown += new PreviewKeyDownEventHandler(grdICD_PreviewKeyDown);
            grdICD.SelectionChanged += new EventHandler(grdICD_SelectionChanged);
            
            //grdICD.MouseDoubleClick += new MouseEventHandler(grdICD_MouseDoubleClick);
            grdICD.Click += new EventHandler(grdICD_Click);
            //grdICD.DoubleClick += new EventHandler(grdICD_DoubleClick);
        }
        public void HideColums(bool AllowedSelectPrice)
        {
           

        }
        void grdICD_DoubleClick(object sender, EventArgs e)
        {
            grdICD_KeyDown(grdICD, new KeyEventArgs(Keys.Enter));
        }

        void grdICD_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdICD)) return;
            grdICD_KeyDown(grdICD, new KeyEventArgs(Keys.Enter));
        }

        void grdICD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdICD_KeyDown(grdICD, new KeyEventArgs(Keys.Enter));
        }
        private void grdICD_LostFocus(object sender, EventArgs e)
        {
           // if (!txtDrug_Code.Focused) grdICD.Visible = false;
        }
        private void grdICD_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdICD) && grdICD.Visible)
                {
                    RowIndex = 1;
                    _OnKeyDown(Utility.Int16Dbnull( grdICD.CurrentRow.Cells["id_benh"].Value.ToString()), grdICD.CurrentRow.Cells["ma_benh"].Value.ToString(), Utility.sDbnull(grdICD.CurrentRow.Cells["ten_benh"].Value, ""));
                }
                if (e.KeyCode == Keys.Escape) grdICD.Visible = false;
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
                if (!Utility.isValidGrid(grdICD))
                {
                    RowIndex = -1;
                    _OnSelectionChanged(-1,"","");
                }
                else
                {
                    RowIndex = 1;
                    _OnSelectionChanged(Utility.Int16Dbnull(grdICD.CurrentRow.Cells["id_benh"].Value.ToString()), grdICD.CurrentRow.Cells["ma_benh"].Value.ToString(), Utility.sDbnull(grdICD.CurrentRow.Cells["ten_benh"].Value, ""));
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
                if (!AllowedChanged) return;
                if (Utility.isValidGrid(grdICD))
                {
                    RowIndex = 1;
                    _OnCellValueChanged(Utility.Int16Dbnull(grdICD.CurrentRow.Cells["id_benh"].Value.ToString()), grdICD.CurrentRow.Cells["ma_benh"].Value.ToString(), Utility.sDbnull(grdICD.CurrentRow.Cells["ten_benh"].Value, ""));

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("_CellValueChanged Error", ex);
            }
        }
    }
}
