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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Table = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Brand = new DataGridViewTextBoxColumn();
            Model = new DataGridViewTextBoxColumn();
            Color = new DataGridViewTextBoxColumn();
            Year = new DataGridViewTextBoxColumn();
            Mileage = new DataGridViewTextBoxColumn();
            WindowTinting = new DataGridViewTextBoxColumn();
            GetCars = new Button();
            DeleteCar = new Button();
            CreateCar = new Button();
            UpdateCar = new Button();
            CarsYear = new Button();
            ColorCar = new Button();
            Secret = new Button();
            MileageDown = new Button();
            TiningSet = new Button();
            pictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)Table).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // Table
            // 
            Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Table.Columns.AddRange(new DataGridViewColumn[] { ID, Brand, Model, Color, Year, Mileage, WindowTinting });
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
            Brand.Width = 70;
            // 
            // Model
            // 
            Model.DataPropertyName = "Model";
            Model.HeaderText = "Model";
            Model.Name = "Model";
            Model.Width = 70;
            // 
            // Color
            // 
            Color.DataPropertyName = "Color";
            Color.HeaderText = "Color";
            Color.Name = "Color";
            Color.Width = 70;
            // 
            // Year
            // 
            Year.DataPropertyName = "Year";
            Year.HeaderText = "Year";
            Year.Name = "Year";
            Year.Width = 70;
            // 
            // Mileage
            // 
            Mileage.DataPropertyName = "Mileage";
            Mileage.HeaderText = "Mileage";
            Mileage.Name = "Mileage";
            Mileage.Width = 70;
            // 
            // WindowTinting
            // 
            WindowTinting.DataPropertyName = "WindowTinting";
            WindowTinting.HeaderText = "WindowTinting";
            WindowTinting.Name = "WindowTinting";
            WindowTinting.Width = 90;
            // 
            // GetCars
            // 
            GetCars.Location = new Point(12, 256);
            GetCars.Name = "GetCars";
            GetCars.Size = new Size(247, 23);
            GetCars.TabIndex = 1;
            GetCars.Text = "Получить все машины";
            GetCars.UseVisualStyleBackColor = true;
            GetCars.Click += GetCars_Click;
            // 
            // DeleteCar
            // 
            DeleteCar.Location = new Point(141, 227);
            DeleteCar.Name = "DeleteCar";
            DeleteCar.Size = new Size(118, 23);
            DeleteCar.TabIndex = 3;
            DeleteCar.Text = "Удалить машину";
            DeleteCar.UseVisualStyleBackColor = true;
            DeleteCar.Click += DeleteCar_Click;
            // 
            // CreateCar
            // 
            CreateCar.Location = new Point(12, 227);
            CreateCar.Name = "CreateCar";
            CreateCar.Size = new Size(123, 23);
            CreateCar.TabIndex = 4;
            CreateCar.Text = "Создать машину";
            CreateCar.UseVisualStyleBackColor = true;
            CreateCar.Click += CreateCar_Click;
            // 
            // UpdateCar
            // 
            UpdateCar.Location = new Point(12, 198);
            UpdateCar.Name = "UpdateCar";
            UpdateCar.Size = new Size(247, 23);
            UpdateCar.TabIndex = 5;
            UpdateCar.Text = "Изменить машину";
            UpdateCar.UseVisualStyleBackColor = true;
            UpdateCar.Click += UpdateCar_Click;
            // 
            // CarsYear
            // 
            CarsYear.Location = new Point(12, 169);
            CarsYear.Name = "CarsYear";
            CarsYear.Size = new Size(247, 23);
            CarsYear.TabIndex = 6;
            CarsYear.Text = "Показать машины по году";
            CarsYear.UseVisualStyleBackColor = true;
            CarsYear.Click += CarAge_Click;
            // 
            // ColorCar
            // 
            ColorCar.Location = new Point(12, 140);
            ColorCar.Name = "ColorCar";
            ColorCar.Size = new Size(247, 23);
            ColorCar.TabIndex = 7;
            ColorCar.Text = "Показать машину по цвету";
            ColorCar.UseVisualStyleBackColor = true;
            ColorCar.Click += ColorCar_Click;
            // 
            // Secret
            // 
            Secret.Location = new Point(12, 324);
            Secret.Name = "Secret";
            Secret.Size = new Size(247, 23);
            Secret.TabIndex = 8;
            Secret.Text = "Секретные функции";
            Secret.UseVisualStyleBackColor = true;
            Secret.Click += Secret_Click;
            // 
            // MileageDown
            // 
            MileageDown.Location = new Point(12, 353);
            MileageDown.Name = "MileageDown";
            MileageDown.Size = new Size(247, 23);
            MileageDown.TabIndex = 9;
            MileageDown.Text = "Скрутить пробег";
            MileageDown.UseVisualStyleBackColor = true;
            MileageDown.Visible = false;
            // 
            // TiningSet
            // 
            TiningSet.Location = new Point(12, 382);
            TiningSet.Name = "TiningSet";
            TiningSet.Size = new Size(247, 23);
            TiningSet.TabIndex = 10;
            TiningSet.Text = "Поставить тонировку";
            TiningSet.UseVisualStyleBackColor = true;
            TiningSet.Visible = false;
            // 
            // pictureBox
            // 
            pictureBox.Image = (Image)resources.GetObject("pictureBox.Image");
            pictureBox.Location = new Point(12, 44);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(247, 90);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 11;
            pictureBox.TabStop = false;
            pictureBox.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox);
            Controls.Add(TiningSet);
            Controls.Add(MileageDown);
            Controls.Add(Secret);
            Controls.Add(ColorCar);
            Controls.Add(CarsYear);
            Controls.Add(UpdateCar);
            Controls.Add(CreateCar);
            Controls.Add(DeleteCar);
            Controls.Add(GetCars);
            Controls.Add(Table);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Table).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView Table;
        private Button GetCars;
        private Button DeleteCar;
        private Button CreateCar;
        private Button UpdateCar;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Brand;
        private DataGridViewTextBoxColumn Model;
        private DataGridViewTextBoxColumn Color;
        private DataGridViewTextBoxColumn Year;
        private DataGridViewTextBoxColumn Mileage;
        private DataGridViewTextBoxColumn WindowTinting;
        private Button CarsYear;
        private Button ColorCar;
        private Button Secret;
        private Button MileageDown;
        private Button TiningSet;
        private PictureBox pictureBox;
    }
}
