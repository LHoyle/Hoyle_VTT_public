using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

using System.IO;
using System.Drawing.Imaging;
//using System.Drawing.Font;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Hoyle_VTT.Properties;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Diagnostics;

namespace Hoyle_VTT
{
    //($"string{variable}");
    //might be useful?
    public partial class VTT : Form
    {
        //Variables 

        //public Grid my_grid;
        //public Grid my_grid = new Grid();
        protected List<Map> Maps=new List<Map>();
        protected List<PictureBox> token_boxes = new List<PictureBox>();
        //protected Map current_map=new Map("base", global::Hoyle_VTT.Properties.Resources.AbandonedDrawingRoom);
        protected Map current_map = new Map("base", global::Hoyle_VTT.Properties.Resources.notebookscan_1);

        
        int submenu_selected=0;
        PictureBox current_box;


        //PaintEventArgs paint_Grid;

        //Initilization
        public VTT()
        {
            InitializeComponent();

            this.KeyPreview = true;
            //this.KeyDown += new KeyEventHandler(VTT_KeyDown);
            //current_box.BackColor = Color.Red;


            old_grid_Box.Visible = false;
            //makes sure the map
            Maps.Add(current_map);
            current_map.file_name = "base";
            current_map.Map_box=grid_picture_Box;
            //grid_picture_Box.Visible=false;
            grid_picture_Box.Parent = VTT_table_box;

            grid_picture_Box.Size = VTT_table_box.Size;
            grid_picture_Box.Location = new Point(0, 0);
            current_map.set_Map_box(grid_picture_Box);
            load_image_by_map_class(current_map);
            //current_map.initialize_grid(gridsize_default);

            //VTT_table_box.Height = vtt_box_height_calculator();
            //VTT_table_box.Width = vtt_box_width_calculator();
            //double width_Ratio = (float)VTT_table_box.Width / current_map.Original_Width;
            //double height_Ratio = (float)VTT_table_box.Height / current_map.Original_Height;
            //current_map.Ratio = Math.Min(width_Ratio, height_Ratio);

            //current_map.set_calculated_dimensions(VTT_table_box.Height, VTT_table_box.Width);
            VTT_picture_box_size_calculator();

            load_image_by_map_class(current_map);

            //my_grid = new Grid();
            //paint_Grid= System.Windows.Forms.PaintEventHandler(this.grid_picture_Box_paint);

            Grid_height_num.Value = current_map.get_subgrid_rows();
            Grid_width_num.Value = current_map.get_subgrid_columns();
            

            int topbox = vtt_box_y_top_calculator();
            int leftbox= vtt_box_x_left_calculator();

            VTT_table_box.Top = topbox;

            VTT_table_box.Left = leftbox;

            //initialize the grid
            //grid_picture_Box.Parent = VTT_table_box;
            //grid_picture_Box.Size = VTT_table_box.Size;
            //grid_picture_Box.Location = new Point(0, 0);


            grid_picture_Box.Paint += new System.Windows.Forms.PaintEventHandler(this.grid_picture_Box_paint);
            this.Controls.Add(grid_picture_Box);
            bulk_invalidate();

            //gridpen= 
            //Console.WriteLine("drawing grid calling from 'VTT'");
            //grid_picture_Box.Invalidate();

            //my_grid.draw_grid(true);

            submenu_selected = 0;
            submenu_alter(-1);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            VTT_table_box.Height = vtt_box_height_calculator();
            VTT_table_box.Width = vtt_box_width_calculator();

            current_map.set_calculated_dimensions(VTT_table_box.Height, VTT_table_box.Width);
        }

