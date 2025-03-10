using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LManagement
{
    public partial class BookItem : UserControl
    {
        private string connectionString = "Server=.;Database=nickgirl;Integrated Security=True";
        public event EventHandler Clicked;
        public Label lblTitle = new Label();
        public PictureBox picCover = new PictureBox();
        public BookItem(int bookId, string title, Image coverImage)
        {
            InitializeComponent();
            lblTitle.Text = title;
            picCover.Image = coverImage;

            this.Click += (s, e) => Clicked?.Invoke(this, e);
            picCover.Click += (s, e) => Clicked?.Invoke(this, e);
        }

    }
}
