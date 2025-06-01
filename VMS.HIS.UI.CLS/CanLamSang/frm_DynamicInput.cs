using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using SubSonic;
using Janus.Windows.GridEX;
using VNS.Libs;
using System.Transactions;

namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_DynamicInput : Form
    {
        public DmucDichvuclsChitiet objDichvuchitiet = null;
        
        public long ImageID = -1;
        public long Id_chidinhchitiet = -1;
        public frm_DynamicInput()
        {
            InitializeComponent();
            this.Load += frm_DynamicInput_Load;
            cmdExit.Click+=cmdExit_Click;
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.KeyDown += frm_DynamicInput_KeyDown;
        }

        void frm_DynamicInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
                return;
            }
        }
        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                var q = from p in grdList.GetDataRows().AsEnumerable()
                        where p != grdList.CurrentRow
                        && Utility.sDbnull(p.Cells[DynamicField.Columns.Ma], "") == e.Value
                        select p;
                if (q.Count() > 0)
                {
                    Utility.ShowMsg("Mã này đã tồn tại, bạn cần nhập mã khác!");
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
            }
        }

        void frm_DynamicInput_Load(object sender, EventArgs e)
        {
            LoadData(); 
        }
        void LoadData()
        {
            try
            {
                if(objDichvuchitiet==null)
                {
                    Utility.ShowMsg("Không xác định được danh mục dịch vụ CĐHA. Bạn cần kiểm tra lại danh mục hệ thống");
                    cmdSave.Enabled = false;
                }
                Utility.SetDataSourceForDataGridEx(grdList, GetDynamicFieldsValues(), true, true, "1=1", "Stt_hthi");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        //private void cmdSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<DynamicField> lstFields = new List<DynamicField>();
        //        List<DynamicValue> lstValues = new List<DynamicValue>();
        //        foreach (GridEXRow _row in grdList.GetDataRows())
        //        {
        //            DynamicField obj = null;
        //            DynamicValue objVal = null;

        //            if (Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1) > 0)
        //            {
        //                obj = DynamicField.FetchByID(Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1));
        //                obj.IsNew = false;
        //                obj.MarkOld();
        //            }
        //            else
        //            {
        //                obj = new DynamicField();
        //                obj.IsNew = true;
        //            }

        //            if (Utility.Int32Dbnull(_row.Cells["idValue"].Value, -1) > 0)
        //            {
        //                objVal = DynamicValue.FetchByID(Utility.Int32Dbnull(_row.Cells["idValue"].Value, -1));
        //                objVal.IsNew = false;
        //                objVal.MarkOld();
        //            }
        //            else
        //            {
        //                objVal = new DynamicValue();
        //                objVal.IsNew = true;
        //            }


        //            obj.Id = Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1);
        //            obj.Ma = Utility.sDbnull(_row.Cells[DynamicField.Columns.Ma].Value, "-1");
        //            obj.Bodypart = objDichvuchitiet.Bodypart;
        //            obj.Viewposition = objDichvuchitiet.ViewPosition;

        //            objVal.Id = Utility.Int32Dbnull(_row.Cells["idValue"].Value, -1);
        //            objVal.Ma = Utility.sDbnull(_row.Cells[DynamicField.Columns.Ma].Value, "-1");
        //            obj.Mota = Utility.sDbnull(_row.Cells[DynamicField.Columns.Mota].Value, "-1");
        //            objVal.Giatri = Utility.sDbnull(_row.Cells[DynamicValue.Columns.Giatri].Value, "-1");
        //            objVal.ImageId = ImageID;
        //            objVal.IdChidinhchitiet = Id_chidinhchitiet;
        //            lstFields.Add(obj);
        //            lstValues.Add(objVal);
        //        }
        //        ActionResult _actionResult = UpdateDynamicValues(lstFields, lstValues);
        //        if (_actionResult == ActionResult.Success)
        //        {
        //            this.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<DynamicValue> lstValues = new List<DynamicValue>();
                foreach (GridEXRow _row in grdList.GetDataRows())
                {
                    DynamicValue objVal = null;

                    
                    if (Utility.Int32Dbnull(_row.Cells["idValue"].Value, -1) > 0)
                    {
                        objVal = DynamicValue.FetchByID(Utility.Int32Dbnull(_row.Cells["idValue"].Value, -1));
                        objVal.IsNew = false;
                        objVal.MarkOld();
                    }
                    else
                    {
                        objVal = new DynamicValue();
                        objVal.IsNew = true;
                    }

                    objVal.Id = Utility.Int32Dbnull(_row.Cells["idValue"].Value, -1);
                    objVal.Ma = Utility.sDbnull(_row.Cells[DynamicField.Columns.Ma].Value, "-1");
                    objVal.Giatri = Utility.sDbnull(_row.Cells[DynamicValue.Columns.Giatri].Value, "-1");
                    objVal.IdChidinhchitiet = Id_chidinhchitiet;
                    
                    lstValues.Add(objVal);
                }
                ActionResult _actionResult = UpdateDynamicValues(lstValues);
                if (_actionResult == ActionResult.Success)
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public DataTable GetDynamicFieldsValues()
        {
            try
            {
                return null;// SPs.HinhanhGetDynamicFieldsValues(objDichvuchitiet.IdChitietdichvu, objDichvuchitiet.MauChuan, objDichvuchitiet.Bodypart, objDichvuchitiet.ViewPosition, ImageID, Id_chidinhchitiet).GetDataSet().Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        public ActionResult UpdateDynamicValues(List<DynamicValue> lstValues)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        foreach (DynamicValue _object in lstValues)
                        {
                            if (_object.Id > 0)
                            {
                                _object.MarkOld();
                                _object.IsNew = false;
                                _object.Save();
                            }
                            else//Insert
                            {
                                _object.IsNew = true;

                                _object.Save();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                return ActionResult.Error;
            }

        }
    }
}
