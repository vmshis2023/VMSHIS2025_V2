using System;
using System.Collections;           // if you would like to use ArrayList insted
using System.Collections.Generic;   // here we use Generic Type List<string>
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using VNS.Libs;
namespace VNS.HIS.UCs
{
   

    // the component is derived from a TextBox 
    // and is therfore called TextBox, but ment is this class (AutoCompleteTextbox_Diachi)
    public class AutoCompleteTextbox_Diachi : TextBox
    {
        #region Fields

        // the ListBox used for suggestions
        private ListBox listBox;
        public Label lblWords;
        public Label lblTest;
        public delegate void OnSelectionChanged();
        public event OnSelectionChanged _OnSelectionChanged;
        public bool PreventTextChanged = false;
        public delegate void OnSelected(string value);
        public event OnSelected _OnSelected;
        // string to remember a former input
        private string oldText;
        bool JustFinished = false;
        public DataTable dtData = new DataTable();
        public bool AllowTextChanged = true;
        public Janus.Windows.GridEX.EditControls.EditBox txtMyID_Edit { get; set; }
        public Janus.Windows.GridEX.EditControls.EditBox txtMyCode_Edit { get; set; }
        public Janus.Windows.GridEX.EditControls.EditBox txtMyName_Edit { get; set; }
        public TextBox txtMyID { get; set; }
        public TextBox txtMyCode { get; set; }
        public TextBox txtMyName { get; set; }
        public object currentID = null;
        public Control txtNext { get; set; }
        public int LengthOfQuickType { get; set; }
        // a Panel for displaying
        private Panel panel;

        #endregion Fields

        #region Constructors

