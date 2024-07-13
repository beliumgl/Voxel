using GameLynx.AccountSystem;
using Guna.UI2.WinForms;
using Monitoring.MultiplayerAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Voxel;
using Voxel.Properties;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring;

public class ServerInfoPlay : Form
{
    private Thread MAIN;

    private string ip;

    private int port;

    private string name;

    private IContainer components;

    private Guna2Button Play;

    private Guna2HtmlLabel NAME;

    private Label ping;

    private Label label1;

    private Guna2PictureBox AVATAR;

    private Label label2;

    private Guna2TextBox INFO_ABOUT_SERVER;

    private Guna2AnimateWindow Anim;

    private Guna2Panel AccInfo;

    private Label NameAcc;

    private Guna2PictureBox AvatarAcc;

    private Guna2Panel loading;

    private Guna2WinProgressIndicator lInd;

    public ServerInfoPlay(string avatar, string name, string description, BedrockServer server, string ip, int port, LibMcNetInfo stat)
    {
        InitializeComponent();
        this.name = name;
        this.port = port;
        this.ip = ip;
        try
        {
            ((Control)(object)NAME).Text = ColorParser.parse(name);
            Text = "Voxel » Сервер " + ColorParser.removeColors(name);
            ((Control)(object)INFO_ABOUT_SERVER).Text = description;
            if (server.AccountServiceID == Acc.ID)
            {
                ((Control)(object)Play).Enabled = false;
                ((Control)(object)Play).Text = "Это ваш сервер.";
            }
            label2.Text = "Сервер появится во вкладке \"Сервера\", если он еще туда не добавлен.";
            NameAcc.Text = server.AccountName;
            ((PictureBox)(object)AvatarAcc).Image = Resources.gamelynx;
            if (avatar != "NULL")
            {
                ((PictureBox)(object)AVATAR).Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(avatar)));
            }
            else
            {
                ((PictureBox)(object)AVATAR).Image = Resources.gamelynx;
            }
            if (stat.ServerUp)
            {
                ping.Text = $"• Пинг {stat.Latency} мс  • {stat.ver_bedrock}  • {stat.Gamemode}  • {stat.CurrentPlayers} из {stat.MaximumPlayers} ";
            }
            else
            {
                ping.Text = "Сервер не в сети";
                ((Control)(object)Play).Enabled = false;
                ((Control)(object)Play).Text = "Сервер не в сети";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public async void Play_Click(object sender, EventArgs e)
    {
        ((Control)(object)loading).Visible = true;
        BedrockServer[] serv = await BackendConnect.updateBedrockServersList();
        VoxelMC.Play(ip, port, name);
        MAIN = new Thread((ThreadStart)delegate
        {
            new ServersMain(serv).ShowDialog();
        });
        MAIN.SetApartmentState(ApartmentState.STA);
        MAIN.Start();
        Close();
    }

    private async void label1_Click(object sender, EventArgs e)
    {
        ((Control)(object)loading).Visible = true;
        BedrockServer[] array = ((VoxelMC.servers_ == null) ? (await BackendConnect.updateBedrockServersList()) : VoxelMC.servers_);
        BedrockServer[] serv = array;
        MAIN = new Thread((ThreadStart)delegate
        {
            new ServersMain(serv).ShowDialog();
        });
        MAIN.SetApartmentState(ApartmentState.STA);
        MAIN.Start();
        Close();
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.ServerInfoPlay));
        this.NAME = new Guna2HtmlLabel();
        this.ping = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.INFO_ABOUT_SERVER = new Guna2TextBox();
        this.Play = new Guna2Button();
        this.Anim = new Guna2AnimateWindow(this.components);
        this.AccInfo = new Guna2Panel();
        this.NameAcc = new System.Windows.Forms.Label();
        this.AvatarAcc = new Guna2PictureBox();
        this.AVATAR = new Guna2PictureBox();
        this.loading = new Guna2Panel();
        this.lInd = new Guna2WinProgressIndicator();
        ((System.Windows.Forms.Control)(object)this.AccInfo).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.AvatarAcc).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.AVATAR).BeginInit();
        ((System.Windows.Forms.Control)(object)this.loading).SuspendLayout();
        base.SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.NAME).AutoSize = false;
        ((System.Windows.Forms.Control)(object)this.NAME).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.NAME).Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.NAME).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.NAME).Location = new System.Drawing.Point(224, 71);
        ((System.Windows.Forms.Control)(object)this.NAME).Name = "NAME";
        ((System.Windows.Forms.Control)(object)this.NAME).Size = new System.Drawing.Size(295, 28);
        ((System.Windows.Forms.Control)(object)this.NAME).TabIndex = 5;
        ((System.Windows.Forms.Control)(object)this.NAME).Text = null;
        this.NAME.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
        this.ping.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.ping.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.ping.ForeColor = System.Drawing.Color.White;
        this.ping.Location = new System.Drawing.Point(25, 317);
        this.ping.Name = "ping";
        this.ping.Size = new System.Drawing.Size(542, 39);
        this.ping.TabIndex = 6;
        this.ping.Text = "INFO";
        this.ping.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.label1.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(21, 18);
        this.label1.TabIndex = 7;
        this.label1.Text = "↩";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.label1.Click += new System.EventHandler(label1_Click);
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label2.ForeColor = System.Drawing.Color.Silver;
        this.label2.Location = new System.Drawing.Point(39, 9);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(393, 16);
        this.label2.TabIndex = 10;
        this.label2.Text = "Сервер появится во вкладке \"Сервера\", если он еще туда не добавлен.";
        ((System.Windows.Forms.ScrollableControl)(object)this.INFO_ABOUT_SERVER).AutoScroll = true;
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.INFO_ABOUT_SERVER.BorderColor = System.Drawing.Color.Black;
        this.INFO_ABOUT_SERVER.BorderThickness = 0;
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.INFO_ABOUT_SERVER.DefaultText = "asd";
        this.INFO_ABOUT_SERVER.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.INFO_ABOUT_SERVER.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.INFO_ABOUT_SERVER.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.INFO_ABOUT_SERVER.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.INFO_ABOUT_SERVER.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.INFO_ABOUT_SERVER.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).ForeColor = System.Drawing.Color.Silver;
        this.INFO_ABOUT_SERVER.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).Location = new System.Drawing.Point(229, 104);
        this.INFO_ABOUT_SERVER.MaxLength = 12312321;
        this.INFO_ABOUT_SERVER.Multiline = true;
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).Name = "INFO_ABOUT_SERVER";
        this.INFO_ABOUT_SERVER.PasswordChar = '\0';
        this.INFO_ABOUT_SERVER.PlaceholderText = "";
        this.INFO_ABOUT_SERVER.ReadOnly = true;
        this.INFO_ABOUT_SERVER.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).Size = new System.Drawing.Size(290, 87);
        ((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER).TabIndex = 26;
        this.Play.BorderColor = System.Drawing.Color.White;
        this.Play.BorderRadius = 10;
        this.Play.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.Play.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.Play.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.Play.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.Play.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.Play).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.Play).ForeColor = System.Drawing.Color.White;
        this.Play.ImageSize = new System.Drawing.Size(25, 25);
        ((System.Windows.Forms.Control)(object)this.Play).Location = new System.Drawing.Point(28, 269);
        ((System.Windows.Forms.Control)(object)this.Play).Name = "Play";
        ((System.Windows.Forms.Control)(object)this.Play).Size = new System.Drawing.Size(539, 45);
        ((System.Windows.Forms.Control)(object)this.Play).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.Play).Text = "Играть";
        ((System.Windows.Forms.Control)(object)this.Play).Click += new System.EventHandler(Play_Click);
        this.Anim.AnimationType = (AnimateWindowType)524288;
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        ((System.Windows.Forms.Control)(object)this.AccInfo).BackColor = System.Drawing.Color.Transparent;
        this.AccInfo.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.AccInfo).Controls.Add(this.NameAcc);
        ((System.Windows.Forms.Control)(object)this.AccInfo).Controls.Add((System.Windows.Forms.Control)(object)this.AvatarAcc);
        this.AccInfo.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.AccInfo).Location = new System.Drawing.Point(28, 214);
        ((System.Windows.Forms.Control)(object)this.AccInfo).Name = "AccInfo";
        ((System.Windows.Forms.Control)(object)this.AccInfo).Size = new System.Drawing.Size(539, 49);
        ((System.Windows.Forms.Control)(object)this.AccInfo).TabIndex = 30;
        this.NameAcc.AutoSize = true;
        this.NameAcc.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.NameAcc.ForeColor = System.Drawing.Color.White;
        this.NameAcc.Location = new System.Drawing.Point(48, 16);
        this.NameAcc.Name = "NameAcc";
        this.NameAcc.Size = new System.Drawing.Size(31, 17);
        this.NameAcc.TabIndex = 1;
        this.NameAcc.Text = "Null";
        this.AvatarAcc.BorderRadius = 3;
        this.AvatarAcc.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
        this.AvatarAcc.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.AvatarAcc).Location = new System.Drawing.Point(7, 7);
        ((System.Windows.Forms.Control)(object)this.AvatarAcc).Name = "AvatarAcc";
        ((System.Windows.Forms.Control)(object)this.AvatarAcc).Size = new System.Drawing.Size(35, 35);
        ((System.Windows.Forms.PictureBox)(object)this.AvatarAcc).SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        ((System.Windows.Forms.PictureBox)(object)this.AvatarAcc).TabIndex = 0;
        ((System.Windows.Forms.PictureBox)(object)this.AvatarAcc).TabStop = false;
        ((System.Windows.Forms.Control)(object)this.AVATAR).BackColor = System.Drawing.Color.Transparent;
        this.AVATAR.BorderRadius = 10;
        this.AVATAR.FillColor = System.Drawing.Color.Transparent;
        this.AVATAR.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.AVATAR).Location = new System.Drawing.Point(65, 71);
        ((System.Windows.Forms.Control)(object)this.AVATAR).Name = "AVATAR";
        ((System.Windows.Forms.Control)(object)this.AVATAR).Size = new System.Drawing.Size(127, 127);
        ((System.Windows.Forms.PictureBox)(object)this.AVATAR).SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        ((System.Windows.Forms.PictureBox)(object)this.AVATAR).TabIndex = 9;
        ((System.Windows.Forms.PictureBox)(object)this.AVATAR).TabStop = false;
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.lInd);
        this.loading.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.loading).Location = new System.Drawing.Point(-2, -1);
        ((System.Windows.Forms.Control)(object)this.loading).Name = "loading";
        ((System.Windows.Forms.Control)(object)this.loading).Size = new System.Drawing.Size(592, 375);
        ((System.Windows.Forms.Control)(object)this.loading).TabIndex = 31;
        ((System.Windows.Forms.Control)(object)this.loading).Visible = false;
        ((Guna2ProgressIndicator)this.lInd).AutoStart = true;
        ((System.Windows.Forms.Control)(object)this.lInd).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.lInd).Location = new System.Drawing.Point(226, 117);
        ((System.Windows.Forms.Control)(object)this.lInd).Name = "lInd";
        ((Guna2ProgressIndicator)this.lInd).NumberOfCircles = 20;
        ((Guna2ProgressIndicator)this.lInd).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.lInd).Size = new System.Drawing.Size(131, 131);
        ((System.Windows.Forms.Control)(object)this.lInd).TabIndex = 0;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        base.ClientSize = new System.Drawing.Size(589, 371);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.loading);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.AccInfo);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.INFO_ABOUT_SERVER);
        base.Controls.Add(this.label2);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.AVATAR);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.ping);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.NAME);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.Play);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "ServerInfoPlay";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "BedrockNet - Сервер.";
        ((System.Windows.Forms.Control)(object)this.AccInfo).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.AccInfo).PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.AvatarAcc).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.AVATAR).EndInit();
        ((System.Windows.Forms.Control)(object)this.loading).ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
