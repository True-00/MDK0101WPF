using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace oop5.Elements
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1(object ItemData)
        {
            InitializeComponent();

            Classes.Shop ShopData = ItemData as Classes.Shop;
            tbName.Content = ShopData.Name;
            tbPrice.Content = "Цена: " + ShopData.Price;

            if (ItemData is Classes.Children)
            {
                Classes.Children ChildrenData = ItemData as Classes.Children;
                tbCcaracteristice.Content = "Возраст: " + ChildrenData.Age;
            }

            if (ItemData is Classes.Sport)
            {
                Classes.Sport SportData = ItemData as Classes.Sport;
                tbCcaracteristice.Content = "Размер: " + SportData.Size;
            }
        }
    }
}
