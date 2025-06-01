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
    // and is therfore called TextBox, but ment is this class (AutoCompleteTextbox_Danhmucchung)
    public class AutoCompleteTextbox_Danhmucchung : TextBox
    {
        #region Fields
        List<string> lstIdCodeName = new List<string>();
        // the ListBox used for suggestions
        private ListBox listBox;
        ToolStripMenuItem _item = new ToolStripMenuItem("Cấu hình danh mục");
        ToolStripMenuItem _itemSaveas = new ToolStripMenuItem("Lưu thành");
        ToolStripMenuItem _itemRefresh = new ToolStripMenuItem("Refresh");
        public delegate void OnSelectionChanged();
        public event OnSelectionChanged _OnSelectionChanged;

        public delegate void OnEnterMe();
        public event OnEnterMe _OnEnterMe;
        public delegate void OnEnterKey(AutoCompleteTextbox_Danhmucchung obj);
        public event OnEnterKey _OnEnterKey;
        public delegate void OnShowData();
        public event OnShowData _OnShowData;
        public delegate void OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj);
        public event OnShowDataV1 _OnShowDataV1;

        public delegate void OnSaveAs();
        public event OnSaveAs _OnSaveAs;

        public delegate void OnSaveAsV1(AutoCompleteTextbox_Danhmucchung obj);
        public event OnSaveAsV1 _OnSaveAsV1;

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
        public Button cmdDropDown { get; set; }
        public Control txtNext1 { get; set; }
        public object MyID { get; set; }
        public string MyCode { get; set; }
        public string myCode = "";
        public bool VisibleDefaultItem = true;
        private System.ComponentModel.IContainer components;
        // a Panel for displaying
        private Panel panel;
        ToolTip _ToolTip = new ToolTip();
        #endregion Fields

        #region Constructors

        // the constructor
        public AutoCompleteTextbox_Danhmucchung()
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
            this.AddValues = true;
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
           
            _item.Click += new EventHandler(_item_Click);
            _itemSaveas.Click += new EventHandler(_itemSaveas_Click);
            _itemRefresh.Click += _itemRefresh_Click;
            
           
        }

        void _itemRefresh_Click(object sender, EventArgs e)
        {
            Init();
        }

        void _itemSaveas_Click(object sender, EventArgs e)
        {
            if (_OnSaveAs != null)
                _OnSaveAs();

            if (_OnSaveAsV1 != null)
                _OnSaveAsV1(this);
        }


        void _item_Click(object sender, EventArgs e)
        {
            if (_OnShowData != null)
                _OnShowData();
            if (_OnShowDataV1 != null)
                _OnShowDataV1(this);
           
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

        public bool buildShortcut
        {
            get;
            set;
        }
        public bool AllowMultiline
        {
            get;
            set;
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
       
        public bool ShowCodeWithValue
        {
            get;
            set;
        }
        /// <summary>
        /// Hiển thị menu save as
        /// </summary>
        public bool AddValues
        {
            get;
            set;
        }
        public bool AllowEdit
        {
            set {
                _item.Visible = value;
                _itemSaveas.Visible = value;
            }

        }
        public bool TakeCode
        {
            get;
            set;
        }
        //public bool VisibleDefaultItem
        //{
        //    get;
        //    set;
        //}
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
        public string LOAI_DANHMUC
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
        public HorizontalAlignment _TextAlign
        {
            get { return this.TextAlign; }
            set { this.TextAlign = value; }
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
        public void SetDefault()
        {
            myCode = defaultItem;
            MyCode = myCode;
            if (dtData == null) return;
            DataRow[] arrDr = dtData.Select("MA='" + defaultItem + "'");
            if (arrDr.Length > 0)
                _Text = arrDr[0]["TEN"].ToString();
            else
            {
                this._Text = "";
                setDefaultValue();
            }

        }
        public void SetCode(string code)
        {
            try
            {
                myCode = code;
                MyCode = myCode;
                if (dtData == null) return;
                DataRow[] arrDr = dtData.Select("MA='" + code + "'");
                if (arrDr.Length > 0)
                    _Text = arrDr[0]["TEN"].ToString();
                else
                {
                    this._Text = "";
                    setDefaultValue();
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
           

        }
        string defaultItem = "";
        public void Init()
        {
            try
            {

                bool visible = globalVariables.IsAdmin;
                if (!visible) visible = globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_themdanhmucdungchung");
                if (!AddValues) visible = false;
                AllowEdit = visible;
                dtData = LayDulieuDanhmucChung(new List<string>() { LOAI_DANHMUC });
                if (dtData == null || dtData.Columns.Count <= 0) return;
                if (!dtData.Columns.Contains("ShortCut")) dtData.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                dtData.DefaultView.Sort = DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten;
                if (dtData.DefaultView.Count == 1)
                    defaultItem = dtData.DefaultView[0]["MA"].ToString().Trim();
                else
                    defaultItem = (from p in dtData.AsEnumerable()
                                   where Utility.Byte2Bool(p[DmucChung.Columns.TrangthaiMacdinh])
                                   select Utility.sDbnull(p["MA"],"")).FirstOrDefault();
                if (buildShortcut)
                {
                    foreach (DataRowView dr in dtData.DefaultView)
                    {
                        if (Utility.Byte2Bool(dr[DmucChung.Columns.TrangthaiMacdinh]))
                            defaultItem = dr["MA"].ToString().Trim();
                        string shortcut = "";
                        string realName = dr["TEN"].ToString().Trim() + " " + Utility.Bodau(dr["TEN"].ToString().Trim());
                        shortcut = dr["MA"].ToString().Trim();
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
                var source = new List<string>();
                var query = from p in dtData.AsEnumerable()
                            select "-1#" + p.Field<string>("MA").ToString() + "@" + p.Field<string>("TEN").ToString() + "@" +  Utility.sDbnull( p.Field<string>("shortcut"),"").ToString();

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                //this.TextAlign = HorizontalAlignment.Center;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
                if (!VisibleDefaultItem) return;
                SetCode(defaultItem);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
            finally
            {
                if (Utility.CoquyenThemnhanhDmucChung(this.LOAI_DANHMUC) || globalVariables.IsAdmin)
                {
                    ctx.Items.Add(_item);
                    ctx.Items.Add(_itemSaveas);
                    ctx.Items.Add(_itemRefresh);
                    this.ContextMenuStrip = ctx;
                }
            }
        }
        public void RefreshGroup(string groupCode)
        {
            try
            {
                DataTable dtDataNew = dtData.Clone();
                if (groupCode == "-1") dtDataNew = dtData.Copy();
                if (groupCode != "-1")
                {
                    DataRow[] arrDr = dtData.Select("PHANLOAI='" + groupCode + "'");
                    if (arrDr.Length > 0)
                    {
                        dtDataNew = arrDr.CopyToDataTable();

                    }
                }
                InitbyGroup(dtDataNew);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        public void Init(List<string> lstValues)
        {
            try
            {
              
                var source = new List<string>();
                var query = from p in lstValues
                            select "-1#" + p + "@" + p + "@" + "SC";

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                //this.TextAlign = HorizontalAlignment.Center;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
                SetCode(defaultItem);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
            finally
            {
                if (Utility.CoquyenThemnhanhDmucChung(this.LOAI_DANHMUC) || globalVariables.IsAdmin)
                {
                    ctx.Items.Add(_item);
                    ctx.Items.Add(_itemSaveas);
                    ctx.Items.Add(_itemRefresh);
                    this.ContextMenuStrip = ctx;
                }
            }
        }
        public void Init(string IncludeList)
        {
            try
            {
                bool _Visible = globalVariables.IsAdmin;
                if (!_Visible)
                    _Visible = globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_themdanhmucdungchung");
                AllowEdit = _Visible;
                IncludeList =","+ IncludeList + ",";
                dtData = LayDulieuDanhmucChung(new List<string>() { LOAI_DANHMUC });
                DataTable tempt = dtData.Clone();
                foreach (DataRow dr in dtData.Rows)
                {
                    if (IncludeList.Contains("ALL") || IncludeList.Contains("," + Utility.sDbnull(dr["MA"]) + ","))
                        tempt.ImportRow(dr);
                }
                dtData = tempt.Copy();
                if (dtData == null || dtData.Columns.Count <= 0) return;
                if (!dtData.Columns.Contains("ShortCut")) dtData.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                dtData.DefaultView.Sort = DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten;
                 if (dtData.DefaultView.Count == 1)
                    defaultItem = dtData.DefaultView[0]["MA"].ToString().Trim();
                else
                    defaultItem = (from p in dtData.AsEnumerable()
                                   where Utility.Byte2Bool(p[DmucChung.Columns.TrangthaiMacdinh])
                                   select Utility.sDbnull(p["MA"],"")).FirstOrDefault();
                 if (buildShortcut)
                 {
                     foreach (DataRowView dr in dtData.DefaultView)
                     {
                         if (Utility.Byte2Bool(dr[DmucChung.Columns.TrangthaiMacdinh]))
                             defaultItem = dr["MA"].ToString().Trim();
                         string shortcut = "";
                         string realName = dr["TEN"].ToString().Trim() + " " + Utility.Bodau(dr["TEN"].ToString().Trim());
                         shortcut = dr["MA"].ToString().Trim();
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
                var source = new List<string>();
                var query = from p in dtData.AsEnumerable()
                           
                            select "-1#" + p.Field<string>("MA").ToString() + "@" + p.Field<string>("TEN").ToString() + "@" + Utility.sDbnull( p.Field<string>("shortcut"),"").ToString();

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                //this.TextAlign = HorizontalAlignment.Center;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
                SetCode(defaultItem);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
            finally
            {
                if (Utility.CoquyenThemnhanhDmucChung(this.LOAI_DANHMUC) || globalVariables.IsAdmin)
                {
                    ctx.Items.Add(_item);
                    ctx.Items.Add(_itemSaveas);
                    ctx.Items.Add(_itemRefresh);
                    this.ContextMenuStrip = ctx;
                }
            }
        }
        public void SetDefaultItem()
        {
            SetCode(defaultItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="lstIdCodeName">new List<string>(){"ID","CODE","NAME"}</string>Điền tên 3 cột đại diện cho ID, mã và tên vào danh sách này</param>
        public void Init(DataTable dtData, List<string> lstIdCodeName)
        {
            try
            {
                this.lstIdCodeName = lstIdCodeName;
                this.dtData = dtData;
                if (dtData == null) return;
                if (!dtData.Columns.Contains("ShortCut"))
                    dtData.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                 if (dtData.DefaultView.Count == 1)
                    defaultItem = dtData.DefaultView[0]["MA"].ToString().Trim();
                else
                    defaultItem = (from p in dtData.AsEnumerable()
                                   where Utility.Byte2Bool(p[DmucChung.Columns.TrangthaiMacdinh])
                                   select Utility.sDbnull(p["MA"],"")).FirstOrDefault();
                 if (buildShortcut)
                 {
                     foreach (DataRowView dr in dtData.DefaultView)
                     {
                         string shortcut = "";
                         string realName = dr[lstIdCodeName[2]].ToString().Trim() + " " +
                                           Utility.Bodau(dr[lstIdCodeName[2]].ToString().Trim());
                         shortcut = dr[lstIdCodeName[1]].ToString().Trim();
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
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in dtData.AsEnumerable()
                            select Utility.sDbnull(p[lstIdCodeName[0]], "") + "#" + Utility.sDbnull(p[lstIdCodeName[1]], "") + "@" + Utility.sDbnull(p[lstIdCodeName[2]], "") + "@" + Utility.sDbnull(p["shortcut"], "");
                source = query.ToList();
                this.AutoCompleteList = source;
                this.TextAlign = _TextAlign;
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
                if (Utility.CoquyenThemnhanhDmucChung(this.LOAI_DANHMUC) || globalVariables.IsAdmin)
                {
                    ctx.Items.Add(_item);
                    ctx.Items.Add(_itemSaveas);
                    ctx.Items.Add(_itemRefresh);
                    this.ContextMenuStrip = ctx;
                }

            }
        }
        public void InitbyGroup(DataTable dt)
        {
            try
            {
                bool _Visible = globalVariables.IsAdmin;
                if (!_Visible)
                    _Visible = globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_themdanhmucdungchung");
                AllowEdit = _Visible;

                string defaultItem = "";
                if (dt == null || dt.Columns.Count <= 0) return;
                DataTable v_dtData = dt.Clone();
                DataRow[] arrDr = dt.Select("LOAI='" + LOAI_DANHMUC + "'", DmucChung.Columns.SttHthi);
                if (arrDr.Length <= 0)
                    return;

                v_dtData = arrDr.CopyToDataTable();
                if (v_dtData == null) return;
                if (!v_dtData.Columns.Contains("ShortCut")) v_dtData.Columns.Add(new DataColumn("ShortCut", typeof(string)));

                v_dtData.DefaultView.Sort = DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten;
                if (v_dtData.DefaultView.Count == 1)
                    defaultItem = v_dtData.DefaultView[0]["MA"].ToString().Trim();
                else
                    defaultItem = (from p in v_dtData.AsEnumerable()
                                   where Utility.Byte2Bool(p[DmucChung.Columns.TrangthaiMacdinh])
                                   select Utility.sDbnull(p["MA"], "")).FirstOrDefault();
                if (buildShortcut)
                {
                    foreach (DataRowView dr in v_dtData.DefaultView)
                    {
                        if (Utility.Byte2Bool(dr[DmucChung.Columns.TrangthaiMacdinh]))
                            defaultItem = dr["MA"].ToString().Trim();
                        string shortcut = "";
                        string realName = dr["TEN"].ToString().Trim() + " " + Utility.Bodau(dr["TEN"].ToString().Trim());
                        shortcut = dr["MA"].ToString().Trim();
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
                var source = new List<string>();
                var query = from p in v_dtData.AsEnumerable()
                            select "-1#" + p.Field<string>("MA").ToString() + "@" + p.Field<string>("TEN").ToString() + "@" + Utility.sDbnull(p.Field<string>("shortcut"), "").ToString();

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                //this.TextAlign = HorizontalAlignment.Center;
                SetCode(defaultItem);
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
            finally
            {
                if (Utility.CoquyenThemnhanhDmucChung(this.LOAI_DANHMUC) || globalVariables.IsAdmin)
                {
                    ctx.Items.Add(_item);
                    ctx.Items.Add(_itemSaveas);
                    ctx.Items.Add(_itemRefresh);
                    this.ContextMenuStrip = ctx;
                }
            }
        }
        public void Init(DataTable dt)
        {
            try
            {
                bool _Visible = globalVariables.IsAdmin;
                if (!_Visible)
                    _Visible = globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_themdanhmucdungchung");
                AllowEdit = _Visible;

                string defaultItem = "";
                if (dt == null || dt.Columns.Count <= 0) return;
                dtData = dt.Clone();
                DataRow[] arrDr = dt.Select("LOAI='" + LOAI_DANHMUC + "'", DmucChung.Columns.SttHthi);
                if (arrDr.Length <= 0)
                    return;

                dtData = arrDr.CopyToDataTable();
                if (dtData == null) return;
                if (!dtData.Columns.Contains("ShortCut")) dtData.Columns.Add(new DataColumn("ShortCut", typeof(string)));

                dtData.DefaultView.Sort = DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten;
                if (dtData.DefaultView.Count == 1)
                    defaultItem = dtData.DefaultView[0]["MA"].ToString().Trim();
                else
                    defaultItem = (from p in dtData.AsEnumerable()
                                   where Utility.Byte2Bool(p[DmucChung.Columns.TrangthaiMacdinh])
                                   select Utility.sDbnull(p["MA"],"")).FirstOrDefault();
                if (buildShortcut)
                {
                    foreach (DataRowView dr in dtData.DefaultView)
                    {
                        if (Utility.Byte2Bool(dr[DmucChung.Columns.TrangthaiMacdinh]))
                            defaultItem = dr["MA"].ToString().Trim();
                        string shortcut = "";
                        string realName = dr["TEN"].ToString().Trim() + " " + Utility.Bodau(dr["TEN"].ToString().Trim());
                        shortcut = dr["MA"].ToString().Trim();
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
                var source = new List<string>();
                var query = from p in dtData.AsEnumerable()
                            select "-1#" + p.Field<string>("MA").ToString() + "@" + p.Field<string>("TEN").ToString() + "@" + Utility.sDbnull( p.Field<string>("shortcut"),"").ToString();

                source = query.ToList<string>();
                this.AutoCompleteList = source;
                //this.TextAlign = HorizontalAlignment.Center;
                SetCode(defaultItem);
                this.CaseSensitive = false;
                this.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Autocomplete cho danh mục dùng chung\n" + ex.Message);
            }
            finally
            {
                if (Utility.CoquyenThemnhanhDmucChung(this.LOAI_DANHMUC) || globalVariables.IsAdmin)
                {
                    ctx.Items.Add(_item);
                    ctx.Items.Add(_itemSaveas);
                    ctx.Items.Add(_itemRefresh);
                    this.ContextMenuStrip = ctx;
                }
            }
        }
        public static DataTable LayDulieuDanhmucChung(List<string> lstLoai, DataTable dtData)
        {
            DataTable m_NN = new DataTable();
            //if(globalVariables.IsAdmin)
            //{
            m_NN = dtData.Clone();
            var p = from q in dtData.AsEnumerable()
                    where lstLoai.Contains(q["LOAI"].ToString()) && q["trang_thai"].ToString()=="1"
                    select q;
            if (p.Any())
                m_NN=p.CopyToDataTable();
            return m_NN;
        }
        public static DataTable LayDulieuDanhmucChung(List<string> lstLoai)
        {
            DataTable m_NN = new DataTable();
            //if(globalVariables.IsAdmin)
            //{
                m_NN =
                new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).In(lstLoai)
                    .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                    .OrderAsc(DmucChung.Columns.SttHthi)
                    .ExecuteDataSet().Tables[0];
            //}
            //else
            //{
            //    m_NN =
            //   new Select().From(DmucChung.Schema)
            //       .Where(DmucChung.Columns.Loai).In(lstLoai)
            //       .And(DmucChung.Columns.NguoiTao).IsEqualTo(globalVariables.UserName)
            //       .Or(DmucChung.Columns.Loai).In(lstLoai)
            //       .And(DmucChung.Columns.NguoiTao).IsEqualTo("ADMIN")
            //       .OrderAsc(DmucChung.Columns.SttHthi)
            //       .ExecuteDataSet().Tables[0];
            //}
            return m_NN;
        }
        public bool isShow = false;
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
                    this.Focus();
                }
                //SelectedIndex = -1;
            }
        }
        public void RaiseEnterEvents()
        {
            if ( _OnEnterMe != null) _OnEnterMe();
        }
        protected override void OnKeyDown(KeyEventArgs args)
        {

             if ((args.Control && args.KeyCode == Keys.D))
            {
                this._Text = "";
                setDefaultValue();
                if (RaiseEventEnterWhenEmpty && _OnEnterMe!=null) _OnEnterMe();
                HideSuggestionListBox();
            }
            // if user presses key.up
            else if ((args.KeyCode == Keys.Up || args.KeyCode == Keys.NumPad8))
            {
                // move the selection in listbox one up
                MoveSelectionInListBox((SelectedIndex - 1));
               
                // work is done
                //if (!Multiline) args.Handled = true;
            }
            // on key.down
            else if ((args.KeyCode == Keys.Down || args.KeyCode == Keys.NumPad2))
            {
                //move one down
                MoveSelectionInListBox((SelectedIndex + 1));

               // if (!Multiline) args.Handled = true;
            }
            else if ((args.KeyCode == Keys.F1 || (args.Control && args.KeyCode==Keys.H)))
            {
                Utility.ShowMsg(LOAI_DANHMUC);
            }
            else if ((args.KeyCode == Keys.PageUp))
            {
                //move 10 up
                MoveSelectionInListBox((SelectedIndex - 10));
                //if (!Multiline) args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageDown))
            {
                //move 10 down
                MoveSelectionInListBox((SelectedIndex + 10));
               // if (!Multiline) args.Handled = true;
            }
            // on enter
            else if ((args.KeyCode == Keys.Enter))
            {
                AllowChangedListBox = false;
                if (listBox.SelectedIndex != -1 && CurrentAutoCompleteList.Count > 0)
                    // select the item in the ListBox
                    SelectItem();
                listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                //if (RaiseEventEnter && _OnEnterMe != null ) _OnEnterMe();
                if (RaiseEventEnter && _OnEnterMe != null && !Multiline) _OnEnterMe();
                if (!Multiline)  this.SelectionStart = 0;
                
                //if (Multiline)
                //{
                //    //SendKeys.Send("{TAB}");
                //    args.SuppressKeyPress = true;
                //}
                //else
                //{
                //}
                if (_OnEnterKey != null)
                {
                    _OnEnterKey(this);
                }
                if (txtNext != null && txtNext.Enabled) txtNext.Focus();
                else if (txtNext1 != null && txtNext1.Enabled) txtNext1.Focus();
                //if (!Multiline) 
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
                   // if (!shownbyClick)
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
                    MyID = arrValues[0];
                    myCode = arrValues[1];
                    MyCode = myCode;
                }
                if (RaiseEvent && _OnSelectionChanged!=null) _OnSelectionChanged();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
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
            //listBox_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            //AllowChangedListBox = false;
            //SelectItem();
            //SelectionStart = 0;
            //this.Focus();
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
                    MyID = arrValues[0];
                    myCode = arrValues[1];
                    MyCode = myCode;

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
                    isShow = true;
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
            if (RaiseEventEnterWhenEmpty && _OnSelectionChanged != null) _OnSelectionChanged();
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
            if (AutoCompleteList == null) return;
            //if (myText == "") myText = " ";
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
                    panel.Width = this.Width +Math.Abs( ExtraWidth);

                    panel.Location = FindLocation(this) + new Size(ExtraWidth<0?ExtraWidth:0, this.Height); //this.Location + new Size(0, this.Height);
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
            if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight && MaxHeight > this.TopLevelControl.ClientSize.Height - this.Height - this.Location.Y)// if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight || MaxHeight > this.TopLevelControl.ClientSize.Height - this.Height - panel.Location.Y)
                return this.TopLevelControl.ClientSize.Height - this.Height - panel.Location.Y;// panel.Location.Y;
            if (MaxHeight != -1)
            {
                if ((CurrentAutoCompleteList.Count + 1) * 15 >= MaxHeight) return MaxHeight;
                else
                    return (CurrentAutoCompleteList.Count + 1) * 15;
            }
            else
            {
                return this.TopLevelControl.ClientSize.Height - this.Height - panel.Location.Y; //-panel.Location.Y;
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