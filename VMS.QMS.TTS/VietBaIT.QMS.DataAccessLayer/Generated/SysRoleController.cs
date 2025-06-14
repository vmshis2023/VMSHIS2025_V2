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
    /// Controller class for Sys_Roles
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysRoleController
    {
        // Preload our schema..
        SysRole thisSchemaLoad = new SysRole();
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
        public SysRoleCollection FetchAll()
        {
            SysRoleCollection coll = new SysRoleCollection();
            Query qry = new Query(SysRole.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysRoleCollection FetchByID(object IRole)
        {
            SysRoleCollection coll = new SysRoleCollection().Where("iRole", IRole).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysRoleCollection FetchByQuery(Query qry)
        {
            SysRoleCollection coll = new SysRoleCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IRole)
        {
            return (SysRole.Delete(IRole) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IRole)
        {
            return (SysRole.Destroy(IRole) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(long IRole,string FpSBranchID)
        {
            Query qry = new Query(SysRole.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IRole", IRole).AND("FpSBranchID", FpSBranchID);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string FpSBranchID,long IParentRole,string SRoleName,string SEngRoleName,int? IOrder,long? FkIFunctionID,DateTime? TDateCreated,string SImgPath,string SDesc,string SIconPath,int? IntShortCutKey,int? IsMid)
	    {
		    SysRole item = new SysRole();
		    
            item.FpSBranchID = FpSBranchID;
            
            item.IParentRole = IParentRole;
            
            item.SRoleName = SRoleName;
            
            item.SEngRoleName = SEngRoleName;
            
            item.IOrder = IOrder;
            
            item.FkIFunctionID = FkIFunctionID;
            
            item.TDateCreated = TDateCreated;
            
            item.SImgPath = SImgPath;
            
            item.SDesc = SDesc;
            
            item.SIconPath = SIconPath;
            
            item.IntShortCutKey = IntShortCutKey;
            
            item.IsMid = IsMid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IRole,string FpSBranchID,long IParentRole,string SRoleName,string SEngRoleName,int? IOrder,long? FkIFunctionID,DateTime? TDateCreated,string SImgPath,string SDesc,string SIconPath,int? IntShortCutKey,int? IsMid)
	    {
		    SysRole item = new SysRole();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IRole = IRole;
				
			item.FpSBranchID = FpSBranchID;
				
			item.IParentRole = IParentRole;
				
			item.SRoleName = SRoleName;
				
			item.SEngRoleName = SEngRoleName;
				
			item.IOrder = IOrder;
				
			item.FkIFunctionID = FkIFunctionID;
				
			item.TDateCreated = TDateCreated;
				
			item.SImgPath = SImgPath;
				
			item.SDesc = SDesc;
				
			item.SIconPath = SIconPath;
				
			item.IntShortCutKey = IntShortCutKey;
				
			item.IsMid = IsMid;
				
	        item.Save(UserName);
	    }
    }
}
