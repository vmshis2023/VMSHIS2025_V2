using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Janus.Windows.GridEX;

namespace VietBaIT.HandyTools
{
    public class JanusUtility
    {
        public static void GridExCommonSettings(GridEX gridEx, DataTable dataTable)
        {
            gridEx.DataSource = dataTable;
            gridEx.GroupByBoxVisible = false;
            gridEx.RecordNavigator = true;
            gridEx.RowHeaders = InheritableBoolean.True;
            gridEx.HideSelection = HideSelection.HighlightInactive;
            gridEx.SelectedInactiveFormatStyle.BackColor = Color.FromKnownColor(KnownColor.Highlight);
            gridEx.FilterMode = FilterMode.Automatic;
            gridEx.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            gridEx.FilterRowFormatStyle.BackColor = Color.FromArgb(255, 255, 192);
            foreach (GridEXColumn column in gridEx.RootTable.Columns)
            {
                column.FilterRowComparison = ConditionOperator.Contains;
            }
        }
    }
}
