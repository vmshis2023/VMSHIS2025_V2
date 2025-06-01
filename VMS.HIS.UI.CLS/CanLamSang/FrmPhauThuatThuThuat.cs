using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class FrmPhauThuatThuThuat : Form
    {
        private long _idChitietChidinh = -1;
        private KcbPhieupttt objKcbPhieuPttt;
        public FrmPhauThuatThuThuat(long idChitietchidinh)
        {
            InitializeComponent();
            _idChitietChidinh = idChitietchidinh;
            txt_chandoan_truoc._OnShowData += txt_chandoan_truoc__OnShowData;
            txt_chandoan_truoc._OnSaveAs += txt_chandoan_truoc__OnSaveAs;
            txt_chandoan_sau._OnShowData += txt_chandoan_sau__OnShowData;
            txt_chandoan_sau._OnSaveAs += txt_chandoan_sau__OnSaveAs;
            txt_pptt._OnShowData += txt_pptt__OnShowData;
            txt_pptt._OnSaveAs += txt_pptt__OnSaveAs;
            txt_loaipt._OnShowData += txt_loaipt__OnShowData;
            txt_loaipt._OnSaveAs += txt_loaipt__OnSaveAs;
            txt_ppvocam._OnShowData += txt_ppvocam__OnShowData;
            txt_ppvocam._OnSaveAs += txt_ppvocam__OnSaveAs;
            txt_ptvien._OnShowData += txt_ptvien__OnShowData;
            txt_ptvien._OnSaveAs += txt_ptvien__OnSaveAs;
            txt_bacsy_gayme._OnShowData += txt_bacsy_gayme__OnShowData;
            txt_bacsy_gayme._OnSaveAs += txt_bacsy_gayme__OnSaveAs;
            txt_ytaphu._OnShowData += txt_ytaphu__OnShowData;
            txt_ytaphu._OnSaveAs += txt_ytaphu__OnSaveAs;
            txt_luocdo_danluu._OnShowData += txt_luocdo_danluu__OnShowData;
            txt_luocdo_danluu._OnSaveAs += txt_luocdo_danluu__OnSaveAs;
            txt_luocdo_bac._OnShowData += txt_luocdo_bac__OnShowData;
            txt_luocdo_bac._OnSaveAs += txt_luocdo_bac__OnSaveAs;
            txt_luocdo_khac._OnShowData += txt_luocdo_khac__OnShowData;
            txt_luocdo_khac._OnSaveAs += txt_luocdo_khac__OnSaveAs;
            dtpCreatedDate.Value = dtpngaycatchi.Value = dtpngayrut.Value = globalVariables.SysDate;
            txtid.Visible = globalVariables.IsAdmin;
        }
        private void txt_luocdo_khac__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_luocdo_khac.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_luocdo_khac.myCode;
                txt_luocdo_khac.Init();
                txt_luocdo_khac.SetCode(oldCode);
                txt_luocdo_khac.Focus();
            }
        }

        private void txt_luocdo_khac__OnSaveAs()
        {
            if (Utility.DoTrim(txt_luocdo_khac.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_luocdo_khac.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_luocdo_khac.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_luocdo_khac.myCode;
                txt_luocdo_khac.Init();
                txt_luocdo_khac.SetCode(oldCode);
                txt_luocdo_khac.Focus();
            }
        }
        private void txt_luocdo_bac__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_luocdo_bac.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_luocdo_bac.myCode;
                txt_luocdo_bac.Init();
                txt_luocdo_bac.SetCode(oldCode);
                txt_luocdo_bac.Focus();
            }
        }

        private void txt_luocdo_bac__OnSaveAs()
        {
            if (Utility.DoTrim(txt_luocdo_bac.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_luocdo_bac.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_luocdo_bac.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_luocdo_bac.myCode;
                txt_luocdo_bac.Init();
                txt_luocdo_bac.SetCode(oldCode);
                txt_luocdo_bac.Focus();
            }
        }
        private void txt_luocdo_danluu__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_luocdo_danluu.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_luocdo_danluu.myCode;
                txt_luocdo_danluu.Init();
                txt_luocdo_danluu.SetCode(oldCode);
                txt_luocdo_danluu.Focus();
            }
        }

        private void txt_luocdo_danluu__OnSaveAs()
        {
            if (Utility.DoTrim(txt_luocdo_danluu.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_luocdo_danluu.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_luocdo_danluu.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_luocdo_danluu.myCode;
                txt_luocdo_danluu.Init();
                txt_luocdo_danluu.SetCode(oldCode);
                txt_luocdo_danluu.Focus();
            }
        }
        private void txt_ytaphu__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_ytaphu.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_ytaphu.myCode;
                txt_ytaphu.Init();
                txt_ytaphu.SetCode(oldCode);
                txt_ytaphu.Focus();
            }
        }

        private void txt_ytaphu__OnSaveAs()
        {
            if (Utility.DoTrim(txt_ytaphu.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_ytaphu.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_ytaphu.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_ytaphu.myCode;
                txt_ytaphu.Init();
                txt_ytaphu.SetCode(oldCode);
                txt_ytaphu.Focus();
            }
        }
        private void txt_bacsy_gayme__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_bacsy_gayme.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_bacsy_gayme.myCode;
                txt_bacsy_gayme.Init();
                txt_bacsy_gayme.SetCode(oldCode);
                txt_bacsy_gayme.Focus();
            }
        }

        private void txt_bacsy_gayme__OnSaveAs()
        {
            if (Utility.DoTrim(txt_bacsy_gayme.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_bacsy_gayme.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_bacsy_gayme.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_bacsy_gayme.myCode;
                txt_bacsy_gayme.Init();
                txt_bacsy_gayme.SetCode(oldCode);
                txt_bacsy_gayme.Focus();
            }
        }
        private void txt_ptvien__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_ptvien.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_ptvien.myCode;
                txt_ptvien.Init();
                txt_ptvien.SetCode(oldCode);
                txt_ptvien.Focus();
            }
        }

        private void txt_ptvien__OnSaveAs()
        {
            if (Utility.DoTrim(txt_ptvien.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_ptvien.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_ptvien.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_ptvien.myCode;
                txt_ptvien.Init();
                txt_ptvien.SetCode(oldCode);
                txt_ptvien.Focus();
            }
        }
        private void txt_ppvocam__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_ppvocam.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_ppvocam.myCode;
                txt_ppvocam.Init();
                txt_ppvocam.SetCode(oldCode);
                txt_ppvocam.Focus();
            }
        }

        private void txt_ppvocam__OnSaveAs()
        {
            if (Utility.DoTrim(txt_ppvocam.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_ppvocam.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_ppvocam.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_ppvocam.myCode;
                txt_ppvocam.Init();
                txt_ppvocam.SetCode(oldCode);
                txt_ppvocam.Focus();
            }
        }

        private void txt_loaipt__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_loaipt.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_loaipt.myCode;
                txt_loaipt.Init();
                txt_loaipt.SetCode(oldCode);
                txt_loaipt.Focus();
            }
        }

        private void txt_loaipt__OnSaveAs()
        {
            if (Utility.DoTrim(txt_loaipt.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_loaipt.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_loaipt.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_loaipt.myCode;
                txt_loaipt.Init();
                txt_loaipt.SetCode(oldCode);
                txt_loaipt.Focus();
            }
        }

        private void txt_pptt__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_pptt.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_pptt.myCode;
                txt_pptt.Init();
                txt_pptt.SetCode(oldCode);
                txt_pptt.Focus();
            }
        }

        private void txt_pptt__OnSaveAs()
        {
            if (Utility.DoTrim(txt_pptt.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_pptt.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_pptt.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_pptt.myCode;
                txt_pptt.Init();
                txt_pptt.SetCode(oldCode);
                txt_pptt.Focus();
            }
        }

        private void txt_chandoan_truoc__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_chandoan_truoc.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_chandoan_truoc.myCode;
                txt_chandoan_truoc.Init();
                txt_chandoan_truoc.SetCode(oldCode);
                txt_chandoan_truoc.Focus();
            }
        }

        private void txt_chandoan_truoc__OnSaveAs()
        {
            if (Utility.DoTrim(txt_chandoan_truoc.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_chandoan_truoc.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_chandoan_truoc.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_chandoan_truoc.myCode;
                txt_chandoan_truoc.Init();
                txt_chandoan_truoc.SetCode(oldCode);
                txt_chandoan_truoc.Focus();
            }
        }


        private void txt_chandoan_sau__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_chandoan_sau.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_chandoan_sau.myCode;
                txt_chandoan_sau.Init();
                txt_chandoan_sau.SetCode(oldCode);
                txt_chandoan_sau.Focus();
            }
        }

        private void txt_chandoan_sau__OnSaveAs()
        {
            if (Utility.DoTrim(txt_chandoan_sau.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_chandoan_sau.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_chandoan_sau.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_chandoan_sau.myCode;
                txt_chandoan_sau.Init();
                txt_chandoan_sau.SetCode(oldCode);
                txt_chandoan_sau.Focus();
            }
        }

        private void ClearControl()
        {
            txt_chandoan_sau.Clear();
            txt_chandoan_truoc.Clear();
            txt_pptt.Clear();
            txt_loaipt.Clear();
            txt_ppvocam.Clear();
            txt_ptvien.Clear();
            txt_bacsy_gayme.Clear();
            txtbacsythuchien.SetId(-1);
            txt_ytaphu.Clear();
            txt_luocdo_bac.Clear();
            txt_luocdo_danluu.Clear();
            txt_luocdo_khac.Clear();
            txtid.Clear();

        }

        private DataTable _dtPhieuPttt = null;
        private void LoadThongTin()
        {
            try
            {
                if(Utility.Int64Dbnull(_idChitietChidinh) < 0 ) return;
                _dtPhieuPttt = new DataTable();
                _dtPhieuPttt = SPs.KcbLaythongtinPhieuPtt(_idChitietChidinh).GetDataSet().Tables[0];
                if (_dtPhieuPttt.Rows.Count> 0)
                {
                    foreach (DataRow row in _dtPhieuPttt.Rows)
                    {
                        txtid.Text = row[KcbPhieuPttt.Columns.Id].ToString();
                        txtHovaTen.Text = row[KcbDanhsachBenhnhan.Columns.TenBenhnhan].ToString();
                        txtNamSinh.Text = row[KcbDanhsachBenhnhan.Columns.NamSinh].ToString();
                        txtGioiTinh.Text = row[KcbDanhsachBenhnhan.Columns.GioiTinh].ToString();
                        txtDoituong.Text = row["ten_doituong"].ToString();
                        txtDichvu.Text = row[DmucDichvuclsChitiet.Columns.TenChitietdichvuBhyt].ToString();
                        dtpCreatedDate.Value = Convert.ToDateTime(row[KcbPhieuPttt.Columns.NgayPhauthuat]);
                        dtpngaycatchi.Value = Convert.ToDateTime(row[KcbPhieuPttt.Columns.LuocdoNgaycatchi]);
                        dtpngayrut.Value = Convert.ToDateTime(row[KcbPhieuPttt.Columns.LuocdoNgayrut]);
                        txt_chandoan_truoc.Text = row[KcbPhieuPttt.Columns.ChandoanTruocpt].ToString();
                        txt_chandoan_sau.Text = row[KcbPhieuPttt.Columns.ChandoanSaupt].ToString();
                        txt_pptt.Text = row[KcbPhieuPttt.Columns.PhuongphapPt].ToString();
                        txt_loaipt.Text = row[KcbPhieuPttt.Columns.LoaiPt].ToString();
                        txt_ppvocam.Text = row[KcbPhieuPttt.Columns.PpVocam].ToString();
                        txtbacsythuchien.SetId(row[KcbPhieuPttt.Columns.BacsyPt]);
                        txt_ptvien.Text = row[KcbPhieuPttt.Columns.Yta1].ToString();
                        txt_bacsy_gayme.Text = row[KcbPhieuPttt.Columns.BacsyGayme].ToString();
                        txt_ytaphu.Text = row[KcbPhieuPttt.Columns.Yta2].ToString();
                        txt_luocdo_danluu.Text = row[KcbPhieuPttt.Columns.LuocdoDanluu].ToString();
                        txt_luocdo_bac.Text = row[KcbPhieuPttt.Columns.LuocdoBac].ToString();
                        txt_luocdo_khac.Text = row[KcbPhieuPttt.Columns.LuocdoKhac].ToString();
                    }
                }
                else
                {
                    _dtPhieuPttt = SPs.KcbLaythongtinChitietChidinh(_idChitietChidinh).GetDataSet().Tables[0];
                    if (_dtPhieuPttt.Rows.Count > 0)
                    {
                        foreach (DataRow row in _dtPhieuPttt.Rows)
                        {
                            txtHovaTen.Text = row[KcbDanhsachBenhnhan.Columns.TenBenhnhan].ToString();
                            txtNamSinh.Text = row[KcbDanhsachBenhnhan.Columns.NamSinh].ToString();
                            txtGioiTinh.Text = row[KcbDanhsachBenhnhan.Columns.GioiTinh].ToString();
                            txtDoituong.Text = row["ten_doituong"].ToString();
                            txtDichvu.Text = row[DmucDichvuclsChitiet.Columns.TenChitietdichvuBhyt].ToString();
                            txtidchitietdichvu.Text = row[DmucDichvuclsChitiet.Columns.IdChitietdichvu].ToString();
                            txtmalankham.Text = row[KcbLuotkham.Columns.MaLuotkham].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 Utility.ShowMsg("Lỗi: "+ ex.Message);   
            }
           
        }
        public action m_enAct = action.Insert;
        private KcbPhieuPttt CreatKcbPhieuPttt()
        {
            KcbPhieuPttt objKcbPhieuPttt = new KcbPhieuPttt();
            if (Utility.Int64Dbnull(txtid.Text) > 0)
            {
                m_enAct = action.Update;
                objKcbPhieuPttt = KcbPhieuPttt.FetchByID(Utility.Int64Dbnull(txtid.Text));
                objKcbPhieuPttt.Id = Utility.Int64Dbnull(txtid.Text);
                objKcbPhieuPttt.Ngaysua = globalVariables.SysDate;
                objKcbPhieuPttt.Nguoisua = globalVariables.UserName;

            }
            else
            {
                m_enAct = action.Insert;
                objKcbPhieuPttt.Id = -1;
                 objKcbPhieuPttt.Ngaytao = globalVariables.SysDate;
                objKcbPhieuPttt.Nguoitao = globalVariables.UserName;
                objKcbPhieuPttt.IpMaytao = globalVariables.gv_strIPAddress;

            }
            objKcbPhieuPttt.IdChidinhChitiet = _idChitietChidinh;
            objKcbPhieuPttt.IdChitietDichvu = Utility.Int32Dbnull(txtidchitietdichvu.Text);
            objKcbPhieuPttt.MaLuotkham = txtmalankham.Text.Trim();
            objKcbPhieuPttt.NgayPhauthuat = dtpCreatedDate.Value;
            objKcbPhieuPttt.ChandoanTruocpt = txt_chandoan_truoc.Text;
            objKcbPhieuPttt.ChandoanSaupt = txt_chandoan_sau.Text;
            objKcbPhieuPttt.PhuongphapPt = txt_pptt.Text;
            objKcbPhieuPttt.PpVocam = txt_ppvocam.Text;
            objKcbPhieuPttt.LoaiPt = txt_loaipt.Text;
            objKcbPhieuPttt.BacsyPt = Utility.sDbnull(txtbacsythuchien.MyID);
            objKcbPhieuPttt.Yta1 = Utility.sDbnull(txt_ptvien.Text);
            objKcbPhieuPttt.BacsyGayme = Utility.sDbnull(txt_bacsy_gayme.Text);
            objKcbPhieuPttt.Yta2 = Utility.sDbnull(txt_ytaphu.Text);
            objKcbPhieuPttt.Yta3 = "";
            objKcbPhieuPttt.Yta4 = "";
            objKcbPhieuPttt.LuocdoDanluu = txt_luocdo_danluu.Text;
            objKcbPhieuPttt.LuocdoBac = txt_luocdo_bac.Text;
            objKcbPhieuPttt.LuocdoKhac = txt_luocdo_khac.Text;
            objKcbPhieuPttt.LuocdoNgaycatchi = dtpngaycatchi.Value;
            objKcbPhieuPttt.LuocdoNgayrut = dtpngayrut.Value;
            return objKcbPhieuPttt;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                objKcbPhieuPttt = CreatKcbPhieuPttt();
                switch (m_enAct)
                {
                    case action.Insert:
                        StoredProcedure sp_insert = SPs.KcbPhieuPtttInsert(objKcbPhieuPttt.Id, objKcbPhieuPttt.IdChidinhChitiet,
                            objKcbPhieuPttt.IdChitietDichvu, objKcbPhieuPttt.MaLuotkham, objKcbPhieuPttt.NgayPhauthuat,
                            objKcbPhieuPttt.ChandoanTruocpt, objKcbPhieuPttt.ChandoanSaupt, objKcbPhieuPttt.PhuongphapPt,
                            objKcbPhieuPttt.LoaiPt, objKcbPhieuPttt.PpVocam, objKcbPhieuPttt.BacsyPt, objKcbPhieuPttt.Yta1,
                            objKcbPhieuPttt.Yta2,
                            objKcbPhieuPttt.Yta3, objKcbPhieuPttt.Yta4, objKcbPhieuPttt.BacsyGayme,
                            objKcbPhieuPttt.LuocdoDanluu, objKcbPhieuPttt.LuocdoBac, objKcbPhieuPttt.LuocdoNgayrut,
                            objKcbPhieuPttt.LuocdoNgaycatchi, objKcbPhieuPttt.LuocdoKhac, objKcbPhieuPttt.Ngaytao,
                            objKcbPhieuPttt.Nguoitao, objKcbPhieuPttt.IpMaytao, objKcbPhieuPttt.Nguoisua,
                            objKcbPhieuPttt.Ngaysua);
                        sp_insert.Execute();
                        txtid.Text = Utility.sDbnull(sp_insert.OutputValues[0]);
                        break;
                    case action.Update:
                        StoredProcedure sp_update = SPs.KcbPhieuPtttUpdate(objKcbPhieuPttt.Id, objKcbPhieuPttt.IdChidinhChitiet,
                           objKcbPhieuPttt.IdChitietDichvu, objKcbPhieuPttt.MaLuotkham, objKcbPhieuPttt.NgayPhauthuat,
                           objKcbPhieuPttt.ChandoanTruocpt, objKcbPhieuPttt.ChandoanSaupt, objKcbPhieuPttt.PhuongphapPt,
                           objKcbPhieuPttt.LoaiPt, objKcbPhieuPttt.PpVocam, objKcbPhieuPttt.BacsyPt, objKcbPhieuPttt.Yta1,
                           objKcbPhieuPttt.Yta2,
                           objKcbPhieuPttt.Yta3, objKcbPhieuPttt.Yta4, objKcbPhieuPttt.BacsyGayme,
                           objKcbPhieuPttt.LuocdoDanluu, objKcbPhieuPttt.LuocdoBac, objKcbPhieuPttt.LuocdoNgayrut,
                           objKcbPhieuPttt.LuocdoNgaycatchi, objKcbPhieuPttt.LuocdoKhac, objKcbPhieuPttt.Ngaytao,
                           objKcbPhieuPttt.Nguoitao, objKcbPhieuPttt.IpMaytao, objKcbPhieuPttt.Nguoisua,
                           objKcbPhieuPttt.Ngaysua);
                        sp_update.Execute();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            
        }

        private void LoadBacsythuchien()
        {
            txtbacsythuchien.Init(globalVariables.gv_dtDmucNhanvien,
                           new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            if (globalVariables.gv_intIDNhanvien <= 0)
            {
                txtbacsythuchien.SetId(-1);
            }
            else
            {
                txtbacsythuchien.SetId(globalVariables.gv_intIDNhanvien);
            }
        }
        private void FrmPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            txt_chandoan_truoc.Init();
            txt_chandoan_sau.Init();
            txt_pptt.Init();
            txt_loaipt.Init();
            txt_ppvocam.Init();
            txt_luocdo_danluu.Init();
            txt_luocdo_bac.Init();
            txt_luocdo_khac.Init();
            txt_ytaphu.Init();
            txt_bacsy_gayme.Init();
            txt_ptvien.Init();
            LoadBacsythuchien();
            LoadThongTin();
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void cmdInPhieu_Click(object sender, EventArgs e)
        {

        }


    }
}
