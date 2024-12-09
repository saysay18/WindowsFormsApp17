using System.Windows.Forms;
using System.Xml;

public partial class NextForm : Form
{
    public NextForm(string id, string name, string fatherName, string email, string dob,
                    string studentClass, string phone, string gender, Image photo)
    {
        InitializeComponent();

        // Display the data in controls
        IDLabel.Text = id;
        NameLabel.Text = name;
        FatherNameLabel.Text = fatherName;
        EmailLabel.Text = email;
        DOBLabel.Text = dob;
        ClassLabel.Text = studentClass;
        PhoneLabel.Text = phone;
        GenderLabel.Text = gender;
        PictureBox.Image = photo;
    }
}

public class Image
{
}