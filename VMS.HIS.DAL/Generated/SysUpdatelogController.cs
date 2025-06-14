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
    /// Controller class for sys_updatelog
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysUpdatelogController
    {
        // Preload our schema..
        SysUpdatelog thisSchemaLoad = new SysUpdatelog();
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
        public SysUpdatelogCollection FetchAll()
        {
            SysUpdatelogCollection coll = new SysUpdatelogCollection();
            Query qry = new Query(SysUpdatelog.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysUpdatelogCollection FetchByID(object Id)
        {
            SysUpdatelogCollection coll = new SysUpdatelogCollection().Where("id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysUpdatelogCollection FetchByQuery(Query qry)
        {
            SysUpdatelogCollection coll = new SysUpdatelogCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysUpdatelog.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysUpdatelog.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? VersionId,string Ipaddress,string Computername,string Filename,DateTime? Dateupdate,byte? Status)
	    {
		    SysUpdatelog item = new SysUpdatelog();
		    
            item.VersionId = VersionId;
            
            item.Ipaddress = Ipaddress;
            
            item.Computername = Computername;
            
            item.Filename = Filename;
            
            item.Dateupdate = Dateupdate;
            
            item.Status = Status;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,int? VersionId,string Ipaddress,string Computername,string Filename,DateTime? Dateupdate,byte? Status)
	    {
		    SysUpdatelog item = new SysUpdatelog();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.VersionId = VersionId;
				
			item.Ipaddress = Ipaddress;
				
			item.Computername = Computername;
				
			item.Filename = Filename;
				
			item.Dateupdate = Dateupdate;
				
			item.Status = Status;
				
	        item.Save(UserName);
	    }
    }
}
