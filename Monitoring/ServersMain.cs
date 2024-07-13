using GameLynx;
using GameLynx.AccountSystem;
using Guna.UI2.WinForms;
using Monitoring.MultiplayerAPI;
using Monitoring.MyList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Properties;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring;

public class ServersMain : Form
{
    public int location_Y = 55;

    public int location_X = 27;

    public static BedrockServer[] ServersList;

    private int ServersCount;

    private IContainer components;

    private Guna2Button my_servers;

    private Guna2Button update_servers;

    private Guna2AnimateWindow Anim;

    private Guna2Button play;

    private Panel bg;

    private Guna2GradientPanel loading;

    private Label L_text;

    private Guna2WinProgressIndicator vpn_l;

    private Label label2;

    private Guna2Panel Account;

    private Guna2PictureBox Avatar;

    private Label name;

    private Label label1;

    private Guna2Button ServersButton;

    private Guna2Panel Servers;

    private Label Nothing;

    private Guna2Button add_server;

    private Label exit;

    public ServersMain(BedrockServer[] servers)
    {
        ServersList = servers;
        InitializeComponent();
        ServersCount = servers.Length;
        name.Text = Acc.Name;
        ((PictureBox)(object)Avatar).Image = Acc.Avatar;
    }

    public async Task isVpn(bool init)
    {
        VLAN.loaded_vpn = VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith);
        if (!VLAN.loaded_vpn)
        {
            L_text.Text = "Вход в сеть Voxel...";
            ((Control)(object)loading).Visible = true;
            await VLAN.GameLynxNetInit();
            if (!VoxelMC.networkStartsWith.StartsWith(VoxelMC.networkStartsWith))
            {
                await isVpn(init: false);
            }
            ((Control)(object)loading).Visible = false;
        }
        if (init)
        {
            await INIT_SERVERS();
        }
    }

    public async Task INIT_SERVERS()
    {
        await Task.Run(async delegate
        {
            if (ServersList.Length != 0)
            {
                for (int i = 0; i <= ServersList.Length - 1; i++)
                {
                    BedrockServer bedrockServer = ServersList[i];
                    string iP = bedrockServer.IP;
                    ushort port = Convert.ToUInt16(bedrockServer.Port);
                    LibMcNetInfo libMcNetInfo = new LibMcNetInfo(iP, port);
                    if (libMcNetInfo.ServerUp)
                    {
                        AddToPanel(bedrockServer.ID, libMcNetInfo, bedrockServer, bedrockServer.Name, bedrockServer.IP, Convert.ToInt32(bedrockServer.Port), bedrockServer.Avatar, bedrockServer.Description, location_Y, location_X);
                    }
                    else
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>
                        {
                            {
                                "key",
                                Voxel.Properties.Settings.Default.Gtm8XVOhghwEc45gi1Y14HdUYhoSV7
                            },
                            { "value", bedrockServer.ID },
                            { "param", "ID" },
                            { "table", "ServersList" }
                        };
                        await BackendConnect.getBackendResponse(v.BACKEND + "/del_content.php", dictionary);
                        ServersCount--;
                    }
                }
            }
        });
    }

    public void isNothing()
    {
        if (ServersCount == 0)
        {
            Nothing.Visible = true;
        }
        else
        {
            Nothing.Visible = false;
        }
    }

    public void AddToPanel(string id, LibMcNetInfo stat, BedrockServer serv, string name, string ip, int port, string base64_avatar, string description, int Location_Y, int Location_X)
    {
        try
        {
            if (method_0())
            {
                Invoke((MethodInvoker)delegate
                {
                    AddToPanel(id, stat, serv, name, ip, port, base64_avatar, description, Location_Y, Location_X);
                });
                return;
            }
        }
        catch
        {
        }
        Guna2Panel val = new Guna2Panel();
        ((Control)(object)val).Location = new Point(Location_X, Location_Y);
        ((Control)(object)val).Size = new Size(1063, 80);
        val.FillColor = Color.FromArgb(20, 20, 20);
        val.BorderRadius = 10;
        ((Control)(object)Servers).Controls.Add((Control)(object)val);
        Guna2PictureBox val2 = new Guna2PictureBox();
        ((Control)(object)val2).Size = new Size(69, 69);
        ((Control)(object)val2).BackgroundImageLayout = ImageLayout.Stretch;
        if (base64_avatar != "NULL")
        {
            ((PictureBox)(object)val2).Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64_avatar)));
        }
        else
        {
            ((PictureBox)(object)val2).Image = Resources.gamelynx;
        }
        ((Control)(object)val2).Location = new Point(14, 6);
        val2.BorderRadius = 10;
        ((PictureBox)(object)val2).SizeMode = PictureBoxSizeMode.StretchImage;
        Guna2HtmlLabel val3 = new Guna2HtmlLabel();
        ((Control)(object)val3).Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold, GraphicsUnit.Point);
        ((Control)(object)val3).Size = new Size(12 * name.Length, ((Control)(object)val3).Height);
        ((Control)(object)val3).Text = ColorParser.parse(name);
        ((Control)(object)val3).Location = new Point(98, 7);
        ((Control)(object)val3).ForeColor = Color.White;
        Label label = new Label();
        label.Location = new Point(99, 59);
        stat.Gamemode = gamemode(stat.Gamemode);
        label.ForeColor = Color.Silver;
        label.Text = "• " + stat.ver_bedrock + "  • " + stat.CurrentPlayers + " из " + stat.MaximumPlayers + $"  • {stat.Gamemode}  • Пинг {stat.Latency} мс";
        label.Font = new Font("Microsoft YaHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
        label.Size = new Size(8 * label.Text.Length, label.Height);
        Label label2 = new Label();
        label2.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point);
        label2.Text = description;
        label2.Size = new Size(9 * description.Length, label2.Height);
        if ((float)label2.Width > 9f)
        {
            label2.AutoSize = false;
        }
        label2.Size = new Size(727, 31);
        label2.Location = new Point(99, 28);
        label2.ForeColor = Color.Silver;
        if (label2.Text.Length >= 100)
        {
            label2.Text = label2.Text.Substring(0, 97) + "...";
        }
        Guna2Button val4 = new Guna2Button();
        val4.BorderRadius = 10;
        ((Control)(object)val4).Text = "Перейти к игре";
        val4.FillColor = Color.FromArgb(37, 61, 61);
        ((Control)(object)val4).BackColor = Color.Transparent;
        ((Control)(object)val4).ForeColor = Color.White;
        ((Control)(object)val4).Font = new Font("Microsoft YaHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
        ((Control)(object)val4).Location = new Point(852, 19);
        ((Control)(object)val4).Size = new Size(180, 45);
        ((Control)(object)val4).Click += delegate (object sender, EventArgs e)
        {
            PlayButton(sender, e, serv, stat, ip, port, name, base64_avatar, description);
        };
        ((Control)(object)val).Controls.Add(label2);
        ((Control)(object)val).Controls.Add((Control)(object)val4);
        ((Control)(object)val).Controls.Add((Control)(object)val3);
        ((Control)(object)val).Controls.Add(label);
        ((Control)(object)val).Controls.Add((Control)(object)val2);
        ((Control)(object)Servers).Controls.Add((Control)(object)val);
        location_Y += 95;
    }

    public static string gamemode(string gamemode)
    {
        return gamemode switch
        {
            "Hardcore" => "Хардкор",
            "Adventure" => "Приключение",
            "Creative" => "Творческий",
            "Survival" => "Выживание",
            _ => null,
        };
    }

    public void PlayButton(object sender, EventArgs e, BedrockServer serv, LibMcNetInfo stat, string ip, int port, string name, string avatar, string Desription)
    {
        try
        {
            string str = new string(Path.GetInvalidFileNameChars());
            string regexPattern = $"[{Regex.Escape(str)}]";
            Regex.Replace(name, regexPattern, "");
            Close();
            Thread thread = new Thread((ThreadStart)delegate
            {
                new ServerInfoPlay(avatar, Regex.Replace(name, regexPattern, ""), Desription, serv, ip, port, stat).ShowDialog();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void ServersMain_Load(object sender, EventArgs e)
    {
        await isVpn(init: true);
        isNothing();
    }

    private void add_server_Click(object sender, EventArgs e)
    {
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new AddServer().ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    private async void update_servers_Click(object sender, EventArgs e)
    {
        L_text.Text = "Загрузка...";
        ((Control)(object)loading).Visible = true;
        BedrockServer[] serv = await BackendConnect.updateBedrockServersList();
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new ServersMain(serv).ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    private void my_servers_Click(object sender, EventArgs e)
    {
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new List(ServersList).ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    private async void play_Click(object sender, EventArgs e)
    {
        ((Control)(object)loading).Visible = true;
        await Task.Run(async delegate
        {
            await isVpn(init: false);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    L_text.Text = "Запуск Minecraft...";
                }));
            }
            VoxelMC.runMinecraft();
            ((Control)(object)loading).Visible = false;
        });
    }

    private void exit_Click(object sender, EventArgs e)
    {
        Application.Exit();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new App().ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.ServersMain));
        this.play = new Guna2Button();
        this.update_servers = new Guna2Button();
        this.my_servers = new Guna2Button();
        this.Anim = new Guna2AnimateWindow(this.components);
        this.exit = new System.Windows.Forms.Label();
        this.Servers = new Guna2Panel();
        this.Nothing = new System.Windows.Forms.Label();
        this.add_server = new Guna2Button();
        this.ServersButton = new Guna2Button();
        this.label1 = new System.Windows.Forms.Label();
        this.Account = new Guna2Panel();
        this.Avatar = new Guna2PictureBox();
        this.name = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.loading = new Guna2GradientPanel();
        this.L_text = new System.Windows.Forms.Label();
        this.vpn_l = new Guna2WinProgressIndicator();
        this.bg = new System.Windows.Forms.Panel();
        ((System.Windows.Forms.Control)(object)this.Servers).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.Account).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.Avatar).BeginInit();
        ((System.Windows.Forms.Control)(object)this.loading).SuspendLayout();
        this.bg.SuspendLayout();
        base.SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.play).BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.play.BorderColor = System.Drawing.Color.Transparent;
        this.play.BorderRadius = 5;
        this.play.BorderThickness = 1;
        this.play.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.play.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.play.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.play.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.play.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.play).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.play).ForeColor = System.Drawing.Color.White;
        this.play.HoverState.BorderColor = System.Drawing.Color.White;
        this.play.HoverState.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.play).Location = new System.Drawing.Point(295, 12);
        ((System.Windows.Forms.Control)(object)this.play).Name = "play";
        ((System.Windows.Forms.Control)(object)this.play).Size = new System.Drawing.Size(431, 28);
        ((System.Windows.Forms.Control)(object)this.play).TabIndex = 16;
        ((System.Windows.Forms.Control)(object)this.play).Text = "Список локальных миров Minecraft: Bedrock Edition";
        ((System.Windows.Forms.Control)(object)this.play).Click += new System.EventHandler(play_Click);
        ((System.Windows.Forms.Control)(object)this.update_servers).BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.update_servers.BorderColor = System.Drawing.Color.Transparent;
        this.update_servers.BorderRadius = 5;
        this.update_servers.BorderThickness = 1;
        this.update_servers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.update_servers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.update_servers.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.update_servers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.update_servers.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.update_servers).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.update_servers).ForeColor = System.Drawing.Color.White;
        this.update_servers.HoverState.BorderColor = System.Drawing.Color.White;
        this.update_servers.HoverState.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.update_servers).Location = new System.Drawing.Point(1104, 12);
        ((System.Windows.Forms.Control)(object)this.update_servers).Name = "update_servers";
        ((System.Windows.Forms.Control)(object)this.update_servers).Size = new System.Drawing.Size(262, 28);
        ((System.Windows.Forms.Control)(object)this.update_servers).TabIndex = 10;
        ((System.Windows.Forms.Control)(object)this.update_servers).Text = "Обновить список";
        ((System.Windows.Forms.Control)(object)this.update_servers).Click += new System.EventHandler(update_servers_Click);
        ((System.Windows.Forms.Control)(object)this.my_servers).BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.my_servers.BorderColor = System.Drawing.Color.Transparent;
        this.my_servers.BorderRadius = 5;
        this.my_servers.BorderThickness = 1;
        this.my_servers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.my_servers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.my_servers.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.my_servers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.my_servers.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.my_servers).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.my_servers).ForeColor = System.Drawing.Color.White;
        this.my_servers.HoverState.BorderColor = System.Drawing.Color.White;
        this.my_servers.HoverState.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.my_servers).Location = new System.Drawing.Point(732, 12);
        ((System.Windows.Forms.Control)(object)this.my_servers).Name = "my_servers";
        ((System.Windows.Forms.Control)(object)this.my_servers).Size = new System.Drawing.Size(366, 28);
        ((System.Windows.Forms.Control)(object)this.my_servers).TabIndex = 8;
        ((System.Windows.Forms.Control)(object)this.my_servers).Text = "Ваши сервера";
        ((System.Windows.Forms.Control)(object)this.my_servers).Click += new System.EventHandler(my_servers_Click);
        this.Anim.AnimationType = (AnimateWindowType)524288;
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        this.exit.BackColor = System.Drawing.Color.Transparent;
        this.exit.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.exit.ForeColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.exit.Location = new System.Drawing.Point(12, 9);
        this.exit.Name = "exit";
        this.exit.Size = new System.Drawing.Size(21, 18);
        this.exit.TabIndex = 15;
        this.exit.Text = "↩";
        this.exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.exit.Click += new System.EventHandler(exit_Click);
        ((System.Windows.Forms.ScrollableControl)(object)this.Servers).AutoScroll = true;
        ((System.Windows.Forms.Control)(object)this.Servers).BackColor = System.Drawing.Color.Transparent;
        this.Servers.BorderColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.Servers).Controls.Add(this.Nothing);
        this.Servers.CustomBorderColor = System.Drawing.Color.White;
        this.Servers.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.Servers).ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
        ((System.Windows.Forms.Control)(object)this.Servers).Location = new System.Drawing.Point(270, -1);
        ((System.Windows.Forms.Control)(object)this.Servers).Name = "Servers";
        ((System.Windows.Forms.Control)(object)this.Servers).Size = new System.Drawing.Size(1148, 682);
        ((System.Windows.Forms.Control)(object)this.Servers).TabIndex = 1;
        this.Nothing.BackColor = System.Drawing.Color.Transparent;
        this.Nothing.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.Nothing.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
        this.Nothing.Location = new System.Drawing.Point(384, 291);
        this.Nothing.Name = "Nothing";
        this.Nothing.Size = new System.Drawing.Size(405, 96);
        this.Nothing.TabIndex = 12;
        this.Nothing.Text = "В данный момент никто не опубликовал сервер.";
        this.Nothing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.Nothing.Visible = false;
        ((System.Windows.Forms.Control)(object)this.add_server).BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.add_server.BorderColor = System.Drawing.Color.Transparent;
        this.add_server.BorderRadius = 10;
        this.add_server.BorderThickness = 1;
        this.add_server.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.add_server.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.add_server.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.add_server.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.add_server.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.add_server).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.add_server).ForeColor = System.Drawing.Color.White;
        this.add_server.HoverState.BorderColor = System.Drawing.Color.White;
        this.add_server.HoverState.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.add_server.Image = (System.Drawing.Image)resources.GetObject("add_server.Image");
        this.add_server.ImageSize = new System.Drawing.Size(32, 32);
        ((System.Windows.Forms.Control)(object)this.add_server).Location = new System.Drawing.Point(820, 550);
        ((System.Windows.Forms.Control)(object)this.add_server).Name = "add_server";
        ((System.Windows.Forms.Control)(object)this.add_server).Size = new System.Drawing.Size(67, 67);
        ((System.Windows.Forms.Control)(object)this.add_server).TabIndex = 7;
        ((System.Windows.Forms.Control)(object)this.add_server).Click += new System.EventHandler(add_server_Click);
        ((System.Windows.Forms.Control)(object)this.ServersButton).BackColor = System.Drawing.Color.DarkSlateGray;
        this.ServersButton.BorderColor = System.Drawing.Color.Transparent;
        this.ServersButton.BorderRadius = 10;
        this.ServersButton.BorderThickness = 1;
        this.ServersButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.ServersButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.ServersButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.ServersButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        ((System.Windows.Forms.Control)(object)this.ServersButton).Enabled = false;
        this.ServersButton.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.ServersButton).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.ServersButton).ForeColor = System.Drawing.Color.White;
        this.ServersButton.HoverState.BorderColor = System.Drawing.Color.White;
        this.ServersButton.HoverState.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.ServersButton).Location = new System.Drawing.Point(34, 112);
        ((System.Windows.Forms.Control)(object)this.ServersButton).Name = "ServersButton";
        ((System.Windows.Forms.Control)(object)this.ServersButton).Size = new System.Drawing.Size(199, 45);
        ((System.Windows.Forms.Control)(object)this.ServersButton).TabIndex = 4;
        ((System.Windows.Forms.Control)(object)this.ServersButton).Text = "Многопользовательская игра";
        this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(29, 44);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(209, 28);
        this.label1.TabIndex = 2;
        this.label1.Text = "Voxel Multiplayer";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.Account.BorderColor = System.Drawing.Color.White;
        this.Account.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.Account).Controls.Add((System.Windows.Forms.Control)(object)this.Avatar);
        ((System.Windows.Forms.Control)(object)this.Account).Controls.Add(this.name);
        this.Account.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.Account).Location = new System.Drawing.Point(34, 511);
        ((System.Windows.Forms.Control)(object)this.Account).Name = "Account";
        ((System.Windows.Forms.Control)(object)this.Account).Size = new System.Drawing.Size(199, 157);
        ((System.Windows.Forms.Control)(object)this.Account).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.Avatar).BackColor = System.Drawing.Color.Transparent;
        this.Avatar.BorderRadius = 5;
        this.Avatar.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.Avatar).Location = new System.Drawing.Point(57, 21);
        ((System.Windows.Forms.Control)(object)this.Avatar).Name = "Avatar";
        ((System.Windows.Forms.Control)(object)this.Avatar).Size = new System.Drawing.Size(79, 80);
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).TabIndex = 7;
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).TabStop = false;
        this.name.BackColor = System.Drawing.Color.Transparent;
        this.name.Font = new System.Drawing.Font("Microsoft YaHei", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.name.ForeColor = System.Drawing.Color.White;
        this.name.Location = new System.Drawing.Point(26, 113);
        this.name.Name = "name";
        this.name.Size = new System.Drawing.Size(137, 24);
        this.name.TabIndex = 8;
        this.name.Text = "BedrockNet\r\n";
        this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label2.ForeColor = System.Drawing.Color.Silver;
        this.label2.Location = new System.Drawing.Point(40, 72);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(187, 16);
        this.label2.TabIndex = 12;
        this.label2.Text = "Раздел Minecraft: Bedrock Edition";
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add(this.L_text);
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.vpn_l);
        this.loading.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        this.loading.FillColor2 = System.Drawing.Color.FromArgb(37, 61, 61);
        this.loading.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        ((System.Windows.Forms.Control)(object)this.loading).Location = new System.Drawing.Point(0, 0);
        ((System.Windows.Forms.Control)(object)this.loading).Name = "loading";
        ((System.Windows.Forms.Control)(object)this.loading).Size = new System.Drawing.Size(1418, 681);
        ((System.Windows.Forms.Control)(object)this.loading).TabIndex = 14;
        ((System.Windows.Forms.Control)(object)this.loading).Visible = false;
        this.L_text.BackColor = System.Drawing.Color.Transparent;
        this.L_text.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.L_text.ForeColor = System.Drawing.Color.White;
        this.L_text.Location = new System.Drawing.Point(593, 463);
        this.L_text.Name = "L_text";
        this.L_text.Size = new System.Drawing.Size(231, 91);
        this.L_text.TabIndex = 1;
        this.L_text.Text = "Вход в сеть Voxel...";
        this.L_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((Guna2ProgressIndicator)this.vpn_l).AutoStart = true;
        ((System.Windows.Forms.Control)(object)this.vpn_l).BackColor = System.Drawing.Color.Transparent;
        ((Guna2ProgressIndicator)this.vpn_l).CircleSize = 4f;
        ((System.Windows.Forms.Control)(object)this.vpn_l).Location = new System.Drawing.Point(598, 238);
        ((System.Windows.Forms.Control)(object)this.vpn_l).Name = "vpn_l";
        ((Guna2ProgressIndicator)this.vpn_l).NumberOfCircles = 21;
        ((Guna2ProgressIndicator)this.vpn_l).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.vpn_l).Size = new System.Drawing.Size(215, 213);
        ((System.Windows.Forms.Control)(object)this.vpn_l).TabIndex = 0;
        this.bg.BackColor = System.Drawing.Color.DarkSlateGray;
        this.bg.Controls.Add(this.label2);
        this.bg.Controls.Add((System.Windows.Forms.Control)(object)this.Account);
        this.bg.Controls.Add(this.label1);
        this.bg.Controls.Add((System.Windows.Forms.Control)(object)this.ServersButton);
        this.bg.Controls.Add((System.Windows.Forms.Control)(object)this.Servers);
        this.bg.Controls.Add(this.exit);
        this.bg.Dock = System.Windows.Forms.DockStyle.Left;
        this.bg.Location = new System.Drawing.Point(0, 0);
        this.bg.Name = "bg";
        this.bg.Size = new System.Drawing.Size(1418, 680);
        this.bg.TabIndex = 2;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.Black;
        base.ClientSize = new System.Drawing.Size(1416, 680);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.loading);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.add_server);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.play);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.update_servers);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.my_servers);
        base.Controls.Add(this.bg);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "ServersMain";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Раздел Minecraft: Bedrock Edition";
        base.Load += new System.EventHandler(ServersMain_Load);
        ((System.Windows.Forms.Control)(object)this.Servers).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.Account).ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.Avatar).EndInit();
        ((System.Windows.Forms.Control)(object)this.loading).ResumeLayout(false);
        this.bg.ResumeLayout(false);
        this.bg.PerformLayout();
        base.ResumeLayout(false);
    }

    bool method_0()
    {
        return base.InvokeRequired;
    }
}
