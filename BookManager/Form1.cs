using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BookManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "도서관 관리";

            lblTotalBooks.Text = DataManager.Books.Count.ToString();
            lblTotalUsers.Text = DataManager.Users.Count.ToString();
            lblTotalRent.Text = DataManager.Books.Where((x) => x.isBorrowed).Count().ToString();
            lblTotalOverdue.Text = DataManager.Books.Where((x) => { return x.isBorrowed && x.BorrowedAt.AddDays(7) < DateTime.Now; }).Count().ToString();

            dataGridView2.DataSource = DataManager.Books;
            dataGridView1.DataSource = DataManager.Users;
            dataGridView2.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.CurrentCellChanged += DataGridView2_CurrentCellChanged;

            btnBorrow.Click += Button1_Click;
            btnReturn.Click += Button2_Click;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Book book = dataGridView2.CurrentRow.DataBoundItem as Book;
                tbIsbn.Text = book.Isbn;
                tbBookName.Text = book.Name;
            }
            catch(Exception exception)
            {

            }
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User book = dataGridView1.CurrentRow.DataBoundItem as User;
                tbUserId.Text = book.Id.ToString();
            }
            catch (Exception exception)
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (tbIsbn.Text.Trim() == "")
            {
                MessageBox.Show("ISBN을 입력해주세요.");
            }
            else if (tbUserId.Text.Trim() == "")
            {
                MessageBox.Show("사용자 ID를 입력해주세요.");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.Isbn == tbIsbn.Text);
                    if (book.isBorrowed)
                    {
                        MessageBox.Show("이미 대여 중인 도서입니다.");
                    }
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == tbUserId.Text);
                        book.UserId = user.Id;
                        book.UserName = user.Name;
                        book.isBorrowed = true;
                        book.BorrowedAt = DateTime.Now;

                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = DataManager.Books;
                        DataManager.Save();

                        MessageBox.Show("\"" + book.Name + "\"이/가\"" + user.Name + "\"님께 대여되었습니다.");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서 또는 사용자 입니다.");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (tbIsbn.Text.Trim() == "")
            {
                MessageBox.Show("ISBN을 입력해주세요.");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.Isbn == tbIsbn.Text);
                    if (book.isBorrowed)
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == tbUserId.Text);
                        book.UserId = 0;
                        book.UserName = "";
                        book.isBorrowed = false;
                        book.BorrowedAt = new DateTime();

                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = DataManager.Books;
                        DataManager.Save();

                        if (book.BorrowedAt.AddDays(7) > DateTime.Now)
                        {
                            MessageBox.Show("\"" + book.Name + "\"이/가 연체 상태로 반납되었습니다.");
                        }
                        else
                        {
                            MessageBox.Show("\"" + book.Name + "\"이/가 반납되었습니다.");

                        }
                    }
                    else
                    {
                        MessageBox.Show("대여 상태가 아닙니다.");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서 또는 사용자 입니다.");
                }
            }
        }



        private void lblTotalBooks_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 도서관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBook().ShowDialog();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormUser().ShowDialog();
        }
    }
}
