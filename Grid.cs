using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hoyle_VTT
{
    [Serializable()]
    public class Grid
    {
        //variables
        protected int grid_rows,grid_columns,image_width,image_height;
        protected PictureBox stored_map;

        protected Bitmap grid_bit;

        public Graphics my_graphics;
        public Pen gridpen = new Pen(Color.Black);
        public PaintEventArgs paint_Event_saved;
        public GraphicsState clear;
        

        //initilizers
        public Grid()
        {
            grid_rows = 10;
            grid_columns = 10;
        }
        public Grid(int grid_rows_in, int grid_columns_in, PictureBox stored_map_in)
        {
            set_grid_rows(grid_rows_in);
            set_grid_columns(grid_columns_in);
            set_grid_picturebox(stored_map_in);
            //grid_rows = grid_rows_in;
            //grid_columns = grid_columns_in;
            //stored_map = stored_map_in;
        }
        public Grid(int grid_rows_in, int grid_columns_in, PictureBox stored_map_in,Graphics in_graphic,Pen in_pen)
        {
            set_grid_rows(grid_rows_in);
            set_grid_columns(grid_columns_in);
            set_grid_picturebox(stored_map_in);
            //grid_rows = grid_rows_in;
            //grid_columns = grid_columns_in;
            //stored_map = stored_map_in;
            my_graphics = in_graphic;
            gridpen = in_pen;

        }
        public Grid(int grid_rows_in, int grid_columns_in, PictureBox stored_map_in, Graphics in_graphic)
        {
            set_grid_rows(grid_rows_in);
            set_grid_columns(grid_columns_in);
            set_grid_picturebox(stored_map_in);
            //grid_rows = grid_rows_in;
            //grid_columns = grid_columns_in;
            //stored_map = stored_map_in;
            my_graphics = in_graphic;
            //gridpen = in_pen;

        }

        //sets
        public void set_grid_size(int number_width, int number_height) {
            //Console.WriteLine("setting width to"+number_width);
            set_grid_columns(number_width);
            //Console.WriteLine("setting height to" + number_height);
            set_grid_rows(number_height);
            //Console.WriteLine("Both should now be set to "+ number_width+" and "+ number_height+ " respectively.");

        }
        public void set_grid_columns(int number_width)
        {
            grid_columns = number_width;
        }
        public void set_grid_rows(int number_height)
        {
            grid_rows = number_height;
        }
        public void set_grid_picturebox(PictureBox pictureBox_in)
        {
            stored_map = pictureBox_in;
            //stored_map.BackColor = Color.Red;

            image_width = stored_map.Width;
            image_height = stored_map.Height;
            //my_graphics= stored_map.CreateGraphics();
            //clear = my_graphics.Save();
        }
        public void set_gridbox_width(int width)
        {
            image_width=width;
        }
        public void set_gridbox_height(int height)
        {
            image_height = height;
        }

        //gets
        public int get_grid_rows()
        {
            return grid_rows;
        }
        public int get_grid_columns()
        {
            return grid_columns;
        }
        public PictureBox get_grid_picturebox()
        {
            return stored_map;
        }

        public List<int> get_square_size()
        {
            List<int> square_size = new List<int>();

            int square_width = (image_width/grid_columns);
            int square_height= (image_height/grid_rows) ;
            int pen_width = (int)gridpen.Width;

            square_size.Add(square_height);
            square_size.Add(square_width);
            square_size.Add(pen_width);


            //square_size.Add(-1);
            //square_size.Add(-1);
            return square_size;
        }

        //Grid Drawing
        public void draw_grid(PaintEventArgs paint_Event)
        {
            paint_Event_saved = paint_Event;
            my_graphics = paint_Event.Graphics;
            //Graphics my_graphics = my_grid.get_graphics();
            //Pen gridpen = my_grid.get_pen();
            //my_graphics.Clear(Color.Transparent);
            draw_grid();
        }
        public void draw_grid()
        {
            if (stored_map == null)
            {
                Console.WriteLine("no map. no where to build the grid.");

                return;
            }
            if (paint_Event_saved == null)
            {
                Console.WriteLine("no painting event.");

                return;
            }
            if (grid_bit != null)
            {
                grid_bit.Dispose();
            }
            //stored_map.BackColor = Color.Blue;
            grid_bit =new Bitmap(stored_map.Width, stored_map.Height);
            my_graphics = paint_Event_saved.Graphics;
            int cell_size_y = 0;
            int cell_size_X = 0;

            //Console.WriteLine("getting the grid stuff");

            int height_cells = grid_rows;
            //Console.WriteLine("Grid height said to be " + height_cells);

            int width_cells = grid_columns;
            //Console.WriteLine("Grid width said to be " + width_cells);

            if (height_cells != 0)
            {
                float cell_size_y_float = stored_map.Height / height_cells;
                cell_size_y = (int)Math.Ceiling(cell_size_y_float);
            }
            else
            {
                Console.WriteLine("oops! error! height size is 0");
                grid_bit.Dispose();

                return;
            }
            if (width_cells != 0)
            {
                float cell_size_X_float = stored_map.Width / width_cells;

                cell_size_X = (int)Math.Ceiling(cell_size_X_float);
            }
            else
            {
                Console.WriteLine("oops! error! width size is 0");
                grid_bit.Dispose();

                return;
            }

            //using (Graphics g = Graphics.FromImage(grid_bit))
            //{
                for (int y = 0; y < (height_cells+1); ++y)
                {
                    int y_first = y * cell_size_y;
                    //int height_first = height_cells * cell_size_y;
                    int width_first= stored_map.Width;
                    int y_second = y * cell_size_y;
                    //Console.WriteLine($"Valus for the grid are {y_first},{width_first}, and {y_second}. The Cell size y is {cell_size_y} there are {height_cells} height cells.\nThe graphics is {my_graphics} and the pen is {gridpen}");
                    my_graphics.DrawLine(gridpen, 0, y_first, width_first, y_second);
                    //g.DrawLine(gridpen, 0, y_first, width_first, y_second);

                }

                for (int x = 0; x < (width_cells + 1); ++x)
                {
                    int x_first = x * cell_size_X;
                    int height_first = stored_map.Height;

                    int x_second = x * cell_size_X;


                    my_graphics.DrawLine(gridpen, x_first, 0, x_second, height_first);
                    //g.DrawLine(gridpen, x_first, 0, x_second, height_first);

                }
            //}
            //print_grid();
        }



        //print
        public void print_grid()
        {
            Console.WriteLine("grid X" + grid_columns + " Grid Y: " + grid_rows + " grid picture box: " + stored_map); 
            return;}
        

    }
}
//public void draw_grid(bool setup)
//{
//    //paint_Event_saved = paint_Event;


