using GameLynx;
using GameLynx.AccountSystem;
using GameLynx.MultiplayerAPI.Proxy;
using Guna.UI2.WinForms;
using Monitoring.GameLynxMC.JavaPage;
using Monitoring.MultiplayerAPI;
using Monitoring.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Properties;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.ServersList;

public class javaMultiplayer : Form
{
    public static CancellationToken t;

    private static int Y = 21;

    private string UID = Acc.ID;

    private static string[] args = new string[2];

    private static CancellationTokenSource source = new CancellationTokenSource();

    public static CancellationToken token = source.Token;

    public JavaWorld[] WorldsList;

    private static int countWorlds;

    private IContainer components;

    private Guna2Button add_server;

    private Guna2Panel loading;

    private Guna2WinProgressIndicator prg;

    private Label l_text;

    private Label label4;

    private Guna2AnimateWindow anim;

    private Guna2Panel guna2Panel2;

    private Guna2Panel guna2Panel3;

    private Guna2Button this_;

    private Label label7;

    private Label label8;

    private Guna2Panel Account;

    private Guna2PictureBox Avatar;

    private Label name;

    private Label exit;

    private Guna2Button update_worlds;

    private Panel java;

    private Guna2WinProgressIndicator add_server_runtime;

    private Guna2Button selW;

    private Guna2PictureBox guna2PictureBox1;

    private Guna2PictureBox cloud;

    public javaMultiplayer(JavaWorld[] worlds)
    {
        InitializeComponent();
        WorldsList = worlds;
        if (!rpcAPI.isStarted)
        {
            rpcAPI.RPC_INIT();
        }
        name.Text = Acc.Name;
        ((PictureBox)(object)Avatar).Image = Acc.Avatar;
        Y = 21;
        countWorlds = WorldsList.Length;
    }

