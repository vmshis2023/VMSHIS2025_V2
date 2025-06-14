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
namespace VMS.QMS.DAL
{
    /// <summary>
    /// Controller class for QMS_THOIGIAN_CHO
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QmsThoigianChoController
    {
        // Preload our schema..
        QmsThoigianCho thisSchemaLoad = new QmsThoigianCho();
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
        public QmsThoigianChoCollection FetchAll()
        {
            QmsThoigianChoCollection coll = new QmsThoigianChoCollection();
            Query qry = new Query(QmsThoigianCho.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QmsThoigianChoCollection FetchByID(object MaLoai)
        {
            QmsThoigianChoCollection coll = new QmsThoigianChoCollection().Where("ma_loai", MaLoai).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QmsThoigianChoCollection FetchByQuery(Query qry)
        {
            QmsThoigianChoCollection coll = new QmsThoigianChoCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object MaLoai)
        {
            return (QmsThoigianCho.Delete(MaLoai) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object MaLoai)
        {
            return (QmsThoigianCho.Destroy(MaLoai) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaLoai,string TenLoai,int Thoigiancho,string Ghichu,byte? Trangthai,int? SobanTiepdon)
	    {
		    QmsThoigianCho item = new QmsThoigianCho();
		    
            item.MaLoai = MaLoai;
            
            item.TenLoai = TenLoai;
            
            item.Thoigiancho = Thoigiancho;
            
            item.Ghichu = Ghichu;
            
            item.Trangthai = Trangthai;
            
            item.SobanTiepdon = SobanTiepdon;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string MaLoai,string TenLoai,int Thoigiancho,string Ghichu,byte? Trangthai,int? SobanTiepdon)
	    {
		    QmsThoigianCho item = new QmsThoigianCho();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.MaLoai = MaLoai;
				
			item.TenLoai = TenLoai;
				
			item.Thoigiancho = Thoigiancho;
				
			item.Ghichu = Ghichu;
				
			item.Trangthai = Trangthai;
				
			item.SobanTiepdon = SobanTiepdon;
				
	        item.Save(UserName);
	    }
    }
}
