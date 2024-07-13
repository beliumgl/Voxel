using GameLynx;
using GameLynx.AccountSystem;
using Guna.UI2.WinForms;
using Monitoring.APIs.MultiplayerAPI;
using Monitoring.MultiplayerAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring;

public class AddServer : Form
{
    public static byte[] image_bytes = new byte[0];

    private IContainer components;

    private Guna2Panel Panel;

    private Guna2HtmlLabel label2;

    private Guna2TextBox NAME;

    private Label label_add_Server;

    private Guna2Button AddButton;

    private Guna2Button Image;

    private Label label4;

    private Label label3;

    private Guna2TextBox Description;

    private Label label8;

    private Guna2TextBox Port;

    private Label label9;

    private Guna2TextBox IP;

    private Guna2GradientPanel guna2GradientPanel2;

    private Label Selected_IMAGE;

    private Guna2GradientPanel loading;

    private Guna2WinProgressIndicator progressIndicator;

    private Label exit;

    private Guna2CheckBox isLW;

    private Label label1;

    private Label load_text;

    private Guna2AnimateWindow anim;

    public AddServer()
    {
        InitializeComponent();
    }

    private void Image_Click(object sender, EventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG files *.png|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "")
            {
                Selected_IMAGE.Text = openFileDialog.FileName;
                image_bytes = File.ReadAllBytes(openFileDialog.FileName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла ошибка! " + ex.Message);
        }
    }

    private async void AddButton_Click(object sender, EventArgs e)
    {
        if (((CheckBox)(object)isLW).Checked)
        {
            return;
        }
        if (image_bytes.Length > 12 && ((Control)(object)NAME).Text.Length > 2 && ((Control)(object)Description).Text.Length > 10 && ((Control)(object)IP).Text.Length > 6 && ((Control)(object)Port).Text.Length > 2)
        {
            LibMcNetInfo libMcNetInfo = new LibMcNetInfo(((Control)(object)IP).Text, Convert.ToUInt16(((Control)(object)Port).Text));
            if (!libMcNetInfo.ServerUp || libMcNetInfo.Protocol != 0)
            {
                MessageBox.Show("Не удалось проверить сервер.");
                return;
            }
            ((Control)(object)loading).Visible = true;
            exit.Visible = false;
            await Task.Run(async delegate
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    {
                        "key",
                        Voxel.Properties.Settings.Default.Gtm8XVOhghwEc45gi1Y14HdUYhoSV7
                    },
                    {
                        "ip",
                        ((Control)(object)IP).Text
                    },
                    {
                        "port",
                        ((Control)(object)Port).Text
                    },
                    {
                        "desc",
                        ((Control)(object)Description).Text
                    },
                    {
                        "name",
                        ((Control)(object)NAME).Text
                    },
                    {
                        "service",
                        Acc.Service.ToString()
                    },
                    {
                        "avatar",
                        Convert.ToBase64String(image_bytes)
                    },
                    {
                        "username",
                        Acc.Name
                    },
                    {
                        "servid",
                        Acc.ID
                    }
                };
                await BackendConnect.getBackendResponse(v.BACKEND + "/add_bedrock_server.php", dictionary);
                BedrockServer[] serv = await BackendConnect.updateBedrockServersList();
                Close();
                Thread thread = new Thread((ThreadStart)delegate
                {
                    new ServersMain(serv).ShowDialog();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            });
        }
        else
        {
            MessageBox.Show("Не все поля заполнены.");
        }
    }

    private async void exit_Click(object sender, EventArgs e)
    {
        ((Control)(object)loading).Visible = true;
        load_text.Text = "Загрузка...";
        BedrockServer[] array = ((VoxelMC.servers_ == null) ? (await BackendConnect.updateBedrockServersList()) : VoxelMC.servers_);
        BedrockServer[] serv = array;
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new ServersMain(serv).ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    public async Task isVpn()
    {
        VLAN.loaded_vpn = VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith);
        if (!VLAN.loaded_vpn)
        {
            load_text.Text = "Вход в сеть Voxel...";
            ((Control)(object)loading).Visible = true;
            await VLAN.GameLynxNetInit();
            load_text.Text = "Подключение к серверу...";
            ((Control)(object)loading).Visible = false;
        }
    }

