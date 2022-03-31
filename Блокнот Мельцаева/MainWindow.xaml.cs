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
using System.Windows.Threading;


namespace Notebook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bloknot bloknot;
        public MainWindow()
        {
            InitializeComponent();
            bloknot = new Bloknot(richtextbox);
        }
        DispatcherTimer timer;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.IsEnabled = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            time.Text = d.ToString("HH:mm:ss");
            data.Text = d.ToString("dd:MM:yyyy");
        }
        private void Создать_Click(object sender, RoutedEventArgs e)
        {
            bloknot.Create();
            this.Title = bloknot.NameFile;
        }

        private void richtextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bloknot.Modified = true;
            find.IsEnabled = true;
        }

        private void Открыть_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;
            }
            if (bloknot.Open() == false) return;
            this.Title = bloknot.NameFile;

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
           bloknot.ASaveBloknot();
            this.Title = bloknot.NameFile;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;
                if (result == MessageBoxResult.No) this.Close();    
            }
            else this.Close();
        }

        private void StatusBar_Click(object sender, RoutedEventArgs e)
        {
            if(status.Visibility == Visibility.Visible)
            {
                status.Visibility = Visibility.Collapsed;
                richtextbox.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                status.Visibility = Visibility.Visible;
                richtextbox.Margin = new Thickness(0, 0, 0, 22);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(bloknot.NameFile=="")
            {
                bloknot.ASaveBloknot();
                this.Title = bloknot.NameFile;
            }
            else
            {
                bloknot.Save();
            }
            this.Title = bloknot.NameFile;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.Undo();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.Cut();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.Paste();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.Selection.Text = "";
        }

        private void AllSelect_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.SelectAll();
        }
        private void Standart_Click(object sender, RoutedEventArgs e)
        {
            richtextbox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            richtextbox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
        }
        private void Fat_Click(object sender, RoutedEventArgs e)
        {

            richtextbox.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);

        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {

            richtextbox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        private void UnderLine_Click(object sender, RoutedEventArgs e)
        {
            if (richtextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) != TextDecorations.Underline)
            {
                richtextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);

            }
            else
            {
                richtextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            Find find = new Find(richtextbox);
            find.Show();
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            Replace replace = new Replace(richtextbox);
            replace.Show();
        }

        private void richtextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

       
        private void Новое_окно_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
        }

        private void О_программе_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработала студентка группы ИСП-31 Мельцаева Дарья\nРазработанная программа обеспечивает:\n1. Ввод и редактирование текста.\n2.Сохранение текста.Название сохраненного текста должно выводиться в строке заголовке.При повторном сохранении, имя файла не запрашивать.\n3.Сохранение с новым именем.\n4.Загрузка текста из файла.\n5.Завершение работы с программой.При завершении если текст не сохранен, запроситьсохранение.\n6.Создание нового документа.\n7.Справка о программе.\n8.Реализовать действия с буфером обмена.\n9.Реализовать отмену последнего действия.\n10.Реализовать контекстное меню для действий с буфером обмена.\n11. Изменения шрифта текста. Поиск и замена","О программе", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