    public async Task isVpn(bool init)
    {
        VLAN.loaded_vpn = VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith);
        if (!VLAN.loaded_vpn)
        {
            l_text.Text = "Вход в сеть Voxel...";
            ((Control)(object)loading).Visible = true;
            await VLAN.GameLynxNetInit();
            if (!VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith))
            {
                await isVpn(init: false);
            }
            ((Control)(object)loading).Visible = false;
        }
        if (init)
        {
            await initJavaWorlds();
        }
    }

    public async Task initJavaWorlds()
    {
        Y = 21;
        await Task.Run(delegate
        {
            Parallel.ForEach(WorldsList, async delegate (JavaWorld localWorld)
            {
                string text = VoxelMC.algHashDecode(localWorld.ip, Voxel.Properties.Settings.Default.DfjrKYARpE8kpfNME7uq2gp61y7Oyv);
                string text2 = localWorld.id.ToString();
                string userName = localWorld.UserName;
                bool passIsEnabled = localWorld.PassIsEnabled;
                ushort port = Convert.ToUInt16(VoxelMC.algHashDecode(localWorld.port, Voxel.Properties.Settings.Default.DfjrKYARpE8kpfNME7uq2gp61y7Oyv));
                bool isOld = IntToBool(localWorld.IsOldProtocol);
                LibMcNetInfo libMcNetInfo = new LibMcNetInfo(text, port);
                if (libMcNetInfo.ServerUp)
                {
                    if (localWorld.userID.ToString() == UID)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                ((Control)(object)add_server).Enabled = false;
                            }));
                        }
                    }
                    addWorld(text, port, text2, libMcNetInfo, userName, passIsEnabled, localWorld, isOld);
                }
                else
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>
                    {
                        {
                            "key",
                            Voxel.Properties.Settings.Default.Gtm8XVOhghwEc45gi1Y14HdUYhoSV7
                        },
                        { "value", text2 },
                        { "table", "JavaLocalWorldsList" },
                        { "param", "id" }
                    };
                    countWorlds--;
                    await BackendConnect.getBackendResponse(v.BACKEND + "/del_content.php", parameters);
                }
            });
        });
        if (countWorlds == 0 || countWorlds < 0)
        {
            label4.Visible = true;
            ((Control)(object)add_server).Enabled = true;
        }
        ((Control)(object)loading).Visible = false;
        ((Control)(object)add_server_runtime).Visible = false;
        exit.Enabled = true;
        ((Control)(object)update_worlds).Enabled = true;
    }

    public bool IntToBool(int value)
    {
        return value != 0;
    }

    public static Bitmap pingPictureFinder(int latency)
    {
        if (latency <= 50)
        {
            return Resources.ok_ping;
        }
        if (latency <= 50 || latency > 100)
        {
            if (latency > 100 && latency <= 200)
            {
                return Resources.low_ping;
            }
            if (latency > 200 && latency <= 300)
            {
                return Resources.bad_ping;
            }
            if (latency <= 300)
            {
                return Resources.unknown_ping;
            }
            return Resources.ultra_bad_ping;
        }
        return Resources.norm_ping;
    }

    public void addWorld(string ip, ushort port, string id, LibMcNetInfo stat, string uName, bool isPass, JavaWorld World, bool isOld)
    {
        try
        {
            if (method_0())
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    addWorld(ip, port, id, stat, uName, isPass, World, isOld);
                });
                return;
            }
            try
            {
                Guna2Panel val = new Guna2Panel
                {
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(20, 20, 20)
                };
                ((Control)val).Location = new Point(25, Y);
                ((Control)val).Size = new Size(1095, 75);
                ((Control)val).TabIndex = 1;
                Guna2Panel val2 = val;
                java.Controls.Add((Control)(object)val2);
                PictureBox value = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Image = pingPictureFinder((int)stat.Latency),
                    Location = new Point(825, 12),
                    Size = new Size(49, 45),
                    SizeMode = PictureBoxSizeMode.CenterImage
                };
                Label value2 = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Microsoft YaHei", 18f, FontStyle.Bold, GraphicsUnit.Point, 204),
                    ForeColor = Color.White,
                    Location = new Point(527, 12),
                    Size = new Size(288, 45),
                    Text = stat.CurrentPlayers + " из " + stat.MaximumPlayers,
                    TextAlign = ContentAlignment.MiddleRight
                };
                Label value3 = new Label
                {
                    Text = stat.Stripped_Motd.Replace("\n", ""),
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = new Font("Microsoft YaHei", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 204),
                    ForeColor = Color.White,
                    Location = new Point(69, 12),
                    Size = new Size(176, 25),
                    TabIndex = 2
                };
                Label label = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = new Font("Microsoft YaHei", 9f, FontStyle.Regular, GraphicsUnit.Point, 204),
                    ForeColor = Color.Silver,
                    Location = new Point(71, 46),
                    Size = new Size(171, 19),
                    TabIndex = 4,
                    Text = $"• {stat.Version}  • Пинг {stat.Latency} мс  • Хост {uName}"
                };
                Guna2PictureBox val3 = new Guna2PictureBox();
                ((Control)val3).BackColor = Color.Transparent;
                val3.BorderRadius = 3;
                val3.FillColor = Color.FromArgb(10, 10, 10);
                val3.ImageRotate = 0f;
                ((Control)val3).Location = new Point(12, 12);
                ((Control)val3).Size = new Size(51, 51);
                ((PictureBox)val3).SizeMode = PictureBoxSizeMode.StretchImage;
                ((PictureBox)val3).TabStop = false;
                Guna2PictureBox val4 = val3;
                try
                {
                    ((PictureBox)(object)val4).Image = Image.FromStream(new MemoryStream(stat.FaviconBytes));
                }
                catch
                {
                    ((PictureBox)(object)val4).Image = Resources.gamelynx;
                }
                Guna2Button val5 = new Guna2Button();
                ((Control)val5).BackColor = Color.Transparent;
                val5.BorderRadius = 10;
                val5.FillColor = Color.FromArgb(37, 61, 61);
                val5.BorderColor = Color.Transparent;
                val5.HoverState = new Guna.UI2.WinForms.Suite.ButtonState
                {
                    BorderColor = Color.White,
                    FillColor = Color.FromArgb(41, 68, 68)
                };
                val5.BorderThickness = 1;
                ((Control)val5).Font = new Font("Microsoft YaHei", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
                ((Control)val5).ForeColor = Color.White;
                ((Control)val5).Location = new Point(892, 15);
                ((Control)val5).Size = new Size(180, 45);
                ((Control)val5).TabIndex = 3;
                ((Control)val5).Text = "Играть";
                Guna2Button val6 = val5;
                if (isPass)
                {
                    label.Text += "  • Без пароля";
                    ((Control)(object)val6).Click += delegate (object sender, EventArgs e)
                    {
                        copyWithPass(sender, e, $"{ip}:{port}", World.PassString, stat, uName, isOld);
                    };
                }
                else
                {
                    ((Control)(object)val6).Click += delegate (object sender, EventArgs e)
                    {
                        copyWithoutPass(sender, e, $"{ip}:{port}", stat, uName, isOld);
                    };
                }
                ((Control)(object)val2).Controls.Add(value2);
                ((Control)(object)val2).Controls.Add(value);
                ((Control)(object)val2).Controls.Add((Control)(object)val6);
                ((Control)(object)val2).Controls.Add((Control)(object)val4);
                ((Control)(object)val2).Controls.Add(value3);
                ((Control)(object)val2).Controls.Add(label);
                java.Controls.Add((Control)(object)val2);
                Y += 85;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        catch
        {
        }
    }

    public void copyWithoutPass(object sender, EventArgs e, string adress, LibMcNetInfo f, string n, bool isOld)
    {
        StartProxy(f, n, isOld);
    }

    public void StartProxy(LibMcNetInfo f, string n, bool isOld)
    {
        CancellationTokenSource s = new CancellationTokenSource();
        t = s.Token;
        ProxyServerForJava prx = new ProxyServerForJava("Voxel Host » " + f.Stripped_Motd, f.Address, f.Port, isOld);
        ((Control)(object)cloud).Visible = true;
        ((Control)(object)loading).Visible = true;
        l_text.Text = "Хост " + n + " отображён в локальных мирах Minecraft: Java Edition.";
        ((Control)(object)selW).Visible = true;
        ((Control)(object)selW).Click += delegate
        {
            s.Cancel();
        };
        Task.Run(delegate
        {
            prx.Start(t);
        }, t).ContinueWith(delegate
        {
        }, TaskScheduler.FromCurrentSynchronizationContext());
        ((Control)(object)selW).Click += delegate
        {
            s.Cancel();
            ((Control)(object)loading).Visible = false;
            ((Control)(object)cloud).Visible = false;
            ((Control)(object)selW).Visible = false;
        };
    }

    public void copyWithPass(object sender, EventArgs e, string adress, string pass, LibMcNetInfo f, string n, bool isOld)
    {
        GMessageBoxLeaveTextBox gMessageBoxLeaveTextBox = new GMessageBoxLeaveTextBox(n + " поставил(а) пароль к этому хосту. Введите пароль от хоста.", isPass: true, "Введите пароль от мира...");
        DialogResult dialogResult = gMessageBoxLeaveTextBox.ShowDialog();
        if (dialogResult == DialogResult.Yes && VoxelMC.getHashPass(gMessageBoxLeaveTextBox.text_) == pass)
        {
            StartProxy(f, n, isOld);
        }
        else if (dialogResult != DialogResult.Cancel)
        {
            new GMessageBoxOK("Неверный пароль.").ShowDialog();
        }
    }

    private void add_server_Click(object sender, EventArgs e)
    {
        if (new GMessageBoxYesNo("Брандмауэр будет отключен для предотвращения проблем. Хотите захостить ваш локальный мир?").ShowDialog() == DialogResult.Yes)
        {
            Close();
            Thread thread = new Thread((ThreadStart)delegate
            {
                Application.Run(new AddHost());
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }

    private async void label6_Click(object sender, EventArgs e)
    {
        Close();
        BedrockServer[] serv = await BackendConnect.updateBedrockServersList();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new ServersMain(serv).ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    private async void javaMultiplayer_Load(object sender, EventArgs e)
    {
        exit.Enabled = false;
        ((Control)(object)update_worlds).Enabled = false;
        await VLAN.disFirewall();
        await isVpn(init: true);
    }

    private void exit_Click(object sender, EventArgs e)
    {
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new App().ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    private async void update_worlds_Click(object sender, EventArgs e)
    {
        ((Control)(object)loading).Visible = true;
        l_text.Text = "Загрузка...";
        JavaWorld[] worlds = await BackendConnect.updateJavaWorldsList();
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new javaMultiplayer(worlds).ShowDialog();
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.ServersList.javaMultiplayer));
        this.loading = new Guna2Panel();
        this.cloud = new Guna2PictureBox();
        this.selW = new Guna2Button();
        this.l_text = new System.Windows.Forms.Label();
        this.prg = new Guna2WinProgressIndicator();
        this.label4 = new System.Windows.Forms.Label();
        this.anim = new Guna2AnimateWindow(this.components);
        this.guna2Panel2 = new Guna2Panel();
        this.update_worlds = new Guna2Button();
        this.guna2PictureBox1 = new Guna2PictureBox();
        this.guna2Panel3 = new Guna2Panel();
        this.exit = new System.Windows.Forms.Label();
        this.Account = new Guna2Panel();
        this.Avatar = new Guna2PictureBox();
        this.name = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.this_ = new Guna2Button();
        this.java = new System.Windows.Forms.Panel();
        this.add_server_runtime = new Guna2WinProgressIndicator();
        this.add_server = new Guna2Button();
        ((System.Windows.Forms.Control)(object)this.loading).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.cloud).BeginInit();
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.guna2PictureBox1).BeginInit();
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.Account).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.Avatar).BeginInit();
        this.java.SuspendLayout();
        base.SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.cloud);
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.selW);
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add(this.l_text);
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.prg);
        this.loading.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.loading).Location = new System.Drawing.Point(0, 0);
        ((System.Windows.Forms.Control)(object)this.loading).Name = "loading";
        ((System.Windows.Forms.Control)(object)this.loading).Size = new System.Drawing.Size(1424, 572);
        ((System.Windows.Forms.Control)(object)this.loading).TabIndex = 9;
        ((System.Windows.Forms.Control)(object)this.loading).Visible = false;
        ((System.Windows.Forms.Control)(object)this.cloud).BackColor = System.Drawing.Color.Transparent;
        this.cloud.BorderRadius = 15;
        this.cloud.FillColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.PictureBox)(object)this.cloud).Image = Voxel.Properties.Resources.cloud;
        this.cloud.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.cloud).Location = new System.Drawing.Point(598, 109);
        ((System.Windows.Forms.Control)(object)this.cloud).Name = "cloud";
        this.cloud.ShadowDecoration.BorderRadius = 15;
        this.cloud.ShadowDecoration.Color = System.Drawing.Color.DarkSlateGray;
        this.cloud.ShadowDecoration.Depth = 100;
        this.cloud.ShadowDecoration.Enabled = true;
        ((System.Windows.Forms.Control)(object)this.cloud).Size = new System.Drawing.Size(199, 194);
        ((System.Windows.Forms.PictureBox)(object)this.cloud).SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        ((System.Windows.Forms.PictureBox)(object)this.cloud).TabIndex = 4;
        ((System.Windows.Forms.PictureBox)(object)this.cloud).TabStop = false;
        ((System.Windows.Forms.Control)(object)this.cloud).Visible = false;
        ((System.Windows.Forms.Control)(object)this.selW).BackColor = System.Drawing.Color.Transparent;
        this.selW.BorderColor = System.Drawing.Color.Transparent;
        this.selW.BorderRadius = 10;
        this.selW.BorderThickness = 1;
        this.selW.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.selW.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.selW.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.selW.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.selW.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.selW).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.selW).ForeColor = System.Drawing.Color.White;
        this.selW.HoverState.BorderColor = System.Drawing.Color.White;
        this.selW.HoverState.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.selW).Location = new System.Drawing.Point(543, 466);
        ((System.Windows.Forms.Control)(object)this.selW).Name = "selW";
        ((System.Windows.Forms.Control)(object)this.selW).Size = new System.Drawing.Size(303, 45);
        ((System.Windows.Forms.Control)(object)this.selW).TabIndex = 3;
        ((System.Windows.Forms.Control)(object)this.selW).Text = "←    Перейти в список хостов";
        ((System.Windows.Forms.Control)(object)this.selW).Visible = false;
        this.l_text.BackColor = System.Drawing.Color.Transparent;
        this.l_text.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.l_text.ForeColor = System.Drawing.Color.White;
        this.l_text.Location = new System.Drawing.Point(548, 334);
        this.l_text.Name = "l_text";
        this.l_text.Size = new System.Drawing.Size(298, 107);
        this.l_text.TabIndex = 1;
        this.l_text.Text = "Загрузка...";
        this.l_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((Guna2ProgressIndicator)this.prg).AutoStart = true;
        ((System.Windows.Forms.Control)(object)this.prg).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.prg).Location = new System.Drawing.Point(598, 109);
        ((System.Windows.Forms.Control)(object)this.prg).Name = "prg";
        ((Guna2ProgressIndicator)this.prg).NumberOfCircles = 20;
        ((Guna2ProgressIndicator)this.prg).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.prg).Size = new System.Drawing.Size(199, 194);
        ((System.Windows.Forms.Control)(object)this.prg).TabIndex = 0;
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label4.ForeColor = System.Drawing.Color.Gray;
        this.label4.Location = new System.Drawing.Point(314, 214);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(530, 35);
        this.label4.TabIndex = 10;
        this.label4.Text = "Никто не хостит свой локальный мир";
        this.label4.Visible = false;
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Controls.Add((System.Windows.Forms.Control)(object)this.update_worlds);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Controls.Add((System.Windows.Forms.Control)(object)this.guna2PictureBox1);
        this.guna2Panel2.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Location = new System.Drawing.Point(278, 0);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Name = "guna2Panel2";
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Size = new System.Drawing.Size(1146, 74);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).TabIndex = 13;
        ((System.Windows.Forms.Control)(object)this.update_worlds).BackColor = System.Drawing.Color.Transparent;
        this.update_worlds.BorderColor = System.Drawing.Color.Transparent;
        this.update_worlds.BorderRadius = 5;
        this.update_worlds.BorderThickness = 1;
        this.update_worlds.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.update_worlds.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.update_worlds.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.update_worlds.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.update_worlds.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        ((System.Windows.Forms.Control)(object)this.update_worlds).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.update_worlds).ForeColor = System.Drawing.Color.White;
        this.update_worlds.HoverState.BorderColor = System.Drawing.Color.White;
        this.update_worlds.HoverState.FillColor = System.Drawing.Color.Black;
        ((System.Windows.Forms.Control)(object)this.update_worlds).Location = new System.Drawing.Point(29, 20);
        ((System.Windows.Forms.Control)(object)this.update_worlds).Name = "update_worlds";
        ((System.Windows.Forms.Control)(object)this.update_worlds).Size = new System.Drawing.Size(150, 35);
        ((System.Windows.Forms.Control)(object)this.update_worlds).TabIndex = 15;
        ((System.Windows.Forms.Control)(object)this.update_worlds).Text = "Обновить список";
        ((System.Windows.Forms.Control)(object)this.update_worlds).Click += new System.EventHandler(update_worlds_Click);
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).Image = Voxel.Properties.Resources.onlineGame;
        this.guna2PictureBox1.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).Location = new System.Drawing.Point(23, 20);
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).Name = "guna2PictureBox1";
        ((System.Windows.Forms.Control)(object)this.guna2PictureBox1).Size = new System.Drawing.Size(1111, 35);
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).TabIndex = 11;
        ((System.Windows.Forms.PictureBox)(object)this.guna2PictureBox1).TabStop = false;
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).BackColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add(this.exit);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add((System.Windows.Forms.Control)(object)this.Account);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add(this.label7);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add(this.label8);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add((System.Windows.Forms.Control)(object)this.this_);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Dock = System.Windows.Forms.DockStyle.Left;
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Location = new System.Drawing.Point(0, 0);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Name = "guna2Panel3";
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Size = new System.Drawing.Size(278, 570);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).TabIndex = 14;
        this.exit.BackColor = System.Drawing.Color.Transparent;
        this.exit.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.exit.ForeColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.exit.Location = new System.Drawing.Point(12, 9);
        this.exit.Name = "exit";
        this.exit.Size = new System.Drawing.Size(21, 18);
        this.exit.TabIndex = 16;
        this.exit.Text = "↩";
        this.exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.exit.Click += new System.EventHandler(exit_Click);
        this.Account.BorderColor = System.Drawing.Color.White;
        this.Account.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.Account).Controls.Add((System.Windows.Forms.Control)(object)this.Avatar);
        ((System.Windows.Forms.Control)(object)this.Account).Controls.Add(this.name);
        this.Account.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.Account).Location = new System.Drawing.Point(21, 388);
        ((System.Windows.Forms.Control)(object)this.Account).Name = "Account";
        ((System.Windows.Forms.Control)(object)this.Account).Size = new System.Drawing.Size(235, 157);
        ((System.Windows.Forms.Control)(object)this.Account).TabIndex = 15;
        ((System.Windows.Forms.Control)(object)this.Avatar).BackColor = System.Drawing.Color.Transparent;
        this.Avatar.BorderRadius = 5;
        this.Avatar.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.Avatar).Location = new System.Drawing.Point(76, 25);
        ((System.Windows.Forms.Control)(object)this.Avatar).Name = "Avatar";
        ((System.Windows.Forms.Control)(object)this.Avatar).Size = new System.Drawing.Size(79, 80);
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).TabIndex = 7;
        ((System.Windows.Forms.PictureBox)(object)this.Avatar).TabStop = false;
        this.name.BackColor = System.Drawing.Color.Transparent;
        this.name.Font = new System.Drawing.Font("Microsoft YaHei", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.name.ForeColor = System.Drawing.Color.White;
        this.name.Location = new System.Drawing.Point(37, 118);
        this.name.Name = "name";
        this.name.Size = new System.Drawing.Size(159, 24);
        this.name.TabIndex = 8;
        this.name.Text = "BedrockNet\r\n";
        this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.label7.AutoSize = true;
        this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label7.ForeColor = System.Drawing.Color.Silver;
        this.label7.Location = new System.Drawing.Point(51, 74);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(166, 16);
        this.label7.TabIndex = 14;
        this.label7.Text = "Раздел Minecraft: Java Edition";
        this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label8.ForeColor = System.Drawing.Color.White;
        this.label8.Location = new System.Drawing.Point(21, 46);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(235, 28);
        this.label8.TabIndex = 13;
        this.label8.Text = "Voxel Multiplayer";
        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.this_.BorderRadius = 10;
        this.this_.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.this_.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.this_.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.this_.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        ((System.Windows.Forms.Control)(object)this.this_).Enabled = false;
        this.this_.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.this_).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.this_).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.this_).Location = new System.Drawing.Point(21, 124);
        ((System.Windows.Forms.Control)(object)this.this_).Name = "this_";
        ((System.Windows.Forms.Control)(object)this.this_).Size = new System.Drawing.Size(235, 45);
        ((System.Windows.Forms.Control)(object)this.this_).TabIndex = 1;
        ((System.Windows.Forms.Control)(object)this.this_).Text = "Многопользовательская игра";
        this.java.AutoScroll = true;
        this.java.Controls.Add(this.label4);
        this.java.Location = new System.Drawing.Point(278, 74);
        this.java.Name = "java";
        this.java.Size = new System.Drawing.Size(1146, 496);
        this.java.TabIndex = 15;
        ((Guna2ProgressIndicator)this.add_server_runtime).AutoStart = true;
        ((Guna2ProgressIndicator)this.add_server_runtime).CircleSize = 0.1f;
        ((System.Windows.Forms.Control)(object)this.add_server_runtime).Location = new System.Drawing.Point(820, 463);
        ((System.Windows.Forms.Control)(object)this.add_server_runtime).Name = "add_server_runtime";
        ((Guna2ProgressIndicator)this.add_server_runtime).NumberOfCircles = 12;
        ((Guna2ProgressIndicator)this.add_server_runtime).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.add_server_runtime).Size = new System.Drawing.Size(67, 67);
        ((System.Windows.Forms.Control)(object)this.add_server_runtime).TabIndex = 11;
        ((System.Windows.Forms.Control)(object)this.add_server).BackColor = System.Drawing.Color.Transparent;
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
        ((System.Windows.Forms.Control)(object)this.add_server).Location = new System.Drawing.Point(820, 463);
        ((System.Windows.Forms.Control)(object)this.add_server).Name = "add_server";
        ((System.Windows.Forms.Control)(object)this.add_server).Size = new System.Drawing.Size(67, 67);
        ((System.Windows.Forms.Control)(object)this.add_server).TabIndex = 8;
        ((System.Windows.Forms.Control)(object)this.add_server).Click += new System.EventHandler(add_server_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        base.ClientSize = new System.Drawing.Size(1424, 570);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.loading);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.add_server_runtime);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.add_server);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel3);
        base.Controls.Add(this.java);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel2);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "javaMultiplayer";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Раздел Minecraft: Java Edition";
        base.Load += new System.EventHandler(javaMultiplayer_Load);
        ((System.Windows.Forms.Control)(object)this.loading).ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.cloud).EndInit();
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.guna2PictureBox1).EndInit();
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).PerformLayout();
        ((System.Windows.Forms.Control)(object)this.Account).ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.Avatar).EndInit();
        this.java.ResumeLayout(false);
        this.java.PerformLayout();
        base.ResumeLayout(false);
    }

    bool method_0()
    {
        return base.InvokeRequired;
    }
}
