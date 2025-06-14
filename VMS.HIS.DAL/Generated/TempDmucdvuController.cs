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
    /// Controller class for temp_dmucdvu
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TempDmucdvuController
    {
        // Preload our schema..
        TempDmucdvu thisSchemaLoad = new TempDmucdvu();
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
        public TempDmucdvuCollection FetchAll()
        {
            TempDmucdvuCollection coll = new TempDmucdvuCollection();
            Query qry = new Query(TempDmucdvu.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TempDmucdvuCollection FetchByID(object Id)
        {
            TempDmucdvuCollection coll = new TempDmucdvuCollection().Where("id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TempDmucdvuCollection FetchByQuery(Query qry)
        {
            TempDmucdvuCollection coll = new TempDmucdvuCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TempDmucdvu.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TempDmucdvu.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaDvu,string TenDvu,string Dvt,string LoaiDvu,string NhomDvu,string DonGia)
	    {
		    TempDmucdvu item = new TempDmucdvu();
		    
            item.MaDvu = MaDvu;
            
            item.TenDvu = TenDvu;
            
            item.Dvt = Dvt;
            
            item.LoaiDvu = LoaiDvu;
            
            item.NhomDvu = NhomDvu;
            
            item.DonGia = DonGia;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,string MaDvu,string TenDvu,string Dvt,string LoaiDvu,string NhomDvu,string DonGia)
	    {
		    TempDmucdvu item = new TempDmucdvu();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.MaDvu = MaDvu;
				
			item.TenDvu = TenDvu;
				
			item.Dvt = Dvt;
				
			item.LoaiDvu = LoaiDvu;
				
			item.NhomDvu = NhomDvu;
				
			item.DonGia = DonGia;
				
	        item.Save(UserName);
	    }
    }
}