    private async void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)(object)isLW).Checked)
        {
            new howcreate().ShowDialog();
            await isVpn();
            VoxelMC.runMinecraft();
            ((Control)(object)loading).Visible = true;
            load_text.Text = "Загрузка...";
            BedrockServer[] serv = await BackendConnect.updateBedrockServersList();
            Close();
            Thread thread = new Thread((ThreadStart)delegate
            {
                new ServersMain(serv).ShowDialog();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        if (!((CheckBox)(object)isLW).Checked)
        {
            ((Control)(object)IP).Enabled = true;
            ((Control)(object)Port).Enabled = true;
        }
    }

    private void NAME_TextChanged(object sender, EventArgs e)
    {
        ((Control)(object)label2).Text = "Название: " + ColorParser.parse(((Control)(object)NAME).Text);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.AddServer));
        this.Panel = new Guna2Panel();
        this.loading = new Guna2GradientPanel();
        this.load_text = new System.Windows.Forms.Label();
        this.progressIndicator = new Guna2WinProgressIndicator();
        this.label8 = new System.Windows.Forms.Label();
        this.Port = new Guna2TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.IP = new Guna2TextBox();
        this.guna2GradientPanel2 = new Guna2GradientPanel();
        this.Image = new Guna2Button();
        this.AddButton = new Guna2Button();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.Description = new Guna2TextBox();
        this.label2 = new Guna2HtmlLabel();
        this.NAME = new Guna2TextBox();
        this.label_add_Server = new System.Windows.Forms.Label();
        this.isLW = new Guna2CheckBox();
        this.label1 = new System.Windows.Forms.Label();
        this.Selected_IMAGE = new System.Windows.Forms.Label();
        this.exit = new System.Windows.Forms.Label();
        this.anim = new Guna2AnimateWindow(this.components);
        ((System.Windows.Forms.Control)(object)this.Panel).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.loading).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).SuspendLayout();
        base.SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.Panel).BackColor = System.Drawing.Color.Transparent;
        this.Panel.BorderRadius = 15;
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.loading);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.label8);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.Port);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.label9);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.IP);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.guna2GradientPanel2);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.AddButton);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.label4);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.label3);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.Description);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.label2);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.NAME);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.label_add_Server);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add((System.Windows.Forms.Control)(object)this.isLW);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.label1);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.Selected_IMAGE);
        ((System.Windows.Forms.Control)(object)this.Panel).Controls.Add(this.exit);
        this.Panel.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.Panel).Location = new System.Drawing.Point(13, 13);
        ((System.Windows.Forms.Control)(object)this.Panel).Name = "Panel";
        ((System.Windows.Forms.Control)(object)this.Panel).Size = new System.Drawing.Size(1181, 729);
        ((System.Windows.Forms.Control)(object)this.Panel).TabIndex = 0;
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add(this.load_text);
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.progressIndicator);
        this.loading.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        this.loading.FillColor2 = System.Drawing.Color.FromArgb(37, 61, 61);
        this.loading.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ((System.Windows.Forms.Control)(object)this.loading).Location = new System.Drawing.Point(-1, 0);
        ((System.Windows.Forms.Control)(object)this.loading).Name = "loading";
        ((System.Windows.Forms.Control)(object)this.loading).Size = new System.Drawing.Size(1182, 729);
        ((System.Windows.Forms.Control)(object)this.loading).TabIndex = 16;
        ((System.Windows.Forms.Control)(object)this.loading).Visible = false;
        this.load_text.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.load_text.ForeColor = System.Drawing.Color.White;
        this.load_text.Location = new System.Drawing.Point(455, 480);
        this.load_text.Name = "load_text";
        this.load_text.Size = new System.Drawing.Size(264, 124);
        this.load_text.TabIndex = 1;
        this.load_text.Text = "Подключение к серверу...";
        this.load_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((Guna2ProgressIndicator)this.progressIndicator).AutoStart = true;
        ((System.Windows.Forms.Control)(object)this.progressIndicator).Location = new System.Drawing.Point(461, 236);
        ((System.Windows.Forms.Control)(object)this.progressIndicator).Name = "progressIndicator";
        ((Guna2ProgressIndicator)this.progressIndicator).NumberOfCircles = 24;
        ((Guna2ProgressIndicator)this.progressIndicator).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.progressIndicator).Size = new System.Drawing.Size(264, 238);
        ((System.Windows.Forms.Control)(object)this.progressIndicator).TabIndex = 0;
        this.label8.AutoSize = true;
        this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label8.ForeColor = System.Drawing.Color.White;
        this.label8.Location = new System.Drawing.Point(25, 332);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(116, 19);
        this.label8.TabIndex = 15;
        this.label8.Text = "Порт сервера";
        this.Port.BorderRadius = 10;
        this.Port.BorderThickness = 0;
        ((System.Windows.Forms.Control)(object)this.Port).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.Port.DefaultText = "";
        this.Port.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.Port.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.Port.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.Port.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.Port.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.Port.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.Port).Font = new System.Drawing.Font("Segoe UI", 9f);
        this.Port.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.Port).Location = new System.Drawing.Point(29, 354);
        ((System.Windows.Forms.Control)(object)this.Port).Name = "Port";
        this.Port.PasswordChar = '\0';
        this.Port.PlaceholderForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
        this.Port.PlaceholderText = "Введите порт вашего сервера...";
        this.Port.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.Port).Size = new System.Drawing.Size(1122, 36);
        ((System.Windows.Forms.Control)(object)this.Port).TabIndex = 14;
        this.label9.AutoSize = true;
        this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label9.ForeColor = System.Drawing.Color.White;
        this.label9.Location = new System.Drawing.Point(25, 261);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(125, 19);
        this.label9.TabIndex = 13;
        this.label9.Text = "Адрес сервера";
        this.IP.BorderRadius = 10;
        this.IP.BorderThickness = 0;
        ((System.Windows.Forms.Control)(object)this.IP).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.IP.DefaultText = "";
        this.IP.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.IP.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.IP.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.IP.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.IP.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.IP.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.IP).Font = new System.Drawing.Font("Segoe UI", 9f);
        this.IP.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.IP).Location = new System.Drawing.Point(29, 283);
        ((System.Windows.Forms.Control)(object)this.IP).Name = "IP";
        this.IP.PasswordChar = '\0';
        this.IP.PlaceholderForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
        this.IP.PlaceholderText = "Введите айпи вашего сервера...";
        this.IP.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.IP).Size = new System.Drawing.Size(1122, 36);
        ((System.Windows.Forms.Control)(object)this.IP).TabIndex = 12;
        this.guna2GradientPanel2.BorderRadius = 15;
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).Controls.Add((System.Windows.Forms.Control)(object)this.Image);
        this.guna2GradientPanel2.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.guna2GradientPanel2.FillColor2 = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).Location = new System.Drawing.Point(29, 423);
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).Name = "guna2GradientPanel2";
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).Size = new System.Drawing.Size(1122, 106);
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).TabIndex = 10;
        this.Image.BorderRadius = 10;
        this.Image.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.Image.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.Image.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.Image.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.Image.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.Image).Font = new System.Drawing.Font("Microsoft YaHei", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.Image).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.Image).Location = new System.Drawing.Point(28, 33);
        ((System.Windows.Forms.Control)(object)this.Image).Name = "Image";
        ((System.Windows.Forms.Control)(object)this.Image).Size = new System.Drawing.Size(1057, 36);
        ((System.Windows.Forms.Control)(object)this.Image).TabIndex = 6;
        ((System.Windows.Forms.Control)(object)this.Image).Text = "Выбрать изображение на ПК...";
        ((System.Windows.Forms.Control)(object)this.Image).Click += new System.EventHandler(Image_Click);
        this.AddButton.BorderRadius = 10;
        this.AddButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.AddButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.AddButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.AddButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.AddButton.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.AddButton).Font = new System.Drawing.Font("Microsoft YaHei", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.AddButton).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.AddButton).Location = new System.Drawing.Point(29, 676);
        ((System.Windows.Forms.Control)(object)this.AddButton).Name = "AddButton";
        ((System.Windows.Forms.Control)(object)this.AddButton).Size = new System.Drawing.Size(1122, 36);
        ((System.Windows.Forms.Control)(object)this.AddButton).TabIndex = 7;
        ((System.Windows.Forms.Control)(object)this.AddButton).Text = "Добавить";
        ((System.Windows.Forms.Control)(object)this.AddButton).Click += new System.EventHandler(AddButton_Click);
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(25, 401);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(121, 19);
        this.label4.TabIndex = 5;
        this.label4.Text = "Изображение";
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(25, 192);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(87, 19);
        this.label3.TabIndex = 4;
        this.label3.Text = "Описание";
        this.Description.BorderRadius = 10;
        this.Description.BorderThickness = 0;
        ((System.Windows.Forms.Control)(object)this.Description).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.Description.DefaultText = "";
        this.Description.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.Description.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.Description.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.Description.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.Description.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.Description.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.Description).Font = new System.Drawing.Font("Segoe UI", 9f);
        this.Description.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.Description).Location = new System.Drawing.Point(29, 214);
        ((System.Windows.Forms.Control)(object)this.Description).Name = "Description";
        this.Description.PasswordChar = '\0';
        this.Description.PlaceholderForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
        this.Description.PlaceholderText = "Минимум 10 символов...";
        this.Description.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.Description).Size = new System.Drawing.Size(1122, 36);
        ((System.Windows.Forms.Control)(object)this.Description).TabIndex = 3;
        ((System.Windows.Forms.Control)(object)this.label2).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.label2).Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.label2).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.label2).Location = new System.Drawing.Point(29, 116);
        ((System.Windows.Forms.Control)(object)this.label2).Name = "label2";
        ((System.Windows.Forms.Control)(object)this.label2).Size = new System.Drawing.Size(83, 21);
        ((System.Windows.Forms.Control)(object)this.label2).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.label2).Text = "Название:";
        this.NAME.BorderRadius = 10;
        this.NAME.BorderThickness = 0;
        ((System.Windows.Forms.Control)(object)this.NAME).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.NAME.DefaultText = "";
        this.NAME.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.NAME.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.NAME.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.NAME.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.NAME.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.NAME.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.NAME).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.NAME).ForeColor = System.Drawing.Color.White;
        this.NAME.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.NAME).Location = new System.Drawing.Point(29, 143);
        ((System.Windows.Forms.Control)(object)this.NAME).Name = "NAME";
        this.NAME.PasswordChar = '\0';
        this.NAME.PlaceholderForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
        this.NAME.PlaceholderText = "Минимум 5 символов...";
        this.NAME.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.NAME).Size = new System.Drawing.Size(1122, 36);
        ((System.Windows.Forms.Control)(object)this.NAME).TabIndex = 1;
        this.NAME.TextChanged += new System.EventHandler(NAME_TextChanged);
        this.label_add_Server.AutoSize = true;
        this.label_add_Server.Font = new System.Drawing.Font("Microsoft YaHei", 36f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label_add_Server.ForeColor = System.Drawing.Color.White;
        this.label_add_Server.Location = new System.Drawing.Point(30, 9);
        this.label_add_Server.Name = "label_add_Server";
        this.label_add_Server.Size = new System.Drawing.Size(694, 64);
        this.label_add_Server.TabIndex = 0;
        this.label_add_Server.Text = "Добавить сервер в список";
        this.isLW.Animated = true;
        ((System.Windows.Forms.Control)(object)this.isLW).AutoSize = true;
        this.isLW.CheckedState.BorderColor = System.Drawing.Color.White;
        this.isLW.CheckedState.BorderRadius = 0;
        this.isLW.CheckedState.BorderThickness = 0;
        this.isLW.CheckedState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.isLW.CheckMarkColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.isLW).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.isLW).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.isLW).Location = new System.Drawing.Point(29, 649);
        ((System.Windows.Forms.Control)(object)this.isLW).Name = "isLW";
        ((System.Windows.Forms.Control)(object)this.isLW).Size = new System.Drawing.Size(129, 21);
        ((System.Windows.Forms.Control)(object)this.isLW).TabIndex = 24;
        ((System.Windows.Forms.Control)(object)this.isLW).Text = "Локальный мир";
        this.isLW.UncheckedState.BorderColor = System.Drawing.Color.White;
        this.isLW.UncheckedState.BorderRadius = 0;
        this.isLW.UncheckedState.BorderThickness = 0;
        this.isLW.UncheckedState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        ((System.Windows.Forms.CheckBox)(object)this.isLW).CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label1.ForeColor = System.Drawing.Color.DimGray;
        this.label1.Location = new System.Drawing.Point(155, 651);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(596, 17);
        this.label1.TabIndex = 25;
        this.label1.Text = "(ваш локальный мир будет доступен пользователям GameLynx, то есть они смогут к вам зайти.)";
        this.Selected_IMAGE.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.Selected_IMAGE.ForeColor = System.Drawing.Color.White;
        this.Selected_IMAGE.Location = new System.Drawing.Point(57, 538);
        this.Selected_IMAGE.Name = "Selected_IMAGE";
        this.Selected_IMAGE.Size = new System.Drawing.Size(1057, 19);
        this.Selected_IMAGE.TabIndex = 9;
        this.Selected_IMAGE.Text = "Ничего не выбрано, выберите изображение вашему серверу.";
        this.Selected_IMAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.exit.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.exit.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.exit.ForeColor = System.Drawing.Color.DarkSlateGray;
        this.exit.Location = new System.Drawing.Point(-1, 0);
        this.exit.Name = "exit";
        this.exit.Size = new System.Drawing.Size(21, 18);
        this.exit.TabIndex = 23;
        this.exit.Text = "↩";
        this.exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.exit.Click += new System.EventHandler(exit_Click);
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.Black;
        base.ClientSize = new System.Drawing.Size(1206, 754);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.Panel);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "AddServer";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "GameLynx » Добавление вашего сервера.";
        ((System.Windows.Forms.Control)(object)this.Panel).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.Panel).PerformLayout();
        ((System.Windows.Forms.Control)(object)this.loading).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2GradientPanel2).ResumeLayout(false);
        base.ResumeLayout(false);
    }
}