        // the constructor
        public AutoCompleteTextbox_Diachi()
            : base()
        {
            // assigning some default values
            // minimum characters to be typed before suggestions are displayed
            this.MinTypedCharacters = 2;
            MaxHeight = -1;
            RaiseEvent = false;
            ExtraWidth = 0;
            splitChar = '@';
            splitCharIDAndCode = '#';
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

            // set the input to remember, which is empty so far
            oldText = this.Text;
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
        public int ExtraWidth
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
        public bool FillValueAfterSelect
        {
            set;
            get;
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
        // minimum characters to be typed before suggestions are displayed
        public int MinTypedCharacters
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
        public bool RaiseEvent
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

        // the parent Form of this control
        //private Form ParentForm
        //{
        //    get { return this.Parent.FindForm(); }
        //}

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
        // hides the listbox
        public void HideSuggestionListBox()
        {
            if ((TopLevelControl != null))
            {
                // hiding the panel also hides the listbox
                panel.Hide();
                // now remove it from the ParentForm (Form1 in this example)
                if (this.TopLevelControl.Controls.Contains(panel))
                {
                    this.TopLevelControl.Controls.Remove(panel);
                }
                //SelectedIndex = -1;
            }
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {

            if (lblTest != null) lblTest.Text = String.Format("{0}{1}", listBox.SelectedIndex.ToString(), CurrentAutoCompleteList.Count.ToString());
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
                if (listBox.SelectedIndex != -1 && CurrentAutoCompleteList.Count > 0)
                    // select the item in the ListBox
                    SelectItem();
                listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                //if (RaiseEventEnter) _OnEnterMe();
                if (txtNext != null) txtNext.Focus();
                args.Handled = true;

            }
            else
            {
                // work is not done, maybe the base class will process the event, so call it...
                base.OnKeyDown(args);
            }
        }
        string CurrentWord = "";
        int start = -1;
        int currPos = -1;
        string stext = "";
        void findtheword(string text, int currPos)
        {
            try
            {
                if (text.TrimStart().TrimEnd().Length == 0)
                {
                    int a;
               }
                this.currPos = currPos;
                CurrentWord = text;
                stext = text;
                if (text.TrimStart().TrimEnd().Length <= 0) CurrentWord = "";
                start = -1;
                for (int i = currPos; i >= 1; i--)
                {
                    if (text.Substring(i - 1, 1).Trim() != "" && text.Substring(i - 1, 1).Trim() != ",")
                    {
                        start = i - 1;
                        continue;
                    }
                    else if (text.Substring(i - 1, 1).Trim() == "" || text.Substring(i - 1, 1).Trim() == ",")
                    {
                        if (currPos - (i - 1) == 1) //Vừa nhập dấu cách hoặc dấu ,
                            CurrentWord = "";
                        else
                            break;//Kết thúc để lấy từ nhập gần nhất
                    }
                }
                if (start < 0) start = 0;
                CurrentWord = text.Substring(start, currPos - start);

            }
            catch (Exception)
            {

            }
            finally
            {
               if(lblWords!=null) lblWords.Text = String.Format("{0} -{1} -{2}", oldText, this.Text, CurrentWord);
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
        List<string> lstwords = new List<string>();
        // if the input changes, call ShowSuggests()
        protected override void OnTextChanged(EventArgs args)
        {
            //if (JustFinished) goto finish;
            // avoids crashing the designer
            if (!PreventTextChanged)
            {
                if (!this.DesignMode)
                {
                    findtheword(this.Text, this.SelectionStart);
                    // lblWords.Text = String.Format("{0} -{1} ", this.SelectionStart.ToString(), CurrentWord);
                    ShowSuggests();
                }
                base.OnTextChanged(args);
                // remember input
                oldText = this.Text;
            }
            PreventTextChanged = false;
            // finish:
            //JustFinished = false;
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
                if (RaiseEvent) _OnSelectionChanged();
                if (AllowChangedListBox && (CurrentAutoCompleteList.Count <= 0 || listBox.SelectedItem == null))
                {
                    txtMyID.Text = "";
                    txtMyID_Edit.Text = "";
                    txtMyCode.Text = "";
                    txtMyCode_Edit.Text = "";
                    txtMyName.Clear();
                    txtMyName_Edit.Clear();
                    return;
                }
                if (CurrentAutoCompleteList.Count != CurrentAutoCompleteList_IDAndCode.Count) return;

                string[] arrValues = CurrentAutoCompleteList[listBox.SelectedIndex].ToString().Trim().Split(splitCharIDAndCode);
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
                }
                if (RaiseEvent) _OnSelectionChanged();
            }
            catch (Exception)
            {

                //throw;
            }
            finally
            {
                if (lblTest != null) lblTest.Text = String.Format("{0}{1}", listBox.SelectedIndex.ToString(), CurrentAutoCompleteList.Count.ToString());
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
            //if (RaiseEventEnter && _OnEnterMe != null) _OnEnterMe();
            SelectionStart = 0;
            this.Focus();
            AllowChangedListBox = true;
        }

        // event for DoubleClick in the ListBox
        private void listBox_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //listBox_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            // select the current item
            //SelectItem();
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
                //Chia dữ liệu thành 3 phần, previous+ current word+ next

                lstwords.Clear();
                lstwords.Add(stext.Substring(0, this.start));
                lstwords.Add("");//Chờ chọn từ AutoComplete
                lstwords.Add(stext.Substring(currPos));
                if (_OnSelected != null && CurrentAutoCompleteList_IDAndCode.Count > 0 && listBox.SelectedIndex <= CurrentAutoCompleteList_IDAndCode.Count-1) _OnSelected(CurrentAutoCompleteList_IDAndCode[listBox.SelectedIndex]);
                this._Text = String.Format("{0}{1}{2}", lstwords[0], this.listBox.SelectedItem.ToString(), lstwords[2]);// this.listBox.SelectedItem.ToString();
                //Test

                if (lblTest != null) lblTest.Text = String.Format("{0}{1}{2}", lstwords[0], this.listBox.SelectedItem.ToString(), lstwords[2]);

                this.Select(this.Text.Length , 0);
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
                JustFinished = true;
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
            }
        }

        // shows the suggestions in ListBox beneath the TextBox
        // and fitting it into the ParentForm
        private void ShowSuggests()
        {
            if (!AllowTextChanged) return;
            if (lblWords != null) lblWords.Text = String.Format("{0} -{1} -{2}", oldText, this.Text, CurrentWord);
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
            }
            // hide if typed chars <= MinCharsTyped
            else
            {
                AllowChangedListBox = false;
               listBox.SelectedIndex = -1;
                AllowChangedListBox = true;
                CurrentAutoCompleteList.Clear();
                //setDefaultValue();
                this.HideSuggestionListBox();
            }
        }
        private void buildData(string TenTp)
        {
            if (Utility.DoTrim(TenTp) != "")
                TenTp = TenTp.TrimStart();//Tránh trường hợp nhập " hn" hệ thống không tìm được dữ liệu
            TenTp = TenTp.Replace("'", "");
            string _rowFilter = "1=1";
            if (TenTp == "")// || TenTp.Length > 6)
            {
                CurrentAutoCompleteList.Clear();
                CurrentAutoCompleteList_IDAndCode.Clear();
                return;
            }
            if (TenTp != "" && TenTp.Trim() == "") TenTp = " ";
            dtData.DefaultView.RowFilter = "1=1";
            //Tạm khóa lại
            //if (TenTp.Trim() != "")
            //{
            //    if (TenTp.Length == 1)
            //    {
            //        _rowFilter = " ShortcutTP like '%" + TenTp + "%' AND ShortcutXP='kx' AND ShortcutQH='kx'";
            //    }
            //    if (TenTp.Length == 2)
            //    {
            //        _rowFilter = " ShortcutTP = '" + TenTp + "' AND ShortcutXP='kx' AND ShortcutQH='kx'";
            //    }
            //    if (TenTp.Length == 3)
            //    {
            //        _rowFilter = " ShortcutTP like '%" + TenTp.Substring(2, 1) +
            //                     "%' AND ShortcutXP='kx' AND ShortcutQH = '" + TenTp.Substring(0, 2) + "'";
            //    }
            //    if (TenTp.Length == 4)
            //    {
            //        _rowFilter = " ShortcutTP = '" + TenTp.Substring(2, 2) + "' AND ShortcutXP='kx' AND ShortcutQH = '" +
            //                     TenTp.Substring(0, 2) + "'";
            //    }
            //    if (TenTp.Length == 5)
            //    {
            //        _rowFilter = " ShortcutTP like '%" + TenTp.Substring(4, 1) + "%' AND ShortcutXP ='" +
            //                     TenTp.Substring(0, 2) + "' AND ShortcutQH = '" + TenTp.Substring(2, 2) + "'";
            //    }
            //    if (TenTp.Length == 6)
            //    {
            //        _rowFilter = " ShortcutTP = '" + TenTp.Substring(4, 2) + "' AND ShortcutXP='" +
            //                     TenTp.Substring(0, 2) + "' AND ShortcutQH = '" + TenTp.Substring(2, 2) + "'";
            //    }

            //    //_rowFilter = " Shortcut like '%" + TenTp + "%'  OR Value like  '%" + TenTp + "'%";
            //    // string.Format(", LQuickSearch.Columns.Shortform);
            //    m_DC.DefaultView.RowFilter = _rowFilter;
            //}
            if (Utility.DoTrim( TenTp) != "")
            {
                //Tạm bỏ đoạn dưới để xử lý 3 kí tự thành phố 230805
                //if (Utility.DoTrim(TenTp).Contains(" ") || Utility.CheckUnicode(TenTp) || Utility.DoTrim(TenTp).Length > LengthOfQuickType)
                //{
                //    _rowFilter = "1=0";
                //}
                //else if (Utility.DoTrim(TenTp).Contains(" ") || Utility.DoTrim(TenTp).Length > LengthOfQuickType)//Gõ trực tiếp tên thay vì gõ tắt
                    if (Utility.DoTrim(TenTp).Contains(" ") || Utility.DoTrim(TenTp).Length > LengthOfQuickType)//Gõ trực tiếp tên thay vì gõ tắt
                   // if (Utility.DoTrim(TenTp).Contains(" ") || Utility.CheckUnicode(TenTp) || Utility.DoTrim(TenTp).Length > 6)//Gõ trực tiếp tên thay vì gõ tắt
                {
                    _rowFilter = " Value like '%" + TenTp + "%' Or ComparedValue like '%" + TenTp + "%'";
                }
                else
                {
                    if (LengthOfQuickType == 6)
                    {
                        if (TenTp.Length == 1)
                        {
                            _rowFilter = " ShortcutTP like '%" + TenTp + "%' AND ShortcutXP='kx' AND ShortcutQH='kx'";
                        }
                        if (TenTp.Length == 2)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp + "' AND ShortcutXP='kx' AND ShortcutQH='kx'";
                        }
                        if (TenTp.Length == 3)
                        {
                            _rowFilter = " ShortcutTP like '%" + TenTp.Substring(2, 1) +
                                         "%' AND ShortcutQH = '" + TenTp.Substring(0, 2) + "'";
                        }
                        if (TenTp.Length == 4)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp.Substring(2, 2) + "' AND ShortcutQH = '" +
                                         TenTp.Substring(0, 2) + "'";
                        }
                        if (TenTp.Length == 5)
                        {
                            _rowFilter = " ShortcutTP like '%" + TenTp.Substring(4, 1) + "%' AND ShortcutXP ='" +
                                         TenTp.Substring(0, 2) + "' AND ShortcutQH = '" + TenTp.Substring(2, 2) + "'";
                        }
                        if (TenTp.Length == 6)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp.Substring(4, 2) + "' AND ShortcutXP='" +
                                         TenTp.Substring(0, 2) + "' AND ShortcutQH = '" + TenTp.Substring(2, 2) + "'";
                        }
                    }
                    else if (LengthOfQuickType == 7)
                    {
                        if (TenTp.Length == 1)
                        {
                            _rowFilter = " ShortcutTP like '%" + TenTp + "%' AND ShortcutXP='kx' AND ShortcutQH='kx'";
                        }
                        if (TenTp.Length == 2)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp + "' AND ShortcutXP='kx' AND ShortcutQH='kx'";
                        }
                        if (TenTp.Length == 3)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp + "' AND ShortcutXP='kx' AND ShortcutQH='kx'";
                        }
                        if (TenTp.Length == 4)
                        {
                            _rowFilter = " ShortcutTP like '%" + TenTp.Substring(3, 1) +
                                         "%' AND ShortcutQH = '" + TenTp.Substring(0, 1) + "'";
                        }
                        if (TenTp.Length == 5)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp.Substring(2, 3) + "' AND ShortcutQH = '" +
                                         TenTp.Substring(0, 2) + "'";
                        }
                        if (TenTp.Length == 6)
                        {
                            _rowFilter = " ShortcutTP like '%" + TenTp.Substring(4, 2) + "%' AND ShortcutXP ='" +
                                         TenTp.Substring(0, 2) + "' AND ShortcutQH = '" + TenTp.Substring(2, 2) + "'";
                        }
                        if (TenTp.Length == 7)
                        {
                            _rowFilter = " ShortcutTP = '" + TenTp.Substring(4, 3) + "' AND ShortcutXP='" +
                                         TenTp.Substring(0, 2) + "' AND ShortcutQH = '" + TenTp.Substring(2, 2) + "'";
                        }
                    }


                }
                //_rowFilter = " Shortcut like '%" + TenTp + "%'  OR Value like  '%" + TenTp + "'%";
                // string.Format(", LQuickSearch.Columns.Shortform);
                dtData.DefaultView.RowFilter = _rowFilter;
            }
            dtData.AcceptChanges();
            for (int i = 0; i <= dtData.DefaultView.Count - 1; i++)
            {
                string ItemsValue = Utility.sDbnull(dtData.DefaultView[i]["Value"], "");
                string Diachinh = string.Format("{0};{1};{2}", Utility.sDbnull(dtData.DefaultView[i]["MaTinhTP"], ""), Utility.sDbnull(dtData.DefaultView[i]["MaQH"], ""), Utility.sDbnull(dtData.DefaultView[i]["MaXaPhuong"], ""));
                CurrentAutoCompleteList.Add(ItemsValue);
                CurrentAutoCompleteList_IDAndCode.Add(Diachinh);
            }
        }
        // This is a timecritical part
        // Fills/ refreshed the CurrentAutoCompleteList with appropreate candidates
        private void UpdateCurrentAutoCompleteList()
        {
            if (CurrentWord == "")
            {
                int a=0;
            }
            // the list of suggestions has to be refreshed so clear it
            CurrentAutoCompleteList.Clear();
            CurrentAutoCompleteList_IDAndCode.Clear();
            // an find appropreate candidates for the new CurrentAutoCompleteList in AutoCompleteList

            buildData(CurrentWord);

            //foreach (string Str in AutoCompleteList)
            //{
            //    // be casesensitive
            //    if (CaseSensitive)
            //    {
            //        // search for the substring (equal to SQL Like Command)
            //        if ((Str.IndexOf(this.Text) > -1))
            //        // Add candidates to new CurrentAutoCompleteList
            //        { 
            //            CurrentAutoCompleteList.Add(Str.Split(splitChar)[1]);
            //            CurrentAutoCompleteList_IDAndCode.Add(Str.Split(splitChar)[0]); 
            //        }
            //    }
            //    // or ignore case
            //    else
            //    {
            //        // and search for the substring (equal to SQL Like Command)
            //        if ((Str.ToLower().IndexOf(this.Text.ToLower()) > -1))
            //        // Add candidates to new CurrentAutoCompleteList
            //        {
            //            CurrentAutoCompleteList.Add(Str.Split(splitChar)[1]);
            //            CurrentAutoCompleteList_IDAndCode.Add(Str.Split(splitChar)[0]);
            //        }
            //    }
            //}
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

                // if there is a ParentForm
                if ((TopLevelControl != null))
                {
                    // get its width
                    panel.Width = this.Width + ExtraWidth;
                    // calculate the remeining height beneath the TextBox
                    panel.Height = GetMyHeigh();// ;
                    // and the Location to use

                    panel.Location = FindLocation(this) + new Size(0, this.Height);// this.Location + new Size(0, this.Height);
                    // Panel and ListBox have to be added to ParentForm.Controls before calling BingingContext
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
            finally
            {
                if (lblTest != null) lblTest.Text = String.Format("{0}{1}", listBox.SelectedIndex.ToString(), CurrentAutoCompleteList.Count.ToString());
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

            return listBox.Height;
        }

        #endregion Methods

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