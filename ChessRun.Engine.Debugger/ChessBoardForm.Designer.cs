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
            this.lblTurn = new System.Windows.Forms.Label();
            this.lblTurnValue = new System.Windows.Forms.Label();
            this.lblEnPassant = new System.Windows.Forms.Label();
            this.lblEnPassantValue = new System.Windows.Forms.Label();
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
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurn.Location = new System.Drawing.Point(418, 21);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(61, 26);
            this.lblTurn.TabIndex = 1;
            this.lblTurn.Text = "Turn:";
            // 
            // lblTurnValue
            // 
            this.lblTurnValue.AutoSize = true;
            this.lblTurnValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurnValue.Location = new System.Drawing.Point(554, 12);
            this.lblTurnValue.Name = "lblTurnValue";
            this.lblTurnValue.Size = new System.Drawing.Size(99, 37);
            this.lblTurnValue.TabIndex = 2;
            this.lblTurnValue.Text = "White";
            // 
            // lblEnPassant
            // 
            this.lblEnPassant.AutoSize = true;
            this.lblEnPassant.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnPassant.Location = new System.Drawing.Point(418, 58);
            this.lblEnPassant.Name = "lblEnPassant";
            this.lblEnPassant.Size = new System.Drawing.Size(130, 26);
            this.lblEnPassant.TabIndex = 3;
            this.lblEnPassant.Text = "En Passant:";
            // 
            // lblEnPassantValue
            // 
            this.lblEnPassantValue.AutoSize = true;
            this.lblEnPassantValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnPassantValue.Location = new System.Drawing.Point(554, 49);
            this.lblEnPassantValue.Name = "lblEnPassantValue";
            this.lblEnPassantValue.Size = new System.Drawing.Size(94, 37);
            this.lblEnPassantValue.TabIndex = 4;
            this.lblEnPassantValue.Text = "None";
            // 
            // ChessBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(683, 431);
            this.Controls.Add(this.lblEnPassantValue);
            this.Controls.Add(this.lblEnPassant);
            this.Controls.Add(this.lblTurnValue);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.pctBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChessBoardForm";
            this.Text = "Chess Board";
            ((System.ComponentModel.ISupportInitialize)(this.pctBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBoard;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Label lblTurnValue;
        private System.Windows.Forms.Label lblEnPassant;
        private System.Windows.Forms.Label lblEnPassantValue;
    }
}