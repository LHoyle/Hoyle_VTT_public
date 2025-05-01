using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hoyle_VTT
{
    [Serializable()]
    public class Map 
    {
        //test
        //variables
        public Bitmap Map_image;
        public Grid Map_grid;
        public List<Token> Token_list=new List<Token>();

        //public string file_name;
        public string Map_name;
        public string file_name { get; set; }

        Token[,,] Location_storage;

        public int Calculated_height, Calculated_width, Elevation_up, Elevation_down, Elevation_total;
        public PictureBox Map_box;

        //getset variables
        public int Original_Height { get; set; }
        public int Original_Width { get; set; }
        public double Ratio { get; set; }
        public int Y_location { get; set; }
        public int X_location { get; set; }
        public int Elevation_ground {  get; set; }

        int grid_x_count,grid_y_count;
        //constant
        const int gridsize_default = 75;


        //initilization
        public Map(string name, Bitmap map_in) 
        {
            Map_name = name;
            Map_image = map_in;
            Map_box = new PictureBox();
            Map_box.Image = map_in;
            Map_box.Visible = false;
            Original_Height = Map_image.Height;
            Calculated_height=Original_Height;
            Original_Width = Map_image.Width;
            Calculated_width = Map_image.Width; 
            Y_location = 0;
            X_location = 0;
            grid_x_count = gridsize_default;
            grid_y_count = gridsize_default;

            Ratio = calculate_ratio();
            Map_grid = new Grid();
            initialize_grid();

            Elevation_up = 0;
            Elevation_down = 0;
            Elevation_total = 1;
            Elevation_ground = 0;


            //Location_storage = new Token[Calculated_width, Calculated_height, Elevation_total];
            map_matrix_initilization();
        }
        public Map(string name, Bitmap map_in, Grid map,PictureBox in_box)
        {
            Map_name = name;
            Map_image = map_in;
            Original_Height = map_in.Height;
            Original_Width = map_in.Width;

            Original_Width = Map_image.Width;
            Calculated_width = Map_image.Width;
            Y_location = 0;
            X_location = 0;

            Map_grid = map;
            Map_box = in_box;
            Ratio = calculate_ratio();
            Elevation_up = 0;
            Elevation_down = 0;
            Elevation_total = 1;
            Elevation_ground = 0;
            //initialize_grid();
            grid_x_count=gridsize_default;
            grid_y_count = gridsize_default;

            map_matrix_initilization();
        }
        public void initialize_grid()
        {
            float grid_width_float = Get_Calculated_width() / grid_x_count;
            float grid_height_float = Get_Calculated_height() / grid_y_count;

            int gridsize_width = (int)Math.Ceiling(grid_width_float);
            int gridsize_height = (int)Math.Ceiling(grid_height_float);
            Map_grid = new Grid(gridsize_width, gridsize_height, Map_box);

            //Console.WriteLine("grid: " + gridsize_width + "," + gridsize_height);

            //Console.WriteLine("printing grid");
            //Map_grid.print_grid();
            //Console.WriteLine("grid: " + Map_box.Location + " VTT: " + Map_box.Location);
        }
        public void map_matrix_initilization()
        {
            if (Location_storage != null)
            {
                map_matrix_convert();
                return;
            }
            if (Map_grid == null)
            {
                initialize_grid();
            }
            int x_max = Map_grid.get_grid_columns();
            int y_max = Map_grid.get_grid_rows();
            int z_max = Elevation_total;
            //((int)Math.Ceiling((double)Token_list.Count() / 3)
            x_max = Math.Max(1, x_max);
            y_max = Math.Max(1, y_max);
            z_max = Math.Max(1, z_max);
            int token_divided_by_3 = (int)Math.Ceiling((double)Token_list.Count() / 3);

            //x_max = Math.Max(x_max, (int)Math.Ceiling((double)Token_list.Count() / 3));
            //y_max = Math.Max(((int)Math.Ceiling((double)Token_list.Count() / 3)), y_max);
            //z_max = Math.Max(((int)Math.Ceiling((double)Token_list.Count() / 3)), z_max);

            z_max = Math.Max(z_max, (int)Math.Ceiling((double)Token_list.Count() / 3));
            y_max = Math.Max(((int)Math.Ceiling((double)Token_list.Count() / 3)), y_max);
            x_max = Math.Max(((int)Math.Ceiling((double)Token_list.Count() / 3)), x_max);


            //this is setting up the array with x_max and such. not creating a token!
            Location_storage = new Token[x_max, y_max, z_max];

            for (int x = 0; x < x_max; x++) {
                for (int y = 0; y < y_max; y++)
                {
                    for (int z = 0; z < z_max; z++) {
                        Location_storage[x, y, z] = null;
                    }
                }
            }
            return;
        }
        
        //Calculations
        public void calculate_grid(int gridsize_default_x,int gridsize_default_y)
        {
            float grid_width_float = Get_Calculated_width() / gridsize_default_x;
            float grid_height_float = Get_Calculated_height() / gridsize_default_y;

            int gridsize_width = (int)Math.Ceiling(grid_width_float);
            int gridsize_height = (int)Math.Ceiling(grid_height_float);
            Map_grid = new Grid(gridsize_width, gridsize_height, Map_box);

            //Console.WriteLine("grid: " + gridsize_width + "," + gridsize_height);

            //Console.WriteLine("printing grid");
            //Map_grid.print_grid();
            //Console.WriteLine("grid: " + Map_box.Location + " VTT: " + Map_box.Location);
            map_matrix_convert();
        }
        public double calculate_ratio()
        {
            //int origin_img_height = bitMap_image.Height;
            int height = Original_Height;
            //int origin_img_width = bitMap_image.Width;
            int width = Original_Width;
            Ratio = (double)Original_Height / (double)Original_Width;
            return Ratio;
        }


        //conversion
        public void map_matrix_convert()
        {
            if (Location_storage == null) {
                map_matrix_initilization();
                return;
            }
            if (Map_grid == null)
            {
                initialize_grid();
            }
            int x_max = Map_grid.get_grid_columns();
            int y_max = Map_grid.get_grid_rows();
            int z_max = Elevation_total;
            if (x_max <= 0 & y_max <= 0 & z_max <= 0)
            {
                //Console.WriteLine("grid size changed to 0,0,0. ignoring this change");
                return;
            }
            if (x_max <= 0 & y_max <= 0)
            {
                //Console.WriteLine("grid size changed to 0,0,0. ignoring this change");
                return;
            }
            if (Token_list.Count() != 2) { 
            x_max = Math.Max(1, x_max);
            y_max = Math.Max(1, y_max);
            z_max = Math.Max(1, z_max);
            }
            else
            {

                x_max = Math.Max(2, x_max);
                y_max = Math.Max(2, y_max);
                z_max = Math.Max(2, z_max);
            }
            int token_divided_by_3 = (int)Math.Ceiling((double)Token_list.Count());
            //int token_divided_by_3 = (int)Math.Ceiling((double)Token_list.Count()/3);

            //Console.WriteLine($"token_divided_by_3: {token_divided_by_3}");

            x_max = Math.Max(x_max, token_divided_by_3);
            y_max = Math.Max(token_divided_by_3, y_max);
            z_max = Math.Max(token_divided_by_3, z_max);

            Token[,,] Location_storage_new = new Token[x_max, y_max, z_max];
            for (int x = 0; x < x_max; x++)
            {
                for (int y = 0; y < y_max; y++)
                {
                    for (int z = 0; z < z_max; z++)
                    {
                        Location_storage_new[x, y, z] = null;
                    }
                }
            }

            int x_max_copy = Math.Min(Location_storage.GetLength(0), x_max);
            int y_max_copy = Math.Min(Location_storage.GetLength(1), y_max);
            int z_max_copy = Math.Min(Location_storage.GetLength(2), z_max);
            z_max_copy = Math.Min(Location_storage.GetLength(2), z_max);


            //Console.WriteLine($"Location_storage_new:[{Location_storage_new.GetLength(0)},{Location_storage_new.GetLength(1)},{Location_storage_new.GetLength(2)}],Location_storage:[{Location_storage.GetLength(0)},{Location_storage.GetLength(1)},{Location_storage.GetLength(2)}]");
            //Console.WriteLine($"x_max_copy:{x_max_copy},y_max_copy:{y_max_copy},z_max_copy:{z_max_copy}");
            for (int x = 0; x < Math.Min(Location_storage.GetLength(0), x_max); x++)
            {
                for (int y = 0; y < Math.Min(Location_storage.GetLength(1), y_max); y++)
                {
                    //for (int z = 0; z < z_max_copy-1; z++)
                    for (int z = 0; z < Math.Min(Location_storage.GetLength(2), z_max); z++)

                        {
                        //    Console.WriteLine($"min:{Math.Min(Location_storage.GetLength(2), z_max)},z_max_copy:{z_max_copy},z_max:{z_max},Location_storage.GetLength(2):{Location_storage.GetLength(2)}");

                        //Console.WriteLine($"Location_storage_new:[{Location_storage_new.GetLength(0)},{Location_storage_new.GetLength(1)},{Location_storage_new.GetLength(2)}],Location_storage:[{Location_storage.GetLength(0)},{Location_storage.GetLength(1)},{Location_storage.GetLength(2)}]");

                        //Console.WriteLine($"x_max_copy:{x_max_copy},y_max_copy:{y_max_copy},z_max_copy:{z_max_copy}");
                        //Console.Write($"x:{x}, y:{y}, z:{z}\n");
                        //Console.WriteLine($"Location_storage_new[x, y, z]{Location_storage_new[x, y, z]}<endline>");
                        //Console.WriteLine($"Location_storage[x, y, z]{Location_storage[x, y, z]} <endline>");

                        Location_storage_new[x, y, z] = Location_storage[x,y,z];
                    }
                }

            x_max_copy = Math.Max(1, x_max_copy);
            y_max_copy = Math.Max(1, y_max_copy);
            z_max_copy = Math.Max(1, z_max_copy);

            x_max_copy = Math.Max(x_max_copy, (int)Math.Ceiling((double)Token_list.Count() / 3));
            y_max_copy = Math.Max(((int)Math.Ceiling((double)Token_list.Count() / 3)), y_max_copy);
            z_max_copy = Math.Max(((int)Math.Ceiling((double)Token_list.Count() / 3)), z_max_copy);

            }
            Token[,,] third_time=verify_tokens_moved(Math.Min(Location_storage.GetLength(0), x_max), Math.Min(Location_storage.GetLength(1), y_max), Math.Min(Location_storage.GetLength(2), z_max), Location_storage_new);
            Location_storage = third_time;
            Map_grid.get_grid_picturebox().Invalidate();
        }

        public Token[,,] verify_tokens_moved(int x_max_copy, int y_max_copy, int z_max_copy, Token[,,] Location_storage_new)
        {
            //throw new Exception("unfinished");
            for (int i = 0; i < Token_list.Count(); i++)
            {
                int X_coord = Token_list[i].X_coord;
                int Y_coord = Token_list[i].Y_coord;
                int Z_coord = Token_list[i].Z_coord;
                int maximized_x = x_max_copy - 1;
                int maximized_y = y_max_copy - 1;
                int maximized_z = z_max_copy - 1;


                int new_x = -1;
                int new_y = -1;
                int new_z = -1;
                bool x_bad = false;
                bool y_bad = false;
                bool z_bad = false;
                bool fix = true;


                //bool x_bigger = Location_storage_new.GetLength(0) > Location_storage.GetLength(0);
                //bool y_bigger = Location_storage_new.GetLength(1) > Location_storage.GetLength(1);
                //bool z_bigger = Location_storage_new.GetLength(2) > Location_storage.GetLength(2);
                //if (x_bigger)
                //{
                //}
                x_max_copy = Math.Min(Location_storage.GetLength(0), maximized_x+1);
                y_max_copy = Math.Min(Location_storage.GetLength(1), maximized_y+1);
                z_max_copy = Math.Min(Location_storage.GetLength(2), maximized_z+1);


                if (X_coord >= x_max_copy)
                {
                    x_bad = true;
                    fix = false;

                }
                if (Y_coord >= y_max_copy)
                {
                    y_bad = true;
                    fix = false;

                }
                if (Z_coord >= z_max_copy)
                {
                    z_bad = true;
                    fix = false;

                }


                if (x_bad & y_bad & z_bad)
                {
                    if (Location_storage[maximized_x, maximized_y, maximized_z] == null)
                    {
                        //all three can be easily just put to the 'max' value
                        Token_list[i].X_coord = maximized_x;
                        Token_list[i].Y_coord = maximized_y;
                        Token_list[i].Z_coord = maximized_z;
                        Location_storage_new[maximized_x, maximized_y, maximized_z] = Token_list[i];
                        continue;
                    }
                    else
                    {
                        //the max position is now open. so we search one at a time. 

                        //starts by finding the next free X location
                        new_x = next_location_to_place_token__X(maximized_y, maximized_z, Location_storage_new);
                        if (new_x == -1 & x_max_copy - 1 >= 0)
                        {
                            //is hit when there is no X position found.
                            for (int med_x = maximized_x; med_x > 0; med_x = med_x - 1)
                            {
                                //starts the same search on Y
                                //Console.WriteLine("searching for Y on all_bad");
                                new_y = next_location_to_place_token__Y(med_x, maximized_z, Location_storage_new);
                                if (new_y == -1 & y_max_copy - 1 >= 0)
                                {
                                    //no Y found. 
                                    for (int med_y = y_max_copy; med_y > 0; med_y = med_y - 1)
                                    {
                                        //Searching under Z
                                        new_z = next_location_to_place_token__Z(med_x, med_y, Location_storage_new);
                                        if (new_z == -1 & z_max_copy - 1 >= 0)
                                        {
                                            //the 'next_location_to_place_token__Z' takes care of looking through the for loop
                                            //that means that there isn't anything else to do other than search another Y, so we use continue to go to the next Y
                                            continue;
                                        }
                                        else if (Location_storage[med_x, med_y, new_z] == null)
                                        {
                                            Token_list[i].X_coord = med_x;
                                            Token_list[i].Y_coord = med_y;
                                            Location_storage_new[med_x, med_y, new_z] = Token_list[i];
                                            fix = true;
                                            break;
                                        }
                                        //else {throw new Exception("space given is not free");}
                                    }
                                    if (fix == true) { break; }
                                }
                                else
                                {
                                    //y found and saved
                                    if (Location_storage[med_x, new_y, maximized_z] == null)
                                    {
                                        Token_list[i].X_coord = med_x;
                                        Token_list[i].Y_coord = new_y;
                                        Token_list[i].Z_coord = maximized_z;

                                        Location_storage_new[med_x, new_y, maximized_z] = Token_list[i];
                                        fix = true;
                                        break;
                                    }
                                    //else{throw new Exception("space given is not free");}
                                }
                                //if (fix == true) { break; }
                            }
                            if (fix == false) { throw new Exception("no space available for X"); }
                            else { continue; }
                        }
                        else if (Location_storage[new_x, maximized_y, maximized_z] == null)
                        {
                            Token_list[i].X_coord = new_x;
                            Token_list[i].Y_coord = maximized_y;
                            Token_list[i].Z_coord = maximized_z;
                            Location_storage_new[new_x, maximized_y, maximized_z] = Token_list[i];
                            fix = true;
                            continue;
                        }
                        else{throw new Exception("no space available");}
                    }
                }

                if (x_bad)
                    {
                    //X as bad with Y or Z, not And Z.
                    if (y_bad)
                    {
                        //x and y bad
                        //Console.WriteLine($"new_x:{new_x},maximized_y:{maximized_y},Z_coord:{Z_coord},Location_storage_new:[{Location_storage_new.GetLength(0)},{Location_storage_new.GetLength(1)},{Location_storage_new.GetLength(2)}]");
                        new_x = next_location_to_place_token__X(maximized_y, Z_coord, Location_storage_new);
                        if (new_x == -1 & maximized_x - 1 >= 0)
                        {
                            //x not free on Y coord
                            for (int med_x = maximized_x; med_x > 0; med_x = med_x - 1)
                            {
                                //Console.WriteLine("searching for Y on x_Bad and y_bad");
                                new_y = next_location_to_place_token__Y(med_x, Z_coord, Location_storage_new);
                                if (new_y == -1 & y_max_copy - 1 >= 0)
                                {
                                    //this is a continue, because Z is 'good'
                                    continue;
                                }
                                else if (Location_storage[med_x, new_y, new_z] == null)
                                {
                                    Token_list[i].X_coord = med_x;
                                    Token_list[i].Y_coord = new_y;
                                    Location_storage_new[med_x, new_y, new_z] = Token_list[i];
                                    fix = true;
                                    break;
                                }

                                //else {throw new Exception("space given is not free");}
                            }
                            if (fix == true) { continue; }
                        }

                        else if (Location_storage[new_x, maximized_y, Z_coord] == null)
                        {
                            Token_list[i].X_coord = new_x;
                            Token_list[i].Y_coord = maximized_y;
                            //Console.WriteLine($"new_x:{new_x},maximized_y:{maximized_y},Z_coord:{Z_coord},Location_storage_new:[{Location_storage_new.GetLength(0)},{Location_storage_new.GetLength(1)},{Location_storage_new.GetLength(2)}]");
                            Location_storage_new[new_x, maximized_y, Z_coord] = Token_list[i];
                            fix = true;
                            continue;
                        }
                    }
                    if (z_bad)
                    {
                        //x and z bad
                        new_x = next_location_to_place_token__X(Y_coord, maximized_z, Location_storage_new);
                        if (new_x == -1 & maximized_x - 1 >= 0)
                        {
                            //x not free on Z coord
                            for (int med_x = maximized_x; med_x > 0; med_x = med_x - 1)
                            {
                                new_z = next_location_to_place_token__Z(med_x, Y_coord, Location_storage_new);
                                if (new_z == -1 & maximized_z - 1 >= 0)
                                {
                                    //this is a continue, because Z is unable to find one and the new_z takes care of looking for it in the x and y coords
                                    //thus no need to loop on z
                                    continue;
                                }
                                else if (Location_storage[med_x, Y_coord, new_z] == null)
                                {
                                    Token_list[i].X_coord = med_x;
                                    Token_list[i].Z_coord = new_z;
                                    Location_storage_new[med_x, Y_coord, new_z] = Token_list[i];
                                    fix = true;
                                    break;
                                }

                                //else {throw new Exception("space given is not free");}
                            }
                            if (fix == true) { continue; }
                            else {throw new Exception("space given is not free");}
                        }
                        else if (Location_storage[new_x, new_y, new_z] == null)
                        {
                            //new x is good
                            Token_list[i].X_coord = new_x;
                            Token_list[i].Y_coord = new_y;
                            Location_storage_new[new_x, new_y, new_z] = Token_list[i];
                            fix = true;
                            continue;
                        }
                    }
                    else
                    {
                        //only X bad
                        new_x = next_location_to_place_token__X(Y_coord, Z_coord, Location_storage_new);
                        if (new_x == -1 & maximized_x - 1 >= 0) { throw new Exception("no space"); }
                        if (maximized_x - 1 >= 0) {  }
                        else if (Location_storage[new_x, Y_coord, Z_coord] == null)
                        {
                            //new x is good
                            Token_list[i].X_coord = new_x;
                            Location_storage_new[new_x, Z_coord, Z_coord] = Token_list[i];
                            fix = true;
                            continue;
                        }
                        else { throw new Exception("space given is not free"); }
                    }

                    }

                if (y_bad)
                    {
                        if (z_bad)
                        {
                            // Y and Z bad.
                            for (int med_y = maximized_y; med_y > 0; med_y = med_y - 1)
                            {
                                new_z = next_location_to_place_token__Z(X_coord, med_y, Location_storage_new);
                                if (new_z == -1 & maximized_z - 1 >= 0) { continue; }
                                else if (Location_storage[X_coord, med_y, new_z] == null)
                                {
                                    Token_list[i].X_coord = X_coord;
                                    Token_list[i].Y_coord = med_y;
                                    Location_storage_new[X_coord, med_y, new_z] = Token_list[i];
                                    fix = true;
                                    break;
                                }
                                //else {throw new Exception("space given is not free");}
                            }
                            if (fix== true) { continue;}
                            else {throw new Exception("space given is not free");}

                        }
                        else
                        {
                        //only Y bad
                        //Console.WriteLine("searching for Y on Y_bad");

                        new_y = next_location_to_place_token__Y(X_coord, Z_coord, Location_storage_new);
                            //Console.WriteLine($"new_y:{new_y},y_max_copy{y_max_copy}");
                            if (new_y == -1 & y_max_copy - 1 >= 0){
                                //Console.WriteLine($"X_coord:{X_coord},new_y:{new_y},Z_coord:{Z_coord},Location_storage_new:[{Location_storage_new.GetLength(0)},{Location_storage_new.GetLength(1)},{Location_storage_new.GetLength(2)}]");
                                for (int med_y = maximized_y; med_y >= 0; med_y = med_y - 1)
                                {
                                    //Searching under X to see if X has more space
                                    new_x = next_location_to_place_token__X(med_y, Z_coord, Location_storage_new);
                                    if (new_x == -1 & x_max_copy - 1 >= 0)
                                    {
                                        //the 'next_location_to_place_token__Z' takes care of looking through the for loop
                                        //that means that there isn't anything else to do other than search another Y, so we use continue to go to the next Y
                                        continue;
                                    }
                                    else if (Location_storage[new_x, med_y, Z_coord] == null)
                                    {
                                        Token_list[i].X_coord = new_x;
                                        Token_list[i].Y_coord = med_y;
                                        Location_storage_new[new_x, med_y, Z_coord] = Token_list[i];
                                        fix = true;
                                        break;
                                    }
                                    //else {throw new Exception("space given is not free");}
                                }
                                if (fix == true)
                                {
                                    continue;
                                }
                            }
                            else if (Location_storage[X_coord, new_y, Z_coord] == null)
                            {
                                Token_list[i].Y_coord = new_y;
                                //Console.WriteLine($"X_coord:{X_coord},new_y:{new_y},Z_coord:{Z_coord},Location_storage_new:[{Location_storage_new.GetLength(0)},{Location_storage_new.GetLength(1)},{Location_storage_new.GetLength(2)}]");

                                Location_storage_new[X_coord, new_y, Z_coord] = Token_list[i];
                                fix = true;
                                continue;
                            }
                            else {throw new Exception("space given is not free");}
                        }
                    }

                if (z_bad)
                    {
                        new_z = next_location_to_place_token__Z(X_coord, Y_coord, Location_storage_new);
                        if (new_z == -1 & maximized_z - 1 >= 0)
                        {
                            throw new Exception("no space");
                        }
                        else if (Location_storage[X_coord, Y_coord, new_z] == null)
                        {
                            Token_list[i].X_coord = X_coord;
                            Token_list[i].Y_coord = Y_coord;
                            Location_storage_new[X_coord, Y_coord, new_z] = Token_list[i];
                            continue;
                        }
                        else
                        {
                            throw new Exception("space given is not free");
                        }
                    }



                    //if (X_coord > x_max_copy)
                    //{
                    //    if (Y_coord > y_max_copy)
                    //    {
                    //        if (Z_coord > z_max_copy)
                    //        {
                    //            if (Location_storage[x_max_copy, y_max_copy, z_max_copy] == null)
                    //            {
                    //                Token_list[i].Z_coord = z_max_copy;
                    //            }
                    //            else
                    //            {
                    //                new_z = next_location_to_place_token__Z(x_max_copy, y_max_copy);
                    //                if (new_z == -1)
                    //                {
                    //                    throw new Exception("no space for token in Z");
                    //                }
                    //                else
                    //                {
                    //                    Token_list[i].Z_coord = new_z;
                    //                    continue;
                    //                }
                    //            }
                    //            new_y = next_location_to_place_token__Y(X_coord, Z_coord);
                    //            if (new_y == -1)
                    //            {
                    //                throw new Exception("no space for token in Y");
                    //            }
                    //            else
                    //            {
                    //                Token_list[i].Y_coord = new_y;
                    //                continue;
                    //            }
                    //        }
                    //        new_x = next_location_to_place_token__X(Y_coord, Z_coord);
                    //        if (new_y == -1)
                    //        {
                    //            throw new Exception("no space for token in X");
                    //        }
                    //        else
                    //        {
                    //            Token_list[i].X_coord = new_x;
                    //            continue;
                    //        }
                    //    }
                    //}
                    //else if (Token_list[i].Y_coord > y_max_copy)
                    //{

                    //    if (Token_list[i].Z_coord > z_max_copy)
                    //    {
                    //        if (Location_storage[X_coord, y_max_copy, z_max_copy] == null)
                    //        {
                    //            Token_list[i].Z_coord = z_max_copy;
                    //        }
                    //        else
                    //        {
                    //            new_z = next_location_to_place_token__Z(X_coord, y_max_copy);
                    //            if (new_z == -1)
                    //            {
                    //                throw new Exception("no space for token in Z");
                    //            }
                    //            else
                    //            {
                    //                Token_list[i].Z_coord = new_z;
                    //                continue;
                    //            }
                    //        }
                    //    }
                    //}
                    //else if (Token_list[i].Z_coord > z_max_copy)
                    //{
                    //    if (Location_storage[X_coord, Y_coord, z_max_copy] == null)
                    //    {
                    //        Token_list[i].Z_coord = z_max_copy;
                    //    }
                    //    else
                    //    {
                    //        new_z = next_location_to_place_token__Z(X_coord, Y_coord);
                    //        if (new_z == -1)
                    //        {
                    //            throw new Exception("no space for token in Z");
                    //        }
                    //        else
                    //        {
                    //            Token_list[i].Z_coord = new_z;
                    //            continue;
                    //        }
                    //    }

                    //}
                }
            return Location_storage_new;
            }
        

        //display
        public void display_map(PaintEventArgs paint_Event)
        {
            //Console.WriteLine($"Grid width={Map_grid.get_grid_columns()},Grid Height={Map_grid.get_grid_rows()}, Location_storage.GetLength(0){Location_storage.GetLength(0)},Location_storage.GetLength(1){Location_storage.GetLength(1)}");
            map_matrix_convert();
            Map_grid.draw_grid(paint_Event);
            //Console.WriteLine($"attempting to display map");
            //Console.WriteLine($"map grid size is {Map_grid.get_grid_picturebox().Size}");
            //if (Token_list != null & Token_list.Count > 0)
            //{
            //    //Token_list[0].physical_token.Invalidate();
            //    Console.WriteLine($"telling token {Token_list[0].name} to redraw itself");
            //    //    for (int i = 0; i < Token_list.Count; i++)
            //    //    {
            //    //    }
            //}
        }
        public void Invalidate_tokens()
        {
            map_matrix_convert();

            if (Token_list != null)
            {
                //print_token_locations();
                for (int i = 0; i < Token_list.Count; i++)
                {
                    List<int> token_location = get_token_size(Token_list[i]);
                    if (token_location.Count < 3)
                    {
                        throw new Exception("token display error");
                    }

                    //Token_list[i].physical_token.Dispose();
                    Token_list[i].Invalidate();
                }
            }
        }
        public void display_token(PaintEventArgs paint_Event)
        {
            if (Token_list != null)
            {
                //print_token_locations();
                for (int i = 0; i < Token_list.Count; i++)
                {
                    List<int> token_location = get_token_size(Token_list[i]);
                    if (token_location.Count < 3)
                    {
                        throw new Exception("token display error");
                    }

                    //Token_list[i].physical_token.Dispose();
                    Token_list[i].display_Token(paint_Event, token_location);
                }
            }
        }
        public void display_token_singular(PictureBox box, PaintEventArgs paint_Event)
        {
            Token tok = get_token_by_picturebox(box);
            if (tok != null)
            {
                //Console.WriteLine($"displying singular token{tok} ({tok.name}) with picturebox{box}");

                List<int> token_location = get_token_size(tok);
                if (token_location.Count < 3)
                {
                    throw new Exception("token display error");
                }

                //Token_list[i].physical_token.Dispose();
                tok.display_Token(paint_Event, token_location);
            }
            else
            {
                //throw null;
            }
        }

        //Tokens
        public Token make_new_token(PictureBox new_token_picturebox)
        {
            Console.WriteLine($"Grid width={Map_grid.get_grid_columns()},Grid Height={Map_grid.get_grid_rows()}, Location_storage.GetLength(0){Location_storage.GetLength(0)},Location_storage.GetLength(1){Location_storage.GetLength(1)}");

            //List<int> dimensions = Map_grid.get_square_size();
            //Console.WriteLine($"the dimensions of the map_grid_size are width={dimensions[0]} and height={dimensions[1]} (for the new token)b");
            //new_token_picturebox.Width = dimensions[0];
            //new_token_picturebox.Height = dimensions[1];
            //Console.WriteLine("adding new token");
            //print_token_locations();

            List<int> next_location=next_location_to_place_token();
            if (next_location[0] == -1)
            {
                Console.WriteLine("too many cooks in the kitchen, delete some and try again.");
                //throw new Alert("too many tokens");
                return null;
                
            }

            Token made_token;
            string name_token="null";
            if (Token_list != null)
            {
                name_token = "Token" + Token_list.Count.ToString();

                if (Token_list.Count != 0)
                {
                    //name_token ="Token"+ Token_list.Count.ToString();
                    //Console.WriteLine($"token list:{Token_list}\n end token list. naming the token{name_token}");
                    made_token = new Token(name_token, new_token_picturebox, next_location[0], next_location[1], next_location[2]);
                    //Token_list.Add(made_token);
                    append_token(made_token);
                    Location_storage[next_location[0], next_location[1], next_location[2]] = made_token;
                    //Console.WriteLine($" at {next_location[0]},{next_location[1]},{next_location[2]} in location storage ({Location_storage}) the token has been set to {made_token}({made_token.name})");
                    //print_token_locations();

                    //Console.WriteLine("token should now be in the list");

                    //print_token_locations();
                    return made_token;
                }
                //name_token = "Token" + Token_list.Count.ToString();

                made_token = new Token(name_token, new_token_picturebox, next_location[0], next_location[1], next_location[2]);
                //Token_list.Add(made_token);
                append_token(made_token);
                //Console.WriteLine($"token list:{Token_list}\n end token list. naming the token{name_token}");

                //Console.WriteLine($"token list (having started with count =0):{Token_list} end token list, naming the token \"{name_token}\"");

                //Console.WriteLine($"[0]{next_location[0]}");
                //Console.WriteLine($"[1]{next_location[1]}");
                //Console.WriteLine($"[2]{next_location[2]}");

                Location_storage[next_location[0], next_location[1], next_location[2]] = made_token;
                //Console.WriteLine($" at {next_location[0]},{next_location[1]},{next_location[2]} in location storage ({Location_storage}) the token has been set to {made_token}({made_token.name})");
                //print_token_locations();
                //Console.WriteLine("token should now be in the list");

                //print_token_locations();

                return made_token;
            }
            Token_list = new List<Token>();
            //Console.WriteLine("token_list==null");
            made_token = new Token(name_token, new_token_picturebox, next_location[0], next_location[1], next_location[2]);
            //Token_list.Add(made_token);
            append_token(made_token);
            Location_storage[next_location[0], next_location[1], next_location[2]] = made_token;
            //Console.WriteLine($" at {next_location[0]},{next_location[1]},{next_location[2]} in location storage ({Location_storage}) the token has been set to {made_token}({made_token.name})");
            //Console.WriteLine("token should now be in the list");
            //print_token_locations();
            return made_token;
        }

        public List<int> get_token_size(Token token_tested)
        {
            List<int> token_location= new List<int>();
            List<int> grid_size = Map_grid.get_square_size();
            //int tokenx = token_location_x(token_tested);
            //int tokeny = token_location_y(token_tested);
            //int tokenz = token_location_z(token_tested);
            token_location.Add(grid_size[0]);
            token_location.Add(grid_size[1]);
            token_location.Add(grid_size[2]);

            //token_location.Add(tokenx * grid_size[0]);
            //token_location.Add(tokeny * grid_size[1]);
            //token_location.Add(tokenz);

            return token_location;
        }

        public void print_token_locations()
        {
            Console.WriteLine($"printing token locations Location_storage length:{Location_storage} ({Location_storage.GetLength(0)},{Location_storage.GetLength(1)},{Location_storage.GetLength(2)})");
            for (int i = 0; i < Location_storage.GetLength(0); i++)
            {
                for (int j = 0; j < Location_storage.GetLength(1); j++)
                {
                    for (int k = 0; k < Location_storage.GetLength(2); k++)
                    {
                        if (Location_storage[i, j, k] != null)
                        {
                            Console.WriteLine($"Token at {i},{j},{k}, is  {Location_storage[i, j, k]}. it is named {Location_storage[i, j, k].name}");
                        }
                    }
                }
            }
        }

        public List<int> next_location_to_place_token()
        {
            //0 is Y(or width) 1 is X(or Height) 2 is pen width
            List<int>output = new List<int>();
            List<int> gridsize = map_get_square_size();
            int Y_uno  = Location_storage.GetLength(0) / 2;
            int X_one = Location_storage.GetLength(1) / 2;
            if (Location_storage[Y_uno, X_one, Elevation_ground] == null)
            {
                //outx = gridsize[0] * one;
                //outy = gridsize[1] * uno;

                //output.Add(outx);
                //output.Add(outy);
                output.Add(Y_uno);
                output.Add(X_one);


                output.Add(Elevation_ground);
                return output;
            }
            for (int i = 0; i < Location_storage.GetLength(0); i++)
            {
                for (int j = 0; j < Location_storage.GetLength(1); j++)
                {
                    if (Location_storage[i, j, Elevation_ground] == null)
                    {

                        //outx = gridsize[0] * i;
                        //outy = gridsize[1] * j;
                        //output.Add(outx);
                        //output.Add(outy);

                        output.Add(i);
                        output.Add(j);
                        output.Add(Elevation_ground);
                        return output;
                    }
                }
            }
            //for (int i = 0; i < Location_storage.GetLength(0) / 2; i++)
            //{
            //    for (int j = 0; j < Location_storage.GetLength(1) / 2; j++)
            //    {
            //        if (Location_storage[i, j, Elevation_ground] == null)
            //        {
            //            output.Add(i);
            //            output.Add(j);
            //            output.Add(Elevation_ground);
            //            return output;
            //        }
            //    }
            //}
            output.Add(-1);
            return output;
        }
        public int next_location_to_place_token__Z(int i, int j, Token[,,] Location_storage_new)
        {
            if (Location_storage_new[i, j, Elevation_ground] == null)
            {
                return Elevation_ground;
            }
            for (int k = Elevation_ground; k < Elevation_up; k++)
            {
                if (Location_storage_new[i, j, k] == null)
                {
                    return k;
                }
            }
            for (int l = Elevation_ground; l < Elevation_down; l=l-1)
            {
                if (Location_storage_new[i, j, l] == null)
                {
                    return l;
                }
            }
            return -1;

        }
        public int next_location_to_place_token__Y(int x, int z, Token[,,] Location_storage_new)
        {

            int height= Location_storage_new.GetLength(1);
            for (int k = 0; k < height; k++)
            //for (int k = 0; k < (height - 1); k++)

                {
                    //Console.WriteLine($"x:{x} xmax:{Location_storage_new.GetLength(0)}, k:{k} ymax:{Location_storage_new.GetLength(1)}, z:{z} zmax:{Location_storage_new.GetLength(2)}");

                if (Location_storage_new[x, k, z] == null)
                {
                    return k;
                }
            }
            return -1;

        }
        public int next_location_to_place_token__X(int y, int z, Token[,,] Location_storage_new)
        {    
            int width = Location_storage_new.GetLength(0);
            //Console.WriteLine($"width:{width}");
            for (int k = 0; k < width; k++)
            {
                //Console.WriteLine($"k:{k},width:{width}");

                if (Location_storage_new[k, y, z] == null)
                {
                    return k;
                }
            }
            return -1;

        }

        //move tokens
        public bool move_token_east(KeyEventArgs e,Token token_to_move)
        {
            //Console.WriteLine($"Grid width={Map_grid.get_grid_columns()},Grid Height={Map_grid.get_grid_rows()}, Location_storage.GetLength(0){Location_storage.GetLength(0)},Location_storage.GetLength(1){Location_storage.GetLength(1)}");

            //Console.WriteLine("moving east");
            if (token_to_move.X_coord + 1 < Location_storage.GetLength(0))
            {
                //Console.WriteLine("premove");
                //print_token_locations();
                //Console.WriteLine($"Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //Console.WriteLine($"New Location_storage[{token_to_move.X_coord+1},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord+1, token_to_move.Y_coord, token_to_move.Z_coord]}. ");

                Token change = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord];
                //Console.WriteLine($"Token{change}");
                Token being_replaced = Location_storage[token_to_move.X_coord + 1, token_to_move.Y_coord, token_to_move.Z_coord];
                if (being_replaced != null)
                {
                e.Handled = true;
                    return false;
                }
                //Console.WriteLine($"old spot{being_replaced}");
                Location_storage[token_to_move.X_coord + 1, token_to_move.Y_coord, token_to_move.Z_coord] = change;


                Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = null;
                token_to_move.X_coord = token_to_move.X_coord + 1;
                Location_storage = Location_storage;
                //Console.WriteLine("Postmove");

                //print_token_locations();
                //token_to_move.physical_token.Dispose();
                token_to_move.Invalidate();

                e.Handled = true;
                return true;
            }
                e.Handled = true;
            return false;

        }
        public bool move_token_west(KeyEventArgs e,Token token_to_move)
        {

            if (token_to_move.X_coord - 1 >= 0)
            {
                //Console.WriteLine($"Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //Console.WriteLine($"New Location_storage[{token_to_move.X_coord - 1},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord - 1, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //print_token_locations();

                Token change = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord];
                Token being_replaced = Location_storage[token_to_move.X_coord - 1, token_to_move.Y_coord, token_to_move.Z_coord];
                if (being_replaced != null)
                {
                e.Handled = true;
                    return false;
                }
                Location_storage[token_to_move.X_coord - 1, token_to_move.Y_coord, token_to_move.Z_coord] = change;

                Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = null;
                token_to_move.X_coord = token_to_move.X_coord - 1;
                token_to_move.Invalidate();



                //Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = Location_storage[token_to_move.X_coord - 1, token_to_move.Y_coord, token_to_move.Z_coord];
                //token_to_move.X_coord = token_to_move.X_coord - 1;
                e.Handled = true;
                return true;
            }
                e.Handled = true;
            return false;

        }
        public bool move_token_north(KeyEventArgs e,Token token_to_move)
        {
            if (token_to_move.Y_coord - 1 >= 0)
            {
                //Console.WriteLine($"Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //Console.WriteLine($"New Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord-1},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord-1, token_to_move.Z_coord]}. ");
                //print_token_locations();

                Token change = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord];
                Token being_replaced = Location_storage[token_to_move.X_coord, token_to_move.Y_coord - 1, token_to_move.Z_coord];
                if (being_replaced != null)
                {
                e.Handled = true;
                    return false;
                }
                Location_storage[token_to_move.X_coord, token_to_move.Y_coord - 1, token_to_move.Z_coord] = change;

                Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = null;
                token_to_move.Y_coord = token_to_move.Y_coord - 1;
                token_to_move.Invalidate();

                e.Handled = true;
                return true;
            }
                e.Handled = true;
            return false;
        }
        public bool move_token_south(KeyEventArgs e,Token token_to_move)
        {

            if (token_to_move.Y_coord + 1 < Location_storage.GetLength(1))
            {
                //Console.WriteLine($"Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //Console.WriteLine($"New Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord+1},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord+1, token_to_move.Z_coord]}. ");
                //print_token_locations();

                Token change = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord];
                Token being_replaced = Location_storage[token_to_move.X_coord, token_to_move.Y_coord + 1, token_to_move.Z_coord];
                if (being_replaced != null)
                {
                e.Handled = true;
                    return false;
                }
                Location_storage[token_to_move.X_coord, token_to_move.Y_coord + 1, token_to_move.Z_coord] = change;

                Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = null;
                token_to_move.Y_coord = token_to_move.Y_coord + 1;
                token_to_move.Invalidate();

                e.Handled = true;
                return true;
            }
                e.Handled = true;
            return false;
        }
        public bool move_token_out(KeyEventArgs e,Token token_to_move)
        {
            if (token_to_move.Z_coord + 1 < Elevation_up)
            {
                //Console.WriteLine($"Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //Console.WriteLine($"New Location_storage[{token_to_move.X_coord + 1},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord + 1, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //print_token_locations();
                Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord+1];
                token_to_move.Y_coord = token_to_move.Y_coord + 1;
                e.Handled = true;

                return true;
            }
            set_elevation_up(Elevation_up + 1);
            Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord + 1];
            token_to_move.Z_coord = token_to_move.Z_coord + 1;
            e.Handled = true;

            return true;

            e.Handled = true;
            return false;
        }
        public bool move_token_in(KeyEventArgs e,Token token_to_move)
        {
            if (token_to_move.Z_coord - 1 >= Elevation_down)
            {
                //Console.WriteLine($"Location_storage[{token_to_move.X_coord},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //Console.WriteLine($"New Location_storage[{token_to_move.X_coord + 1},{token_to_move.Y_coord},{token_to_move.Z_coord}]:{Location_storage[token_to_move.X_coord + 1, token_to_move.Y_coord, token_to_move.Z_coord]}. ");
                //print_token_locations();
                Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord-1];
                token_to_move.Z_coord = token_to_move.Z_coord - 1;
                e.Handled = true;

                return true;
            }

            set_elevation_down(Elevation_down+ 1);
            try { 
            Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord] = Location_storage[token_to_move.X_coord, token_to_move.Y_coord, token_to_move.Z_coord - 1];
            }
            catch { return false; }
            token_to_move.Z_coord = token_to_move.Z_coord - 1;
            e.Handled = true;
            return true;
            e.Handled = true;

            return false;
        }


        //public int token_location_x(Token token_tested)
        //{
        //    if (token_tested == null) { return 0; }

        //    return token_tested.X_coord;
        //}
        //public int token_location_y(Token token_tested)
        //{
        //    if (token_tested == null) { return 0; }
        //    return token_tested.Y_coord;
        //}
        //public int token_location_z(Token token_tested)
        //{
        //    if (token_tested == null) { return Elevation_ground; }
        //    return token_tested.Z_coord;
        //}



        //setters 
        public int append_token(Token token_in)
        {
            Token_list.Add(token_in);
            return Token_list.Count;
        }

        public void set_grid(Grid Map_grid_in)
        {
            Map_grid = Map_grid_in;
        }
        public void set_name(string name)
        {
            Map_name = name;
        }
        public void set_Map_image(Bitmap map_in)
        {
            Map_image=map_in;
            Original_Height = map_in.Height;
            Original_Width = map_in.Width;
        }
        public void set_elevation_difference(int elevation_gain, int elevation_loss)
        {
            Elevation_up = elevation_gain;
            Elevation_down = elevation_loss;
            Elevation_total = 1 + Elevation_up + Elevation_down;
            //not plus one because this is the size, so the location would be -1 making the needed plus one 'out' of the elevation_down redundant.
            Elevation_ground = Elevation_down;
            map_matrix_convert();
        }
        public void set_elevation_up(int elevation_gain)
        {
            Elevation_up = elevation_gain;
            Elevation_total = 1 + Elevation_up + Elevation_down;
            //not plus one because this is the size, so the location would be -1 making the needed plus one 'out' of the elevation_down redundant.

            Elevation_ground = Elevation_down;
            map_matrix_convert();
        }
        public void set_elevation_down(int elevation_loss)
        {
            Elevation_down = elevation_loss;
            Elevation_total = 1 + Elevation_up + Elevation_down;
            //not plus one because this is the size, so the location would be -1 making the needed plus one 'out' of the elevation_down redundant.
            Elevation_ground = Elevation_down + 1;
            map_matrix_convert();
        }
        public void set_calculated_dimensions(int width, int height)
        {
            Calculated_height = height;
            Calculated_width = width;
            set_subgrid_dimensions(width, height);
            map_matrix_convert();

        }
        public void set_calculated_dimensions(int width, int height,int up, int down)
        {
            Calculated_height = height;
            Calculated_width = width;

            Elevation_up = up;
            Elevation_down = down;
            Elevation_total = 1 + Elevation_up + Elevation_down;

            set_subgrid_dimensions(width, height);
            map_matrix_convert();

        }
        public void set_Calculated_height(int calculated_height_in)
        {
            Calculated_height = calculated_height_in;
            Map_grid.set_gridbox_height(calculated_height_in);
            //Calculated_height
            map_matrix_convert();
        }
        public void set_Calculated_width(int calculated_width_in)
        {
            Calculated_width = calculated_width_in;

            Map_grid.set_gridbox_width(calculated_width_in);
            //Calculated_height
            map_matrix_convert();
        }
        public void set_Map_box(PictureBox inbox)
        {
            Map_box = inbox;
            Map_grid.set_grid_picturebox(Map_box);
            set_calculated_dimensions(Map_box.Width, Map_box.Height);
            map_matrix_convert();
            //throws an error of being ones own parent! this is good
            //Map_grid.get_grid_picturebox().Parent = Map_box;
        }
        public void set_subgrid_dimensions(int width,int height)
        {
            
            set_subgrid_width(width);
            set_subgrid_height(height);
            calculate_grid(grid_x_count, grid_y_count);
            //map_matrix_convert();

        }
        public void set_subgrid_width(int width)
        {
             Map_grid.set_gridbox_width(width);
            map_matrix_convert();
        }
        public void set_subgrid_height(int height)
        {
            Map_grid.set_gridbox_height(height);
            map_matrix_convert();
        }
        public void set_subgrid_size(int columns,int rows)
        {
            Map_grid.set_grid_size(columns,rows);
            map_matrix_convert();
        }
        public void set_subgrid_columns(int columns)
        {
            Map_grid.set_grid_columns(columns);
            map_matrix_convert();
        }
        public void set_subgrid_rows(int rows)
        {
            Map_grid.set_grid_rows(rows);
            map_matrix_convert();
        }


        //getters
        public string get_name()
        {
            return Map_name;
        }
        public Grid get_grid() {return Map_grid;}
        public List<Token> get_token_list()
        {
            return Token_list;
        }
        public Token get_token_by_id(int id) {
            if (id<Token_list.Count){
                return Token_list[id];
            }
            Console.WriteLine("Token outside of token list");
            return null;
        }
        public Token get_token_by_picturebox(PictureBox id)
        {
            for (int i=0; i < Token_list.Count; i++) { 
                if (Token_list[i].physical_token== id)
                {
                    return Token_list[i];
                }

            }
            Console.WriteLine("Token outside of token list");
            return null;
        }
        public Bitmap get_Map_image()
        {
            return Map_image;
        }
        public int get_elevation_Gain()
        {
            return Elevation_up;
        }
        public int get_Elevation_down()
        {
            return Elevation_down;
        }
        public Token Get_token_at_location(int x,int y,int z)
        {
            return Location_storage[x,y,z];
        }
        public Token Get_token_at_location(int x, int y)
        {
            return Location_storage[x, y,Elevation_ground];
        }
        public int Get_Calculated_height()
        {
            return Calculated_height;
        }
        public int Get_Calculated_width()
        {
            return Calculated_width;
        }
        public (int,int) Get_Calculated_dimensions()
        {
            return (Get_Calculated_height(), Get_Calculated_width());
        }

        public int get_subgrid_columns()
        {
            return Map_grid.get_grid_columns();
        }
        public int get_subgrid_rows()
        {
            return Map_grid.get_grid_rows();
        }
        public PictureBox get_Map_box()
        {
            return Map_box;
        }

        public List<int> map_get_square_size()
        {
            return Map_grid.get_square_size();
        }





        //unfinished and or broken
        public int get_token_id(Token token_query) {
            //    if (Token_list.Contains(token_query)) {
            //        Token_list.FindIndex()
            //        Token_list.FindIndex(Token_list,token_query);
            //    }
            //throw ;
            //raise unimplimented 
            return -1;
        }

        public static bool compare_tokens(Token token) { 
        //{
        //    if (token == null) return false;
            return false;
        }



}
}
