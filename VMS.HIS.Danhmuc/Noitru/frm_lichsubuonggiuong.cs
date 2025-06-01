using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Noitru
{
    public partial class frm_lichsubuonggiuong : Form
    {
        public string mabenhvien = "";
        public int idBenhvien = -1;
        public bool mv_blnCancel=true;
        int id_khoa = -1;
        int id_buong = -1;
        int id_giuong = -1;
        bool autoLoad = false;
        public frm_lichsubuonggiuong(int id_khoa, int id_buong, int id_giuong,bool autoLoad)
        {
            InitializeComponent();
            this.autoLoad = autoLoad;
            this.id_khoa = id_khoa;
            this.id_buong = id_buong;
            this.id_giuong = id_giuong;
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.KeyDown += new KeyEventHandler(frm_lichsubuonggiuong_KeyDown);
            this.Load += new EventHandler(frm_lichsubuonggiuong_Load);
            autoKhoa._OnEnterMe += autoKhoa__OnEnterMe;
            autoBuong._OnEnterMe += autoBuong__OnEnterMe;
            autoGiuong._OnEnterMe += autoGiuong__OnEnterMe;
        }

        void autoGiuong__OnEnterMe()
        {
            id_giuong = Utility.Int32Dbnull(autoGiuong.MyID, -1);
            SearchHistory();
        }

        void autoBuong__OnEnterMe()
        {
            id_buong = Utility.Int32Dbnull(autoBuong.MyID, -1);
            DataTable dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(id_khoa, id_buong, 1);
            autoGiuong.Init(dtGiuong, new List<string>() { NoitruDmucGiuongbenh.Columns.IdGiuong, NoitruDmucGiuongbenh.Columns.MaGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong });
            autoGiuong.Focus();
        }

        void autoKhoa__OnEnterMe()
        {
            id_khoa = Utility.Int32Dbnull(autoKhoa.MyID, -1);
            DataTable dtBuong = SPs.NoitruTimkiembuongTheokhoa(id_khoa).GetDataSet().Tables[0];
            autoBuong.Init(dtBuong, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
            autoBuong.RaiseEnterEvents();
            autoBuong.Focus();
        }
        void frm_lichsubuonggiuong_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKhoa = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                autoKhoa.Init(dtKhoa, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                if (autoLoad)
                {
                    //autoKhoa.SetId(id_khoa);
                    //autoKhoa.RaiseEnterEvents();
                    //autoBuong.SetId(id_buong);
                    //autoBuong.RaiseEnterEvents();
                    //autoGiuong.SetId(id_giuong);
                    //autoGiuong.RaiseEnterEvents();
                    SearchHistory();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        void SearchHistory()
        {
            DataTable dt = SPs.NoitruXemlichsunamgiuong(id_khoa, id_buong, id_giuong).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grd_List, dt, true, true, "1=1", "ngay_batdau");
            if (grd_List.RowCount > 0) grd_List.MoveFirst();
        }

        void frm_lichsubuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            else if (e.KeyCode == Keys.F2)
                autoKhoa.Focus();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
