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
    /// Controller class for L_Message
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LMessageController
    {
        // Preload our schema..
        LMessage thisSchemaLoad = new LMessage();
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
        public LMessageCollection FetchAll()
        {
            LMessageCollection coll = new LMessageCollection();
            Query qry = new Query(LMessage.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LMessageCollection FetchByID(object MessageId)
        {
            LMessageCollection coll = new LMessageCollection().Where("Message_ID", MessageId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LMessageCollection FetchByQuery(Query qry)
        {
            LMessageCollection coll = new LMessageCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object MessageId)
        {
            return (LMessage.Delete(MessageId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object MessageId)
        {
            return (LMessage.Destroy(MessageId) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string MessageId,string MessageType)
        {
            Query qry = new Query(LMessage.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("MessageId", MessageId).AND("MessageType", MessageType);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MessageId,string MessageType,string MessageName,int? MessageOrder)
	    {
		    LMessage item = new LMessage();
		    
            item.MessageId = MessageId;
            
            item.MessageType = MessageType;
            
            item.MessageName = MessageName;
            
            item.MessageOrder = MessageOrder;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string MessageId,string MessageType,string MessageName,int? MessageOrder)
	    {
		    LMessage item = new LMessage();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.MessageId = MessageId;
				
			item.MessageType = MessageType;
				
			item.MessageName = MessageName;
				
			item.MessageOrder = MessageOrder;
				
	        item.Save(UserName);
	    }
    }
}
