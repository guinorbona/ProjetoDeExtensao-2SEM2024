namespace SeoPimenta.Telas.CadastroProcuraProdutos
{
    partial class TelaListarProdutos
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
            this.dataGridProduos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProduos)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridProduos
            // 
            this.dataGridProduos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProduos.Location = new System.Drawing.Point(12, 12);
            this.dataGridProduos.Name = "dataGridProduos";
            this.dataGridProduos.Size = new System.Drawing.Size(895, 262);
            this.dataGridProduos.TabIndex = 1;
            // 
            // TelaListarProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 450);
            this.Controls.Add(this.dataGridProduos);
            this.Name = "TelaListarProdutos";
            this.Text = "TelaListarProdutos";
            this.Load += new System.EventHandler(this.TelaListarProdutos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProduos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridProduos;
    }
}