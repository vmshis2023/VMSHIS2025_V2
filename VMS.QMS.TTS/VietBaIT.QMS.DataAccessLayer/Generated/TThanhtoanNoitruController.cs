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
    /// Controller class for T_THANHTOAN_NOITRU
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TThanhtoanNoitruController
    {
        // Preload our schema..
        TThanhtoanNoitru thisSchemaLoad = new TThanhtoanNoitru();
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
        public TThanhtoanNoitruCollection FetchAll()
        {
            TThanhtoanNoitruCollection coll = new TThanhtoanNoitruCollection();
            Query qry = new Query(TThanhtoanNoitru.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThanhtoanNoitruCollection FetchByID(object PaymentId)
        {
            TThanhtoanNoitruCollection coll = new TThanhtoanNoitruCollection().Where("Payment_ID", PaymentId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThanhtoanNoitruCollection FetchByQuery(Query qry)
        {
            TThanhtoanNoitruCollection coll = new TThanhtoanNoitruCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PaymentId)
        {
            return (TThanhtoanNoitru.Delete(PaymentId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PaymentId)
        {
            return (TThanhtoanNoitru.Destroy(PaymentId) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int PaymentId,string PatientCode,int PatientId,int KieuPhieu)
        {
            Query qry = new Query(TThanhtoanNoitru.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("PaymentId", PaymentId).AND("PatientCode", PatientCode).AND("PatientId", PatientId).AND("KieuPhieu", KieuPhieu);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int PaymentId,string PatientCode,int PatientId,DateTime? PaymentDate,int IdBenhly,string MaDtuong,int IdGoiDvu,decimal SoTienGoi,decimal? TongDongia,decimal? TongPhuthu,decimal? TongBhct,decimal? TongBnct,decimal? TongTamung,decimal? TongBnPtra,decimal? SoTienTraLai,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,int KieuPhieu,int? DaTraLaitien,decimal? TongTutuc,string LyDoCkhau,DateTime? NgayCkhau,decimal? SoTienCkhau,decimal? TongBhctNgoaiGoi,decimal? TongBhctTrongGoi,decimal? TongBnctPsinhNgoaiGoi,decimal? TongBnctTrongGoi)
	    {
		    TThanhtoanNoitru item = new TThanhtoanNoitru();
		    
            item.PaymentId = PaymentId;
            
            item.PatientCode = PatientCode;
            
            item.PatientId = PatientId;
            
            item.PaymentDate = PaymentDate;
            
            item.IdBenhly = IdBenhly;
            
            item.MaDtuong = MaDtuong;
            
            item.IdGoiDvu = IdGoiDvu;
            
            item.SoTienGoi = SoTienGoi;
            
            item.TongDongia = TongDongia;
            
            item.TongPhuthu = TongPhuthu;
            
            item.TongBhct = TongBhct;
            
            item.TongBnct = TongBnct;
            
            item.TongTamung = TongTamung;
            
            item.TongBnPtra = TongBnPtra;
            
            item.SoTienTraLai = SoTienTraLai;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.KieuPhieu = KieuPhieu;
            
            item.DaTraLaitien = DaTraLaitien;
            
            item.TongTutuc = TongTutuc;
            
            item.LyDoCkhau = LyDoCkhau;
            
            item.NgayCkhau = NgayCkhau;
            
            item.SoTienCkhau = SoTienCkhau;
            
            item.TongBhctNgoaiGoi = TongBhctNgoaiGoi;
            
            item.TongBhctTrongGoi = TongBhctTrongGoi;
            
            item.TongBnctPsinhNgoaiGoi = TongBnctPsinhNgoaiGoi;
            
            item.TongBnctTrongGoi = TongBnctTrongGoi;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int PaymentId,string PatientCode,int PatientId,DateTime? PaymentDate,int IdBenhly,string MaDtuong,int IdGoiDvu,decimal SoTienGoi,decimal? TongDongia,decimal? TongPhuthu,decimal? TongBhct,decimal? TongBnct,decimal? TongTamung,decimal? TongBnPtra,decimal? SoTienTraLai,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,int KieuPhieu,int? DaTraLaitien,decimal? TongTutuc,string LyDoCkhau,DateTime? NgayCkhau,decimal? SoTienCkhau,decimal? TongBhctNgoaiGoi,decimal? TongBhctTrongGoi,decimal? TongBnctPsinhNgoaiGoi,decimal? TongBnctTrongGoi)
	    {
		    TThanhtoanNoitru item = new TThanhtoanNoitru();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PaymentId = PaymentId;
				
			item.PatientCode = PatientCode;
				
			item.PatientId = PatientId;
				
			item.PaymentDate = PaymentDate;
				
			item.IdBenhly = IdBenhly;
				
			item.MaDtuong = MaDtuong;
				
			item.IdGoiDvu = IdGoiDvu;
				
			item.SoTienGoi = SoTienGoi;
				
			item.TongDongia = TongDongia;
				
			item.TongPhuthu = TongPhuthu;
				
			item.TongBhct = TongBhct;
				
			item.TongBnct = TongBnct;
				
			item.TongTamung = TongTamung;
				
			item.TongBnPtra = TongBnPtra;
				
			item.SoTienTraLai = SoTienTraLai;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.KieuPhieu = KieuPhieu;
				
			item.DaTraLaitien = DaTraLaitien;
				
			item.TongTutuc = TongTutuc;
				
			item.LyDoCkhau = LyDoCkhau;
				
			item.NgayCkhau = NgayCkhau;
				
			item.SoTienCkhau = SoTienCkhau;
				
			item.TongBhctNgoaiGoi = TongBhctNgoaiGoi;
				
			item.TongBhctTrongGoi = TongBhctTrongGoi;
				
			item.TongBnctPsinhNgoaiGoi = TongBnctPsinhNgoaiGoi;
				
			item.TongBnctTrongGoi = TongBnctTrongGoi;
				
	        item.Save(UserName);
	    }
    }
}
