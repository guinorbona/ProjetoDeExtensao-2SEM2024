namespace SeoPimenta.Telas.menuFuncionarioUsuario
{
    partial class visualizarUser
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
            this.body_visualizarUser = new System.Windows.Forms.GroupBox();
            this.dataGridUsuariosAtivos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDesativar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridUsuariosInativos = new System.Windows.Forms.DataGridView();
            this.btbAtivar = new System.Windows.Forms.Button();
            this.body_visualizarUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsuariosAtivos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsuariosInativos)).BeginInit();
            this.SuspendLayout();
            // 
            // body_visualizarUser
            // 
            this.body_visualizarUser.BackColor = System.Drawing.Color.White;
            this.body_visualizarUser.Controls.Add(this.groupBox2);
            this.body_visualizarUser.Controls.Add(this.groupBox1);
            this.body_visualizarUser.Controls.Add(this.label1);
            this.body_visualizarUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.body_visualizarUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body_visualizarUser.Location = new System.Drawing.Point(0, 0);
            this.body_visualizarUser.Name = "body_visualizarUser";
            this.body_visualizarUser.Size = new System.Drawing.Size(1037, 729);
            this.body_visualizarUser.TabIndex = 17;
            this.body_visualizarUser.TabStop = false;
            this.body_visualizarUser.Enter += new System.EventHandler(this.body_visualizarUser_Enter);
            // 
            // dataGridUsuariosAtivos
            // 
            this.dataGridUsuariosAtivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUsuariosAtivos.Location = new System.Drawing.Point(32, 32);
            this.dataGridUsuariosAtivos.Name = "dataGridUsuariosAtivos";
            this.dataGridUsuariosAtivos.Size = new System.Drawing.Size(430, 412);
            this.dataGridUsuariosAtivos.TabIndex = 68;
            this.dataGridUsuariosAtivos.SelectionChanged += new System.EventHandler(this.dataGridUsuariosAtivos_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Leelawadee UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(381, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Controle de  Usuários";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnDesativar
            // 
            this.btnDesativar.BackColor = System.Drawing.Color.Black;
            this.btnDesativar.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesativar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDesativar.Location = new System.Drawing.Point(293, 466);
            this.btnDesativar.Name = "btnDesativar";
            this.btnDesativar.Size = new System.Drawing.Size(169, 43);
            this.btnDesativar.TabIndex = 71;
            this.btnDesativar.Text = "Desativar Usuário";
            this.btnDesativar.UseVisualStyleBackColor = false;
            this.btnDesativar.Click += new System.EventHandler(this.btnDesativar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridUsuariosAtivos);
            this.groupBox1.Controls.Add(this.btnDesativar);
            this.groupBox1.Font = new System.Drawing.Font("Leelawadee UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(22, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 531);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuários Ativos";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridUsuariosInativos);
            this.groupBox2.Controls.Add(this.btbAtivar);
            this.groupBox2.Font = new System.Drawing.Font("Leelawadee UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(508, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(502, 531);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuários Inativos";
            // 
            // dataGridUsuariosInativos
            // 
            this.dataGridUsuariosInativos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUsuariosInativos.Location = new System.Drawing.Point(32, 32);
            this.dataGridUsuariosInativos.Name = "dataGridUsuariosInativos";
            this.dataGridUsuariosInativos.Size = new System.Drawing.Size(464, 412);
            this.dataGridUsuariosInativos.TabIndex = 68;
            // 
            // btbAtivar
            // 
            this.btbAtivar.BackColor = System.Drawing.Color.Black;
            this.btbAtivar.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btbAtivar.ForeColor = System.Drawing.SystemColors.Control;
            this.btbAtivar.Location = new System.Drawing.Point(327, 454);
            this.btbAtivar.Name = "btbAtivar";
            this.btbAtivar.Size = new System.Drawing.Size(169, 43);
            this.btbAtivar.TabIndex = 71;
            this.btbAtivar.Text = "Ativar Usuário";
            this.btbAtivar.UseVisualStyleBackColor = false;
            this.btbAtivar.Click += new System.EventHandler(this.btbAtivar_Click);
            // 
            // visualizarUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 729);
            this.Controls.Add(this.body_visualizarUser);
            this.Name = "visualizarUser";
            this.Text = "Menu de Funcionários e Usuários (Visualizar Usuário)";
            this.Load += new System.EventHandler(this.visualizarUser_Load);
            this.body_visualizarUser.ResumeLayout(false);
            this.body_visualizarUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsuariosAtivos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsuariosInativos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox body_visualizarUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridUsuariosAtivos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridUsuariosInativos;
        private System.Windows.Forms.Button btbAtivar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDesativar;
    }
}