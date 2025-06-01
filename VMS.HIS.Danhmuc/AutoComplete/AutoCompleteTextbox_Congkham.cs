using System;
using System.Collections;           // if you would like to use ArrayList insted
using System.Collections.Generic;   // here we use Generic Type List<string>
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UCs
{
    

    // the component is derived from a TextBox 
    // and is therfore called TextBox, but ment is this class (AutoCompleteTextbox_Congkham)
    public class AutoCompleteTextbox_Congkham : TextBox
    {
        #region Fields
        ContextMenuStrip ctx = new ContextMenuStrip();
        // the ListBox used for suggestions
        private ListBox listBox;
        ToolStripMenuItem _useGrid = new ToolStripMenuItem("Tìm theo lưới");
        public delegate void OnSelectionChanged();
        public delegate void OnChangedView(bool gridview);
        public event OnChangedView _OnChangedView;
        public event OnSelectionChanged _OnSelectionChanged;
        public delegate void OnGridSelectionChanged(int id_congkham, string ma_congkham, string ten_congkham, string ten_phongkham);
        public event OnGridSelectionChanged _OnGridSelectionChanged;
        public delegate void OnEnterMe();
        public event OnEnterMe _OnEnterMe;
        public uc_grdCongkham _grid;
        // string to remember a former input
        private string oldText;
        public DataTable dtData=null;
        public bool AllowTextChanged=true;
        public Janus.Windows.GridEX.EditControls.EditBox txtMyID_Edit { get; set; }
        public Janus.Windows.GridEX.EditControls.EditBox txtMyCode_Edit { get; set; }
        public Janus.Windows.GridEX.EditControls.EditBox txtMyName_Edit { get; set; }
        public TextBox txtMyID { get; set; }
        public TextBox txtMyCode { get; set; }
        public TextBox txtMyName { get; set; }
        public object currentID = null;
        private System.ComponentModel.IContainer components;
        public object MyID { get; set; }
        public string MyCode { get; set; }

        public Control txtNext { get; set; }
        // a Panel for displaying
        private Panel panel;

        #endregion Fields

        #region Constructors

        // the constructor
        public AutoCompleteTextbox_Congkham()
            : base()
        {
            // assigning some default values
            // minimum characters to be typed before suggestions are displayed
            this.MinTypedCharacters = 2;
            ExtraWidth = 0;
            ExtraWidth_Pre = 0;
            GridView = true;
            RaiseEventEnter = false;
            RaiseEventEnterWhenEmpty = false;
            DefaultCode = "-1";
            DefaultID = "-1";
            splitChar = '@';
            splitCharIDAndCode = '#';
            MaxHeight = -1;
            MyText = "";
            RaiseEvent = false;
            CompareNoID = true;
            FillValueAfterSelect = false;
            MyID = DefaultID;
            MyCode = DefaultCode;
            _useGrid.CheckOnClick = true;
            _useGrid.Click += new EventHandler(_useGrid_Click);
            ctx.Items.Add(_useGrid);
            this.ContextMenuStrip = ctx;
            // not cases sensitive
            this.CaseSensitive = false;
            // the list for our suggestions database
            // the sample dictionary en-EN.dic is stored here when form1 is loaded (see form1.Load event)
            this.AutoCompleteList = new List<string>();
            _grid = new uc_grdCongkham();
            // the listbox used for suggestions
            this.listBox = new ListBox();
            this.listBox.Name = "SuggestionListBox";
            this.listBox.Font = this.Font;
            this.listBox.Visible = true;
            this.listBox.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.listBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox.ScrollAlwaysVisible = true;

           
            // the panel to hold the listbox later on
            this.panel = new Panel();
            this.panel.Visible = false;
            this.panel.Font = this.Font;
            // to be able to fit to changing sizes of the parent form
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            // initialize with minimum size to avoid overlaping or flickering problems
            this.panel.ClientSize = new System.Drawing.Size(1, 1);
            this.panel.Name = "SuggestionPanel";
            this.panel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.panel.BackColor = Color.Transparent;
            this.panel.ForeColor = Color.Transparent;
            this.panel.Text = "";
            this.panel.PerformLayout();
            // add the listbox to the panel if not already done
            if (!panel.Controls.Contains(listBox))
            {
                this.panel.Controls.Add(listBox);
                
            }
            if (!panel.Controls.Contains(_grid))
            {
                this.panel.Controls.Add(_grid);

            }
            // make the listbox fill the panel
            this.listBox.Dock = DockStyle.Fill;
            _grid.Dock = DockStyle.Fill;
            // only one itme can be selected from the listbox
            this.listBox.SelectionMode = SelectionMode.One;
            // the events to be fired if an item is selected
            this.listBox.KeyDown += new KeyEventHandler(listBox_KeyDown);
            this.listBox.MouseClick += new MouseEventHandler(listBox_MouseClick);
            this.listBox.MouseDoubleClick += new MouseEventHandler(listBox_MouseDoubleClick);
            this.listBox.SelectedIndexChanged += new EventHandler(listBox_SelectedIndexChanged);
            #region Excursus: ArrayList vs. List<string>
            // surpringly ArrayList is a little faster than List<string>
            // to use ArrayList instead, replace every 'List<string>' with 'ArrayList'
            // i will used List<string> cause it's generic type
            #endregion Excursus: ArrayList vs. List<string>
            // the list of suggestions actually displayed in the listbox
            // a subset of AutoCompleteList according to the typed in keyphrase
            this.CurrentAutoCompleteList = new List<string>();
            this.CurrentAutoCompleteList_IDAndCode = new List<string>();
            this.CurrentAutoCompleteList_IDThuockho = new List<string>();

            #region Excursus: DataSource vs. AddRange
            // using DataSource is faster than adding items (see
            // uncommented listBox.Items.AddRange method below)
            #endregion Excursus: DataSource vs. AddRange
            // Bind the CurrentAutoCompleteList as DataSource to the listbox
            listBox.DataSource = CurrentAutoCompleteList;

            _grid._OnCellValueChanged += _grid__OnCellValueChanged1;
            _grid._OnKeyDown += _grid__OnKeyDown1;
            _grid._OnPreviewKeyDown += _grid__OnPreviewKeyDown;
            _grid._OnSelectionChanged += _grid__OnSelectionChanged1;
            this.MouseClick += AutoCompleteTextbox_Congkham_MouseClick;
            _grid.AllowedChanged = false;
            // set the input to remember, which is empty so far
            oldText = this.Text;
        }

        private void AutoCompleteTextbox_Congkham_MouseClick(object sender, MouseEventArgs e)
        {
            this.Select(this.Text.Length, 0);
            ShowSuggests(" ");
        }

        private void _grid__OnPreviewKeyDown()
        {
            
        }

        private void _grid__OnKeyDown1(int id_congkham, string ma_congkham, string ten_congkham, string ten_phongkham)
        {
            _Text = ten_congkham;
            MyText = ten_congkham;
            MyCode = ma_congkham;
            MyID = id_congkham;
            OnKeyDown(new KeyEventArgs(Keys.Enter));
            //if(_OnGridSelectionChanged != null) _OnGridSelectionChanged(id_congkham, ma_congkham, ten_congkham, ten_phongkham);
        }

        private void _grid__OnCellValueChanged1(int id_congkham, string ma_congkham, string ten_congkham, string ten_phongkham)
        {
           
        }

        private void _grid__OnSelectionChanged1(int id_congkham, string ma_congkham, string ten_congkham, string ten_phongkham)
        {
            if (!AllowTextChanged) return;
            if (txtMyID != null)
                txtMyID.Text = id_congkham.ToString();
            if (txtMyID_Edit != null)
                txtMyID_Edit.Text = id_congkham.ToString();
            MyID = id_congkham;
            if (txtMyName != null)
                txtMyName.Text = ten_congkham;
            if (txtMyName_Edit != null)
                txtMyName_Edit.Text = ten_congkham;
            if (_OnGridSelectionChanged != null) _OnGridSelectionChanged(id_congkham, ma_congkham, ten_congkham, ten_phongkham);
        }

        bool _RaiseEvent_old, RaiseEventEnter_old, RaiseEventEnterWhenEmpty_old;
        public void PreventEvents()
        {
            _RaiseEvent_old = RaiseEvent;
            RaiseEventEnter_old = RaiseEventEnter;
            RaiseEventEnterWhenEmpty_old = RaiseEventEnterWhenEmpty;

            RaiseEvent = false;
            RaiseEventEnter = false;
            RaiseEventEnterWhenEmpty = false;
        }
        bool ON_OFF = false;
        public void ShowMe()
        {
            ON_OFF = !ON_OFF;
            if (ON_OFF)
            {
                _Text = "";
                Text = " ";
            }
            else
            {
                HideSuggestionListBox();
            }
        }
        public void ResetEvents()
        {
            RaiseEvent = _RaiseEvent_old;
            RaiseEventEnter = RaiseEventEnter_old;
            RaiseEventEnterWhenEmpty = RaiseEventEnterWhenEmpty_old;
        }
        void _useGrid_Click(object sender, EventArgs e)
        {
            GridView = _useGrid.Checked;
           
            ChangeDataSource();
            if (_OnChangedView != null) _OnChangedView(GridView);
        }

       

        #endregion Constructors

        #region Properties
        public bool TakeCode
        {
            get;
            set;
        }
        public Color _backcolor
        {
            get { return listBox.BackColor; }
            set { listBox.BackColor = value; }
        }
        public Font _Font
        {
            get { return listBox.Font; }
            set { listBox.Font = value; }
        }
        public string DefaultID
        {
            get;
            set;
        }
        public string DefaultCode
        {
            get;
            set;
        }
        // the list for our suggestions database
        public List<string> AutoCompleteList
        {
            get;
            set;
        }

        // case sensitivity
        public bool CaseSensitive
        {
            get;
            set;
        }
        public bool GridView
        {
            get;
            set;
        }
        public bool AllowedSelectPrice
        {
            get;
            set;
        }
        public bool CompareNoID
        {
            get;
            set;
        }

       
        // minimum characters to be typed before suggestions are displayed
        public int MinTypedCharacters
        {
            get;
            set;
        }
        public int ExtraWidth
        {
            get;
            set;
        }
        public int ExtraWidth_Pre
        {
            get;
            set;
        }
        public char splitChar
        {
            get;
            set;
        }
        public char splitCharIDAndCode
        {
            get;
            set;
        }
        public string _Text
        {
            set
            {
                AllowTextChanged = false;
                Text = value;
                AllowTextChanged = true;
            }
        }
        public string MyText { get; set; }
        public void ClearText()
        {
            AllowTextChanged = false;
            PreventEvents();
            _Text = "";
            ResetEvents();
            AllowTextChanged = true;

        }
        // the index selected in the listbox
        // maybe of intrest for other classes
        public int SelectedIndex
        {
            get
            {
                if (GridView)
                {
                    if (_grid.grdList.CurrentRow == null) return -1;
                    return _grid.grdList.CurrentRow.Position;
                }
                else
                {
                    return listBox.SelectedIndex;
                }
            }
            set
            {
                if (GridView)
                {
//if(_grid.grdICD.GetDataRows().Length>0)
//    _grid.grdICD.se
                }
                else
                {
                    // musn't be null
                    if (listBox.Items.Count != 0)
                    { listBox.SelectedIndex = value; }
                }
            }
        }

        public string Drug_ID { get; set; }
        // the actual list of currently displayed suggestions
        private List<string> CurrentAutoCompleteList
        {
            set;
            get;
        }
        public bool RaiseEvent
        {
            set;
            get;
        }
        public bool RaiseEventEnter
        {
            set;
            get;
        }
        public bool RaiseEventEnterWhenEmpty
        {
            set;
            get;
        }
        public bool FillValueAfterSelect
        {
            set;
            get;
        }
        private List<string> CurrentAutoCompleteList_IDAndCode
        {
            set;
            get;
        }
        private List<string> CurrentAutoCompleteList_IDThuockho
        {
            set;
            get;
        }
        public int MaxHeight
        {
            set;
            get;
        }

        
        #endregion Properties

        #region Methods
       
        public void ChangeDataSource()
        {
            try
            {
                if (AllowedSelectPrice) GridView = true;
                _grid.AllowedChanged = false;
                _grid.HideColums(this.AllowedSelectPrice);
                this.MinTypedCharacters = 1;
                _grid.Visible = GridView;
                listBox.Visible = !GridView;
                if (dtData == null)
                {
                    this.AutoCompleteList = new List<string>();
                    return;
                }
                dtData.DefaultView.RowFilter = "1=1";
                if (this.AutoCompleteList == null || this.AutoCompleteList.Count <= 0)
                    CreateAutocomplete();
                if(GridView)
                {
                    Utility.SetDataSourceForDataGridEx_BasicNofilter(_grid.grdList, dtData, true, true, "1=1", DmucDichvukcb.Columns.TenDichvukcb);
                    if (_grid.grdList.GetDataRows().Length > 0) _grid.grdList.MoveFirst();
                }
            }
            catch
            {
            }
            finally
            {
                _grid.AllowedChanged = true;
            }
            //OnTextChanged(new EventArgs());
        }
        private void CreateAutocomplete()
        {
            DataRow[] arrDr = null;
            try
            {
                if (dtData == null) return;
                if (!dtData.Columns.Contains("ShortCut"))
                    dtData.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtData.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucDichvukcb.Columns.TenDichvukcb].ToString().Trim()+" ";
                    shortcut = dr[DmucDichvukcb.Columns.MaDichvukcb].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in dtData.Select("1=1", DmucDichvukcb.Columns.TenDichvukcb).AsEnumerable()
                            select p[DmucDichvukcb.Columns.IdDichvukcb].ToString() + "#" + p[DmucDichvukcb.Columns.MaDichvukcb].ToString() + string.Format("@Id: {0};Code: {1}; ", p[DmucDichvukcb.Columns.TenDichvukcb].ToString(), p[DmucDichvukcb.Columns.TenDichvukcb].ToString()) + p[DmucDichvukcb.Columns.TenDichvukcb].ToString() + "@" + p["shortcut"].ToString() + "$" +  "-1";
                source = query.ToList();
                this.AutoCompleteList = source;
                //this.TextAlign = HorizontalAlignment.Left;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;

            }
        }

        public void SetId(object _Id)
        {
            try
            {
                MyID = _Id;
                var p = (from q in this.AutoCompleteList
                         where q.Contains(Utility.sDbnull(_Id) + "#")
                         select q).ToList();
                if (p.Count > 0)
                    try2getName(p.First());
                else
                {
                    setDefaultValue();
                    _Text = "";
                }
            }
            catch
            {
            }
        }
        public void SetCode(object code)
        {
            try
            {
                MyCode = code.ToString();
                var p = (from q in this.AutoCompleteList
                         where q.Contains("#" + Utility.DoTrim(code.ToString()) + "@")
                         select q).ToList();
                if (p.Count > 0)
                    try2getName(p.First());
                else
                {
                    setDefaultValue();
                    _Text = "";
                }
            }
            catch
            {
            }
        }
        void try2getName(string value)
        {
            try
            {
                if(!GridView)
                {
                    string[] lst1 = value.Split('#');
                    string[] lst2 = lst1[1].Split('@');
                    if (lst1.Count() == 2)
                    {
                        if (txtMyID != null)
                            txtMyID.Text = lst1[0];
                        if (txtMyID_Edit != null)
                            txtMyID_Edit.Text = lst1[0];
                        MyID = lst1[0];
                    }
                    if (lst2.Count() >= 2)
                    {
                        if (txtMyCode != null)
                            txtMyCode.Text = lst2[0];
                        if (txtMyCode_Edit != null)
                            txtMyCode_Edit.Text = lst2[0];
                        if (txtMyName_Edit != null)
                            txtMyName_Edit.Text = lst2[1];
                        MyCode = lst2[0];
                        MyText = lst2[1];
                        if (TakeCode)
                            _Text = MyCode;
                        else
                            _Text = lst2[1];
                    }
                }
                else
                {
                    DataRow[] arrDr = dtData.Select("id_dichvukcb="+MyID);
                    if(arrDr.Length>0)
                    {
                        MyCode =Utility.sDbnull( arrDr[0]["ma_dichvukcb"]);
                        MyText = Utility.sDbnull(arrDr[0]["ten_dichvukcb"]);
                        _Text = MyText;
                    }    
                }    
               

            }
            catch
            {
            }
        }
        private void txtDrug_Code_KeyDown(object sender, KeyEventArgs e)
        {

            //try
            //{
            //    if (e.KeyCode == Keys.Escape) grdICD.Visible = false;
            //    if (e.KeyCode == Keys.Enter && grdICD.RowCount > 0)
            //        grdICD_KeyDown(grdICD, new KeyEventArgs(Keys.Enter));
            //    if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.NumPad2) ||
            //        (e.KeyCode == Keys.Up || e.KeyCode == Keys.NumPad8))
            //        if (grdICD.Visible && (e.KeyCode == Keys.Down || e.KeyCode == Keys.NumPad2))
            //        {
            //            if (grdICD.CurrentRow.Position < grdICD.RowCount - 1)
            //                grdICD.MoveNext();
            //            else
            //                grdICD.MoveFirst();
            //        }
            //    if (grdICD.Visible && (e.KeyCode == Keys.Up || e.KeyCode == Keys.NumPad8))
            //    {
            //        if (grdICD.CurrentRow.Position > 0)
            //            grdICD.MovePrevious();
            //        else
            //            grdICD.MoveLast();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void txtDrug_Code_TextChanged(object sender, EventArgs e)
        {
            //rowFilter = "1=1";
            //LoadFilterDrugCode(txtDrug_Code.Text.Trim());
        }

        /// <summary>
        /// hàm thực hiện việc lọc thong tin của thuốc
        /// </summary>
        /// <param name="text"></param>
        private void LoadFilterDrugCode(string text)
        {
            string rowFilter = "";
            if ( dtData == null || dtData.Rows.Count <= 0) return;
            try
            {
                if (Utility.DoTrim(text) == "") rowFilter = "1=1";
                else text = text.TrimStart();
                if (text.Trim() != "")
                {
                    rowFilter ="ten_phongkham like '%" + text+"%' OR " + DmucDichvukcb.Columns.TenDichvukcb + " like '%" + text + "%' OR " + DmucDichvukcb.Columns.MaDichvukcb + " LIKE '%" + text + "%'";
                }
                dtData.DefaultView.RowFilter = rowFilter;
               _grid.grdList.MoveFirst();
            }
            catch (Exception)
            {
            }
            finally
            {
                _grid.RowIndex = 1;
            }
        }

       

        private string UnitName = "";
        private int GridRowPosition = -1;
      
        private int getHeighOfGridview()
        {
            if ((_grid.grdList.RowCount + 1) * 15 >= 210) return 210;
            else
                return (_grid.grdList.RowCount + 1) * 20;
        }
        // hides the listbox
        public void HideSuggestionListBox()
        {
            if ((TopLevelControl != null))
            {
                // hiding the panel also hides the listbox
                panel.Hide();
                // now remove it from the TopLevelControl (Form1 in this example)
                if (this.TopLevelControl.Controls.Contains(panel))
                {
                    if (_grid.grdList != null && _grid.grdList.CurrentRow != null)
                        GridRowPosition = _grid.grdList.CurrentRow.Position;
                    this.TopLevelControl.Controls.Remove(panel);
                }
                //SelectedIndex = -1;
            }
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {

            if ((args.Control && args.KeyCode == Keys.D) )
            {
                this._Text = "";
                setDefaultValue();
                if (RaiseEventEnterWhenEmpty) _OnEnterMe();
                HideSuggestionListBox();
            }
            // if user presses key.up
            if ((args.KeyCode == Keys.Up || args.KeyCode == Keys.NumPad8))
            {
                // move the selection in listbox one up
                MoveSelectionInListBox((SelectedIndex - 1),false);
               
                // work is done
                args.Handled = true;
            }
            // on key.down
            else if ((args.KeyCode == Keys.Down || args.KeyCode == Keys.NumPad2))
            {
                //move one down
                MoveSelectionInListBox((SelectedIndex + 1),true);
                
                args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageUp))
            {
                //move 10 up
                MoveSelectionInListBox((SelectedIndex - 10),false);
                args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageDown))
            {
                //move 10 down
                MoveSelectionInListBox((SelectedIndex + 10),true);
                args.Handled = true;
            }
            // on enter
            else if ((args.KeyCode == Keys.Enter))
            {
                AllowChangedListBox = false;
                if (GridView)
                {
                    if (panel.Visible)
                    {
                        try
                        {
                            if (GridRowPosition > 0 && _grid.grdList.CurrentRow.Position <= 0)
                                _grid.grdList.MoveTo(GridRowPosition);
                        }
                        catch (Exception)
                        {


                        }

                        //if (_grid.grdICD.RowCount>0 && _grid.grdICD.CurrentRow != null && _grid.grdICD.CurrentRow.RowType == Janus.Windows.GridEX.RowType.Record)
                        if (Utility.isValidGrid(_grid.grdList) && _grid.grdList.CurrentRow.Position >= 0)
                            SelectItem();
                        _grid.RowIndex = -1;
                        _grid.AllowedChanged = true;
                        if (txtNext != null) txtNext.Focus();
                    }
                }
                else
                {
                    if (listBox.SelectedIndex != -1 && CurrentAutoCompleteList.Count > 0)//&& this.TopLevelControl.Controls.Contains(panel))
                        // select the item in the ListBox
                        SelectItem();
                    listBox.SelectedIndex = -1;
                    if (txtNext != null) txtNext.Focus();
                }
                AllowChangedListBox = true;
                if (RaiseEventEnter && _OnEnterMe!=null) _OnEnterMe();
                args.Handled = true;
            }
            else
            {
                // work is not done, maybe the base class will process the event, so call it...
                base.OnKeyDown(args);
            }
        }

        // if the user leaves the TextBox, the ListBox and the panel ist hidden
        protected override void OnLostFocus(System.EventArgs e)
        {
            try
            {
                if (!panel.ContainsFocus)
                {
                    _grid.RowIndex = -1;
                    // call the baseclass event
                    base.OnLostFocus(e);
                    // then hide the stuff
                    this.HideSuggestionListBox();
                }
            }
            catch
            {
            }
            finally
            {
                //CurrentAutoCompleteList.Clear();
                //CurrentAutoCompleteList_IDAndCode.Clear();
            }
        }
        protected override void OnGotFocus(System.EventArgs e)
        {
            try
            {
                if (!panel.ContainsFocus)
                {
                    this.SelectAll();
                    // call the baseclass event
                    base.OnGotFocus(e);
                    // then hide the stuff
                   
                }
            }
            catch
            {
            }
            finally
            {
                //CurrentAutoCompleteList.Clear();
                //CurrentAutoCompleteList_IDAndCode.Clear();
            }
        }
        // if the input changes, call ShowSuggests()
        protected override void OnTextChanged(EventArgs args)
        {
            // avoids crashing the designer
            if (!this.DesignMode)
                ShowSuggests();
            base.OnTextChanged(args);
            // remember input
            oldText = this.Text;
        }

        // event for any key pressed in the ListBox
        private void listBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                // select the current item
                SelectItem();
                OnKeyDown(new KeyEventArgs(Keys.Enter));
                // work done
                e.Handled = true;
            }
        }
        bool AllowChangedListBox = true;
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowChangedListBox || !this.TopLevelControl.Controls.Contains(panel)) return;
                if (AllowChangedListBox && (CurrentAutoCompleteList.Count <= 0 || listBox.SelectedItem == null))
                {
                    setDefaultValue();
                    if (txtMyName != null)
                        txtMyName.Clear();
                    if (txtMyName_Edit != null)
                        txtMyName_Edit.Clear();
                    return;
                }
                if (CurrentAutoCompleteList.Count != CurrentAutoCompleteList_IDAndCode.Count || listBox.SelectedIndex<0) return;
                
                string[] arrValues = CurrentAutoCompleteList_IDAndCode[listBox.SelectedIndex].ToString().Trim().Split(splitCharIDAndCode);
                // set the Text of the TextBox to the selected item of the ListBox
                if (arrValues.Length ==2 )
                {
                    if (txtMyID != null)
                        txtMyID.Text = arrValues[0];
                    if (txtMyID_Edit != null)
                        txtMyID_Edit.Text = arrValues[0];
                    if (txtMyCode != null)
                        txtMyCode.Text = arrValues[1];
                    if (txtMyCode_Edit != null)
                        txtMyCode_Edit.Text = arrValues[1];
                    MyID = arrValues[0];
                    MyCode = arrValues[1];
                    MyText = listBox.SelectedItem.ToString(); 
                    //find id_thuockho
                    //DataRow[] arrDr = dtData.Select("id_thuoc=" + MyID);
                    //if (arrDr.Length > 0)
                    if (txtMyName != null)
                        txtMyName.Text = listBox.SelectedItem.ToString();
                    if (txtMyName_Edit != null)
                        txtMyName_Edit.Text = listBox.SelectedItem.ToString();
                }
                if (RaiseEvent) if(_OnSelectionChanged!=null)  _OnSelectionChanged();
            }
            catch (Exception ex)
            {
                Utility.CatchException("listBox_SelectedIndexChanged",ex);
                //throw;
            }


        }
        public void setDefaultValue()
        {
            _grid.RowIndex = -1;
            if (txtMyID != null)
                txtMyID.Text = DefaultID;
            if (txtMyID_Edit != null)
                txtMyID_Edit.Text = DefaultID;
            if (txtMyCode != null)
                txtMyCode.Text = DefaultCode;
            if (txtMyCode_Edit != null)
                txtMyCode_Edit.Text = DefaultCode;

            MyID = DefaultID;
            MyCode = DefaultCode;
            MyText = "";
        }
        // event for MouseClick in the ListBox
        private void listBox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //AllowChangedListBox = false;
            //listBox_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            //AllowChangedListBox = true;
            AllowChangedListBox = false;
            SelectItem();
            if (RaiseEventEnter && _OnEnterMe != null) _OnEnterMe();
            SelectionStart = 0;
            this.Focus();
            AllowChangedListBox = true;
        }

        // event for DoubleClick in the ListBox
        private void listBox_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //AllowChangedListBox = false;
            //// select the current item
            //SelectItem();
            //if (RaiseEventEnter) _OnEnterMe();
            //AllowChangedListBox = true;
        }

        private void MoveSelectionInListBox(int Index,bool foreward)
        {
            if (GridView)
            {
                MoveSelectionInGrid(foreward);
            }
            else
            {
                // beginning of list
                if (Index <= -1)
                {
                    this.SelectedIndex = listBox.Items.Count - 1;
                }
                else
                    // end of liste
                    if (Index > (listBox.Items.Count - 1))
                    {
                        SelectedIndex = 0;// (listBox.Items.Count - 1);
                    }
                    else
                    // somewhere in the middle
                    {
                        SelectedIndex = Index;
                    }
            }
        }
        private void MoveSelectionInGrid(bool foreward)
        {

            if (!Utility.isValidGrid(_grid.grdList)) return;
            // beginning of list
            if (foreward)
            {
                if (_grid.grdList.CurrentRow.Position < _grid.grdList.RowCount - 1)
                    _grid.grdList.MoveNext();
                else
                    _grid.grdList.MoveFirst();
            }
            else
            {
                if (_grid.grdList.CurrentRow.Position > 0)
                    _grid.grdList.MovePrevious();
                 else
                    _grid.grdList.MoveLast();
            }
        }

        // selects the current item
        private bool SelectItem()
        {
            try
            {
                if (GridView)
                {
                    if (Utility.isValidGrid(_grid.grdList))
                    {
                        //string[] arrValues = listBox.SelectedItem.ToString().Trim().Split('-');
                        //// set the Text of the TextBox to the selected item of the ListBox
                        //if (arrValues.Length > 1)
                        //    this._Text = arrValues[1];//this.listBox.SelectedItem.ToString();
                        //else
                        _Text = _grid.grdList.GetValue(DmucDichvukcb.Columns.TenDichvukcb).ToString();
                        MyID = Utility.Int32Dbnull(_grid.grdList.GetValue("id_dichvukcb"), "-1");
                        MyCode = Utility.sDbnull(_grid.grdList.GetValue("ma_dichvukcb"), "");

                        this.Select(this.Text.Length, 0);
                        AllowTextChanged = false;
                        _grid.AllowedChanged = AllowTextChanged;
                        _grid.grdList.MoveFirst();
                       
                        AllowTextChanged = true;
                    }
                }
                else
                {
                    // if the ListBox is not empty
                    if (((this.listBox.Items.Count > 0) && (this.SelectedIndex > -1)))
                    {
                        if (CurrentAutoCompleteList.Count != CurrentAutoCompleteList_IDAndCode.Count || listBox.SelectedIndex < 0) return false;

                        string[] arrValues = CurrentAutoCompleteList_IDAndCode[listBox.SelectedIndex].ToString().Trim().Split(splitCharIDAndCode);
                        // set the Text of the TextBox to the selected item of the ListBox
                        if (arrValues.Length == 2)
                        {
                            if (txtMyID != null)
                                txtMyID.Text = arrValues[0];
                            if (txtMyID_Edit != null)
                                txtMyID_Edit.Text = arrValues[0];
                            if (txtMyCode != null)
                                txtMyCode.Text = arrValues[1];
                            if (txtMyCode_Edit != null)
                                txtMyCode_Edit.Text = arrValues[1];
                            MyID = arrValues[0];
                            MyCode = arrValues[1];
                            MyText = listBox.SelectedItem.ToString(); 
                            //find id_thuockho
                            //DataRow[] arrDr = dtData.Select("id_thuoc=" + MyID);
                            //if (arrDr.Length > 0)
                            if (txtMyName != null)
                                txtMyName.Text = listBox.SelectedItem.ToString();
                            if (txtMyName_Edit != null)
                                txtMyName_Edit.Text = listBox.SelectedItem.ToString();
                        }

                        this._Text = this.listBox.SelectedItem.ToString();
                        this.Select(this.Text.Length, 0);
                        SelectedIndex = 0;

                    }
                }
                // and hide the ListBox
                this.HideSuggestionListBox();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
                CurrentAutoCompleteList_IDThuockho.Clear();
            }
        }

       
        // shows the suggestions in ListBox beneath the TextBox
        // and fitting it into the TopLevelControl
        private void ShowSuggests()
        {
            if (!AllowTextChanged) return;
            
            // show only if MinTypedCharacters have been typed
            if (this.Text.Length >= MinTypedCharacters)
            {
                // prevent overlapping problems with other controls
                // while loading data there is nothing to draw, so suspend layout
                panel.SuspendLayout();
                // user is typing forward, char has been added at the end of the former input
                if ((this.Text.Length > 0) && (this.oldText == this.Text.Substring(0, this.Text.Length - 1)))
                {
                    //handle forward typing with refresh
                    UpdateCurrentAutoCompleteList(this.Text);
                }
                // user is typing backward - char bas been removed
                else if ((this.oldText.Length > 0) && (this.Text == this.oldText.Substring(0, this.oldText.Length - 1)))
                {
                    //handle backward typing with refresh
                    UpdateCurrentAutoCompleteList(this.Text);
                }
                // something within has changed
                else
                {
                    // handle other things like corrections in the middle of the input, clipboard pastes, etc. with refresh
                    UpdateCurrentAutoCompleteList(this.Text);
                }

                if ((GridView && ((DataView)_grid.grdList.DataSource).Count>0) || ((CurrentAutoCompleteList != null) && CurrentAutoCompleteList.Count > 0))
                {
                    // finally show Panel and ListBox
                    // (but after refresh to prevent drawing empty rectangles)
                    panel.Show();
                    // at the top of all controls
                    panel.BringToFront();
                    if (GridView) _grid.grdList.MoveFirst();
                    if (!GridView)
                        listBox_SelectedIndexChanged(listBox, new EventArgs());
                    // then give the focuse back to the TextBox (this control)
                    this.Focus();
                }
                // or hide if no results
                else
                {
                    this.HideSuggestionListBox();
                }
                // prevent overlapping problems with other controls
                panel.ResumeLayout(true);
                if (Utility.DoTrim(this.Text) == "")
                {
                    setDefaultValue();
                }
            }
            // hide if typed chars <= MinCharsTyped
            else
            {
                AllowChangedListBox = false;
                listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
                CurrentAutoCompleteList_IDThuockho.Clear();
                setDefaultValue();
                this.HideSuggestionListBox();
            }
            if (RaiseEventEnterWhenEmpty && _OnSelectionChanged != null) _OnSelectionChanged();
            if (Utility.DoTrim(this.Text) == "" && RaiseEventEnterWhenEmpty) if(_OnEnterMe!=null) _OnEnterMe();
        }
        private void ShowSuggests(string _content)
        {
            if (!AllowTextChanged) return;

            // show only if MinTypedCharacters have been typed
            if (_content.Length >= MinTypedCharacters)
            {
                // prevent overlapping problems with other controls
                // while loading data there is nothing to draw, so suspend layout
                panel.SuspendLayout();
                // user is typing forward, char has been added at the end of the former input
                if ((_content.Length > 0) && (this.oldText == _content.Substring(0, _content.Length - 1)))
                {
                    //handle forward typing with refresh
                    UpdateCurrentAutoCompleteList(_content);
                }
                // user is typing backward - char bas been removed
                else if ((this.oldText.Length > 0) && (_content == this.oldText.Substring(0, this.oldText.Length - 1)))
                {
                    //handle backward typing with refresh
                    UpdateCurrentAutoCompleteList(_content);
                }
                // something within has changed
                else
                {
                    // handle other things like corrections in the middle of the input, clipboard pastes, etc. with refresh
                    UpdateCurrentAutoCompleteList(_content);
                }

                if ((GridView && ((DataView)_grid.grdList.DataSource).Count > 0) || ((CurrentAutoCompleteList != null) && CurrentAutoCompleteList.Count > 0))
                {
                    // finally show Panel and ListBox
                    // (but after refresh to prevent drawing empty rectangles)
                    panel.Show();
                    // at the top of all controls
                    panel.BringToFront();
                    if (GridView)
                        if (Utility.sDbnull(MyID)=="")
                            _grid.grdList.MoveFirst();
                        else
                            Utility.GotoNewRowJanus(_grid.grdList, "id_dichvukcb",Utility.sDbnull( MyID));
                    if (!GridView)
                        listBox_SelectedIndexChanged(listBox, new EventArgs());
                    // then give the focuse back to the TextBox (this control)
                    this.Focus();
                }
                // or hide if no results
                else
                {
                    this.HideSuggestionListBox();
                }
                // prevent overlapping problems with other controls
                panel.ResumeLayout(true);
                if (Utility.DoTrim(_content) == "" || Utility.sDbnull(MyID)=="")
                {
                    setDefaultValue();
                }
            }
            // hide if typed chars <= MinCharsTyped
            else
            {
                AllowChangedListBox = false;
                listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
                CurrentAutoCompleteList_IDThuockho.Clear();
                setDefaultValue();
                this.HideSuggestionListBox();
            }
            if (RaiseEventEnterWhenEmpty && _OnSelectionChanged != null) _OnSelectionChanged();
            if (Utility.DoTrim(_content) == "" && RaiseEventEnterWhenEmpty) if (_OnEnterMe != null) _OnEnterMe();
        }
        private void ShowSuggests(string _content,string my_id)
        {
            if (!AllowTextChanged) return;

            // show only if MinTypedCharacters have been typed
            if (_content.Length >= MinTypedCharacters)
            {
                // prevent overlapping problems with other controls
                // while loading data there is nothing to draw, so suspend layout
                panel.SuspendLayout();
                // user is typing forward, char has been added at the end of the former input
                if ((_content.Length > 0) && (this.oldText == _content.Substring(0, _content.Length - 1)))
                {
                    //handle forward typing with refresh
                    UpdateCurrentAutoCompleteList(_content);
                }
                // user is typing backward - char bas been removed
                else if ((this.oldText.Length > 0) && (_content == this.oldText.Substring(0, this.oldText.Length - 1)))
                {
                    //handle backward typing with refresh
                    UpdateCurrentAutoCompleteList(_content);
                }
                // something within has changed
                else
                {
                    // handle other things like corrections in the middle of the input, clipboard pastes, etc. with refresh
                    UpdateCurrentAutoCompleteList(_content);
                }

                if ((GridView && ((DataView)_grid.grdList.DataSource).Count > 0) || ((CurrentAutoCompleteList != null) && CurrentAutoCompleteList.Count > 0))
                {
                    // finally show Panel and ListBox
                    // (but after refresh to prevent drawing empty rectangles)
                    panel.Show();
                    // at the top of all controls
                    panel.BringToFront();
                    if (GridView) _grid.grdList.MoveFirst();
                    if (!GridView)
                        listBox_SelectedIndexChanged(listBox, new EventArgs());
                    // then give the focuse back to the TextBox (this control)
                    this.Focus();
                }
                // or hide if no results
                else
                {
                    this.HideSuggestionListBox();
                }
                // prevent overlapping problems with other controls
                panel.ResumeLayout(true);
                if (Utility.DoTrim(_content) == "")
                {
                    setDefaultValue();
                }
            }
            // hide if typed chars <= MinCharsTyped
            else
            {
                AllowChangedListBox = false;
                listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
                CurrentAutoCompleteList_IDThuockho.Clear();
                setDefaultValue();
                this.HideSuggestionListBox();
            }
            if (RaiseEventEnterWhenEmpty && _OnSelectionChanged != null) _OnSelectionChanged();
            if (Utility.DoTrim(_content) == "" && RaiseEventEnterWhenEmpty) if (_OnEnterMe != null) _OnEnterMe();
        }
        public void ClearMe()
        {
            AllowTextChanged = false;
            this.Clear();
            AllowChangedListBox = false;
            listBox.SelectedIndex = -1;
            AllowChangedListBox = true;
            CurrentAutoCompleteList.Clear();
            CurrentAutoCompleteList_IDAndCode.Clear();
            CurrentAutoCompleteList_IDThuockho.Clear();
            setDefaultValue();
            AllowTextChanged = true;
            this.HideSuggestionListBox();

        }
        // This is a timecritical part
        // Fills/ refreshed the CurrentAutoCompleteList with appropreate candidates
        private void UpdateCurrentAutoCompleteList(string _content)
        {
            if (GridView)
            {
                LoadFilterDrugCode(_content);
            }
            else
            {
                string myText = Utility.DoTrim(_content);
                if (myText == "") myText = " ";
                // the list of suggestions has to be refreshed so clear it
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
                CurrentAutoCompleteList_IDThuockho.Clear();
                if (AutoCompleteList == null) return;
                // an find appropreate candidates for the new CurrentAutoCompleteList in AutoCompleteList
                foreach (string Str in AutoCompleteList)
                {
                    string _value = Str;
                    if (CompareNoID && Str.IndexOf("#") >= 0) _value = Str.Split('#')[1];
                    // be casesensitive
                    if (CaseSensitive)
                    {
                        // search for the substring (equal to SQL Like Command)
                        if ((_value.IndexOf(myText) > -1))
                        // Add candidates to new CurrentAutoCompleteList
                        {
                            CurrentAutoCompleteList.Add(Str.Split(splitChar)[1]);
                            CurrentAutoCompleteList_IDAndCode.Add(Str.Split(splitChar)[0]);
                            CurrentAutoCompleteList_IDThuockho.Add(Str.Substring(Str.LastIndexOf("$") + 1));
                        }
                    }
                    // or ignore case
                    else
                    {
                        // and search for the substring (equal to SQL Like Command)
                        if ((_value.ToLower().IndexOf(myText.ToLower()) > -1))
                        // Add candidates to new CurrentAutoCompleteList
                        {
                            CurrentAutoCompleteList.Add(Str.Split(splitChar)[1]);
                            CurrentAutoCompleteList_IDAndCode.Add(Str.Split(splitChar)[0]);
                            CurrentAutoCompleteList_IDThuockho.Add(Str.Substring(Str.LastIndexOf("$") + 1));
                        }
                    }
                }
            }
            #region Excursus: Performance measuring of Linq queries
            // This is a simple example of speedmeasurement for Linq querries
            /*
            CurrentAutoCompleteList.Clear();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // using Linq query seems to be slower (twice as slow)
            var query =
                from expression in this.AutoCompleteList
                where expression.ToLower().Contains(this.Text.ToLower())
                select expression;
            foreach (string searchTerm in query)
            {
                CurrentAutoCompleteList.Add(searchTerm);
            }
            stopWatch.Stop();
            // method (see below) for printing the performance values to console
            PrintStopwatch(stopWatch, "Linq - Contains");
            */
            #endregion Excursus: Performance measuring of Linq queries

            // countinue to update the ListBox - the visual part
            UpdateListBoxItems();
        }

        // This is the most timecritical part, adding items to the ListBox
        private void UpdateListBoxItems()
        {
            try
            {
                #region Excursus: DataSource vs. AddRange
                /*
                    // This part is not used due to 'listBox.DataSource' approach (see constructor)
                    // left for comparison, delete for productive use
                    listBox.BeginUpdate();
                    listBox.Items.Clear();
                    //Fills the ListBox
                    listBox.Items.AddRange(this.CurrentAutoCompleteList.ToArray());
                    listBox.EndUpdate();
                    // to use this method remove the following
                    // "((CurrencyManager)listBox.BindingContext[CurrentAutoCompleteList]).Refresh();" line and
                    // "listBox.DataSource = CurrentAutoCompleteList;" line from the constructor
                    */
                #endregion Excursus: DataSource vs. AddRange

                // if there is a TopLevelControl
                if ((TopLevelControl != null))
                {
                    _grid.Visible = GridView;
                    listBox.Visible = !GridView;
                    if (GridView)
                        _grid.BringToFront();
                    else
                        _grid.SendToBack();
                    // get its width
                    panel.Width = this.Width + ExtraWidth + (GridView ? ExtraWidth_Pre : 0);
                    // calculate the remeining height beneath the TextBox
                    panel.Height = GetMyHeigh();// ;
                    // and the Location to use
                    panel.Location = FindLocation(this) + new Size(0, this.Height); //this.Location + new Size(0, this.Height);
                    // Panel and ListBox have to be added to TopLevelControl.Controls before calling BingingContext
                    if (!this.TopLevelControl.Controls.Contains(panel))
                    {
                        // add the Panel and ListBox to the PartenForm
                        this.TopLevelControl.Controls.Add(panel);
                    }
                    // Update the listBox manually - List<string> dosn't support change events
                    // this is the DataSource approche, this is a bit tricky and may cause conflicts,
                    // so in case fall back to AddRange approache (see Excursus)
                    if (!GridView)
                        ((CurrencyManager)listBox.BindingContext[CurrentAutoCompleteList]).Refresh();
                }
            }
            catch (Exception)
            {


            }

        }
        private Point FindLocation(Control ctrl)
        {

            Point locationOnForm = ctrl.TopLevelControl.PointToClient(ctrl.Parent.PointToScreen(ctrl.Location));
            return new Point(locationOnForm.X - (GridView ? ExtraWidth_Pre : 0), locationOnForm.Y);
            //if (ctrl.Parent is Form)
            //    return ctrl.Location;
            //else
            //{
            //    Point p = FindLocation(ctrl.Parent);
            //    p.X += ctrl.Location.X;
            //    p.Y += ctrl.Location.Y;
            //    return p;
            //}
        }
        private int GetMyHeigh()
        {
            if (GridView)
            {
                //if ((dtData.DefaultView.Count+1 ) * 15 >= MaxHeight && MaxHeight > this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y)
                //    return this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y;
                //if (MaxHeight != -1)
                //{
                //    if ((dtData.DefaultView.Count + 1) * 15 >= MaxHeight) return MaxHeight;
                //    else
                //        return (dtData.DefaultView.Count + 1) * 20;
                //}
                //else
                //{
                //    return this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y;
                //}
                return MaxHeight <= -1 ? 300 : MaxHeight;
            }
            else
            {
                if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight && MaxHeight > this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y)
                    return this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y;
                if (MaxHeight != -1)
                {
                    if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight) return MaxHeight;
                    else
                        return (CurrentAutoCompleteList.Count + 1) * 15;
                }
                else
                {
                    return this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y;
                }
            }
                return listBox.Height;
            
        }

        #endregion Methods

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        #region Other

        /*
        // only needed for performance measuring - delete for productional use
        private void PrintStopwatch(Stopwatch stopWatch, string comment)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00},{3:000}s \t {4}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds, comment);
            Console.WriteLine("RunTime " + elapsedTime);
        }
        */

        #endregion Other
    }
}