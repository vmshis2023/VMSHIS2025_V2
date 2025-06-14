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
    /// Controller class for L_Stocks
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LStockController
    {
        // Preload our schema..
        LStock thisSchemaLoad = new LStock();
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
        public LStockCollection FetchAll()
        {
            LStockCollection coll = new LStockCollection();
            Query qry = new Query(LStock.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LStockCollection FetchByID(object StockId)
        {
            LStockCollection coll = new LStockCollection().Where("Stock_ID", StockId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LStockCollection FetchByQuery(Query qry)
        {
            LStockCollection coll = new LStockCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object StockId)
        {
            return (LStock.Delete(StockId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object StockId)
        {
            return (LStock.Destroy(StockId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string StockCode,string StockName,byte StockNature,byte StockType,short DepartmentId,string SDesc,byte? StockCategory,int? IntOrder)
	    {
		    LStock item = new LStock();
		    
            item.StockCode = StockCode;
            
            item.StockName = StockName;
            
            item.StockNature = StockNature;
            
            item.StockType = StockType;
            
            item.DepartmentId = DepartmentId;
            
            item.SDesc = SDesc;
            
            item.StockCategory = StockCategory;
            
            item.IntOrder = IntOrder;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short StockId,string StockCode,string StockName,byte StockNature,byte StockType,short DepartmentId,string SDesc,byte? StockCategory,int? IntOrder)
	    {
		    LStock item = new LStock();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.StockId = StockId;
				
			item.StockCode = StockCode;
				
			item.StockName = StockName;
				
			item.StockNature = StockNature;
				
			item.StockType = StockType;
				
			item.DepartmentId = DepartmentId;
				
			item.SDesc = SDesc;
				
			item.StockCategory = StockCategory;
				
			item.IntOrder = IntOrder;
				
	        item.Save(UserName);
	    }
    }
}
