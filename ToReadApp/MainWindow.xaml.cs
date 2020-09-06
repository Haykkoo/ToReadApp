using System;
using System.ComponentModel;
using System.Windows;
using ToReadApp.Models;
using ToReadApp.Services;

namespace ToReadApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<ToReadModel> _ToReadBookList;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\BookList.json";
        private FileIOService _fileIOService;
        public MainWindow() 
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            _fileIOService = new FileIOService(PATH);

            try
            {
                _ToReadBookList = _fileIOService.LoadData();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Close();
            }
            dgToReadList.ItemsSource = _ToReadBookList;
            _ToReadBookList.ListChanged += ToReadBookList_ListChanged;
        }

        private void ToReadBookList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    Close();

                }
                dgToReadList.ItemsSource = _ToReadBookList;
                _ToReadBookList.ListChanged += ToReadBookList_ListChanged;
            }

        }


    }
}
