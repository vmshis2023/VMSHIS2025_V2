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
    /// Controller class for t_thuockho
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TThuockhoController
    {
        // Preload our schema..
        TThuockho thisSchemaLoad = new TThuockho();
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
        public TThuockhoCollection FetchAll()
        {
            TThuockhoCollection coll = new TThuockhoCollection();
            Query qry = new Query(TThuockho.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThuockhoCollection FetchByID(object IdThuockho)
        {
            TThuockhoCollection coll = new TThuockhoCollection().Where("id_thuockho", IdThuockho).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThuockhoCollection FetchByQuery(Query qry)
        {
            TThuockhoCollection coll = new TThuockhoCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThuockho)
        {
            return (TThuockho.Delete(IdThuockho) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThuockho)
        {
            return (TThuockho.Destroy(IdThuockho) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdKho,int IdThuoc,DateTime NgayHethan,decimal GiaNhap,decimal GiaBan,decimal SoLuong,decimal Vat,string SoLo,string MaNhacungcap,int? SttBan,long? IdChuyen,DateTime? NgayNhap,decimal? GiaBhyt,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,byte? ChophepKetutruc,byte? ChophepKedon,string KieuThuocvattu,string SoDky,string SoQdinhthau,long? IdPhieu,long? IdQdinh)
	    {
		    TThuockho item = new TThuockho();
		    
            item.IdKho = IdKho;
            
            item.IdThuoc = IdThuoc;
            
            item.NgayHethan = NgayHethan;
            
            item.GiaNhap = GiaNhap;
            
            item.GiaBan = GiaBan;
            
            item.SoLuong = SoLuong;
            
            item.Vat = Vat;
            
            item.SoLo = SoLo;
            
            item.MaNhacungcap = MaNhacungcap;
            
            item.SttBan = SttBan;
            
            item.IdChuyen = IdChuyen;
            
            item.NgayNhap = NgayNhap;
            
            item.GiaBhyt = GiaBhyt;
            
            item.PhuthuDungtuyen = PhuthuDungtuyen;
            
            item.PhuthuTraituyen = PhuthuTraituyen;
            
            item.ChophepKetutruc = ChophepKetutruc;
            
            item.ChophepKedon = ChophepKedon;
            
            item.KieuThuocvattu = KieuThuocvattu;
            
            item.SoDky = SoDky;
            
            item.SoQdinhthau = SoQdinhthau;
            
            item.IdPhieu = IdPhieu;
            
            item.IdQdinh = IdQdinh;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdThuockho,int IdKho,int IdThuoc,DateTime NgayHethan,decimal GiaNhap,decimal GiaBan,decimal SoLuong,decimal Vat,string SoLo,string MaNhacungcap,int? SttBan,long? IdChuyen,DateTime? NgayNhap,decimal? GiaBhyt,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,byte? ChophepKetutruc,byte? ChophepKedon,string KieuThuocvattu,string SoDky,string SoQdinhthau,long? IdPhieu,long? IdQdinh)
	    {
		    TThuockho item = new TThuockho();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThuockho = IdThuockho;
				
			item.IdKho = IdKho;
				
			item.IdThuoc = IdThuoc;
				
			item.NgayHethan = NgayHethan;
				
			item.GiaNhap = GiaNhap;
				
			item.GiaBan = GiaBan;
				
			item.SoLuong = SoLuong;
				
			item.Vat = Vat;
				
			item.SoLo = SoLo;
				
			item.MaNhacungcap = MaNhacungcap;
				
			item.SttBan = SttBan;
				
			item.IdChuyen = IdChuyen;
				
			item.NgayNhap = NgayNhap;
				
			item.GiaBhyt = GiaBhyt;
				
			item.PhuthuDungtuyen = PhuthuDungtuyen;
				
			item.PhuthuTraituyen = PhuthuTraituyen;
				
			item.ChophepKetutruc = ChophepKetutruc;
				
			item.ChophepKedon = ChophepKedon;
				
			item.KieuThuocvattu = KieuThuocvattu;
				
			item.SoDky = SoDky;
				
			item.SoQdinhthau = SoQdinhthau;
				
			item.IdPhieu = IdPhieu;
				
			item.IdQdinh = IdQdinh;
				
	        item.Save(UserName);
	    }
    }
}
