using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.UI;

public class GMessageBoxYesNo : Form
{
    private DialogResult dialogResult = DialogResult.No;

    private IContainer components;

    private Label txt;

    private Guna2Button yes;

    private Guna2Button no;

    private Guna2AnimateWindow anim;

    public GMessageBoxYesNo(string txt)
    {
        InitializeComponent();
        base.DialogResult = dialogResult;
        this.txt.Text = txt;
    }

    private void yes_Click(object sender, EventArgs e)
    {
        dialogResult = DialogResult.Yes;
        base.DialogResult = dialogResult;
        Close();
    }

    private void no_Click(object sender, EventArgs e)
    {
        dialogResult = DialogResult.No;
        base.DialogResult = dialogResult;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.UI.GMessageBoxYesNo));
        this.txt = new System.Windows.Forms.Label();
        this.yes = new Guna2Button();
        this.no = new Guna2Button();
        this.anim = new Guna2AnimateWindow(this.components);
        base.SuspendLayout();
        this.txt.Font = new System.Drawing.Font("Microsoft YaHei", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.txt.ForeColor = System.Drawing.Color.White;
        this.txt.Location = new System.Drawing.Point(12, 13);
        this.txt.Name = "txt";
        this.txt.Size = new System.Drawing.Size(303, 76);
        this.txt.TabIndex = 0;
        this.txt.Text = "TEXT_MAIN";
        this.txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.yes.BorderColor = System.Drawing.Color.Transparent;
        this.yes.BorderRadius = 5;
        this.yes.BorderThickness = 1;
        this.yes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.yes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.yes.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.yes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.yes.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.yes).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold);
        ((System.Windows.Forms.Control)(object)this.yes).ForeColor = System.Drawing.Color.White;
        this.yes.HoverState.BorderColor = System.Drawing.Color.White;
        this.yes.HoverState.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.yes).Location = new System.Drawing.Point(12, 104);
        ((System.Windows.Forms.Control)(object)this.yes).Name = "yes";
        ((System.Windows.Forms.Control)(object)this.yes).Size = new System.Drawing.Size(150, 25);
        ((System.Windows.Forms.Control)(object)this.yes).TabIndex = 1;
        ((System.Windows.Forms.Control)(object)this.yes).Text = "Да";
        ((System.Windows.Forms.Control)(object)this.yes).Click += new System.EventHandler(yes_Click);
        this.no.BorderColor = System.Drawing.Color.Transparent;
        this.no.BorderRadius = 5;
        this.no.BorderThickness = 1;
        this.no.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.no.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.no.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.no.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.no.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.no).Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        ((System.Windows.Forms.Control)(object)this.no).ForeColor = System.Drawing.Color.White;
        this.no.HoverState.BorderColor = System.Drawing.Color.White;
        this.no.HoverState.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.no).Location = new System.Drawing.Point(164, 104);
        ((System.Windows.Forms.Control)(object)this.no).Name = "no";
        ((System.Windows.Forms.Control)(object)this.no).Size = new System.Drawing.Size(151, 25);
        ((System.Windows.Forms.Control)(object)this.no).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.no).Text = "Нет";
        ((System.Windows.Forms.Control)(object)this.no).Click += new System.EventHandler(no_Click);
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        base.ClientSize = new System.Drawing.Size(327, 141);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.no);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.yes);
        base.Controls.Add(this.txt);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "GMessageBoxYesNo";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Выбор";
        base.TopMost = true;
        base.ResumeLayout(false);
    }
}
