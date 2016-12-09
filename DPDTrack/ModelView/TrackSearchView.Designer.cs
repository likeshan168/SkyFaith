namespace DPDTrack.ModelView
{
    partial class TrackSearchView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            this.txtAgentNum = new DevExpress.XtraEditors.TextEdit();
            this.txtOrderNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtImportData = new DevExpress.XtraEditors.DateEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cbxBusinessParter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.trackNumInfoCollectionView = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::DPDTrack.LoadingFrm), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtImportData.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtImportData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxBusinessParter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumInfoCollectionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAgentNum
            // 
            this.txtAgentNum.Location = new System.Drawing.Point(714, 12);
            this.txtAgentNum.Name = "txtAgentNum";
            this.txtAgentNum.Size = new System.Drawing.Size(150, 20);
            this.txtAgentNum.TabIndex = 12;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.Location = new System.Drawing.Point(501, 12);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Size = new System.Drawing.Size(150, 20);
            this.txtOrderNum.TabIndex = 11;
            // 
            // labelControl4
            // 
            this.labelControl4.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.labelControl4.Location = new System.Drawing.Point(660, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "代理单号";
            // 
            // labelControl3
            // 
            this.labelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.labelControl3.Location = new System.Drawing.Point(459, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "订单号";
            // 
            // labelControl2
            // 
            this.labelControl2.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.labelControl2.Location = new System.Drawing.Point(243, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "导入时间";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "商业伙伴名称";
            // 
            // dtImportData
            // 
            this.dtImportData.EditValue = "";
            this.dtImportData.Location = new System.Drawing.Point(297, 12);
            this.dtImportData.Name = "dtImportData";
            this.dtImportData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtImportData.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtImportData.Size = new System.Drawing.Size(150, 20);
            this.dtImportData.TabIndex = 10;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(969, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "清空表格";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(888, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            // 
            // cbxBusinessParter
            // 
            this.cbxBusinessParter.Location = new System.Drawing.Point(81, 12);
            this.cbxBusinessParter.Name = "cbxBusinessParter";
            this.cbxBusinessParter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxBusinessParter.Size = new System.Drawing.Size(150, 20);
            this.cbxBusinessParter.TabIndex = 15;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(DPDTrack.ModelView.TrackSearchViewModel);
            // 
            // trackNumInfoCollectionView
            // 
            this.trackNumInfoCollectionView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackNumInfoCollectionView.Location = new System.Drawing.Point(3, 40);
            this.trackNumInfoCollectionView.MainView = this.gridView1;
            this.trackNumInfoCollectionView.Name = "trackNumInfoCollectionView";
            this.trackNumInfoCollectionView.Size = new System.Drawing.Size(1041, 371);
            this.trackNumInfoCollectionView.TabIndex = 16;
            this.trackNumInfoCollectionView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Name = "Condition istop: Y";
            gridFormatRule1.Rule = null;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.GridControl = this.trackNumInfoCollectionView;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView1_CustomDrawEmptyForground);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // TrackSearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackNumInfoCollectionView);
            this.Controls.Add(this.cbxBusinessParter);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtAgentNum);
            this.Controls.Add(this.txtOrderNum);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dtImportData);
            this.Name = "TrackSearchView";
            this.Size = new System.Drawing.Size(1049, 418);
            ((System.ComponentModel.ISupportInitialize)(this.txtAgentNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtImportData.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtImportData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxBusinessParter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumInfoCollectionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.TextEdit txtAgentNum;
        private DevExpress.XtraEditors.TextEdit txtOrderNum;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtImportData;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.ComboBoxEdit cbxBusinessParter;
        private DevExpress.XtraGrid.GridControl trackNumInfoCollectionView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}
