using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
namespace UpdateVersions
{
	public partial class Form1
	{
		public bool HasDownloadedV = true;
		public bool gv_bHasCheckedVer = false;
		public bool gv_bIsChecking = false;
		public DataSet gv_DSVersionFiles = new DataSet();
		public SqlConnection gv_oSqlCnn;
		public string gv_sAnnouncement = "Thông báo";
		public string gv_sConnString = "";
		public bool gv_bCallCheckUpdate = false;
			// Chứa tên hoặc ID của máy chủ CSDL
		public string gv_sComName = System.Environment.MachineName;
			// Tên CSDL
		public string gv_sDBName = "Assembly";
			// Chứa tên đăng nhập QTHT
		public string gv_sUID = "";
			// Chứa tên đăng nhập QTHT
		public string gv_sStaffName = "";
			// Chứa tên đăng nhập QTHT
		public int gv_StaffID = -1;
			// Chứa mật khẩu công khai của QTHT
		public string gv_sPWD = "";
		public bool gv_bLoginSuccess = false;
		public string gv_sSymmetricAlgorithmName = "Rijndael";
		private IAsyncResult _IArCheckingVersion;
		private delegate void StartUpCallBack();
		private void cmdGetFolder_Click(System.Object sender, System.EventArgs e)
		{
			FolderBrowserDialog oform = new FolderBrowserDialog();
			if (oform.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				txtFolder.Text = oform.SelectedPath;
			}
		}

