namespace ReachConnectCE
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblBluetoothConnection = new System.Windows.Forms.Label();
            this.picIconGrey = new System.Windows.Forms.PictureBox();
            this.picIconBlue = new System.Windows.Forms.PictureBox();
            this.lblRealtimeAverage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblQuality = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.lblEast = new System.Windows.Forms.Label();
            this.lblNorth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCopyEast = new System.Windows.Forms.Button();
            this.btnCopyNorth = new System.Windows.Forms.Button();
            this.btnCopyHeight = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.lblSecond = new System.Windows.Forms.Label();
            this.btnTimer = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.lblPointNumber = new System.Windows.Forms.Label();
            this.btnTopSURV = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBluetoothConnection
            // 
            this.lblBluetoothConnection.Location = new System.Drawing.Point(46, 18);
            this.lblBluetoothConnection.Name = "lblBluetoothConnection";
            this.lblBluetoothConnection.Size = new System.Drawing.Size(173, 20);
            this.lblBluetoothConnection.Text = "Verbindung aufbauen...";
            // 
            // picIconGrey
            // 
            this.picIconGrey.Image = ((System.Drawing.Image)(resources.GetObject("picIconGrey.Image")));
            this.picIconGrey.Location = new System.Drawing.Point(17, 11);
            this.picIconGrey.Name = "picIconGrey";
            this.picIconGrey.Size = new System.Drawing.Size(20, 30);
            // 
            // picIconBlue
            // 
            this.picIconBlue.Image = ((System.Drawing.Image)(resources.GetObject("picIconBlue.Image")));
            this.picIconBlue.Location = new System.Drawing.Point(17, 11);
            this.picIconBlue.Name = "picIconBlue";
            this.picIconBlue.Size = new System.Drawing.Size(20, 30);
            this.picIconBlue.Visible = false;
            // 
            // lblRealtimeAverage
            // 
            this.lblRealtimeAverage.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblRealtimeAverage.Location = new System.Drawing.Point(17, 49);
            this.lblRealtimeAverage.Name = "lblRealtimeAverage";
            this.lblRealtimeAverage.Size = new System.Drawing.Size(206, 20);
            this.lblRealtimeAverage.Text = "UTM Koordinaten:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.Text = "Lösung:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.Text = "Zone:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.Text = "Ostwert:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.Text = "Nordwert:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.Text = "Höhe:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblQuality
            // 
            this.lblQuality.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuality.Location = new System.Drawing.Point(81, 72);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(90, 20);
            // 
            // lblZone
            // 
            this.lblZone.Location = new System.Drawing.Point(81, 95);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(90, 20);
            // 
            // lblEast
            // 
            this.lblEast.Location = new System.Drawing.Point(81, 118);
            this.lblEast.Name = "lblEast";
            this.lblEast.Size = new System.Drawing.Size(90, 20);
            // 
            // lblNorth
            // 
            this.lblNorth.Location = new System.Drawing.Point(81, 141);
            this.lblNorth.Name = "lblNorth";
            this.lblNorth.Size = new System.Drawing.Size(90, 20);
            // 
            // lblHeight
            // 
            this.lblHeight.Location = new System.Drawing.Point(81, 164);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(90, 20);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(17, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 20);
            this.label6.Text = "Position mitteln:";
            // 
            // btnCopyEast
            // 
            this.btnCopyEast.Enabled = false;
            this.btnCopyEast.Location = new System.Drawing.Point(163, 117);
            this.btnCopyEast.Name = "btnCopyEast";
            this.btnCopyEast.Size = new System.Drawing.Size(60, 20);
            this.btnCopyEast.TabIndex = 29;
            this.btnCopyEast.Text = "Kopieren";
            this.btnCopyEast.Click += new System.EventHandler(this.btnCopyEast_Click);
            // 
            // btnCopyNorth
            // 
            this.btnCopyNorth.Enabled = false;
            this.btnCopyNorth.Location = new System.Drawing.Point(163, 140);
            this.btnCopyNorth.Name = "btnCopyNorth";
            this.btnCopyNorth.Size = new System.Drawing.Size(60, 20);
            this.btnCopyNorth.TabIndex = 30;
            this.btnCopyNorth.Text = "Kopieren";
            this.btnCopyNorth.Click += new System.EventHandler(this.btnCopyNorth_Click);
            // 
            // btnCopyHeight
            // 
            this.btnCopyHeight.Enabled = false;
            this.btnCopyHeight.Location = new System.Drawing.Point(163, 163);
            this.btnCopyHeight.Name = "btnCopyHeight";
            this.btnCopyHeight.Size = new System.Drawing.Size(60, 20);
            this.btnCopyHeight.TabIndex = 31;
            this.btnCopyHeight.Text = "Kopieren";
            this.btnCopyHeight.Click += new System.EventHandler(this.btnCopyHeight_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblSecond
            // 
            this.lblSecond.Location = new System.Drawing.Point(17, 214);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(98, 20);
            this.lblSecond.Text = "Sekunden: 0";
            // 
            // btnTimer
            // 
            this.btnTimer.Enabled = false;
            this.btnTimer.Location = new System.Drawing.Point(163, 211);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(60, 20);
            this.btnTimer.TabIndex = 27;
            this.btnTimer.Text = "Start";
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // lblPointNumber
            // 
            this.lblPointNumber.Location = new System.Drawing.Point(17, 237);
            this.lblPointNumber.Name = "lblPointNumber";
            this.lblPointNumber.Size = new System.Drawing.Size(154, 20);
            this.lblPointNumber.Text = "Neuen Punkt Nr. 101";
            // 
            // btnTopSURV
            // 
            this.btnTopSURV.Enabled = false;
            this.btnTopSURV.Location = new System.Drawing.Point(163, 257);
            this.btnTopSURV.Name = "btnTopSURV";
            this.btnTopSURV.Size = new System.Drawing.Size(60, 20);
            this.btnTopSURV.TabIndex = 50;
            this.btnTopSURV.Text = "Neu";
            this.btnTopSURV.Click += new System.EventHandler(this.btnTopSURV_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.Text = "in TopSURV anlegen:";
            // 
            // btnPlus
            // 
            this.btnPlus.Enabled = false;
            this.btnPlus.Location = new System.Drawing.Point(194, 234);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(29, 20);
            this.btnPlus.TabIndex = 68;
            this.btnPlus.Text = "+";
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Enabled = false;
            this.btnMinus.Location = new System.Drawing.Point(163, 234);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(29, 20);
            this.btnMinus.TabIndex = 69;
            this.btnMinus.Text = "-";
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnTopSURV);
            this.Controls.Add(this.lblPointNumber);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.btnCopyHeight);
            this.Controls.Add(this.btnCopyNorth);
            this.Controls.Add(this.btnCopyEast);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRealtimeAverage);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblNorth);
            this.Controls.Add(this.lblEast);
            this.Controls.Add(this.lblZone);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picIconBlue);
            this.Controls.Add(this.picIconGrey);
            this.Controls.Add(this.lblBluetoothConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ReachConnectCE";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBluetoothConnection;
        private System.Windows.Forms.PictureBox picIconGrey;
        private System.Windows.Forms.PictureBox picIconBlue;
        private System.Windows.Forms.Label lblRealtimeAverage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.Label lblEast;
        private System.Windows.Forms.Label lblNorth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCopyEast;
        private System.Windows.Forms.Button btnCopyNorth;
        private System.Windows.Forms.Button btnCopyHeight;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Button btnTimer;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Label lblPointNumber;
        private System.Windows.Forms.Button btnTopSURV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;

    }
}

