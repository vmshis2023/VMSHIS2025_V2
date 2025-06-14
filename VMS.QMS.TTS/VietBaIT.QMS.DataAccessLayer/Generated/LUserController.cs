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
    /// Controller class for L_User
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LUserController
    {
        // Preload our schema..
        LUser thisSchemaLoad = new LUser();
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
        public LUserCollection FetchAll()
        {
            LUserCollection coll = new LUserCollection();
            Query qry = new Query(LUser.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LUserCollection FetchByID(object UserId)
        {
            LUserCollection coll = new LUserCollection().Where("User_ID", UserId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LUserCollection FetchByQuery(Query qry)
        {
            LUserCollection coll = new LUserCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object UserId)
        {
            return (LUser.Delete(UserId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object UserId)
        {
            return (LUser.Destroy(UserId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(decimal UserId,string UserName,decimal RoleId)
	    {
		    LUser item = new LUser();
		    
            item.UserId = UserId;
            
            item.UserName = UserName;
            
            item.RoleId = RoleId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(decimal UserId,string UserName,decimal RoleId)
	    {
		    LUser item = new LUser();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.UserId = UserId;
				
			item.UserName = UserName;
				
			item.RoleId = RoleId;
				
	        item.Save(UserName);
	    }
    }
}