        //Map
        public Map map_exists(string name)
        {
            if (Maps != null & (Maps.Count == 0))
            {
                //bool exists = false;
                for (int i = 0; i < Maps.Count(); i++)
                {
                    if (name == Maps[i].file_name)
                    {
                        return Maps[i];
                    }
                }
            }
            return null;
        }
        private void load_image_by_map_class(Map map)
        {
            current_map=map;
            //if (map
            Maps.Add(map);
            if (grid_picture_Box.Parent != VTT_table_box)
            {
                grid_picture_Box.Parent = VTT_table_box;
                grid_picture_Box.BringToFront();
            }
            VTT_picture_box_size_calculator();
            current_map.set_Map_box(grid_picture_Box);
            current_map.get_Map_box().Parent = VTT_table_box;
            VTT_table_box.Image = map.get_Map_image();

            List<Token> token_List=current_map.get_token_list();
            foreach (PictureBox pic in token_boxes)
            {
                pic.Visible = false;
            }
            token_boxes = new List<PictureBox>();
            foreach (Token token in token_List)
            {
                PictureBox picture = token.physical_token;
                if (picture.Visible == false)
                {
                    picture.Visible = true;
                }
                token_boxes.Add(picture);
            }
            //VTT_table_box.Height = (int)Math.Ceiling(map.Original_Height * map.Ratio);
            //VTT_table_box.Width = (int)Math.Ceiling(map.Original_Width * map.Ratio);

            //VTT_table_box.Top = map.Y_location;
            //VTT_table_box.Left = map.X_location;

            //VTT_table_box.BringToFront();


            Grid_height_num.Value = current_map.get_subgrid_rows();
            Grid_width_num.Value = current_map.get_subgrid_columns();

            //Grid_height_num.Value = my_grid.get_grid_rows();
            //Grid_width_num.Value = my_grid.get_grid_columns();
            bulk_invalidate();
            //grid_picture_Box.Invalidate(); 
        }
        private void load_image_without_map(string filePath)
        {
            Map map_exists_check = map_exists(filePath);
            if (map_exists_check != null)
            {
                load_image_by_map_class(map_exists_check);
                return;
            }

            Image image1;
            try
            {
                image1 = Image.FromFile(filePath);
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("Out of memory error" + ex);
                toolStripStatusLabel1.Text = ("Out of memory error Check console for more information");

                return;
            }
            catch (ArgumentException AE)
            {
                Console.WriteLine("Argument error" + AE);
                toolStripStatusLabel1.Text = ("Argument error. Check console for more information");

                return;
            }
            Bitmap bitmap_image = new Bitmap(image1);

            //+ VTT_table_box.Location.Y


            //double vtt_ratio = (double)vtt_width / (double)vtt_height;

            //Map new_map = new Map(filePath, bitmap_image, my_grid, VTT_table_box);
            Map new_map = new Map(filePath, bitmap_image);
            new_map.Map_box = grid_picture_Box;
            VTT_picture_box_size_calculator();

            //int vtt_height = vtt_box_height_calculator();
            //int vtt_width = vtt_box_width_calculator();
            //new_map.set_calculated_dimensions(vtt_width, vtt_height);

            //new_map.Y_location = vtt_box_y_top_calculator();
            //new_map.X_location = vtt_box_x_left_calculator();

            //double width_Ratio = (float)vtt_width / new_map.Original_Width;
            //double height_Ratio = (float)vtt_height / new_map.Original_Height;
            //new_map.Ratio = Math.Min(width_Ratio, height_Ratio);

            toolStripStatusLabel1.Text = ("Image Loaded!");
            suboptions_Menu.DataSource = map_data_Set;

            Maps.Add(new_map);
            load_image_by_map_class(new_map);
            return;
            //System.IO.Stream file = filePath;

            //picture_grabbing.InitialDirectory

            //.OpenFile();
            //picture_grabbing.
            //VTT_table_box.Image= picture_grabbing.OpenFile();

            // https://learn.microsoft.com/en-us/dotnet/api/system.drawing.image.fromfile?view=windowsdesktop-9.0

            //file.
            //Image image1=Image.FromFile(file_name_in);
        }
        private void load_image_without_map(string name, Bitmap image1)
        {
            Map map_exists_check = map_exists(name);
            if (map_exists_check == null)
            {
                load_image_by_map_class(map_exists_check);
                return;
            }

            Map new_map = new Map(name, image1);
            new_map.Map_box = grid_picture_Box;
            VTT_picture_box_size_calculator();

            //int vtt_height = vtt_box_height_calculator();
            //int vtt_width = vtt_box_width_calculator();
            //new_map.set_calculated_dimensions(vtt_width, vtt_height);

            //new_map.Y_location = vtt_box_y_top_calculator();
            //new_map.X_location = vtt_box_x_left_calculator();

            //double width_Ratio = (float)vtt_width / new_map.Original_Width;
            //double height_Ratio = (float)vtt_height / new_map.Original_Height;
            //new_map.Ratio = Math.Min(width_Ratio, height_Ratio);

            toolStripStatusLabel1.Text = ("Image Loaded!");
            suboptions_Menu.DataSource = map_data_Set;

            Maps.Add(new_map);
            load_image_by_map_class(new_map);
            return;
            //System.IO.Stream file = filePath;

            //picture_grabbing.InitialDirectory

            //.OpenFile();
            //picture_grabbing.
            //VTT_table_box.Image= picture_grabbing.OpenFile();

            // https://learn.microsoft.com/en-us/dotnet/api/system.drawing.image.fromfile?view=windowsdesktop-9.0

            //file.
            //Image image1=Image.FromFile(file_name_in);
        }

        //Grid
        private void Grid_height_num_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine($"grid height changed to {Grid_height_num.Value}");


            //Grid_width_num.Value = current_map.get_subgrid_columns();
            current_map.set_subgrid_rows((int)Grid_height_num.Value);
            //my_grid.set_grid_rows();
            //my_grid.draw_grid();
            //grid_picture_Box.Invalidate();
            bulk_invalidate();

            //my_grid.draw_grid(false);

        }
        private void Grid_width_num_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine($"grid width changed to {Grid_width_num.Value}");
            current_map.set_subgrid_columns((int)Grid_width_num.Value);

            //my_grid.set_grid_columns((int)Grid_width_num.Value);
            //my_grid.draw_grid();
            //grid_picture_Box.Invalidate();
            bulk_invalidate();

