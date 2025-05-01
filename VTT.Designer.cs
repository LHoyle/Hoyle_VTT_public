namespace Hoyle_VTT
{
    partial class VTT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Load_map_button = new System.Windows.Forms.Button();
            this.grid_toggle_button = new System.Windows.Forms.Button();
            this.Change_grid_button = new System.Windows.Forms.Button();
            this.Drawing_menu_button = new System.Windows.Forms.Button();
            this.Token_menu_button = new System.Windows.Forms.Button();
            this.Button_Box = new System.Windows.Forms.GroupBox();
            this.user_info_display = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Options_menu = new System.Windows.Forms.GroupBox();
            this.Token_list_box = new System.Windows.Forms.ListBox();
            this.grid_size_box = new System.Windows.Forms.GroupBox();
            this.Grid_height_num = new System.Windows.Forms.NumericUpDown();
            this.Grid_width_num = new System.Windows.Forms.NumericUpDown();
            this.Menu_interact_button = new System.Windows.Forms.Button();
            this.suboptions_Menu = new System.Windows.Forms.ListBox();
            this.map_data_Set = new System.Data.DataSet();
            this.picture_grabbing = new System.Windows.Forms.OpenFileDialog();
            this.Info_bot = new System.Windows.Forms.ToolTip(this.components);
            this.grid_picture_Box = new System.Windows.Forms.PictureBox();
            this.Token_Data_Set = new System.Data.DataSet();
            this.Drawing_Data_set = new System.Data.DataSet();
            this.VTT_table_box = new System.Windows.Forms.PictureBox();
            this.old_grid_Box = new System.Windows.Forms.PictureBox();
            this.Button_Box.SuspendLayout();
            this.user_info_display.SuspendLayout();
            this.Options_menu.SuspendLayout();
            this.grid_size_box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_height_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_width_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.map_data_Set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_picture_Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Token_Data_Set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Drawing_Data_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VTT_table_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.old_grid_Box)).BeginInit();
            this.SuspendLayout();
            // 
            // Load_map_button
            // 
            this.Load_map_button.Cursor = System.Windows.Forms.Cursors.Default;
            this.Load_map_button.Location = new System.Drawing.Point(5, 15);
            this.Load_map_button.Margin = new System.Windows.Forms.Padding(2);
            this.Load_map_button.Name = "Load_map_button";
            this.Load_map_button.Size = new System.Drawing.Size(217, 38);
            this.Load_map_button.TabIndex = 0;
            this.Load_map_button.Text = "Load image";
            this.Load_map_button.UseVisualStyleBackColor = true;
            this.Load_map_button.Click += new System.EventHandler(this.Load_click);
            // 
            // grid_toggle_button
            // 
            this.grid_toggle_button.Location = new System.Drawing.Point(225, 14);
            this.grid_toggle_button.Margin = new System.Windows.Forms.Padding(2);
            this.grid_toggle_button.Name = "grid_toggle_button";
            this.grid_toggle_button.Size = new System.Drawing.Size(217, 38);
            this.grid_toggle_button.TabIndex = 1;
            this.grid_toggle_button.Text = "Toggle Grid";
            this.grid_toggle_button.UseVisualStyleBackColor = true;
            this.grid_toggle_button.Click += new System.EventHandler(this.grid_toggle_Click);
            // 
            // Change_grid_button
            // 
            this.Change_grid_button.Location = new System.Drawing.Point(447, 15);
            this.Change_grid_button.Margin = new System.Windows.Forms.Padding(2);
            this.Change_grid_button.Name = "Change_grid_button";
            this.Change_grid_button.Size = new System.Drawing.Size(217, 38);
            this.Change_grid_button.TabIndex = 2;
            this.Change_grid_button.Text = "Change Grid Type";
            this.Change_grid_button.UseVisualStyleBackColor = true;
            this.Change_grid_button.Click += new System.EventHandler(this.Change_grid_Click);
            // 
            // Drawing_menu_button
            // 
            this.Drawing_menu_button.Location = new System.Drawing.Point(666, 14);
            this.Drawing_menu_button.Margin = new System.Windows.Forms.Padding(2);
            this.Drawing_menu_button.Name = "Drawing_menu_button";
            this.Drawing_menu_button.Size = new System.Drawing.Size(217, 38);
            this.Drawing_menu_button.TabIndex = 3;
            this.Drawing_menu_button.Text = "Drawing";
            this.Drawing_menu_button.UseVisualStyleBackColor = true;
            this.Drawing_menu_button.Click += new System.EventHandler(this.Drawing_click);
            // 
            // Token_menu_button
            // 
            this.Token_menu_button.Location = new System.Drawing.Point(887, 14);
            this.Token_menu_button.Margin = new System.Windows.Forms.Padding(2);
            this.Token_menu_button.Name = "Token_menu_button";
            this.Token_menu_button.Size = new System.Drawing.Size(217, 38);
            this.Token_menu_button.TabIndex = 4;
            this.Token_menu_button.Text = "Tokens";
            this.Token_menu_button.UseVisualStyleBackColor = true;
            this.Token_menu_button.Click += new System.EventHandler(this.Token_menu_Click);
            // 
            // Button_Box
            // 
            this.Button_Box.Controls.Add(this.Load_map_button);
            this.Button_Box.Controls.Add(this.Token_menu_button);
            this.Button_Box.Controls.Add(this.grid_toggle_button);
            this.Button_Box.Controls.Add(this.Drawing_menu_button);
            this.Button_Box.Controls.Add(this.Change_grid_button);
            this.Button_Box.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_Box.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Button_Box.Location = new System.Drawing.Point(4, 615);
            this.Button_Box.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Box.Name = "Button_Box";
            this.Button_Box.Padding = new System.Windows.Forms.Padding(2);
            this.Button_Box.Size = new System.Drawing.Size(1388, 52);
            this.Button_Box.TabIndex = 6;
            this.Button_Box.TabStop = false;
            this.Button_Box.Resize += new System.EventHandler(this.Button_Box_Resize);
            // 
            // user_info_display
            // 
            this.user_info_display.Dock = System.Windows.Forms.DockStyle.Top;
            this.user_info_display.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.user_info_display.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.user_info_display.Location = new System.Drawing.Point(4, 4);
            this.user_info_display.Name = "user_info_display";
            this.user_info_display.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.user_info_display.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.user_info_display.Size = new System.Drawing.Size(1388, 39);
            this.user_info_display.TabIndex = 7;
            this.user_info_display.Text = "feedback display";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(117, 30);
            this.toolStripStatusLabel1.Text = "strip_status";
            this.toolStripStatusLabel1.MouseEnter += new System.EventHandler(this.toolStripStatusLabel1_MouseEnter);
            this.toolStripStatusLabel1.MouseLeave += new System.EventHandler(this.toolStripStatusLabel1_MouseLeave);
            // 
            // Options_menu
            // 
            this.Options_menu.Controls.Add(this.Token_list_box);
            this.Options_menu.Controls.Add(this.grid_size_box);
            this.Options_menu.Controls.Add(this.Menu_interact_button);
            this.Options_menu.Dock = System.Windows.Forms.DockStyle.Right;
            this.Options_menu.Location = new System.Drawing.Point(1175, 43);
            this.Options_menu.Margin = new System.Windows.Forms.Padding(2);
            this.Options_menu.Name = "Options_menu";
            this.Options_menu.Padding = new System.Windows.Forms.Padding(21, 42, 21, 21);
            this.Options_menu.Size = new System.Drawing.Size(217, 572);
            this.Options_menu.TabIndex = 8;
            this.Options_menu.TabStop = false;
            this.Options_menu.Text = "options_menu";
            this.Options_menu.Resize += new System.EventHandler(this.Options_menu_Resize);
            // 
            // Token_list_box
            // 
            this.Token_list_box.FormattingEnabled = true;
            this.Token_list_box.ItemHeight = 24;
            this.Token_list_box.Location = new System.Drawing.Point(27, 90);
            this.Token_list_box.Margin = new System.Windows.Forms.Padding(4);
            this.Token_list_box.Name = "Token_list_box";
            this.Token_list_box.Size = new System.Drawing.Size(149, 364);
            this.Token_list_box.TabIndex = 11;
            this.Token_list_box.SelectedIndexChanged += new System.EventHandler(this.Token_list_box_SelectedIndexChanged_1);
            this.Token_list_box.MouseEnter += new System.EventHandler(this.Token_list_box_MouseEnter);
            // 
            // grid_size_box
            // 
            this.grid_size_box.Controls.Add(this.Grid_height_num);
            this.grid_size_box.Controls.Add(this.Grid_width_num);
            this.grid_size_box.Location = new System.Drawing.Point(5, 469);
            this.grid_size_box.Margin = new System.Windows.Forms.Padding(2);
            this.grid_size_box.Name = "grid_size_box";
            this.grid_size_box.Padding = new System.Windows.Forms.Padding(2);
            this.grid_size_box.Size = new System.Drawing.Size(217, 50);
            this.grid_size_box.TabIndex = 10;
            this.grid_size_box.TabStop = false;
            // 
            // Grid_height_num
            // 
            this.Grid_height_num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid_height_num.Location = new System.Drawing.Point(131, 19);
            this.Grid_height_num.Margin = new System.Windows.Forms.Padding(2);
            this.Grid_height_num.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Grid_height_num.Name = "Grid_height_num";
            this.Grid_height_num.Size = new System.Drawing.Size(78, 29);
            this.Grid_height_num.TabIndex = 11;
            this.Grid_height_num.ValueChanged += new System.EventHandler(this.Grid_height_num_ValueChanged);
            // 
            // Grid_width_num
            // 
            this.Grid_width_num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Grid_width_num.Location = new System.Drawing.Point(19, 19);
            this.Grid_width_num.Margin = new System.Windows.Forms.Padding(2);
            this.Grid_width_num.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Grid_width_num.Name = "Grid_width_num";
            this.Grid_width_num.Size = new System.Drawing.Size(78, 29);
            this.Grid_width_num.TabIndex = 10;
            this.Grid_width_num.ValueChanged += new System.EventHandler(this.Grid_width_num_ValueChanged);
            // 
            // Menu_interact_button
            // 
            this.Menu_interact_button.Location = new System.Drawing.Point(5, 530);
            this.Menu_interact_button.Margin = new System.Windows.Forms.Padding(2);
            this.Menu_interact_button.Name = "Menu_interact_button";
            this.Menu_interact_button.Size = new System.Drawing.Size(190, 33);
            this.Menu_interact_button.TabIndex = 6;
            this.Menu_interact_button.Text = "Menu Interact";
            this.Menu_interact_button.UseVisualStyleBackColor = true;
            this.Menu_interact_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Menu_interact_button_MouseClick);
            // 
            // suboptions_Menu
            // 
            this.suboptions_Menu.DataSource = this.map_data_Set;
            this.suboptions_Menu.FormattingEnabled = true;
            this.suboptions_Menu.ItemHeight = 24;
            this.suboptions_Menu.Location = new System.Drawing.Point(502, 20);
            this.suboptions_Menu.Margin = new System.Windows.Forms.Padding(2);
            this.suboptions_Menu.Name = "suboptions_Menu";
            this.suboptions_Menu.Size = new System.Drawing.Size(191, 364);
            this.suboptions_Menu.TabIndex = 0;
            this.suboptions_Menu.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // map_data_Set
            // 
            this.map_data_Set.DataSetName = "Map_data_Set";
            // 
            // picture_grabbing
            // 
            this.picture_grabbing.Title = "Load Map";
            this.picture_grabbing.FileOk += new System.ComponentModel.CancelEventHandler(this.picture_grabbing_FileOk);
            // 
            // grid_picture_Box
            // 
            this.grid_picture_Box.BackColor = System.Drawing.Color.Transparent;
            this.grid_picture_Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grid_picture_Box.Cursor = System.Windows.Forms.Cursors.Default;
            this.grid_picture_Box.Location = new System.Drawing.Point(4, 43);
            this.grid_picture_Box.Margin = new System.Windows.Forms.Padding(2);
            this.grid_picture_Box.MinimumSize = new System.Drawing.Size(105, 105);
            this.grid_picture_Box.Name = "grid_picture_Box";
            this.grid_picture_Box.Size = new System.Drawing.Size(769, 457);
            this.grid_picture_Box.TabIndex = 9;
            this.grid_picture_Box.TabStop = false;
            this.grid_picture_Box.Click += new System.EventHandler(this.grid_picture_Box_Click);
            // 
            // Token_Data_Set
            // 
            this.Token_Data_Set.DataSetName = "Token Menu data";
            // 
            // Drawing_Data_set
            // 
            this.Drawing_Data_set.DataSetName = "Drawing Data Menu";
            // 
            // VTT_table_box
            // 
            this.VTT_table_box.Cursor = System.Windows.Forms.Cursors.Default;
            this.VTT_table_box.Image = global::Hoyle_VTT.Properties.Resources.notebookscan_1;
            this.VTT_table_box.Location = new System.Drawing.Point(4, 41);
            this.VTT_table_box.Margin = new System.Windows.Forms.Padding(2);
            this.VTT_table_box.MinimumSize = new System.Drawing.Size(105, 105);
            this.VTT_table_box.Name = "VTT_table_box";
            this.VTT_table_box.Size = new System.Drawing.Size(875, 457);
            this.VTT_table_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VTT_table_box.TabIndex = 1;
            this.VTT_table_box.TabStop = false;
            this.VTT_table_box.SizeChanged += new System.EventHandler(this.VTT_table_box_SizeChanged);
            this.VTT_table_box.Click += new System.EventHandler(this.VTT_table_box_Click);
            // 
            // old_grid_Box
            // 
            this.old_grid_Box.BackColor = System.Drawing.Color.Transparent;
            this.old_grid_Box.BackgroundImage = global::Hoyle_VTT.Properties.Resources.graph_paper_grid_png_4;
            this.old_grid_Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.old_grid_Box.Cursor = System.Windows.Forms.Cursors.Default;
            this.old_grid_Box.Location = new System.Drawing.Point(846, 20);
            this.old_grid_Box.Margin = new System.Windows.Forms.Padding(2);
            this.old_grid_Box.MinimumSize = new System.Drawing.Size(105, 105);
            this.old_grid_Box.Name = "old_grid_Box";
            this.old_grid_Box.Size = new System.Drawing.Size(344, 140);
            this.old_grid_Box.TabIndex = 10;
            this.old_grid_Box.TabStop = false;
            // 
            // VTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1396, 671);
            this.Controls.Add(this.old_grid_Box);
            this.Controls.Add(this.suboptions_Menu);
            this.Controls.Add(this.Options_menu);
            this.Controls.Add(this.user_info_display);
            this.Controls.Add(this.Button_Box);
            this.Controls.Add(this.grid_picture_Box);
            this.Controls.Add(this.VTT_table_box);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VTT";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "Hoyle Vitual Table Top";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.VTT_DpiChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VTT_KeyDown);
            this.Resize += new System.EventHandler(this.VTT_Resize);
            this.Button_Box.ResumeLayout(false);
            this.user_info_display.ResumeLayout(false);
            this.user_info_display.PerformLayout();
            this.Options_menu.ResumeLayout(false);
            this.grid_size_box.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_height_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_width_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.map_data_Set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_picture_Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Token_Data_Set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Drawing_Data_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VTT_table_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.old_grid_Box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Load_map_button;
        private System.Windows.Forms.Button grid_toggle_button;
        private System.Windows.Forms.Button Change_grid_button;
        private System.Windows.Forms.Button Drawing_menu_button;
        private System.Windows.Forms.Button Token_menu_button;
        private System.Windows.Forms.GroupBox Button_Box;
        private System.Windows.Forms.StatusStrip user_info_display;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox Options_menu;
        private System.Windows.Forms.OpenFileDialog picture_grabbing;
        private System.Windows.Forms.ToolTip Info_bot;
        private System.Windows.Forms.ListBox suboptions_Menu;
        private System.Windows.Forms.PictureBox grid_picture_Box;
        private System.Windows.Forms.Button Menu_interact_button;
        private System.Data.DataSet map_data_Set;
        private System.Data.DataSet Token_Data_Set;
        private System.Data.DataSet Drawing_Data_set;
        private System.Windows.Forms.PictureBox VTT_table_box;
        private System.Windows.Forms.NumericUpDown Grid_width_num;
        private System.Windows.Forms.NumericUpDown Grid_height_num;
        private System.Windows.Forms.GroupBox grid_size_box;
        private System.Windows.Forms.PictureBox old_grid_Box;
        private System.Windows.Forms.ListBox Token_list_box;
    }
}

