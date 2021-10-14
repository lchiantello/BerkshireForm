
namespace BerkshireForm
{
    partial class BHHCReasonViewer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lstReasons = new System.Windows.Forms.ListBox();
            this.lblReasons = new System.Windows.Forms.Label();
            this.btnAddReason = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstReasons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblReasons, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddReason, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.86475F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.13525F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(891, 487);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(260, 340);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 29);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lstReasons
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lstReasons, 4);
            this.lstReasons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReasons.FormattingEnabled = true;
            this.lstReasons.ItemHeight = 20;
            this.lstReasons.Location = new System.Drawing.Point(10, 45);
            this.lstReasons.Margin = new System.Windows.Forms.Padding(10);
            this.lstReasons.Name = "lstReasons";
            this.lstReasons.Size = new System.Drawing.Size(871, 275);
            this.lstReasons.TabIndex = 0;
            // 
            // lblReasons
            // 
            this.lblReasons.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblReasons, 3);
            this.lblReasons.Location = new System.Drawing.Point(10, 10);
            this.lblReasons.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.lblReasons.Name = "lblReasons";
            this.lblReasons.Size = new System.Drawing.Size(239, 20);
            this.lblReasons.TabIndex = 1;
            this.lblReasons.Text = "Reasons I\'d Like to Work for BHHC:";
            this.lblReasons.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnAddReason
            // 
            this.btnAddReason.Location = new System.Drawing.Point(10, 340);
            this.btnAddReason.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.btnAddReason.Name = "btnAddReason";
            this.btnAddReason.Size = new System.Drawing.Size(94, 29);
            this.btnAddReason.TabIndex = 2;
            this.btnAddReason.Text = "Add...";
            this.btnAddReason.UseVisualStyleBackColor = true;
            this.btnAddReason.Click += new System.EventHandler(this.btnAddReason_onClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(135, 340);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 29);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(787, 340);
            this.btnExit.Margin = new System.Windows.Forms.Padding(10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 29);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BHHCReasonViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 487);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BHHCReasonViewer";
            this.Text = "BHHC Reasons";
            this.Load += new System.EventHandler(this.BHHCReasonViewer_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstReasons;
        private System.Windows.Forms.Label lblReasons;
        private System.Windows.Forms.Button btnAddReason;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
    }
}

