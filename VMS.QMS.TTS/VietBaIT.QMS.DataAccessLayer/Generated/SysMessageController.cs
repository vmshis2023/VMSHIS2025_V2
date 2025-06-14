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
    /// Controller class for Sys_Messages
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysMessageController
    {
        // Preload our schema..
        SysMessage thisSchemaLoad = new SysMessage();
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
        public SysMessageCollection FetchAll()
        {
            SysMessageCollection coll = new SysMessageCollection();
            Query qry = new Query(SysMessage.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMessageCollection FetchByID(object MsgCode)
        {
            SysMessageCollection coll = new SysMessageCollection().Where("Msg_Code", MsgCode).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMessageCollection FetchByQuery(Query qry)
        {
            SysMessageCollection coll = new SysMessageCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object MsgCode)
        {
            return (SysMessage.Delete(MsgCode) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object MsgCode)
        {
            return (SysMessage.Destroy(MsgCode) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MsgCode,string VnMsgContent,string EnMsgContent,string VnMsgTitle,string EnMsgTitle,string MsgType,string ActionType,short SubsystemId,string SDesc)
	    {
		    SysMessage item = new SysMessage();
		    
            item.MsgCode = MsgCode;
            
            item.VnMsgContent = VnMsgContent;
            
            item.EnMsgContent = EnMsgContent;
            
            item.VnMsgTitle = VnMsgTitle;
            
            item.EnMsgTitle = EnMsgTitle;
            
            item.MsgType = MsgType;
            
            item.ActionType = ActionType;
            
            item.SubsystemId = SubsystemId;
            
            item.SDesc = SDesc;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string MsgCode,string VnMsgContent,string EnMsgContent,string VnMsgTitle,string EnMsgTitle,string MsgType,string ActionType,short SubsystemId,string SDesc)
	    {
		    SysMessage item = new SysMessage();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.MsgCode = MsgCode;
				
			item.VnMsgContent = VnMsgContent;
				
			item.EnMsgContent = EnMsgContent;
				
			item.VnMsgTitle = VnMsgTitle;
				
			item.EnMsgTitle = EnMsgTitle;
				
			item.MsgType = MsgType;
				
			item.ActionType = ActionType;
				
			item.SubsystemId = SubsystemId;
				
			item.SDesc = SDesc;
				
	        item.Save(UserName);
	    }
    }
}
