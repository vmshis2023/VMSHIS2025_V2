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
    /// Controller class for DynamicFields
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DynamicFieldController
    {
        // Preload our schema..
        DynamicField thisSchemaLoad = new DynamicField();
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
        public DynamicFieldCollection FetchAll()
        {
            DynamicFieldCollection coll = new DynamicFieldCollection();
            Query qry = new Query(DynamicField.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DynamicFieldCollection FetchByID(object Id)
        {
            DynamicFieldCollection coll = new DynamicFieldCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DynamicFieldCollection FetchByQuery(Query qry)
        {
            DynamicFieldCollection coll = new DynamicFieldCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (DynamicField.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (DynamicField.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Ma,string Mota,short? Stt,int? IdVungks,byte? Rtxt,byte? TopLabel,byte? Multiline,int? W,int? H,int? LblW,byte? AllowEmpty,byte? Bold)
	    {
		    DynamicField item = new DynamicField();
		    
            item.Ma = Ma;
            
            item.Mota = Mota;
            
            item.Stt = Stt;
            
            item.IdVungks = IdVungks;
            
            item.Rtxt = Rtxt;
            
            item.TopLabel = TopLabel;
            
            item.Multiline = Multiline;
            
            item.W = W;
            
            item.H = H;
            
            item.LblW = LblW;
            
            item.AllowEmpty = AllowEmpty;
            
            item.Bold = Bold;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Ma,string Mota,short? Stt,int? IdVungks,byte? Rtxt,byte? TopLabel,byte? Multiline,int? W,int? H,int? LblW,byte? AllowEmpty,byte? Bold)
	    {
		    DynamicField item = new DynamicField();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Ma = Ma;
				
			item.Mota = Mota;
				
			item.Stt = Stt;
				
			item.IdVungks = IdVungks;
				
			item.Rtxt = Rtxt;
				
			item.TopLabel = TopLabel;
				
			item.Multiline = Multiline;
				
			item.W = W;
				
			item.H = H;
				
			item.LblW = LblW;
				
			item.AllowEmpty = AllowEmpty;
				
			item.Bold = Bold;
				
	        item.Save(UserName);
	    }
    }
}
