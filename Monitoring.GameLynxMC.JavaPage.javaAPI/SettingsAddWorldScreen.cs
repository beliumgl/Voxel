using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.GameLynxMC.JavaPage.javaAPI;

public class SettingsAddWorldScreen : Form
{
    private IContainer components;

    private Guna2Button dalee;

    private Guna2CheckBox isPass;

    private Guna2TextBox pass;

    private Guna2AnimateWindow anim;

    private Label motd;

    public string PasswordValue { get; set; }

    public bool IsPassword { get; set; }

    public SettingsAddWorldScreen()
    {
        InitializeComponent();
    }

    private void isPass_CheckedChanged(object sender, EventArgs e)
    {
        ((Control)(object)pass).Visible = ((CheckBox)(object)isPass).Checked;
        IsPassword = ((CheckBox)(object)isPass).Checked;
    }

    private void dalee_Click(object sender, EventArgs e)
    {
        if (((CheckBox)(object)isPass).Checked)
        {
            if (PasswordValue != "" && !PasswordValue.Contains(" "))
            {
                Close();
            }
        }
        else
        {
            Close();
        }
    }

    private void pass_TextChanged(object sender, EventArgs e)
    {
        PasswordValue = ((Control)(object)pass).Text;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.GameLynxMC.JavaPage.javaAPI.SettingsAddWorldScreen));
        this.dalee = new Guna2Button();
        this.isPass = new Guna2CheckBox();
        this.pass = new Guna2TextBox();
        this.anim = new Guna2AnimateWindow(this.components);
        this.motd = new System.Windows.Forms.Label();
        base.SuspendLayout();
        this.dalee.BorderColor = System.Drawing.Color.Transparent;
        this.dalee.BorderRadius = 10;
        this.dalee.BorderThickness = 1;
        this.dalee.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.dalee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.dalee.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.dalee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.dalee.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.dalee).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.dalee).ForeColor = System.Drawing.Color.White;
        this.dalee.HoverState.BorderColor = System.Drawing.Color.White;
        this.dalee.HoverState.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.dalee).Location = new System.Drawing.Point(24, 377);
        ((System.Windows.Forms.Control)(object)this.dalee).Name = "dalee";
        ((System.Windows.Forms.Control)(object)this.dalee).Size = new System.Drawing.Size(288, 45);
        ((System.Windows.Forms.Control)(object)this.dalee).TabIndex = 0;
        ((System.Windows.Forms.Control)(object)this.dalee).Text = "Продолжить";
        ((System.Windows.Forms.Control)(object)this.dalee).Click += new System.EventHandler(dalee_Click);
        this.isPass.Animated = true;
        ((System.Windows.Forms.Control)(object)this.isPass).AutoSize = true;
        this.isPass.CheckedState.BorderColor = System.Drawing.Color.Transparent;
        this.isPass.CheckedState.BorderRadius = 3;
        this.isPass.CheckedState.BorderThickness = 0;
        this.isPass.CheckedState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.isPass.CheckMarkColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.isPass).Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.isPass).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.isPass).Location = new System.Drawing.Point(24, 77);
        ((System.Windows.Forms.Control)(object)this.isPass).Name = "isPass";
        ((System.Windows.Forms.Control)(object)this.isPass).Size = new System.Drawing.Size(125, 20);
        ((System.Windows.Forms.Control)(object)this.isPass).TabIndex = 1;
        ((System.Windows.Forms.Control)(object)this.isPass).Text = "Поставить пароль";
        this.isPass.UncheckedState.BorderColor = System.Drawing.Color.Transparent;
        this.isPass.UncheckedState.BorderRadius = 3;
        this.isPass.UncheckedState.BorderThickness = 0;
        this.isPass.UncheckedState.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        ((System.Windows.Forms.CheckBox)(object)this.isPass).CheckedChanged += new System.EventHandler(isPass_CheckedChanged);
        ((System.Windows.Forms.Control)(object)this.pass).BackColor = System.Drawing.Color.Transparent;
        this.pass.BorderColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.pass.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.pass).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.pass.DefaultText = "";
        this.pass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.pass.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.pass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.pass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.pass.FillColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.pass.FocusedState.BorderColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.pass).Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.pass).ForeColor = System.Drawing.Color.White;
        this.pass.HoverState.BorderColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.pass).Location = new System.Drawing.Point(24, 103);
        ((System.Windows.Forms.Control)(object)this.pass).Name = "pass";
        this.pass.PasswordChar = '•';
        this.pass.PlaceholderForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
        this.pass.PlaceholderText = "Придумайте пароль...";
        this.pass.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.pass).Size = new System.Drawing.Size(288, 43);
        ((System.Windows.Forms.Control)(object)this.pass).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.pass).Visible = false;
        this.pass.TextChanged += new System.EventHandler(pass_TextChanged);
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        this.motd.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.motd.ForeColor = System.Drawing.Color.White;
        this.motd.Location = new System.Drawing.Point(24, 18);
        this.motd.Name = "motd";
        this.motd.Size = new System.Drawing.Size(288, 47);
        this.motd.TabIndex = 3;
        this.motd.Text = "Настройки хоста";
        this.motd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(20, 20, 20);
        base.ClientSize = new System.Drawing.Size(335, 438);
        base.Controls.Add(this.motd);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.pass);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.isPass);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.dalee);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "SettingsAddWorldScreen";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Настройки добавления мира Minecraft: Java Edition";
        base.TopMost = true;
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