            //my_grid.draw_grid(false);
        }

        //Tokens
        private void Make_new_token()
        {
            List<int> size = current_map.map_get_square_size();
            PictureBox new_token_picturebox= new PictureBox();
            token_boxes.Add(new_token_picturebox);
            new_token_picturebox.Width = size[0]-size[2];
            new_token_picturebox.Height = size[1]-size[2];
            Top = size[2];
            Left= size[2];
            //new_token.Paint += new System.Windows.Forms.PaintEventHandler(this.token_picture_Box_paint);
            //new_token.Parent = VTT_table_box;
            //new_token.Parent = grid_picture_Box;


            new_token_picturebox.Paint += new System.Windows.Forms.PaintEventHandler(this.single_token_picture_Box_paint);
            this.Controls.Add(new_token_picturebox);

            //this.Controls.Add(new_token);
            Token new_Token=current_map.make_new_token(new_token_picturebox);
            new_token_picturebox.BringToFront();
            Token_list_box.Items.Add(new_Token.name);

            current_box= new_token_picturebox;

            toolStripStatusLabel1.Text = ("Created token!");
        }


        //Clicks
        private void Change_grid_Click(object sender, EventArgs e)
        {
            int former_submenu = submenu_selected;
            submenu_selected = 2;
            submenu_alter(former_submenu);




            if (grid_picture_Box.Visible == true)
            {
                if (grid_picture_Box.BackgroundImageLayout == System.Windows.Forms.ImageLayout.Tile)
                {
                    toolStripStatusLabel1.Text = ("grid changed to Stretch");
                    grid_picture_Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    return;
                }
                else if (grid_picture_Box.BackgroundImageLayout == System.Windows.Forms.ImageLayout.Stretch)
                {
                    toolStripStatusLabel1.Text = ("grid changed to Tile");
                    grid_picture_Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
                    return;
                }

                //if (grid_picture_Box.SizeMode == System.Windows.Forms.PictureBoxSizeMode.AutoSize)
                //{
                //    toolStripStatusLabel1.Text = ("grid changed to Stretch");

                //    grid_picture_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                //    grid_picture_Box.Update();

                //}
                //else if(grid_picture_Box.SizeMode == System.Windows.Forms.PictureBoxSizeMode.StretchImage)
                //{
                //    toolStripStatusLabel1.Text = ("grid changed to Center");

                //    grid_picture_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                //    grid_picture_Box.Update();


                //}
                //else if(grid_picture_Box.SizeMode == System.Windows.Forms.PictureBoxSizeMode.CenterImage)
                //{
                //    toolStripStatusLabel1.Text = ("grid changed to 'Normal'");

                //    grid_picture_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
                //    grid_picture_Box.Update();


                //}
                //else if(grid_picture_Box.SizeMode == System.Windows.Forms.PictureBoxSizeMode.Normal)
                //{
                //    toolStripStatusLabel1.Text = ("grid changed to Zoom");

                //    grid_picture_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                //    grid_picture_Box.Update();


                //}
                //else if(grid_picture_Box.SizeMode == System.Windows.Forms.PictureBoxSizeMode.Zoom)
                //{
                //    toolStripStatusLabel1.Text = ("grid changed to Auto");

                //    grid_picture_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                //    grid_picture_Box.Update();

                //}
            }
            else
            {
                toolStripStatusLabel1.Text = ("Grid is not visible to be toggled.");

            }

        }
        private void Drawing_click(object sender, EventArgs e)
        {
            int former_submenu = submenu_selected;
            submenu_selected = 3;

            //statusStrip1.Visible = true;
            toolStripStatusLabel1.Text = ("opening Drawing Menu!");
            submenu_alter(former_submenu);
            suboptions_Menu.DataSource = Drawing_Data_set;

        }
        private void grid_picture_Box_Click(object sender, EventArgs e) {}
        private void grid_toggle_Click(object sender, EventArgs e)
        {
            int former_submenu = submenu_selected;
            submenu_selected = 1;
            submenu_alter(former_submenu);



            if (grid_picture_Box.Visible == true)
            {
                toolStripStatusLabel1.Text = ("Grid Turned off");

                grid_picture_Box.Visible = false;
            }
            else
            {
                toolStripStatusLabel1.Text = ("Grid Turned on");

                grid_picture_Box.Visible = true;
            }
        }
        private void Load_click(object sender, EventArgs e)
        {
            int former_submenu = submenu_selected;
            submenu_selected = 0;
            submenu_alter(former_submenu);

            //double modified_height;
            //double radtio_height;
            //int outheight;
            //double modified_width;
            //double radtio_width;
            //int outwidth;
            var filePath = string.Empty;
            //statusStrip1.Visible = true;
            toolStripStatusLabel1.Text = ("time to load!");
            //Console.WriteLine("load button clicked!");

            picture_grabbing.InitialDirectory = "c:/";

            picture_grabbing.Filter = "Image files (*.png)|*.png|(*.jpg)|*.jpg|All Files (*.*)|*.*";
            //picture_grabbing.Filter = "All Files (*.*)|*.*";

            if (picture_grabbing.ShowDialog() == DialogResult.OK)
            {
                filePath = picture_grabbing.FileName;
                if (File.Exists(filePath))
                {
                    Map map_buffer = map_exists(filePath);
                    if (map_buffer != null)
                    {
                        Console.WriteLine($"Map is {map_buffer.get_name()}");
                        load_image_by_map_class(map_buffer);
                        return;
                    }
                    
                    //string fileContents = File.ReadAllText(file);
                    //toolStripStatusLabel1.Text = fileContents;
                }
                else
                {
                    toolStripStatusLabel1.Text = "File Not Found";
                    //picture_grabbing.OpenFile();
                    return;
                }
            }
            load_image_without_map(filePath);

            return;
            //Image image1 = Image.FromStream(filePath);
            //Image image2 = Image.FromFile(file);
            //PropertyItem propItem = image1.GetPropertyItem(20624);
            //propItem.Id = 20625;
            //image2.SetPropertyItem(propItem);
            //if (File.Exists(file))
            //{
            //    string fileContents = File.ReadAllText(file);
            //    toolStripStatusLabel1.Text = fileContents;
            //}
            //else
            //{
            //    toolStripStatusLabel1.Text = "File Not Found";
            //picture_grabbing.OpenFile();
        }
        private void Token_menu_Click(object sender, EventArgs e)
        {
            int former_submenu = submenu_selected;

            submenu_selected = 4;
            //statusStrip1.Visible = true;
            toolStripStatusLabel1.Text = ("opening Token Menu!");
            submenu_alter(former_submenu);

            suboptions_Menu.DataSource = Token_Data_Set;
        }
        private void VTT_table_box_Click(object sender, EventArgs e){}
        
        //key presses

        private void VTT_KeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine($"keydown: {e.KeyData}\n current_box:{current_box}");

            if (e.KeyData == (Keys.Shift | Keys.Up)) 
            {
                //Console.WriteLine($"keydown{e.KeyData}, should be shift up");

                shift_up_arrow(e);
                //Console.WriteLine("moved");
                e.Handled = true;
                return;
            }
            if (e.KeyData == (Keys.Shift | Keys.Down))
            {
                //Console.WriteLine($"keydown{e.KeyData}, should be shift down");

                shift_down_arrow(e);
                //Console.WriteLine("moved");
                e.Handled = true;
                return;
            }
            if (e.KeyData == Keys.Up)
            {
                //Console.WriteLine($"keydown{e.KeyData}, should be up");

                up_arrow(e);
                //Console.WriteLine("moved");
                e.Handled = true;
                return;
            }
            if (e.KeyData == Keys.Down)
            {
            //Console.WriteLine($"keydown{e.KeyData}, should be down");
                down_arrow(e);
                e.Handled = true;
                return;
            }
            if (e.KeyData == Keys.Right)
            {
            //Console.WriteLine($"keydown{e.KeyData}, should be right");
                right_arrow(e);
                //Console.WriteLine("moved");
                e.Handled = true;
                return;
            }
            if (e.KeyData == Keys.Left)
            {
            //Console.WriteLine($"keydown{e.KeyData}, should be left");
                left_arrow(e);
                //Console.WriteLine("moved");
                e.Handled = true;
                return;
            }


        }

        private void right_arrow(KeyEventArgs e)
        {
            if (current_box != null)
            {
                Token token_of_box=current_map.get_token_by_picturebox(current_box);
                //Console.WriteLine($"Token:{token_of_box} ({token_of_box.name}) (move right)");
                if (token_of_box == null)
                {
                    throw new Exception("not found");
                }
                bool moved=current_map.move_token_east(e, token_of_box);
                e.Handled = true;

                if (moved == false)
                {
                    Console.WriteLine("failed to move");
                }
                current_box.Invalidate();
            }
        }
        private void left_arrow(KeyEventArgs e)
        {
            if (current_box != null)
            {
                Token token_of_box = current_map.get_token_by_picturebox(current_box);
                //Console.WriteLine($"Token:{token_of_box} ({token_of_box.name}) (move Left)");


                if (token_of_box == null)
                {
                    throw new Exception("not found");
                }
                bool moved = current_map.move_token_west(e, token_of_box);
                e.Handled = true;

                if (moved == false)
                {
                    Console.WriteLine("failed to move");
                }
                current_box.Invalidate();
            }
        }
        private void up_arrow(KeyEventArgs e)
        {
            if (current_box != null)
            {
                Token token_of_box = current_map.get_token_by_picturebox(current_box);
                //Console.WriteLine($"Token:{token_of_box} ({token_of_box.name}) (move up)");

                if (token_of_box == null)
                {
                    throw new Exception("not found");
                }
                bool moved = current_map.move_token_north(e, token_of_box);
                e.Handled = true;

                if (moved == false)
                {
                    Console.WriteLine("failed to move");
                }
                current_box.Invalidate();
            }
        }
        private void down_arrow(KeyEventArgs e)
        {
            if (current_box != null)
            {
                Token token_of_box = current_map.get_token_by_picturebox(current_box);
                //Console.WriteLine($"Token:{token_of_box} ({token_of_box.name}) (move down)");

                if (token_of_box == null)
                {
                    throw new Exception("not found");
                }
                bool moved = current_map.move_token_south(e, token_of_box);
                e.Handled = true;

                if (moved == false)
                {
                    Console.WriteLine("failed to move");
                }
                current_box.Invalidate();
            }
        }
        private void shift_up_arrow(KeyEventArgs e)
        {
            if (current_box != null)
            {
                Token token_of_box = current_map.get_token_by_picturebox(current_box);
                //Console.WriteLine($"Token:{token_of_box} ({token_of_box.name}) (move out)");

                if (token_of_box == null)
                {
                    throw new Exception("not found");
                }
                bool moved = current_map.move_token_out(e, token_of_box);
                e.Handled = true;

                if (moved == false)
                {
                    Console.WriteLine("failed to move");
                }
                current_box.Invalidate();
            }
        }
        private void shift_down_arrow(KeyEventArgs e)
        {
            if (current_box != null)
            {
                Token token_of_box = current_map.get_token_by_picturebox(current_box);
                //Console.WriteLine($"Token:{token_of_box} ({token_of_box.name}) (move in)");

                if (token_of_box == null)
                {
                    throw new Exception("not found");
                }
                bool moved = current_map.move_token_in(e, token_of_box);
                e.Handled = true;

                if (moved == false)
                {
                    Console.WriteLine("failed to move");
                }
                current_box.Invalidate();
            }
        }

        //Visual changes/resizes/painting event
        private void Button_Box_Resize(object sender, EventArgs e)
        {
            //+Button_Box.Padding.Right+ Button_Box.Padding.Left+ Button_Box.Margin.Right + Button_Box.Margin.Left
            Load_map_button.Parent = Button_Box;
            Change_grid_button.Parent = Button_Box;
            Drawing_menu_button.Parent = Button_Box;
            Token_menu_button.Parent = Button_Box;

            int modified_width = (Button_Box.Width) / 5;
            Load_map_button.Width = modified_width;
            Load_map_button.Left = 0;

            grid_toggle_button.Width = modified_width;
            grid_toggle_button.Left = modified_width + Load_map_button.Left;

            Change_grid_button.Width = modified_width;
            Change_grid_button.Left = modified_width * 2 + Load_map_button.Left;

            Drawing_menu_button.Width = modified_width;
            Drawing_menu_button.Left = modified_width * 3 + Load_map_button.Left;

            Token_menu_button.Width = modified_width;
            Token_menu_button.Left = modified_width * 4 + Load_map_button.Left;

            //+ Button_Box.Padding.Bottom
            int height_buffer = (Button_Box.Height - Button_Box.Padding.Top) / 2;
            int Button_by_half = Load_map_button.Height / 2;
            int height_modification = height_buffer - (Button_by_half / 2);

            Load_map_button.Top = height_modification;
            grid_toggle_button.Top = height_modification;
            Change_grid_button.Top = height_modification;
            Drawing_menu_button.Top = height_modification;
            Token_menu_button.Top = height_modification;
        }
        protected void grid_picture_Box_paint(object sender, PaintEventArgs e)
        {
            if (grid_picture_Box.Parent!= VTT_table_box)
            {
                grid_picture_Box.Parent = VTT_table_box;
            }
            current_map.display_map(e);
            
            //grid_picture_Box.BringToFront();
            if (token_boxes != null) {
                //current_map.Invalidate_tokens();
             //token_picture_Box_paint(sender, e);
            }
        }
        private bool options_menu_formatting(int former_menu)
        {
            //FontFamily.name
            int font_height = (int)Font.Size;
            // +Options_menu.Location.Y
            suboptions_Menu.Top = Options_menu.Padding.Top + font_height;
            suboptions_Menu.Width = Options_menu.Width - Options_menu.Padding.Horizontal;
            suboptions_Menu.Left = Options_menu.Padding.Left;
            Token_list_box.Top = suboptions_Menu.Top;
            Token_list_box.Width=suboptions_Menu.Width;
            Token_list_box.Left = suboptions_Menu.Left;
            int size_detractors = Menu_interact_button.Height + Options_menu.Padding.Vertical + font_height;

            //submenu0=load. 1=toggle grid 2=change grid,3=drawing,4=tokens
            if (submenu_selected == 0 | former_menu == -1)
            {
                //Console.WriteLine("submenu changed to Map!");
                suboptions_Menu.Visible = true;
                Token_list_box.Visible = false;
                Options_menu.Text = "Map menu";
                Menu_interact_button.Text = "map_undefined_action";
                Grid_width_num.Visible = false;
                Grid_height_num.Visible = false;
                grid_size_box.Visible = false;
                suboptions_Menu.Height = (Options_menu.Height - size_detractors);


                return true;
            }
            if (submenu_selected == 1 | submenu_selected == 2)
            {
                //Console.WriteLine("submenu changed to Grid!");
                Options_menu.Text = "Grid Menu";
                Menu_interact_button.Text = "grid_undefined_action";

                Grid_width_num.Visible = true;
                Grid_height_num.Visible = true;
                grid_size_box.Visible = true;

                size_detractors += grid_size_box.Height;
                suboptions_Menu.Height = (Options_menu.Height - size_detractors);
                grid_size_box.Top = suboptions_Menu.Bottom + Options_menu.Padding.Bottom;

                int buffer_space_width = grid_size_box.Padding.Horizontal;
                grid_size_box.Width = Options_menu.Width;
                int widthbuffer = ((grid_size_box.Width - buffer_space_width) / 2);
                //Console.WriteLine($"width buffersize = {widthbuffer}");
                if (widthbuffer < 10)
                {
                    //Console.WriteLine($"Width buffer too small at{widthbuffer}. setting width to 10");
                    widthbuffer = 10;
                }
                Grid_width_num.Width = widthbuffer;
                Grid_height_num.Width = widthbuffer;


                Grid_width_num.Parent = grid_size_box;
                Grid_width_num.Location = new Point(0, 0);
                Grid_height_num.Top = (grid_size_box.Padding.Top);
                Grid_width_num.BringToFront();

                Grid_height_num.Parent = grid_size_box;
                Grid_height_num.Left = (Grid_width_num.Width + buffer_space_width);
                Grid_height_num.Top = (grid_size_box.Padding.Top);
                Grid_height_num.BringToFront();

                suboptions_Menu.Visible = true;

                Token_list_box.Visible = false;



                //suboptions_Menu.Height = (Options_menu.Height - Options_menu.Padding.Vertical - Menu_interact_button.Height - Grid_height_num.Height);

                return true;

            }
            if (submenu_selected == 3)
            {
                //Console.WriteLine("submenu changed to drawing!");
                Options_menu.Text = "Drawing menu";
                Menu_interact_button.Text = "draw_undefined_action";

                Grid_width_num.Visible = false;
                Grid_height_num.Visible = false;
                grid_size_box.Visible = false;
                suboptions_Menu.Height = (Options_menu.Height - size_detractors);

                suboptions_Menu.Visible = true;
                Token_list_box.Visible = false;

                return true;
            }
            if (submenu_selected == 4)
            {
                List<Token> tokens_list = current_map.get_token_list();
                if (tokens_list.Count()!= token_boxes.Count())
                {
                    Console.WriteLine($"tokens_list.Count():{tokens_list.Count()},token_boxes.Count(){token_boxes.Count()}");
                    throw new Exception("how are these not equal?");
                }

                //List<Token> tokens_list = current_map.get_token_list();
                PictureBox current = current_box;
                Token_list_box.Items.Clear();
                foreach (Token token in tokens_list)
                {
                    Token_list_box.Items.Add(token.name);
                }
                foreach (PictureBox box in token_boxes)
                {
                    Console.Write($"box:{box}\n");
                }
                //Token_list_box.Items.Clear();

                //foreach (Token token in tokens_list)
                //{
                //    Token_list_box.Items.Add(token.name);

                //}
                //Console.WriteLine("submenu changed to token!");
                Options_menu.Text = "Token menu ";
                Menu_interact_button.Text = "add token";

                Grid_width_num.Visible = false;
                Grid_height_num.Visible = false;
                grid_size_box.Visible = false;
                suboptions_Menu.Height = (Options_menu.Height - size_detractors);
                suboptions_Menu.Visible = false;
                Token_list_box.Height = (Options_menu.Height - size_detractors);
                Token_list_box.Visible= true;

                return true;
            }
            return false;
        }
        private void Options_menu_Resize(object sender, EventArgs e)
        {
            submenu_alter(submenu_selected);
        }
        private bool submenu_alter(int former_menu)
        {
            //Console.WriteLine("current submenu= " + submenu_selected);
            //this was compacte because it was taking up too much space and bothering me.
            //if ((former_menu == submenu_selected) & (former_menu != -1)) { Console.WriteLine("no change in submenu needed!"); return false; }

            suboptions_Menu.Parent = Options_menu;
            Menu_interact_button.Parent = Options_menu;
            //Menu_interact_button.Bottom = Options_menu.Padding.Bottom;
            Menu_interact_button.Dock = DockStyle.Bottom;

            bool return_value = options_menu_formatting(former_menu);

            return return_value;
        }
        private void VTT_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            //VTT_table_box.Height = vtt_box_height_calculator();
            //VTT_table_box.Width = vtt_box_width_calculator();

            //current_map.set_calculated_dimensions(VTT_table_box.Height, VTT_table_box.Width);
            VTT_picture_box_size_calculator();


            grid_picture_Box.Parent = VTT_table_box;
            grid_picture_Box.Size = VTT_table_box.Size;
            grid_picture_Box.Location = new Point(0, 0);

            //grid_picture_Box.


            //splitter1.Update();
            //splitter2.Update();
            //Console.WriteLine("drawing grid calling from 'VTT_Dpi_changed'");
            VTT_table_box_SizeChanged(sender, e);
            bulk_invalidate();
            //float grid_width_float = VTT_table_box.Width / gridsize_default;
            //float grid_height_float = VTT_table_box.Height / gridsize_default;

            //int gridsize_width = (int)Math.Ceiling(grid_width_float);
            //int gridsize_height = (int)Math.Ceiling(grid_height_float);
            //my_grid.set_grid_size(gridsize_width, gridsize_height);
            //my_grid.draw_grid(false);

            int former_submenu = submenu_selected;
            submenu_alter(former_submenu);


            //grid_picture_Box.Scale(e);
        }
        private void VTT_Resize(object sender, EventArgs e)
        {
            VTT_picture_box_size_calculator();
            //    VTT_table_box.Height = vtt_box_height_calculator();
            //    VTT_table_box.Width = vtt_box_width_calculator();

            //    current_map.set_calculated_dimensions(VTT_table_box.Height, VTT_table_box.Width);
            //VTT_table_box_SizeChanged(object sender,e);
            //Console.WriteLine("drawing grid calling from 'VTTresize'");



            //my_grid.draw_grid(false);

            submenu_alter(submenu_selected);
            Options_menu_Resize(sender, e);
            Button_Box_Resize(sender, e);
            bulk_invalidate();


            ////VTT_table_box.Image;
            //Bitmap grid_image = (Bitmap)grid_picture_Box.Image;
            //ScaleBitmapLogicalToDevice(ref grid_image);
            //Bitmap table_image = (Bitmap)VTT_table_box.Image;
            //if (table_image != null)
            //{
            //    ScaleBitmapLogicalToDevice(ref table_image);
            //}
            //grid_picture_Box.Scale();
            //PerformAutoScale();

            //splitter1.Height = Height;
            //splitter1.SendToBack();
            //splitter1.BackColor = Color.Red;
            //splitter1.Location = new System.Drawing.Point();
        }
        private void VTT_table_box_SizeChanged(object sender, EventArgs e)
        {
            grid_picture_Box.Parent = VTT_table_box;
            grid_picture_Box.Size = VTT_table_box.Size;
            grid_picture_Box.Location = new Point(0, 0);
            current_map.set_Map_box(grid_picture_Box);
            //grid_picture_Box.BackColor = Color.Transparent;
            //float grid_width_float = VTT_table_box.Width / gridsize_default;
            //float grid_height_float = VTT_table_box.Height / gridsize_default;

            //int gridsize_width = (int)Math.Ceiling(grid_width_float);
            //int gridsize_height = (int)Math.Ceiling(grid_height_float);
            //my_grid.set_grid_size(gridsize_width, gridsize_height);
            //grid_picture_Box.Invalidate();
            bulk_invalidate();
        }
        protected void token_picture_Box_paint(object sender, PaintEventArgs e)
        {
            //foreach (PictureBox pic in token_boxes)
            //{
            //    if (pic.Parent!= VTT_table_box)
            //    {
            //        pic.Parent = VTT_table_box;
            //    }
            //}
            current_map.display_token(e);
            
            //grid_picture_Box.BringToFront();
        }
        protected void single_token_picture_Box_paint(object sender, PaintEventArgs e)
        {
            foreach (PictureBox pic in token_boxes) {
                bool same=sender.Equals(pic);

                if (same) 
                //if (pic.Parent != VTT_table_box & same)
                {
                    pic.Parent = VTT_table_box;
                    current_map.display_token_singular(pic,e);
                    pic.BringToFront();

                }
            }

            //grid_picture_Box.BringToFront();
        }
        public void VTT_picture_box_size_calculator()
        {
            if (current_map.get_Map_box().Parent != VTT_table_box)
            {
                current_map.get_Map_box().Parent = VTT_table_box;
            }
            int vtt_height = vtt_box_height_calculator();
            int vtt_width = vtt_box_width_calculator();
            current_map.set_calculated_dimensions(vtt_width, vtt_height);

            current_map.Y_location = vtt_box_y_top_calculator();
            current_map.X_location = vtt_box_x_left_calculator();

            double width_Ratio = (float)vtt_width / current_map.Original_Width;
            double height_Ratio = (float)vtt_height / current_map.Original_Height;
            current_map.Ratio = Math.Min(width_Ratio, height_Ratio);


            VTT_table_box.Height = (int)Math.Ceiling(current_map.Original_Height * current_map.Ratio);
            VTT_table_box.Width = (int)Math.Ceiling(current_map.Original_Width * current_map.Ratio);


            VTT_table_box.Top = current_map.Y_location;
            VTT_table_box.Left = current_map.X_location;

            //current_map.Y_location = vtt_box_y_top_calculator();
            //current_map.X_location = vtt_box_x_left_calculator();

        }

        public void bulk_invalidate()
        {
            if (token_boxes != null)
            {
                //Console.WriteLine("tokenbox is not null");
                if (token_boxes.Count > 0)
                {
                    //Console.WriteLine($"tokenbox:{token_boxes} has size of {token_boxes.Count}");

                    for (int i = 0; i < token_boxes.Count; i++)
                    {
                        //Console.WriteLine("invalidating token box: "+token_boxes[i]);
                        token_boxes[i].Invalidate();
                    }
                }
            }
            grid_picture_Box.Invalidate();


            toolStripStatusLabel1.Text = ("token redrew!");
        }

        //Calculations
        public int vtt_box_height_calculator()
        {

            Rectangle screenRectangle = VTT_table_box.Parent.RectangleToScreen(VTT_table_box.Parent.ClientRectangle);
            int titleHeight_top = screenRectangle.Top - VTT_table_box.Parent.Top;
            int titleHeight_bottom = 0;
            //int titleHeight_bottom =  screenRectangle.Bottom- VTT_table_box.Parent.Bottom;
            int titleHeight = titleHeight_top + titleHeight_bottom;

            //Console.WriteLine($"The title height is{titleHeight} from top: {titleHeight_top} and bottom: {titleHeight_bottom}");
            int vtt_height = -1;
            int modified_height = Button_Box.Height + Button_Box.Margin.Vertical + grid_picture_Box.Margin.Vertical +
                user_info_display.Height + user_info_display.Margin.Vertical + VTT_table_box.Parent.Padding.Vertical + titleHeight;
            vtt_height = VTT_table_box.Parent.Height - modified_height;
            return vtt_height;
        }
        public int vtt_box_width_calculator()
        {
            int vtt_width = -1;

            int titlewidth_right = 0;
            int titlewidth_left = 0;
            Rectangle screenRectangle = VTT_table_box.Parent.RectangleToScreen(VTT_table_box.Parent.ClientRectangle);
            titlewidth_left = screenRectangle.Left - VTT_table_box.Parent.Left;
            // titlewidth_right = screenRectangle.Right - VTT_table_box.Parent.Right;
            int titlewidth = titlewidth_right + titlewidth_left;
            //Console.WriteLine($"Thescreen width offset is {titlewidth} from left: {titlewidth_left} and right: {titlewidth_right}");
            int modified_width = Options_menu.Width + Options_menu.Margin.Horizontal + VTT_table_box.Margin.Horizontal + VTT_table_box.Parent.Padding.Horizontal + titlewidth;
            vtt_width = VTT_table_box.Parent.Width - modified_width;
            return vtt_width;
        }
        public int vtt_box_y_top_calculator()
        {
            int vtt_y = -1;
            //+ Padding.Top
            vtt_y = (user_info_display.Bottom + user_info_display.Margin.Bottom + grid_picture_Box.Margin.Top);
            return vtt_y;
        }
        public int vtt_box_x_left_calculator()
        {
            int vtt_x = -1;
            //+ Padding.Top
            vtt_x = (grid_picture_Box.Margin.Left);
            return vtt_x;
        }

        //Label stuff
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void picture_grabbing_FileOk(object sender, CancelEventArgs e) { }
        private void toolStripStatusLabel1_MouseEnter(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = ("I'm highlighted!");             
        }
        private void toolStripStatusLabel1_MouseLeave(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = ("No longer highlighted!");
            //statusStrip1.Visible = false;
        }

        private void Menu_interact_button_MouseClick(object sender, MouseEventArgs e)
        {
            if (submenu_selected == 0)
            {
                Load_click(sender, e);
            }
            if (submenu_selected == 4)
            {
                Make_new_token();
                //grid_picture_Box.Invalidate();
                bulk_invalidate();
            }

        }
        private void Token_list_box_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            List<Token> tokens_list=current_map.get_token_list();
            PictureBox current = current_box;
            Console.Write($"tokens list:{tokens_list}\nwith a count of { tokens_list.Count()}\n");

            //Token_list_box.Items.Clear();
            //foreach (Token token in tokens_list) { 
            //    Token_list_box.Items.Add(token.name); 
            //}
            //foreach (PictureBox box in token_boxes)
            //{
            //    Console.Write($"box:{box}\n");
            //}
            int selection = Token_list_box.SelectedIndex;
            List<Token> current_token_list= current_map.get_token_list();
            //Console.WriteLine($"current_token_list:{current_token_list}");
            //Console.WriteLine($"tokens_list:{tokens_list.Count()},token_boxes{token_boxes.Count()}");
            Console.WriteLine($"Selection{selection}");
            if (selection < tokens_list.Count() & selection!=-1) { 
                current_box = tokens_list[selection].physical_token;
                Console.WriteLine($"token selected: {tokens_list[selection].name} ");
            }
            current_box = current_box;
            //VTT.
        }

        private void Token_list_box_MouseEnter(object sender, EventArgs e)
        {

            List<Token> tokens_list = current_map.get_token_list();
            PictureBox current = current_box;
            Token_list_box.Items.Clear();
            foreach (Token token in tokens_list)
            {
                Token_list_box.Items.Add(token.name);
            }
            foreach (PictureBox box in token_boxes)
            {
                Console.Write($"box:{box}\n");
            }
        }
    }
    //todo:
}



