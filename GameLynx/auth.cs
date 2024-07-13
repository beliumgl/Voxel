using GameLynx.AccountSystem;
using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace GameLynx;

public class auth : Form
{
    private IContainer components;

    private Guna2Button github_auth;

    private Label loading;

    private Guna2Button google_auth;

    private Guna2CheckBox isAutoLog;

    private Guna2AnimateWindow Anim;

    public auth()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        ((Control)(object)github_auth).Enabled = false;
        ((Control)(object)github_auth).Visible = false;
        ((Control)(object)google_auth).Enabled = false;
        ((Control)(object)google_auth).Visible = false;
        ((Control)(object)isAutoLog).Enabled = false;
        loading.Visible = true;
        Acc.Login(0, "51741c92904cfaa9765c75e5ded551d07aab4121", "4d6973c27b52b75d2821", "Voxel Multiplayer");
        if (((CheckBox)(object)isAutoLog).Checked)
        {
            string contents = "{\"AccID\":\"" + Acc.ID + "\",\"AccName\":\"" + Acc.Name + "\",\"AccServ\":\"" + Acc.Service + "\",\"AccAvContent\":\"" + Convert.ToBase64String(Utils.getBytesImage(Acc.Avatar)) + "\"}";
            File.WriteAllText(v.ACC_PATH, contents);
        }
    }

    private void guna2Button1_Click(object sender, EventArgs e)
    {
        ((Control)(object)github_auth).Enabled = false;
        ((Control)(object)github_auth).Visible = false;
        ((Control)(object)google_auth).Enabled = false;
        ((Control)(object)google_auth).Visible = false;
        ((Control)(object)isAutoLog).Visible = false;
        loading.Visible = true;
        Acc.Login(1, "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", "20074425134-0cjrvkknc56puiq4karf2ah13m219r24.apps.googleusercontent.com", "Voxel Multiplayer");
        if (((CheckBox)(object)isAutoLog).Checked)
        {
            string contents = "{\"AccID\":\"" + Acc.ID + "\",\"AccName\":\"" + Acc.Name + "\",\"AccServ\":\"" + Acc.Service + "\",\"AccAvContent\":\"" + Convert.ToBase64String(Utils.getBytesImage(Acc.Avatar)) + "\"}";
            File.WriteAllText(v.ACC_PATH, contents);
        }
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameLynx.auth));
        this.loading = new System.Windows.Forms.Label();
        this.google_auth = new Guna2Button();
        this.github_auth = new Guna2Button();
        this.isAutoLog = new Guna2CheckBox();
        this.Anim = new Guna2AnimateWindow(this.components);
        base.SuspendLayout();
        this.loading.AutoSize = true;
        this.loading.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.loading.ForeColor = System.Drawing.Color.White;
        this.loading.Location = new System.Drawing.Point(137, 31);
        this.loading.Name = "loading";
        this.loading.Size = new System.Drawing.Size(166, 17);
        this.loading.TabIndex = 3;
        this.loading.Text = "Ожидание авторизации...";
        this.loading.Visible = false;
        this.google_auth.BorderColor = System.Drawing.Color.White;
        this.google_auth.BorderRadius = 10;
        this.google_auth.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.google_auth.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.google_auth.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.google_auth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.google_auth.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.google_auth).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.google_auth).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.google_auth).Location = new System.Drawing.Point(219, 12);
        ((System.Windows.Forms.Control)(object)this.google_auth).Name = "google_auth";
        ((System.Windows.Forms.Control)(object)this.google_auth).Size = new System.Drawing.Size(206, 36);
        ((System.Windows.Forms.Control)(object)this.google_auth).TabIndex = 4;
        ((System.Windows.Forms.Control)(object)this.google_auth).Text = "Авторизоваться в Google";
        ((System.Windows.Forms.Control)(object)this.google_auth).Click += new System.EventHandler(guna2Button1_Click);
        this.github_auth.BorderColor = System.Drawing.Color.White;
        this.github_auth.BorderRadius = 10;
        this.github_auth.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.github_auth.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.github_auth.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.github_auth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.github_auth.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.github_auth).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.github_auth).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.github_auth).Location = new System.Drawing.Point(12, 12);
        ((System.Windows.Forms.Control)(object)this.github_auth).Name = "github_auth";
        ((System.Windows.Forms.Control)(object)this.github_auth).Size = new System.Drawing.Size(201, 36);
        ((System.Windows.Forms.Control)(object)this.github_auth).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.github_auth).Text = "Авторизоваться в GitHub";
        ((System.Windows.Forms.Control)(object)this.github_auth).Click += new System.EventHandler(button1_Click);
        this.isAutoLog.Animated = true;
        ((System.Windows.Forms.Control)(object)this.isAutoLog).AutoSize = true;
        this.isAutoLog.CheckedState.BorderColor = System.Drawing.Color.White;
        this.isAutoLog.CheckedState.BorderRadius = 2;
        this.isAutoLog.CheckedState.BorderThickness = 0;
        this.isAutoLog.CheckedState.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.isAutoLog).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.isAutoLog).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.isAutoLog).Location = new System.Drawing.Point(124, 54);
        ((System.Windows.Forms.Control)(object)this.isAutoLog).Name = "isAutoLog";
        ((System.Windows.Forms.Control)(object)this.isAutoLog).Size = new System.Drawing.Size(171, 21);
        ((System.Windows.Forms.Control)(object)this.isAutoLog).TabIndex = 5;
        ((System.Windows.Forms.Control)(object)this.isAutoLog).Text = "Входить автоматически";
        this.isAutoLog.UncheckedState.BorderColor = System.Drawing.Color.White;
        this.isAutoLog.UncheckedState.BorderRadius = 2;
        this.isAutoLog.UncheckedState.BorderThickness = 0;
        this.isAutoLog.UncheckedState.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        this.Anim.AnimationType = (AnimateWindowType)524288;
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DarkSlateGray;
        base.ClientSize = new System.Drawing.Size(437, 83);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.isAutoLog);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.google_auth);
        base.Controls.Add(this.loading);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.github_auth);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "auth";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "GameLynx » Авторизация в...";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
