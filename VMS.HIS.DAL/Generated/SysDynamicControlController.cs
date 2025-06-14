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
    /// Controller class for sys_dynamic_controls
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysDynamicControlController
    {
        // Preload our schema..
        SysDynamicControl thisSchemaLoad = new SysDynamicControl();
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
        public SysDynamicControlCollection FetchAll()
        {
            SysDynamicControlCollection coll = new SysDynamicControlCollection();
            Query qry = new Query(SysDynamicControl.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysDynamicControlCollection FetchByID(object Id)
        {
            SysDynamicControlCollection coll = new SysDynamicControlCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysDynamicControlCollection FetchByQuery(Query qry)
        {
            SysDynamicControlCollection coll = new SysDynamicControlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysDynamicControl.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysDynamicControl.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Ma,string Mota,short? Stt,int? MultiReportId,byte? Rtxt,byte? TopLabel,byte? Multiline,int? X,int? Y,int? W,int? H,int? LblW,byte? AllowEmpty,byte? Bold,string DataSource,string DisplayMember,string ValueMember,byte? ControlType,string DefaultOption,string GroupId,string LoaiDanhmuc,string Parent,string Filter)
	    {
		    SysDynamicControl item = new SysDynamicControl();
		    
            item.Ma = Ma;
            
            item.Mota = Mota;
            
            item.Stt = Stt;
            
            item.MultiReportId = MultiReportId;
            
            item.Rtxt = Rtxt;
            
            item.TopLabel = TopLabel;
            
            item.Multiline = Multiline;
            
            item.X = X;
            
            item.Y = Y;
            
            item.W = W;
            
            item.H = H;
            
            item.LblW = LblW;
            
            item.AllowEmpty = AllowEmpty;
            
            item.Bold = Bold;
            
            item.DataSource = DataSource;
            
            item.DisplayMember = DisplayMember;
            
            item.ValueMember = ValueMember;
            
            item.ControlType = ControlType;
            
            item.DefaultOption = DefaultOption;
            
            item.GroupId = GroupId;
            
            item.LoaiDanhmuc = LoaiDanhmuc;
            
            item.Parent = Parent;
            
            item.Filter = Filter;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Ma,string Mota,short? Stt,int? MultiReportId,byte? Rtxt,byte? TopLabel,byte? Multiline,int? X,int? Y,int? W,int? H,int? LblW,byte? AllowEmpty,byte? Bold,string DataSource,string DisplayMember,string ValueMember,byte? ControlType,string DefaultOption,string GroupId,string LoaiDanhmuc,string Parent,string Filter)
	    {
		    SysDynamicControl item = new SysDynamicControl();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Ma = Ma;
				
			item.Mota = Mota;
				
			item.Stt = Stt;
				
			item.MultiReportId = MultiReportId;
				
			item.Rtxt = Rtxt;
				
			item.TopLabel = TopLabel;
				
			item.Multiline = Multiline;
				
			item.X = X;
				
			item.Y = Y;
				
			item.W = W;
				
			item.H = H;
				
			item.LblW = LblW;
				
			item.AllowEmpty = AllowEmpty;
				
			item.Bold = Bold;
				
			item.DataSource = DataSource;
				
			item.DisplayMember = DisplayMember;
				
			item.ValueMember = ValueMember;
				
			item.ControlType = ControlType;
				
			item.DefaultOption = DefaultOption;
				
			item.GroupId = GroupId;
				
			item.LoaiDanhmuc = LoaiDanhmuc;
				
			item.Parent = Parent;
				
			item.Filter = Filter;
				
	        item.Save(UserName);
	    }
    }
}
