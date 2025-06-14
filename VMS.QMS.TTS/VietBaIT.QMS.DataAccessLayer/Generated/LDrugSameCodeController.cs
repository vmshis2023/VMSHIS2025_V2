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
    /// Controller class for L_Drug_SameCode
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LDrugSameCodeController
    {
        // Preload our schema..
        LDrugSameCode thisSchemaLoad = new LDrugSameCode();
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
        public LDrugSameCodeCollection FetchAll()
        {
            LDrugSameCodeCollection coll = new LDrugSameCodeCollection();
            Query qry = new Query(LDrugSameCode.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LDrugSameCodeCollection FetchByID(object DrugId)
        {
            LDrugSameCodeCollection coll = new LDrugSameCodeCollection().Where("Drug_ID", DrugId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LDrugSameCodeCollection FetchByQuery(Query qry)
        {
            LDrugSameCodeCollection coll = new LDrugSameCodeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object DrugId)
        {
            return (LDrugSameCode.Delete(DrugId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object DrugId)
        {
            return (LDrugSameCode.Destroy(DrugId) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int DrugId,int SameDrugId)
        {
            Query qry = new Query(LDrugSameCode.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("DrugId", DrugId).AND("SameDrugId", SameDrugId);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int DrugId,int SameDrugId)
	    {
		    LDrugSameCode item = new LDrugSameCode();
		    
            item.DrugId = DrugId;
            
            item.SameDrugId = SameDrugId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int DrugId,int SameDrugId)
	    {
		    LDrugSameCode item = new LDrugSameCode();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.DrugId = DrugId;
				
			item.SameDrugId = SameDrugId;
				
	        item.Save(UserName);
	    }
    }
}
