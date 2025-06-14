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
    /// Controller class for kcb_phieukhamtoanthan
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbPhieukhamtoanthanController
    {
        // Preload our schema..
        KcbPhieukhamtoanthan thisSchemaLoad = new KcbPhieukhamtoanthan();
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
        public KcbPhieukhamtoanthanCollection FetchAll()
        {
            KcbPhieukhamtoanthanCollection coll = new KcbPhieukhamtoanthanCollection();
            Query qry = new Query(KcbPhieukhamtoanthan.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbPhieukhamtoanthanCollection FetchByID(object Id)
        {
            KcbPhieukhamtoanthanCollection coll = new KcbPhieukhamtoanthanCollection().Where("id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbPhieukhamtoanthanCollection FetchByQuery(Query qry)
        {
            KcbPhieukhamtoanthanCollection coll = new KcbPhieukhamtoanthanCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (KcbPhieukhamtoanthan.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (KcbPhieukhamtoanthan.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaLuotkham,long IdBenhnhan,DateTime NgayKham,string NhomMau,string NhietDo,string HuyetAp,string Mach,string NhịpTho,string ChieuCao,string CanNang,string MotaThem,string Bmi,string DangDi,string TinhThan,string HinhdangChung,string MausacDa,string TinhtrangDa,string HethongLongtoc,string NghiCobenh,string BophanDau,string BophanCo,string BophanNguc,string BophanBung,string BophanCotsong,string BophanChatthai,short? IdBacsi,string ToanThan,string BoPhan,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua)
	    {
		    KcbPhieukhamtoanthan item = new KcbPhieukhamtoanthan();
		    
            item.MaLuotkham = MaLuotkham;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.NgayKham = NgayKham;
            
            item.NhomMau = NhomMau;
            
            item.NhietDo = NhietDo;
            
            item.HuyetAp = HuyetAp;
            
            item.Mach = Mach;
            
            item.NhịpTho = NhịpTho;
            
            item.ChieuCao = ChieuCao;
            
            item.CanNang = CanNang;
            
            item.MotaThem = MotaThem;
            
            item.Bmi = Bmi;
            
            item.DangDi = DangDi;
            
            item.TinhThan = TinhThan;
            
            item.HinhdangChung = HinhdangChung;
            
            item.MausacDa = MausacDa;
            
            item.TinhtrangDa = TinhtrangDa;
            
            item.HethongLongtoc = HethongLongtoc;
            
            item.NghiCobenh = NghiCobenh;
            
            item.BophanDau = BophanDau;
            
            item.BophanCo = BophanCo;
            
            item.BophanNguc = BophanNguc;
            
            item.BophanBung = BophanBung;
            
            item.BophanCotsong = BophanCotsong;
            
            item.BophanChatthai = BophanChatthai;
            
            item.IdBacsi = IdBacsi;
            
            item.ToanThan = ToanThan;
            
            item.BoPhan = BoPhan;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,string MaLuotkham,long IdBenhnhan,DateTime NgayKham,string NhomMau,string NhietDo,string HuyetAp,string Mach,string NhịpTho,string ChieuCao,string CanNang,string MotaThem,string Bmi,string DangDi,string TinhThan,string HinhdangChung,string MausacDa,string TinhtrangDa,string HethongLongtoc,string NghiCobenh,string BophanDau,string BophanCo,string BophanNguc,string BophanBung,string BophanCotsong,string BophanChatthai,short? IdBacsi,string ToanThan,string BoPhan,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua)
	    {
		    KcbPhieukhamtoanthan item = new KcbPhieukhamtoanthan();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.MaLuotkham = MaLuotkham;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.NgayKham = NgayKham;
				
			item.NhomMau = NhomMau;
				
			item.NhietDo = NhietDo;
				
			item.HuyetAp = HuyetAp;
				
			item.Mach = Mach;
				
			item.NhịpTho = NhịpTho;
				
			item.ChieuCao = ChieuCao;
				
			item.CanNang = CanNang;
				
			item.MotaThem = MotaThem;
				
			item.Bmi = Bmi;
				
			item.DangDi = DangDi;
				
			item.TinhThan = TinhThan;
				
			item.HinhdangChung = HinhdangChung;
				
			item.MausacDa = MausacDa;
				
			item.TinhtrangDa = TinhtrangDa;
				
			item.HethongLongtoc = HethongLongtoc;
				
			item.NghiCobenh = NghiCobenh;
				
			item.BophanDau = BophanDau;
				
			item.BophanCo = BophanCo;
				
			item.BophanNguc = BophanNguc;
				
			item.BophanBung = BophanBung;
				
			item.BophanCotsong = BophanCotsong;
				
			item.BophanChatthai = BophanChatthai;
				
			item.IdBacsi = IdBacsi;
				
			item.ToanThan = ToanThan;
				
			item.BoPhan = BoPhan;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
	        item.Save(UserName);
	    }
    }
}
