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
namespace VMS.HIS.DAL
{
    /// <summary>
    /// Controller class for kcb_bienbanhoichan
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbBienbanhoichanController
    {
        // Preload our schema..
        KcbBienbanhoichan thisSchemaLoad = new KcbBienbanhoichan();
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
        public KcbBienbanhoichanCollection FetchAll()
        {
            KcbBienbanhoichanCollection coll = new KcbBienbanhoichanCollection();
            Query qry = new Query(KcbBienbanhoichan.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbBienbanhoichanCollection FetchByID(object Id)
        {
            KcbBienbanhoichanCollection coll = new KcbBienbanhoichanCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbBienbanhoichanCollection FetchByQuery(Query qry)
        {
            KcbBienbanhoichanCollection coll = new KcbBienbanhoichanCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (KcbBienbanhoichan.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (KcbBienbanhoichan.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaBbhc,string MaHinhthucHc,string MaLydoHc,long IdBenhnhan,string MaLuotkham,DateTime NgayHoichan,string ChuToa,string ThuKy,string HopTai,string BacsiThamgia,int BacsiDexuat,string YeucauHoichan,string TsbNoikhoa,string TsbSankhoa,string TsbNgoaikhoa,string TsbKhac,string TrangthaiVaovien,string ChanDoan,string DienbienBenh,string ChandoanNguyennhanTienluong,string Pphapdieutri,string ChamSoc,string KetLuan,string QuatrinhChamsoc,string QuatrinhDieutri,string TienLuong,string ChandoanSaukham,string NguyenNhan,string KetluanTienluong,string Huongxuly,string KetluanChandoan,string IdKhoadieutri,DateTime NgayTao,string NguoiTao,string IpMaytao,string MacMaytao,string NguoiSua,DateTime? NgaySua,string IpMaysua,string MacMaysua,string Mach,string Ha,string NhietDo,string Cao,string CanNang,string Bmi,string Nhommau,string ToanThan,string TrieuchungConang,string TrieuchungThucthe,byte? Timach,string TimmachKhac,byte? Hohap,string HohapKhac,string XnHct,string XnHc,string XnBc,string XnTieucau,string XnTqr,string XnTckr,string XnRh,string XnHiv,string XnHcv,string XnHbsAg,string XnQuicktest,string XnGlucose,string XnUre,string XnCreatinin,string XnAst,string XnAlt,string XnNuoctieu,string CdhaXq,string CdhaSa,string CdhaDientim,string CdhaCt,string CdhaMri,string CdhaKhac,string BenhlyKemtheo,byte? PhanloaiVetmo,string KhangsinhDukien,string PhuongphapPttt,string PhuongphapVocam,DateTime? DukienthoigianPttt,string IdbacsiPttt,string IdbacsiPtttPhu,string ChuanbiChuyenbiet,string TutheNguoibenh,string DutruMau,string IdbacsiGayme)
	    {
		    KcbBienbanhoichan item = new KcbBienbanhoichan();
		    
            item.MaBbhc = MaBbhc;
            
            item.MaHinhthucHc = MaHinhthucHc;
            
            item.MaLydoHc = MaLydoHc;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.MaLuotkham = MaLuotkham;
            
            item.NgayHoichan = NgayHoichan;
            
            item.ChuToa = ChuToa;
            
            item.ThuKy = ThuKy;
            
            item.HopTai = HopTai;
            
            item.BacsiThamgia = BacsiThamgia;
            
            item.BacsiDexuat = BacsiDexuat;
            
            item.YeucauHoichan = YeucauHoichan;
            
            item.TsbNoikhoa = TsbNoikhoa;
            
            item.TsbSankhoa = TsbSankhoa;
            
            item.TsbNgoaikhoa = TsbNgoaikhoa;
            
            item.TsbKhac = TsbKhac;
            
            item.TrangthaiVaovien = TrangthaiVaovien;
            
            item.ChanDoan = ChanDoan;
            
            item.DienbienBenh = DienbienBenh;
            
            item.ChandoanNguyennhanTienluong = ChandoanNguyennhanTienluong;
            
            item.Pphapdieutri = Pphapdieutri;
            
            item.ChamSoc = ChamSoc;
            
            item.KetLuan = KetLuan;
            
            item.QuatrinhChamsoc = QuatrinhChamsoc;
            
            item.QuatrinhDieutri = QuatrinhDieutri;
            
            item.TienLuong = TienLuong;
            
            item.ChandoanSaukham = ChandoanSaukham;
            
            item.NguyenNhan = NguyenNhan;
            
            item.KetluanTienluong = KetluanTienluong;
            
            item.Huongxuly = Huongxuly;
            
            item.KetluanChandoan = KetluanChandoan;
            
            item.IdKhoadieutri = IdKhoadieutri;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
            item.IpMaytao = IpMaytao;
            
            item.MacMaytao = MacMaytao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.IpMaysua = IpMaysua;
            
            item.MacMaysua = MacMaysua;
            
            item.Mach = Mach;
            
            item.Ha = Ha;
            
            item.NhietDo = NhietDo;
            
            item.Cao = Cao;
            
            item.CanNang = CanNang;
            
            item.Bmi = Bmi;
            
            item.Nhommau = Nhommau;
            
            item.ToanThan = ToanThan;
            
            item.TrieuchungConang = TrieuchungConang;
            
            item.TrieuchungThucthe = TrieuchungThucthe;
            
            item.Timach = Timach;
            
            item.TimmachKhac = TimmachKhac;
            
            item.Hohap = Hohap;
            
            item.HohapKhac = HohapKhac;
            
            item.XnHct = XnHct;
            
            item.XnHc = XnHc;
            
            item.XnBc = XnBc;
            
            item.XnTieucau = XnTieucau;
            
            item.XnTqr = XnTqr;
            
            item.XnTckr = XnTckr;
            
            item.XnRh = XnRh;
            
            item.XnHiv = XnHiv;
            
            item.XnHcv = XnHcv;
            
            item.XnHbsAg = XnHbsAg;
            
            item.XnQuicktest = XnQuicktest;
            
            item.XnGlucose = XnGlucose;
            
            item.XnUre = XnUre;
            
            item.XnCreatinin = XnCreatinin;
            
            item.XnAst = XnAst;
            
            item.XnAlt = XnAlt;
            
            item.XnNuoctieu = XnNuoctieu;
            
            item.CdhaXq = CdhaXq;
            
            item.CdhaSa = CdhaSa;
            
            item.CdhaDientim = CdhaDientim;
            
            item.CdhaCt = CdhaCt;
            
            item.CdhaMri = CdhaMri;
            
            item.CdhaKhac = CdhaKhac;
            
            item.BenhlyKemtheo = BenhlyKemtheo;
            
            item.PhanloaiVetmo = PhanloaiVetmo;
            
            item.KhangsinhDukien = KhangsinhDukien;
            
            item.PhuongphapPttt = PhuongphapPttt;
            
            item.PhuongphapVocam = PhuongphapVocam;
            
            item.DukienthoigianPttt = DukienthoigianPttt;
            
            item.IdbacsiPttt = IdbacsiPttt;
            
            item.IdbacsiPtttPhu = IdbacsiPtttPhu;
            
            item.ChuanbiChuyenbiet = ChuanbiChuyenbiet;
            
            item.TutheNguoibenh = TutheNguoibenh;
            
            item.DutruMau = DutruMau;
            
            item.IdbacsiGayme = IdbacsiGayme;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,string MaBbhc,string MaHinhthucHc,string MaLydoHc,long IdBenhnhan,string MaLuotkham,DateTime NgayHoichan,string ChuToa,string ThuKy,string HopTai,string BacsiThamgia,int BacsiDexuat,string YeucauHoichan,string TsbNoikhoa,string TsbSankhoa,string TsbNgoaikhoa,string TsbKhac,string TrangthaiVaovien,string ChanDoan,string DienbienBenh,string ChandoanNguyennhanTienluong,string Pphapdieutri,string ChamSoc,string KetLuan,string QuatrinhChamsoc,string QuatrinhDieutri,string TienLuong,string ChandoanSaukham,string NguyenNhan,string KetluanTienluong,string Huongxuly,string KetluanChandoan,string IdKhoadieutri,DateTime NgayTao,string NguoiTao,string IpMaytao,string MacMaytao,string NguoiSua,DateTime? NgaySua,string IpMaysua,string MacMaysua,string Mach,string Ha,string NhietDo,string Cao,string CanNang,string Bmi,string Nhommau,string ToanThan,string TrieuchungConang,string TrieuchungThucthe,byte? Timach,string TimmachKhac,byte? Hohap,string HohapKhac,string XnHct,string XnHc,string XnBc,string XnTieucau,string XnTqr,string XnTckr,string XnRh,string XnHiv,string XnHcv,string XnHbsAg,string XnQuicktest,string XnGlucose,string XnUre,string XnCreatinin,string XnAst,string XnAlt,string XnNuoctieu,string CdhaXq,string CdhaSa,string CdhaDientim,string CdhaCt,string CdhaMri,string CdhaKhac,string BenhlyKemtheo,byte? PhanloaiVetmo,string KhangsinhDukien,string PhuongphapPttt,string PhuongphapVocam,DateTime? DukienthoigianPttt,string IdbacsiPttt,string IdbacsiPtttPhu,string ChuanbiChuyenbiet,string TutheNguoibenh,string DutruMau,string IdbacsiGayme)
	    {
		    KcbBienbanhoichan item = new KcbBienbanhoichan();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.MaBbhc = MaBbhc;
				
			item.MaHinhthucHc = MaHinhthucHc;
				
			item.MaLydoHc = MaLydoHc;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.MaLuotkham = MaLuotkham;
				
			item.NgayHoichan = NgayHoichan;
				
			item.ChuToa = ChuToa;
				
			item.ThuKy = ThuKy;
				
			item.HopTai = HopTai;
				
			item.BacsiThamgia = BacsiThamgia;
				
			item.BacsiDexuat = BacsiDexuat;
				
			item.YeucauHoichan = YeucauHoichan;
				
			item.TsbNoikhoa = TsbNoikhoa;
				
			item.TsbSankhoa = TsbSankhoa;
				
			item.TsbNgoaikhoa = TsbNgoaikhoa;
				
			item.TsbKhac = TsbKhac;
				
			item.TrangthaiVaovien = TrangthaiVaovien;
				
			item.ChanDoan = ChanDoan;
				
			item.DienbienBenh = DienbienBenh;
				
			item.ChandoanNguyennhanTienluong = ChandoanNguyennhanTienluong;
				
			item.Pphapdieutri = Pphapdieutri;
				
			item.ChamSoc = ChamSoc;
				
			item.KetLuan = KetLuan;
				
			item.QuatrinhChamsoc = QuatrinhChamsoc;
				
			item.QuatrinhDieutri = QuatrinhDieutri;
				
			item.TienLuong = TienLuong;
				
			item.ChandoanSaukham = ChandoanSaukham;
				
			item.NguyenNhan = NguyenNhan;
				
			item.KetluanTienluong = KetluanTienluong;
				
			item.Huongxuly = Huongxuly;
				
			item.KetluanChandoan = KetluanChandoan;
				
			item.IdKhoadieutri = IdKhoadieutri;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
			item.IpMaytao = IpMaytao;
				
			item.MacMaytao = MacMaytao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.IpMaysua = IpMaysua;
				
			item.MacMaysua = MacMaysua;
				
			item.Mach = Mach;
				
			item.Ha = Ha;
				
			item.NhietDo = NhietDo;
				
			item.Cao = Cao;
				
			item.CanNang = CanNang;
				
			item.Bmi = Bmi;
				
			item.Nhommau = Nhommau;
				
			item.ToanThan = ToanThan;
				
			item.TrieuchungConang = TrieuchungConang;
				
			item.TrieuchungThucthe = TrieuchungThucthe;
				
			item.Timach = Timach;
				
			item.TimmachKhac = TimmachKhac;
				
			item.Hohap = Hohap;
				
			item.HohapKhac = HohapKhac;
				
			item.XnHct = XnHct;
				
			item.XnHc = XnHc;
				
			item.XnBc = XnBc;
				
			item.XnTieucau = XnTieucau;
				
			item.XnTqr = XnTqr;
				
			item.XnTckr = XnTckr;
				
			item.XnRh = XnRh;
				
			item.XnHiv = XnHiv;
				
			item.XnHcv = XnHcv;
				
			item.XnHbsAg = XnHbsAg;
				
			item.XnQuicktest = XnQuicktest;
				
			item.XnGlucose = XnGlucose;
				
			item.XnUre = XnUre;
				
			item.XnCreatinin = XnCreatinin;
				
			item.XnAst = XnAst;
				
			item.XnAlt = XnAlt;
				
			item.XnNuoctieu = XnNuoctieu;
				
			item.CdhaXq = CdhaXq;
				
			item.CdhaSa = CdhaSa;
				
			item.CdhaDientim = CdhaDientim;
				
			item.CdhaCt = CdhaCt;
				
			item.CdhaMri = CdhaMri;
				
			item.CdhaKhac = CdhaKhac;
				
			item.BenhlyKemtheo = BenhlyKemtheo;
				
			item.PhanloaiVetmo = PhanloaiVetmo;
				
			item.KhangsinhDukien = KhangsinhDukien;
				
			item.PhuongphapPttt = PhuongphapPttt;
				
			item.PhuongphapVocam = PhuongphapVocam;
				
			item.DukienthoigianPttt = DukienthoigianPttt;
				
			item.IdbacsiPttt = IdbacsiPttt;
				
			item.IdbacsiPtttPhu = IdbacsiPtttPhu;
				
			item.ChuanbiChuyenbiet = ChuanbiChuyenbiet;
				
			item.TutheNguoibenh = TutheNguoibenh;
				
			item.DutruMau = DutruMau;
				
			item.IdbacsiGayme = IdbacsiGayme;
				
	        item.Save(UserName);
	    }
    }
}
