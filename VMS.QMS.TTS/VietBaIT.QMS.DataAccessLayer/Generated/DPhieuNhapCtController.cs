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
    /// Controller class for D_PHIEU_NHAP_CT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DPhieuNhapCtController
    {
        // Preload our schema..
        DPhieuNhapCt thisSchemaLoad = new DPhieuNhapCt();
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
        public DPhieuNhapCtCollection FetchAll()
        {
            DPhieuNhapCtCollection coll = new DPhieuNhapCtCollection();
            Query qry = new Query(DPhieuNhapCt.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhieuNhapCtCollection FetchByID(object IdPhieuNhapCt)
        {
            DPhieuNhapCtCollection coll = new DPhieuNhapCtCollection().Where("ID_PHIEU_NHAP_CT", IdPhieuNhapCt).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhieuNhapCtCollection FetchByQuery(Query qry)
        {
            DPhieuNhapCtCollection coll = new DPhieuNhapCtCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieuNhapCt)
        {
            return (DPhieuNhapCt.Delete(IdPhieuNhapCt) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieuNhapCt)
        {
            return (DPhieuNhapCt.Destroy(IdPhieuNhapCt) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdPhieuNhap,DateTime NgayHetHan,int IdThuoc,decimal DonGia,decimal GiaBan,short ThangDu,int SoLuong,string SoLo,int? ChietKhau,decimal? ThanhTien,string MaPhieuXuat,int? IdPhieuXuat,string GhiChu,decimal? Vat)
	    {
		    DPhieuNhapCt item = new DPhieuNhapCt();
		    
            item.IdPhieuNhap = IdPhieuNhap;
            
            item.NgayHetHan = NgayHetHan;
            
            item.IdThuoc = IdThuoc;
            
            item.DonGia = DonGia;
            
            item.GiaBan = GiaBan;
            
            item.ThangDu = ThangDu;
            
            item.SoLuong = SoLuong;
            
            item.SoLo = SoLo;
            
            item.ChietKhau = ChietKhau;
            
            item.ThanhTien = ThanhTien;
            
            item.MaPhieuXuat = MaPhieuXuat;
            
            item.IdPhieuXuat = IdPhieuXuat;
            
            item.GhiChu = GhiChu;
            
            item.Vat = Vat;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdPhieuNhapCt,int IdPhieuNhap,DateTime NgayHetHan,int IdThuoc,decimal DonGia,decimal GiaBan,short ThangDu,int SoLuong,string SoLo,int? ChietKhau,decimal? ThanhTien,string MaPhieuXuat,int? IdPhieuXuat,string GhiChu,decimal? Vat)
	    {
		    DPhieuNhapCt item = new DPhieuNhapCt();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieuNhapCt = IdPhieuNhapCt;
				
			item.IdPhieuNhap = IdPhieuNhap;
				
			item.NgayHetHan = NgayHetHan;
				
			item.IdThuoc = IdThuoc;
				
			item.DonGia = DonGia;
				
			item.GiaBan = GiaBan;
				
			item.ThangDu = ThangDu;
				
			item.SoLuong = SoLuong;
				
			item.SoLo = SoLo;
				
			item.ChietKhau = ChietKhau;
				
			item.ThanhTien = ThanhTien;
				
			item.MaPhieuXuat = MaPhieuXuat;
				
			item.IdPhieuXuat = IdPhieuXuat;
				
			item.GhiChu = GhiChu;
				
			item.Vat = Vat;
				
	        item.Save(UserName);
	    }
    }
}
