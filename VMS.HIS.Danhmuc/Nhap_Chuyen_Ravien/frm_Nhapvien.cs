using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using VMS.HIS.Danhmuc.Dungchung;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Nhapvien : Form
    {
        public KcbLuotkham objLuotkham;
        public Int64 id_kham=-1;
        public short id_bskham=-1;
        public bool b_Cancel = false;
        public bool CallfromParent = false;
        public bool isReadonly = false;
        
        public frm_Nhapvien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyDown += new KeyEventHandler(frm_Nhapvien_KeyDown);
            dtNgayNhapVien.Value = globalVariables.SysDate;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            chkThoatngaysaukhinhapvien.CheckedChanged += new EventHandler(chkThoatngaysaukhinhapvien_CheckedChanged);
            if (globalVariables.gv_TuSinhSo_BA)
            {
                cmdTuSinh.Enabled = false;
                txtSovaovien.ReadOnly = true;
            }
            InitEvents();
        }
        public void SetReadOnly()
        {
            cmdAccept.Enabled = cmdHUY_VAO_VIEN.Enabled = false;
            ucThongtinnguoibenh1.txtMaluotkham.Enabled = false;
            ucThongtinnguoibenh1.cmdSearch.Enabled = false;
        }
        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                getData();
                txtKhoanoitru.Focus();
            }
        }
        void InitEvents()
        {
            autoLydovv._OnShowData += autoLydovv__OnShowData;
            autoGhichuvaovien._OnShowData += autoGhichuvaovien__OnShowData;
            autoGiadinh._OnShowData += autoGiadinh__OnShowData;
            autoBanthan._OnShowData += autoBanthan__OnShowData;
            autoTainanthuongtich._OnShowData += autoTainanthuongtich__OnShowData;
            autoLydovv._OnSaveAsV1 += autoLydovv__OnSaveAsV1;
            FormClosing += frm_Nhapvien_FormClosing;
        }

        void frm_Nhapvien_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void autoLydovv__OnSaveAsV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            if (Utility.DoTrim(obj.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, obj.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }   
        }

        void autoTainanthuongtich__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(autoTainanthuongtich.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoTainanthuongtich.myCode;
                autoTainanthuongtich.Init();
                autoTainanthuongtich.SetCode(oldCode);
                autoTainanthuongtich.Focus();
            }
        }

        void autoBanthan__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(autoBanthan.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoBanthan.myCode;
                autoBanthan.Init();
                autoBanthan.SetCode(oldCode);
                autoBanthan.Focus();
            }
        }

        void autoGiadinh__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(autoGiadinh.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoGiadinh.myCode;
                autoGiadinh.Init();
                autoGiadinh.SetCode(oldCode);
                autoGiadinh.Focus();
            }
        }

        void autoGhichuvaovien__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(autoGhichuvaovien.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoGhichuvaovien.myCode;
                autoGhichuvaovien.Init();
                autoGhichuvaovien.SetCode(oldCode);
                autoGhichuvaovien.Focus();
            }
        }

        void autoLydovv__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(autoLydovv.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = autoLydovv.myCode;
                autoLydovv.Init();
                autoLydovv.SetCode(oldCode);
                autoLydovv.Focus();
            }
        }
        void chkThoatngaysaukhinhapvien_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.ThoatNgaysaukhiNhapvienthanhcong = chkThoatngaysaukhinhapvien.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }

        void frm_Nhapvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdAccept_Click(cmdAccept, new EventArgs());
            if (e.Control && e.KeyCode == Keys.D) cmdHUY_VAO_VIEN_Click(cmdHUY_VAO_VIEN, new EventArgs());
        }
        private void txtGhiChu_LostFocus(object sender, EventArgs e)
        {
            //if (objLuotkham != null && Utility.Int32Dbnull( objLuotkham.IdKhoanoitru,-1)!=-1)
            //{
            //    new Update(KcbLuotkham.Schema)
            //  .Set(KcbLuotkham.Columns.MotaNhapvien).EqualTo(txtGhiChu.Text)
            //  .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
            //  .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
            //  .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //  .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
            //}
          
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPHIEU_VAOVIEN_Click(object sender, EventArgs e)
        {
            
            IN_PHIEU_KHAM_VAO_VIEN();
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSovaovien.Text = Utility.sDbnull(THU_VIEN_CHUNG.LaysoVaovien());// LaySoBenhAn());
        }

        /// <summary>
        /// hàm thực hiện việc lấy thôn gtin của dữ liệu
        /// </summary>
        private void getData()
        {
           objLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
           if (objLuotkham != null)
           {
                 if (id_bskham <= 0) id_bskham = Utility.Int16Dbnull(objLuotkham.IdBacsiNhapvien, -1);
               txtKhoanoitru.SetId(objLuotkham.IdKhoanoitru);
               if (!string.IsNullOrEmpty(objLuotkham.NgayNhapvien.ToString()))
               {
                   dtNgayNhapVien.Value = Convert.ToDateTime(objLuotkham.NgayNhapvien);
               }
               else
               {
                   dtNgayNhapVien.Value = globalVariables.SysDate;
               }
               string chandoan = SPs.KcbLaythongtinchandoannhapvien(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan).GetDataSet().Tables[0].Rows[0]["ChanDoan"].ToString();
               txtChandoanbandau.Text = chandoan;
               if (string.IsNullOrEmpty(chandoan))
               {
                   txtChandoanbandau.Text = Utility.sDbnull(objLuotkham.TrieuChung);
               }
               txtNguoiLienhe.Text = objLuotkham.NguoiLienhe;
               txtSDTLienhe.Text = objLuotkham.DienthoaiLienhe;
               txtDiachiLienhe.Text = objLuotkham.DiachiLienhe;
               autoGhichuvaovien._Text = Utility.sDbnull(objLuotkham.MotaNhapvien);
               KcbChandoanKetluan objCDKL = new Select().From(KcbChandoanKetluan.Schema)
                   .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                   .And(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(id_kham)
                   .ExecuteSingle<KcbChandoanKetluan>();
               //txtSoBenhAn.Text = objLuotkham.SoBenhAn;
               //Nạp các thông tin khác từ chẩn đoán kết luận
               if (objCDKL != null)
               {
                   txtQuanlybenhly.Text = objCDKL.QuatrinhBenhly;
                   autoBanthan._Text = objCDKL.TiensuBenh;
                   txtTTKQLamSang.Text = objCDKL.TomtatCls;
                   txtMach.Text = objCDKL.Mach;
                   txtha.Text = objCDKL.Huyetap;
                   txtNhietDo.Text = objCDKL.Nhietdo;
                   txtChieucao.Text = objCDKL.Chieucao;
                   txtCannang.Text = objCDKL.Cannang;
                   txtNhiptho.Text = objCDKL.Nhiptho;
                   txtSPO2.Text = objCDKL.SPO2;
                   txtBmi.Text =Utility.sDbnull( objCDKL.ChisoIbm,"0");
                   txtNhommau._Text = ucThongtinnguoibenh1.objBenhnhan.NhomMau;
               }
               EmrPhieukhambenh objPKTT = new Select().From(EmrPhieukhambenh.Schema)
                  .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .ExecuteSingle<EmrPhieukhambenh>();
               if (objPKTT != null)
               {
                   txtCacbophan.Text = objPKTT.BoPhan;
                   autoToanthan._Text = objPKTT.ToanThan;
               }
               txtSovaovien.Text = string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoVaovien, ""))
                   ? THU_VIEN_CHUNG.LaysoVaovien()
                   : Utility.sDbnull(objLuotkham.SoVaovien, "");
              
               NoitruPhieunhapvien objphieu = new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
               if (objphieu != null)
               {
                   if (id_kham <= 0) id_kham =Utility.Int64Dbnull( objphieu.IdKham,-1);
                   txtSovaovien.Text = objphieu.SoVaovien;
                   txtKhoanoitru.Enabled = false;
                   txtChandoanbandau.Text = Utility.sDbnull(objphieu.ChandoanVaovien);
                   //autoLydovv.SetCode(objphieu.MaLydoNhapvien);
                   autoLydovv._Text=objphieu.LydoNhapvien;
                   txtQuanlybenhly.Text = objphieu.QuatrinhBenhly;
                   autoBanthan._Text = objphieu.TsuBanthan;
                   autoGiadinh._Text = objphieu.TsuGiadinh;
                   autoToanthan._Text = objphieu.KhamToanthan;
                   txtCacbophan.Text = objphieu.BphanKhac;
                   txtTTKQLamSang.Text = objphieu.TtKquaLamsang;
                   txtDaXuLy.Text = objphieu.DaXuly;
                   txtBacsi.SetId(objLuotkham.IdBacsiNhapvien);
                   txtChovaokhoa._Text = objphieu.ChovaoKhoa;
                   txtMach.Text = objphieu.Mach;
                   txtha.Text = objphieu.Huyetap;
                   txtNhietDo.Text = objphieu.Nhietdo;
                   txtChieucao.Text = objphieu.Chieucao;
                   txtCannang.Text = objphieu.Cannang;
                   txtBmi.Text = objphieu.Bmi;
                   txtNhommau._Text = ucThongtinnguoibenh1.objBenhnhan.NhomMau;
                   txtNhiptho.Text = objphieu.Nhiptho;
                   txtSPO2.Text = objphieu.SPO2;
                   autoTainanthuongtich.SetCode(objphieu.MaTainanthuongtich);
               }
               else//Điền thêm thông tin kết quả CLS, thuốc kê đơn
               {
                   DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan), (byte)0);
                   DataTable dtThuoc = ds.Tables[0];
                   DataTable dtketqua = ds.Tables[1];
                   DataTable dtketquaCDHA = ds.Tables[2];
                   string[] query = (from thuoc in dtThuoc.AsEnumerable()
                                     let y = Utility.sDbnull(thuoc["ten_thuoc"])
                                     select y).ToArray();
                   string donthuoc = string.Join(";", query);
                   string[] querykq = (from kq in dtketqua.AsEnumerable()
                                       let y = Utility.sDbnull(kq["ketqua"])
                                       select y).ToArray();
                   string ketquaCLS = string.Join("; ", querykq);
                   querykq = (from kq in dtketquaCDHA.AsEnumerable()
                                       let y = Utility.sDbnull(kq["ket_qua"])
                                       select y).ToArray();
                   string ketquaCDHA = string.Join("; ", querykq);
                   bool donthuoclaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_THUOCDADUNG_LAYTUBANGDULIEU", "0", true) == "1";
                   bool chandoanlaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_CHANDOAN_LAYTUBANGDULIEU", "0", true) == "1";
                   bool kqclslaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_KQCLS_LAYTUBANGDULIEU", "0", true) == "1";
                   if (donthuoclaytubangdulieu)
                       txtDaXuLy.Text = donthuoc;
                   if (chandoanlaytubangdulieu)
                   {
                       SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                  .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                  .OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
                       var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                       string machandoan = "";
                       string mabenh = "";
                       string phongkhamvaovien = "";
                       string khoanoitru = "";
                       string ten_benhcp = "";
                       foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                       {
                           string ICD_Name = "";
                           string ICD_Code = "";
                           GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                                       Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                           chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                           ? ICD_Name
                                           : Utility.sDbnull(objDiagInfo.Chandoan);
                           mabenh += ICD_Code;
                           ten_benhcp += ICD_Name;
                       }
                       //DataTable dtDataChandoan = SPs.ThamkhamLaythongtinchandoan(machandoan).GetDataSet().Tables[0];
                       //txtkbMa.Text = Utility.sDbnull(mabenh);
                       //if (dtDataChandoan.Rows.Count > 0) chandoan = Utility.sDbnull(dtDataChandoan.Rows[0][0], "");
                       chandoan += "," + ten_benhcp;
                       txtChandoanbandau.Text = string.Format("{0}:{1}", mabenh, ten_benhcp);
                   }
                   if (kqclslaytubangdulieu)
                   {
                       if (Utility.sDbnull(ketquaCDHA).Length > 0 && Utility.sDbnull(ketquaCLS).Length > 0)
                           txtTTKQLamSang.Text = string.Format("{0}\n{1}", ketquaCDHA, ketquaCLS);
                       else if (Utility.sDbnull(ketquaCDHA).Length <= 0 && Utility.sDbnull(ketquaCLS).Length > 0)
                           txtTTKQLamSang.Text = ketquaCLS;
                       else if (Utility.sDbnull(ketquaCDHA).Length > 0 && Utility.sDbnull(ketquaCLS).Length <=0)
                           txtTTKQLamSang.Text = ketquaCDHA;
                   }
               }
           }
           else
           {
               Utility.SetMsg(lblMsg, "Chưa chọn bệnh nhân để nhập viện", true);
           }
        }
        private void ModifyCommand()
        {
            try
            {
                cmdPrint.Enabled = Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, -1) > 0;
               // cmdAccept.Enabled =Utility.Int32Dbnull( objLuotkham.TrangthaiNoitru ,-1)<= 0;
                cmdHUY_VAO_VIEN.Enabled = Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, -1) >= 1;

                cmdAccept.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                cmdAccept.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
            }
            catch (Exception)
            {
            }

        }
        private bool InValiHuyVaoVien()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (!Utility.Coquyen("noitru_nhapvien_quyenhuy"))
            {
                Utility.ShowMsg("Bạn không có quyền hủy phiếu nhập viện(noitru_nhapvien_quyenhuy). Đề nghị liên hệ quản trị hệ thống để được cấp quyền");
                return false;
            }
            KcbLuotkham objKcbLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru <=0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân chưa ở trạng thái nội trú nên bạn không thể hủy nhập viện. Vui lòng kiểm tra lại", true);
                return false;
            }
            DataTable dtCheckData = new Select().From(NoitruPhieudieutri.Schema)
                .Where(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(objKcbLuotkham.IdBenhnhan).ExecuteDataSet().Tables[0];
            if (dtCheckData.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã lập phiếu điều trị nên không thể hủy nhập viện được. Vui lòng kiểm tra lại", true);
                return false;
            }
            dtCheckData = new Select().From(NoitruPhieuchamsoc.Schema)
                .Where(NoitruPhieuchamsoc.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham)
                .And(NoitruPhieuchamsoc.Columns.IdBenhnhan).IsEqualTo(objKcbLuotkham.IdBenhnhan).ExecuteDataSet().Tables[0];
            if (dtCheckData.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã lập phiếu chăm sóc nên không thể hủy nhập viện được. Vui lòng kiểm tra lại", true);
                return false;
            }
            dtCheckData = new Select().From(NoitruPhieutheodoiChucnangsong.Schema)
                .Where(NoitruPhieutheodoiChucnangsong.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham)
                .And(NoitruPhieutheodoiChucnangsong.Columns.IdBenhnhan).IsEqualTo(objKcbLuotkham.IdBenhnhan).ExecuteDataSet().Tables[0];
            if (dtCheckData.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã lập phiếu theo dõi nên không thể hủy nhập viện được. Vui lòng kiểm tra lại", true);
                return false;
            }
            dtCheckData = new Select().From(KcbChandoanKetluan.Schema)
                .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham)
                .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objKcbLuotkham.IdBenhnhan)
                .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(1)
                .ExecuteDataSet().Tables[0];
            if (dtCheckData.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã nhập chẩn đoán nội trú hằng ngày nên không thể hủy nhập viện được. Vui lòng kiểm tra lại", true);
                return false;
            }

            if (objKcbLuotkham != null && objKcbLuotkham.NgayRavien.HasValue)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã ra viện nên không thể hủy nhập viện được. Vui lòng kiểm tra lại", true);
                return false;
            }
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru >1)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã có dữ liệu nội trú nên không thể hủy nhập viện được. Vui lòng kiểm tra lại", true);
                return false;
            }
           
            NoitruPhanbuonggiuongCollection objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
               .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
               .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể hủy thông tin nhập viện", true);
                return false;
            }
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 && Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã phân buồng giường nên bạn không thể hủy thông tin nhập viện ", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }


            DataTable dtTU = SPs.NoitruTimkiemlichsuNoptientamung(objKcbLuotkham.MaLuotkham, objKcbLuotkham.IdBenhnhan, 0, -1, 1).GetDataSet().Tables[0];
            if (dtTU.Select("tthai_huy=0").Length > 0)
            {
                Utility.ShowMsg("Người bệnh đã phát sinh tiền tạm ứng nội trú. Đề nghị hủy tạm ứng trước khi hủy nhập viện");
                return false;
            }
            return true;
        }
        private void cmdHUY_VAO_VIEN_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!InValiHuyVaoVien()) return;
                if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc hủy nhập viện cho bệnh nhân nội trú không,\n Nếu hủy thì bệnh nhân sẽ trở lại ngoại trú,Thông tin gói dịch vụ nội trú sẽ bị xóa theo ?", "Thông báo", true))
                {
                    if (objLuotkham != null)
                    {
                        ActionResult actionResult =
                      new noitru_nhapvien().Huynhapvien(
                     objLuotkham);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(Name, globalVariables.UserName, string.Format("Bệnh nhân có mã lần khám {0} và mã bệnh nhân {1} được hủy nhập viện bởi {2} ", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                                objLuotkham.TrangthaiNoitru = 0;
                                objLuotkham.SoBenhAn = string.Empty;
                                objLuotkham.NgayNhapvien = null;
                                objLuotkham.IdKhoanoitru = -1;
                                objLuotkham.MotaNhapvien = "";
                                objLuotkham.IdNhapvien = -1;
                                txtSovaovien.Clear();
                                ModifyCommand();
                                Utility.SetMsg(lblMsg, "Bạn hủy nội trú cho bệnh nhân thành công", false);
                                break;
                            case ActionResult.Error:
                                Utility.SetMsg(lblMsg, "Lỗi trong quá trình hủy nội trú cho bệnh nhân", true);
                                break;
                        }
                    }
                }
            }
            catch
            { }
            finally
            {
                ModifyCommand();
            }
        }

        private void AutocompleteKhoanoitru()
        {
            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            txtKhoanoitru.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            //try
            //{
            //    if (mDtKhoaNoitru == null) return;
            //    if (!mDtKhoaNoitru.Columns.Contains("ShortCut"))
            //        mDtKhoaNoitru.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            //    foreach (DataRow dr in mDtKhoaNoitru.Rows)
            //    {
            //        string shortcut = "";
            //        string realName = dr[DmucKhoaphong.Columns.TenKhoaphong].ToString().Trim() + " " +
            //                          Utility.Bodau(dr[DmucKhoaphong.Columns.TenKhoaphong].ToString().Trim());
            //        shortcut = dr[DmucKhoaphong.Columns.MaKhoaphong].ToString().Trim();
            //        string[] arrWords = realName.ToLower().Split(' ');
            //        string _space = "";
            //        string _Nospace = "";
            //        foreach (string word in arrWords)
            //        {
            //            if (word.Trim() != "")
            //            {
            //                _space += word + " ";
            //                //_Nospace += word;
            //            }
            //        }
            //        shortcut += _space; // +_Nospace;
            //        foreach (string word in arrWords)
            //        {
            //            if (word.Trim() != "")
            //                shortcut += word.Substring(0, 1);
            //        }
            //        dr["ShortCut"] = shortcut;
            //    }
            //    var source = new List<string>();
            //    var query = from p in mDtKhoaNoitru.AsEnumerable()
            //                select p[DmucKhoaphong.Columns.IdKhoaphong].ToString() + "#" + p[DmucKhoaphong.Columns.MaKhoaphong].ToString() + "@" + p[DmucKhoaphong.Columns.TenKhoaphong].ToString() + "@" + p["shortcut"].ToString();
            //    source = query.ToList();
            //    this.txtKhoanoitru.AutoCompleteList = source;
            //    this.txtKhoanoitru.TextAlign = HorizontalAlignment.Left;
            //    this.txtKhoanoitru.CaseSensitive = false;
            //    this.txtKhoanoitru.MinTypedCharacters = 1;
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }
        void LoadUserConfigs()
        {
            try
            {
                chkInNgaySauKhiNhapVien.Checked = Utility.getUserConfigValue(chkInNgaySauKhiNhapVien.Tag.ToString(), Utility.Bool2byte(chkInNgaySauKhiNhapVien.Checked)) == 1;
                chkThoatngaysaukhinhapvien.Checked = Utility.getUserConfigValue(chkThoatngaysaukhinhapvien.Tag.ToString(), Utility.Bool2byte(chkThoatngaysaukhinhapvien.Checked)) == 1;
                chkUpdateDonthuocCLS.Checked = Utility.getUserConfigValue(chkUpdateDonthuocCLS.Tag.ToString(), Utility.Bool2byte(chkUpdateDonthuocCLS.Checked)) == 1;
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
                Utility.SaveUserConfig(chkUpdateDonthuocCLS.Tag.ToString(), Utility.Bool2byte(chkUpdateDonthuocCLS.Checked));
                Utility.SaveUserConfig(chkInNgaySauKhiNhapVien.Tag.ToString(), Utility.Bool2byte(chkInNgaySauKhiNhapVien.Checked));
                Utility.SaveUserConfig(chkThoatngaysaukhinhapvien.Tag.ToString(), Utility.Bool2byte(chkThoatngaysaukhinhapvien.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void frm_Nhapvien_Load(object sender, EventArgs e)
        {
            txtNhommau.Init();
            autoBanthan.Init();
            autoTainanthuongtich.Init();
            autoToanthan.Init();
            autoLydovv.Init();
            autoGiadinh.Init();
            autoGhichuvaovien.Init();
            AutocompleteKhoanoitru();
            
            if (CallfromParent)
                ucThongtinnguoibenh1.Refresh();
            else
                ucThongtinnguoibenh1.txtMaluotkham.Focus();
            LaydanhsachbacsiChidinh();
            ModifyCommand();
            chkInNgaySauKhiNhapVien.Checked = PropertyLib._NoitruProperties.InphieuNhapvienNgaysaukhiNhapvien;
            chkThoatngaysaukhinhapvien.Checked = PropertyLib._NoitruProperties.ThoatNgaysaukhiNhapvienthanhcong;
            txtKhoanoitru.Focus();
            LoadUserConfigs();
            if (isReadonly) SetReadOnly();

        }
        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                //   m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (id_bskham > 0)
                {
                    txtBacsi.SetId(id_bskham);
                }
                else
                {
                    if (globalVariables.gv_intIDNhanvien <= 0)
                    {
                        txtBacsi.SetId(-1);
                    }
                    else
                    {
                        txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                    }
                }
                if (globalVariables.IsAdmin)
                {
                    txtBacsi.Enabled = true;
                }
                else
                {
                    txtBacsi.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }
        private NoitruPhanbuonggiuong TaoBuonggiuong()
        {
            var objNoitruPhanbuonggiuong = new NoitruPhanbuonggiuong();
            objNoitruPhanbuonggiuong.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objNoitruPhanbuonggiuong.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objNoitruPhanbuonggiuong.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
            objNoitruPhanbuonggiuong.NgayTao = globalVariables.SysDate;
            objNoitruPhanbuonggiuong.NgayVaokhoa = dtNgayNhapVien.Value;
            objNoitruPhanbuonggiuong.IdKham = id_kham;
            objNoitruPhanbuonggiuong.NguoiTao = globalVariables.UserName;
            objNoitruPhanbuonggiuong.IdBacsiChidinh = Utility.Int16Dbnull(txtBacsi.MyID);
            objNoitruPhanbuonggiuong.NoiTru = 1;
            objNoitruPhanbuonggiuong.KieuGiuong = 0;
            objNoitruPhanbuonggiuong.TrangthaiThanhtoan = 0;
            objNoitruPhanbuonggiuong.TrangThai = 0;
            objNoitruPhanbuonggiuong.DuyetBhyt = 0;
            objNoitruPhanbuonggiuong.CachtinhSoluong = 0;
            objNoitruPhanbuonggiuong.SoluongGio = 0;
            return objNoitruPhanbuonggiuong;
        }
        private bool IsValidData()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                Utility.ResetMessageError(errorProvider1);
                if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải chọn khoa nội trú", true);
                    txtKhoanoitru.Focus();
                    return false;
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_PHIEUNHAPVIEN_CHOPHEPSUA_TRONGNOITRU", "0", true) == "0")
                {
                    if (!globalVariables.isSuperAdmin)
                    {
                        var objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                           .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                           .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                        if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
                        {
                            Utility.SetMsg(lblMsg, "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể cập nhật lại thông tin nhập viện", true);
                            return false;
                        }
                        SqlQuery sqlQuery2 = new Select().From(NoitruPhieudieutri.Schema)
                          .Where(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
                        if (sqlQuery2.GetRecordCount() > 0)
                        {
                            Utility.SetMsg(lblMsg, "Bệnh nhân đã có phiếu điều trị nên bạn không thể cập nhật lại thông tin nhập viện", true);
                            return false;
                        }
                        if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 && Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
                        {
                            Utility.ShowMsg("Bệnh nhân đã phân buồng giường nên bạn không thể cập nhật lại thông tin nhập viện", "Thông báo", MessageBoxIcon.Warning);
                            return false;
                        }
                        objLuotkham = new Select().From(KcbLuotkham.Schema)
                          .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                          .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();

                        if (objLuotkham != null && objLuotkham.TrangthaiNoitru > 1)
                        {
                            Utility.SetMsg(lblMsg, "Bệnh nhân đã có dữ liệu nội trú nên bạn không thể cập nhật lại thông tin nhập viện. Vui lòng kiểm tra lại ", true);
                            return false;
                        }
                    }
                }
                //if  (Utility.DoTrim(autoLydovv.Text).Length<=0)
                //{
                //    Utility.SetMsg(lblMsg, "Bạn phải chọn lý do nhập viện .", true);
                //    autoLydovv.Focus();
                //    return false;
                //}
                objPhieuNv = Taophieunhapvien();
                if (objPhieuNv.IdPhieu > 0)
                {
                    if (!InValiUpdateNgayNhapVien()) return false;
                    if (Utility.Coquyen("noitru_suaphieunhapvien") || globalVariables.UserName == objPhieuNv.NguoiTao)
                    {

                    }
                    else
                    {
                        Utility.ShowMsg("Bệnh nhân này được nhập viện bởi bác sĩ khác nên bạn không được phép sửa dữ liệu. Liên hệ quản trị hệ thống để được cấp thêm quyền (noitru_suaphieunhapvien)", "Thông báo");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
           
        }
        private bool InValiUpdateNgayNhapVien()
        {
           
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Không tồn tại bệnh nhân này", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            if (!globalVariables.isSuperAdmin)
            {
                if (objLuotkham.TrangthaiNoitru >= 3)
                {
                    Utility.ShowMsg("Bệnh nhân này đã làm thủ tục ra viện, Mời bạn xem lại thông tin ", "Thông báo", MessageBoxIcon.Error);
                    return false;
                }
            }
            //Tạm khóa 240419
            //NoitruPhanbuonggiuong bg = new Select().From(txtSovaovien.Schema)
            //  .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //  .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //  .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
            //  .AndExpression(NoitruPhanbuonggiuong.Columns.IdKhoachuyen).IsNull().Or(NoitruPhanbuonggiuong.Columns.IdKhoachuyen).IsEqualTo(-1).CloseExpression()
            //  .OrderAsc(NoitruPhanbuonggiuong.Columns.NgayPhangiuong).ExecuteSingle<NoitruPhanbuonggiuong>();
            //if (dtNgayNhapVien.Value > bg.NgayPhangiuong)
            //{
            //    Utility.ShowMsg(string.Format("Ngày vào viện {0} không được sau ngày phân buồng giường lần đầu {1}.\nVui lòng kiểm tra lại thông tin", dtNgayNhapVien.Value.ToString("dd/MM/yyyy HH:mm:ss"), bg.NgayPhangiuong.Value.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo", MessageBoxIcon.Warning);
            //    dtNgayNhapVien.Focus();
            //    return false;
            //}
            //Kiểm tra theo thời gian của công khám nhập viện
            KcbDangkyKcb objcongkhamNV = KcbDangkyKcb.FetchByID(objLuotkham.IdCongkhamNhapvien);
            if (objcongkhamNV != null)
            {
                if (dtNgayNhapVien.Value < objcongkhamNV.ThoigianKetthuc)
                {
                    Utility.ShowMsg(string.Format("Ngày vào viện không thể nhỏ hơn thời gian kết thúc công khám {0}", objcongkhamNV.ThoigianKetthuc.Value.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo", MessageBoxIcon.Warning);
                    dtNgayNhapVien.Focus();
                    return false;
                }
            }
            NoitruPhieudieutri phieudieutri = new Select().From(NoitruPhieudieutri.Schema)
              .Where(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .OrderAsc(NoitruPhieudieutri.Columns.NgayDieutri)
              .ExecuteSingle<NoitruPhieudieutri>();
            if (phieudieutri != null)
            {
                
                string gioidieutri=Utility.DoTrim(phieudieutri.GioDieutri);
                int hours=Utility.Int32Dbnull( gioidieutri.Split(':')[0],0);
                int minutes=Utility.Int32Dbnull( gioidieutri.Split(':')[1],0);
                 int seconds=Utility.Int32Dbnull( gioidieutri.Split(':')[2],0);
                 DateTime ngaydieutri = new DateTime(phieudieutri.NgayDieutri.Value.Year, phieudieutri.NgayDieutri.Value.Month, phieudieutri.NgayDieutri.Value.Day, hours, minutes, seconds);
                if (dtNgayNhapVien.Value > ngaydieutri)
                {
                    Utility.ShowMsg(string.Format("Ngày vào viện {0} không thể sau ngày lập phiếu điều trị lần đầu {1}.\nVui lòng kiểm tra lại thông tin", dtNgayNhapVien.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngaydieutri.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo", MessageBoxIcon.Warning);
                    dtNgayNhapVien.Focus();
                    return false;
                }
            }
            if (dtNgayNhapVien.Value < objLuotkham.NgayTiepdon)
            {
                Utility.ShowMsg(string.Format("Ngày vào viện không thể nhỏ hơn ngày tiếp đón khám bệnh {0}", objLuotkham.NgayTiepdon.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo", MessageBoxIcon.Warning);
                dtNgayNhapVien.Focus();
                return false;
            }
           
            return true;
        }
        NoitruPhieunhapvien Taophieunhapvien()
        {
            NoitruPhieunhapvien objphieu = new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
            if (objphieu == null)
            {
                objphieu = new NoitruPhieunhapvien();
                objphieu.SoVaovien = Utility.sDbnull(txtSovaovien.Text);
                objphieu.NgayTao = DateTime.Now;
                objphieu.NguoiTao = globalVariables.UserName;
                objphieu.IsNew = true;
            }
            else
            {
                objphieu.NgaySua = DateTime.Now;
                objphieu.NguoiSua = globalVariables.UserName;
                objphieu.MarkOld();
            }
            objphieu.ChovaoKhoa = Utility.sDbnull(txtChovaokhoa.Text);
            objphieu.NgayNhapvien = dtNgayNhapVien.Value;
            objphieu.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objphieu.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objphieu.IdKhoanoitru = objLuotkham.IdKhoanoitru;
            objphieu.ChandoanVaovien = txtChandoanbandau.Text;
            objphieu.QuatrinhBenhly = txtQuanlybenhly.Text;
            objphieu.TsuBanthan = autoBanthan.Text;
            objphieu.TsuGiadinh = autoGiadinh.Text;
            objphieu.MaLydoNhapvien = autoLydovv.MyCode;
            objphieu.LydoNhapvien = autoLydovv.Text;
            objphieu.KhamToanthan = autoToanthan.Text;
            objphieu.MaTainanthuongtich = autoTainanthuongtich.MyCode;
            objphieu.BphanKhac = txtCacbophan.Text;
            objphieu.TtKquaLamsang = txtTTKQLamSang.Text;
            objphieu.DaXuly = txtDaXuLy.Text;
            objphieu.Mach = Utility.sDbnull(txtMach.Text);
            objphieu.Nhiptho = Utility.sDbnull(txtNhiptho.Text);
            objphieu.Huyetap = Utility.sDbnull(txtha.Text);
            objphieu.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
            objphieu.Chieucao = Utility.sDbnull(txtChieucao.Text);
            objphieu.Cannang = Utility.sDbnull(txtCannang.Text);
            objphieu.SPO2 = Utility.sDbnull(txtSPO2.Text);
            objphieu.Bmi = Utility.sDbnull(txtBmi.Text);

            objphieu.IdKham = id_kham;
            return objphieu;

        }
        NoitruPhieunhapvien objPhieuNv = null;
        /// <summary>
        /// hàm thuwchj hiện việc nhập viện không có gói, gói sẽ là nulll cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                bool question = true;
                if (!IsValidData()) return;
                //txtSoBenhAn.Text = string.IsNullOrEmpty(Utility.sDbnull(txtSoBenhAn.Text, ""))
                //    ? THU_VIEN_CHUNG.LaysoVaovien()
                //    : Utility.sDbnull(txtSoBenhAn.Text, "-1");
                //if (Utility.AcceptQuestion("Bạn có muốn nhập viện cho bệnh nhân này không","Thông báo nhập viện",true))
                //{
                objLuotkham.IdBacsiNhapvien = Utility.Int16Dbnull(txtBacsi.MyID);
                objLuotkham.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
                objLuotkham.NgayNhapvien = dtNgayNhapVien.Value;
                objLuotkham.SoVaovien = Utility.sDbnull(txtSovaovien.Text);
                objLuotkham.MotaNhapvien = Utility.DoTrim(autoGhichuvaovien.Text);
                objLuotkham.IdCongkhamNhapvien = id_kham;
                objLuotkham.DiachiLienhe = txtDiachiLienhe.Text;
                objLuotkham.DienthoaiLienhe = txtSDTLienhe.Text;
                objLuotkham.NguoiLienhe = txtNguoiLienhe.Text;
                ActionResult actionResult = new noitru_nhapvien().Nhapvien(TaoBuonggiuong(), objLuotkham, Taophieunhapvien(),null);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        
                        txtSovaovien.Text = Utility.sDbnull(objLuotkham.SoVaovien, "");
                        objLuotkham.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
                        objLuotkham.TrangthaiNoitru = 1;
                        objLuotkham.SoVaovien = Utility.sDbnull(txtSovaovien.Text);
                        objLuotkham.NgayNhapvien = dtNgayNhapVien.Value;
                        objLuotkham.MotaNhapvien = Utility.DoTrim(autoGhichuvaovien.Text);

                        b_Cancel = true;
                        Utility.SetMsg(lblMsg,
                            Utility.sDbnull(cmdAccept.Tag, "0") == "0"
                                ? "Bạn thực hiện Nhập viện cho bệnh nhân thành công"
                                : "Bạn thực hiện cập nhật Phiếu nhập viện cho bệnh nhân thành công", false);
                        // this.Close();
                        if (chkInNgaySauKhiNhapVien.Checked)
                        {
                            IN_PHIEU_KHAM_VAO_VIEN();
                        }
                        if (chkThoatngaysaukhinhapvien.Checked) this.Close();
                        break;
                    case ActionResult.Error:
                        Utility.SetMsg(lblMsg, "Lỗi trong quá trình nhập viện ", true);
                        break;
                    case ActionResult.ExistedRecord:
                        Utility.SetMsg(lblMsg, "Bệnh nhân đã thanh toán, Mời bạn xem lại ", true);
                        break;

                }
                //}

            }
            catch (Exception ex)
            {
                Utility.sDbnull("Lỗi:" + ex.Message);
            }

            finally
            {
                ModifyCommand();
            }
          
           
          
        }

        private void IN_PHIEU_KHAM_VAO_VIEN()
        {
            DataTable dsTable =
               new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            if (dsTable.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
                return;
            }

            SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                  .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                  .OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
            var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
            string chandoan = "";
            string machandoan = "";
            string mabenh = "";
            string phongkhamvaovien = "";
            string khoanoitru = "";
            string ten_benhcp = "";
            foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                            Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                ? ICD_Name
                                : Utility.sDbnull(objDiagInfo.Chandoan);
                mabenh += ICD_Code;
                ten_benhcp += ICD_Name;
            }
            //DataTable dtDataChandoan = SPs.ThamkhamLaythongtinchandoan(machandoan).GetDataSet().Tables[0];
            //txtkbMa.Text = Utility.sDbnull(mabenh);
            //if (dtDataChandoan.Rows.Count > 0) chandoan = Utility.sDbnull(dtDataChandoan.Rows[0][0], "");
            chandoan += "," + ten_benhcp;
            DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan),(byte)0);
            DataTable dtThuoc = ds.Tables[0];
            DataTable dtketqua = ds.Tables[1];

            string[] query = (from thuoc in dtThuoc.AsEnumerable()
                              let y = Utility.sDbnull(thuoc["ten_thuoc"])
                              select y).ToArray();
            string donthuoc = string.Join(";", query);
            string[] querykq = (from kq in dtketqua.AsEnumerable()
                                let y = Utility.sDbnull(kq["ketqua"])
                                select y).ToArray();
            string ketquaCLS = string.Join("; ", querykq);

            bool tudongnaplai_thuoc_cls_khiin = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_TUDONGNAP_THUOC_KQCLS_KHIIN", "0", true) == "1";
            bool donthuoclaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_THUOCDADUNG_LAYTUBANGDULIEU", "0", true) == "1";
            bool chandoanlaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_CHANDOAN_LAYTUBANGDULIEU", "0", true) == "1";
            bool kqclslaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_KQCLS_LAYTUBANGDULIEU", "0", true) == "1";
            //foreach (DataRow dr in dsTable.Rows)
            //{
            if (tudongnaplai_thuoc_cls_khiin)
            {
                DataRow dr = dsTable.Rows[0];
                if (dr != null)
                {
                    if (donthuoclaytubangdulieu)
                        dr["thuockedon"] = donthuoc;
                    if (chandoanlaytubangdulieu)
                        dr["CHANDOAN_VAOVIEN"] = chandoan;
                    if (kqclslaytubangdulieu)
                        dr["KETQUA_CLS"] = ketquaCLS;
                }
            }
            dsTable.AcceptChanges();
            VNS.HIS.UI.Baocao.noitru_baocao.Inphieunhapvien(dsTable, "PHIẾU NHẬP VIỆN", globalVariables.SysDate);
        }
        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }

        private void chkInNgaySauKhiNhapVien_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.InphieuNhapvienNgaysaukhiNhapvien = chkInNgaySauKhiNhapVien.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }

        private void cmdRefreshChucnangsong_Click(object sender, EventArgs e)
        {
            try
            {
                frm_XemthongtinChucnangsong _XemthongtinChucnangsong = new frm_XemthongtinChucnangsong(objLuotkham, true, 100);
                _XemthongtinChucnangsong._OnSelectMe += _XemthongtinChucnangsong__OnSelectMe;
                _XemthongtinChucnangsong.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void _XemthongtinChucnangsong__OnSelectMe(string mach, string nhietdo, string nhiptho, string huyetap, string chieucao, string cannang, string bmi, string nhommau,string Spo2)
        {
            txtMach.Text = mach;
            txtNhietDo.Text = nhietdo;
            txtNhiptho.Text = nhiptho;
            txtha.Text = huyetap;
            txtChieucao.Text = chieucao;
            txtCannang.Text = cannang;
            txtBmi.Text = bmi;
            txtNhommau._Text = nhommau;
            txtSPO2.Text = Spo2;
        }
    }
}
