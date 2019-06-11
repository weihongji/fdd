namespace Fdd
{
    partial class FinderForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinderForm));
			this.panelFilter = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.lblFilter = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusBarLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.panelResult = new System.Windows.Forms.Panel();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelFilter.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panelResult.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelFilter
			// 
			this.panelFilter.Controls.Add(this.btnRefresh);
			this.panelFilter.Controls.Add(this.txtFilter);
			this.panelFilter.Controls.Add(this.lblFilter);
			this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelFilter.Location = new System.Drawing.Point(0, 0);
			this.panelFilter.Margin = new System.Windows.Forms.Padding(4);
			this.panelFilter.Name = "panelFilter";
			this.panelFilter.Size = new System.Drawing.Size(534, 49);
			this.panelFilter.TabIndex = 0;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(392, 9);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(80, 30);
			this.btnRefresh.TabIndex = 2;
			this.btnRefresh.Text = "&Refresh";
			this.toolTip1.SetToolTip(this.btnRefresh, "Click to re-load backups from file");
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// txtFilter
			// 
			this.txtFilter.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFilter.Location = new System.Drawing.Point(49, 13);
			this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(330, 22);
			this.txtFilter.TabIndex = 1;
			this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
			this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
			// 
			// lblFilter
			// 
			this.lblFilter.AutoSize = true;
			this.lblFilter.Location = new System.Drawing.Point(6, 16);
			this.lblFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFilter.Name = "lblFilter";
			this.lblFilter.Size = new System.Drawing.Size(43, 17);
			this.lblFilter.TabIndex = 0;
			this.lblFilter.Text = "&Filter:";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 664);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(534, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusBar";
			// 
			// statusBarLabel1
			// 
			this.statusBarLabel1.Name = "statusBarLabel1";
			this.statusBarLabel1.Size = new System.Drawing.Size(85, 17);
			this.statusBarLabel1.Text = "0 record found";
			// 
			// panelResult
			// 
			this.panelResult.Controls.Add(this.txtResult);
			this.panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelResult.Location = new System.Drawing.Point(0, 49);
			this.panelResult.Margin = new System.Windows.Forms.Padding(4);
			this.panelResult.Name = "panelResult";
			this.panelResult.Padding = new System.Windows.Forms.Padding(4);
			this.panelResult.Size = new System.Drawing.Size(534, 615);
			this.panelResult.TabIndex = 1;
			// 
			// txtResult
			// 
			this.txtResult.BackColor = System.Drawing.SystemColors.Window;
			this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtResult.Location = new System.Drawing.Point(4, 4);
			this.txtResult.Margin = new System.Windows.Forms.Padding(4);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResult.Size = new System.Drawing.Size(526, 607);
			this.txtResult.TabIndex = 0;
			this.txtResult.Text = "Servlet.ajax_db_20190313015526.BAK\r\nServlet.allentxparks_db_20190308040345.BAK\r\nS" +
    "ervlet.santamonicarecreation_db_20190221,222,757.BAK";
			this.txtResult.WordWrap = false;
			// 
			// FinderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 686);
			this.Controls.Add(this.panelResult);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.panelFilter);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FinderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DB Finder";
			this.Load += new System.EventHandler(this.FinderForm_Load);
			this.panelFilter.ResumeLayout(false);
			this.panelFilter.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panelResult.ResumeLayout(false);
			this.panelResult.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelResult;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.ToolStripStatusLabel statusBarLabel1;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.ToolTip toolTip1;
    }
}

