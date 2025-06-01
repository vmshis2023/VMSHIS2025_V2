using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.UI.Forms.Dungchung.UCs;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.Forms.Noitru
{
    public partial class frm_chonbacsidieutri : Form
    {
        public delegate void OnAccept(object ID);
        public event OnAccept _OnAccept;
        DataTable dtBacsinoitru = new DataTable();
        KcbLuotkham objLuotkham = null;
        public frm_chonbacsidieutri(int idkhoadieutri, KcbLuotkham objLuotkham)
        {
            InitializeComponent();
            this.objLuotkham = objLuotkham;
            dtBacsinoitru = THU_VIEN_CHUNG.LaydanhsachBacsi(idkhoadieutri, 1);
            txtBacsi.Init(dtBacsinoitru, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtBacsi._OnSelectionChanged += txtBacsi__OnSelectionChanged;
            txtBacsi._OnEnterMe += txtBacsi__OnEnterMe;
            this.Load += frm_chonbacsidieutri_Load;
        }

        void frm_chonbacsidieutri_Load(object sender, EventArgs e)
        {
            NapdanhsachBsidieutri();
            if (objLuotkham != null && objLuotkham.IdBsDieutrinoitruChinh > -1)
            {
                txtBacsi.SetId(objLuotkham.IdBsDieutrinoitruChinh);
                txtBacsi.RaiseEnterEvents();
            }
        }

        void txtBacsi__OnEnterMe()
        {
            SelectControl(txtBacsi.MyID.ToString());
        }

        void txtBacsi__OnSelectionChanged()
        {
            SelectControl(txtBacsi.MyID.ToString());
        }
        void SelectControl(string ID)
        {
            try
            {
                foreach (Control ctrl in pnlBsi.Controls)
                {
                    ucBacsinoitru _item = ctrl as ucBacsinoitru;
                    if (_item.drData[DmucNhanvien.Columns.IdNhanvien].ToString() == ID)
                        _item.SetSelected(true);
                    else
                        _item.SetSelected(false);

                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {


            }
        }
        void HoverControl(string ID)
        {
            try
            {
                foreach (Control ctrl in pnlBsi.Controls)
                {
                    ucBacsinoitru _item = ctrl as ucBacsinoitru;
                    if (_item.drData[DmucNhanvien.Columns.IdNhanvien].ToString() == ID)
                        _item.SetHover(true);
                    else
                        _item.SetHover(false);

                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                
               
            }
        }
        ucBacsinoitru getSelectedItem()
        {
            foreach (Control ctrl in pnlBsi.Controls)
            {
                ucBacsinoitru _item = ctrl as ucBacsinoitru;
                if (_item.isPressed)
                    return _item;

            }
            return null;
        }
        void NapdanhsachBsidieutri()
        {
            pnlBsi.Controls.Clear();
            foreach (DataRow dr in dtBacsinoitru.Rows)
            {
                ucBacsinoitru _item = new ucBacsinoitru(dr);
                _item._OnClick += _item__OnClick;
                _item._OnSelectme += _item__OnSelectme;
                AddControl(pnlBsi, _item);
            }

        }

        void _item__OnSelectme(ucBacsinoitru obj)
        {
            foreach (Control ctrl in pnlBsi.Controls)
            {
                ucBacsinoitru _item = ctrl as ucBacsinoitru;
                if (_item.ID == obj.ID)
                    _item.SetSelected(true);
                else
                    _item.SetSelected(false);

            }
            cmdAccept.PerformClick();
        }

        void _item__OnClick(ucBacsinoitru obj)
        {
            foreach (Control ctrl in pnlBsi.Controls)
            {
                ucBacsinoitru _item = ctrl as ucBacsinoitru;
                if (_item.ID == obj.ID)
                    _item.SetSelected(true);
                else
                    _item.SetSelected(false);

            }
        }
        delegate void _AddControl(Control _parent, Control _child);
        public static void AddControl(Control _parent, Control _child)
        {
            try
            {
                if (_parent.InvokeRequired)
                    _parent.Invoke(new _AddControl(AddControl), new object[] { _parent, _child });
                else
                    _parent.Controls.Add(_child);

            }
            catch
            {
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            ucBacsinoitru _item = getSelectedItem();
            string ID = "-1";
            if (_item != null) ID = _item.ID;
            if (_OnAccept != null) _OnAccept(ID);
            this.Close();
        }
    }
}
