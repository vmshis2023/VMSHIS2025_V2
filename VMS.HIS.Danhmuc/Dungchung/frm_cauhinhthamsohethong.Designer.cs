namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_cauhinhthamsohethong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_cauhinhthamsohethong));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.tabPhieuDieuTri = new Janus.Windows.UI.Tab.UITabPage();
            this.pnlBHYT = new System.Windows.Forms.Panel();
            this.ucTextSysparam16 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam14 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam15 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam13 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam9 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam10 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam5 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam6 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam7 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam8 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam4 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam3 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam1 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam2 = new VNS.UCs.ucTextSysparam();
            this.uiTabPage4 = new Janus.Windows.UI.Tab.UITabPage();
            this.uiTabPage3 = new Janus.Windows.UI.Tab.UITabPage();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.pnlCLS = new System.Windows.Forms.Panel();
            this.ucTextSysparam11 = new VNS.UCs.ucTextSysparam();
            this.ucTextSysparam12 = new VNS.UCs.ucTextSysparam();
            this.uiTabPage2 = new Janus.Windows.UI.Tab.UITabPage();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.tabPhieuDieuTri.SuspendLayout();
            this.pnlBHYT.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            this.pnlCLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 67);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdClose);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 673);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 57);
            this.panel2.TabIndex = 23;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdClose.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose.Location = new System.Drawing.Point(882, 10);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(114, 35);
            this.cmdClose.TabIndex = 19;
            this.cmdClose.Text = "Thoát(Esc)";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(751, 10);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(114, 35);
            this.cmdSave.TabIndex = 18;
            this.cmdSave.Text = "Đồng ý (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để thực hiện chuyển đổi đối tượng";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiTab1.Location = new System.Drawing.Point(0, 67);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(1008, 606);
            this.uiTab1.TabIndex = 24;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.tabPhieuDieuTri,
            this.uiTabPage4,
            this.uiTabPage3,
            this.uiTabPage1,
            this.uiTabPage2});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // tabPhieuDieuTri
            // 
            this.tabPhieuDieuTri.Controls.Add(this.pnlBHYT);
            this.tabPhieuDieuTri.Location = new System.Drawing.Point(1, 23);
            this.tabPhieuDieuTri.Name = "tabPhieuDieuTri";
            this.tabPhieuDieuTri.Size = new System.Drawing.Size(1006, 582);
            this.tabPhieuDieuTri.TabStop = true;
            this.tabPhieuDieuTri.Text = "Tham số Bảo hiểm y tế (F1)";
            // 
            // pnlBHYT
            // 
            this.pnlBHYT.Controls.Add(this.ucTextSysparam16);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam14);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam15);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam13);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam9);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam10);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam5);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam6);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam7);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam8);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam4);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam3);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam1);
            this.pnlBHYT.Controls.Add(this.ucTextSysparam2);
            this.pnlBHYT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBHYT.Location = new System.Drawing.Point(0, 0);
            this.pnlBHYT.Name = "pnlBHYT";
            this.pnlBHYT.Size = new System.Drawing.Size(1006, 582);
            this.pnlBHYT.TabIndex = 2;
            // 
            // ucTextSysparam16
            // 
            this.ucTextSysparam16._paramName = "BHYT_GIAYBHYT_PHANTRAM";
            this.ucTextSysparam16._paramTitle = "% BHYT chi trả khi có giấy BHYT";
            this.ucTextSysparam16._paramValue = "";
            this.ucTextSysparam16.Location = new System.Drawing.Point(515, 5);
            this.ucTextSysparam16.Name = "ucTextSysparam16";
            this.ucTextSysparam16.Size = new System.Drawing.Size(479, 62);
            this.ucTextSysparam16.TabIndex = 13;
            // 
            // ucTextSysparam14
            // 
            this.ucTextSysparam14._paramName = "BHYT_TRAIQUYEN_MAQUYENLOI_100";
            this.ucTextSysparam14._paramTitle = "Trái tuyến không quan tâm mã quyền lợi:";
            this.ucTextSysparam14._paramValue = "";
            this.ucTextSysparam14.Location = new System.Drawing.Point(515, 467);
            this.ucTextSysparam14.Name = "ucTextSysparam14";
            this.ucTextSysparam14.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam14.TabIndex = 12;
            // 
            // ucTextSysparam15
            // 
            this.ucTextSysparam15._paramName = "BHYT_TRAITUYEN_GIAYBHYT_100";
            this.ucTextSysparam15._paramTitle = "Trái tuyến không quan tâm giấy BHYT";
            this.ucTextSysparam15._paramValue = "";
            this.ucTextSysparam15.Location = new System.Drawing.Point(17, 467);
            this.ucTextSysparam15.Name = "ucTextSysparam15";
            this.ucTextSysparam15.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam15.TabIndex = 11;
            // 
            // ucTextSysparam13
            // 
            this.ucTextSysparam13._paramName = "BHYT_TUYEN";
            this.ucTextSysparam13._paramTitle = "Tuyến BHYT";
            this.ucTextSysparam13._paramValue = "";
            this.ucTextSysparam13.Location = new System.Drawing.Point(17, 5);
            this.ucTextSysparam13.Name = "ucTextSysparam13";
            this.ucTextSysparam13.Size = new System.Drawing.Size(479, 62);
            this.ucTextSysparam13.TabIndex = 10;
            // 
            // ucTextSysparam9
            // 
            this.ucTextSysparam9._paramName = "KCB_CHOPHEPTIEPDON_TRAITUYEN";
            this.ucTextSysparam9._paramTitle = "Cho phép check tiếp đón trái tuyến";
            this.ucTextSysparam9._paramValue = "";
            this.ucTextSysparam9.Location = new System.Drawing.Point(515, 392);
            this.ucTextSysparam9.Name = "ucTextSysparam9";
            this.ucTextSysparam9.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam9.TabIndex = 9;
            // 
            // ucTextSysparam10
            // 
            this.ucTextSysparam10._paramName = "BHYT_HIENTHIPHANTRAMDAUTHE";
            this.ucTextSysparam10._paramTitle = "Hiển thị % đầu thẻ khi tiếp đón";
            this.ucTextSysparam10._paramValue = "";
            this.ucTextSysparam10.Location = new System.Drawing.Point(17, 392);
            this.ucTextSysparam10.Name = "ucTextSysparam10";
            this.ucTextSysparam10.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam10.TabIndex = 8;
            // 
            // ucTextSysparam5
            // 
            this.ucTextSysparam5._paramName = "BHYT_LUATTRAITUYEN_2015";
            this.ucTextSysparam5._paramTitle = "Luật BHYT trái tuyến  mới:";
            this.ucTextSysparam5._paramValue = "";
            this.ucTextSysparam5.Location = new System.Drawing.Point(515, 308);
            this.ucTextSysparam5.Name = "ucTextSysparam5";
            this.ucTextSysparam5.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam5.TabIndex = 7;
            // 
            // ucTextSysparam6
            // 
            this.ucTextSysparam6._paramName = "BHYT_TRAITUYENNGOAITRU_GIADICHVU";
            this.ucTextSysparam6._paramTitle = "Lấy giá dịch vụ làm giá trái tuyến ngoại trú";
            this.ucTextSysparam6._paramValue = "";
            this.ucTextSysparam6.Location = new System.Drawing.Point(17, 308);
            this.ucTextSysparam6.Name = "ucTextSysparam6";
            this.ucTextSysparam6.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam6.TabIndex = 6;
            // 
            // ucTextSysparam7
            // 
            this.ucTextSysparam7._paramName = "BHYT_MAQUYENLOI_HUONG100PHANTRAM";
            this.ucTextSysparam7._paramTitle = "Mã quyền lợi hưởng 100%";
            this.ucTextSysparam7._paramValue = "";
            this.ucTextSysparam7.Location = new System.Drawing.Point(17, 238);
            this.ucTextSysparam7.Name = "ucTextSysparam7";
            this.ucTextSysparam7.Size = new System.Drawing.Size(479, 62);
            this.ucTextSysparam7.TabIndex = 4;
            // 
            // ucTextSysparam8
            // 
            this.ucTextSysparam8._paramName = "BHYT_PTRAM_TRAITUYENNOITRU";
            this.ucTextSysparam8._paramTitle = "% trái tuyến nội trú(theo tuyến)";
            this.ucTextSysparam8._paramValue = "";
            this.ucTextSysparam8.Location = new System.Drawing.Point(515, 238);
            this.ucTextSysparam8.Name = "ucTextSysparam8";
            this.ucTextSysparam8.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam8.TabIndex = 5;
            // 
            // ucTextSysparam4
            // 
            this.ucTextSysparam4._paramName = "BHYT_PTRAM_LUONGCOBAN";
            this.ucTextSysparam4._paramTitle = "% lương cơ bản";
            this.ucTextSysparam4._paramValue = "";
            this.ucTextSysparam4.Location = new System.Drawing.Point(515, 149);
            this.ucTextSysparam4.Name = "ucTextSysparam4";
            this.ucTextSysparam4.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam4.TabIndex = 3;
            // 
            // ucTextSysparam3
            // 
            this.ucTextSysparam3._paramName = "BHYT_LUONGCOBAN";
            this.ucTextSysparam3._paramTitle = "Lương cơ bản";
            this.ucTextSysparam3._paramValue = "";
            this.ucTextSysparam3.Location = new System.Drawing.Point(17, 149);
            this.ucTextSysparam3.Name = "ucTextSysparam3";
            this.ucTextSysparam3.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam3.TabIndex = 2;
            // 
            // ucTextSysparam1
            // 
            this.ucTextSysparam1._paramName = "BHYT_NOIDANGKY_KCBBD";
            this.ucTextSysparam1._paramTitle = "Nơi đăng ký KCBBĐ";
            this.ucTextSysparam1._paramValue = "";
            this.ucTextSysparam1.Location = new System.Drawing.Point(17, 79);
            this.ucTextSysparam1.Name = "ucTextSysparam1";
            this.ucTextSysparam1.Size = new System.Drawing.Size(479, 62);
            this.ucTextSysparam1.TabIndex = 0;
            // 
            // ucTextSysparam2
            // 
            this.ucTextSysparam2._paramName = "BHYT_NOICAP_BHYT";
            this.ucTextSysparam2._paramTitle = "Nơi cấp BHYT";
            this.ucTextSysparam2._paramValue = "";
            this.ucTextSysparam2.Location = new System.Drawing.Point(515, 79);
            this.ucTextSysparam2.Name = "ucTextSysparam2";
            this.ucTextSysparam2.Size = new System.Drawing.Size(479, 72);
            this.ucTextSysparam2.TabIndex = 1;
            // 
            // uiTabPage4
            // 
            this.uiTabPage4.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage4.Name = "uiTabPage4";
            this.uiTabPage4.Size = new System.Drawing.Size(1006, 582);
            this.uiTabPage4.TabStop = true;
            this.uiTabPage4.Text = "Tiếp đón và thăm khám (F2)";
            // 
            // uiTabPage3
            // 
            this.uiTabPage3.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage3.Name = "uiTabPage3";
            this.uiTabPage3.Size = new System.Drawing.Size(1006, 582);
            this.uiTabPage3.TabStop = true;
            this.uiTabPage3.Text = "Thuốc (F3)";
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.pnlCLS);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(1006, 582);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Cận lâm sàng(F4)";
            // 
            // pnlCLS
            // 
            this.pnlCLS.Controls.Add(this.ucTextSysparam11);
            this.pnlCLS.Controls.Add(this.ucTextSysparam12);
            this.pnlCLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCLS.Location = new System.Drawing.Point(0, 0);
            this.pnlCLS.Name = "pnlCLS";
            this.pnlCLS.Size = new System.Drawing.Size(1006, 582);
            this.pnlCLS.TabIndex = 4;
            // 
            // ucTextSysparam11
            // 
            this.ucTextSysparam11._paramName = "CLS_GIATHEO_QHE";
            this.ucTextSysparam11._paramTitle = "Lấy giá CLS theo giá quan hệ";
            this.ucTextSysparam11._paramValue = "";
            this.ucTextSysparam11.Location = new System.Drawing.Point(11, 7);
            this.ucTextSysparam11.Name = "ucTextSysparam11";
            this.ucTextSysparam11.Size = new System.Drawing.Size(489, 62);
            this.ucTextSysparam11.TabIndex = 2;
            // 
            // ucTextSysparam12
            // 
            this.ucTextSysparam12._paramName = "CLS_GIATHEO_KHOAKCB";
            this.ucTextSysparam12._paramTitle = "Giá CLS theo khoa:";
            this.ucTextSysparam12._paramValue = "";
            this.ucTextSysparam12.Location = new System.Drawing.Point(526, 7);
            this.ucTextSysparam12.Name = "ucTextSysparam12";
            this.ucTextSysparam12.Size = new System.Drawing.Size(477, 72);
            this.ucTextSysparam12.TabIndex = 3;
            // 
            // uiTabPage2
            // 
            this.uiTabPage2.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage2.Name = "uiTabPage2";
            this.uiTabPage2.Size = new System.Drawing.Size(1006, 582);
            this.uiTabPage2.TabStop = true;
            this.uiTabPage2.Text = "Khác(F5)";
            // 
            // frm_cauhinhthamsohethong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.uiTab1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_cauhinhthamsohethong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình tham số hệ thống";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.tabPhieuDieuTri.ResumeLayout(false);
            this.pnlBHYT.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            this.pnlCLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage tabPhieuDieuTri;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage3;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage2;
        private VNS.UCs.ucTextSysparam ucTextSysparam2;
        private VNS.UCs.ucTextSysparam ucTextSysparam1;
        private System.Windows.Forms.Panel pnlBHYT;
        private VNS.UCs.ucTextSysparam ucTextSysparam5;
        private VNS.UCs.ucTextSysparam ucTextSysparam6;
        private VNS.UCs.ucTextSysparam ucTextSysparam7;
        private VNS.UCs.ucTextSysparam ucTextSysparam8;
        private VNS.UCs.ucTextSysparam ucTextSysparam4;
        private VNS.UCs.ucTextSysparam ucTextSysparam3;
        private VNS.UCs.ucTextSysparam ucTextSysparam9;
        private VNS.UCs.ucTextSysparam ucTextSysparam10;
        private VNS.UCs.ucTextSysparam ucTextSysparam11;
        private VNS.UCs.ucTextSysparam ucTextSysparam12;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private System.Windows.Forms.Panel pnlCLS;
        private VNS.UCs.ucTextSysparam ucTextSysparam13;
        private VNS.UCs.ucTextSysparam ucTextSysparam14;
        private VNS.UCs.ucTextSysparam ucTextSysparam15;
        private VNS.UCs.ucTextSysparam ucTextSysparam16;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage4;
    }
}