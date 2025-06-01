namespace VMS.Invoice
{
    partial class FrmThongtinDieuchinh
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
            this.uiStatusBar2 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtinvoiceType = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtInvoiceNumber = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtInvoiceBuyerName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtInvoiceBuyerAddress = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtsovanban = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtngaydieuchinh = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txthovaten = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtdiachithaythe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtghichu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdChangeDinhDanh = new Janus.Windows.EditControls.UIButton();
            this.cmdDownloadDinhDanh = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInvoiceSeries = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtinv_InvoiceAuth_id = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtngayhoadon = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label13 = new System.Windows.Forms.Label();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.txtinv_InvoiceAuth_id_new = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtmaPin = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmdkyhoadon = new Janus.Windows.EditControls.UIButton();
            this.SuspendLayout();
            // 
            // uiStatusBar2
            // 
            this.uiStatusBar2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiStatusBar2.Location = new System.Drawing.Point(0, 417);
            this.uiStatusBar2.Name = "uiStatusBar2";
            this.uiStatusBar2.Size = new System.Drawing.Size(688, 31);
            this.uiStatusBar2.TabIndex = 11;
            this.uiStatusBar2.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(535, 15);
            this.label4.TabIndex = 461;
            this.label4.Text = "Thông tin hóa đơn cũ __________________________________________________________";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 15);
            this.label1.TabIndex = 462;
            this.label1.Text = "Thông tin điều chỉnh __________________________________________________________";
            // 
            // txtinvoiceType
            // 
            this.txtinvoiceType.Location = new System.Drawing.Point(139, 39);
            this.txtinvoiceType.Name = "txtinvoiceType";
            this.txtinvoiceType.ReadOnly = true;
            this.txtinvoiceType.Size = new System.Drawing.Size(122, 21);
            this.txtinvoiceType.TabIndex = 463;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(533, 39);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.ReadOnly = true;
            this.txtInvoiceNumber.Size = new System.Drawing.Size(122, 21);
            this.txtInvoiceNumber.TabIndex = 464;
            // 
            // txtInvoiceBuyerName
            // 
            this.txtInvoiceBuyerName.Location = new System.Drawing.Point(139, 66);
            this.txtInvoiceBuyerName.Name = "txtInvoiceBuyerName";
            this.txtInvoiceBuyerName.ReadOnly = true;
            this.txtInvoiceBuyerName.Size = new System.Drawing.Size(296, 21);
            this.txtInvoiceBuyerName.TabIndex = 465;
            // 
            // txtInvoiceBuyerAddress
            // 
            this.txtInvoiceBuyerAddress.Location = new System.Drawing.Point(139, 93);
            this.txtInvoiceBuyerAddress.Name = "txtInvoiceBuyerAddress";
            this.txtInvoiceBuyerAddress.ReadOnly = true;
            this.txtInvoiceBuyerAddress.Size = new System.Drawing.Size(516, 21);
            this.txtInvoiceBuyerAddress.TabIndex = 466;
            // 
            // txtsovanban
            // 
            this.txtsovanban.Location = new System.Drawing.Point(139, 174);
            this.txtsovanban.MaxLength = 50;
            this.txtsovanban.Name = "txtsovanban";
            this.txtsovanban.Size = new System.Drawing.Size(296, 21);
            this.txtsovanban.TabIndex = 467;
            // 
            // dtngaydieuchinh
            // 
            this.dtngaydieuchinh.CustomFormat = "dd/MM/yyyy";
            this.dtngaydieuchinh.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtngaydieuchinh.DropDownCalendar.Name = "";
            this.dtngaydieuchinh.Location = new System.Drawing.Point(533, 174);
            this.dtngaydieuchinh.Name = "dtngaydieuchinh";
            this.dtngaydieuchinh.ReadOnly = true;
            this.dtngaydieuchinh.ShowUpDown = true;
            this.dtngaydieuchinh.Size = new System.Drawing.Size(122, 21);
            this.dtngaydieuchinh.TabIndex = 468;
            // 
            // txthovaten
            // 
            this.txthovaten.Location = new System.Drawing.Point(139, 201);
            this.txthovaten.Name = "txthovaten";
            this.txthovaten.Size = new System.Drawing.Size(516, 21);
            this.txthovaten.TabIndex = 469;
            // 
            // txtdiachithaythe
            // 
            this.txtdiachithaythe.Location = new System.Drawing.Point(139, 228);
            this.txtdiachithaythe.Name = "txtdiachithaythe";
            this.txtdiachithaythe.Size = new System.Drawing.Size(516, 21);
            this.txtdiachithaythe.TabIndex = 470;
            // 
            // txtghichu
            // 
            this.txtghichu.Location = new System.Drawing.Point(139, 255);
            this.txtghichu.Name = "txtghichu";
            this.txtghichu.Size = new System.Drawing.Size(516, 21);
            this.txtghichu.TabIndex = 471;
            // 
            // cmdChangeDinhDanh
            // 
            this.cmdChangeDinhDanh.Enabled = false;
            this.cmdChangeDinhDanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChangeDinhDanh.Location = new System.Drawing.Point(139, 369);
            this.cmdChangeDinhDanh.Name = "cmdChangeDinhDanh";
            this.cmdChangeDinhDanh.Size = new System.Drawing.Size(127, 42);
            this.cmdChangeDinhDanh.TabIndex = 472;
            this.cmdChangeDinhDanh.Text = "Gửi điều chỉnh";
            this.cmdChangeDinhDanh.Click += new System.EventHandler(this.cmdChangeDinhDanh_Click);
            // 
            // cmdDownloadDinhDanh
            // 
            this.cmdDownloadDinhDanh.Enabled = false;
            this.cmdDownloadDinhDanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDownloadDinhDanh.Location = new System.Drawing.Point(395, 369);
            this.cmdDownloadDinhDanh.Name = "cmdDownloadDinhDanh";
            this.cmdDownloadDinhDanh.Size = new System.Drawing.Size(127, 42);
            this.cmdDownloadDinhDanh.TabIndex = 473;
            this.cmdDownloadDinhDanh.Text = "Tải File điều chỉnh";
            this.cmdDownloadDinhDanh.Click += new System.EventHandler(this.cmdDownloadDinhDanh_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(29, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 18);
            this.label2.TabIndex = 474;
            this.label2.Text = "Mẫu hóa đơn";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(430, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 18);
            this.label3.TabIndex = 475;
            this.label3.Text = "Số hóa đơn";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(41, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 18);
            this.label5.TabIndex = 476;
            this.label5.Text = "Người mua";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(41, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 18);
            this.label6.TabIndex = 477;
            this.label6.Text = "Địa chỉ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(41, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 18);
            this.label7.TabIndex = 478;
            this.label7.Text = "Số văn bản";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(426, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 18);
            this.label8.TabIndex = 479;
            this.label8.Text = "Ngày điều chỉnh";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInvoiceSeries
            // 
            this.txtInvoiceSeries.Location = new System.Drawing.Point(323, 39);
            this.txtInvoiceSeries.Name = "txtInvoiceSeries";
            this.txtInvoiceSeries.ReadOnly = true;
            this.txtInvoiceSeries.Size = new System.Drawing.Size(112, 21);
            this.txtInvoiceSeries.TabIndex = 480;
            // 
            // txtinv_InvoiceAuth_id
            // 
            this.txtinv_InvoiceAuth_id.Location = new System.Drawing.Point(139, 120);
            this.txtinv_InvoiceAuth_id.Name = "txtinv_InvoiceAuth_id";
            this.txtinv_InvoiceAuth_id.ReadOnly = true;
            this.txtinv_InvoiceAuth_id.Size = new System.Drawing.Size(516, 21);
            this.txtinv_InvoiceAuth_id.TabIndex = 481;
            this.txtinv_InvoiceAuth_id.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.Location = new System.Drawing.Point(225, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 18);
            this.label9.TabIndex = 482;
            this.label9.Text = "Ký hiệu";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.Location = new System.Drawing.Point(15, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 18);
            this.label10.TabIndex = 483;
            this.label10.Text = "Họ và tên thay thế";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.Location = new System.Drawing.Point(41, 230);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 18);
            this.label11.TabIndex = 484;
            this.label11.Text = "Địa chỉ thay thế";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.Location = new System.Drawing.Point(41, 257);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 18);
            this.label12.TabIndex = 485;
            this.label12.Text = "Ghi chú";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtngayhoadon
            // 
            this.dtngayhoadon.CustomFormat = "dd/MM/yyyy";
            this.dtngayhoadon.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtngayhoadon.DropDownCalendar.Name = "";
            this.dtngayhoadon.Location = new System.Drawing.Point(533, 65);
            this.dtngayhoadon.Name = "dtngayhoadon";
            this.dtngayhoadon.ReadOnly = true;
            this.dtngayhoadon.ShowUpDown = true;
            this.dtngayhoadon.Size = new System.Drawing.Size(122, 21);
            this.dtngayhoadon.TabIndex = 486;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F);
            this.label13.Location = new System.Drawing.Point(439, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 18);
            this.label13.TabIndex = 487;
            this.label13.Text = "Ngày hóa đơn";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Location = new System.Drawing.Point(523, 369);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(127, 42);
            this.cmdExit.TabIndex = 488;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // txtinv_InvoiceAuth_id_new
            // 
            this.txtinv_InvoiceAuth_id_new.Enabled = false;
            this.txtinv_InvoiceAuth_id_new.Location = new System.Drawing.Point(139, 334);
            this.txtinv_InvoiceAuth_id_new.Name = "txtinv_InvoiceAuth_id_new";
            this.txtinv_InvoiceAuth_id_new.ReadOnly = true;
            this.txtinv_InvoiceAuth_id_new.Size = new System.Drawing.Size(516, 21);
            this.txtinv_InvoiceAuth_id_new.TabIndex = 489;
            this.txtinv_InvoiceAuth_id_new.Visible = false;
            // 
            // txtmaPin
            // 
            this.txtmaPin.Location = new System.Drawing.Point(139, 285);
            this.txtmaPin.Name = "txtmaPin";
            this.txtmaPin.Size = new System.Drawing.Size(516, 21);
            this.txtmaPin.TabIndex = 490;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.Location = new System.Drawing.Point(12, 282);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 32);
            this.label14.TabIndex = 491;
            this.label14.Text = "Nhập mã PIN để ký hóa đơn";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdkyhoadon
            // 
            this.cmdkyhoadon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdkyhoadon.Location = new System.Drawing.Point(267, 369);
            this.cmdkyhoadon.Name = "cmdkyhoadon";
            this.cmdkyhoadon.Size = new System.Drawing.Size(127, 42);
            this.cmdkyhoadon.TabIndex = 492;
            this.cmdkyhoadon.Text = "Ký hóa đơn";
            this.cmdkyhoadon.Click += new System.EventHandler(this.cmdkyhoadon_Click);
            // 
            // FrmThongtinDieuchinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 448);
            this.Controls.Add(this.cmdkyhoadon);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtmaPin);
            this.Controls.Add(this.txtinv_InvoiceAuth_id_new);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.dtngayhoadon);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtinv_InvoiceAuth_id);
            this.Controls.Add(this.txtInvoiceSeries);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdDownloadDinhDanh);
            this.Controls.Add(this.cmdChangeDinhDanh);
            this.Controls.Add(this.txtghichu);
            this.Controls.Add(this.txtdiachithaythe);
            this.Controls.Add(this.txthovaten);
            this.Controls.Add(this.dtngaydieuchinh);
            this.Controls.Add(this.txtsovanban);
            this.Controls.Add(this.txtInvoiceBuyerAddress);
            this.Controls.Add(this.txtInvoiceBuyerName);
            this.Controls.Add(this.txtInvoiceNumber);
            this.Controls.Add(this.txtinvoiceType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uiStatusBar2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label13);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "FrmThongtinDieuchinh";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Điều chỉnh hóa đơn";
            this.Load += new System.EventHandler(this.frm_thongtin_dieuchinh_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtinvoiceType;
        private Janus.Windows.GridEX.EditControls.EditBox txtInvoiceNumber;
        private Janus.Windows.GridEX.EditControls.EditBox txtInvoiceBuyerName;
        private Janus.Windows.GridEX.EditControls.EditBox txtInvoiceBuyerAddress;
        private Janus.Windows.GridEX.EditControls.EditBox txtsovanban;
        private Janus.Windows.CalendarCombo.CalendarCombo dtngaydieuchinh;
        private Janus.Windows.GridEX.EditControls.EditBox txthovaten;
        private Janus.Windows.GridEX.EditControls.EditBox txtdiachithaythe;
        private Janus.Windows.GridEX.EditControls.EditBox txtghichu;
        private Janus.Windows.EditControls.UIButton cmdChangeDinhDanh;
        private Janus.Windows.EditControls.UIButton cmdDownloadDinhDanh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtInvoiceSeries;
        private Janus.Windows.GridEX.EditControls.EditBox txtinv_InvoiceAuth_id;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private Janus.Windows.CalendarCombo.CalendarCombo dtngayhoadon;
        private System.Windows.Forms.Label label13;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.EditControls.EditBox txtinv_InvoiceAuth_id_new;
        private Janus.Windows.GridEX.EditControls.EditBox txtmaPin;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.EditControls.UIButton cmdkyhoadon;
    }
}