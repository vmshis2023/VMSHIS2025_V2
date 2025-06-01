using System;
using System.Collections;           // if you would like to use ArrayList insted
using System.Collections.Generic;   // here we use Generic Type List<string>
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
namespace VNS.HIS.UCs
{
    

    // the component is derived from a TextBox 
    // and is therfore called TextBox, but ment is this class (AutoCompleteTextbox_Nhanvien)
    public class AutoCompleteTextbox_Nhanvien : TextBox
    {
        #region Fields

        // the ListBox used for suggestions
        private ListBox listBox;

        public delegate void OnSelectionChanged();
        public event OnSelectionChanged _OnSelectionChanged;

        public delegate void OnEnterMe();
        public event OnEnterMe _OnEnterMe;

        public delegate void OnShowData();
        public event OnShowData _OnShowData;

        ContextMenuStrip ctx = new ContextMenuStrip();
        
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
        public Control txtNext { get; set; }
        public object MyID { get; set; }
        public string MyCode { get; set; }
        public string myCode = "";
        private System.ComponentModel.IContainer components;
        // a Panel for displaying
        private Panel panel;
        ToolTip _ToolTip = new ToolTip();
        #endregion Fields

        #region Constructors

        // the constructor
        public AutoCompleteTextbox_Nhanvien()
            : base()
        {
            _ToolTip.IsBalloon = true;
            _ToolTip.ToolTipTitle = "Trợ giúp";
            // assigning some default values
            // minimum characters to be typed before suggestions are displayed
            this.MinTypedCharacters = 2;
            ExtraWidth = 0;
            MaxHeight = -1;
            splitChar = '@';
            splitCharIDAndCode = '#';
            RaiseEvent = false;
            RaiseEventEnter = false;
            RaiseEventEnterWhenEmpty = false;
            DefaultCode = "-1";
            DefaultID = "-1";
            MyID = DefaultID;
            MyCode = DefaultCode;
            myCode = MyCode;
            CompareNoID = true;
            TakeCode = false;
            
            FillValueAfterSelect = false;
            // not cases sensitive
            this.CaseSensitive = false;
            // the list for our suggestions database
            // the sample dictionary en-EN.dic is stored here when form1 is loaded (see form1.Load event)
            this.AutoCompleteList = new List<string>();

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

            // make the listbox fill the panel
            this.listBox.Dock = DockStyle.Fill;
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

            #region Excursus: DataSource vs. AddRange
            // using DataSource is faster than adding items (see
            // uncommented listBox.Items.AddRange method below)
            #endregion Excursus: DataSource vs. AddRange
            // Bind the CurrentAutoCompleteList as DataSource to the listbox
            listBox.DataSource = CurrentAutoCompleteList;
            _ToolTip.SetToolTip(this, "Nhấn chuột phải để cấu hình thêm bớt danh mục");
            // set the input to remember, which is empty so far
            oldText = this.Text;
            ToolStripMenuItem _item = new ToolStripMenuItem("Cấu hình danh mục");
            _item.Click += new EventHandler(_item_Click);
            ctx.Items.Add(_item);
            this.ContextMenuStrip = ctx;
        }


        void _item_Click(object sender, EventArgs e)
        {
            if (_OnShowData != null)
                _OnShowData();
           
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
        public void ResetEvents()
        {
            RaiseEvent = _RaiseEvent_old;
            RaiseEventEnter = RaiseEventEnter_old;
            RaiseEventEnterWhenEmpty = RaiseEventEnterWhenEmpty_old;
        }
        #endregion Constructors

        #region Properties
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
        public bool TakeCode
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
        public char splitChar
        {
            get;
            set;
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
        public string LOAI_NHANVIEN
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
                return listBox.SelectedIndex;
            }
            set
            {
                // musn't be null
                if (listBox.Items.Count != 0)
                { listBox.SelectedIndex = value; }
            }
        }

        public string Drug_ID { get; set; }
        // the actual list of currently displayed suggestions
        private List<string> CurrentAutoCompleteList
        {
            set;
            get;
        }
        /// <summary>
        /// SelectionChanged on ListBox
        /// </summary>
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
        public int MaxHeight
        {
            set;
            get;
        }

        
        #endregion Properties

