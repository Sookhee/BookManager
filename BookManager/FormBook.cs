using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    public partial class FormBook : Form
    {
        public FormBook()
        {
            InitializeComponent();
            Text = "도서 관리";

            dataGridView1.DataSource = DataManager.Books;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;

            btnInsert.Click += (sender, e) =>
            {
                try
                {
                    if (DataManager.Books.Exists((x) => x.Isbn == tbIsbn.Text))
                    {
                        MessageBox.Show("이미 존재하는 도서입니다.");
                    }
                    else
                    {
                        Book book = new Book()
                        {
                            Isbn = tbIsbn.Text,
                            Name = tbBookName.Text,
                            Publisher = tbPublisher.Text,
                            Page = int.Parse(tbPage.Text)

                        };
                        DataManager.Books.Add(book);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;
                        DataManager.Save();
                    }
                }
                catch (Exception exception)
                {

                }
            };

            btnUpdate.Click += (sender, e) =>
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.Isbn == tbIsbn.Text);
                    book.Name = tbBookName.Text;
                    book.Publisher = tbPublisher.Text;
                    book.Page = int.Parse(tbPage.Text);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Books;
                    DataManager.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서입니다.");
                }
            };

            btnDelete.Click += (sender, e) =>
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.Isbn == tbIsbn.Text);

                    DataManager.Books.Remove(book);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Books;
                    DataManager.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서입니다.");
                }
            };
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
                tbIsbn.Text = book.Isbn;
                tbBookName.Text = book.Name;
                tbPublisher.Text = book.Publisher;
                tbPage.Text = book.Page.ToString();
            }
            catch(Exception exception)
            {

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
