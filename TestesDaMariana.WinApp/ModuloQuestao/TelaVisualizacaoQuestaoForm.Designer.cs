namespace TestesDaMariana.WinApp.ModuloQuestao
{
    partial class TelaVisualizacaoQuestaoForm
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
            this.checkedListBoxAlternativas = new System.Windows.Forms.CheckedListBox();
            this.txtDisciplina = new System.Windows.Forms.TextBox();
            this.txtMateria = new System.Windows.Forms.TextBox();
            this.txtEnunciado = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Disciplina: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Matéria: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Enunciado: ";
            // 
            // checkedListBoxAlternativas
            // 
            this.checkedListBoxAlternativas.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxAlternativas.Enabled = false;
            this.checkedListBoxAlternativas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkedListBoxAlternativas.FormattingEnabled = true;
            this.checkedListBoxAlternativas.Location = new System.Drawing.Point(32, 172);
            this.checkedListBoxAlternativas.Name = "checkedListBoxAlternativas";
            this.checkedListBoxAlternativas.Size = new System.Drawing.Size(479, 292);
            this.checkedListBoxAlternativas.TabIndex = 3;
            // 
            // txtDisciplina
            // 
            this.txtDisciplina.Enabled = false;
            this.txtDisciplina.Location = new System.Drawing.Point(93, 24);
            this.txtDisciplina.Name = "txtDisciplina";
            this.txtDisciplina.Size = new System.Drawing.Size(147, 23);
            this.txtDisciplina.TabIndex = 4;
            // 
            // txtMateria
            // 
            this.txtMateria.Enabled = false;
            this.txtMateria.Location = new System.Drawing.Point(327, 24);
            this.txtMateria.Name = "txtMateria";
            this.txtMateria.Size = new System.Drawing.Size(184, 23);
            this.txtMateria.TabIndex = 5;
            // 
            // txtEnunciado
            // 
            this.txtEnunciado.Enabled = false;
            this.txtEnunciado.Location = new System.Drawing.Point(107, 91);
            this.txtEnunciado.Multiline = true;
            this.txtEnunciado.Name = "txtEnunciado";
            this.txtEnunciado.Size = new System.Drawing.Size(384, 44);
            this.txtEnunciado.TabIndex = 6;
            // 
            // TelaVisualizacaoQuestaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 498);
            this.Controls.Add(this.txtEnunciado);
            this.Controls.Add(this.txtMateria);
            this.Controls.Add(this.txtDisciplina);
            this.Controls.Add(this.checkedListBoxAlternativas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaVisualizacaoQuestaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizar Questão";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxAlternativas;
        private System.Windows.Forms.TextBox txtDisciplina;
        private System.Windows.Forms.TextBox txtMateria;
        private System.Windows.Forms.TextBox txtEnunciado;
    }
}