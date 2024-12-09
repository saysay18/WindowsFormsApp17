using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;

private void timer1_Tick(object sender, EventArgs e)
{
    BarcodeReader reader = new BarcodeReader();
    Result result = reader.Decode((Bitmap)pictureBox2.Image);
    try
    {
        string decoded = result?.Text.Trim();
        if (!string.IsNullOrEmpty(decoded))
        {
            // Populate the text boxes
            ID_text.Text = decoded;

            con.Open();
            MySqlCommand coman = new MySqlCommand();
            coman.Connection = con;
            coman.CommandText = "select * from registration_tb where ID Like'%" + ID_text.Text + "%'";
            MySqlDataReader dr = coman.ExecuteReader();
            if (dr.Read())
            {
                // Gather data from the database
                string name = dr["Name"].ToString();
                string fatherName = dr["FatherName"].ToString();
                string email = dr["EmailAddress"].ToString();
                string dob = dr["DateOfBirth"].ToString();
                string studentClass = dr["Class"].ToString();
                string phone = dr["PhoneNumber"].ToString();
                string gender = dr["Gender"].ToString();
                byte[] img = (byte[])dr["Photo"];

                // Convert photo to image
                MemoryStream ms = new MemoryStream(img);
                Image photo = Image.FromStream(ms);

                con.Close();

                // Stop the timer to prevent repeated scans
                timer1.Stop();

                // Pass data to the next form
                NextForm nextForm = new NextForm(decoded, name, fatherName, email, dob, studentClass, phone, gender, photo);
                nextForm.Show();
                this.Hide(); // Hide current form
            }
            else
            {
                con.Close();
                MessageBox.Show("No record found!");
            }
        }
    }
    catch (Exception ex)
    {
        con.Close();
        MessageBox.Show("Error: " + ex.Message);
    }
}
