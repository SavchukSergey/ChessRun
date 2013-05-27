namespace ChessRun.Engine.Debugger {
    partial class ChessBoardForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pctBoard = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTurnValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBoard
            // 
            this.pctBoard.Location = new System.Drawing.Point(12, 12);
            this.pctBoard.Name = "pctBoard";
            this.pctBoard.Size = new System.Drawing.Size(400, 400);
            this.pctBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctBoard.TabIndex = 0;
            this.pctBoard.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(418, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Turn:";
            // 
            // lblTurnValue
            // 
            this.lblTurnValue.AutoSize = true;
            this.lblTurnValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurnValue.Location = new System.Drawing.Point(485, 12);
            this.lblTurnValue.Name = "lblTurnValue";
            this.lblTurnValue.Size = new System.Drawing.Size(99, 37);
            this.lblTurnValue.TabIndex = 2;
            this.lblTurnValue.Text = "White";
            // 
            // ChessBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(683, 431);
            this.Controls.Add(this.lblTurnValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pctBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChessBoardForm";
            this.Text = "ChessBoardForm";
            ((System.ComponentModel.ISupportInitialize)(this.pctBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTurnValue;
    }
}