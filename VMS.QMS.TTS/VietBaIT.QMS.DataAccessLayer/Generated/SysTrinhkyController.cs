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
    /// Controller class for Sys_Trinhky
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysTrinhkyController
    {
        // Preload our schema..
        SysTrinhky thisSchemaLoad = new SysTrinhky();
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
        public SysTrinhkyCollection FetchAll()
        {
            SysTrinhkyCollection coll = new SysTrinhkyCollection();
            Query qry = new Query(SysTrinhky.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysTrinhkyCollection FetchByID(object ReportName)
        {
            SysTrinhkyCollection coll = new SysTrinhkyCollection().Where("ReportName", ReportName).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysTrinhkyCollection FetchByQuery(Query qry)
        {
            SysTrinhkyCollection coll = new SysTrinhkyCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ReportName)
        {
            return (SysTrinhky.Delete(ReportName) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ReportName)
        {
            return (SysTrinhky.Destroy(ReportName) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string ReportName,string ObjectName)
        {
            Query qry = new Query(SysTrinhky.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("ReportName", ReportName).AND("ObjectName", ObjectName);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string ReportName,string ObjectName,string FontName,int? FontSize,string FontStype,string ObjectContent,string TitleReport)
	    {
		    SysTrinhky item = new SysTrinhky();
		    
            item.ReportName = ReportName;
            
            item.ObjectName = ObjectName;
            
            item.FontName = FontName;
            
            item.FontSize = FontSize;
            
            item.FontStype = FontStype;
            
            item.ObjectContent = ObjectContent;
            
            item.TitleReport = TitleReport;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string ReportName,string ObjectName,string FontName,int? FontSize,string FontStype,string ObjectContent,string TitleReport)
	    {
		    SysTrinhky item = new SysTrinhky();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ReportName = ReportName;
				
			item.ObjectName = ObjectName;
				
			item.FontName = FontName;
				
			item.FontSize = FontSize;
				
			item.FontStype = FontStype;
				
			item.ObjectContent = ObjectContent;
				
			item.TitleReport = TitleReport;
				
	        item.Save(UserName);
	    }
    }
}
