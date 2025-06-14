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
    /// Controller class for qhe_bacsi_khoaphong
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QheBacsiKhoaphongController
    {
        // Preload our schema..
        QheBacsiKhoaphong thisSchemaLoad = new QheBacsiKhoaphong();
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
        public QheBacsiKhoaphongCollection FetchAll()
        {
            QheBacsiKhoaphongCollection coll = new QheBacsiKhoaphongCollection();
            Query qry = new Query(QheBacsiKhoaphong.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheBacsiKhoaphongCollection FetchByID(object IdBacsi)
        {
            QheBacsiKhoaphongCollection coll = new QheBacsiKhoaphongCollection().Where("id_bacsi", IdBacsi).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheBacsiKhoaphongCollection FetchByQuery(Query qry)
        {
            QheBacsiKhoaphongCollection coll = new QheBacsiKhoaphongCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdBacsi)
        {
            return (QheBacsiKhoaphong.Delete(IdBacsi) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdBacsi)
        {
            return (QheBacsiKhoaphong.Destroy(IdBacsi) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int IdBacsi,int IdKhoa,int IdPhong,byte Noitru)
        {
            Query qry = new Query(QheBacsiKhoaphong.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdBacsi", IdBacsi).AND("IdKhoa", IdKhoa).AND("IdPhong", IdPhong).AND("Noitru", Noitru);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdBacsi,int IdKhoa,int IdPhong,byte Noitru)
	    {
		    QheBacsiKhoaphong item = new QheBacsiKhoaphong();
		    
            item.IdBacsi = IdBacsi;
            
            item.IdKhoa = IdKhoa;
            
            item.IdPhong = IdPhong;
            
            item.Noitru = Noitru;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdBacsi,int IdKhoa,int IdPhong,byte Noitru)
	    {
		    QheBacsiKhoaphong item = new QheBacsiKhoaphong();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdBacsi = IdBacsi;
				
			item.IdKhoa = IdKhoa;
				
			item.IdPhong = IdPhong;
				
			item.Noitru = Noitru;
				
	        item.Save(UserName);
	    }
    }
}
