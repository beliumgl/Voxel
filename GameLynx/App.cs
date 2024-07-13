using GameLynx.AccountSystem;
using Guna.UI2.WinForms;
using Monitoring;
using Monitoring.APIs.MultiplayerAPI;
using Monitoring.MultiplayerAPI;
using Monitoring.ServersList;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;
using ButtonState = Guna.UI2.WinForms.Suite.ButtonState;

namespace GameLynx;

public class App : Form
{
    private IContainer components;

    private Guna2Button mc;

    private Label mc_label;

    private Guna2GradientPanel mc_game;

    private Guna2GradientPanel panel;

    private Guna2AnimateWindow Anim;

    private Label name;

    private Guna2PictureBox Avatar;

    private Label Service;

    private Label v;

    private Guna2GradientButton exit;

    private Guna2GradientPanel loading;

    private Guna2WinProgressIndicator l_w;

    private Guna2GradientButton ab;

    private Guna2Panel mine;

    private Guna2Panel acc;

    private Guna2GradientButton howplay;

    private Guna2Panel JavaPanel;

    private Guna2Button java;

    private Label label3;

    private Guna2GradientButton Settings;

    private Label lText;

    private Guna2PictureBox guna2PictureBox1;

    private Guna2ProgressBar progress;

    private Label value;

    private Guna2PictureBox av;

    private Label load_text;

    public Guna2Panel main;

    public App()
    {
        InitializeComponent();
        v.Text = GameLynx.v.version[0] + "." + GameLynx.v.version[1] + "." + GameLynx.v.version[2] + " " + GameLynx.v.version[3];
        ((PictureBox)(object)Avatar).Image = Acc.Avatar;
        name.Text = Acc.Name;
        Service.Text = Utils.searchNameService(Acc.Service);
    }

    private async void mc_Click(object sender, EventArgs e)
    {
        lText.Text = "Загрузка...";
        ((Control)(object)loading).Visible = true;
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

    private void exit_Click(object sender, EventArgs e)
    {
        try
        {
            File.Delete(".\\AccLog.json");
        }
        catch
        {
        }
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            Application.Run(new auth());
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    public async Task isVpn()
    {
        VLAN.loaded_vpn = VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith);
        if (!VLAN.loaded_vpn)
        {
            lText.Text = "Вход в сеть Voxel...";
            ((Control)(object)loading).Visible = true;
            await VLAN.GameLynxNetInit();
            if (!VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith))
            {
                await isVpn();
            }
            ((Control)(object)loading).Visible = false;
        }
    }

    private void ab_Click(object sender, EventArgs e)
    {
        new abApp().ShowDialog();
    }

