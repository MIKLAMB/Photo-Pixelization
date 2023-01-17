using System.Windows.Forms;

namespace PhotoRes
{
    public partial class AAAAAAAAAAAH : Form
    {
        private List<Bitmap> _bitmaps=new List<Bitmap>();
        private Random _random = new Random();
        public AAAAAAAAAAAH()
        {
            InitializeComponent();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private/* async*/ void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                pictureBox1.Image = null;
                _bitmaps.Clear();
                var bitmap = new Bitmap(openFileDialog1.FileName);
                RunProcessing(bitmap);
               // await Task.Run(() => { RunProcessing(bitmap)});
            }
        }
        private void RunProcessing(Bitmap bitmap)
        {
            var pixels = GetPixels(bitmap);
            var pixelsInStep = (bitmap.Width * bitmap.Height) / 100;
            var currentPixelsSet = new List<Pixel>(pixels.Count - pixelsInStep);

            for (int i = 0; i < trackBar1.Maximum; i++)
            {
                for (int j = 0; j < pixelsInStep; j++)
                {
                    var index=_random.Next(pixels.Count);
                    currentPixelsSet.Add(pixels[index]);
                    pixels.RemoveAt(index);
                }
                var currentBitmap=new Bitmap(bitmap.Width, bitmap.Height);
                foreach (var pixel in currentPixelsSet)
                    currentBitmap.SetPixel(pixel.Point.X,pixel.Point.Y,pixel.Color);
                _bitmaps.Add(currentBitmap);
                Text = $"{i}%";
            }
            _bitmaps.Add(bitmap);

            
        }
        private List<Pixel> GetPixels(Bitmap bitmap)
        {
            var pixels = new List<Pixel>(bitmap.Width * bitmap.Height);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    pixels.Add(new Pixel()
                    {
                        Color = bitmap.GetPixel(x, y),
                        Point = new Point() {X=x,Y=y },
                    });
                }
            }
            return pixels;
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_bitmaps == null || _bitmaps.Count == 0)
            {
                return;
            }
            pictureBox1.Image = _bitmaps[trackBar1.Value - 1];
            Text = trackBar1.Value.ToString();

        }


        private void AAAAAAAAAAAH_Load(object sender, EventArgs e)
        {

        }
    }
}
