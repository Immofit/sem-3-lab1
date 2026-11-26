using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared
{
    /// <summary>
    /// Объект передачи данных о машине между слоями View, Presenter/ViewModel и BusinessLogic.
    /// Реализует INotifyPropertyChanged для автоматического обновления интерфейса при изменении полей.
    /// </summary>
    public class CarDto : INotifyPropertyChanged
    {
        private int id;
        private string brand = "";
        private string model = "";
        private string color = "";
        private int year;
        private int mileage;
        private bool windowTinting;

        /// <summary>
        /// Возникает при изменении значения любого свойства автомобиля.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Уникальный идентификатор автомобиля.
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Бренд автомобиля.
        /// </summary>
        public string Brand
        {
            get { return brand; }
            set
            {
                if (brand != value)
                {
                    brand = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Модель автомобиля.
        /// </summary>
        public string Model
        {
            get { return model; }
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Цвет кузова автомобиля.
        /// </summary>
        public string Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Год выпуска автомобиля.
        /// </summary>
        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Пробег автомобиля в километрах.
        /// </summary>
        public int Mileage
        {
            get { return mileage; }
            set
            {
                if (mileage != value)
                {
                    mileage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Признак наличия тонировки окон.
        /// </summary>
        public bool WindowTinting
        {
            get { return windowTinting; }
            set
            {
                if (windowTinting != value)
                {
                    windowTinting = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Уведомляет подписчиков об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Возвращает строковое представление машины для вывода на экран.
        /// </summary>
        public override string ToString()
        {
            return "Id=" + Id + " | " + Brand + " " + Model + " | Цвет: " + Color + " | " + Year + " год | Пробег: " + Mileage + " км | Тонировка: " + WindowTinting;
        }
    }
}