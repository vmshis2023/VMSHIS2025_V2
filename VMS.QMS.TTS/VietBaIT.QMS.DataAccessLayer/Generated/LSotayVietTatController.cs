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
    /// Controller class for L_SOTAY_VIET_TAT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LSotayVietTatController
    {
        // Preload our schema..
        LSotayVietTat thisSchemaLoad = new LSotayVietTat();
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
        public LSotayVietTatCollection FetchAll()
        {
            LSotayVietTatCollection coll = new LSotayVietTatCollection();
            Query qry = new Query(LSotayVietTat.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LSotayVietTatCollection FetchByID(object Id)
        {
            LSotayVietTatCollection coll = new LSotayVietTatCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LSotayVietTatCollection FetchByQuery(Query qry)
        {
            LSotayVietTatCollection coll = new LSotayVietTatCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (LSotayVietTat.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (LSotayVietTat.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string TenVietTat,string TenViet,string NguoiTao,DateTime? NgayTao,string KeyCode,string IcdChinh,string IcdPhu)
	    {
		    LSotayVietTat item = new LSotayVietTat();
		    
            item.TenVietTat = TenVietTat;
            
            item.TenViet = TenViet;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.KeyCode = KeyCode;
            
            item.IcdChinh = IcdChinh;
            
            item.IcdPhu = IcdPhu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string TenVietTat,string TenViet,string NguoiTao,DateTime? NgayTao,string KeyCode,string IcdChinh,string IcdPhu)
	    {
		    LSotayVietTat item = new LSotayVietTat();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.TenVietTat = TenVietTat;
				
			item.TenViet = TenViet;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.KeyCode = KeyCode;
				
			item.IcdChinh = IcdChinh;
				
			item.IcdPhu = IcdPhu;
				
	        item.Save(UserName);
	    }
    }
}
