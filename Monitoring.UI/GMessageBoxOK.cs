using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.UI;

public class GMessageBoxOK : Form
{
    private IContainer components;

    private Label txt;

    private Guna2Button ok;

    private Guna2AnimateWindow anim;

    public GMessageBoxOK(string txt)
    {
        InitializeComponent();
        this.txt.Text = txt;
    }

    private void ok_Click(object sender, EventArgs e)
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.UI.GMessageBoxOK));
        this.txt = new System.Windows.Forms.Label();
        this.ok = new Guna2Button();
        this.anim = new Guna2AnimateWindow(this.components);
        base.SuspendLayout();
        this.txt.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.txt.ForeColor = System.Drawing.Color.White;
        this.txt.Location = new System.Drawing.Point(12, 13);
        this.txt.Name = "txt";
        this.txt.Size = new System.Drawing.Size(303, 104);
        this.txt.TabIndex = 0;
        this.txt.Text = "TEXT_MAIN";
        this.txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.ok.BorderColor = System.Drawing.Color.Transparent;
        this.ok.BorderRadius = 5;
        this.ok.BorderThickness = 1;
        this.ok.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.ok.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.ok.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.ok.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.ok.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.ok).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.ok).ForeColor = System.Drawing.Color.White;
        this.ok.HoverState.BorderColor = System.Drawing.Color.White;
        this.ok.HoverState.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.ok).Location = new System.Drawing.Point(12, 130);
        ((System.Windows.Forms.Control)(object)this.ok).Name = "ok";
        ((System.Windows.Forms.Control)(object)this.ok).Size = new System.Drawing.Size(300, 25);
        ((System.Windows.Forms.Control)(object)this.ok).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.ok).Text = "Ок";
        ((System.Windows.Forms.Control)(object)this.ok).Click += new System.EventHandler(ok_Click);
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        base.ClientSize = new System.Drawing.Size(327, 167);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.ok);
        base.Controls.Add(this.txt);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "GMessageBoxOK";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Информация";
        base.TopMost = true;
        base.ResumeLayout(false);
    }
}