		private void Update()
		{
			lblProgress.Visible = true;
			cmdUpdate.Enabled = false;
			lblStatus.Visible = true;
			cmdClose.Enabled = false;
			if (!Directory.Exists(txtFolder.Text)) {
				lblStatus.Text = "Không tồn tại thư mục chứa file. Đề nghị bạn chọn lại";
				pgr.Visible = false;
				lblProgress.Visible = false;
				return;
			}
			pgr.Visible = true;
			//ToolStripStatusLabel5.Visible = True
			pgr.Value = 0;
			HasDownloadedV = false;
			_IArCheckingVersion = BeginInvoke(new StartUpCallBack(CheckingVersion));
			while (!HasDownloadedV && !(pgr.Value + 50 >= pgr.Maximum)) {
				//BeginInvoke(New DelegateUpdatePrgb(AddressOf UpdatePrgbValue))
				Application.DoEvents();
				pgr.Value += 50;
				Application.DoEvents();
				Thread.Sleep(30);
			}
			this.Size = new Size(498, 195);

			lblProgress.Visible = false;
			cmdUpdate.Enabled = true;
			cmdClose.Enabled = true;
			//lblStatus.Visible = False
			//lblStatus.Text = ""
			pgr.Visible = false;
			HasDownloadedV = true;
			if (lblStatus.Text != "Các phiên bản bạn đang dùng là mới nhất")
				lblStatus.Text = "Đã cập nhật phiên bản thành công. Nhấn nút Thoát hoặc phím Esc để kết thúc!";
		}
		private void CheckingVersion()
		{
			if (!gv_bIsChecking) {
				EndInvoke(_IArCheckingVersion);
				gv_bCallCheckUpdate = true;
				Thread threCheckUpdate = null;
				threCheckUpdate = new Thread(CheckLastestVersion);
				threCheckUpdate.Start();
			}
		}
		public void CheckLastestVersion()
		{
			try {
				gv_bHasCheckedVer = false;
				Application.DoEvents();
				DownloadImgAndIcons();
				Application.DoEvents();
				if ((gv_DSVersionFiles != null)) {
					if (gv_DSVersionFiles.Tables.Count > 0) {
						gv_DSVersionFiles.Tables.Clear();
					}
				}
				if (gv_DSVersionFiles.Tables.Count == 0) {
					gv_DSVersionFiles = GetLastestVersion();
				} else {
					DataSet DS = new DataSet();
					DS = GetLastestVersion();
					foreach (DataRow dr in DS.Tables[0].Rows) {
						InsertNewRow(dr, DS.Tables[0], ref gv_DSVersionFiles);
					}
				}

				if (gv_DSVersionFiles.Tables[0].Rows.Count > 0) {
					foreach (DataRow dr in gv_DSVersionFiles.Tables[0].Rows) {
						if (dr["CHON"] == 1) {
							Application.DoEvents();
							lblStatus.Visible = true;
							lblStatus.Text = "Updating:" + sDBnull(dr["sFileName"]).ToString() + "...";
							Application.DoEvents();
							dr["sStatus"] = "Đang download file...";
							SaveOldDLL(dr["sFileName"]);
							byte[] objData = Convert.ToByte(dr["objData"]);
							MemoryStream ms = new MemoryStream(objData);
							FileStream fs = new FileStream(Application.StartupPath + "\\" + (dr["intRar"] == 0 ? dr["sFileName"] : dr["sRarFileName"]), FileMode.Create, FileAccess.Write);
							ms.WriteTo(fs);
							ms.Flush();
							fs.Close();
							try {
								if (dr["intRar"] == 1) {
									string pStartupPath = Application.StartupPath + "\\" + (dr["intRar"] == 0 ? dr["sFileName"] : dr["sRarFileName"]);
									ProcessStartInfo info = new ProcessStartInfo();
									lblStatus.Text = "Giải nén:" + sDBnull(dr["sFileName"]).ToString() + "...";
									info.FileName = Application.StartupPath + "\\WinRAR\\WinRAR.exe";
									info.Arguments = "e -pSYSMAN -o+ " + Strings.Chr(34) + pStartupPath + Strings.Chr(34) + " " + Strings.Chr(34) + Application.StartupPath + Strings.Chr(34);
									info.WindowStyle = ProcessWindowStyle.Hidden;
									Process pro = System.Diagnostics.Process.Start(info);
									pro.WaitForExit();
									//
									//DeleteFile(pStartupPath)
								}
								dr["sStatus"] = "Đã download thành công";
							} catch (Exception ex) {
							}
						}
						gv_DSVersionFiles.Tables[0].AcceptChanges();
					}
				} else {
					lblStatus.Visible = true;
					lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
				}
				HasDownloadedV = true;
				Cursor = Cursors.Default;
			} catch (Exception ex) {
				HasDownloadedV = true;
			}
		}
		public string sDBnull(object pv_obj, string Reval = "")
		{
			if (Information.IsDBNull(pv_obj) | (pv_obj == null)) {
				return Reval;
			} else {
				return pv_obj.ToString();
			}
		}
		private DataSet GetAllVersions()
		{
			SqlDataAdapter DA = new SqlDataAdapter();
			DataSet sv_ds = new DataSet();
			string sv_sSql = null;
			SqlConnection g = null;
			g = new SqlConnection(gv_sConnString);
			g.Open();
			try {
				sv_sSql = "SELECT 1 AS CHON,'' AS sStatus, PK_intID,sFileName,objData,sVersion,dblCapacity,intRar,sRarFileName FROM Sys_VERSION V WHERE PK_intID=(SELECT MAX(PK_intID) FROM Sys_VERSION WHERE sFileName=V.sFileName)";
				DA = new SqlDataAdapter(sv_sSql, g);
				DA.Fill(sv_ds, "Sys_version");
				g.Close();
				g = null;
				return sv_ds;
			} catch (Exception ex) {
				g.Close();
				g = null;
				return sv_ds;
			}
		}
		public DataSet GetLastestVersion()
		{
			DataSet sv_DSLastestV = new DataSet();
			DataSet sv_DSVersion = new DataSet();
			sv_DSVersion = GetAllVersions();
			try {
				sv_DSLastestV = sv_DSVersion.Clone();
				if (sv_DSVersion.Tables[0].Rows.Count > 0) {
					foreach (DataRow dr in sv_DSVersion.Tables[0].Rows) {
						if (!File.Exists(Application.StartupPath + "\\" + dr["sFileName"])) {
							InsertNewRow(dr, sv_DSVersion.Tables[0], ref sv_DSLastestV);
						//Nếu tồn tại thì kiểm tra xem Version có khác nhau không?
						} else {
							FileVersionInfo _FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\" + dr["sFileName"]);
							string sVersion = _FileVersionInfo.ProductVersion;
							if ((sVersion == null)) {
								//InsertNewRow(dr, sv_DSVersion.Tables(0), sv_DSLastestV)
							} else {
								if (!sVersion.Equals(dr["sVersion"])) {
									InsertNewRow(dr, sv_DSVersion.Tables[0], ref sv_DSLastestV);
								}
							}

						}
					}
					sv_DSLastestV.Tables[0].AcceptChanges();
				}
				return sv_DSLastestV;
			} catch (Exception ex) {
				return null;
			}
		}
		public void InsertNewRow(DataRow dr, DataTable pv_SourceTable, ref DataSet pv_DS)
		{
			try {
				DataRow DRLastestV = pv_DS.Tables[0].NewRow();
				foreach (DataColumn col in pv_SourceTable.Columns) {
					DRLastestV[col.ColumnName] = dr[col.ColumnName];
				}
				pv_DS.Tables[0].Rows.Add(DRLastestV);

			} catch (Exception ex) {
			}
		}


