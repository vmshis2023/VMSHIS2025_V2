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
    /// Controller class for L_Sample_Assign_Detail
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LSampleAssignDetailController
    {
        // Preload our schema..
        LSampleAssignDetail thisSchemaLoad = new LSampleAssignDetail();
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
        public LSampleAssignDetailCollection FetchAll()
        {
            LSampleAssignDetailCollection coll = new LSampleAssignDetailCollection();
            Query qry = new Query(LSampleAssignDetail.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LSampleAssignDetailCollection FetchByID(object AssignDetailId)
        {
            LSampleAssignDetailCollection coll = new LSampleAssignDetailCollection().Where("AssignDetail_ID", AssignDetailId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LSampleAssignDetailCollection FetchByQuery(Query qry)
        {
            LSampleAssignDetailCollection coll = new LSampleAssignDetailCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object AssignDetailId)
        {
            return (LSampleAssignDetail.Delete(AssignDetailId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object AssignDetailId)
        {
            return (LSampleAssignDetail.Destroy(AssignDetailId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? AssignId,string AssignCode,string AssignName,short? ServiceId,int ServiceDetailId,decimal OriginPrice,decimal? DiscountPrice,decimal? SurchargePrice,string CreatedBy,DateTime? CreatedDate,DateTime? ModifyDate,string ModifyBy,string KeyCode)
	    {
		    LSampleAssignDetail item = new LSampleAssignDetail();
		    
            item.AssignId = AssignId;
            
            item.AssignCode = AssignCode;
            
            item.AssignName = AssignName;
            
            item.ServiceId = ServiceId;
            
            item.ServiceDetailId = ServiceDetailId;
            
            item.OriginPrice = OriginPrice;
            
            item.DiscountPrice = DiscountPrice;
            
            item.SurchargePrice = SurchargePrice;
            
            item.CreatedBy = CreatedBy;
            
            item.CreatedDate = CreatedDate;
            
            item.ModifyDate = ModifyDate;
            
            item.ModifyBy = ModifyBy;
            
            item.KeyCode = KeyCode;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long AssignDetailId,int? AssignId,string AssignCode,string AssignName,short? ServiceId,int ServiceDetailId,decimal OriginPrice,decimal? DiscountPrice,decimal? SurchargePrice,string CreatedBy,DateTime? CreatedDate,DateTime? ModifyDate,string ModifyBy,string KeyCode)
	    {
		    LSampleAssignDetail item = new LSampleAssignDetail();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.AssignDetailId = AssignDetailId;
				
			item.AssignId = AssignId;
				
			item.AssignCode = AssignCode;
				
			item.AssignName = AssignName;
				
			item.ServiceId = ServiceId;
				
			item.ServiceDetailId = ServiceDetailId;
				
			item.OriginPrice = OriginPrice;
				
			item.DiscountPrice = DiscountPrice;
				
			item.SurchargePrice = SurchargePrice;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedDate = CreatedDate;
				
			item.ModifyDate = ModifyDate;
				
			item.ModifyBy = ModifyBy;
				
			item.KeyCode = KeyCode;
				
	        item.Save(UserName);
	    }
    }
}
