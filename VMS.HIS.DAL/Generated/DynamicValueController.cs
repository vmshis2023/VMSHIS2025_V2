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
    /// Controller class for DynamicValues
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DynamicValueController
    {
        // Preload our schema..
        DynamicValue thisSchemaLoad = new DynamicValue();
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
        public DynamicValueCollection FetchAll()
        {
            DynamicValueCollection coll = new DynamicValueCollection();
            Query qry = new Query(DynamicValue.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DynamicValueCollection FetchByID(object Id)
        {
            DynamicValueCollection coll = new DynamicValueCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DynamicValueCollection FetchByQuery(Query qry)
        {
            DynamicValueCollection coll = new DynamicValueCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (DynamicValue.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (DynamicValue.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Ma,string Giatri,long? IdChidinhchitiet)
	    {
		    DynamicValue item = new DynamicValue();
		    
            item.Ma = Ma;
            
            item.Giatri = Giatri;
            
            item.IdChidinhchitiet = IdChidinhchitiet;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,string Ma,string Giatri,long? IdChidinhchitiet)
	    {
		    DynamicValue item = new DynamicValue();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Ma = Ma;
				
			item.Giatri = Giatri;
				
			item.IdChidinhchitiet = IdChidinhchitiet;
				
	        item.Save(UserName);
	    }
    }
}
