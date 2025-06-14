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
    /// Controller class for dmuc_loaithuoc
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DmucLoaithuocController
    {
        // Preload our schema..
        DmucLoaithuoc thisSchemaLoad = new DmucLoaithuoc();
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
        public DmucLoaithuocCollection FetchAll()
        {
            DmucLoaithuocCollection coll = new DmucLoaithuocCollection();
            Query qry = new Query(DmucLoaithuoc.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucLoaithuocCollection FetchByID(object IdLoaithuoc)
        {
            DmucLoaithuocCollection coll = new DmucLoaithuocCollection().Where("id_loaithuoc", IdLoaithuoc).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucLoaithuocCollection FetchByQuery(Query qry)
        {
            DmucLoaithuocCollection coll = new DmucLoaithuocCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdLoaithuoc)
        {
            return (DmucLoaithuoc.Delete(IdLoaithuoc) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdLoaithuoc)
        {
            return (DmucLoaithuoc.Destroy(IdLoaithuoc) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaLoaithuoc,string TenLoaithuoc,short SttHthi,string MotaThem,string MaNhomthuoc,short? InRieng,string KieuThuocvattu)
	    {
		    DmucLoaithuoc item = new DmucLoaithuoc();
		    
            item.MaLoaithuoc = MaLoaithuoc;
            
            item.TenLoaithuoc = TenLoaithuoc;
            
            item.SttHthi = SttHthi;
            
            item.MotaThem = MotaThem;
            
            item.MaNhomthuoc = MaNhomthuoc;
            
            item.InRieng = InRieng;
            
            item.KieuThuocvattu = KieuThuocvattu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short IdLoaithuoc,string MaLoaithuoc,string TenLoaithuoc,short SttHthi,string MotaThem,string MaNhomthuoc,short? InRieng,string KieuThuocvattu)
	    {
		    DmucLoaithuoc item = new DmucLoaithuoc();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdLoaithuoc = IdLoaithuoc;
				
			item.MaLoaithuoc = MaLoaithuoc;
				
			item.TenLoaithuoc = TenLoaithuoc;
				
			item.SttHthi = SttHthi;
				
			item.MotaThem = MotaThem;
				
			item.MaNhomthuoc = MaNhomthuoc;
				
			item.InRieng = InRieng;
				
			item.KieuThuocvattu = KieuThuocvattu;
				
	        item.Save(UserName);
	    }
    }
}
