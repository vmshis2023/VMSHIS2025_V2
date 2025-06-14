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
    /// Controller class for kcb_danhsach_benhnhan
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbDanhsachBenhnhanController
    {
        // Preload our schema..
        KcbDanhsachBenhnhan thisSchemaLoad = new KcbDanhsachBenhnhan();
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
        public KcbDanhsachBenhnhanCollection FetchAll()
        {
            KcbDanhsachBenhnhanCollection coll = new KcbDanhsachBenhnhanCollection();
            Query qry = new Query(KcbDanhsachBenhnhan.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbDanhsachBenhnhanCollection FetchByID(object IdBenhnhan)
        {
            KcbDanhsachBenhnhanCollection coll = new KcbDanhsachBenhnhanCollection().Where("id_benhnhan", IdBenhnhan).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbDanhsachBenhnhanCollection FetchByQuery(Query qry)
        {
            KcbDanhsachBenhnhanCollection coll = new KcbDanhsachBenhnhanCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdBenhnhan)
        {
            return (KcbDanhsachBenhnhan.Delete(IdBenhnhan) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdBenhnhan)
        {
            return (KcbDanhsachBenhnhan.Destroy(IdBenhnhan) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string TenBenhnhan,string DiaChi,string MaTinhThanhpho,string MaQuanhuyen,DateTime? NgaySinh,short? NamSinh,byte? IdGioitinh,string GioiTinh,string NgheNghiep,string CoQuan,string Cmt,string MaQuocgia,string DienThoai,string Email,DateTime NgayTiepdon,string NguoiTiepdon,string DanToc,string TonGiao,DateTime? NgaySua,DateTime? NgayTao,string NguoiSua,string NguoiTao,string DiachiBhyt,string NguonGoc,byte? KieuBenhnhan,byte? MacDinh,string IpMaytao,string IpMaysua,string TenMaytao,string TenMaysua,string DiachiLienhe,string DienthoaiLienhe,string Fax,string NguoiLienhe,string SoTiemchungQg,string LastActionName,string ChucVu,string MaBenhnhan,string SoLo,string LastNoigioithieu,string FirstNoigioithieu,string CanhBao,string SonhaDuongpho,string MaXaphuong,string NhomMau,string MasoThue)
	    {
		    KcbDanhsachBenhnhan item = new KcbDanhsachBenhnhan();
		    
            item.TenBenhnhan = TenBenhnhan;
            
            item.DiaChi = DiaChi;
            
            item.MaTinhThanhpho = MaTinhThanhpho;
            
            item.MaQuanhuyen = MaQuanhuyen;
            
            item.NgaySinh = NgaySinh;
            
            item.NamSinh = NamSinh;
            
            item.IdGioitinh = IdGioitinh;
            
            item.GioiTinh = GioiTinh;
            
            item.NgheNghiep = NgheNghiep;
            
            item.CoQuan = CoQuan;
            
            item.Cmt = Cmt;
            
            item.MaQuocgia = MaQuocgia;
            
            item.DienThoai = DienThoai;
            
            item.Email = Email;
            
            item.NgayTiepdon = NgayTiepdon;
            
            item.NguoiTiepdon = NguoiTiepdon;
            
            item.DanToc = DanToc;
            
            item.TonGiao = TonGiao;
            
            item.NgaySua = NgaySua;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NguoiTao = NguoiTao;
            
            item.DiachiBhyt = DiachiBhyt;
            
            item.NguonGoc = NguonGoc;
            
            item.KieuBenhnhan = KieuBenhnhan;
            
            item.MacDinh = MacDinh;
            
            item.IpMaytao = IpMaytao;
            
            item.IpMaysua = IpMaysua;
            
            item.TenMaytao = TenMaytao;
            
            item.TenMaysua = TenMaysua;
            
            item.DiachiLienhe = DiachiLienhe;
            
            item.DienthoaiLienhe = DienthoaiLienhe;
            
            item.Fax = Fax;
            
            item.NguoiLienhe = NguoiLienhe;
            
            item.SoTiemchungQg = SoTiemchungQg;
            
            item.LastActionName = LastActionName;
            
            item.ChucVu = ChucVu;
            
            item.MaBenhnhan = MaBenhnhan;
            
            item.SoLo = SoLo;
            
            item.LastNoigioithieu = LastNoigioithieu;
            
            item.FirstNoigioithieu = FirstNoigioithieu;
            
            item.CanhBao = CanhBao;
            
            item.SonhaDuongpho = SonhaDuongpho;
            
            item.MaXaphuong = MaXaphuong;
            
            item.NhomMau = NhomMau;
            
            item.MasoThue = MasoThue;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdBenhnhan,string TenBenhnhan,string DiaChi,string MaTinhThanhpho,string MaQuanhuyen,DateTime? NgaySinh,short? NamSinh,byte? IdGioitinh,string GioiTinh,string NgheNghiep,string CoQuan,string Cmt,string MaQuocgia,string DienThoai,string Email,DateTime NgayTiepdon,string NguoiTiepdon,string DanToc,string TonGiao,DateTime? NgaySua,DateTime? NgayTao,string NguoiSua,string NguoiTao,string DiachiBhyt,string NguonGoc,byte? KieuBenhnhan,byte? MacDinh,string IpMaytao,string IpMaysua,string TenMaytao,string TenMaysua,string DiachiLienhe,string DienthoaiLienhe,string Fax,string NguoiLienhe,string SoTiemchungQg,string LastActionName,string ChucVu,string MaBenhnhan,string SoLo,string LastNoigioithieu,string FirstNoigioithieu,string CanhBao,string SonhaDuongpho,string MaXaphuong,string NhomMau,string MasoThue)
	    {
		    KcbDanhsachBenhnhan item = new KcbDanhsachBenhnhan();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdBenhnhan = IdBenhnhan;
				
			item.TenBenhnhan = TenBenhnhan;
				
			item.DiaChi = DiaChi;
				
			item.MaTinhThanhpho = MaTinhThanhpho;
				
			item.MaQuanhuyen = MaQuanhuyen;
				
			item.NgaySinh = NgaySinh;
				
			item.NamSinh = NamSinh;
				
			item.IdGioitinh = IdGioitinh;
				
			item.GioiTinh = GioiTinh;
				
			item.NgheNghiep = NgheNghiep;
				
			item.CoQuan = CoQuan;
				
			item.Cmt = Cmt;
				
			item.MaQuocgia = MaQuocgia;
				
			item.DienThoai = DienThoai;
				
			item.Email = Email;
				
			item.NgayTiepdon = NgayTiepdon;
				
			item.NguoiTiepdon = NguoiTiepdon;
				
			item.DanToc = DanToc;
				
			item.TonGiao = TonGiao;
				
			item.NgaySua = NgaySua;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NguoiTao = NguoiTao;
				
			item.DiachiBhyt = DiachiBhyt;
				
			item.NguonGoc = NguonGoc;
				
			item.KieuBenhnhan = KieuBenhnhan;
				
			item.MacDinh = MacDinh;
				
			item.IpMaytao = IpMaytao;
				
			item.IpMaysua = IpMaysua;
				
			item.TenMaytao = TenMaytao;
				
			item.TenMaysua = TenMaysua;
				
			item.DiachiLienhe = DiachiLienhe;
				
			item.DienthoaiLienhe = DienthoaiLienhe;
				
			item.Fax = Fax;
				
			item.NguoiLienhe = NguoiLienhe;
				
			item.SoTiemchungQg = SoTiemchungQg;
				
			item.LastActionName = LastActionName;
				
			item.ChucVu = ChucVu;
				
			item.MaBenhnhan = MaBenhnhan;
				
			item.SoLo = SoLo;
				
			item.LastNoigioithieu = LastNoigioithieu;
				
			item.FirstNoigioithieu = FirstNoigioithieu;
				
			item.CanhBao = CanhBao;
				
			item.SonhaDuongpho = SonhaDuongpho;
				
			item.MaXaphuong = MaXaphuong;
				
			item.NhomMau = NhomMau;
				
			item.MasoThue = MasoThue;
				
	        item.Save(UserName);
	    }
    }
}