    private async void java_Click(object sender, EventArgs e)
    {
        lText.Text = "Загрузка...";
        ((Control)(object)loading).Visible = true;
        await Task.Run(async delegate
        {
            JavaWorld[] array = ((VoxelMC.worlds_ == null) ? (await BackendConnect.updateJavaWorldsList()) : VoxelMC.worlds_);
            JavaWorld[] worlds = array;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    Close();
                }));
            }
            Thread thread = new Thread((ThreadStart)delegate
            {
                new javaMultiplayer(worlds).ShowDialog();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        });
    }

    private void howplay_Click(object sender, EventArgs e)
    {
        new howplay().ShowDialog();
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        new Settings().ShowDialog();
    }

    public string getRandomText()
    {
        int maxValue = GameLynx.v.texts.Length;
        int num = new Random().Next(0, maxValue);
        return GameLynx.v.texts[num];
    }

    private async void App_Load(object sender, EventArgs e)
    {
        if (GameLynx.v.isInitApp)
        {
            ((Control)(object)main).Visible = true;
        }
        else
        {
            load_text.Text = getRandomText();
            progress.Value = 0;
            value.Text = "0%, Получение доступа к серверу...";
            await Task.Run(delegate
            {
                GameLynx.v.BACKEND = new WebClient().DownloadString(GameLynx.v.API_CENTRAL + "/download/get_api_url.html").Replace("\n", "");
            });
            progress.Value = 50;
            value.Text = "50%, Получение данных с сервера...";
            await Task.Run(delegate
            {
                VoxelMC.Init();
            });
            progress.Value = 100;
            value.Text = "100%, Данные получены...";
            await Task.Run(delegate
            {
                Thread.Sleep(200);
            });
            GameLynx.v.isInitApp = true;
            ((Control)(object)main).Visible = true;
        }
        await isVpn();
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameLynx.App));
        this.mc_label = new System.Windows.Forms.Label();
        this.mc_game = new Guna2GradientPanel();
        this.Settings = new Guna2GradientButton();
        this.howplay = new Guna2GradientButton();
        this.acc = new Guna2Panel();
        this.name = new System.Windows.Forms.Label();
        this.Avatar = new Guna2PictureBox();
        this.Service = new System.Windows.Forms.Label();
        this.exit = new Guna2GradientButton();
        this.ab = new Guna2GradientButton();
        this.panel = new Guna2GradientPanel();
        this.guna2PictureBox1 = new Guna2PictureBox();
        this.v = new System.Windows.Forms.Label();
        this.Anim = new Guna2AnimateWindow(this.components);
        this.loading = new Guna2GradientPanel();
        this.lText = new System.Windows.Forms.Label();
        this.l_w = new Guna2WinProgressIndicator();
        this.av = new Guna2PictureBox();
        this.value = new System.Windows.Forms.Label();
        this.progress = new Guna2ProgressBar();
        this.mine = new Guna2Panel();
        this.mc = new Guna2Button();
        this.JavaPanel = new Guna2Panel();
        this.java = new Guna2Button();
        this.label3 = new System.Windows.Forms.Label();
        this.main = new Guna2Panel();
        this.load_text = new System.Windows.Forms.Label();
        ((System.Windows.Forms.Control)(object)this.mc_game).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.acc).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.Avatar).BeginInit();
        ((System.Windows.Forms.Control)(object)this.panel).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.guna2PictureBox1).BeginInit();
        ((System.Windows.Forms.Control)(object)this.loading).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.av).BeginInit();
        ((System.Windows.Forms.Control)(object)this.mine).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.JavaPanel).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.main).SuspendLayout();
        base.SuspendLayout();
        this.mc_label.BackColor = System.Drawing.Color.Transparent;
        this.mc_label.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.mc_label.ForeColor = System.Drawing.Color.White;
        this.mc_label.Location = new System.Drawing.Point(48, 247);
        this.mc_label.Name = "mc_label";
        this.mc_label.Size = new System.Drawing.Size(180, 64);
        this.mc_label.TabIndex = 1;
        this.mc_label.Text = "Minecraft: Bedrock Edition";
        this.mc_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((System.Windows.Forms.Control)(object)this.mc_game).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.mc_game).Controls.Add((System.Windows.Forms.Control)(object)this.Settings);
        ((System.Windows.Forms.Control)(object)this.mc_game).Controls.Add((System.Windows.Forms.Control)(object)this.howplay);
        ((System.Windows.Forms.Control)(object)this.mc_game).Controls.Add((System.Windows.Forms.Control)(object)this.acc);
        ((System.Windows.Forms.Control)(object)this.mc_game).Controls.Add((System.Windows.Forms.Control)(object)this.exit);
        ((System.Windows.Forms.Control)(object)this.mc_game).Controls.Add((System.Windows.Forms.Control)(object)this.ab);
        ((System.Windows.Forms.Control)(object)this.mc_game).Dock = System.Windows.Forms.DockStyle.Left;
        this.mc_game.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        this.mc_game.FillColor2 = System.Drawing.Color.FromArgb(37, 61, 61);
        this.mc_game.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ((System.Windows.Forms.Control)(object)this.mc_game).Location = new System.Drawing.Point(0, 0);
        ((System.Windows.Forms.Control)(object)this.mc_game).Name = "mc_game";
        ((System.Windows.Forms.Control)(object)this.mc_game).Size = new System.Drawing.Size(251, 638);
        ((System.Windows.Forms.Control)(object)this.mc_game).TabIndex = 2;
        this.Settings.BorderColor = System.Drawing.Color.Transparent;
        this.Settings.BorderRadius = 10;
        this.Settings.BorderThickness = 1;
        ((ButtonState)this.Settings.DisabledState).BorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.Settings.DisabledState).CustomBorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.Settings.DisabledState).FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.Settings.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
        ((ButtonState)this.Settings.DisabledState).ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.Settings.FillColor = System.Drawing.Color.DarkSlateGray;
        this.Settings.FillColor2 = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.Settings).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.Settings).ForeColor = System.Drawing.Color.White;
        this.Settings.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ((ButtonState)this.Settings.HoverState).BorderColor = System.Drawing.Color.White;
        ((ButtonState)this.Settings.HoverState).FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        this.Settings.HoverState.FillColor2 = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.Settings).Location = new System.Drawing.Point(20, 239);
        ((System.Windows.Forms.Control)(object)this.Settings).Name = "Settings";
        ((System.Windows.Forms.Control)(object)this.Settings).Size = new System.Drawing.Size(203, 47);
        ((System.Windows.Forms.Control)(object)this.Settings).TabIndex = 21;
        ((System.Windows.Forms.Control)(object)this.Settings).Text = "Настройки";
        ((System.Windows.Forms.Control)(object)this.Settings).Click += new System.EventHandler(Settings_Click);
        this.howplay.BorderColor = System.Drawing.Color.Transparent;
        this.howplay.BorderRadius = 10;
        this.howplay.BorderThickness = 1;
        ((ButtonState)this.howplay.DisabledState).BorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.howplay.DisabledState).CustomBorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.howplay.DisabledState).FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.howplay.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
        ((ButtonState)this.howplay.DisabledState).ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.howplay.FillColor = System.Drawing.Color.DarkSlateGray;
        this.howplay.FillColor2 = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.howplay).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.howplay).ForeColor = System.Drawing.Color.White;
        this.howplay.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ((ButtonState)this.howplay.HoverState).BorderColor = System.Drawing.Color.White;
        ((ButtonState)this.howplay.HoverState).FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        this.howplay.HoverState.FillColor2 = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.howplay).Location = new System.Drawing.Point(20, 173);
        ((System.Windows.Forms.Control)(object)this.howplay).Name = "howplay";
        ((System.Windows.Forms.Control)(object)this.howplay).Size = new System.Drawing.Size(203, 47);
        ((System.Windows.Forms.Control)(object)this.howplay).TabIndex = 17;
        ((System.Windows.Forms.Control)(object)this.howplay).Text = "Как пользоваться мультиплеером?";
        ((System.Windows.Forms.Control)(object)this.howplay).Click += new System.EventHandler(howplay_Click);
        ((System.Windows.Forms.Control)(object)this.acc).Controls.Add(this.name);
        ((System.Windows.Forms.Control)(object)this.acc).Controls.Add((System.Windows.Forms.Control)(object)this.Avatar);
        ((System.Windows.Forms.Control)(object)this.acc).Controls.Add(this.Service);
        this.acc.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.acc).Location = new System.Drawing.Point(0, 0);
        ((System.Windows.Forms.Control)(object)this.acc).Name = "acc";
        ((System.Windows.Forms.Control)(object)this.acc).Size = new System.Drawing.Size(251, 83);
        ((System.Windows.Forms.Control)(object)this.acc).TabIndex = 16;
        this.name.AutoSize = true;
        this.name.BackColor = System.Drawing.Color.Transparent;
        this.name.Font = new System.Drawing.Font("Microsoft YaHei", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.name.ForeColor = System.Drawing.Color.White;
        this.name.Location = new System.Drawing.Point(75, 33);
        this.name.Name = "name";
        this.name.Size = new System.Drawing.Size(82, 19);
        this.name.TabIndex = 10;
        this.name.Text = "BedrockNet\r\n";
        this.name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        ((System.Windows.Forms.Control)(object)this.Avatar).BackColor = System.Drawing.Color.Transparent;
        this.Avatar.BorderRadius = 5;
        this.Avatar.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.Avatar).Location = new System.Drawing.Point(12, 12);
        ((System.Windows.Forms.Control)(object)this.Avatar).Name = "Avatar";
        ((System.Windows.Forms.Control)(object)this.Avatar).Size = new System.Drawing.Size(57, 57);
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).TabIndex = 9;
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).TabStop = false;
        this.Service.AutoSize = true;
        this.Service.BackColor = System.Drawing.Color.Transparent;
        this.Service.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.Service.ForeColor = System.Drawing.Color.Silver;
        this.Service.Location = new System.Drawing.Point(76, 52);
        this.Service.Name = "Service";
        this.Service.Size = new System.Drawing.Size(69, 16);
        this.Service.TabIndex = 11;
        this.Service.Text = "BedrockNet\r\n";
        this.Service.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.exit.BorderColor = System.Drawing.Color.Transparent;
        this.exit.BorderRadius = 10;
        this.exit.BorderThickness = 1;
        ((ButtonState)this.exit.DisabledState).BorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.exit.DisabledState).CustomBorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.exit.DisabledState).FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.exit.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
        ((ButtonState)this.exit.DisabledState).ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.exit.FillColor = System.Drawing.Color.DarkSlateGray;
        this.exit.FillColor2 = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.exit).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.exit).ForeColor = System.Drawing.Color.White;
        this.exit.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ((ButtonState)this.exit.HoverState).BorderColor = System.Drawing.Color.White;
        ((ButtonState)this.exit.HoverState).FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        this.exit.HoverState.FillColor2 = System.Drawing.Color.Transparent;
        this.exit.Image = Voxel.Properties.Resources.exit;
        ((System.Windows.Forms.Control)(object)this.exit).Location = new System.Drawing.Point(20, 571);
        ((System.Windows.Forms.Control)(object)this.exit).Name = "exit";
        ((System.Windows.Forms.Control)(object)this.exit).Size = new System.Drawing.Size(203, 55);
        ((System.Windows.Forms.Control)(object)this.exit).TabIndex = 13;
        ((System.Windows.Forms.Control)(object)this.exit).Text = "Выйти из аккаунта";
        ((System.Windows.Forms.Control)(object)this.exit).Click += new System.EventHandler(exit_Click);
        this.ab.BorderColor = System.Drawing.Color.Transparent;
        this.ab.BorderRadius = 10;
        this.ab.BorderThickness = 1;
        ((ButtonState)this.ab.DisabledState).BorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.ab.DisabledState).CustomBorderColor = System.Drawing.Color.DarkGray;
        ((ButtonState)this.ab.DisabledState).FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.ab.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(169, 169, 169);
        ((ButtonState)this.ab.DisabledState).ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.ab.FillColor = System.Drawing.Color.DarkSlateGray;
        this.ab.FillColor2 = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.ab).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.ab).ForeColor = System.Drawing.Color.White;
        this.ab.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ((ButtonState)this.ab.HoverState).BorderColor = System.Drawing.Color.White;
        ((ButtonState)this.ab.HoverState).FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        this.ab.HoverState.FillColor2 = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.ab).Location = new System.Drawing.Point(20, 107);
        ((System.Windows.Forms.Control)(object)this.ab).Name = "ab";
        ((System.Windows.Forms.Control)(object)this.ab).Size = new System.Drawing.Size(203, 47);
        ((System.Windows.Forms.Control)(object)this.ab).TabIndex = 15;
        ((System.Windows.Forms.Control)(object)this.ab).Text = "О приложении";
        ((System.Windows.Forms.Control)(object)this.ab).Click += new System.EventHandler(ab_Click);
        ((System.Windows.Forms.Control)(object)this.panel).Controls.Add((System.Windows.Forms.Control)(object)this.guna2PictureBox1);
        ((System.Windows.Forms.Control)(object)this.panel).Dock = System.Windows.Forms.DockStyle.Top;
        this.panel.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        this.panel.FillColor2 = System.Drawing.Color.FromArgb(37, 61, 61);
        this.panel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        ((System.Windows.Forms.Control)(object)this.panel).Location = new System.Drawing.Point(251, 0);
        ((System.Windows.Forms.Control)(object)this.panel).Name = "panel";
        ((System.Windows.Forms.Control)(object)this.panel).Size = new System.Drawing.Size(1174, 83);
        ((System.Windows.Forms.Control)(object)this.panel).TabIndex = 3;
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).BackColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).Image = Voxel.Properties.Resources.title;
        this.guna2PictureBox1.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).Location = new System.Drawing.Point(0, 12);
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).Name = "guna2PictureBox1";
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).Size = new System.Drawing.Size(1178, 61);
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).TabIndex = 13;
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).TabStop = false;
        this.v.BackColor = System.Drawing.Color.Transparent;
        this.v.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.v.ForeColor = System.Drawing.Color.Silver;
        this.v.Location = new System.Drawing.Point(1257, 610);
        this.v.Name = "v";
        this.v.Size = new System.Drawing.Size(154, 16);
        this.v.TabIndex = 12;
        this.v.Text = "Version";
        this.v.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.Anim.AnimationType = (AnimateWindowType)524288;
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        ((System.Windows.Forms.Control)(object)this.loading).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add(this.lText);
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.l_w);
        this.loading.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        this.loading.FillColor2 = System.Drawing.Color.FromArgb(37, 61, 61);
        this.loading.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        ((System.Windows.Forms.Control)(object)this.loading).Location = new System.Drawing.Point(0, 0);
        ((System.Windows.Forms.Control)(object)this.loading).Name = "loading";
        ((System.Windows.Forms.Control)(object)this.loading).Size = new System.Drawing.Size(1424, 646);
        ((System.Windows.Forms.Control)(object)this.loading).TabIndex = 14;
        ((System.Windows.Forms.Control)(object)this.loading).Visible = false;
        this.lText.Font = new System.Drawing.Font("Microsoft YaHei", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.lText.ForeColor = System.Drawing.Color.White;
        this.lText.Location = new System.Drawing.Point(563, 422);
        this.lText.Name = "lText";
        this.lText.Size = new System.Drawing.Size(304, 76);
        this.lText.TabIndex = 1;
        this.lText.Text = "Вход в сеть Voxel...";
        this.lText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((Guna2ProgressIndicator)this.l_w).AutoStart = true;
        ((Guna2ProgressIndicator)this.l_w).CircleSize = 3f;
        ((System.Windows.Forms.Control)(object)this.l_w).Location = new System.Drawing.Point(593, 150);
        ((System.Windows.Forms.Control)(object)this.l_w).Name = "l_w";
        ((Guna2ProgressIndicator)this.l_w).NumberOfCircles = 24;
        ((Guna2ProgressIndicator)this.l_w).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.l_w).Size = new System.Drawing.Size(245, 245);
        ((System.Windows.Forms.Control)(object)this.l_w).TabIndex = 0;
        if (!GameLynx.v.isInitApp)
        {
            this.av.BorderRadius = 20;
            this.av.FillColor = System.Drawing.Color.Transparent;
            ((System.Windows.Forms.PictureBox)(object)this.av).Image = Voxel.Properties.Resources.gamelynx;
            this.av.ImageRotate = 0f;
            ((System.Windows.Forms.Control)(object)this.av).Location = new System.Drawing.Point(589, 107);
            ((System.Windows.Forms.Control)(object)this.av).Name = "av";
            ((System.Windows.Forms.Control)(object)this.av).Size = new System.Drawing.Size(245, 245);
            ((System.Windows.Forms.PictureBox)(object)this.av).SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            ((System.Windows.Forms.PictureBox)(object)this.av).TabIndex = 4;
            ((System.Windows.Forms.PictureBox)(object)this.av).TabStop = false;
            this.value.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            this.value.ForeColor = System.Drawing.Color.White;
            this.value.Location = new System.Drawing.Point(559, 481);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(300, 19);
            this.value.TabIndex = 3;
            this.value.Text = "0%, Получение данных сервера...";
            this.value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.load_text.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            this.load_text.ForeColor = System.Drawing.Color.White;
            this.load_text.Location = new System.Drawing.Point(319, 372);
            this.load_text.Name = "load_text";
            this.load_text.Size = new System.Drawing.Size(784, 91);
            this.load_text.TabIndex = 2;
            this.load_text.Text = "Вход в сеть Voxel...";
            this.load_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            ((System.Windows.Forms.Control)(object)this.progress).BackColor = System.Drawing.Color.Transparent;
            this.progress.BorderRadius = 10;
            this.progress.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
            ((System.Windows.Forms.Control)(object)this.progress).Location = new System.Drawing.Point(559, 503);
            ((System.Windows.Forms.Control)(object)this.progress).Name = "progress";
            this.progress.ProgressColor = System.Drawing.Color.FromArgb(37, 61, 61);
            this.progress.ProgressColor2 = System.Drawing.Color.DarkSlateGray;
            ((System.Windows.Forms.Control)(object)this.progress).Size = new System.Drawing.Size(300, 30);
            ((System.Windows.Forms.Control)(object)this.progress).TabIndex = 2;
            this.progress.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
        }
        this.mine.BorderRadius = 15;
        ((System.Windows.Forms.Control)(object)this.mine).Controls.Add((System.Windows.Forms.Control)(object)this.mc);
        ((System.Windows.Forms.Control)(object)this.mine).Controls.Add(this.mc_label);
        this.mine.FillColor = System.Drawing.Color.FromArgb(35, 35, 35);
        ((System.Windows.Forms.Control)(object)this.mine).Location = new System.Drawing.Point(292, 125);
        ((System.Windows.Forms.Control)(object)this.mine).Name = "mine";
        ((System.Windows.Forms.Control)(object)this.mine).Size = new System.Drawing.Size(274, 338);
        ((System.Windows.Forms.Control)(object)this.mine).TabIndex = 15;
        ((System.Windows.Forms.Control)(object)this.mc).BackColor = System.Drawing.Color.Transparent;
        this.mc.BorderColor = System.Drawing.Color.Transparent;
        this.mc.BorderRadius = 10;
        this.mc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.mc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.mc.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.mc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.mc.FillColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.mc).Font = new System.Drawing.Font("Segoe UI Black", 9f, System.Drawing.FontStyle.Bold);
        ((System.Windows.Forms.Control)(object)this.mc).ForeColor = System.Drawing.Color.White;
        this.mc.HoverState.FillColor = System.Drawing.Color.Transparent;
        this.mc.HoverState.Image = Voxel.Properties.Resources.minecraft_dark;
        this.mc.Image = Voxel.Properties.Resources.minecraft;
        this.mc.ImageSize = new System.Drawing.Size(185, 185);
        ((System.Windows.Forms.Control)(object)this.mc).Location = new System.Drawing.Point(30, 35);
        ((System.Windows.Forms.Control)(object)this.mc).Name = "mc";
        this.mc.PressedColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.mc).Size = new System.Drawing.Size(221, 185);
        ((System.Windows.Forms.Control)(object)this.mc).TabIndex = 1;
        ((System.Windows.Forms.Control)(object)this.mc).Click += new System.EventHandler(mc_Click);
        this.JavaPanel.BorderRadius = 15;
        ((System.Windows.Forms.Control)(object)this.JavaPanel).Controls.Add((System.Windows.Forms.Control)(object)this.java);
        ((System.Windows.Forms.Control)(object)this.JavaPanel).Controls.Add(this.label3);
        this.JavaPanel.FillColor = System.Drawing.Color.FromArgb(35, 35, 35);
        ((System.Windows.Forms.Control)(object)this.JavaPanel).Location = new System.Drawing.Point(593, 125);
        ((System.Windows.Forms.Control)(object)this.JavaPanel).Name = "JavaPanel";
        ((System.Windows.Forms.Control)(object)this.JavaPanel).Size = new System.Drawing.Size(274, 338);
        ((System.Windows.Forms.Control)(object)this.JavaPanel).TabIndex = 16;
        ((System.Windows.Forms.Control)(object)this.java).BackColor = System.Drawing.Color.Transparent;
        this.java.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.java.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.java.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.java.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.java.FillColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.java).Font = new System.Drawing.Font("Segoe UI Black", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.java).ForeColor = System.Drawing.Color.White;
        this.java.HoverState.FillColor = System.Drawing.Color.Transparent;
        this.java.HoverState.Image = Voxel.Properties.Resources.mcJava_dark;
        this.java.Image = Voxel.Properties.Resources.mcJava;
        this.java.ImageSize = new System.Drawing.Size(185, 198);
        ((System.Windows.Forms.Control)(object)this.java).Location = new System.Drawing.Point(34, 25);
        ((System.Windows.Forms.Control)(object)this.java).Name = "java";
        this.java.PressedColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.java).Size = new System.Drawing.Size(212, 200);
        ((System.Windows.Forms.Control)(object)this.java).TabIndex = 1;
        ((System.Windows.Forms.Control)(object)this.java).Click += new System.EventHandler(java_Click);
        this.label3.BackColor = System.Drawing.Color.Transparent;
        this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(48, 247);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(180, 64);
        this.label3.TabIndex = 1;
        this.label3.Text = "Minecraft: Java Edition";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((System.Windows.Forms.Control)(object)this.main).BackColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.main).Controls.Add((System.Windows.Forms.Control)(object)this.loading);
        ((System.Windows.Forms.Control)(object)this.main).Controls.Add(this.v);
        ((System.Windows.Forms.Control)(object)this.main).Controls.Add((System.Windows.Forms.Control)(object)this.panel);
        ((System.Windows.Forms.Control)(object)this.main).Controls.Add((System.Windows.Forms.Control)(object)this.mine);
        ((System.Windows.Forms.Control)(object)this.main).Controls.Add((System.Windows.Forms.Control)(object)this.mc_game);
        ((System.Windows.Forms.Control)(object)this.main).Controls.Add((System.Windows.Forms.Control)(object)this.JavaPanel);
        ((System.Windows.Forms.Control)(object)this.main).Location = new System.Drawing.Point(-2, 0);
        ((System.Windows.Forms.Control)(object)this.main).Name = "main";
        ((System.Windows.Forms.Control)(object)this.main).Size = new System.Drawing.Size(1425, 638);
        ((System.Windows.Forms.Control)(object)this.main).TabIndex = 17;
        ((System.Windows.Forms.Control)(object)this.main).Visible = GameLynx.v.isInitApp;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(20, 20, 20);
        base.ClientSize = new System.Drawing.Size(1423, 638);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.main);
        if (!GameLynx.v.isInitApp)
        {
            base.Controls.Add(this.load_text);
            base.Controls.Add(this.value);
            base.Controls.Add((System.Windows.Forms.Control)(object)this.av);
            base.Controls.Add((System.Windows.Forms.Control)(object)this.progress);
        }
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "App";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Меню.";
        base.Load += new System.EventHandler(App_Load);
        ((System.Windows.Forms.Control)(object)this.mc_game).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.acc).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.acc).PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.Avatar).EndInit();
        ((System.Windows.Forms.Control)(object)this.panel).ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.guna2PictureBox1).EndInit();
        ((System.Windows.Forms.Control)(object)this.loading).ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.av).EndInit();
        ((System.Windows.Forms.Control)(object)this.mine).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.JavaPanel).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.main).ResumeLayout(false);
        base.ResumeLayout(false);
    }
}
