﻿namespace WhiskeyEditor.UI.Menu
{
    partial class NewFileForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.okayBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.scriptBox = new System.Windows.Forms.ComboBox();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.scriptLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.24841F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.75159F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(259, 157);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.okayBtn);
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 36);
            this.panel1.TabIndex = 0;
            // 
            // okayBtn
            // 
            this.okayBtn.Location = new System.Drawing.Point(94, 3);
            this.okayBtn.Name = "okayBtn";
            this.okayBtn.Size = new System.Drawing.Size(75, 23);
            this.okayBtn.TabIndex = 1;
            this.okayBtn.Text = "Okay";
            this.okayBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(175, 3);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nameBox);
            this.panel2.Controls.Add(this.scriptBox);
            this.panel2.Controls.Add(this.typeBox);
            this.panel2.Controls.Add(this.scriptLabel);
            this.panel2.Controls.Add(this.nameLabel);
            this.panel2.Controls.Add(this.typeLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 109);
            this.panel2.TabIndex = 1;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(72, 36);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(178, 22);
            this.nameBox.TabIndex = 5;
            // 
            // scriptBox
            // 
            this.scriptBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scriptBox.FormattingEnabled = true;
            this.scriptBox.Location = new System.Drawing.Point(72, 64);
            this.scriptBox.Name = "scriptBox";
            this.scriptBox.Size = new System.Drawing.Size(178, 24);
            this.scriptBox.TabIndex = 4;
            // 
            // typeBox
            // 
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Location = new System.Drawing.Point(72, 7);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(178, 24);
            this.typeBox.TabIndex = 3;
            // 
            // scriptLabel
            // 
            this.scriptLabel.AutoSize = true;
            this.scriptLabel.Location = new System.Drawing.Point(12, 67);
            this.scriptLabel.Name = "scriptLabel";
            this.scriptLabel.Size = new System.Drawing.Size(29, 17);
            this.scriptLabel.TabIndex = 2;
            this.scriptLabel.Text = "For";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 39);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 17);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(12, 10);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(40, 17);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "Type";
            // 
            // NewFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 160);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewFileForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button okayBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ComboBox scriptBox;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label scriptLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label typeLabel;
    }
}
