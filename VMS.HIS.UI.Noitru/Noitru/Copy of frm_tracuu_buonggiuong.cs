using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VNS.HIS.DAL;
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
            Utility.VisiableGridEx(grdBed, NoitruDmucGiuongbenh.Columns.IdGiuong, globalVariables.IsAdmin);
        }
        void InitEvents()
        {
            FormClosing += frm_tracuu_buonggiuong_FormClosing;
            this.grdBed.SelectionChanged += this.grdBed_SelectionChanged;
            autokhoa._OnEnterMe += autokhoa__OnEnterMe;
            autoBuong._OnEnterMe += autoBuong__OnEnterMe;

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
            frm_lichsubuonggiuong _history = new frm_lichsubuonggiuong();
            _history.ShowDialog();
            autoGiuong1.Focus();
            autoGiuong1.SelectAll();
        }
        void autoGiuong1__OnEnterMe()
        {
            id_giuong = Utility.Int32Dbnull(autoGiuong1.MyID, -1);
            if (id_giuong == -1) _Giuong = null;
            autoGiuong1.Focus();
            autoGiuong1.SelectAll();
            buildText();
            ShowHistory();

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
            id_khoa = Utility.Int32Dbnull(autokhoa1.MyID, -1);
            SelectKhoa(id_khoa);
            if (id_khoa == -1) _Khoa = null;
            VisibleItem(pnlTimkiemKhoa);
            LoadBuong();
        }
        void VisibleItem(Control ctrl)
        {
            foreach (Control ctr in pnlSearch.Controls)
                if (ctr.Name == ctrl.Name)
                {
                    ctr.Visible = true;
                    ctr.BringToFront();
                }
                else
                    ctrl.Visible = false;
        }
        void VisibleFlowItem(Control ctrl)
        {
            foreach (Control ctr in pnlDohoa.Controls)
                if (ctr.Name == ctrl.Name)
                {
                    ctr.Visible = true;
                    ctr.BringToFront();
                }
                else if (ctr.Name != pnlNavigation.Name)
                    ctrl.Visible = false;



        }
        void SelectKhoa(object id_khoa)
        {
            try
            {
                foreach (Control ctrl in flowKhoa.Controls)
                {
                    ucKhoa _item = ctrl as ucKhoa;
                    if (_item.drData[DmucKhoaphong.Columns.IdKhoaphong].ToString() == id_khoa.ToString())
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
                    if (_item.drData[NoitruDmucBuong.Columns.IdBuong].ToString() == id_buong.ToString())
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
                    if (_item.drData[NoitruDmucGiuongbenh.Columns.IdGiuong].ToString() == id_giuong.ToString())
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
            id_buong = Utility.Int32Dbnull(autoBuong1.MyID, -1);
            if (id_buong == -1) _Buong = null;
            //autoBuong.SetId(autoBuong1.MyID);
            LoadGiuong();
            
        }

        

        void autoBuong__OnEnterMe()
        {
            autoBuong1.SetId(autoBuong.MyID);
            LoadGiuong();
        }

        void autokhoa__OnEnterMe()
        {
            autokhoa1.SetId(autokhoa.MyID);
            LoadBuong();
        }

        void frm_tracuu_buonggiuong_Load(object sender, EventArgs e)
        {
            m_dtKhoa = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autokhoa.Init(m_dtKhoa, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            autokhoa1.Init(autokhoa.AutoCompleteSource, autokhoa.defaultItem);
            CreateKhoa();
            VisibleItem(pnlTimkiemKhoa);
            VisibleFlowItem(pnlKhoa);
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

        void _khoa__OnClick(ucKhoa obj)
        {
            _Khoa = obj;
            if (id_khoa == obj.id_khoa) return;
            id_khoa = Utility.Int32Dbnull(obj.id_khoa, -1);
            LoadBuong();
            VisibleItem(pnlTimkiemBuong);
            VisibleFlowItem(pnlBuong);
            _khoa__OnSelectme(obj);
            autoBuong1.Focus();
            autoBuong1.SelectAll();
            buildText();
        }

        private void LoadData()
        {
            bHasloaded = false;
            LoadBuong();
            pnlBuong.BringToFront();
            DataTable dtLoaiGiuong = SPs.DmucLaydanhmucGiabuonggiuong("-1").GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdGiaGiuong, dtLoaiGiuong, true, true, "1=1", NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia);
            bHasloaded = true;
        }

        private void LoadBuong()
        {
            Utility.WaitNow(this);
            try
            {
                pnlBuong.BringToFront();
                flowGiuong.Controls.Clear();
                flowBuong.Controls.Clear();
                m_dtBuong = SPs.NoitruTimkiembuongTheokhoa(id_khoa).GetDataSet().Tables[0];
                autoBuong.Init(m_dtBuong, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
                autoBuong1.Init(autoBuong.AutoCompleteSource, autoBuong.defaultItem);
                CreateBuong();
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
            pnlBuong.BringToFront();
            flowBuong.Controls.Clear();
            foreach (DataRow drBuong in m_dtBuong.Rows)
            {
                ucBuong _buong = new ucBuong(drBuong);
                _buong._OnClick += _buong__OnClick;
                _buong._OnSelectme += _buong__OnSelectme;
                AddControl(flowBuong, _buong);
            }
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
            if (id_buong == obj.id_buong) return;
            LoadGiuong();
           
            VisibleItem(pnlTimkiemGiuong);
            VisibleFlowItem(pnlGiuong);
            autoGiuong1.Focus();
            autoGiuong1.SelectAll();
            buildText();
        }
        private void LoadGiuong()
        {
            bHasloaded = false;
            Utility.WaitNow(this);
            try
            {
                pnlGiuong.BringToFront();
                id_buong = Utility.Int32Dbnull(autoBuong.MyID, -1);
                m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(id_khoa, id_buong, 1);
                //m_dtDataGiuong = SPs.NoitruTimkiemGiuong(Utility.Int32Dbnull(autokhoa.MyID, -1), Utility.Int32Dbnull(autoBuong.MyID, -1), "", "").GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdBed, m_dtGiuong, true, true, "1=1", NoitruDmucGiuongbenh.Columns.SttHthi);
                autoGiuong1.Init(m_dtGiuong, new List<string>() { NoitruDmucGiuongbenh.Columns.IdGiuong, NoitruDmucGiuongbenh.Columns.MaGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong });
                CreateGiuong();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Utility.DefaultNow(this);
                bHasloaded = true;
                if (isGrid) grdBed_SelectionChanged(grdBed, new EventArgs());
            }

        }
        void CreateGiuong()
        {

            flowGiuong.Controls.Clear();
            foreach (DataRow drGiuong in m_dtGiuong.Rows)
            {
                ucGiuong _giuong = new ucGiuong(drGiuong);
                _giuong._OnClick += _giuong__OnClick;
                _giuong._OnSelectme += _giuong__OnSelectme;
                AddControl(flowGiuong, _giuong);
            }
            pnlGiuong.BringToFront();
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
            _Giuong = obj;
            ShowHistory();
            buildText();

        }
        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSearchGiuong_Click(object sender, EventArgs e)
        {
            LoadGiuong();
        }


        private void grdBed_SelectionChanged(object sender, EventArgs e)
        {
            if (!bHasloaded) return;
            LoadQheGiuong_LoaiGiuong();
        }
        private void LoadQheGiuong_LoaiGiuong()
        {
            try
            {
                if (!Utility.isValidGrid(grdBed))
                {
                    return;
                }
                Int16 bedId = Utility.Int16Dbnull(Utility.getValueOfGridCell(grdBed, NoitruQheGiuongGium.Columns.IdGiuong), -1);
                NoitruQheGiuongGiumCollection lstQhe = new Select().From(NoitruQheGiuongGium.Schema)
                .Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(bedId)).
                ExecuteAsCollection<NoitruQheGiuongGiumCollection>();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdGiaGiuong.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    var query = from kho in lstQhe.AsEnumerable()
                                where kho.IdGia == Utility.Int32Dbnull(gridExRow.Cells[NoitruGiabuonggiuong.Columns.IdGia].Value)
                                select kho;
                    if (query.Count() > 0)
                    {
                        gridExRow.IsChecked = true;
                    }

                    else
                    {
                        gridExRow.IsChecked = false;
                    }
                    gridExRow.EndEdit();

                }
            }
            catch (Exception ex)
            {


            }

        }


        bool isGrid = true;
        private void frm_tracuu_buonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();

            if (e.KeyCode == Keys.F3) cmdSearchGiuong.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                if (isGrid) pnlGrid.BringToFront();
                else pnlDohoa.BringToFront();
                isGrid = !isGrid;
                chkGrid.Checked = isGrid;
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
            pnlBuong.Visible = true;
            pnlKhoa.Visible = false;
            pnlGiuong.Visible = false;
        }

        private void cmdBackKhoa_Click(object sender, EventArgs e)
        {
            VisibleItem(pnlTimkiemKhoa);
        }
    }
}