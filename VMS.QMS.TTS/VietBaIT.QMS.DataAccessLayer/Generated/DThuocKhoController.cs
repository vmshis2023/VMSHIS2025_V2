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
    /// Controller class for D_THUOC_KHO
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DThuocKhoController
    {
        // Preload our schema..
        DThuocKho thisSchemaLoad = new DThuocKho();
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
        public DThuocKhoCollection FetchAll()
        {
            DThuocKhoCollection coll = new DThuocKhoCollection();
            Query qry = new Query(DThuocKho.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DThuocKhoCollection FetchByID(object IdKho)
        {
            DThuocKhoCollection coll = new DThuocKhoCollection().Where("ID_KHO", IdKho).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DThuocKhoCollection FetchByQuery(Query qry)
        {
            DThuocKhoCollection coll = new DThuocKhoCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdKho)
        {
            return (DThuocKho.Delete(IdKho) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdKho)
        {
            return (DThuocKho.Destroy(IdKho) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int IdKho,int IdThuoc,DateTime NgayHetHan,decimal DonGia,decimal GiaBan,decimal Vat)
        {
            Query qry = new Query(DThuocKho.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdKho", IdKho).AND("IdThuoc", IdThuoc).AND("NgayHetHan", NgayHetHan).AND("DonGia", DonGia).AND("GiaBan", GiaBan).AND("Vat", Vat);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdKho,int IdThuoc,DateTime NgayHetHan,decimal DonGia,decimal GiaBan,int SoLuong,decimal Vat)
	    {
		    DThuocKho item = new DThuocKho();
		    
            item.IdKho = IdKho;
            
            item.IdThuoc = IdThuoc;
            
            item.NgayHetHan = NgayHetHan;
            
            item.DonGia = DonGia;
            
            item.GiaBan = GiaBan;
            
            item.SoLuong = SoLuong;
            
            item.Vat = Vat;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdKho,int IdThuoc,DateTime NgayHetHan,decimal DonGia,decimal GiaBan,int SoLuong,decimal Vat,int Id)
	    {
		    DThuocKho item = new DThuocKho();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdKho = IdKho;
				
			item.IdThuoc = IdThuoc;
				
			item.NgayHetHan = NgayHetHan;
				
			item.DonGia = DonGia;
				
			item.GiaBan = GiaBan;
				
			item.SoLuong = SoLuong;
				
			item.Vat = Vat;
				
			item.Id = Id;
				
	        item.Save(UserName);
	    }
    }
}
