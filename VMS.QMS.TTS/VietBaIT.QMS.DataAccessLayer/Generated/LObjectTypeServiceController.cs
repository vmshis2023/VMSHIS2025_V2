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
    /// Controller class for L_ObjectType_Service
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LObjectTypeServiceController
    {
        // Preload our schema..
        LObjectTypeService thisSchemaLoad = new LObjectTypeService();
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
        public LObjectTypeServiceCollection FetchAll()
        {
            LObjectTypeServiceCollection coll = new LObjectTypeServiceCollection();
            Query qry = new Query(LObjectTypeService.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LObjectTypeServiceCollection FetchByID(object ObjectTypeServiceId)
        {
            LObjectTypeServiceCollection coll = new LObjectTypeServiceCollection().Where("ObjectType_Service_ID", ObjectTypeServiceId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LObjectTypeServiceCollection FetchByQuery(Query qry)
        {
            LObjectTypeServiceCollection coll = new LObjectTypeServiceCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ObjectTypeServiceId)
        {
            return (LObjectTypeService.Delete(ObjectTypeServiceId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ObjectTypeServiceId)
        {
            return (LObjectTypeService.Destroy(ObjectTypeServiceId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(short ObjectTypeId,short ServiceId,int? ServiceDetailId,decimal? DiscountRate,byte DiscountType,string SDesc,decimal? LastPrice,decimal? Surcharge,int? ObjectTypeType,decimal? PhuThuTraiTuyen,string MaDtuong,DateTime? NgayTao,string NguoiTao,DateTime? NgaySua,string NguoiSua,string MaKhoaThien,decimal? GiaCu)
	    {
		    LObjectTypeService item = new LObjectTypeService();
		    
            item.ObjectTypeId = ObjectTypeId;
            
            item.ServiceId = ServiceId;
            
            item.ServiceDetailId = ServiceDetailId;
            
            item.DiscountRate = DiscountRate;
            
            item.DiscountType = DiscountType;
            
            item.SDesc = SDesc;
            
            item.LastPrice = LastPrice;
            
            item.Surcharge = Surcharge;
            
            item.ObjectTypeType = ObjectTypeType;
            
            item.PhuThuTraiTuyen = PhuThuTraiTuyen;
            
            item.MaDtuong = MaDtuong;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
            item.NgaySua = NgaySua;
            
            item.NguoiSua = NguoiSua;
            
            item.MaKhoaThien = MaKhoaThien;
            
            item.GiaCu = GiaCu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short ObjectTypeServiceId,short ObjectTypeId,short ServiceId,int? ServiceDetailId,decimal? DiscountRate,byte DiscountType,string SDesc,decimal? LastPrice,decimal? Surcharge,int? ObjectTypeType,decimal? PhuThuTraiTuyen,string MaDtuong,DateTime? NgayTao,string NguoiTao,DateTime? NgaySua,string NguoiSua,string MaKhoaThien,decimal? GiaCu)
	    {
		    LObjectTypeService item = new LObjectTypeService();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ObjectTypeServiceId = ObjectTypeServiceId;
				
			item.ObjectTypeId = ObjectTypeId;
				
			item.ServiceId = ServiceId;
				
			item.ServiceDetailId = ServiceDetailId;
				
			item.DiscountRate = DiscountRate;
				
			item.DiscountType = DiscountType;
				
			item.SDesc = SDesc;
				
			item.LastPrice = LastPrice;
				
			item.Surcharge = Surcharge;
				
			item.ObjectTypeType = ObjectTypeType;
				
			item.PhuThuTraiTuyen = PhuThuTraiTuyen;
				
			item.MaDtuong = MaDtuong;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
			item.NgaySua = NgaySua;
				
			item.NguoiSua = NguoiSua;
				
			item.MaKhoaThien = MaKhoaThien;
				
			item.GiaCu = GiaCu;
				
	        item.Save(UserName);
	    }
    }
}
