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
    /// Controller class for t_thau_chitiet
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TThauChitietController
    {
        // Preload our schema..
        TThauChitiet thisSchemaLoad = new TThauChitiet();
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
        public TThauChitietCollection FetchAll()
        {
            TThauChitietCollection coll = new TThauChitietCollection();
            Query qry = new Query(TThauChitiet.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThauChitietCollection FetchByID(object IdThauCt)
        {
            TThauChitietCollection coll = new TThauChitietCollection().Where("id_thau_ct", IdThauCt).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThauChitietCollection FetchByQuery(Query qry)
        {
            TThauChitietCollection coll = new TThauChitietCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThauCt)
        {
            return (TThauChitiet.Delete(IdThauCt) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThauCt)
        {
            return (TThauChitiet.Destroy(IdThauCt) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long IdThau,int IdThuoc,string MaDvt,decimal SoLuong,decimal DonGia,int? SlNhap,int? SlDieutietDi,int? SlDieutietDen,string NhomTckt,string NguonGoc,string DangSoche,string Tccl,string NhomThau,string SoDangky,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,byte? TrangThai)
	    {
		    TThauChitiet item = new TThauChitiet();
		    
            item.IdThau = IdThau;
            
            item.IdThuoc = IdThuoc;
            
            item.MaDvt = MaDvt;
            
            item.SoLuong = SoLuong;
            
            item.DonGia = DonGia;
            
            item.SlNhap = SlNhap;
            
            item.SlDieutietDi = SlDieutietDi;
            
            item.SlDieutietDen = SlDieutietDen;
            
            item.NhomTckt = NhomTckt;
            
            item.NguonGoc = NguonGoc;
            
            item.DangSoche = DangSoche;
            
            item.Tccl = Tccl;
            
            item.NhomThau = NhomThau;
            
            item.SoDangky = SoDangky;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.TrangThai = TrangThai;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdThauCt,long IdThau,int IdThuoc,string MaDvt,decimal SoLuong,decimal DonGia,int? SlNhap,int? SlDieutietDi,int? SlDieutietDen,string NhomTckt,string NguonGoc,string DangSoche,string Tccl,string NhomThau,string SoDangky,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,byte? TrangThai)
	    {
		    TThauChitiet item = new TThauChitiet();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThauCt = IdThauCt;
				
			item.IdThau = IdThau;
				
			item.IdThuoc = IdThuoc;
				
			item.MaDvt = MaDvt;
				
			item.SoLuong = SoLuong;
				
			item.DonGia = DonGia;
				
			item.SlNhap = SlNhap;
				
			item.SlDieutietDi = SlDieutietDi;
				
			item.SlDieutietDen = SlDieutietDen;
				
			item.NhomTckt = NhomTckt;
				
			item.NguonGoc = NguonGoc;
				
			item.DangSoche = DangSoche;
				
			item.Tccl = Tccl;
				
			item.NhomThau = NhomThau;
				
			item.SoDangky = SoDangky;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.TrangThai = TrangThai;
				
	        item.Save(UserName);
	    }
    }
}
