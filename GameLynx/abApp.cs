using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace GameLynx;

public class abApp : Form
{
    private IContainer components;

    private Label label1;

    private Guna2PictureBox avatar;

    private Label label2;

    private Guna2Panel guna2Panel1;

    private Guna2Button yt;

    private Label label3;

    private Guna2Button telegram;

    private Guna2Button discord;

    private Label exit;

    private Guna2AnimateWindow Anim;

    private Guna2Button site;

    public abApp()
    {
        InitializeComponent();
    }

    private void yt_Click(object sender, EventArgs e)
    {
        Process.Start("https://www.youtube.com/channel/UCtcPxL_nO4jk4BJMza14-Mw");
    }

    private void discord_Click(object sender, EventArgs e)
    {
        Process.Start("https://discord.gg/NWUqUZ2rRq");
    }

    private void telegram_Click(object sender, EventArgs e)
    {
        Process.Start("https://t.me/gamelynx");
    }

    private void exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void site_Click(object sender, EventArgs e)
    {
        Process.Start(v.API_CENTRAL);
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameLynx.abApp));
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.guna2Panel1 = new Guna2Panel();
        this.label3 = new System.Windows.Forms.Label();
        this.exit = new System.Windows.Forms.Label();
        this.Anim = new Guna2AnimateWindow(this.components);
        this.telegram = new Guna2Button();
        this.discord = new Guna2Button();
        this.yt = new Guna2Button();
        this.avatar = new Guna2PictureBox();
        this.site = new Guna2Button();
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.avatar).BeginInit();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft YaHei Light", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(279, 67);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(238, 92);
        this.label1.TabIndex = 0;
        this.label1.Text = "Основатель,\r\nРазработчик\r\n";
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft YaHei Light", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(546, 197);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(122, 28);
        this.label2.TabIndex = 2;
        this.label2.Text = "ZinderXLive";
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add((System.Windows.Forms.Control)(object)this.site);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add((System.Windows.Forms.Control)(object)this.telegram);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add((System.Windows.Forms.Control)(object)this.discord);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add((System.Windows.Forms.Control)(object)this.yt);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add(this.label3);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Dock = System.Windows.Forms.DockStyle.Bottom;
        this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Location = new System.Drawing.Point(0, 248);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Name = "guna2Panel1";
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Size = new System.Drawing.Size(964, 202);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).TabIndex = 3;
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft YaHei Light", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(388, 32);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(188, 28);
        this.label3.TabIndex = 4;
        this.label3.Text = "Соц. сети проекта";
        this.exit.AutoSize = true;
        this.exit.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.exit.Location = new System.Drawing.Point(13, 13);
        this.exit.Name = "exit";
        this.exit.Size = new System.Drawing.Size(17, 16);
        this.exit.TabIndex = 4;
        this.exit.Text = "↩";
        this.exit.Click += new System.EventHandler(exit_Click);
        this.Anim.AnimationType = (AnimateWindowType)524288;
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        this.telegram.BorderColor = System.Drawing.Color.Transparent;
        this.telegram.BorderRadius = 5;
        this.telegram.BorderThickness = 1;
        this.telegram.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.telegram.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.telegram.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.telegram.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.telegram.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
        ((System.Windows.Forms.Control)(object)this.telegram).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.telegram).ForeColor = System.Drawing.Color.White;
        this.telegram.HoverState.BorderColor = System.Drawing.Color.White;
        this.telegram.HoverState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.telegram.Image = Voxel.Properties.Resources._50px_TG;
        this.telegram.ImageSize = new System.Drawing.Size(50, 50);
        ((System.Windows.Forms.Control)(object)this.telegram).Location = new System.Drawing.Point(492, 75);
        ((System.Windows.Forms.Control)(object)this.telegram).Name = "telegram";
        ((System.Windows.Forms.Control)(object)this.telegram).Size = new System.Drawing.Size(100, 100);
        ((System.Windows.Forms.Control)(object)this.telegram).TabIndex = 7;
        ((System.Windows.Forms.Control)(object)this.telegram).Click += new System.EventHandler(telegram_Click);
        this.discord.BorderColor = System.Drawing.Color.Transparent;
        this.discord.BorderRadius = 5;
        this.discord.BorderThickness = 1;
        this.discord.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.discord.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.discord.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.discord.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.discord.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
        ((System.Windows.Forms.Control)(object)this.discord).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.discord).ForeColor = System.Drawing.Color.White;
        this.discord.HoverState.BorderColor = System.Drawing.Color.White;
        this.discord.HoverState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.discord.Image = Voxel.Properties.Resources._50px_DS;
        this.discord.ImageSize = new System.Drawing.Size(50, 50);
        ((System.Windows.Forms.Control)(object)this.discord).Location = new System.Drawing.Point(370, 75);
        ((System.Windows.Forms.Control)(object)this.discord).Name = "discord";
        ((System.Windows.Forms.Control)(object)this.discord).Size = new System.Drawing.Size(100, 100);
        ((System.Windows.Forms.Control)(object)this.discord).TabIndex = 6;
        ((System.Windows.Forms.Control)(object)this.discord).Click += new System.EventHandler(discord_Click);
        this.yt.BorderColor = System.Drawing.Color.Transparent;
        this.yt.BorderRadius = 5;
        this.yt.BorderThickness = 1;
        this.yt.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.yt.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.yt.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.yt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.yt.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
        ((System.Windows.Forms.Control)(object)this.yt).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.yt).ForeColor = System.Drawing.Color.White;
        this.yt.HoverState.BorderColor = System.Drawing.Color.White;
        this.yt.HoverState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.yt.Image = Voxel.Properties.Resources._50px_YT;
        this.yt.ImageSize = new System.Drawing.Size(90, 70);
        ((System.Windows.Forms.Control)(object)this.yt).Location = new System.Drawing.Point(244, 75);
        ((System.Windows.Forms.Control)(object)this.yt).Name = "yt";
        ((System.Windows.Forms.Control)(object)this.yt).Size = new System.Drawing.Size(100, 100);
        ((System.Windows.Forms.Control)(object)this.yt).TabIndex = 5;
        ((System.Windows.Forms.Control)(object)this.yt).Click += new System.EventHandler(yt_Click);
        this.avatar.BorderRadius = 10;
        this.avatar.FillColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.PictureBox)(object)this.avatar).Image = Voxel.Properties.Resources._150px_AV;
        this.avatar.ImageRotate = 0f;
        ((System.Windows.Forms.Control)(object)this.avatar).Location = new System.Drawing.Point(533, 34);
        ((System.Windows.Forms.Control)(object)this.avatar).Name = "avatar";
        ((System.Windows.Forms.Control)(object)this.avatar).Size = new System.Drawing.Size(150, 150);
        ((System.Windows.Forms.PictureBox)(object)this.avatar).SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        ((System.Windows.Forms.PictureBox)(object)this.avatar).TabIndex = 1;
        ((System.Windows.Forms.PictureBox)(object)this.avatar).TabStop = false;
        this.site.BorderColor = System.Drawing.Color.Transparent;
        this.site.BorderRadius = 5;
        this.site.BorderThickness = 1;
        this.site.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.site.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.site.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.site.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.site.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
        ((System.Windows.Forms.Control)(object)this.site).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.site).ForeColor = System.Drawing.Color.White;
        this.site.HoverState.BorderColor = System.Drawing.Color.White;
        this.site.HoverState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.site.Image = Voxel.Properties.Resources._50px_SITE;
        this.site.ImageSize = new System.Drawing.Size(50, 50);
        ((System.Windows.Forms.Control)(object)this.site).Location = new System.Drawing.Point(610, 75);
        ((System.Windows.Forms.Control)(object)this.site).Name = "site";
        ((System.Windows.Forms.Control)(object)this.site).Size = new System.Drawing.Size(100, 100);
        ((System.Windows.Forms.Control)(object)this.site).TabIndex = 8;
        ((System.Windows.Forms.Control)(object)this.site).Click += new System.EventHandler(site_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DarkSlateGray;
        base.ClientSize = new System.Drawing.Size(964, 450);
        base.Controls.Add(this.exit);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel1);
        base.Controls.Add(this.label2);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.avatar);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "abApp";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » О приложении";
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.avatar).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
