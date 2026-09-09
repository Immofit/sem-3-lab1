namespace Presentation.WinForms
{
    partial class AddCarForm
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
            brandLabel = new Label();
            brandBox = new TextBox();
            modelLabel = new Label();
            colorLabel = new Label();
            yearLabel = new Label();
            modelBox = new TextBox();
            colorBox = new TextBox();
            yearBox = new NumericUpDown();
            addButton = new Button();
            closeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)yearBox).BeginInit();
            SuspendLayout();
            // 
            // brandLabel
            // 
            brandLabel.AutoSize = true;
            brandLabel.Location = new Point(29, 45);
            brandLabel.Name = "brandLabel";
            brandLabel.Size = new Size(43, 15);
            brandLabel.TabIndex = 0;
            brandLabel.Text = "Бренд:";
            // 
            // brandBox
            // 
            brandBox.Location = new Point(122, 42);
            brandBox.Name = "brandBox";
            brandBox.Size = new Size(134, 23);
            brandBox.TabIndex = 1;
            // 
            // modelLabel
            // 
            modelLabel.AutoSize = true;
            modelLabel.Location = new Point(29, 74);
            modelLabel.Name = "modelLabel";
            modelLabel.Size = new Size(53, 15);
            modelLabel.TabIndex = 2;
            modelLabel.Text = "Модель:";
            // 
            // colorLabel
            // 
            colorLabel.AutoSize = true;
            colorLabel.Location = new Point(29, 103);
            colorLabel.Name = "colorLabel";
            colorLabel.Size = new Size(36, 15);
            colorLabel.TabIndex = 3;
            colorLabel.Text = "Цвет:";
            // 
            // yearLabel
            // 
            yearLabel.AutoSize = true;
            yearLabel.Location = new Point(29, 132);
            yearLabel.Name = "yearLabel";
            yearLabel.Size = new Size(29, 15);
            yearLabel.TabIndex = 5;
            yearLabel.Text = "Год:";
            // 
            // modelBox
            // 
            modelBox.Location = new Point(122, 71);
            modelBox.Name = "modelBox";
            modelBox.Size = new Size(134, 23);
            modelBox.TabIndex = 6;
            // 
            // colorBox
            // 
            colorBox.Location = new Point(122, 100);
            colorBox.Name = "colorBox";
            colorBox.Size = new Size(134, 23);
            colorBox.TabIndex = 7;
            // 
            // yearBox
            // 
            yearBox.Location = new Point(122, 130);
            yearBox.Maximum = new decimal(new int[] { 2026, 0, 0, 0 });
            yearBox.Name = "yearBox";
            yearBox.Size = new Size(134, 23);
            yearBox.TabIndex = 8;
            // 
            // addButton
            // 
            addButton.Location = new Point(29, 216);
            addButton.Name = "addButton";
            addButton.Size = new Size(75, 23);
            addButton.TabIndex = 10;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += AddButton_Click;
            // 
            // closeButton
            // 
            closeButton.DialogResult = DialogResult.Cancel;
            closeButton.Location = new Point(151, 218);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 11;
            closeButton.Text = "Закрыть";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // AddCarForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 281);
            Controls.Add(closeButton);
            Controls.Add(addButton);
            Controls.Add(yearBox);
            Controls.Add(colorBox);
            Controls.Add(modelBox);
            Controls.Add(yearLabel);
            Controls.Add(colorLabel);
            Controls.Add(modelLabel);
            Controls.Add(brandBox);
            Controls.Add(brandLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddCarForm";
            Text = "Добавить машину";
            ((System.ComponentModel.ISupportInitialize)yearBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label brandLabel;
        private TextBox brandBox;
        private Label modelLabel;
        private Label colorLabel;
        private Label yearLabel;
        private TextBox modelBox;
        private TextBox colorBox;
        private NumericUpDown yearBox;
        private Button addButton;
        private Button closeButton;
    }
}