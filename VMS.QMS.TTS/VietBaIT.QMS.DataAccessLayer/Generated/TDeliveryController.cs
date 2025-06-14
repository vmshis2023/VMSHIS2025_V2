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
    /// Controller class for T_Delivery
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TDeliveryController
    {
        // Preload our schema..
        TDelivery thisSchemaLoad = new TDelivery();
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
        public TDeliveryCollection FetchAll()
        {
            TDeliveryCollection coll = new TDeliveryCollection();
            Query qry = new Query(TDelivery.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDeliveryCollection FetchByID(object Id)
        {
            TDeliveryCollection coll = new TDeliveryCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDeliveryCollection FetchByQuery(Query qry)
        {
            TDeliveryCollection coll = new TDeliveryCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TDelivery.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TDelivery.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime InputDate,string InputUserId,int DeliverUserId,int DepartmentId,string DepartmentName,int StaffId,string StaffName,int Status,string SDesc,int? ConfirmStatus,DateTime? CreatedDate,string CreatedBy)
	    {
		    TDelivery item = new TDelivery();
		    
            item.InputDate = InputDate;
            
            item.InputUserId = InputUserId;
            
            item.DeliverUserId = DeliverUserId;
            
            item.DepartmentId = DepartmentId;
            
            item.DepartmentName = DepartmentName;
            
            item.StaffId = StaffId;
            
            item.StaffName = StaffName;
            
            item.Status = Status;
            
            item.SDesc = SDesc;
            
            item.ConfirmStatus = ConfirmStatus;
            
            item.CreatedDate = CreatedDate;
            
            item.CreatedBy = CreatedBy;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,DateTime InputDate,string InputUserId,int DeliverUserId,int DepartmentId,string DepartmentName,int StaffId,string StaffName,int Status,string SDesc,int? ConfirmStatus,DateTime? CreatedDate,string CreatedBy)
	    {
		    TDelivery item = new TDelivery();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.InputDate = InputDate;
				
			item.InputUserId = InputUserId;
				
			item.DeliverUserId = DeliverUserId;
				
			item.DepartmentId = DepartmentId;
				
			item.DepartmentName = DepartmentName;
				
			item.StaffId = StaffId;
				
			item.StaffName = StaffName;
				
			item.Status = Status;
				
			item.SDesc = SDesc;
				
			item.ConfirmStatus = ConfirmStatus;
				
			item.CreatedDate = CreatedDate;
				
			item.CreatedBy = CreatedBy;
				
	        item.Save(UserName);
	    }
    }
}
