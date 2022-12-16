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
using System.Data.SqlClient;
using System.Data;

namespace yiy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int pageNumber = 1;
        string tt = null;

        public static int end;
        public SqlConnection con = new SqlConnection(@"Data source=DESKTOP-OSVGS4N\SQLEXPRESS01;database=ABDTHREE;Trusted_Connection=true");
        public SqlCommand com = new SqlCommand();
        int i = 1;
        SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
            com.Connection = con;
        }
        
        private void FirstPage_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            i -= 3;
         
            com.CommandText = $"select Title from Agent where Id='{i}'";
            SqlCommand com1 = new SqlCommand($"select Title from Product where Id='{i + 1}'", con);
            SqlCommand com2 = new SqlCommand($"select Title from Product where Id='{i + 2}'", con);
            SqlCommand com3 = new SqlCommand($"select Title from Product where Id='{i + 3}'", con);



            string ex1 = (string)com1.ExecuteScalar();
            string ex2 = (string)com2.ExecuteScalar();
            string ex3 = (string)com3.ExecuteScalar();
            if (i == 1)
                FirstPage.IsEnabled = false;
            else
                FirstPage.IsEnabled = true;
            Title1.Text = ex3;
            Title2.Text = ex1;
            Title3.Text = ex2;
    
            con.Close();


        }
        private void LastPage_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand tcom = new SqlCommand($"Select Title from Material,ProductMaterial where ID=ProductMaterial.MaterialID and ProductMaterial.ProductID='{pageNumber}'", con);
            SqlDataReader treader;
            treader = tcom.ExecuteReader();
            while (treader.Read())
            {
                tt += treader["Title"] + ", ";
            }
            treader.Close();
            com.CommandText = $"Select Title, ArticleNumber, ID,ProductTypeID,(Select Title from ProductType Where ID=ProductTypeID) as ProductType from Product Where ID='{pageNumber}'";
            SqlCommand com1 = new SqlCommand($"SELECT ProductMaterial.ProductID, SUM(Material.Cost * ProductMaterial.Count) AS Stoimost FROM Product, Material, " +
                $"ProductMaterial WHERE ProductMaterial.ProductID = Product.ID AND Material.ID = ProductMaterial.MaterialID AND Product.ID='{pageNumber}' group by ProductMaterial.ProductID", con);
            SqlDataReader sreader;
            sreader = com1.ExecuteReader();
            while (sreader.Read())
            {
                object stoimo = sreader["stoimost"];
                UNA1.Content = stoimo;
            }
            sreader.Close();
            reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Object Product = reader["Title"];
                    Object ProductType = reader["ProductType"];
                    Title1.Text = Convert.ToString(ProductType) + " | " + Convert.ToString(Product);   
                }
            }
            con.Close();
            con.Open();

            SqlCommand tcom1 = new SqlCommand($"Select Title from Material,ProductMaterial where ID=ProductMaterial.MaterialID and ProductMaterial.ProductID='{pageNumber + 1}'", con);
            SqlDataReader treader1;
            treader1 = tcom.ExecuteReader();
            while (treader1.Read())
            {
                tt += treader1["Title"] + ", ";
            }
            treader1.Close();
            com.CommandText = $"Select Title, ArticleNumber, ID,ProductTypeID,(Select Title from ProductType Where ID=ProductTypeID) as ProductType from Product Where ID='{pageNumber + 1}'";
            SqlCommand com11 = new SqlCommand($"SELECT ProductMaterial.ProductID, SUM(Material.Cost * ProductMaterial.Count)" +
                $" AS Stoimost FROM Product, Material, ProductMaterial WHERE ProductMaterial.ProductID = Product.ID AND Material.ID = ProductMaterial.MaterialID " +
                $"AND Product.ID='{pageNumber + 1}' group by ProductMaterial.ProductID", con);

            sreader = com11.ExecuteReader();
            while (sreader.Read())
            {
                object stoimo = sreader["stoimost"];
                UNA1.Content = stoimo;
            }
            sreader.Close();
            reader = com.ExecuteReader();
          
            con.Close();
            con.Open();

            SqlCommand tcom2 = new SqlCommand($"Select Title from Material,ProductMaterial where ID=ProductMaterial.MaterialID and ProductMaterial.ProductID='{pageNumber + 2}'", con);
            SqlDataReader treader2;
            treader2 = tcom.ExecuteReader();
            while (treader2.Read())
            {
                tt += treader2["Title"] + ", ";
            }
            treader2.Close();
            com.CommandText = $"Select Title, ArticleNumber, ID,ProductTypeID,(Select Title from ProductType Where ID=ProductTypeID) as ProductType from Product Where ID='{pageNumber + 2}'";
            SqlCommand com12 = new SqlCommand($"SELECT ProductMaterial.ProductID, SUM(Material.Cost * ProductMaterial.Count) AS Stoimost" +
                $" FROM Product, Material, ProductMaterial WHERE ProductMaterial.ProductID = Product.ID AND Material.ID = ProductMaterial.MaterialID AND" +
                $" Product.ID='{pageNumber + 2}' group by ProductMaterial.ProductID", con);
            SqlDataReader sreader2;
            sreader2 = com12.ExecuteReader();
            while (sreader2.Read())
            {
                object stoimo = sreader2["stoimost"];
                UNA2.Content = stoimo;
            }
            sreader2.Close();
            reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Object Product = reader["Title"];
                    Object ProductType = reader["ProductType"];
                    Title2.Text = Convert.ToString(ProductType) + " | " + Convert.ToString(Product);                

                }
            }

            con.Close();
            con.Open();

            SqlCommand tcom3 = new SqlCommand($"Select Title from Material,ProductMaterial where ID=ProductMaterial.MaterialID and ProductMaterial.ProductID='{pageNumber + 3}'", con);
            SqlDataReader treader3;
            treader3 = tcom3.ExecuteReader();
            while (treader3.Read())
            {
                tt += treader3["Title"] + ", ";
            }
            treader3.Close();
            com.CommandText = $"Select Title, ArticleNumber, ID,ProductTypeID,(Select Title from ProductType Where ID=ProductTypeID) as ProductType from Product Where ID='{pageNumber + 3}'";
            SqlCommand com13 = new SqlCommand($"SELECT ProductMaterial.ProductID, SUM(Material.Cost * ProductMaterial.Count) AS Stoimost " +
                $"FROM Product, Material, ProductMaterial WHERE ProductMaterial.ProductID = Product.ID AND" +
                $" Material.ID = ProductMaterial.MaterialID AND Product.ID='{pageNumber + 3}' group by ProductMaterial.ProductID", con);
            SqlDataReader sreader3;
            sreader3 = com13.ExecuteReader();
            while (sreader3.Read())
            {
                object stoimo = sreader3["stoimost"];
                UNA3.Content = stoimo;
            }
            sreader3.Close();
            reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Object Product = reader["Title"];
                    Object ProductType = reader["ProductType"];
                    Title3.Text = Convert.ToString(ProductType) + " | " + Convert.ToString(Product);
                }
            }

            con.Close();
            pageNumber += 4;

        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Robert_Polson.Text = "";
        }
    }

}
