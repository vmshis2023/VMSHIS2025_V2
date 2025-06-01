using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Janus.Windows.EditControls;

namespace VNS.HandyTools
{
    public class DataBinding
    {
        public static string ConfigPath = Application.StartupPath + "\\AppConfig\\";

        static DataBinding()
        {
            if (!System.IO.Directory.Exists(ConfigPath))
                System.IO.Directory.CreateDirectory(ConfigPath);
        }

        public static void BindDataCombobox(UIComboBox comboBox, DataTable dataTable, string valueColumnName, string displayColumnName)
        {
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayColumnName;
            comboBox.ValueMember = valueColumnName;
            dataTable.AcceptChanges();
            if (dataTable.Rows.Count > 0) comboBox.SelectedIndex = 0;
        }

        public static void BindDataCombobox(UIComboBox comboBox, DataTable dataTable, string valueColumnName, string displayColumnName, string defaultValue)
        {
            DataRow dr = dataTable.NewRow();
            dr[displayColumnName] = defaultValue;
            dataTable.Rows.InsertAt(dr,0);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayColumnName;
            comboBox.ValueMember = valueColumnName;
            dataTable.AcceptChanges();
            if (dataTable.Rows.Count > 0) comboBox.SelectedIndex = 0;
        }

        public static void BindDataCombobox(ComboBox comboBox, DataTable dataTable, string valueColumnName, string displayColumnName)
        {
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayColumnName;
            comboBox.ValueMember = valueColumnName;
            dataTable.AcceptChanges();
            if (dataTable.Rows.Count > 0) comboBox.SelectedIndex = 0;
        }

        public static void BindDataCombobox(ComboBox comboBox, DataTable dataTable, string valueColumnName, string displayColumnName, string defaultValue)
        {
            DataRow dr = dataTable.NewRow();
            dr[displayColumnName] = defaultValue;
            dataTable.Rows.InsertAt(dr, 0);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayColumnName;
            comboBox.ValueMember = valueColumnName;
            dataTable.AcceptChanges();
            if (dataTable.Rows.Count > 0) comboBox.SelectedIndex = 0;
        }
    }
}
