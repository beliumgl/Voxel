using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.APIs.MultiplayerAPI;

public class howcreate : Form
{
    private IContainer components;

    private Label label4;

    private Label label6;

    private Guna2Button ok;

    private Guna2AnimateWindow anim;

    public howcreate()
    {
        InitializeComponent();
    }

    private void method_0(object sender, EventArgs e)
    {
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
        this.label4 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.ok = new Guna2Button();
        this.anim = new Guna2AnimateWindow(this.components);
        base.SuspendLayout();
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(222, 42);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(357, 72);
        this.label4.TabIndex = 4;
        this.label4.Text = "Как хостить локальный \r\n     мир в сети Voxel.";
        this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(55, 133);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(684, 122);
        this.label6.TabIndex = 6;
        this.label6.Text = "Чтобы хостить мир в сеть Voxel, необходимо и иметь клиент Voxel и игру. Далее просто заходите в игру и в ваш мир. Теперь в разделе \"Друзья\" все пользователи GameLynx будут видеть ваш мир.";
        this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.ok.BorderColor = System.Drawing.Color.White;
        this.ok.BorderRadius = 10;
        this.ok.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.ok.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.ok.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.ok.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.ok.FillColor = System.Drawing.Color.FromArgb(37, 61, 61);
        ((System.Windows.Forms.Control)(object)this.ok).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.ok).ForeColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.ok).Location = new System.Drawing.Point(59, 258);
        ((System.Windows.Forms.Control)(object)this.ok).Name = "ok";
        ((System.Windows.Forms.Control)(object)this.ok).Size = new System.Drawing.Size(680, 45);
        ((System.Windows.Forms.Control)(object)this.ok).TabIndex = 7;
        ((System.Windows.Forms.Control)(object)this.ok).Text = "Продолжить";
        ((System.Windows.Forms.Control)(object)this.ok).Click += new System.EventHandler(method_0);
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DarkSlateGray;
        base.ClientSize = new System.Drawing.Size(800, 341);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.ok);
        base.Controls.Add(this.label6);
        base.Controls.Add(this.label4);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "howcreate";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel Multiplayer » Хост мира.";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