//    //my_graphics.Restore();
//    //Graphics my_graphics = my_graphics.;
//    //Pen gridpen = my_grid.get_pen();


//    Console.WriteLine("attempting to draw grid");
//    if (setup == true | my_graphics == null)
//    {



//    }
//    if (stored_map == null)
//    {
//        Console.WriteLine("no map. no where to build the grid.");

//        return;
//    }
//    if (paint_Event_saved == null)
//    {
//        Console.WriteLine("no painting event.");

//        return;
//    }
//    if (my_graphics != null)
//    {
//        //my_graphics.Dispose();
//    }
//    //stored_map.Paint += new System.Windows.Forms.PaintEventHandler(this.grid_picture_Box_paint);
//    //this.Controls.Add(stored_map);

//    my_graphics = stored_map.CreateGraphics();
//    gridpen=new Pen(Color.Black);
//    //my_graphics.Restore(clear);
//    my_graphics = paint_Event_saved.Graphics;


//    //my_graphics.Clear(Color.White);
//    //my_graphics;

//    my_graphics = paint_Event_saved.Graphics;
//    int cell_size_y = 0;
//    int cell_size_X = 0;

//    Console.WriteLine("getting the grid stuff");

//    int height_cells = grid_rows;
//    Console.WriteLine("Grid height said to be " + height_cells);

//    int width_cells = grid_columns;
//    Console.WriteLine("Grid width said to be " + width_cells);

//    if (height_cells != 0)
//    {
//        cell_size_y = stored_map.Height / height_cells;
//    }
//    else
//    {
//        Console.WriteLine("oops! error! height size is 0");
//        return;
//    }
//    if (width_cells != 0)
//    {
//        cell_size_X = stored_map.Width / width_cells;
//    }
//    else
//    {
//        Console.WriteLine("oops! error! width size is 0");

//        return;
//    }
//    for (int y = 0; y < height_cells; ++y)
//    {
//        int y_first = y * cell_size_y;
//        int height_first = height_cells * cell_size_y;
//        int y_second = y * cell_size_y;
//        Console.WriteLine($"Valus for the grid are {y_first},{height_first}, and {y_second}. The Cell size y is {cell_size_y} there are {height_cells} height cells.\nThe graphics is {my_graphics} and the pen is {gridpen}");
//        my_graphics.DrawLine(gridpen, 0, y_first, height_first, y_second);
//    }

//    for (int x = 0; x < grid_columns; ++x)
//    {
//        my_graphics.DrawLine(gridpen, x * cell_size_X, 0, x * cell_size_X, cell_size_X * cell_size_X);
//    }
//}

//public void draw_grid(bool setup)
//{
//    Graphics  my_graphics = my_grid.get_graphics();
//    Pen gridpen = my_grid.get_pen();

//    Console.WriteLine("attempting to draw grid");
//    if (setup == true | my_graphics==null) { 

//    //my_graphics.Clear(Color.White);
//    //my_graphics;
//    Console.WriteLine("created the graphics box with "+grid_picture_Box);
//    }

//    int cell_size_y = 0;
//    int cell_size_X = 0;

//    Console.WriteLine("getting the grid stuff");

//    int height_cells = my_grid.get_grid_rows();
//    Console.WriteLine("Grid height said to be " + height_cells);

//    int width_cells = my_grid.get_grid_columns();
//    Console.WriteLine("Grid width said to be " + width_cells);

//    if (height_cells != 0)
//    {
//         cell_size_y = grid_picture_Box.Height / height_cells;
//    }
//    else
//    {
//        Console.WriteLine("oops! error! height size is 0");
//        return;
//    }
//    if (width_cells != 0)
//    {
//         cell_size_X = grid_picture_Box.Width / width_cells;
//    }
//    else
//    {
//        Console.WriteLine("oops! error! width size is 0");

//        return;
//    }
//    for (int y = 0; y < height_cells; ++y)
//    {
//        my_graphics.DrawLine(gridpen, 0, y * cell_size_y, height_cells * cell_size_y, y * cell_size_y);
//    }

//    for (int x = 0; x < my_grid.get_grid_columns(); ++x)
//    {
//        my_graphics.DrawLine(gridpen, x * cell_size_X, 0, x * cell_size_X, cell_size_X * cell_size_X);
//    }
//}