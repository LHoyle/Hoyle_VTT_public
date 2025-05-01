using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hoyle_VTT
{
    //Controls for VTT tokens.
    //Arrow keys for up down left right. Shift for down, Spacebar for up.

    //Token sizes- 0:tiny, 1: small, 2:medium,3:large,4:huge,5:collosal
    [Serializable()]
    public class Token
    {
        
        public string name { get; set; }
        public Bitmap Icon { get; set; }
        public PictureBox physical_token { get; set; }
        public Bitmap token_displayed;
        public int X_coord{get;set;}
        public int Y_coord{get;set;}
        public int Z_coord{get;set;}
        public int token_meta_size{get; set;}
        public Color token_color { get; set; }

        public Token(string name_in, PictureBox token_in)
        {
            //Console.WriteLine($"Initializing token {name} (no positions)");

            name = name_in;
            physical_token=token_in;
            token_meta_size = 2;
            //Console.WriteLine($"finished token {name}");
            token_color=random_color();

            //Random random = new Random();
            //double doubleRed = random.NextDouble();
            //double doubleGreen = random.NextDouble();
            //double doubleblue = random.NextDouble();
            //int red = (int)(255 / doubleRed);
            //int Green = (int)(255 / doubleRed);
            //int blue = (int)(255 / doubleRed);
            //token_color = Color.FromArgb(0, red,Green,blue);

            //token_color.MakeArgb(0, 255 / Red, 255 / Green, 255 / blue) = ;
        }
        public Token(string name_in, PictureBox token_in, int xpos, int ypos,  int zpos)
        //public Token(string name_in, PictureBox token_in,int ypos, int xpos, int zpos)
        {
            //Console.WriteLine($"Initializing token {name}");

            name = name_in;
            physical_token = token_in;
            X_coord = xpos;
            Y_coord = ypos;
            Z_coord = zpos;
            token_meta_size = 2;
            //Console.WriteLine($"finished token {name}");
            token_color=random_color();

        }
        public Token(string name_in, PictureBox token_in, int xpos, int ypos, int zpos,int size_in)
        {
            //Console.WriteLine($"Initializing token {name} with size {token_meta_size}");

            name = name_in;
            physical_token = token_in;
            X_coord = xpos;
            Y_coord = ypos;
            Z_coord = zpos;
            token_meta_size = size_in;
            //Console.WriteLine($"Initializing token {name}");

            token_color=random_color();

        }

        public Color random_color()
        {
            Random random = new Random();
            double doubleRed = random.NextDouble()%255;
            double doubleGreen = random.NextDouble() % 255;
            double doubleblue = random.NextDouble() % 255;
            int red = (int)(doubleRed*255);
            int Green = (int)(doubleGreen *255);
            int blue = (int)(doubleblue* 255);
            //Console.WriteLine($"red:{red} ({doubleRed}), green: {Green} ({doubleGreen}), Blue: {blue} ({doubleblue})");
            token_color = Color.FromArgb(255, red, Green, blue);
            //Console.WriteLine($"token color: {token_color}");
            return token_color;
        }

        public void display_Token(PaintEventArgs paint_Event,List<int> size)
        {

            Graphics my_graphics = paint_Event.Graphics;
            //Console.WriteLine($"graphics event{my_graphics}");
            //if (token_displayed != null)
            //{
            //    Console.WriteLine("token_displayed is not null");
            //    token_displayed.Dispose();
            //}


            //physical_token.Width = size[1]- (size[2]*2);
            //physical_token.Height = size[0] - (size[2]*2);
            //physical_token.Top = size[1] * X_coord;
            //physical_token.Left = size[0] * Y_coord;


            physical_token.Width = size[1] - (size[2] * 2);
            physical_token.Height = size[0] - (size[2] * 2);
            physical_token.Top= (size[0])* Y_coord;
            physical_token.Left= (size[1] )*X_coord;
            physical_token.BackColor = token_color;
            //Console.WriteLine($"width:{physical_token.Width}, Height: {physical_token.Height}, Top: {physical_token.Top}, Left: {physical_token.Left}");
            //token_displayed = new Bitmap(physical_token.Width, physical_token.Height, physical_token.Top, physical_token.Left);


            //token_displayed = new Bitmap(physical_token.Width, physical_token.Height, my_graphics);
            //token_displayed = new Bitmap(physical_token.Width, physical_token.Height);
            token_displayed = new Bitmap(physical_token.Height, physical_token.Width);

            //Console.WriteLine($"token to display:{token_displayed}");


            //token_displayed.

            //Console.WriteLine($"width:{size[1]},height:{size[0]},top:{X_coord},left:{Y_coord}");

            //Console.WriteLine($"physical_token rectangle, {physical_token.Top},{physical_token.Left},{physical_token.Width},{physical_token.Height}");
            //Console.WriteLine($"displaying token {name}");
            if (physical_token == null)
            {
                throw new Exception("somehow physical token is empty");
            }


            //using (Graphics g = Graphics.FromImage(token_displayed))
            //{
            //my_graphics.Clip = new Region(new Rectangle(physical_token.Top, physical_token.Left, physical_token.Width, physical_token.Height));
            //my_graphics.FillRectangle(new SolidBrush(token_color), new Rectangle(physical_token.Top, physical_token.Left, physical_token.Width, physical_token.Height));

            //}
            //my_graphics.Clip = new Region(new Rectangle(physical_token.Top, physical_token.Left, token_displayed.Width, token_displayed.Height));
            //my_graphics.FillRectangle(new SolidBrush(token_color), new Rectangle(physical_token.Top, physical_token.Left, token_displayed.Width, token_displayed.Height));

            paint_Event.Graphics.Clip = new Region(new Rectangle(physical_token.Top, physical_token.Left, token_displayed.Width, token_displayed.Height));
            paint_Event.Graphics.FillRectangle(new SolidBrush(token_color), new Rectangle(physical_token.Top, physical_token.Left, token_displayed.Width, token_displayed.Height));


            //Console.WriteLine($"display rectangle {physical_token.DisplayRectangle}, {physical_token.DisplayRectangle.Top},{physical_token.DisplayRectangle.Left},{physical_token.DisplayRectangle.Width},{physical_token.DisplayRectangle.Height}");
            //Console.WriteLine($"physical_token rectangle {physical_token}, {physical_token.Top},{physical_token.Left},{physical_token.Width},{physical_token.Height}");

            //physical_token.DisplayRectangle


            //paint_Event_saved = paint_Event;
            //Graphics my_graphics = paint_Event.Graphics;

            //my_graphics.Clear(Color.FromName("red"));

            //Region coloring_zone = new Region(physical_token.DisplayRectangle);
            //Console.WriteLine($"colornig zone: {coloring_zone},with a size of, {physical_token.DisplayRectangle.Size}");
            //Region clippy = coloring_zone;
            //= .displayRectangle;
            //SolidBrush bruush = new SolidBrush(Color.FromName("red"));
            //my_graphics.FillRegion(bruush, clippy);
            //paint_Event.Graphics.Clip = new Region(physical_token.DisplayRectangle);

            //paint_Event.Graphics.

            //paint_Event.Graphics.FillRegion(new SolidBrush(Color.FromName("red")), paint_Event.Graphics.Clip);



            //paint_Event.Graphics.DrawString("Outside of Clip", new Font("Arial",
            //    12.0F, FontStyle.Regular), Brushes.Blue, 0.0F, 0.0F);
            //my_graphics.FillEllipse(bruush, physical_token.DisplayRectangle);



            //my_graphics.Clear(random_color());
        }
        public void Invalidate()
        {
            //Console.WriteLine($"invalidating Token{name}");
            physical_token.Invalidate();
            if (token_displayed != null) { 
            token_displayed.Dispose();
            }

        }

    }
}
//class Token_old
//{
//    public string name;
//    public string Name
//    {
//        get { return name; }
//        set { name = value; }
//    }

//    public Image icon;
//    public Image Icon
//    {
//        get { return icon; }
//        set { icon = value; }
//    }

//    public float X_pos;
//    public float X_Pos
//    {
//        get { return X_pos; }
//        set { X_pos = value; }

//    }
//    public float Y_pos;
//    public float Y_Pos
//    {
//        get { return Y_pos; }
//        set { Y_pos = value; }

//    }

//    public string owner;
//    public string Owner
//    {
//        get { return owner; }
//        set { owner = value; }
//    }

//    public float width;
//    public float Width
//    {
//        get { return width; }
//        set { width = value; }

//    }
//    public float height;
//    public float Height
//    {
//        get { return height; }
//        set { height = value; }

//    }

//    public Token_old()
//    {
//    }
//}
