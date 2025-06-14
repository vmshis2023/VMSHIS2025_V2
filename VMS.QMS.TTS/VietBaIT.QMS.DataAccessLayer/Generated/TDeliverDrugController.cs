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
    /// Controller class for T_DeliverDrug
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TDeliverDrugController
    {
        // Preload our schema..
        TDeliverDrug thisSchemaLoad = new TDeliverDrug();
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
        public TDeliverDrugCollection FetchAll()
        {
            TDeliverDrugCollection coll = new TDeliverDrugCollection();
            Query qry = new Query(TDeliverDrug.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDeliverDrugCollection FetchByID(object Id)
        {
            TDeliverDrugCollection coll = new TDeliverDrugCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDeliverDrugCollection FetchByQuery(Query qry)
        {
            TDeliverDrugCollection coll = new TDeliverDrugCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TDeliverDrug.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TDeliverDrug.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long Id,DateTime InputDate,short InputUserId,short DeliverUserId,int? PresId,string SDesc)
	    {
		    TDeliverDrug item = new TDeliverDrug();
		    
            item.Id = Id;
            
            item.InputDate = InputDate;
            
            item.InputUserId = InputUserId;
            
            item.DeliverUserId = DeliverUserId;
            
            item.PresId = PresId;
            
            item.SDesc = SDesc;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,DateTime InputDate,short InputUserId,short DeliverUserId,int? PresId,string SDesc)
	    {
		    TDeliverDrug item = new TDeliverDrug();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.InputDate = InputDate;
				
			item.InputUserId = InputUserId;
				
			item.DeliverUserId = DeliverUserId;
				
			item.PresId = PresId;
				
			item.SDesc = SDesc;
				
	        item.Save(UserName);
	    }
    }
}
