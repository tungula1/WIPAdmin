using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WebPageAdmin;

namespace WebPageAdmin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string VideoLink_Geo, VideoLink_Eng, VideoLink_Rus;
        List<string> images = new List<string>();

        string path = @"C:\Images\";


        //შენახვა
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                WebPageAdmin.DataConDataContext db = new DataConDataContext();
                WebPageAdmin.Item pIn = new Item();
                pIn.Name_Geo = Name_Geo.Text;
                pIn.Name_Eng = Name_Eng.Text;
                pIn.Name_Rus = Name_Rus.Text;

                pIn.Description_Geo = Desc_Geo.Text;
                pIn.Description_Eng = Desc_Eng.Text;
                pIn.Description_Rus = Desc_Rus.Text;

                pIn.VideoLink_Geo = VideoLink_Geo;
                pIn.VideoLink_Eng = VideoLink_Eng;
                pIn.VideoLink_Rus = VideoLink_Rus;


                pIn.CreateDate = DateTime.Now;

                db.Items.InsertOnSubmit(pIn);
                db.SubmitChanges();


                //SaveImages
                if (images != null && images.Count > 0)
                {
                    foreach (var item in images)
                    {
                        WebPageAdmin.ItemImage _image = new ItemImage()
                        {
                            ItemId = pIn.Id,
                            Url = item
                        };

                        db.ItemImages.InsertOnSubmit(_image);
                        db.SubmitChanges();
                    }
                }



                Name_Geo.Text = "";
                Name_Eng.Text = "";
                Name_Rus.Text = "";
                Desc_Geo.Text = "";
                Desc_Eng.Text = "";
                Desc_Rus.Text = "";
                VideoLink_Geo = "";
                VideoLink_Rus = "";
                VideoLink_Eng = "";

                MessageBox.Show("ოპერაცია წარმატებით დასრულდა");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        //Upload Rus Video
        private void button6_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;

                if (fileToOpen != "")
                {
                    var gID = Guid.NewGuid();
                    System.IO.File.Copy(fileToOpen, path + gID + Path.GetExtension(FD.SafeFileName));
                    VideoLink_Rus = path + path + gID + Path.GetExtension(FD.SafeFileName);
                }

            }
        }

        //Upload Geo Video
        private void button1_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;

                if (fileToOpen != "")
                {
                    var gID = Guid.NewGuid();
                    System.IO.File.Copy(fileToOpen, path + gID + Path.GetExtension(FD.SafeFileName));
                    VideoLink_Geo = path + path + gID + Path.GetExtension(FD.SafeFileName);
                }

            }
        }

        //Upload Eng Video
        private void button3_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;

                if (fileToOpen != "")
                {
                    var gID = Guid.NewGuid();
                    System.IO.File.Copy(fileToOpen, path + gID + Path.GetExtension(FD.SafeFileName));
                    VideoLink_Eng = path + path + gID + Path.GetExtension(FD.SafeFileName);
                }
            }
        }



        //Upload Images
        private void button5_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            FD.Multiselect = true;

            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (FD.FileNames != null)
                {
                    foreach (var item in FD.FileNames)
                    {
                        if (item != "")
                        {
                            var gID = Guid.NewGuid();
                            System.IO.File.Copy(item, path + gID + Path.GetExtension(item));
                            images.Add(path + gID + Path.GetExtension(item));
                        }
                    }
                }
            }
        }

    }
}

