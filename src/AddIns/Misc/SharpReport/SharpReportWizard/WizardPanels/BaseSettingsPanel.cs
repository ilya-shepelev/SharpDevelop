/*
 * Created by SharpDevelop.
 * User: Forstmeier Peter
 * Date: 27.01.2005
 * Time: 09:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Globalization;
//using System.ComponentModel;

using System.Windows.Forms;

using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;

using SharpReportCore;

namespace ReportGenerator{
	/// <summary>
	/// Description of ReportGenerator.
	/// </summary>
	public class BaseSettingsPanel : AbstractWizardPanel
	{
		private System.Windows.Forms.RadioButton radioPushModell;
		private System.Windows.Forms.RadioButton radioPullModell;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtReportName;
		private System.Windows.Forms.ComboBox cboReportType;
		private System.Windows.Forms.ComboBox cboGraphicsUnit;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton radioFormSheet;
		
		ReportGenerator generator;
		Properties customizer;
	
		bool initDone;
		
		public BaseSettingsPanel(){	
			InitializeComponent();
			Localise();
			
			DoInit();
			base.VisibleChanged += new EventHandler (ChangedEvent );
		}
		
		private void Localise() {
			this.radioFormSheet.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.ReportModel.FormSheet");
			
			this.label1.Text = "Report Name";
			this.label2.Text = ResourceService.GetString("Global.Path");
			this.label3.Text = "Report Type ";
			this.label4.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.GraphicsUnit");
			this.label5.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.FileName");
			
			this.groupBox1.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.Group");
			this.groupBox2.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.ReportModel");
			
			this.radioPullModell.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.ReportModel.Pull");
																					
			this.radioPushModell.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.ReportModel.Push");
			this.radioFormSheet.Text = ResourceService.GetString("SharpReport.Wizard.BaseSettings.ReportModel.FormSheet");
			
			this.button1.Text = "...";
		}
		#region overrides
		
		public override bool ReceiveDialogMessage(DialogMessage message){
			
			if (message == DialogMessage.Finish) {
//				System.Console.WriteLine("  resiveMessage Finish");
			} else if ( message == DialogMessage.Next) {
//				System.Console.WriteLine("  resiveMessage Next");
				base.EnableFinish = true;
			}
			return true;
		}
		
		public override object CustomizationObject {
			get {
				return customizer;
			}
			set {
		
				this.customizer = (Properties)value;
				generator = (ReportGenerator)customizer.Get("Generator");
			}
		}
		
		#endregion
		
		
		void DoInit(){
			txtFileName.Text = GlobalValues.SharpReportPlainFileName;
			txtReportName.Text = GlobalValues.SharpReportStandartFileName;
					
			cboReportType.Items.AddRange(Enum.GetNames (typeof(GlobalEnums.enmReportType)));
			cboReportType.SelectedIndex = cboReportType.FindString(GlobalEnums.enmReportType.DataReport.ToString());
			cboGraphicsUnit.Items.Add (GraphicsUnit.Millimeter);
			cboGraphicsUnit.Items.Add (GraphicsUnit.Inch);
			cboGraphicsUnit.SelectedIndex = cboGraphicsUnit.FindString(GraphicsUnit.Millimeter.ToString());
			
			this.radioPullModell.Checked = true;
			initDone = true;
		}
	
		void ChangedEvent(object sender, EventArgs e){
			
			if (initDone) {
				generator.ReportName = txtReportName.Text;
				generator.FileName = txtFileName.Text;
				generator.Path = this.txtPath.Text;
				generator.GraphicsUnit = (GraphicsUnit)Enum.Parse(typeof(GraphicsUnit),this.cboGraphicsUnit.Text);
				SetSuccessor (this,new EventArgs());
			}
			
		}
		
		
		void SetSuccessor(object sender, System.EventArgs e)
		{
			if (initDone) {
				if (this.radioPullModell.Checked == true) {
					base.NextWizardPanelID = "PullModel";
					generator.DataModel = GlobalEnums.enmPushPullModel.PullData;
					GoOn();
				} else if (this.radioPushModell.Checked == true){
					base.NextWizardPanelID = "PushModel";
					generator.DataModel = GlobalEnums.enmPushPullModel.PushData;
					GoOn();
				} else if (this.radioFormSheet.Checked == true){
					generator.DataModel = GlobalEnums.enmPushPullModel.FormSheet;
					base.EnableNext = false;
					base.IsLastPanel = true;
				}
				base.EnableFinish = true;
			}
			
		}
		
		void GoOn (){
			base.EnableNext = true;
			base.IsLastPanel = false;
		}
		
		///<summary>Browse for Report Folder</summary>
		void Button1Click(object sender, System.EventArgs e) {
			try {
				ICSharpCode.SharpDevelop.Gui.FolderDialog ff = new ICSharpCode.SharpDevelop.Gui.FolderDialog();
				ff.DisplayDialog("");
				if (!String.IsNullOrEmpty(ff.Path)) {
					if (!ff.Path.EndsWith(@"\")){
					                    
						this.txtPath.Text = ff.Path + @"\";
						System.Console.WriteLine("added slash");
					} else {
						
						this.txtPath.Text = ff.Path;
						System.Console.WriteLine("no slash added");
					}
					generator.Path = this.txtPath.Text;
				}
				
			} catch (Exception ) {
				throw ;
			}
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>

		private void InitializeComponent() {
			this.radioFormSheet = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.cboGraphicsUnit = new System.Windows.Forms.ComboBox();
			this.cboReportType = new System.Windows.Forms.ComboBox();
			this.txtReportName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.radioPullModell = new System.Windows.Forms.RadioButton();
			this.radioPushModell = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// radioFormSheet
			// 
			this.radioFormSheet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioFormSheet.Location = new System.Drawing.Point(256, 16);
			this.radioFormSheet.Name = "radioFormSheet";
			this.radioFormSheet.Size = new System.Drawing.Size(80, 24);
			this.radioFormSheet.TabIndex = 22;
			this.radioFormSheet.CheckedChanged += new System.EventHandler(this.SetSuccessor);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 24);
			this.label4.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 24);
			this.label5.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 24);
			this.label2.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 14;
			this.label3.Visible = false;
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(112, 24);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(224, 20);
			this.txtPath.TabIndex = 13;
			this.txtPath.Text = "";
			this.txtPath.TextChanged += new System.EventHandler(this.ChangedEvent);
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(120, 80);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(248, 20);
			this.txtFileName.TabIndex = 11;
			this.txtFileName.Text = "";
			// 
			// cboGraphicsUnit
			// 
			this.cboGraphicsUnit.Location = new System.Drawing.Point(112, 160);
			this.cboGraphicsUnit.Name = "cboGraphicsUnit";
			this.cboGraphicsUnit.Size = new System.Drawing.Size(248, 21);
			this.cboGraphicsUnit.TabIndex = 17;
			// 
			// cboReportType
			// 
			this.cboReportType.Location = new System.Drawing.Point(112, 120);
			this.cboReportType.Name = "cboReportType";
			this.cboReportType.Size = new System.Drawing.Size(248, 21);
			this.cboReportType.TabIndex = 16;
			this.cboReportType.Visible = false;
			this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.ChangedEvent);
			// 
			// txtReportName
			// 
			this.txtReportName.Location = new System.Drawing.Point(120, 40);
			this.txtReportName.Name = "txtReportName";
			this.txtReportName.Size = new System.Drawing.Size(248, 20);
			this.txtReportName.TabIndex = 0;
			this.txtReportName.Text = "";
			this.txtReportName.TextChanged += new System.EventHandler(this.ChangedEvent);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.cboGraphicsUnit);
			this.groupBox1.Controls.Add(this.cboReportType);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtPath);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(8, 160);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(392, 192);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioFormSheet);
			this.groupBox2.Controls.Add(this.radioPullModell);
			this.groupBox2.Controls.Add(this.radioPushModell);
			this.groupBox2.Location = new System.Drawing.Point(16, 56);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(360, 48);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(352, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 21);
			this.button1.TabIndex = 18;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// radioPullModell
			// 
			this.radioPullModell.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioPullModell.Location = new System.Drawing.Point(24, 16);
			this.radioPullModell.Name = "radioPullModell";
			this.radioPullModell.Size = new System.Drawing.Size(88, 24);
			this.radioPullModell.TabIndex = 21;
			this.radioPullModell.CheckedChanged += new System.EventHandler(this.SetSuccessor);
			// 
			// radioPushModell
			// 
			this.radioPushModell.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioPushModell.Location = new System.Drawing.Point(136, 16);
			this.radioPushModell.Name = "radioPushModell";
			this.radioPushModell.Size = new System.Drawing.Size(96, 24);
			this.radioPushModell.TabIndex = 20;
			this.radioPushModell.CheckedChanged += new System.EventHandler(this.SetSuccessor);
			// 
			// BaseSettingsPanel
			// 
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtReportName);
			this.Name = "BaseSettingsPanel";
			this.Size = new System.Drawing.Size(424, 368);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion	
	}
}
