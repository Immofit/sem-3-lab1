using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;

namespace Presentation.WinForms
{
    public partial class AddCarForm : Form
    {
        public Car CreatedCar { get; private set; }


        public AddCarForm()
        {
            InitializeComponent();
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(brandBox.Text) ||
                string.IsNullOrWhiteSpace(modelBox.Text) ||
                string.IsNullOrWhiteSpace(colorBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            
            DialogResult = DialogResult.OK;
            Tag = new
            {
                Brand = brandBox.Text,
                Model = modelBox.Text,
                Color = colorBox.Text,
                Year = (int)yearBox.Value,
            };
            Close();
        }

        public (string brand, string model, string color, int year) GetValues()
        {
            return (brandBox.Text, modelBox.Text, colorBox.Text, (int)yearBox.Value);
        }
    }
}
