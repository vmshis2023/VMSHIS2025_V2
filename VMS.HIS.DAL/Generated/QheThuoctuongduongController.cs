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
namespace VMS.HIS.DAL
{
    /// <summary>
    /// Controller class for qhe_thuoctuongduong
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QheThuoctuongduongController
    {
        // Preload our schema..
        QheThuoctuongduong thisSchemaLoad = new QheThuoctuongduong();
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
        public QheThuoctuongduongCollection FetchAll()
        {
            QheThuoctuongduongCollection coll = new QheThuoctuongduongCollection();
            Query qry = new Query(QheThuoctuongduong.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheThuoctuongduongCollection FetchByID(object IdThuoc)
        {
            QheThuoctuongduongCollection coll = new QheThuoctuongduongCollection().Where("id_thuoc", IdThuoc).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheThuoctuongduongCollection FetchByQuery(Query qry)
        {
            QheThuoctuongduongCollection coll = new QheThuoctuongduongCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThuoc)
        {
            return (QheThuoctuongduong.Delete(IdThuoc) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThuoc)
        {
            return (QheThuoctuongduong.Destroy(IdThuoc) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int IdThuoc,int IdThuoctuongduong)
        {
            Query qry = new Query(QheThuoctuongduong.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdThuoc", IdThuoc).AND("IdThuoctuongduong", IdThuoctuongduong);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdThuoc,int IdThuoctuongduong)
	    {
		    QheThuoctuongduong item = new QheThuoctuongduong();
		    
            item.IdThuoc = IdThuoc;
            
            item.IdThuoctuongduong = IdThuoctuongduong;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdThuoc,int IdThuoctuongduong)
	    {
		    QheThuoctuongduong item = new QheThuoctuongduong();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThuoc = IdThuoc;
				
			item.IdThuoctuongduong = IdThuoctuongduong;
				
	        item.Save(UserName);
	    }
    }
}
