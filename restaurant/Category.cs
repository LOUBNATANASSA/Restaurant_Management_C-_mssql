using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace restaurant
{
    public partial class Category : Form

    {
        Functions Con;
        

        public Category()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCategories();

            CategoriesList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CategoriesList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CategoriesList.ColumnHeadersVisible = true;
            
        }

        private void ShowCategories()
        {
            try
            {
               string Query = "select * from CategoryTb1";
               CategoriesList.DataSource = Con.GetData(Query);
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);

            }
        



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cname.Text == "" || cdesc.Text == "")
            {
                MessageBox.Show("MISSING DATA");
            }
            else
            {
                try
                {
                    string Category = cname.Text;
                    string Desc = cdesc.Text;
                    string Query = "INSERT INTO CategoryTb1 (CatName, CatDesc) VALUES (@Category, @Desc)";

                    int rowsAffected = Con.SetData(Query, Category, Desc);

                    if (rowsAffected > 0)
                    {
                        ShowCategories();
                        MessageBox.Show("Category added");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add category");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            // Vérifiez si une ligne est sélectionnée
            if (CategoriesList.SelectedRows.Count > 0)
            {
                try
                {
                    // Récupérez l'identifiant de la catégorie sélectionnée
                    int categoryId = Convert.ToInt32(CategoriesList.SelectedRows[0].Cells["CategoryIdColumnName"].Value);

                    // Construction de la requête de suppression
                    string query = "DELETE FROM CategoryTb1 WHERE CategoryId = @CategoryId";

                    // Exécution de la requête de suppression
                    int rowsAffected = Con.SetData(query, categoryId);

                    // Vérification du nombre de lignes affectées
                    if (rowsAffected > 0)
                    {
                        // Si la suppression réussit, actualisez l'affichage des catégories
                        ShowCategories();
                        MessageBox.Show("Category deleted");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete category");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete");
            }
        }



    }
}