//unused stuff
/*
 

            if (vtt_ratio == ratio)
            {
                VTT_table_box.Image = image1;
                toolStripStatusLabel1.Text = ("Proper Ratio!");
            }
            if (width > height) {
                Console.WriteLine("width larger than height");
                if (width > VTT_table_box.MaximumSize.Width)
                {

                    int oldwidth = cale_Down_ratio = (double)Width / (double)oldwidth;
                    Console.WriteLine("Scaledown ratio: " + scale_Down_ratio);
                    modified_height = (double)height* ratio;
                    modified_height = modified_height * scale_Down_ratio;
                    double height_check = height * scale_Down_ratio;
                    Console.WriteLine("Width of " + height_check + " Should equal the modified width " + modified_height);


                }
                elsewidth;
                    Console.WriteLine("width of " + oldwidth + " too big, using window size of " + VTT_table_box.MaximumSize.Width + "for width instead");
                    width = VTT_table_box.MaximumSize.Width;
                    //height = VTT_table_box.MaximumSize.Width;
                    VTT_table_box.Width = height;
                    double s
                {
                    VTT_table_box.Width = width;
                    modified_width = (double)width * ratio;
                }
                VTT_table_box.Width = width;
                modified_height = (double)height * ratio;
                Console.WriteLine("the modified height based on the ratio:"+modified_height);
                radtio_height =Math.Ceiling(modified_height);
                Console.WriteLine("pushing the former value to its ceiling:"+radtio_height);
                outheight = (int)radtio_height;
                VTT_table_box.Height = outheight;
                Console.WriteLine("the outout width:"+outheight);

                if (outheight > VTT_table_box.MaximumSize.Height)
                {
                    Console.WriteLine("too big! resizing down");
                    modified_height = (double)VTT_table_box.Height * ratio;
                    Console.WriteLine("the modified width based on the ratio:"+modified_height);
                    radtio_height = Math.Ceiling(modified_height);
                    Console.WriteLine("pushing the former value to its ceiling:"+radtio_height);
                    outheight = (int)radtio_height;

                    VTT_table_box.Height = outheight;
                }

                
                toolStripStatusLabel1.Text = ("Width bigger, ratio:"+ outratio);
                //scale = width;
            }
            else
            {
                //
                //
                //height larger than width
                //
                //

                Console.WriteLine("height larger than width");

                if (height > VTT_table_box.MaximumSize.Height)
                {
                    int oldheight = height;
                    Console.WriteLine("height of "+oldheight+" too big, using window size of "+ VTT_table_box.MaximumSize.Height+"for height instead");
                    height = VTT_table_box.MaximumSize.Height;
                    VTT_table_box.Height = height;
                    double scale_Down_ratio=(double)height/ (double)oldheight;
                    Console.WriteLine("Scaledown ratio: "+scale_Down_ratio);
                    double modified_width_before_ratio= width * scale_Down_ratio;
                    modified_width = modified_width_before_ratio * ratio;
                    double width_check = (origin_img_width * scale_Down_ratio)*ratio;
                    double secondary_width_check = ((modified_width / ratio) / scale_Down_ratio);
                    double height_check = (origin_img_height *scale_Down_ratio);
                    Console.WriteLine("Width of: " + width_check + " Should equal the modified width: " + modified_width);
                    Console.WriteLine("the modified Width of: " + secondary_width_check + " Should equal the original  width: " + origin_img_width);
                    Console.WriteLine("height of: " + height_check + " should equal the maximized height of the window: " + VTT_table_box.MaximumSize.Height);


                }
                else {
                    VTT_table_box.Height = height;
                    modified_width = (double)height * ratio;
                }
                
                Console.WriteLine("the modified width based on the ratio:"+modified_width);
                radtio_width = Math.Ceiling(modified_width);
                Console.WriteLine("pushing the former value to its ceiling:"+radtio_width);              
                outwidth = (int)radtio_width;
                Console.WriteLine("the outout width:"+outwidth);
                VTT_table_box.Width = outwidth;
                if (outwidth > VTT_table_box.MaximumSize.Width)
                {
                    Console.WriteLine("too big! resizing down");
                    modified_width = (double)VTT_table_box.Height * ratio;
                    Console.WriteLine("the modified width based on the ratio:"+modified_width);
                    radtio_width = Math.Ceiling(modified_width);
                    Console.WriteLine("pushing the former value to its ceiling:"+radtio_width);
                    outwidth = (int)radtio_width;

                    VTT_table_box.Width = outwidth;
                }
                
                toolStripStatusLabel1.Text = ("Height bigger, ratio:" + outratio);
            }

            
 */
