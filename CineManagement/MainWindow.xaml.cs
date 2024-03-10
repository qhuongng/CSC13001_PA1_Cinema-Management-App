using System.IO;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;


namespace CineManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Actor actor;
        public MainWindow()
        {   
            InitializeComponent();
            ActorManage actorManage = new ActorManage();
            actor = actorManage.getActorById(1);
            System.Windows.MessageBox.Show(actor.actorName);
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = ImageTransfer.ByteArrayToImage(actor.avatar);
            Form form = new Form();
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            form.Controls.Add(pictureBox1);
            form.ShowDialog();
        }
    }
    public class ImageTransfer()
    {
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
    }
}