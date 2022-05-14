namespace TestesDaMariana.WinApp.ModuloTeste
{
    partial class TelaCadastroTeste
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTituloTeste = new System.Windows.Forms.TextBox();
            this.comboBoxDisciplina = new System.Windows.Forms.ComboBox();
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.numQuestoes = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSortearQuestoes = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxQuestoesSorteadas = new System.Windows.Forms.GroupBox();
            this.listBoxQuestoes = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestoes)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBoxQuestoesSorteadas.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Título: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Disciplina: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Matéria: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Quantidade de Questões: ";
            // 
            // txtTituloTeste
            // 
            this.txtTituloTeste.Location = new System.Drawing.Point(93, 48);
            this.txtTituloTeste.Name = "txtTituloTeste";
            this.txtTituloTeste.Size = new System.Drawing.Size(273, 23);
            this.txtTituloTeste.TabIndex = 4;
            // 
            // comboBoxDisciplina
            // 
            this.comboBoxDisciplina.FormattingEnabled = true;
            this.comboBoxDisciplina.Location = new System.Drawing.Point(93, 87);
            this.comboBoxDisciplina.Name = "comboBoxDisciplina";
            this.comboBoxDisciplina.Size = new System.Drawing.Size(273, 23);
            this.comboBoxDisciplina.TabIndex = 5;
            this.comboBoxDisciplina.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplina_SelectedIndexChanged);
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Location = new System.Drawing.Point(93, 128);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(273, 23);
            this.comboBoxMateria.TabIndex = 6;
            // 
            // numQuestoes
            // 
            this.numQuestoes.Location = new System.Drawing.Point(171, 174);
            this.numQuestoes.Name = "numQuestoes";
            this.numQuestoes.Size = new System.Drawing.Size(51, 23);
            this.numQuestoes.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(341, 429);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(260, 429);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(75, 30);
            this.btnGravar.TabIndex = 9;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnSortearQuestoes
            // 
            this.btnSortearQuestoes.Location = new System.Drawing.Point(138, 429);
            this.btnSortearQuestoes.Name = "btnSortearQuestoes";
            this.btnSortearQuestoes.Size = new System.Drawing.Size(116, 30);
            this.btnSortearQuestoes.TabIndex = 11;
            this.btnSortearQuestoes.Text = "Sortear Questões";
            this.btnSortearQuestoes.UseVisualStyleBackColor = true;
            this.btnSortearQuestoes.Click += new System.EventHandler(this.btnSortearQuestoes_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 35);
            this.panel2.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Gerador de Testes Aleatórios";
            // 
            // groupBoxQuestoesSorteadas
            // 
            this.groupBoxQuestoesSorteadas.Controls.Add(this.listBoxQuestoes);
            this.groupBoxQuestoesSorteadas.Location = new System.Drawing.Point(21, 205);
            this.groupBoxQuestoesSorteadas.Name = "groupBoxQuestoesSorteadas";
            this.groupBoxQuestoesSorteadas.Size = new System.Drawing.Size(395, 218);
            this.groupBoxQuestoesSorteadas.TabIndex = 13;
            this.groupBoxQuestoesSorteadas.TabStop = false;
            this.groupBoxQuestoesSorteadas.Text = "Questões Sorteadas";
            // 
            // listBoxQuestoes
            // 
            this.listBoxQuestoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxQuestoes.FormattingEnabled = true;
            this.listBoxQuestoes.ItemHeight = 15;
            this.listBoxQuestoes.Location = new System.Drawing.Point(3, 19);
            this.listBoxQuestoes.Name = "listBoxQuestoes";
            this.listBoxQuestoes.Size = new System.Drawing.Size(389, 196);
            this.listBoxQuestoes.TabIndex = 0;
            // 
            // TelaCadastroTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 471);
            this.Controls.Add(this.groupBoxQuestoesSorteadas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSortearQuestoes);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.numQuestoes);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.comboBoxDisciplina);
            this.Controls.Add(this.txtTituloTeste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TelaCadastroTeste";
            ((System.ComponentModel.ISupportInitialize)(this.numQuestoes)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBoxQuestoesSorteadas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTituloTeste;
        private System.Windows.Forms.ComboBox comboBoxDisciplina;
        private System.Windows.Forms.ComboBox comboBoxMateria;
        private System.Windows.Forms.NumericUpDown numQuestoes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSortearQuestoes;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxQuestoesSorteadas;
        private System.Windows.Forms.ListBox listBoxQuestoes;
    }
}