        #region Methods
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
        public void SetId(object _Id)
        {
            try
            {
                MyID = _Id;
                var p = (from q in this.AutoCompleteList
                         where q.Contains(Utility.DoTrim(_Id.ToString()) + "#")
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
                    MyCode = lst2[0];
                    _Text = lst2[1];
                }

            }
            catch
            {
            }
        }
        //public void SetCode(string code)
        //{
        //    myCode = code;
        //    MyCode = myCode;
        //    DataRow[] arrDr = m_dtDanhmucChung.Select("MA_NHANVIEN='" + code + "'");
        //    if (arrDr.Length > 0)
        //        _Text = arrDr[0][VDmucNhanvien.Columns.TenNhanvien].ToString();
        //}
        DataTable m_dtDanhmucChung = null;
        public void Init()
        {
            try
            {
                m_dtDanhmucChung = Laydanhsachnhanvien( LOAI_NHANVIEN );
                if (m_dtDanhmucChung == null || m_dtDanhmucChung.Columns.Count <= 0) return;
             if (!m_dtDanhmucChung.Columns.Contains("ShortCut")) m_dtDanhmucChung.Columns.Add(new DataColumn("ShortCut", typeof(string)));
             foreach (DataRow dr in m_dtDanhmucChung.Rows)
             {
                 string shortcut = "";
                 string realName = dr[VDmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " + Utility.Bodau(dr[VDmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                 shortcut = dr[VDmucNhanvien.Columns.MaNhanvien].ToString().Trim();
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

             var source = new List<string>();
             var query = from p in m_dtDanhmucChung.AsEnumerable()
                         select Utility.sDbnull( p[VDmucNhanvien.Columns.IdNhanvien],"")+"#" + Utility.sDbnull( p[VDmucNhanvien.Columns.MaNhanvien],"") + "@" + Utility.sDbnull( p[VDmucNhanvien.Columns.TenNhanvien],"") + "@" + p.Field<string>("shortcut").ToString();

             source = query.ToList<string>();
             this.AutoCompleteList = source;
             this.TextAlign = HorizontalAlignment.Center;
             this.CaseSensitive = false;
             this.MinTypedCharacters = 1;
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
        }
        public void Init(string LOAI_NHANVIEN)
        {
            try
            {
                this.LOAI_NHANVIEN = LOAI_NHANVIEN;
                m_dtDanhmucChung = Laydanhsachnhanvien(LOAI_NHANVIEN);
                if (m_dtDanhmucChung == null || m_dtDanhmucChung.Columns.Count <= 0) return;
                if (!m_dtDanhmucChung.Columns.Contains("ShortCut")) m_dtDanhmucChung.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtDanhmucChung.Rows)
                {
                    string shortcut = "";
                    string realName = dr[VDmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " + Utility.Bodau(dr[VDmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    shortcut = dr[VDmucNhanvien.Columns.MaNhanvien].ToString().Trim();
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

                var source = new List<string>();
                var query = from p in m_dtDanhmucChung.AsEnumerable()
                            select Utility.sDbnull(p[VDmucNhanvien.Columns.IdNhanvien], "") + "#" + Utility.sDbnull(p[VDmucNhanvien.Columns.MaNhanvien], "") + "@" + Utility.sDbnull(p[VDmucNhanvien.Columns.TenNhanvien], "") + "@" + p.Field<string>("shortcut").ToString();

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                this.TextAlign = HorizontalAlignment.Center;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
        }
        public void Init(DataTable dtData)
        {
            try
            {
                if (dtData == null || dtData.Columns.Count <= 0) return;
                DataRow[] arrDr = dtData.Select("1=1", VDmucNhanvien.Columns.SttHthi);
                if (arrDr.Length <= 0)
                    return;

                m_dtDanhmucChung = arrDr.CopyToDataTable();
                if (m_dtDanhmucChung == null) return;
                if (!m_dtDanhmucChung.Columns.Contains("ShortCut")) m_dtDanhmucChung.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtDanhmucChung.Rows)
                {
                    string shortcut = "";
                    string realName = dr[VDmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " + Utility.Bodau(dr[VDmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    shortcut = dr[VDmucNhanvien.Columns.MaNhanvien].ToString().Trim();
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

                var source = new List<string>();
                var query = from p in m_dtDanhmucChung.AsEnumerable()
                            select Utility.sDbnull(p[VDmucNhanvien.Columns.IdNhanvien], "") + "#" + Utility.sDbnull(p[VDmucNhanvien.Columns.MaNhanvien], "") + "@" + Utility.sDbnull(p[VDmucNhanvien.Columns.TenNhanvien], "") + "@" + p.Field<string>("shortcut").ToString();

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                this.TextAlign = HorizontalAlignment.Center;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
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
                if (RaiseEventEnterWhenEmpty && _OnEnterMe!=null) _OnEnterMe();
                HideSuggestionListBox();
            }
            // if user presses key.up
            if ((args.KeyCode == Keys.Up || args.KeyCode == Keys.NumPad8))
            {
                // move the selection in listbox one up
                MoveSelectionInListBox((SelectedIndex - 1));
               
                // work is done
                args.Handled = true;
            }
            // on key.down
            else if ((args.KeyCode == Keys.Down || args.KeyCode == Keys.NumPad2))
            {
                //move one down
                MoveSelectionInListBox((SelectedIndex + 1));
                
                args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageUp))
            {
                //move 10 up
                MoveSelectionInListBox((SelectedIndex - 10));
                args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageDown))
            {
                //move 10 down
                MoveSelectionInListBox((SelectedIndex + 10));
                args.Handled = true;
            }
            // on enter
            else if ((args.KeyCode == Keys.Enter))
            {
                AllowChangedListBox = false;
                if (listBox.SelectedIndex != -1 && CurrentAutoCompleteList.Count > 0)//&& this.TopLevelControl.Controls.Contains(panel))
                    // select the item in the ListBox
                    SelectItem();
                listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                if (RaiseEventEnter && _OnEnterMe != null) _OnEnterMe();
                if (txtNext != null) txtNext.Focus();
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
                if (CurrentAutoCompleteList.Count != CurrentAutoCompleteList_IDAndCode.Count) return;
                
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

                    if (txtMyName != null)
                        txtMyName.Text = listBox.SelectedItem.ToString();
                    if (txtMyName_Edit != null)
                        txtMyName_Edit.Text = listBox.SelectedItem.ToString();
                    myCode = arrValues[1];
                    MyCode = myCode;
                    MyID = arrValues[0];
                }
                if (RaiseEvent && _OnSelectionChanged!=null) _OnSelectionChanged();
            }
            catch (Exception)
            {

                //throw;
            }


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
            //if (RaiseEventEnter && _OnEnterMe != null) _OnEnterMe();
            //AllowChangedListBox = true;
        }

        private void MoveSelectionInListBox(int Index)
        {
            // beginning of list
            if (Index <= -1)
            { this.SelectedIndex = listBox.Items.Count - 1; }
            else
                // end of liste
                if (Index > (listBox.Items.Count - 1))
                {
                    SelectedIndex = 0;// (listBox.Items.Count - 1);
                }
                else
                // somewhere in the middle
                { SelectedIndex = Index; }
        }

        // selects the current item
        private bool SelectItem()
        {
            try
            {
            // if the ListBox is not empty
            if (((this.listBox.Items.Count > 0) && (this.SelectedIndex > -1)))
            {
                //string[] arrValues = listBox.SelectedItem.ToString().Trim().Split('-');
                //// set the Text of the TextBox to the selected item of the ListBox
                //if (arrValues.Length > 1)
                //    this._Text = arrValues[1];//this.listBox.SelectedItem.ToString();
                //else
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

                    if (txtMyName != null)
                        txtMyName.Text = listBox.SelectedItem.ToString();
                    if (txtMyName_Edit != null)
                        txtMyName_Edit.Text = listBox.SelectedItem.ToString();
                    myCode = arrValues[1];
                    MyCode = myCode;
                    MyID = arrValues[0];

                    if (TakeCode)
                    {
                        if (arrValues.Length > 1)
                            this._Text = arrValues[1];
                    }
                    else
                        this._Text = this.listBox.SelectedItem.ToString();
                }
                this.Select(this.Text.Length, 0);
                SelectedIndex = 0;
               
                // and hide the ListBox
                this.HideSuggestionListBox();
            }
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
                    UpdateCurrentAutoCompleteList();
                }
                // user is typing backward - char bas been removed
                else if ((this.oldText.Length > 0) && (this.Text == this.oldText.Substring(0, this.oldText.Length - 1)))
                {
                    //handle backward typing with refresh
                    UpdateCurrentAutoCompleteList();
                }
                // something within has changed
                else
                {
                    // handle other things like corrections in the middle of the input, clipboard pastes, etc. with refresh
                    UpdateCurrentAutoCompleteList();
                }

                if (((CurrentAutoCompleteList != null) && CurrentAutoCompleteList.Count > 0))
                {
                    // finally show Panel and ListBox
                    // (but after refresh to prevent drawing empty rectangles)
                    panel.Show();
                    // at the top of all controls
                    panel.BringToFront();
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
                setDefaultValue();
                this.HideSuggestionListBox();
            }
            if (Utility.DoTrim(this.Text) == "" && RaiseEventEnterWhenEmpty && _OnEnterMe != null) _OnEnterMe();
        }
        public void setDefaultValue()
        {
            if (txtMyID != null)
                txtMyID.Text = DefaultID;
            if (txtMyID_Edit != null)
                txtMyID_Edit.Text = DefaultID;
            if (txtMyCode != null)
                txtMyCode.Text = DefaultCode;
            if (txtMyCode_Edit != null)
                txtMyCode_Edit.Text = DefaultCode;
            myCode = DefaultCode;
            MyCode = myCode;
            MyID = DefaultID;
        }
        // This is a timecritical part
        // Fills/ refreshed the CurrentAutoCompleteList with appropreate candidates
        private void UpdateCurrentAutoCompleteList()
        {
            // the list of suggestions has to be refreshed so clear it
            CurrentAutoCompleteList.Clear();
            CurrentAutoCompleteList_IDAndCode.Clear();
            string myText = Utility.DoTrim(this.Text);
            if (myText == "") myText = " ";
            if (AutoCompleteList == null) return;
            // an find appropreate candidates for the new CurrentAutoCompleteList in AutoCompleteList
            foreach (string Str in AutoCompleteList)
            {
                string _value = Str;
                if (CompareNoID && Str.IndexOf("#") >= 0) _value = Str.Split('#')[1];
                string[] arrvalues = Str.Split(splitChar);
                if (arrvalues.Length > 1)
                {
                    // be casesensitive
                    if (CaseSensitive)
                    {
                        // search for the substring (equal to SQL Like Command)
                        if ((_value.IndexOf(myText) > -1))
                        // Add candidates to new CurrentAutoCompleteList
                        {
                            CurrentAutoCompleteList.Add(arrvalues[1]);
                            CurrentAutoCompleteList_IDAndCode.Add(arrvalues[0]);
                        }
                    }
                    // or ignore case
                    else
                    {
                        // and search for the substring (equal to SQL Like Command)
                        if ((_value.ToLower().IndexOf(myText.ToLower()) > -1))
                        // Add candidates to new CurrentAutoCompleteList
                        {
                            CurrentAutoCompleteList.Add(arrvalues[1]);
                            CurrentAutoCompleteList_IDAndCode.Add(arrvalues[0]);
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
            if (CurrentAutoCompleteList.Count <= 0)
                setDefaultValue();
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
                    // get its width
                    panel.Width = this.Width + ExtraWidth;

                    panel.Location = FindLocation(this) + new Size(0, this.Height); //this.Location + new Size(0, this.Height);
                    // calculate the remeining height beneath the TextBox
                    panel.Height = GetMyHeigh();// ;
                    // and the Location to use
                    
                    // Panel and ListBox have to be added to TopLevelControl.Controls before calling BingingContext
                    if (!this.TopLevelControl.Controls.Contains(panel))
                    {
                        // add the Panel and ListBox to the PartenForm
                        this.TopLevelControl.Controls.Add(panel);
                    }
                    // Update the listBox manually - List<string> dosn't support change events
                    // this is the DataSource approche, this is a bit tricky and may cause conflicts,
                    // so in case fall back to AddRange approache (see Excursus)
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
            return locationOnForm;
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
            if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight || MaxHeight > this.TopLevelControl.ClientSize.Height - this.Height - panel.Location.Y)
                return this.TopLevelControl.ClientSize.Height - this.Height - panel.Location.Y;
            if (MaxHeight != -1)
            {
                if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight) return MaxHeight;
                else
                    return (CurrentAutoCompleteList.Count + 1) * 15;
            }
            else
            {
                return this.TopLevelControl.ClientSize.Height - this.Height - panel.Location.Y;
            }
           
            return listBox.Height;
        }
        public static DataTable Laydanhsachnhanvien(string ma_loainv)
        {
            try
            {
                DataTable dtData = new DataTable();

                dtData = SPs.DmucLaydanhsachNhanvien(ma_loainv).GetDataSet().Tables[0];
                return dtData;
            }
            catch (Exception ex)
            {

                return null;
            }
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