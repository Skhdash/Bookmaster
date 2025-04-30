using Bookmaster.AppData;
using Bookmaster.Model;
using Bookmaster.View.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bookmaster.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BrowseCatalogPage.xaml
    /// </summary>
    public partial class BrowseCatalogPage : Page
    {
        List<Book> _books = App.context.Book.ToList();
        PaginationService _booksPagination;
        public BrowseCatalogPage()
        {
            InitializeComponent();
        }


        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchResultsGrid.Visibility = Visibility.Visible;

            if (string.IsNullOrEmpty(SearchByBookTitleTb.Text) && string.IsNullOrEmpty(SearchByAuthorNameTb.Text) && string.IsNullOrEmpty(SearchByBookSubjectTb.Text))
            {
                _booksPagination = new PaginationService(_books);
            }
            else
            {
                List<Book> searhResults = _books.Where(book => book.Title.ToLower().Contains(SearchByBookSubjectTb.Text.ToLower()) && book.Authors.ToLower().Contains(SearchByBookSubjectTb.Text.ToLower())).ToList();

                // Реализуем алгоритм поиска
                _booksPagination = new PaginationService(searhResults);
            }

            // Загружаем данные из таблицы BookAuthor в список List View
            BookAuthorLv.ItemsSource = _booksPagination.CurrentPageOfBooks;
            TotalPagesTbl.DataContext = TotalBooksTbl.DataContext = _booksPagination;
            CurrentPageTb.Text = _booksPagination.CurrentPageNumber.ToString();
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void PreviousBookBtn_Click(object sender, RoutedEventArgs e)
        {
            BookAuthorLv.ItemsSource = _booksPagination.PreviousPage();
            CurrentPageTb.Text = _booksPagination.CurrentPageNumber.ToString();
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void NextBookBtn_Click(object sender, RoutedEventArgs e)
        {
            BookAuthorLv.ItemsSource = _booksPagination.NextPage();
            CurrentPageTb.Text = _booksPagination.CurrentPageNumber.ToString();
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void CurrentPageTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(CurrentPageTb.Text, out int pageNumber) && pageNumber >= 1 && pageNumber <= _booksPagination.TotalPages)
            {
                BookAuthorLv.ItemsSource = _booksPagination.SetCurrentPage(pageNumber);
            }
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void BookAuthorLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selectedBook = BookAuthorLv.SelectedItem as Book;
            BookDetailsGrid.DataContext = selectedBook;
        }

        private void PreviousCovertBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextCovertBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AuthorsDetailsHl_Click(object sender, RoutedEventArgs e)
        {
            BookAuthorsDetailsWindow _bookAuthorsDetailsWindow = new BookAuthorsDetailsWindow();
            _bookAuthorsDetailsWindow.ShowDialog();
        }
    }
}
