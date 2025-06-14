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
    /// Controller class for L_Diseases
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LDiseaseController
    {
        // Preload our schema..
        LDisease thisSchemaLoad = new LDisease();
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
        public LDiseaseCollection FetchAll()
        {
            LDiseaseCollection coll = new LDiseaseCollection();
            Query qry = new Query(LDisease.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LDiseaseCollection FetchByID(object DiseaseId)
        {
            LDiseaseCollection coll = new LDiseaseCollection().Where("Disease_ID", DiseaseId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LDiseaseCollection FetchByQuery(Query qry)
        {
            LDiseaseCollection coll = new LDiseaseCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object DiseaseId)
        {
            return (LDisease.Delete(DiseaseId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object DiseaseId)
        {
            return (LDisease.Destroy(DiseaseId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string DiseaseCode,short DiseaseTypeId,string DiseaseName,string SDesc,string FirstChar)
	    {
		    LDisease item = new LDisease();
		    
            item.DiseaseCode = DiseaseCode;
            
            item.DiseaseTypeId = DiseaseTypeId;
            
            item.DiseaseName = DiseaseName;
            
            item.SDesc = SDesc;
            
            item.FirstChar = FirstChar;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short DiseaseId,string DiseaseCode,short DiseaseTypeId,string DiseaseName,string SDesc,string FirstChar)
	    {
		    LDisease item = new LDisease();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.DiseaseId = DiseaseId;
				
			item.DiseaseCode = DiseaseCode;
				
			item.DiseaseTypeId = DiseaseTypeId;
				
			item.DiseaseName = DiseaseName;
				
			item.SDesc = SDesc;
				
			item.FirstChar = FirstChar;
				
	        item.Save(UserName);
	    }
    }
}
