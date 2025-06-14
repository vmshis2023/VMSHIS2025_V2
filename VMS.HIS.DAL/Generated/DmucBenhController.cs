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
    /// Controller class for dmuc_benh
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DmucBenhController
    {
        // Preload our schema..
        DmucBenh thisSchemaLoad = new DmucBenh();
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
        public DmucBenhCollection FetchAll()
        {
            DmucBenhCollection coll = new DmucBenhCollection();
            Query qry = new Query(DmucBenh.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucBenhCollection FetchByID(object IdBenh)
        {
            DmucBenhCollection coll = new DmucBenhCollection().Where("id_benh", IdBenh).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucBenhCollection FetchByQuery(Query qry)
        {
            DmucBenhCollection coll = new DmucBenhCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdBenh)
        {
            return (DmucBenh.Delete(IdBenh) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdBenh)
        {
            return (DmucBenh.Destroy(IdBenh) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaBenh,string MaLoaibenh,string TenBenh,string MotaThem,string Viettat,string TenQuocte)
	    {
		    DmucBenh item = new DmucBenh();
		    
            item.MaBenh = MaBenh;
            
            item.MaLoaibenh = MaLoaibenh;
            
            item.TenBenh = TenBenh;
            
            item.MotaThem = MotaThem;
            
            item.Viettat = Viettat;
            
            item.TenQuocte = TenQuocte;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short IdBenh,string MaBenh,string MaLoaibenh,string TenBenh,string MotaThem,string Viettat,string TenQuocte)
	    {
		    DmucBenh item = new DmucBenh();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdBenh = IdBenh;
				
			item.MaBenh = MaBenh;
				
			item.MaLoaibenh = MaLoaibenh;
				
			item.TenBenh = TenBenh;
				
			item.MotaThem = MotaThem;
				
			item.Viettat = Viettat;
				
			item.TenQuocte = TenQuocte;
				
	        item.Save(UserName);
	    }
    }
}
