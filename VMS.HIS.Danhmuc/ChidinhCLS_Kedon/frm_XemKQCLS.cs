using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;

namespace VMS.HIS.Danhmuc.Dungchung
{
    public partial class frm_XemKQCLS : Form
    {
        public KcbLuotkham objLuotkham = null;
        public KcbDanhsachBenhnhan objBenhnhan= null;
        byte noitru = 100;
        string SplitterPath = "";
        bool hasLoaded = false;
        public frm_XemKQCLS(KcbLuotkham objLuotkham,byte noitru)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor.html");
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            this.objLuotkham = objLuotkham;
            if (objBenhnhan == null && objLuotkham != null)
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
            this.noitru = noitru;
            grdAssignDetail.SelectionChanged += grdAssignDetail_SelectionChanged;
            this.Shown += frm_XemKQCLS_Shown;
            this.FormClosing += frm_XemKQCLS_FormClosing;
        }

        void frm_XemKQCLS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString()});
        }

        void frm_XemKQCLS_Shown(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            Try2Splitter();

            Utility.DefaultNow(this);
        }
        GridEXRow RowCLS = null;
        void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!hasLoaded ) return;
            RowCLS =Utility.findthelastChild(grdAssignDetail.CurrentRow);
            ShowResult();
        }
        DataTable dtKQXN = null;
        bool CKEditorInput = true;
        int SplitterKQ = -1;
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count == 1)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                    SplitterKQ = splitContainer1.SplitterDistance;
                }
            }
            catch (Exception)
            {

            }
        }
        private void ShowResult()
        {
            try
            {
                if (!Utility.isValidGrid(grdAssignDetail))
                {
                    grdKetqua.DataSource = null;
                    txtNoiDung.Clear();
                    //splitContainer1.Panel2Collapsed = true;
                    return;
                }
                dtKQXN = null;
                CKEditorInput = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.ResultType) == "1";
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);


                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(grdAssignDetail, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (Utility.Coquyen("quyen_nhap_kqxn"))
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.TextBox;
                else
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.NoEdit;

                if (trangthaiChitiet <= 2)
                //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        pnlXN.SendToBack();
                       // splitContainer1.Panel2Collapsed = true;
                    }
                    else
                    {
                        pnlXN.BringToFront();
                       // splitContainer1.Panel2Collapsed = true;
                        ShowKQXN();
                        //mnuNhapKQXN.Visible = true; 
                    }

                    Application.DoEvents();
                }
                else//Có kết quả CLS
                {
                    if (maloaiDichvuCls == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    //splitContainer1.Panel2Collapsed = false;
                    //if (SplitterKQ > 0)
                    //    splitContainer1.SplitterDistance = SplitterKQ;
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        //mnuNhapKQXN.Visible = true;

                        ShowKQXN();
                       
                    }
                    else //XQ,SA,DT,NS
                    {
                        // mnuNhapKQCDHA.Visible = true;//Mở nếu dùng cho phòng khám
                        if (CKEditorInput)
                        {
                            pnlCKEditor.BringToFront();
                            ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), 0));
                        }
                        else
                        {
                            pnlCKEditor.SendToBack();
                            FillDynamicValues();
                        }
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        DataTable dtDynamicData = null;
        void FillDynamicValues()
        {
            try
            {
                pnlCKEditor.SendToBack();
                long v_id_chitietchidinh = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), -1);
                int id_vungks = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_vungks"), -1);
                if (v_id_chitietchidinh <= 0) return;

                flowDynamics.SuspendLayout();
                flowDynamics.Controls.Clear();
                dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(id_vungks, v_id_chitietchidinh).GetDataSet().Tables[0];
                if (dtDynamicData.Rows.Count == 0)
                {
                    pnlCKEditor.BringToFront();
                    ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_VungKS"), 0));
                    return;
                }
                foreach (DataRow dr in dtDynamicData.Select("1=1", "Stt_hthi"))
                {
                    VNS.UCs.ucAutoCompleteParam _ucp = new VNS.UCs.ucAutoCompleteParam(dr, true);
                    _ucp.txtValue.VisibleDefaultItem = false;
                    _ucp.txtValue.ReadOnly = true;
                    _ucp.IdChidinhchitiet = v_id_chitietchidinh;
                    _ucp.txtValue.RaiseEventEnter = true;
                    //_ucp.lblName.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    //_ucp.txtValue.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    _ucp.TabStop = true;
                    _ucp.txtValue.AllowEmpty = Utility.Int32Dbnull(dr[DynamicField.Columns.AllowEmpty], 0) == 1;
                    _ucp.txtValue.Multiline = Utility.Int32Dbnull(dr[DynamicField.Columns.Multiline], 0) == 1;
                    _ucp.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.W], 0);
                    _ucp.Height = Utility.Int32Dbnull(dr[DynamicField.Columns.H], 0);
                    _ucp.lblName.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.LblW], 0);
                    _ucp.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);

                    if (_ucp.Width >= flowDynamics.Width)
                    {
                        _ucp.Width = flowDynamics.Width - PropertyLib._HinhAnhProperties.AutoCompleteMargin;
                    }

                    _ucp.Init();

                    flowDynamics.Controls.Add(_ucp);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);


            }
            finally
            {
                flowDynamics.ResumeLayout(true);
            }
        }
        void ShowEditor(int id_chidinhchitiet)
        {
            pnlCKEditor.BringToFront();
            DataTable dtKQCDHA = SPs.ClsLayketquaHa(id_chidinhchitiet).GetDataSet().Tables[0];
            if (dtKQCDHA.Rows.Count > 0)
            {
                txtNoiDung.Text = Utility.sDbnull(dtKQCDHA.Rows[0]["mota_html"], "");
                timer1.Start();
                LoadHTML();
            }
        }
        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        #region NhapKQXN
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        void ShowKQXN()
        {
            if (!Utility.isValidGrid(grdAssignDetail)) return;
            int tempRowIdx = grdAssignDetail.CurrentRow.RowIndex;
            if (currRowIdx == -1 || currRowIdx != tempRowIdx)
            {
                currRowIdx = tempRowIdx;
                id_dichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);
                IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                co_chitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);
                id_chidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                MaChidinh = Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh);
                MaBenhpham = Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham);
                HienthiNhapketqua(id_dichvu, co_chitiet);
            }
        }
        void HienthiNhapketqua(int id_dichvu, int co_chitiet)
        {
            try
            {
                // DataTable dt = SPs.ClsTimkiemthongsoXNNhapketqua(ma_luotkham, MaChidinh, MaBenhpham, id_chidinh, co_chitiet, id_dichvu, IdChitietdichvu).GetDataSet().Tables[0];
                DataTable dt = SPs.ClsLayKetquaXn(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, MaChidinh, id_chidinh, 0, objBenhnhan.IdGioitinh).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,stt_in");

                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);


            }
        }
        #endregion
    

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoiDung.Text)) ClickData();
        }
        private void ClickData()
        {
            webBrowser1.Document.InvokeScript("setValue", new[] { txtNoiDung.Text });
            timer1.Stop();
        }

        private void frm_XemKQCLS_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.KcbThamkhamLaythongtinclsLichsukcb(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, -1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, dtData, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                hasLoaded = true;
                grdAssignDetail.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cdViewPDF_Click(object sender, EventArgs e)
        {
            if (RowCLS == null || objLuotkham == null ) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(0);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
        }
    }
}
