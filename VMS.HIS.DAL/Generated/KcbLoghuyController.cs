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
    /// Controller class for kcb_loghuy
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbLoghuyController
    {
        // Preload our schema..
        KcbLoghuy thisSchemaLoad = new KcbLoghuy();
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
        public KcbLoghuyCollection FetchAll()
        {
            KcbLoghuyCollection coll = new KcbLoghuyCollection();
            Query qry = new Query(KcbLoghuy.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbLoghuyCollection FetchByID(object Id)
        {
            KcbLoghuyCollection coll = new KcbLoghuyCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbLoghuyCollection FetchByQuery(Query qry)
        {
            KcbLoghuyCollection coll = new KcbLoghuyCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (KcbLoghuy.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (KcbLoghuy.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(short IdNhanvien,long IdBenhnhan,string MaLuotkham,string TenBenhnhan,DateTime NgayHuy,decimal SotienHuy,byte LoaiphieuHuy,string LydoHuy,DateTime NgayTao,string NguoiTao)
	    {
		    KcbLoghuy item = new KcbLoghuy();
		    
            item.IdNhanvien = IdNhanvien;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.MaLuotkham = MaLuotkham;
            
            item.TenBenhnhan = TenBenhnhan;
            
            item.NgayHuy = NgayHuy;
            
            item.SotienHuy = SotienHuy;
            
            item.LoaiphieuHuy = LoaiphieuHuy;
            
            item.LydoHuy = LydoHuy;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,short IdNhanvien,long IdBenhnhan,string MaLuotkham,string TenBenhnhan,DateTime NgayHuy,decimal SotienHuy,byte LoaiphieuHuy,string LydoHuy,DateTime NgayTao,string NguoiTao)
	    {
		    KcbLoghuy item = new KcbLoghuy();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.IdNhanvien = IdNhanvien;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.MaLuotkham = MaLuotkham;
				
			item.TenBenhnhan = TenBenhnhan;
				
			item.NgayHuy = NgayHuy;
				
			item.SotienHuy = SotienHuy;
				
			item.LoaiphieuHuy = LoaiphieuHuy;
				
			item.LydoHuy = LydoHuy;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
	        item.Save(UserName);
	    }
    }
}
