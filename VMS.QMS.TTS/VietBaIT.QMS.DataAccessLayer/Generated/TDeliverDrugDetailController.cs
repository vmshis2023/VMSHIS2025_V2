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
    /// Controller class for T_DeliverDrug_Detail
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TDeliverDrugDetailController
    {
        // Preload our schema..
        TDeliverDrugDetail thisSchemaLoad = new TDeliverDrugDetail();
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
        public TDeliverDrugDetailCollection FetchAll()
        {
            TDeliverDrugDetailCollection coll = new TDeliverDrugDetailCollection();
            Query qry = new Query(TDeliverDrugDetail.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDeliverDrugDetailCollection FetchByID(object DetailId)
        {
            TDeliverDrugDetailCollection coll = new TDeliverDrugDetailCollection().Where("Detail_ID", DetailId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDeliverDrugDetailCollection FetchByQuery(Query qry)
        {
            TDeliverDrugDetailCollection coll = new TDeliverDrugDetailCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object DetailId)
        {
            return (TDeliverDrugDetail.Delete(DetailId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object DetailId)
        {
            return (TDeliverDrugDetail.Destroy(DetailId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long Id,int DrugId,int Quantity,byte HasDelivered,short StockId)
	    {
		    TDeliverDrugDetail item = new TDeliverDrugDetail();
		    
            item.Id = Id;
            
            item.DrugId = DrugId;
            
            item.Quantity = Quantity;
            
            item.HasDelivered = HasDelivered;
            
            item.StockId = StockId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long DetailId,long Id,int DrugId,int Quantity,byte HasDelivered,short StockId)
	    {
		    TDeliverDrugDetail item = new TDeliverDrugDetail();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.DetailId = DetailId;
				
			item.Id = Id;
				
			item.DrugId = DrugId;
				
			item.Quantity = Quantity;
				
			item.HasDelivered = HasDelivered;
				
			item.StockId = StockId;
				
	        item.Save(UserName);
	    }
    }
}
