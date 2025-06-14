using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace VietBaIT.HISLink.DataAccessLayer
{
    /// <summary>
    /// Controller class for T_BENH_AN_NGOAITRU
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TBenhAnNgoaitruController
    {
        // Preload our schema..
        TBenhAnNgoaitru thisSchemaLoad = new TBenhAnNgoaitru();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TBenhAnNgoaitruCollection FetchAll()
        {
            TBenhAnNgoaitruCollection coll = new TBenhAnNgoaitruCollection();
            Query qry = new Query(TBenhAnNgoaitru.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TBenhAnNgoaitruCollection FetchByID(object Id)
        {
            TBenhAnNgoaitruCollection coll = new TBenhAnNgoaitruCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TBenhAnNgoaitruCollection FetchByQuery(Query qry)
        {
            TBenhAnNgoaitruCollection coll = new TBenhAnNgoaitruCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TBenhAnNgoaitru.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TBenhAnNgoaitru.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string SoBenhAn,int IdBnhan,string NgaySinh,string ThangSinh,string DanToc,string NoiLamviec,string ThongtinLhe,string DthoaiLhe,string CdoanGioithieu,short? YTe,short? LdoVaovienMm,short? LdoVaovienGsc,short? LdoVaovienKndn,short? LdoVaovienGtl,short? LdoVaovienKhac,string HbNam,string HbNoiCdoan,short? HbDieuTri,short? HbTiemInsulin,string HbInsulin1,string HbInsulin2,string HbThuochaDuonghuyet,string HbHa,string HbRllp,string HbThuocChongdong,short? HtaiMm,short? HtaiGaysc,string HtaiKgDau,string HtaiKgSau,short? HtaiKhatnhieu,string HtaiUong,short? HtaiDainhieu,string HtaiDai,short? HtaiGiamtl,string HtaiKhac,short? TsbBtChuaphathien,short? TsbBtNhoimaucotim,string TsbBtNamNmct,short? TsbBtTbmn,string TsbBtNamTbmn,string TsbHaNam,string TsbHaMax,short? TsbBtCON4000,string TsbBtKhac,short? TsgdDtd,short? TsgdTanghuyetap,short? TsgdNhoimaucotim,string TsgdKhac,short? KcbMatnuoc,short? KcbXuathuyet,short? KcbPhu,string KcbToanthanKhac,string KcbNhiptim,short? KcbTthaiNhiptim,string KcbTiengtim,string KcbHohap,string KcbBung,short? KcbChanphai,string KcbChanphaiKhac,short? KcbChantrai,string KcbChantraiKhac,string KcbMatphai,string KcbMattrai,string KcbRanghammat,string KcbTkinhKhac,string KcbMach,string KcbNhietdo,string KcbHuyetap1,string KcbHuyetap2,string KcbNhiptho,string KcbCannang,string KcbChieucao,string KcbBmi,string KcbTomtatClsChinh,string KcbCdoanBdau,string KcbDaXly,string KcbCdoanRavien,DateTime? KcbDtriTungay,DateTime? KcbDtriDenngay,string TkbaQtrinhBenhlyDbienCls,string TkbaPphapDtri,string DiaChi,string DoiTuong,string IcdBenhChnh,string IcdBenhPhu,short? NamSinh,string SoBhyt,string TenBenhChinh,string TenBenhPhu,string TkbaHuongTtTieptheo,string TkbaTomtatKqCls,string TkbaTtRavien,short? TrangThai,short? InPhieuLog,short? LoaiBenhAn,DateTime? NgayIn,string NguoiIn,short? TgHbHtGsc,string TgHbHtKgcuoi,string TgHbHtKgdau,string TgHbHtKhac,short? TgHbHtMl,short? TgHbHtMm,short? TgHbHtNnb,short? TgHbHtRct,short? TgHbHtRlth,string TgHbHtSldn,string TgHbHtTcp,short? TgHbHtVtgt,string TgHbQtblChandoan,short? TgHbQtblDieutriDeu,string TgHbQtblNam,string TgHbQtblNoiChanDoan,string TgHbQtblThuocDIEUTRI1,string TgHbQtblThuocDIEUTRI2,string TgHbQtblThuocDIEUTRI3,string TgHbQtblThuocDIEUTRI4,string TgHbQtblThuocDIEUTRI5,short? TgHbTsbBasedow,short? TgHbTsbBc,short? TgHbTsbBldd,short? TgHbTsbHpq,string TgHbTsbKhac,string TgHbTsbKHAC1,short? TgHbTsbKn,string TgKbKbB,short? TgKbKbBlt,string TgKbKbBmi,short? TgKbKbBN2T,short? TgKbKbBntp,short? TgKbKbBntt,string TgKbKbCannang,string TgKbKbCdbd,string TgKbKbCdrv,string TgKbKbChieucao,short? TgKbKbCmmv,DateTime? TgKbKbDenngay,short? TgKbKbDna,string TgKbKbDxl,string TgKbKbHh,string TgKbKbHuyetap,short? TgKbKbLa,short? TgKbKbLb,short? TgKbKbLl,short? TgKbKbLll,string TgKbKbMach,short? TgKbKbMdc,short? TgKbKbMdm,string TgKbKbMp,string TgKbKbMt,string TgKbKbNhietdo,string TgKbKbNhiptho,string TgKbKbNt,short? TgKbKbNtDeu,string TgKbKbTk,short? TgKbKbTr,string TgKbKbTt,string TgKbKbTtkqcks,short? TgKbKbTtltp,short? TgKbKbTtltt,short? TgKbKbTtttp,short? TgKbKbTtttt,DateTime? TgKbKbTungay,short? TgLdvvCt,short? TgLdvvGsc,short? TgLdvvKhac,string TgLdvvKHAC1,short? TgLdvvMl,short? TgLdvvRct,short? TgLlvvMm,short? BaLog,string BatBhQtbl,string BatHbBt,string BatHbGd,string BatKbCbp,string BatKbCdbd,string BatKbCdrv,string BatKbDxl,string BatKbKqcls,string BatKbTt,string BatLdvv,DateTime? NgaySua,DateTime? NgayTao,string NguoiSua,string NguoiTao,DateTime? NgayKham)
	    {
		    TBenhAnNgoaitru item = new TBenhAnNgoaitru();
		    
            item.SoBenhAn = SoBenhAn;
            
            item.IdBnhan = IdBnhan;
            
            item.NgaySinh = NgaySinh;
            
            item.ThangSinh = ThangSinh;
            
            item.DanToc = DanToc;
            
            item.NoiLamviec = NoiLamviec;
            
            item.ThongtinLhe = ThongtinLhe;
            
            item.DthoaiLhe = DthoaiLhe;
            
            item.CdoanGioithieu = CdoanGioithieu;
            
            item.YTe = YTe;
            
            item.LdoVaovienMm = LdoVaovienMm;
            
            item.LdoVaovienGsc = LdoVaovienGsc;
            
            item.LdoVaovienKndn = LdoVaovienKndn;
            
            item.LdoVaovienGtl = LdoVaovienGtl;
            
            item.LdoVaovienKhac = LdoVaovienKhac;
            
            item.HbNam = HbNam;
            
            item.HbNoiCdoan = HbNoiCdoan;
            
            item.HbDieuTri = HbDieuTri;
            
            item.HbTiemInsulin = HbTiemInsulin;
            
            item.HbInsulin1 = HbInsulin1;
            
            item.HbInsulin2 = HbInsulin2;
            
            item.HbThuochaDuonghuyet = HbThuochaDuonghuyet;
            
            item.HbHa = HbHa;
            
            item.HbRllp = HbRllp;
            
            item.HbThuocChongdong = HbThuocChongdong;
            
            item.HtaiMm = HtaiMm;
            
            item.HtaiGaysc = HtaiGaysc;
            
            item.HtaiKgDau = HtaiKgDau;
            
            item.HtaiKgSau = HtaiKgSau;
            
            item.HtaiKhatnhieu = HtaiKhatnhieu;
            
            item.HtaiUong = HtaiUong;
            
            item.HtaiDainhieu = HtaiDainhieu;
            
            item.HtaiDai = HtaiDai;
            
            item.HtaiGiamtl = HtaiGiamtl;
            
            item.HtaiKhac = HtaiKhac;
            
            item.TsbBtChuaphathien = TsbBtChuaphathien;
            
            item.TsbBtNhoimaucotim = TsbBtNhoimaucotim;
            
            item.TsbBtNamNmct = TsbBtNamNmct;
            
            item.TsbBtTbmn = TsbBtTbmn;
            
            item.TsbBtNamTbmn = TsbBtNamTbmn;
            
            item.TsbHaNam = TsbHaNam;
            
            item.TsbHaMax = TsbHaMax;
            
            item.TsbBtCON4000 = TsbBtCON4000;
            
            item.TsbBtKhac = TsbBtKhac;
            
            item.TsgdDtd = TsgdDtd;
            
            item.TsgdTanghuyetap = TsgdTanghuyetap;
            
            item.TsgdNhoimaucotim = TsgdNhoimaucotim;
            
            item.TsgdKhac = TsgdKhac;
            
            item.KcbMatnuoc = KcbMatnuoc;
            
            item.KcbXuathuyet = KcbXuathuyet;
            
            item.KcbPhu = KcbPhu;
            
            item.KcbToanthanKhac = KcbToanthanKhac;
            
            item.KcbNhiptim = KcbNhiptim;
            
            item.KcbTthaiNhiptim = KcbTthaiNhiptim;
            
            item.KcbTiengtim = KcbTiengtim;
            
            item.KcbHohap = KcbHohap;
            
            item.KcbBung = KcbBung;
            
            item.KcbChanphai = KcbChanphai;
            
            item.KcbChanphaiKhac = KcbChanphaiKhac;
            
            item.KcbChantrai = KcbChantrai;
            
            item.KcbChantraiKhac = KcbChantraiKhac;
            
            item.KcbMatphai = KcbMatphai;
            
            item.KcbMattrai = KcbMattrai;
            
            item.KcbRanghammat = KcbRanghammat;
            
            item.KcbTkinhKhac = KcbTkinhKhac;
            
            item.KcbMach = KcbMach;
            
            item.KcbNhietdo = KcbNhietdo;
            
            item.KcbHuyetap1 = KcbHuyetap1;
            
            item.KcbHuyetap2 = KcbHuyetap2;
            
            item.KcbNhiptho = KcbNhiptho;
            
            item.KcbCannang = KcbCannang;
            
            item.KcbChieucao = KcbChieucao;
            
            item.KcbBmi = KcbBmi;
            
            item.KcbTomtatClsChinh = KcbTomtatClsChinh;
            
            item.KcbCdoanBdau = KcbCdoanBdau;
            
            item.KcbDaXly = KcbDaXly;
            
            item.KcbCdoanRavien = KcbCdoanRavien;
            
            item.KcbDtriTungay = KcbDtriTungay;
            
            item.KcbDtriDenngay = KcbDtriDenngay;
            
            item.TkbaQtrinhBenhlyDbienCls = TkbaQtrinhBenhlyDbienCls;
            
            item.TkbaPphapDtri = TkbaPphapDtri;
            
            item.DiaChi = DiaChi;
            
            item.DoiTuong = DoiTuong;
            
            item.IcdBenhChnh = IcdBenhChnh;
            
            item.IcdBenhPhu = IcdBenhPhu;
            
            item.NamSinh = NamSinh;
            
            item.SoBhyt = SoBhyt;
            
            item.TenBenhChinh = TenBenhChinh;
            
            item.TenBenhPhu = TenBenhPhu;
            
            item.TkbaHuongTtTieptheo = TkbaHuongTtTieptheo;
            
            item.TkbaTomtatKqCls = TkbaTomtatKqCls;
            
            item.TkbaTtRavien = TkbaTtRavien;
            
            item.TrangThai = TrangThai;
            
            item.InPhieuLog = InPhieuLog;
            
            item.LoaiBenhAn = LoaiBenhAn;
            
            item.NgayIn = NgayIn;
            
            item.NguoiIn = NguoiIn;
            
            item.TgHbHtGsc = TgHbHtGsc;
            
            item.TgHbHtKgcuoi = TgHbHtKgcuoi;
            
            item.TgHbHtKgdau = TgHbHtKgdau;
            
            item.TgHbHtKhac = TgHbHtKhac;
            
            item.TgHbHtMl = TgHbHtMl;
            
            item.TgHbHtMm = TgHbHtMm;
            
            item.TgHbHtNnb = TgHbHtNnb;
            
            item.TgHbHtRct = TgHbHtRct;
            
            item.TgHbHtRlth = TgHbHtRlth;
            
            item.TgHbHtSldn = TgHbHtSldn;
            
            item.TgHbHtTcp = TgHbHtTcp;
            
            item.TgHbHtVtgt = TgHbHtVtgt;
            
            item.TgHbQtblChandoan = TgHbQtblChandoan;
            
            item.TgHbQtblDieutriDeu = TgHbQtblDieutriDeu;
            
            item.TgHbQtblNam = TgHbQtblNam;
            
            item.TgHbQtblNoiChanDoan = TgHbQtblNoiChanDoan;
            
            item.TgHbQtblThuocDIEUTRI1 = TgHbQtblThuocDIEUTRI1;
            
            item.TgHbQtblThuocDIEUTRI2 = TgHbQtblThuocDIEUTRI2;
            
            item.TgHbQtblThuocDIEUTRI3 = TgHbQtblThuocDIEUTRI3;
            
            item.TgHbQtblThuocDIEUTRI4 = TgHbQtblThuocDIEUTRI4;
            
            item.TgHbQtblThuocDIEUTRI5 = TgHbQtblThuocDIEUTRI5;
            
            item.TgHbTsbBasedow = TgHbTsbBasedow;
            
            item.TgHbTsbBc = TgHbTsbBc;
            
            item.TgHbTsbBldd = TgHbTsbBldd;
            
            item.TgHbTsbHpq = TgHbTsbHpq;
            
            item.TgHbTsbKhac = TgHbTsbKhac;
            
            item.TgHbTsbKHAC1 = TgHbTsbKHAC1;
            
            item.TgHbTsbKn = TgHbTsbKn;
            
            item.TgKbKbB = TgKbKbB;
            
            item.TgKbKbBlt = TgKbKbBlt;
            
            item.TgKbKbBmi = TgKbKbBmi;
            
            item.TgKbKbBN2T = TgKbKbBN2T;
            
            item.TgKbKbBntp = TgKbKbBntp;
            
            item.TgKbKbBntt = TgKbKbBntt;
            
            item.TgKbKbCannang = TgKbKbCannang;
            
            item.TgKbKbCdbd = TgKbKbCdbd;
            
            item.TgKbKbCdrv = TgKbKbCdrv;
            
            item.TgKbKbChieucao = TgKbKbChieucao;
            
            item.TgKbKbCmmv = TgKbKbCmmv;
            
            item.TgKbKbDenngay = TgKbKbDenngay;
            
            item.TgKbKbDna = TgKbKbDna;
            
            item.TgKbKbDxl = TgKbKbDxl;
            
            item.TgKbKbHh = TgKbKbHh;
            
            item.TgKbKbHuyetap = TgKbKbHuyetap;
            
            item.TgKbKbLa = TgKbKbLa;
            
            item.TgKbKbLb = TgKbKbLb;
            
            item.TgKbKbLl = TgKbKbLl;
            
            item.TgKbKbLll = TgKbKbLll;
            
            item.TgKbKbMach = TgKbKbMach;
            
            item.TgKbKbMdc = TgKbKbMdc;
            
            item.TgKbKbMdm = TgKbKbMdm;
            
            item.TgKbKbMp = TgKbKbMp;
            
            item.TgKbKbMt = TgKbKbMt;
            
            item.TgKbKbNhietdo = TgKbKbNhietdo;
            
            item.TgKbKbNhiptho = TgKbKbNhiptho;
            
            item.TgKbKbNt = TgKbKbNt;
            
            item.TgKbKbNtDeu = TgKbKbNtDeu;
            
            item.TgKbKbTk = TgKbKbTk;
            
            item.TgKbKbTr = TgKbKbTr;
            
            item.TgKbKbTt = TgKbKbTt;
            
            item.TgKbKbTtkqcks = TgKbKbTtkqcks;
            
            item.TgKbKbTtltp = TgKbKbTtltp;
            
            item.TgKbKbTtltt = TgKbKbTtltt;
            
            item.TgKbKbTtttp = TgKbKbTtttp;
            
            item.TgKbKbTtttt = TgKbKbTtttt;
            
            item.TgKbKbTungay = TgKbKbTungay;
            
            item.TgLdvvCt = TgLdvvCt;
            
            item.TgLdvvGsc = TgLdvvGsc;
            
            item.TgLdvvKhac = TgLdvvKhac;
            
            item.TgLdvvKHAC1 = TgLdvvKHAC1;
            
            item.TgLdvvMl = TgLdvvMl;
            
            item.TgLdvvRct = TgLdvvRct;
            
            item.TgLlvvMm = TgLlvvMm;
            
            item.BaLog = BaLog;
            
            item.BatBhQtbl = BatBhQtbl;
            
            item.BatHbBt = BatHbBt;
            
            item.BatHbGd = BatHbGd;
            
            item.BatKbCbp = BatKbCbp;
            
            item.BatKbCdbd = BatKbCdbd;
            
            item.BatKbCdrv = BatKbCdrv;
            
            item.BatKbDxl = BatKbDxl;
            
            item.BatKbKqcls = BatKbKqcls;
            
            item.BatKbTt = BatKbTt;
            
            item.BatLdvv = BatLdvv;
            
            item.NgaySua = NgaySua;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayKham = NgayKham;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,string SoBenhAn,int IdBnhan,string NgaySinh,string ThangSinh,string DanToc,string NoiLamviec,string ThongtinLhe,string DthoaiLhe,string CdoanGioithieu,short? YTe,short? LdoVaovienMm,short? LdoVaovienGsc,short? LdoVaovienKndn,short? LdoVaovienGtl,short? LdoVaovienKhac,string HbNam,string HbNoiCdoan,short? HbDieuTri,short? HbTiemInsulin,string HbInsulin1,string HbInsulin2,string HbThuochaDuonghuyet,string HbHa,string HbRllp,string HbThuocChongdong,short? HtaiMm,short? HtaiGaysc,string HtaiKgDau,string HtaiKgSau,short? HtaiKhatnhieu,string HtaiUong,short? HtaiDainhieu,string HtaiDai,short? HtaiGiamtl,string HtaiKhac,short? TsbBtChuaphathien,short? TsbBtNhoimaucotim,string TsbBtNamNmct,short? TsbBtTbmn,string TsbBtNamTbmn,string TsbHaNam,string TsbHaMax,short? TsbBtCON4000,string TsbBtKhac,short? TsgdDtd,short? TsgdTanghuyetap,short? TsgdNhoimaucotim,string TsgdKhac,short? KcbMatnuoc,short? KcbXuathuyet,short? KcbPhu,string KcbToanthanKhac,string KcbNhiptim,short? KcbTthaiNhiptim,string KcbTiengtim,string KcbHohap,string KcbBung,short? KcbChanphai,string KcbChanphaiKhac,short? KcbChantrai,string KcbChantraiKhac,string KcbMatphai,string KcbMattrai,string KcbRanghammat,string KcbTkinhKhac,string KcbMach,string KcbNhietdo,string KcbHuyetap1,string KcbHuyetap2,string KcbNhiptho,string KcbCannang,string KcbChieucao,string KcbBmi,string KcbTomtatClsChinh,string KcbCdoanBdau,string KcbDaXly,string KcbCdoanRavien,DateTime? KcbDtriTungay,DateTime? KcbDtriDenngay,string TkbaQtrinhBenhlyDbienCls,string TkbaPphapDtri,string DiaChi,string DoiTuong,string IcdBenhChnh,string IcdBenhPhu,short? NamSinh,string SoBhyt,string TenBenhChinh,string TenBenhPhu,string TkbaHuongTtTieptheo,string TkbaTomtatKqCls,string TkbaTtRavien,short? TrangThai,short? InPhieuLog,short? LoaiBenhAn,DateTime? NgayIn,string NguoiIn,short? TgHbHtGsc,string TgHbHtKgcuoi,string TgHbHtKgdau,string TgHbHtKhac,short? TgHbHtMl,short? TgHbHtMm,short? TgHbHtNnb,short? TgHbHtRct,short? TgHbHtRlth,string TgHbHtSldn,string TgHbHtTcp,short? TgHbHtVtgt,string TgHbQtblChandoan,short? TgHbQtblDieutriDeu,string TgHbQtblNam,string TgHbQtblNoiChanDoan,string TgHbQtblThuocDIEUTRI1,string TgHbQtblThuocDIEUTRI2,string TgHbQtblThuocDIEUTRI3,string TgHbQtblThuocDIEUTRI4,string TgHbQtblThuocDIEUTRI5,short? TgHbTsbBasedow,short? TgHbTsbBc,short? TgHbTsbBldd,short? TgHbTsbHpq,string TgHbTsbKhac,string TgHbTsbKHAC1,short? TgHbTsbKn,string TgKbKbB,short? TgKbKbBlt,string TgKbKbBmi,short? TgKbKbBN2T,short? TgKbKbBntp,short? TgKbKbBntt,string TgKbKbCannang,string TgKbKbCdbd,string TgKbKbCdrv,string TgKbKbChieucao,short? TgKbKbCmmv,DateTime? TgKbKbDenngay,short? TgKbKbDna,string TgKbKbDxl,string TgKbKbHh,string TgKbKbHuyetap,short? TgKbKbLa,short? TgKbKbLb,short? TgKbKbLl,short? TgKbKbLll,string TgKbKbMach,short? TgKbKbMdc,short? TgKbKbMdm,string TgKbKbMp,string TgKbKbMt,string TgKbKbNhietdo,string TgKbKbNhiptho,string TgKbKbNt,short? TgKbKbNtDeu,string TgKbKbTk,short? TgKbKbTr,string TgKbKbTt,string TgKbKbTtkqcks,short? TgKbKbTtltp,short? TgKbKbTtltt,short? TgKbKbTtttp,short? TgKbKbTtttt,DateTime? TgKbKbTungay,short? TgLdvvCt,short? TgLdvvGsc,short? TgLdvvKhac,string TgLdvvKHAC1,short? TgLdvvMl,short? TgLdvvRct,short? TgLlvvMm,short? BaLog,string BatBhQtbl,string BatHbBt,string BatHbGd,string BatKbCbp,string BatKbCdbd,string BatKbCdrv,string BatKbDxl,string BatKbKqcls,string BatKbTt,string BatLdvv,DateTime? NgaySua,DateTime? NgayTao,string NguoiSua,string NguoiTao,DateTime? NgayKham)
	    {
		    TBenhAnNgoaitru item = new TBenhAnNgoaitru();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.SoBenhAn = SoBenhAn;
				
			item.IdBnhan = IdBnhan;
				
			item.NgaySinh = NgaySinh;
				
			item.ThangSinh = ThangSinh;
				
			item.DanToc = DanToc;
				
			item.NoiLamviec = NoiLamviec;
				
			item.ThongtinLhe = ThongtinLhe;
				
			item.DthoaiLhe = DthoaiLhe;
				
			item.CdoanGioithieu = CdoanGioithieu;
				
			item.YTe = YTe;
				
			item.LdoVaovienMm = LdoVaovienMm;
				
			item.LdoVaovienGsc = LdoVaovienGsc;
				
			item.LdoVaovienKndn = LdoVaovienKndn;
				
			item.LdoVaovienGtl = LdoVaovienGtl;
				
			item.LdoVaovienKhac = LdoVaovienKhac;
				
			item.HbNam = HbNam;
				
			item.HbNoiCdoan = HbNoiCdoan;
				
			item.HbDieuTri = HbDieuTri;
				
			item.HbTiemInsulin = HbTiemInsulin;
				
			item.HbInsulin1 = HbInsulin1;
				
			item.HbInsulin2 = HbInsulin2;
				
			item.HbThuochaDuonghuyet = HbThuochaDuonghuyet;
				
			item.HbHa = HbHa;
				
			item.HbRllp = HbRllp;
				
			item.HbThuocChongdong = HbThuocChongdong;
				
			item.HtaiMm = HtaiMm;
				
			item.HtaiGaysc = HtaiGaysc;
				
			item.HtaiKgDau = HtaiKgDau;
				
			item.HtaiKgSau = HtaiKgSau;
				
			item.HtaiKhatnhieu = HtaiKhatnhieu;
				
			item.HtaiUong = HtaiUong;
				
			item.HtaiDainhieu = HtaiDainhieu;
				
			item.HtaiDai = HtaiDai;
				
			item.HtaiGiamtl = HtaiGiamtl;
				
			item.HtaiKhac = HtaiKhac;
				
			item.TsbBtChuaphathien = TsbBtChuaphathien;
				
			item.TsbBtNhoimaucotim = TsbBtNhoimaucotim;
				
			item.TsbBtNamNmct = TsbBtNamNmct;
				
			item.TsbBtTbmn = TsbBtTbmn;
				
			item.TsbBtNamTbmn = TsbBtNamTbmn;
				
			item.TsbHaNam = TsbHaNam;
				
			item.TsbHaMax = TsbHaMax;
				
			item.TsbBtCON4000 = TsbBtCON4000;
				
			item.TsbBtKhac = TsbBtKhac;
				
			item.TsgdDtd = TsgdDtd;
				
			item.TsgdTanghuyetap = TsgdTanghuyetap;
				
			item.TsgdNhoimaucotim = TsgdNhoimaucotim;
				
			item.TsgdKhac = TsgdKhac;
				
			item.KcbMatnuoc = KcbMatnuoc;
				
			item.KcbXuathuyet = KcbXuathuyet;
				
			item.KcbPhu = KcbPhu;
				
			item.KcbToanthanKhac = KcbToanthanKhac;
				
			item.KcbNhiptim = KcbNhiptim;
				
			item.KcbTthaiNhiptim = KcbTthaiNhiptim;
				
			item.KcbTiengtim = KcbTiengtim;
				
			item.KcbHohap = KcbHohap;
				
			item.KcbBung = KcbBung;
				
			item.KcbChanphai = KcbChanphai;
				
			item.KcbChanphaiKhac = KcbChanphaiKhac;
				
			item.KcbChantrai = KcbChantrai;
				
			item.KcbChantraiKhac = KcbChantraiKhac;
				
			item.KcbMatphai = KcbMatphai;
				
			item.KcbMattrai = KcbMattrai;
				
			item.KcbRanghammat = KcbRanghammat;
				
			item.KcbTkinhKhac = KcbTkinhKhac;
				
			item.KcbMach = KcbMach;
				
			item.KcbNhietdo = KcbNhietdo;
				
			item.KcbHuyetap1 = KcbHuyetap1;
				
			item.KcbHuyetap2 = KcbHuyetap2;
				
			item.KcbNhiptho = KcbNhiptho;
				
			item.KcbCannang = KcbCannang;
				
			item.KcbChieucao = KcbChieucao;
				
			item.KcbBmi = KcbBmi;
				
			item.KcbTomtatClsChinh = KcbTomtatClsChinh;
				
			item.KcbCdoanBdau = KcbCdoanBdau;
				
			item.KcbDaXly = KcbDaXly;
				
			item.KcbCdoanRavien = KcbCdoanRavien;
				
			item.KcbDtriTungay = KcbDtriTungay;
				
			item.KcbDtriDenngay = KcbDtriDenngay;
				
			item.TkbaQtrinhBenhlyDbienCls = TkbaQtrinhBenhlyDbienCls;
				
			item.TkbaPphapDtri = TkbaPphapDtri;
				
			item.DiaChi = DiaChi;
				
			item.DoiTuong = DoiTuong;
				
			item.IcdBenhChnh = IcdBenhChnh;
				
			item.IcdBenhPhu = IcdBenhPhu;
				
			item.NamSinh = NamSinh;
				
			item.SoBhyt = SoBhyt;
				
			item.TenBenhChinh = TenBenhChinh;
				
			item.TenBenhPhu = TenBenhPhu;
				
			item.TkbaHuongTtTieptheo = TkbaHuongTtTieptheo;
				
			item.TkbaTomtatKqCls = TkbaTomtatKqCls;
				
			item.TkbaTtRavien = TkbaTtRavien;
				
			item.TrangThai = TrangThai;
				
			item.InPhieuLog = InPhieuLog;
				
			item.LoaiBenhAn = LoaiBenhAn;
				
			item.NgayIn = NgayIn;
				
			item.NguoiIn = NguoiIn;
				
			item.TgHbHtGsc = TgHbHtGsc;
				
			item.TgHbHtKgcuoi = TgHbHtKgcuoi;
				
			item.TgHbHtKgdau = TgHbHtKgdau;
				
			item.TgHbHtKhac = TgHbHtKhac;
				
			item.TgHbHtMl = TgHbHtMl;
				
			item.TgHbHtMm = TgHbHtMm;
				
			item.TgHbHtNnb = TgHbHtNnb;
				
			item.TgHbHtRct = TgHbHtRct;
				
			item.TgHbHtRlth = TgHbHtRlth;
				
			item.TgHbHtSldn = TgHbHtSldn;
				
			item.TgHbHtTcp = TgHbHtTcp;
				
			item.TgHbHtVtgt = TgHbHtVtgt;
				
			item.TgHbQtblChandoan = TgHbQtblChandoan;
				
			item.TgHbQtblDieutriDeu = TgHbQtblDieutriDeu;
				
			item.TgHbQtblNam = TgHbQtblNam;
				
			item.TgHbQtblNoiChanDoan = TgHbQtblNoiChanDoan;
				
			item.TgHbQtblThuocDIEUTRI1 = TgHbQtblThuocDIEUTRI1;
				
			item.TgHbQtblThuocDIEUTRI2 = TgHbQtblThuocDIEUTRI2;
				
			item.TgHbQtblThuocDIEUTRI3 = TgHbQtblThuocDIEUTRI3;
				
			item.TgHbQtblThuocDIEUTRI4 = TgHbQtblThuocDIEUTRI4;
				
			item.TgHbQtblThuocDIEUTRI5 = TgHbQtblThuocDIEUTRI5;
				
			item.TgHbTsbBasedow = TgHbTsbBasedow;
				
			item.TgHbTsbBc = TgHbTsbBc;
				
			item.TgHbTsbBldd = TgHbTsbBldd;
				
			item.TgHbTsbHpq = TgHbTsbHpq;
				
			item.TgHbTsbKhac = TgHbTsbKhac;
				
			item.TgHbTsbKHAC1 = TgHbTsbKHAC1;
				
			item.TgHbTsbKn = TgHbTsbKn;
				
			item.TgKbKbB = TgKbKbB;
				
			item.TgKbKbBlt = TgKbKbBlt;
				
			item.TgKbKbBmi = TgKbKbBmi;
				
			item.TgKbKbBN2T = TgKbKbBN2T;
				
			item.TgKbKbBntp = TgKbKbBntp;
				
			item.TgKbKbBntt = TgKbKbBntt;
				
			item.TgKbKbCannang = TgKbKbCannang;
				
			item.TgKbKbCdbd = TgKbKbCdbd;
				
			item.TgKbKbCdrv = TgKbKbCdrv;
				
			item.TgKbKbChieucao = TgKbKbChieucao;
				
			item.TgKbKbCmmv = TgKbKbCmmv;
				
			item.TgKbKbDenngay = TgKbKbDenngay;
				
			item.TgKbKbDna = TgKbKbDna;
				
			item.TgKbKbDxl = TgKbKbDxl;
				
			item.TgKbKbHh = TgKbKbHh;
				
			item.TgKbKbHuyetap = TgKbKbHuyetap;
				
			item.TgKbKbLa = TgKbKbLa;
				
			item.TgKbKbLb = TgKbKbLb;
				
			item.TgKbKbLl = TgKbKbLl;
				
			item.TgKbKbLll = TgKbKbLll;
				
			item.TgKbKbMach = TgKbKbMach;
				
			item.TgKbKbMdc = TgKbKbMdc;
				
			item.TgKbKbMdm = TgKbKbMdm;
				
			item.TgKbKbMp = TgKbKbMp;
				
			item.TgKbKbMt = TgKbKbMt;
				
			item.TgKbKbNhietdo = TgKbKbNhietdo;
				
			item.TgKbKbNhiptho = TgKbKbNhiptho;
				
			item.TgKbKbNt = TgKbKbNt;
				
			item.TgKbKbNtDeu = TgKbKbNtDeu;
				
			item.TgKbKbTk = TgKbKbTk;
				
			item.TgKbKbTr = TgKbKbTr;
				
			item.TgKbKbTt = TgKbKbTt;
				
			item.TgKbKbTtkqcks = TgKbKbTtkqcks;
				
			item.TgKbKbTtltp = TgKbKbTtltp;
				
			item.TgKbKbTtltt = TgKbKbTtltt;
				
			item.TgKbKbTtttp = TgKbKbTtttp;
				
			item.TgKbKbTtttt = TgKbKbTtttt;
				
			item.TgKbKbTungay = TgKbKbTungay;
				
			item.TgLdvvCt = TgLdvvCt;
				
			item.TgLdvvGsc = TgLdvvGsc;
				
			item.TgLdvvKhac = TgLdvvKhac;
				
			item.TgLdvvKHAC1 = TgLdvvKHAC1;
				
			item.TgLdvvMl = TgLdvvMl;
				
			item.TgLdvvRct = TgLdvvRct;
				
			item.TgLlvvMm = TgLlvvMm;
				
			item.BaLog = BaLog;
				
			item.BatBhQtbl = BatBhQtbl;
				
			item.BatHbBt = BatHbBt;
				
			item.BatHbGd = BatHbGd;
				
			item.BatKbCbp = BatKbCbp;
				
			item.BatKbCdbd = BatKbCdbd;
				
			item.BatKbCdrv = BatKbCdrv;
				
			item.BatKbDxl = BatKbDxl;
				
			item.BatKbKqcls = BatKbKqcls;
				
			item.BatKbTt = BatKbTt;
				
			item.BatLdvv = BatLdvv;
				
			item.NgaySua = NgaySua;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayKham = NgayKham;
				
	        item.Save(UserName);
	    }
    }
}
