using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using System.Diagnostics;
using System.IO;
using Aspose.Words;

namespace VMS.HIS.Danhmuc.UserControls
{
    public partial class uc_donkinh : UserControl
    {
        KcbLuotkham objLuotkham;
        KcbDonkinh objDonkinh;
        KcbDanhsachBenhnhan objBenhnhan;
        KcbDangkyKcb objCongkham;
        bool hasInit = false;
        int id_bacsi = -1;
        long my_id = -1;
        public uc_donkinh()
        {
            InitializeComponent();
            this.Load += uc_donkinh_Load;
            this.KeyDown += Uc_donkinh_KeyDown;
        }
        void CreateHistory()
        {
            try
            {
                flowHistory.Controls.Clear();
                string sql = string.Format("select *,(select top 1 ten_nhanvien from dmuc_nhanvien where id_nhanvien=p.id_bacsi_kedon ) as ten_bacsi from kcb_donkinh p where id_benhnhan={0} order by ngay_kedon desc",objCongkham.IdBenhnhan);
                DataTable dtHistory = Utility.ExecuteSql(sql, CommandType.Text).Tables[0];
                   
                flowHistory.SuspendLayout();
                foreach (DataRow dr in dtHistory.Rows)
                {
                    uc_lsu_donkinh _lsu_donkinh = new uc_lsu_donkinh(dr);
                    _lsu_donkinh._OnClick += _lsu_donkinh__OnClick;
                    _lsu_donkinh.LoadData();
                    flowHistory.Controls.Add(_lsu_donkinh);
                }
                flowHistory.ResumeLayout();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void _lsu_donkinh__OnClick(uc_lsu_donkinh obj)
        {
            my_id = obj.id_donkinh;
            LoadData(obj.id_donkinh);
        }

        private void Uc_donkinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.ActiveControl != null && !this.ActiveControl.GetType().Equals(cbo_donkinh_thiluc_phai.GetType()))
            {
                SendKeys.Send("{TAB}");
            }
        }
        public void ClearData(bool isReset)
        {
            if (isReset)
            {
                objLuotkham = null;
                objBenhnhan = null;
                objCongkham = null;
            }
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType().Equals(cbo_donkinh_cau_phai.GetType()))
                {
                    ((VNS.HIS.UCs.EasyCompletionComboBox)ctrl).SelectedValue = -1;
                }
                else if (ctrl.GetType().Equals(txt_donkinh_add_phai.GetType()))
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)ctrl).Clear();
                }
            }

            modifyCommands();
        }
        public void InitData()
        {
            try
            {
                if (hasInit) return;
                DataTable dtDmucThiluc = THU_VIEN_CHUNG.LayDulieuDanhmucChung("DANHSACHTHILUC", true);
                dtpNgaykham.Value = DateTime.Now;
                DataRow dr = dtDmucThiluc.NewRow();
                dr[DmucChung.Columns.Ma] = "";
                dr[DmucChung.Columns.Ten] = "";
                dtDmucThiluc.Rows.InsertAt(dr, 0);
                DataBinding.BindDataCombobox(cbo_donkinh_thiluc_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_donkinh_thiluc_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_kxht_thiluc_phai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataBinding.BindDataCombobox(cbo_kxht_thiluc_trai, dtDmucThiluc.Copy(), DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                if (cbo_donkinh_cau_phai.Items.Count <= 0)
                {
                    //Thêm các thông tin vào các cbo cầu,trụ, trục
                    List<string> lstCautru = new List<string>();
                    List<string> lstTruc = new List<string>();
                    string sCauTru = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_KHAMKHUCXA_CYL_RANGE", "1;180", true);
                    string sTruc = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_KHAMKHUCXA_AX_RANGE", "-30;20", true);
                    int from = Utility.Int32Dbnull(sCauTru.Split(';')[0]);
                    int to = Utility.Int32Dbnull(sCauTru.Split(';')[1]);
                    for (int i = from; i <= to; i++)
                        lstTruc.Add(i.ToString());
                    from = Utility.Int32Dbnull(sTruc.Split(';')[0]);
                    to = Utility.Int32Dbnull(sTruc.Split(';')[1]);
                    for (int i = from; i <= to; i++)
                    {
                        string sval = "";
                        if (i < 0)
                            sval = i.ToString();
                        else if (i == 0)
                            sval = "0";
                        else if (i > 0)
                            sval = "+" + i.ToString();
                        lstCautru.Add(sval);
                    }

                    cbo_donkinh_cau_phai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_donkinh_cau_trai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_donkinh_tru_phai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_donkinh_tru_trai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_donkinh_truc_phai.Items.AddRange(lstTruc.ToArray<string>());
                    cbo_donkinh_truc_trai.Items.AddRange(lstTruc.ToArray<string>());

                    cbo_kxht_cau_phai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_kxht_cau_trai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_kxht_tru_phai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_kxht_tru_trai.Items.AddRange(lstCautru.ToArray<string>());
                    cbo_kxht_truc_phai.Items.AddRange(lstTruc.ToArray<string>());
                    cbo_kxht_truc_trai.Items.AddRange(lstTruc.ToArray<string>());
                }
                txtBacsi.Enabled = false;
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
                hasInit = true;
            }
            catch (Exception)
            {


            }
            finally
            {
              
            }
        }
        public void SetData(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan,KcbDangkyKcb objCongkham)
        {
            this.objBenhnhan = objBenhnhan;
            this.objLuotkham = objLuotkham;
            this.objCongkham = objCongkham;
            InitData();
            LoadData();
            CreateHistory();
            modifyCommands();
        }
        void uc_donkinh_Load(object sender, EventArgs e)
        {
           
        }
        public void SetPermision(bool quyenkedon)
        {
             cmdSave.Visible = cmdXoa.Visible = quyenkedon;
        }
        public void SetBacsiKham(int id_bacsi)
        {
            this.id_bacsi = id_bacsi;
            txtBacsi.SetId(id_bacsi);
        }
       
        public void LoadData(long id_donkinh)
        {
            try
            {
                KcbDonkinh objHistory = new Select().From(KcbDonkinh.Schema)
                    .Where(KcbDonkinh.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
                    //.And(KcbDonkinh.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
                     .And(KcbDonkinh.Columns.IdDonkinh).IsEqualTo(id_donkinh)
                    .ExecuteSingle<KcbDonkinh>();
                if (objHistory != null)
                {
                    //Khúc xạ hiện tại
                    cbo_kxht_cau_phai.Text = objHistory.KxhtCauPhai;
                    cbo_kxht_cau_trai.Text = objHistory.KxhtCauTrai;
                    cbo_kxht_tru_phai.Text = objHistory.KxhtTruPhai;
                    cbo_kxht_tru_trai.Text = objHistory.KxhtTruTrai;
                    cbo_kxht_truc_phai.Text = objHistory.KxhtTrucPhai;
                    cbo_kxht_truc_trai.Text = objHistory.KxhtTrucTrai;
                    cbo_kxht_thiluc_phai.Text = objHistory.KxhtThilucPhai;
                    cbo_kxht_thiluc_trai.Text = objHistory.KxhtThilucTrai;
                    txt_kxht_add_phai.Text = objHistory.KxhtAddPhai;
                    txt_kxht_add_trai.Text = objHistory.KxhtAddTrai;
                    //Đơn kinh
                    cbo_donkinh_cau_phai.Text = objHistory.DonkinhCauPhai;
                    cbo_donkinh_cau_trai.Text = objHistory.DonkinhCauTrai;
                    cbo_donkinh_tru_phai.Text = objHistory.DonkinhTruPhai;
                    cbo_donkinh_tru_trai.Text = objHistory.DonkinhTruTrai;
                    cbo_donkinh_truc_phai.Text = objHistory.DonkinhTrucPhai;
                    cbo_donkinh_truc_trai.Text = objHistory.DonkinhTrucTrai;
                    cbo_donkinh_thiluc_phai.Text = objHistory.DonkinhThilucPhai;
                    cbo_donkinh_thiluc_trai.Text = objHistory.DonkinhThilucTrai;
                    txt_donkinh_add_phai.Text = objHistory.DonkinhAddPhai;
                    txt_donkinh_add_trai.Text = objHistory.DonkinhAddTrai;
                    //Ghi chú
                    txt_kcdt_xa.Text = objHistory.KcdtXa;
                    txt_kcdt_gan.Text = objHistory.KcdtGan;

                    chk_kinhdatrong.Checked = Utility.Bool2Bool(objHistory.Kinhdatrong);
                    chk_kinhnhingan.Checked = Utility.Bool2Bool(objHistory.Kinhnhingan);
                    chk_kinh2trong.Checked = Utility.Bool2Bool(objHistory.Kinh2trong);
                    chk_kinhdoimau.Checked = Utility.Bool2Bool(objHistory.Kinhdoimau);
                    chk_kinhpoly.Checked = Utility.Bool2Bool(objHistory.Kinhpoly);
                    chk_kinhaptrong.Checked = Utility.Bool2Bool(objHistory.Kinhaptrong);


                    txtGhichu.Text = objHistory.GhiChu;
                    txtBacsi.SetId(objHistory.IdBacsiKedon);
                    dtpNgaykham.Value = objHistory.NgayKedon;
                    cmdSave.Enabled = cmdXoa.Enabled = objCongkham.IdKham == objHistory.IdCongkham;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
               
            }
        }
        public void LoadData()
        {
            try
            {
                objDonkinh = new Select().From(KcbDonkinh.Schema)
                    .Where(KcbDonkinh.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
                    .And(KcbDonkinh.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
                     .And(KcbDonkinh.Columns.IdCongkham).IsEqualTo(objCongkham.IdKham)
                    .ExecuteSingle<KcbDonkinh>();
                if (objDonkinh != null)
                {
                    my_id = objDonkinh.IdDonkinh;
                    //Khúc xạ hiện tại
                    cbo_kxht_cau_phai.Text = objDonkinh.KxhtCauPhai;
                    cbo_kxht_cau_trai.Text = objDonkinh.KxhtCauTrai;
                    cbo_kxht_tru_phai.Text = objDonkinh.KxhtTruPhai;
                    cbo_kxht_tru_trai.Text = objDonkinh.KxhtTruTrai;
                    cbo_kxht_truc_phai.Text = objDonkinh.KxhtTrucPhai;
                    cbo_kxht_truc_trai.Text = objDonkinh.KxhtTrucTrai;
                    cbo_kxht_thiluc_phai.Text = objDonkinh.KxhtThilucPhai;
                    cbo_kxht_thiluc_trai.Text = objDonkinh.KxhtThilucTrai;
                    txt_kxht_add_phai.Text = objDonkinh.KxhtAddPhai;
                    txt_kxht_add_trai.Text = objDonkinh.KxhtAddTrai;
                    //Đơn kinh
                    cbo_donkinh_cau_phai.Text = objDonkinh.DonkinhCauPhai;
                    cbo_donkinh_cau_trai.Text = objDonkinh.DonkinhCauTrai;
                    cbo_donkinh_tru_phai.Text = objDonkinh.DonkinhTruPhai;
                    cbo_donkinh_tru_trai.Text = objDonkinh.DonkinhTruTrai;
                    cbo_donkinh_truc_phai.Text = objDonkinh.DonkinhTrucPhai;
                    cbo_donkinh_truc_trai.Text = objDonkinh.DonkinhTrucTrai;
                    cbo_donkinh_thiluc_phai.Text = objDonkinh.DonkinhThilucPhai;
                    cbo_donkinh_thiluc_trai.Text = objDonkinh.DonkinhThilucTrai;
                    txt_donkinh_add_phai.Text = objDonkinh.DonkinhAddPhai;
                    txt_donkinh_add_trai.Text = objDonkinh.DonkinhAddTrai;
                    //Ghi chú
                    txt_kcdt_xa.Text = objDonkinh.KcdtXa;
                    txt_kcdt_gan.Text = objDonkinh.KcdtGan;

                    chk_kinhdatrong.Checked = Utility.Bool2Bool(objDonkinh.Kinhdatrong);
                    chk_kinhnhingan.Checked = Utility.Bool2Bool(objDonkinh.Kinhnhingan);
                    chk_kinh2trong.Checked = Utility.Bool2Bool(objDonkinh.Kinh2trong);
                    chk_kinhdoimau.Checked = Utility.Bool2Bool(objDonkinh.Kinhdoimau);
                    chk_kinhpoly.Checked = Utility.Bool2Bool(objDonkinh.Kinhpoly);
                    chk_kinhaptrong.Checked = Utility.Bool2Bool(objDonkinh.Kinhaptrong);

                  
                    txtGhichu.Text = objDonkinh.GhiChu;
                    txtBacsi.SetId(objDonkinh.IdBacsiKedon);
                    dtpNgaykham.Value = objDonkinh.NgayKedon;
                   
                }
                else
                {
                    txtGhichu.Clear();
                    txtBacsi.SetId(id_bacsi);
                    dtpNgaykham.Value = DateTime.Now;

                    //Load dữ liệu từ phiếu khám khúc xạ vào
                    KcbPhieukhamThiluc objKhamthiluc = new Select().From(KcbPhieukhamThiluc.Schema)
                  .Where(KcbPhieukhamThiluc.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
                  .And(KcbPhieukhamThiluc.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
                  .ExecuteSingle<KcbPhieukhamThiluc>();
                    if (objKhamthiluc != null)
                    {
                        //Khúc xạ hiện tại
                        cbo_kxht_cau_phai.Text = objKhamthiluc.KhucxahientaiCauPhai;
                        cbo_kxht_cau_trai.Text = objKhamthiluc.KhucxahientaiCauTrai;
                        cbo_kxht_tru_phai.Text = objKhamthiluc.KhucxahientaiTruPhai;
                        cbo_kxht_tru_trai.Text = objKhamthiluc.KhucxahientaiTruTrai;
                        cbo_kxht_truc_phai.Text = objKhamthiluc.KhucxahientaiTrucPhai;
                        cbo_kxht_truc_trai.Text = objKhamthiluc.KhucxahientaiTrucTrai;
                        cbo_kxht_thiluc_phai.Text = objKhamthiluc.KhucxahientaiThilucCokinhPhai;
                        cbo_kxht_thiluc_trai.Text = objKhamthiluc.KhucxahientaiThilucCokinhTrai;
                        txt_kxht_add_phai.Text = objKhamthiluc.KhucxahientaiAddPhai;
                        txt_kxht_add_trai.Text = objKhamthiluc.KhucxahientaiAddTrai;
                        //Đơn kinh
                        cbo_donkinh_cau_phai.Text = objKhamthiluc.DonkinhCauPhai;
                        cbo_donkinh_cau_trai.Text = objKhamthiluc.DonkinhCauTrai;
                        cbo_donkinh_tru_phai.Text = objKhamthiluc.DonkinhTruPhai;
                        cbo_donkinh_tru_trai.Text = objKhamthiluc.DonkinhTruTrai;
                        cbo_donkinh_truc_phai.Text = objKhamthiluc.DonkinhTrucPhai;
                        cbo_donkinh_truc_trai.Text = objKhamthiluc.DonkinhTrucTrai;
                        cbo_donkinh_thiluc_phai.Text = objKhamthiluc.DonkinhThilucCokinhPhai;
                        cbo_donkinh_thiluc_trai.Text = objKhamthiluc.DonkinhThilucCokinhTrai;
                        txt_donkinh_add_phai.Text = objKhamthiluc.DonkinhAddPhai;
                        txt_donkinh_add_trai.Text = objKhamthiluc.DonkinhAddTrai;
                        //Ghi chú
                        txt_kcdt_xa.Text = objKhamthiluc.KhoangcachdongtuXa;
                        txt_kcdt_gan.Text = objKhamthiluc.KhoangcachdongtuGan;

                        chk_kinhdatrong.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhdatrong);
                        chk_kinhnhingan.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhnhingan);
                        chk_kinh2trong.Checked = Utility.Bool2Bool(objKhamthiluc.Kinh2trong);
                        chk_kinhdoimau.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhdoimau);
                        chk_kinhpoly.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhpoly);
                        chk_kinhaptrong.Checked = Utility.Bool2Bool(objKhamthiluc.Kinhaptrong);

                      
                    }
                    
                        objDonkinh = new KcbDonkinh();
                }    
                   
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                cmdSave.Enabled = cmdXoa.Enabled = true;
            }
        }
        public void PrepareData4Save()
        {
            try
            {
                if (objDonkinh == null) objDonkinh = new KcbDonkinh();//99% ko xảy ra
                if (objDonkinh.IdDonkinh > 0)
                {
                    objDonkinh.MarkOld();
                    objDonkinh.IsNew = false;
                    objDonkinh.NgaySua = DateTime.Now;
                    objDonkinh.NguoiSua = globalVariables.UserName;
                }
                objDonkinh.IdBenhnhan = objLuotkham.IdBenhnhan;
                objDonkinh.MaLuotkham = objLuotkham.MaLuotkham;
                objDonkinh.IdCongkham = objCongkham.IdKham;
                //Khúc xạ hiện tại
                objDonkinh.KxhtCauPhai = Utility.sDbnull(cbo_kxht_cau_phai.Text);
                objDonkinh.KxhtCauTrai= Utility.sDbnull(cbo_kxht_cau_trai.Text);
                objDonkinh.KxhtTruPhai= Utility.sDbnull(cbo_kxht_tru_phai.Text);
                objDonkinh.KxhtTruTrai= Utility.sDbnull(cbo_kxht_tru_trai.Text);
                objDonkinh.KxhtTrucPhai= Utility.sDbnull(cbo_kxht_truc_phai.Text);
                objDonkinh.KxhtTrucTrai= Utility.sDbnull(cbo_kxht_truc_trai.Text);
                objDonkinh.KxhtThilucPhai= Utility.sDbnull(cbo_kxht_thiluc_phai.Text);
                objDonkinh.KxhtThilucTrai= Utility.sDbnull(cbo_kxht_thiluc_trai.Text);
                objDonkinh.KxhtAddPhai= Utility.sDbnull(txt_kxht_add_phai.Text);
                objDonkinh.KxhtAddTrai= Utility.sDbnull(txt_kxht_add_trai.Text);
                //Đơn kinh
                objDonkinh.DonkinhCauPhai= Utility.sDbnull(cbo_donkinh_cau_phai.Text);
                objDonkinh.DonkinhCauTrai= Utility.sDbnull(cbo_donkinh_cau_trai.Text);
                objDonkinh.DonkinhTruPhai= Utility.sDbnull(cbo_donkinh_tru_phai.Text);
                objDonkinh.DonkinhTruTrai= Utility.sDbnull(cbo_donkinh_tru_trai.Text);
                objDonkinh.DonkinhTrucPhai= Utility.sDbnull(cbo_donkinh_truc_phai.Text);
                objDonkinh.DonkinhTrucTrai= Utility.sDbnull(cbo_donkinh_truc_trai.Text);
                objDonkinh.DonkinhThilucPhai= Utility.sDbnull(cbo_donkinh_thiluc_phai.Text);
                objDonkinh.DonkinhThilucTrai= Utility.sDbnull(cbo_donkinh_thiluc_trai.Text);
                objDonkinh.DonkinhAddPhai= Utility.sDbnull(txt_donkinh_add_phai.Text);
                objDonkinh.DonkinhAddTrai= Utility.sDbnull(txt_donkinh_add_trai.Text);

                //Ghi chú
                objDonkinh.KcdtXa = Utility.sDbnull(txt_kcdt_xa.Text);
                objDonkinh.KcdtGan = Utility.sDbnull(txt_kcdt_gan.Text);
                objDonkinh.GhiChu = Utility.sDbnull(txtGhichu.Text);

                objDonkinh.Kinhdatrong = chk_kinhdatrong.Checked;
                objDonkinh.Kinhnhingan = chk_kinhnhingan.Checked;
                objDonkinh.Kinh2trong = chk_kinh2trong.Checked;
                objDonkinh.Kinhdoimau = chk_kinhdoimau.Checked;
                objDonkinh.Kinhpoly = chk_kinhpoly.Checked;
                 objDonkinh.Kinhaptrong = chk_kinhaptrong.Checked;

              
             
                objDonkinh.IdBacsiKedon = Utility.Int32Dbnull(txtBacsi.MyID, -1);
                objDonkinh.NgayKedon = dtpNgaykham.Value;
                cmdXoa.Enabled = cmdIn.Enabled = true;
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện nhập thông tin khám Đo thị lực");
                return;
            }
            if (txtBacsi.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần nhập bác sĩ khám");
                txtBacsi.Focus();
                return;
            }
             
            PrepareData4Save();
            
            objDonkinh.TrangThai = 1;
            objDonkinh.Save();
            Utility.Log(Name, globalVariables.UserName, string.Format("Lưu thông tin khám thị lực cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Save, this.GetType().Assembly.ManifestModule.Name);
            if (!FindMe())
                AddMe(objDonkinh.IdDonkinh);
            //CreateHistory();

            modifyCommands();
        }
        void AddMe(long id_donkinh)
        {
            DataTable dtCurrent = new Select().From(KcbDonkinh.Schema)
              .Where(KcbDonkinh.Columns.IdBenhnhan).IsEqualTo(objCongkham.IdBenhnhan)
              //.And(KcbDonkinh.Columns.MaLuotkham).IsEqualTo(objCongkham.MaLuotkham)
              .And(KcbDonkinh.Columns.IdDonkinh).IsEqualTo(id_donkinh)
              .ExecuteDataSet().Tables[0];
            uc_lsu_donkinh _lsu_donkinh = new uc_lsu_donkinh(dtCurrent.Rows[0]);
            _lsu_donkinh._OnClick += _lsu_donkinh__OnClick;
            _lsu_donkinh.LoadData();
            flowHistory.Controls.Add(_lsu_donkinh);
            flowHistory.Controls.SetChildIndex(_lsu_donkinh, 0);
        }
        bool FindMe()
        {
            bool isFound = false;
            foreach (uc_lsu_donkinh ctrl in flowHistory.Controls)
            {
                if (ctrl.id_donkinh == objDonkinh.IdDonkinh)
                    isFound = true;
            }
            return isFound;
        }
        void modifyCommands()
        {
            cmdSave.Enabled = true;
            if (objDonkinh == null || objDonkinh.IdDonkinh <= 0 || objDonkinh.IdBenhnhan <= 0)
            {
               cmdIn.Enabled = cmdXoa.Enabled = false;
            }
            else
            {
                cmdIn.Enabled = cmdXoa.Enabled = true;
            }
        }
      
        /// <summary>
        /// Xóa quay trở lại khâu mới bắt đầu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null || objCongkham == null || objBenhnhan == null)
            {
                Utility.ShowMsg("Mời bạn chọn người bệnh trước khi thực hiện xóa thông tin đơn kính");
                return;
            }
            if(objDonkinh!=null && objDonkinh.NguoiTao!=globalVariables.UserName)
            {
                if(!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg(string.Format( "Bạn không có quyền xóa đơn kính được tạo bởi người dùng {0}. Vui lòng liên hệ người tạo đơn kính hoặc dùng quyền super admin để xóa", objDonkinh.NguoiTao));
                    return;
                }    
            }
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin đơn kính đang làm?", "Xác nhận xóa", true)) return;
            new Delete().From(KcbDonkinh.Schema)
                      .Where(KcbDonkinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                      .And(KcbDonkinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                      .And(KcbDonkinh.Columns.IdCongkham).IsEqualTo(objCongkham.IdKham)
                      .Execute();
            Utility.Log(Name, globalVariables.UserName, string.Format("Xóa đơn kính cho Bệnh nhân {0} có mã lần khám {1} và ID {2}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
         
            objDonkinh = null;
            modifyCommands();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            InDonKinh();
        }
        void InDonKinh()
        {
            try
            {
                KcbDonkinh _printObj = KcbDonkinh.FetchByID(my_id);
                if (_printObj == null || _printObj.IdDonkinh <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Đo thị lực trước khi thực hiện in");
                    return;
                }

                DataTable dtData = SPs.KcbDonkinhIn(_printObj.IdDonkinh, _printObj.IdBenhnhan, _printObj.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "DonKinh";
                List<string> lstAddedFields = new List<string>() {"gioitinh_nam","gioitinh_nu","noikhoa_khong", "noikhoa_co", "pttt_khong", "pttt_co",
                "tinhtrangravien_khoi", "tinhtrangravien_do", "tinhtrangravien_khongthaydoi",
                "tinhtrangravien_nanghon", "tinhtrangravien_tuvong", "tinhtrangravien_xinve","tinhtrangravien_khongxacdinh","chkkinh2trong","chkKinhdatrong","chkKinhnhingan","chkKinhdoimau","chkKinhpoly","chkKinhaptrong",};
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));


                THU_VIEN_CHUNG.CreateXML(dtData, "DonKinh.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "DonKinh";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["donvi_captren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diachi_benhvien"] = globalVariables.Branch_Address;
                drData["dienthoai_benhvien"] = globalVariables.Branch_Phone;
                drData["hotline_benhvien"] = globalVariables.Branch_Hotline;
                drData["fax_benhvien"] = globalVariables.Branch_Fax;
                drData["website_benhvien"] = globalVariables.Branch_Website;
                drData["email_benhvien"] = globalVariables.Branch_Email;
                drData["ngay_thuchien"] = Utility.FormatDateTimeWithLocation(Utility.sDbnull(drData["sNgaykedon"], DateTime.Now.ToString("dd/MM/yyyy")), globalVariables.gv_strDiadiem);
               
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("chkkinh2trong",Utility.Bool2byte(_printObj.Kinh2trong).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhdatrong", Utility.Bool2byte(_printObj.Kinhdatrong).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhnhingan", Utility.Bool2byte(_printObj.Kinhnhingan).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhdoimau", Utility.Bool2byte(_printObj.Kinhdoimau).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhpoly", Utility.Bool2byte(_printObj.Kinhpoly).ToString() == "1" ? "1" : "0");
                dicMF.Add("chkKinhaptrong", Utility.Bool2byte(_printObj.Kinhaptrong).ToString() == "1" ? "1" : "0");
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\DonKinh.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("DonKinh", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Tóm tắt hồ sơ bệnh án tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "DonKinh", _printObj.MaLuotkham, Utility.sDbnull(_printObj.IdDonkinh), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }



                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);
                    byte[] QRCode = Utility.GetQRCode(_printObj.MaLuotkham);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("qrsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("qrcode") && QRCode != null && QRCode.Length > 100)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(QRCode, w, h);
                            else
                                builder.InsertImage(QRCode);
                        }
                        else
                            builder.InsertImage(QRCode);
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
