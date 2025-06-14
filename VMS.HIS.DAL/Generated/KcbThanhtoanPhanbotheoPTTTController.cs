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
    /// Controller class for kcb_thanhtoan_phanbotheoPTTT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbThanhtoanPhanbotheoPTTTController
    {
        // Preload our schema..
        KcbThanhtoanPhanbotheoPTTT thisSchemaLoad = new KcbThanhtoanPhanbotheoPTTT();
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
        public KcbThanhtoanPhanbotheoPTTTCollection FetchAll()
        {
            KcbThanhtoanPhanbotheoPTTTCollection coll = new KcbThanhtoanPhanbotheoPTTTCollection();
            Query qry = new Query(KcbThanhtoanPhanbotheoPTTT.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbThanhtoanPhanbotheoPTTTCollection FetchByID(object Id)
        {
            KcbThanhtoanPhanbotheoPTTTCollection coll = new KcbThanhtoanPhanbotheoPTTTCollection().Where("id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbThanhtoanPhanbotheoPTTTCollection FetchByQuery(Query qry)
        {
            KcbThanhtoanPhanbotheoPTTTCollection coll = new KcbThanhtoanPhanbotheoPTTTCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (KcbThanhtoanPhanbotheoPTTT.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (KcbThanhtoanPhanbotheoPTTT.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long? IdThanhtoan,string MaPttt,long IdBenhnhan,string MaLuotkham,byte NoiTru,decimal TongTien,decimal SoTien,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,string MaNganhang,long? IdPhieuthu,long? IdTamung,byte? TthaiHuy,byte? LoaiPhanbo)
	    {
		    KcbThanhtoanPhanbotheoPTTT item = new KcbThanhtoanPhanbotheoPTTT();
		    
            item.IdThanhtoan = IdThanhtoan;
            
            item.MaPttt = MaPttt;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.MaLuotkham = MaLuotkham;
            
            item.NoiTru = NoiTru;
            
            item.TongTien = TongTien;
            
            item.SoTien = SoTien;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.MaNganhang = MaNganhang;
            
            item.IdPhieuthu = IdPhieuthu;
            
            item.IdTamung = IdTamung;
            
            item.TthaiHuy = TthaiHuy;
            
            item.LoaiPhanbo = LoaiPhanbo;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long? IdThanhtoan,string MaPttt,long IdBenhnhan,string MaLuotkham,byte NoiTru,decimal TongTien,decimal SoTien,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,string MaNganhang,long Id,long? IdPhieuthu,long? IdTamung,byte? TthaiHuy,byte? LoaiPhanbo)
	    {
		    KcbThanhtoanPhanbotheoPTTT item = new KcbThanhtoanPhanbotheoPTTT();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThanhtoan = IdThanhtoan;
				
			item.MaPttt = MaPttt;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.MaLuotkham = MaLuotkham;
				
			item.NoiTru = NoiTru;
				
			item.TongTien = TongTien;
				
			item.SoTien = SoTien;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.MaNganhang = MaNganhang;
				
			item.Id = Id;
				
			item.IdPhieuthu = IdPhieuthu;
				
			item.IdTamung = IdTamung;
				
			item.TthaiHuy = TthaiHuy;
				
			item.LoaiPhanbo = LoaiPhanbo;
				
	        item.Save(UserName);
	    }
    }
}
