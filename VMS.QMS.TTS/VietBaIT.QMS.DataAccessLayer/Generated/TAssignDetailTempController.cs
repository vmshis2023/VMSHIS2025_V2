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
    /// Controller class for T_Assign_Detail_Temp
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TAssignDetailTempController
    {
        // Preload our schema..
        TAssignDetailTemp thisSchemaLoad = new TAssignDetailTemp();
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
        public TAssignDetailTempCollection FetchAll()
        {
            TAssignDetailTempCollection coll = new TAssignDetailTempCollection();
            Query qry = new Query(TAssignDetailTemp.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TAssignDetailTempCollection FetchByID(object AssignDetailId)
        {
            TAssignDetailTempCollection coll = new TAssignDetailTempCollection().Where("AssignDetail_ID", AssignDetailId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TAssignDetailTempCollection FetchByQuery(Query qry)
        {
            TAssignDetailTempCollection coll = new TAssignDetailTempCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object AssignDetailId)
        {
            return (TAssignDetailTemp.Delete(AssignDetailId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object AssignDetailId)
        {
            return (TAssignDetailTemp.Destroy(AssignDetailId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long AssignDetailId,long? ExamId,long AssignId,short? ServiceId,int ServiceDetailId,short? DiagPerson,decimal? DiscountPrice,decimal? SurchargePrice,string UserId,byte? PaymentStatus,byte? IsPayment,int? Quantity,decimal? GiaBhytCt,decimal? GiaBnct,byte? ChoPhepIn,short? Stt,short? IdNoiThien,byte? DaGuiCls,string GhiChu,DateTime? InputDate)
	    {
		    TAssignDetailTemp item = new TAssignDetailTemp();
		    
            item.AssignDetailId = AssignDetailId;
            
            item.ExamId = ExamId;
            
            item.AssignId = AssignId;
            
            item.ServiceId = ServiceId;
            
            item.ServiceDetailId = ServiceDetailId;
            
            item.DiagPerson = DiagPerson;
            
            item.DiscountPrice = DiscountPrice;
            
            item.SurchargePrice = SurchargePrice;
            
            item.UserId = UserId;
            
            item.PaymentStatus = PaymentStatus;
            
            item.IsPayment = IsPayment;
            
            item.Quantity = Quantity;
            
            item.GiaBhytCt = GiaBhytCt;
            
            item.GiaBnct = GiaBnct;
            
            item.ChoPhepIn = ChoPhepIn;
            
            item.Stt = Stt;
            
            item.IdNoiThien = IdNoiThien;
            
            item.DaGuiCls = DaGuiCls;
            
            item.GhiChu = GhiChu;
            
            item.InputDate = InputDate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long AssignDetailId,long? ExamId,long AssignId,short? ServiceId,int ServiceDetailId,short? DiagPerson,decimal? DiscountPrice,decimal? SurchargePrice,string UserId,byte? PaymentStatus,byte? IsPayment,int? Quantity,decimal? GiaBhytCt,decimal? GiaBnct,byte? ChoPhepIn,short? Stt,short? IdNoiThien,byte? DaGuiCls,string GhiChu,DateTime? InputDate)
	    {
		    TAssignDetailTemp item = new TAssignDetailTemp();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.AssignDetailId = AssignDetailId;
				
			item.ExamId = ExamId;
				
			item.AssignId = AssignId;
				
			item.ServiceId = ServiceId;
				
			item.ServiceDetailId = ServiceDetailId;
				
			item.DiagPerson = DiagPerson;
				
			item.DiscountPrice = DiscountPrice;
				
			item.SurchargePrice = SurchargePrice;
				
			item.UserId = UserId;
				
			item.PaymentStatus = PaymentStatus;
				
			item.IsPayment = IsPayment;
				
			item.Quantity = Quantity;
				
			item.GiaBhytCt = GiaBhytCt;
				
			item.GiaBnct = GiaBnct;
				
			item.ChoPhepIn = ChoPhepIn;
				
			item.Stt = Stt;
				
			item.IdNoiThien = IdNoiThien;
				
			item.DaGuiCls = DaGuiCls;
				
			item.GhiChu = GhiChu;
				
			item.InputDate = InputDate;
				
	        item.Save(UserName);
	    }
    }
}
