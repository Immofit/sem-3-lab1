namespace Presentation.WinForms
{
    partial class Form1
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
            Table = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Brand = new DataGridViewTextBoxColumn();
            Model = new DataGridViewTextBoxColumn();
            Color = new DataGridViewTextBoxColumn();
            Year = new DataGridViewTextBoxColumn();
            GetCars = new Button();
            DeleteCar = new Button();
            CreateCar = new Button();
            ((System.ComponentModel.ISupportInitialize)Table).BeginInit();
            SuspendLayout();
            // 
            // Table
            // 
            Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Table.Columns.AddRange(new DataGridViewColumn[] { ID, Brand, Model, Color, Year });
            Table.Location = new Point(276, 44);
            Table.Name = "Table";
            Table.Size = new Size(512, 394);
            Table.TabIndex = 0;
            // 
            // ID
            // 
            ID.DataPropertyName = "Id";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.Width = 30;
            // 
            // Brand
            // 
            Brand.DataPropertyName = "Brand";
            Brand.HeaderText = "Brand";
            Brand.Name = "Brand";
            Brand.Width = 110;
            // 
            // Model
            // 
            Model.DataPropertyName = "Model";
            Model.HeaderText = "Model";
            Model.Name = "Model";
            Model.Width = 110;
            // 
            // Color
            // 
            Color.DataPropertyName = "Color";
            Color.HeaderText = "Color";
            Color.Name = "Color";
            Color.Width = 110;
            // 
            // Year
            // 
            Year.DataPropertyName = "Year";
            Year.HeaderText = "Year";
            Year.Name = "Year";
            Year.Width = 110;
            // 
            // GetCars
            // 
            GetCars.Location = new Point(12, 415);
            GetCars.Name = "GetCars";
            GetCars.Size = new Size(247, 23);
            GetCars.TabIndex = 1;
            GetCars.Text = "Получить все машины";
            GetCars.UseVisualStyleBackColor = true;
            GetCars.Click += GetCars_Click;
            // 
            // DeleteCar
            // 
            DeleteCar.Location = new Point(141, 377);
            DeleteCar.Name = "DeleteCar";
            DeleteCar.Size = new Size(118, 23);
            DeleteCar.TabIndex = 3;
            DeleteCar.Text = "Удалить машину";
            DeleteCar.UseVisualStyleBackColor = true;
            DeleteCar.Click += DeleteCar_Click;
            // 
            // CreateCar
            // 
            CreateCar.Location = new Point(12, 377);
            CreateCar.Name = "CreateCar";
            CreateCar.Size = new Size(123, 23);
            CreateCar.TabIndex = 4;
            CreateCar.Text = "Создать машину";
            CreateCar.UseVisualStyleBackColor = true;
            CreateCar.Click += CreateCar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CreateCar);
            Controls.Add(DeleteCar);
            Controls.Add(GetCars);
            Controls.Add(Table);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Table).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView Table;
        private Button GetCars;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Brand;
        private DataGridViewTextBoxColumn Model;
        private DataGridViewTextBoxColumn Color;
        private DataGridViewTextBoxColumn Year;
        private Button DeleteCar;
        private Button CreateCar;
    }
}
