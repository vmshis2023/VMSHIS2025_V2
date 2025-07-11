// Copyright © Serge Weinstock 2014.
//
// This library is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace VNS.HIS.UCs
{
    /// <summary>
    /// This is a combobox with a suggestion list à la "Sublime Text"
    /// 
    /// Searches are made against the pattern in the combo textbox by matching
    /// all the characters in the pattern in the right order but not consecutively
    /// </summary>
    public class EasyCompletionComboBox_v2 : ComboBox
    {
        private DataView _view;

        public EasyCompletionComboBox_v2()
        {
            DropDownStyle = ComboBoxStyle.DropDown;
            AutoCompleteMode = AutoCompleteMode.None;
            AutoCompleteSource = AutoCompleteSource.None;

            this.BindingContext = new BindingContext();  // mỗi control riêng biệt

            this.KeyUp += EasyCompletionComboBox_KeyUp;
        }

        /// <summary>
        /// Gán DataTable và thiết lập hiển thị
        /// </summary>
        public void SetDataSource(DataTable dt, string valueMember, string displayMember)
        {
            _view = new DataView(dt);
            this.DataSource = _view;
            this.ValueMember = valueMember;
            this.DisplayMember = displayMember;
        }

        private void EasyCompletionComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (_view == null) return;

            string text = this.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(text))
            {
                _view.RowFilter = "";
                this.DroppedDown = false;
            }
            else
            {
                _view.RowFilter = $"{DisplayMember} LIKE '%{text}%'";
                this.DroppedDown = _view.Count > 0;
            }

            this.SelectionStart = this.Text.Length;
            this.SelectionLength = 0;
        }
    }


}