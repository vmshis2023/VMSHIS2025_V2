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
    /// Controller class for t_phieu_nhapxuatthuoc
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TPhieuNhapxuatthuocController
    {
        // Preload our schema..
        TPhieuNhapxuatthuoc thisSchemaLoad = new TPhieuNhapxuatthuoc();
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
        public TPhieuNhapxuatthuocCollection FetchAll()
        {
            TPhieuNhapxuatthuocCollection coll = new TPhieuNhapxuatthuocCollection();
            Query qry = new Query(TPhieuNhapxuatthuoc.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TPhieuNhapxuatthuocCollection FetchByID(object IdPhieu)
        {
            TPhieuNhapxuatthuocCollection coll = new TPhieuNhapxuatthuocCollection().Where("id_phieu", IdPhieu).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TPhieuNhapxuatthuocCollection FetchByQuery(Query qry)
        {
            TPhieuNhapxuatthuocCollection coll = new TPhieuNhapxuatthuocCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieu)
        {
            return (TPhieuNhapxuatthuoc.Delete(IdPhieu) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieu)
        {
            return (TPhieuNhapxuatthuoc.Destroy(IdPhieu) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaPhieu,string SoHoadon,DateTime NgayHoadon,short? IdKhonhap,short? IdKhoxuat,decimal? Vat,string MaNhacungcap,string MotaThem,string NguoiGiao,string NguoiNhan,string NguoiHuy,short? IdNhanvien,byte? SttHthi,byte? TrangThai,DateTime? NgayTao,string NguoiTao,string NguoiSua,DateTime? NgaySua,decimal? TongTien,byte? LoaiPhieu,string TenLoaiphieu,short? IdNhanvienXacnhan,string NguoiXacnhan,DateTime? NgayXacnhan,short? IdKhoalinh,string KieuThuocvattu,string SoChungtuKemtheo,byte? PhieuVay,string TkNo,string TkCo,byte? DuTru,string HoidongThanhly,string DiadiemThanhly,string YkienDexuat,DateTime? ThoigianThanhlyTu,DateTime? ThoigianThanhlyDen,byte? NoiTru,string LastActionName,DateTime? NgayLap,byte? TrongThau)
	    {
		    TPhieuNhapxuatthuoc item = new TPhieuNhapxuatthuoc();
		    
            item.MaPhieu = MaPhieu;
            
            item.SoHoadon = SoHoadon;
            
            item.NgayHoadon = NgayHoadon;
            
            item.IdKhonhap = IdKhonhap;
            
            item.IdKhoxuat = IdKhoxuat;
            
            item.Vat = Vat;
            
            item.MaNhacungcap = MaNhacungcap;
            
            item.MotaThem = MotaThem;
            
            item.NguoiGiao = NguoiGiao;
            
            item.NguoiNhan = NguoiNhan;
            
            item.NguoiHuy = NguoiHuy;
            
            item.IdNhanvien = IdNhanvien;
            
            item.SttHthi = SttHthi;
            
            item.TrangThai = TrangThai;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.TongTien = TongTien;
            
            item.LoaiPhieu = LoaiPhieu;
            
            item.TenLoaiphieu = TenLoaiphieu;
            
            item.IdNhanvienXacnhan = IdNhanvienXacnhan;
            
            item.NguoiXacnhan = NguoiXacnhan;
            
            item.NgayXacnhan = NgayXacnhan;
            
            item.IdKhoalinh = IdKhoalinh;
            
            item.KieuThuocvattu = KieuThuocvattu;
            
            item.SoChungtuKemtheo = SoChungtuKemtheo;
            
            item.PhieuVay = PhieuVay;
            
            item.TkNo = TkNo;
            
            item.TkCo = TkCo;
            
            item.DuTru = DuTru;
            
            item.HoidongThanhly = HoidongThanhly;
            
            item.DiadiemThanhly = DiadiemThanhly;
            
            item.YkienDexuat = YkienDexuat;
            
            item.ThoigianThanhlyTu = ThoigianThanhlyTu;
            
            item.ThoigianThanhlyDen = ThoigianThanhlyDen;
            
            item.NoiTru = NoiTru;
            
            item.LastActionName = LastActionName;
            
            item.NgayLap = NgayLap;
            
            item.TrongThau = TrongThau;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdPhieu,string MaPhieu,string SoHoadon,DateTime NgayHoadon,short? IdKhonhap,short? IdKhoxuat,decimal? Vat,string MaNhacungcap,string MotaThem,string NguoiGiao,string NguoiNhan,string NguoiHuy,short? IdNhanvien,byte? SttHthi,byte? TrangThai,DateTime? NgayTao,string NguoiTao,string NguoiSua,DateTime? NgaySua,decimal? TongTien,byte? LoaiPhieu,string TenLoaiphieu,short? IdNhanvienXacnhan,string NguoiXacnhan,DateTime? NgayXacnhan,short? IdKhoalinh,string KieuThuocvattu,string SoChungtuKemtheo,byte? PhieuVay,string TkNo,string TkCo,byte? DuTru,string HoidongThanhly,string DiadiemThanhly,string YkienDexuat,DateTime? ThoigianThanhlyTu,DateTime? ThoigianThanhlyDen,byte? NoiTru,string LastActionName,DateTime? NgayLap,byte? TrongThau)
	    {
		    TPhieuNhapxuatthuoc item = new TPhieuNhapxuatthuoc();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieu = IdPhieu;
				
			item.MaPhieu = MaPhieu;
				
			item.SoHoadon = SoHoadon;
				
			item.NgayHoadon = NgayHoadon;
				
			item.IdKhonhap = IdKhonhap;
				
			item.IdKhoxuat = IdKhoxuat;
				
			item.Vat = Vat;
				
			item.MaNhacungcap = MaNhacungcap;
				
			item.MotaThem = MotaThem;
				
			item.NguoiGiao = NguoiGiao;
				
			item.NguoiNhan = NguoiNhan;
				
			item.NguoiHuy = NguoiHuy;
				
			item.IdNhanvien = IdNhanvien;
				
			item.SttHthi = SttHthi;
				
			item.TrangThai = TrangThai;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.TongTien = TongTien;
				
			item.LoaiPhieu = LoaiPhieu;
				
			item.TenLoaiphieu = TenLoaiphieu;
				
			item.IdNhanvienXacnhan = IdNhanvienXacnhan;
				
			item.NguoiXacnhan = NguoiXacnhan;
				
			item.NgayXacnhan = NgayXacnhan;
				
			item.IdKhoalinh = IdKhoalinh;
				
			item.KieuThuocvattu = KieuThuocvattu;
				
			item.SoChungtuKemtheo = SoChungtuKemtheo;
				
			item.PhieuVay = PhieuVay;
				
			item.TkNo = TkNo;
				
			item.TkCo = TkCo;
				
			item.DuTru = DuTru;
				
			item.HoidongThanhly = HoidongThanhly;
				
			item.DiadiemThanhly = DiadiemThanhly;
				
			item.YkienDexuat = YkienDexuat;
				
			item.ThoigianThanhlyTu = ThoigianThanhlyTu;
				
			item.ThoigianThanhlyDen = ThoigianThanhlyDen;
				
			item.NoiTru = NoiTru;
				
			item.LastActionName = LastActionName;
				
			item.NgayLap = NgayLap;
				
			item.TrongThau = TrongThau;
				
	        item.Save(UserName);
	    }
    }
}
