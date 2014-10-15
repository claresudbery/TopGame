namespace TopGameWindowsApp
{
    partial class CardPicker
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
            this.btnSaveCards = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSpuds = new System.Windows.Forms.Button();
            this.lblPlayer01Label = new System.Windows.Forms.Label();
            this.lblPlayer02Label = new System.Windows.Forms.Label();
            this.lblPlayer02Cards = new System.Windows.Forms.Label();
            this.lblPlayer01Cards = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveCards
            // 
            this.btnSaveCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCards.Location = new System.Drawing.Point(1142, 603);
            this.btnSaveCards.Name = "btnSaveCards";
            this.btnSaveCards.Size = new System.Drawing.Size(106, 42);
            this.btnSaveCards.TabIndex = 0;
            this.btnSaveCards.Text = "Save";
            this.btnSaveCards.UseVisualStyleBackColor = true;
            this.btnSaveCards.Click += new System.EventHandler(this.btnSaveCards_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(1253, 603);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 42);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSpuds
            // 
            this.btnSpuds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpuds.Location = new System.Drawing.Point(1031, 603);
            this.btnSpuds.Name = "btnSpuds";
            this.btnSpuds.Size = new System.Drawing.Size(106, 42);
            this.btnSpuds.TabIndex = 3;
            this.btnSpuds.Text = "Spuds";
            this.btnSpuds.UseVisualStyleBackColor = true;
            this.btnSpuds.Click += new System.EventHandler(this.btnSpuds_Click);
            // 
            // lblPlayer01Label
            // 
            this.lblPlayer01Label.AutoSize = true;
            this.lblPlayer01Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer01Label.Location = new System.Drawing.Point(12, 585);
            this.lblPlayer01Label.Name = "lblPlayer01Label";
            this.lblPlayer01Label.Size = new System.Drawing.Size(132, 36);
            this.lblPlayer01Label.TabIndex = 4;
            this.lblPlayer01Label.Text = "Player 1:";
            // 
            // lblPlayer02Label
            // 
            this.lblPlayer02Label.AutoSize = true;
            this.lblPlayer02Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer02Label.Location = new System.Drawing.Point(12, 634);
            this.lblPlayer02Label.Name = "lblPlayer02Label";
            this.lblPlayer02Label.Size = new System.Drawing.Size(132, 36);
            this.lblPlayer02Label.TabIndex = 5;
            this.lblPlayer02Label.Text = "Player 2:";
            // 
            // lblPlayer02Cards
            // 
            this.lblPlayer02Cards.AutoSize = true;
            this.lblPlayer02Cards.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer02Cards.Location = new System.Drawing.Point(134, 634);
            this.lblPlayer02Cards.Name = "lblPlayer02Cards";
            this.lblPlayer02Cards.Size = new System.Drawing.Size(226, 36);
            this.lblPlayer02Cards.TabIndex = 6;
            this.lblPlayer02Cards.Text = "Player 2\'s cards";
            // 
            // lblPlayer01Cards
            // 
            this.lblPlayer01Cards.AutoSize = true;
            this.lblPlayer01Cards.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer01Cards.Location = new System.Drawing.Point(134, 585);
            this.lblPlayer01Cards.Name = "lblPlayer01Cards";
            this.lblPlayer01Cards.Size = new System.Drawing.Size(226, 36);
            this.lblPlayer01Cards.TabIndex = 7;
            this.lblPlayer01Cards.Text = "Player 1\'s cards";
            // 
            // CardPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1364, 676);
            this.Controls.Add(this.lblPlayer01Cards);
            this.Controls.Add(this.lblPlayer02Cards);
            this.Controls.Add(this.lblPlayer02Label);
            this.Controls.Add(this.lblPlayer01Label);
            this.Controls.Add(this.btnSpuds);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveCards);
            this.Name = "CardPicker";
            this.Text = "Form1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveCards;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSpuds;
        private System.Windows.Forms.Label lblPlayer01Label;
        private System.Windows.Forms.Label lblPlayer02Label;
        private System.Windows.Forms.Label lblPlayer02Cards;
        private System.Windows.Forms.Label lblPlayer01Cards;
    }
}