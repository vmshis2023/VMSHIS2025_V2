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
    /// Controller class for L_Military_Unit
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LMilitaryUnitController
    {
        // Preload our schema..
        LMilitaryUnit thisSchemaLoad = new LMilitaryUnit();
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
        public LMilitaryUnitCollection FetchAll()
        {
            LMilitaryUnitCollection coll = new LMilitaryUnitCollection();
            Query qry = new Query(LMilitaryUnit.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LMilitaryUnitCollection FetchByID(object UnitId)
        {
            LMilitaryUnitCollection coll = new LMilitaryUnitCollection().Where("Unit_ID", UnitId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LMilitaryUnitCollection FetchByQuery(Query qry)
        {
            LMilitaryUnitCollection coll = new LMilitaryUnitCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object UnitId)
        {
            return (LMilitaryUnit.Delete(UnitId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object UnitId)
        {
            return (LMilitaryUnit.Destroy(UnitId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string UnitCode,string UnitName,int? ParentId,int? IntOrder,string SDesc)
	    {
		    LMilitaryUnit item = new LMilitaryUnit();
		    
            item.UnitCode = UnitCode;
            
            item.UnitName = UnitName;
            
            item.ParentId = ParentId;
            
            item.IntOrder = IntOrder;
            
            item.SDesc = SDesc;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int UnitId,string UnitCode,string UnitName,int? ParentId,int? IntOrder,string SDesc)
	    {
		    LMilitaryUnit item = new LMilitaryUnit();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.UnitId = UnitId;
				
			item.UnitCode = UnitCode;
				
			item.UnitName = UnitName;
				
			item.ParentId = ParentId;
				
			item.IntOrder = IntOrder;
				
			item.SDesc = SDesc;
				
	        item.Save(UserName);
	    }
    }
}
