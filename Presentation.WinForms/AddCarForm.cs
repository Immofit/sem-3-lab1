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
        /// <summary>
        /// Инициализирует новый экземпляр формы добавления/изменения машины.
        /// </summary>
        public AddCarForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Применить": проверяет заполненность полей
        /// и, если данные валидны, закрывает форму с результатом DialogResult.OK.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
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

        /// <summary>
        /// Возвращает значения, введённые пользователем в полях формы.
        /// </summary>
        /// <returns>Кортеж с брендом, моделью, цветом и годом машины.</returns>
        public (string brand, string model, string color, int year) GetValues()
        {
            return (brandBox.Text, modelBox.Text, colorBox.Text, (int)yearBox.Value);
        }
    }
}