		public void SaveOldDLL(string pv_sFileName)
		{
			try {
				if (!Directory.Exists(Application.StartupPath + "\\OldVersion")) {
					Directory.CreateDirectory(Application.StartupPath + "\\OldVersion");
				}
				if (File.Exists(Application.StartupPath + "\\" + pv_sFileName)) {
					File.Copy(Application.StartupPath + "\\" + pv_sFileName, Application.StartupPath + "\\OldVersion\\" + pv_sFileName, true);
				}

			} catch (Exception ex) {
			}
		}
		public void DeleteFile(string pv_sFilePath)
		{
			try {
				if (File.Exists(pv_sFilePath)) {
					File.Delete(pv_sFilePath);
				}

			} catch (Exception ex) {
			}
		}
		private DataSet GetAllImgAndIcons()
		{
			SqlDataAdapter DA = new SqlDataAdapter();
			DataSet sv_ds = new DataSet();
			string sv_sSql = null;
			SqlConnection g = null;
			g = new SqlConnection(gv_sConnString);
			g.Open();
			try {
				sv_sSql = "SELECT sFileName,Data FROM Sys_IMGANDICON";
				DA = new SqlDataAdapter(sv_sSql, g);
				DA.Fill(sv_ds, "Sys_IMGANDICON");
				g.Close();
				g = null;
				return sv_ds;
			} catch (Exception ex) {
				g.Close();
				g = null;
				return sv_ds;
			}
		}
		public bool KhoiTaoKetNoi()
		{
			VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt("Rijndael");
			string fv_sUID = null;
			string fv_sPWD = null;
			try {
				if (bGetConfigInfor(ref fv_sUID, ref fv_sPWD)) {
					if (fv_sUID == null | fv_sPWD == null) {
						return false;
					}
					string sv_sConnectionString = "workstation id=" + gv_sComName + ";packet size=4096;data source=" + gv_sComName + ";persist security info=False;initial catalog=" + gv_sDBName + ";uid=" + sv_oEncrypt.GiaiMa(fv_sUID) + ";pwd=" + sv_oEncrypt.GiaiMa(fv_sPWD);
					gv_sConnString = sv_sConnectionString;
					if ((gv_oSqlCnn == null)) {
						gv_oSqlCnn = new SqlConnection(sv_sConnectionString);
						gv_oSqlCnn.Open();
						//GetBranchInfor(gv_sBranchID)
					} else if (gv_oSqlCnn.State == ConnectionState.Closed) {
						gv_oSqlCnn.Open();
					}
					return true;
				} else {
					return false;
				}
			} catch (Exception ex) {
				MessageBox.Show("Không kết nối được vào CSDL. Liên hệ với quản trị hệ thống ", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
		}
		//------------------------------------------------------------------------------------------------------------
		//Mục đích        : Đọc file cấu hình để lấy về mã đơn vị quản lý, tên CSDL, UserName, Password
		//Đầu vào          :
		//Đầu ra            :Thành công=True. Không thành công=False
		//Người tạo       :CuongDV
		//Ngày tạo         :09/03/2005
		//Nhật kí sửa đổi:
		//------------------------------------------------------------------------------------------------------------
		public bool bGetConfigInfor(ref string pv_sUID, ref string pv_sPWD)
		{
			DataSet fv_DS = new DataSet();
			string fv_sUID = null;
			string fv_sPWD = null;
			try {
				if (File.Exists(Application.StartupPath + "\\Config.XML")) {
					// Tiến hành đọc File cấu hình vào DataSet
					fv_DS.ReadXml(Application.StartupPath + "\\Config.XML");
					if (fv_DS.Tables[0].Rows.Count > 0) {
						// Đọc dữ liệu vào các biến toàn cục
						//Địa chỉ máy chủ CSDL
						gv_sComName = fv_DS.Tables[0].Rows[0]["SERVERADDRESS"];
						//Mã chi nhánh
						//gv_sBranchID = fv_DS.Tables(0).Rows(0)("BranchID")
						//UID côngkhai
						fv_sUID = fv_DS.Tables[0].Rows[0]["USERNAME"];
						//Mật khẩu công khai
						fv_sPWD = fv_DS.Tables[0].Rows[0]["PASSWORD"];
						//Tên Cơ sở dữ liệu
						gv_sDBName = fv_DS.Tables[0].Rows[0]["DATABASE_ID"];
						//Ngôn ngữ hiển thị
						//gv_sLanguageDisplay = fv_DS.Tables(0).Rows(0)("LANGUAGEDISPLAY")
						//gv_sSubSystemDisplay = fv_DS.Tables(0).Rows(0)("INTERFACEDISPLAY")

						//Tiến hành kết nối bằng tài khoản công khai vừa đọc trong file Config để lấy về tài khoản đăng nhập CSDL
						SqlConnection fv_oSQLCon = null;
						dynamic fv_sSqlConstr = "workstation id=" + gv_sComName + ";packet size=4096;data source=" + gv_sComName + ";persist security info=False;initial catalog=" + gv_sDBName + ";uid=" + fv_sUID + ";pwd=" + fv_sPWD;
						fv_oSQLCon = new SqlConnection(fv_sSqlConstr);
						//Mở CSDL
						try {
							fv_oSQLCon.Open();
							//Lấy tài khoản bí mật để đăng nhập CSDL
							GetSecretAccount(fv_oSQLCon, ref pv_sUID, ref pv_sPWD);
						} catch (Exception SQLex) {
							MessageBox.Show("Không đăng nhập được vào CSDL " + gv_sDBName + " bằng tài khoản công khai(UID=" + fv_sUID + ";PWD=" + fv_sPWD + "). Hãy cấu hình lại File Config.XML sau đó đăng nhập lại.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
							return false;
						}
					} else {
						MessageBox.Show("Không có dữ liệu trong File cấu hình! Bạn hãy xem lại", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
						return false;
					}
				} else {
					MessageBox.Show("Không tồn tại File cấu hình có tên: Config.XML!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return false;
				}
				return true;

			} catch (Exception ex) {
			}
		}
		private void GetSecretAccount(SqlConnection pv_Conn, ref string pv_sUID, ref string pv_sPWD)
		{
			DataSet sv_Ds = new DataSet();
			SqlDataAdapter sv_DA = null;
			try {
				sv_DA = new SqlDataAdapter("SELECT * FROM Sys_SECURITY", pv_Conn);
				sv_DA.Fill(sv_Ds, "Sys_SECURITY");
				if (sv_Ds.Tables[0].Rows.Count > 0) {
					pv_sUID = sv_Ds.Tables[0].Rows[0]["sUID"];
					pv_sPWD = sv_Ds.Tables[0].Rows[0]["sPWD"];
				} else {
					MessageBox.Show("Không tồn tại tài khoản đăng nhập trong bảng Sys_SECURITY! Đề nghị với DBAdmin tạo tài khoản đăng nhập trong bảng đó.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			} catch (Exception ex) {
				MessageBox.Show("Bạn cần gán lại quyền truy cập vào bảng Sys_SECURITY cho tài khoản công khai! Đề nghị với DBAdmin thực hiện công việc này bằng tiện ích CreateUser.exe", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void GetBranchInfor(string pv_sBranchID)
		{
			DataSet sv_Ds = new DataSet();
			SqlDataAdapter sv_DA = null;
			try {
				sv_DA = new SqlDataAdapter("SELECT * FROM Sys_ManagementUnit WHERE PK_sBranchID=N'" + pv_sBranchID + "'", gv_oSqlCnn);
				sv_DA.Fill(sv_Ds, "Sys_ManagementUnit");
				if (sv_Ds.Tables[0].Rows.Count > 0) {

				} else {
					return;
				}

			} catch (Exception ex) {
			}
		}
		public void DownloadImgAndIcons()
		{
			DataSet Ds = new DataSet();
			Ds = GetAllImgAndIcons();
			if (Directory.Exists("C:\\ImagesAndIcons")) {
			} else {
				Directory.CreateDirectory("C:\\ImagesAndIcons");
			}
			try {
				if (Ds.Tables.Count > 0) {
					if (Ds.Tables[0].Rows.Count > 0) {
						foreach (DataRow dr in Ds.Tables[0].Rows) {
							try {
								if (!File.Exists("C:\\ImagesAndIcons\\" + dr["sFileName"])) {
									byte[] objData = Convert.ToByte(dr["Data"]);
									MemoryStream ms = new MemoryStream(objData);
									FileStream fs = new FileStream("C:\\ImagesAndIcons\\" + dr["sFileName"], FileMode.Create, FileAccess.Write);
									ms.WriteTo(fs);
									ms.Flush();
									fs.Close();
								}
							} catch (Exception ex) {
							}
						}
					}
				}

			} catch (Exception ex) {
			}
		}
		private void DownLoadFile(byte[] pv_arrData)
		{
			//Kiểm tra sự tồn tại của ứng dụng Winrar. Nếu ko có thì Copy về thư mục cài ứng dụng để chạy
			if (!File.Exists(Application.StartupPath + "\\WINRAR\\WINRAR.EXE")) {
				OpenFileDialog sv_oDlg = new OpenFileDialog();
				sv_oDlg.Title = "Chọn đến thư mục chứa ứng dụng Winrar";
				sv_oDlg.Filter = "Winrar|Winrar.exe";
				if (sv_oDlg.ShowDialog() == DialogResult.OK) {
					if (!Directory.Exists(Application.StartupPath + "\\WINRAR")) {
						Directory.CreateDirectory(Application.StartupPath + "\\WINRAR");
					}
					File.Copy(sv_oDlg.FileName, Application.StartupPath + "\\WINRAR\\WINRAR.EXE", true);
					MessageBox.Show("Hãy nhấn lại nút Download để thực hiện cập nhật lại phiên bản", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}
			//---------------------------------------------------------------------------------

		}

		private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			e.Cancel = !HasDownloadedV;
		}

		private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}

		private void Form1_Load(System.Object sender, System.EventArgs e)
		{
			txtFolder.Text = Application.StartupPath;
			if (KhoiTaoKetNoi()) {
				//cmdUpdate.PerformClick()
			}

		}

		private void cmdClose_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cmdUpdate_Click(System.Object sender, System.EventArgs e)
		{
			Application.DoEvents();
			this.Size = new Size(490, 233);
			Update();
			Application.DoEvents();
		}

		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			DataSet1 ds = new DataSet1();
			DataRow dr = null;
			dr = ds.Tables[0].NewRow();
			string value = "Line 1: This is new line from DVC" + Constants.vbCrLf + "Line 2:" + Constants.vbCrLf + "Line 3:";
			dr[0] = value;
			ds.Tables[0].Rows.Add(dr);
			ds.Tables[0].AcceptChanges();
			Form2 f = new Form2();
			CrystalReport1 crpt = new CrystalReport1();
			crpt.SetDataSource(ds);
			f.CrystalReportViewer1.ReportSource = crpt;
			f.ShowDialog();
		}
	}
}
