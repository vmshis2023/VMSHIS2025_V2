using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using VNS.HIS.UI.Forms.Dungchung.UCs;
using VNS.HIS.UI.Forms.Noitru;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_tracuu_buonggiuong : Form
    {
        private bool bHasloaded;
        private DataTable m_dtGiuong = new DataTable();
        private DataTable m_dtKhoa = new DataTable();
        private DataTable m_dtBuong = new DataTable();
        int id_khoa = -1;
        int id_buong = -1;
        int id_giuong = -1;
        ucBuong _Buong;
        ucKhoa _Khoa;
        ucGiuong _Giuong;
        public frm_tracuu_buonggiuong()
        {
            InitializeComponent();
            LoadUserConfigs();
            InitEvents();
        }
        void InitEvents()
        {
            FormClosing += frm_tracuu_buonggiuong_FormClosing;
            autokhoa1._OnEnterMe += autokhoa1__OnEnterMe;
            autoBuong1._OnEnterMe += autoBuong1__OnEnterMe;
            autoBuong1._OnSelectionChanged += autoBuong1__OnSelectionChanged;
            autokhoa1._OnSelectionChanged += autokhoa1__OnSelectionChanged;
            autoGiuong1._OnEnterMe += autoGiuong1__OnEnterMe;
            autoGiuong1._OnSelectionChanged += autoGiuong1__OnSelectionChanged;
        }

        void autoGiuong1__OnSelectionChanged()
        {
            SelectGiuong(autoGiuong1.MyID);
        }
        void ShowHistory()
        {
            frm_lichsubuonggiuong _history = new frm_lichsubuonggiuong(id_khoa,id_buong,id_giuong,true);
            _history.ShowDialog();
            autoGiuong1.Focus();
            autoGiuong1.SelectAll();
        }
        void autoGiuong1__OnEnterMe()
        {
            SelectGiuong(autoGiuong1.MyID);
            if (_Giuong != null) _giuong__OnClick(_Giuong);
            //id_giuong = Utility.Int32Dbnull(autoGiuong1.MyID, -1);
            //if (id_giuong == -1) _Giuong = null;
            //autoGiuong1.Focus();
            //autoGiuong1.SelectAll();
            //buildText();
            //ShowHistory();

        }

        void autoBuong1__OnSelectionChanged()
        {
            SelectBuong(autoBuong1.MyID);
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
        void autokhoa1__OnSelectionChanged()
        {
            SelectKhoa(autokhoa1.MyID);
        }
        void autokhoa1__OnEnterMe()
        {
            //autokhoa.SetId(autokhoa1.MyID);
            SelectKhoa(autokhoa1.MyID);
            if (_Khoa != null) _khoa__OnClick(_Khoa);
        }
        
        void SelectKhoa(object id_khoa)
        {
            try
            {
                foreach (Control ctrl in flowKhoa.Controls)
                {
                    ucKhoa _item = ctrl as ucKhoa;
                    if (_item.id_khoa.ToString() == id_khoa.ToString())
                    {
                        _Khoa = _item;
                        _item.SetSelected(true);
                    }
                    else
                        _item.SetSelected(false);

                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {


            }
        }
        void SelectBuong(object id_buong)
        {
            try
            {
                foreach (Control ctrl in flowBuong.Controls)
                {
                    ucBuong _item = ctrl as ucBuong;
                    if (_item.id_buong.ToString()== id_buong.ToString())
                    {
                        _Buong = _item;
                        _item.SetSelected(true);
                    }
                    else
                        _item.SetSelected(false);

                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {


            }
        }
        void SelectGiuong(object id_giuong)
        {
            try
            {
                foreach (Control ctrl in flowGiuong.Controls)
                {
                    ucGiuong _item = ctrl as ucGiuong;
                    if (_item.id_giuong.ToString() == id_giuong.ToString())
                    {
                        _Giuong = _item;
                        _item.SetSelected(true);
                    }
                    else
                        _item.SetSelected(false);
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {


            }
        }

        void LoadUserConfigs()
        {
            try
            {
                chkGrid.Checked = Utility.getUserConfigValue(chkGrid.Tag.ToString(), Utility.Bool2byte(chkGrid.Checked)) == 1;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkGrid.Tag.ToString(), Utility.Bool2byte(chkGrid.Checked));

            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void frm_tracuu_buonggiuong_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void autoBuong1__OnEnterMe()
        {
            SelectBuong(id_buong);
            if (_Buong != null) _buong__OnClick(_Buong);
            
        }

        void frm_tracuu_buonggiuong_Load(object sender, EventArgs e)
        {
            m_dtKhoa = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autokhoa1.Init(m_dtKhoa, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            CreateKhoa();
            pnlTimkiemKhoa.BringToFront();
            pnlKhoa.BringToFront();
            autokhoa1.Focus();
        }

        void CreateKhoa()
        {
            flowKhoa.Controls.Clear();
            flowKhoa.SuspendLayout();
            foreach (DataRow drKhoa in m_dtKhoa.Rows)
            {
                ucKhoa _khoa = new ucKhoa(drKhoa);
                _khoa._OnClick += _khoa__OnClick;
                _khoa._OnSelectme += _khoa__OnSelectme;
                AddControl(flowKhoa, _khoa);
            }
            flowKhoa.ResumeLayout();
        }

        void _khoa__OnSelectme(ucKhoa obj)
        {
            foreach (Control ctrl in flowKhoa.Controls)
            {
                ucKhoa _item = ctrl as ucKhoa;
                if (_item.id_khoa == obj.id_khoa)
                    _item.SetSelected(true);
                else
                    _item.SetSelected(false);

            }
        }
        bool reload = true;
        void _khoa__OnClick(ucKhoa obj)
        {
            _Khoa = obj;
            if (id_khoa == obj.id_khoa) reload=false;
            id_khoa = Utility.Int32Dbnull(obj.id_khoa, -1);
            LoadBuong();

            _khoa__OnSelectme(obj);
            autoBuong1.Focus();
            autoBuong1.SelectAll();
            if (flowBuong.Controls.Count <= 0)
            {
                _Buong = null;
                _Giuong = null;
            }
            buildText();
        }

        private void LoadBuong()
        {
            Utility.WaitNow(this);
            try
            {
                pnlBuong.BringToFront();
                pnlTimkiemBuong.BringToFront();
               
                if (reload)
                {
                    //flowGiuong.Controls.Clear();
                    //flowBuong.Controls.Clear();
                    m_dtBuong = SPs.NoitruTimkiembuongTheokhoa(id_khoa).GetDataSet().Tables[0];
                    autoBuong1.Init(m_dtBuong, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });

                    CreateBuong();
                }
                reload = true;
            }
            catch (Exception ex)
            {


            }
            finally
            {
                Utility.DefaultNow(this);
                bHasloaded = true;
            }
        }
        void CreateBuong()
        {
            flowBuong.SuspendLayout();
            pnlBuong.BringToFront();
            flowBuong.Controls.Clear();
            foreach (DataRow drBuong in m_dtBuong.Rows)
            {
                ucBuong _buong = new ucBuong(drBuong);
                _buong._OnClick += _buong__OnClick;
                _buong._OnSelectme += _buong__OnSelectme;
                AddControl(flowBuong, _buong);
            }
            flowBuong.ResumeLayout();
        }

        void _buong__OnSelectme(ucBuong obj)
        {
            foreach (Control ctrl in flowBuong.Controls)
            {
                ucBuong _item = ctrl as ucBuong;
                if (_item.id_buong == obj.id_buong)
                    _item.SetSelected(true);
                else
                    _item.SetSelected(false);

            }
        }
        void buildText()
        {
            lblGuide.Text = String.Format(@"Bạn đang ở: {0}\{1}\{2}", _Khoa != null ? _Khoa.lblName.Text : "", _Buong != null ? _Buong.lblName.Text : "", _Giuong != null ? _Giuong.lblName.Text : "");
        }
        void _buong__OnClick(ucBuong obj)
        {
            _Buong = obj;
            if (id_buong == obj.id_buong) reload=true;
            id_buong = obj.id_buong;
            _buong__OnSelectme(obj);
            LoadGiuong();
            autoGiuong1.Focus();
            autoGiuong1.SelectAll();
            if (flowGiuong.Controls.Count <= 0)
            {
                _Giuong = null;
            }
            buildText();
        }
        private void LoadGiuong()
        {
            bHasloaded = false;
            Utility.WaitNow(this);
            try
            {
                pnlGiuong.BringToFront();
                pnlTimkiemGiuong.BringToFront();
                if (reload)
                {
                    m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(id_khoa, id_buong, 1);
                    autoGiuong1.Init(m_dtGiuong, new List<string>() { NoitruDmucGiuongbenh.Columns.IdGiuong, NoitruDmucGiuongbenh.Columns.MaGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong });
                    CreateGiuong();
                }
                reload = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Utility.DefaultNow(this);
                bHasloaded = true;
              
            }

        }
        void CreateGiuong()
        {
            flowGiuong.SuspendLayout();
            flowGiuong.Controls.Clear();
            foreach (DataRow drGiuong in m_dtGiuong.Rows)
            {
                ucGiuong _giuong = new ucGiuong(drGiuong);
                _giuong._OnClick += _giuong__OnClick;
                _giuong._OnSelectme += _giuong__OnSelectme;
                AddControl(flowGiuong, _giuong);
            }
            pnlGiuong.BringToFront();
            flowGiuong.ResumeLayout();
        }

        void _giuong__OnSelectme(ucGiuong obj)
        {
            foreach (Control ctrl in flowGiuong.Controls)
            {
                ucGiuong _item = ctrl as ucGiuong;
                if (_item.id_giuong == obj.id_giuong)
                    _item.SetSelected(true);
                else
                    _item.SetSelected(false);

            }
        }

        void _giuong__OnClick(ucGiuong obj)
        {
            id_giuong = obj.id_giuong;
            _Giuong = obj;
            buildText();
            ShowHistory();
            

        }
        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool isGrid = true;
        private void frm_tracuu_buonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            else if (e.Control && e.KeyCode == Keys.D1)
            {
                pnlKhoa.BringToFront();
                pnlTimkiemKhoa.BringToFront();
            }
            else if (e.Control && e.KeyCode == Keys.D2)
            {
                pnlBuong.BringToFront();
                pnlTimkiemBuong.BringToFront();
            }
        }
        int step = 0;
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            step = 1;
            _Giuong = null;
            pnlBuong.BringToFront();
            pnlTimkiemBuong.BringToFront();
            buildText();
        }

        private void cmdBackKhoa_Click(object sender, EventArgs e)
        {
            step = 0;
            _Buong = null;
            _Giuong = null;
            pnlKhoa.BringToFront();
            pnlTimkiemKhoa.BringToFront();
            buildText();
        }
    }
}