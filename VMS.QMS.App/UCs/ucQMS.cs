using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.QMS;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs.AppUI;
using Microsoft.VisualBasic;
namespace QMS.UCs
{
    public partial class ucQMS : UserControl
    {
        private QMSProperties _QMSProperties;
        int id_khoakcb = -1;
        string ma_doituong_kcb = "ALL";
        DataTable dtBuongkham = null;
        DataTable dtSttKham = null;
        public ucQMS()
        {
            InitializeComponent();
            
        }
        public void Init(DataTable dtBuongkham, DataTable dtSttKham , int id_khoakcb, QMSProperties _QMSProperties, string ma_doituong_kcb)
        {
            this.id_khoakcb = id_khoakcb;
            this.ma_doituong_kcb = ma_doituong_kcb;
            this._QMSProperties = _QMSProperties;
            this.dtBuongkham = dtBuongkham;
            this.dtSttKham = dtSttKham;
            InitControl();
        }
        public void InitControl()
        {
            try
            {
                int soluotkham =Utility.Int32Dbnull( dtSttKham.Compute("SUM(so_luong)", "id_khoakcb" + "=" + id_khoakcb.ToString()),0);
                string str = Utility.sDbnull(soluotkham, "0");
                if (Utility.Int32Dbnull(soluotkham, 0) < 10)
                {
                    str = Utility.FormatNumberToString(soluotkham, "00");
                }
                UIAction.SetText(lblTotal, str);
                flowLayoutPanel1.Controls.Clear();
                foreach (DataRow dr in dtBuongkham.Rows)
                {
                    ucQMSItem _ucQMSItem = new ucQMSItem(Utility.Int32Dbnull(dr["id_khoaphong"], 0), Utility.sDbnull(dr["ma_khoaphong"], "KKB"), Utility.Int32Dbnull(dr["id_kieukham"], 0),
                        Utility.Int32Dbnull(dr["id_phongkham"], 0), Utility.Int32Dbnull(dr["id_dichvukcb"], 0), Utility.sDbnull(dr["ten_hienthi"]), _QMSProperties, ma_doituong_kcb, 0);
                    _ucQMSItem._OnCreatedQMSNumber += _ucQMSItem__OnCreatedQMSNumber;
                    DataRow[] arrDr = dtSttKham.Select("id_khoakcb" + "=" + _ucQMSItem.id_KhoaKcb.ToString() + " AND " + "id_phongkham" + "=" + _ucQMSItem.id_phongkham.ToString());
                    //if (arrDr.Length > 0)
                    //{
                    DataTable dtSTTKhamItem = dtSttKham.Clone() ;
                    if (arrDr.Length > 0)
                        dtSTTKhamItem = arrDr.CopyToDataTable();
                    _ucQMSItem.Init(dtSTTKhamItem);
                        flowLayoutPanel1.Controls.Add(_ucQMSItem);
                    //}
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }

        void _ucQMSItem__OnCreatedQMSNumber(string MaxNumber)
        {
            DataTable dtSttKham = SPs.QmsLayMaxSTTKham(id_khoakcb, -1).GetDataSet().Tables[0];
            int soluotkham = Utility.Int32Dbnull(dtSttKham.Compute("SUM(so_luong)", "id_khoakcb" + "=" + id_khoakcb.ToString()), 0);
            string svalue = "";
            if(soluotkham<10)
                svalue=Utility.FormatNumberToString(soluotkham, "00");
            else 
                svalue=soluotkham.ToString();
            UIAction.SetText(lblTotal, svalue);
        }
    }
}
