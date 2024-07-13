using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.UI;

public class GMessageBoxLeaveTextBox : Form
{
    private DialogResult dialogResult = DialogResult.Cancel;

    private IContainer components;

    private Label txt;

    private Guna2Button yes;

    private Guna2Button no;

    private Guna2AnimateWindow anim;

    private Guna2TextBox txtbox;

    public string text_ { get; set; }

    public GMessageBoxLeaveTextBox(string txt, bool isPass, string plcTetx)
    {
        InitializeComponent();
        if (isPass)
        {
            txtbox.PasswordChar = '•';
        }
        txtbox.PlaceholderText = plcTetx;
        base.DialogResult = dialogResult;
        this.txt.Text = txt;
    }

    private void yes_Click(object sender, EventArgs e)
    {
        dialogResult = DialogResult.Yes;
        base.DialogResult = dialogResult;
        try
        {
            if (((Control)(object)txtbox).Text != "")
            {
                text_ = ((Control)(object)txtbox).Text;
            }
            Close();
        }
        catch
        {
        }
    }

    private void no_Click(object sender, EventArgs e)
    {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.UI.GMessageBoxLeaveTextBox));
        this.txt = new System.Windows.Forms.Label();
        this.yes = new Guna2Button();
        this.no = new Guna2Button();
        this.anim = new Guna2AnimateWindow(this.components);
        this.txtbox = new Guna2TextBox();
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
        ((System.Windows.Forms.Control)(object)this.yes).Location = new System.Drawing.Point(12, 142);
        ((System.Windows.Forms.Control)(object)this.yes).Name = "yes";
        ((System.Windows.Forms.Control)(object)this.yes).Size = new System.Drawing.Size(150, 25);
        ((System.Windows.Forms.Control)(object)this.yes).TabIndex = 1;
        ((System.Windows.Forms.Control)(object)this.yes).Text = "Продолжить";
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
        ((System.Windows.Forms.Control)(object)this.no).Location = new System.Drawing.Point(164, 142);
        ((System.Windows.Forms.Control)(object)this.no).Name = "no";
        ((System.Windows.Forms.Control)(object)this.no).Size = new System.Drawing.Size(151, 25);
        ((System.Windows.Forms.Control)(object)this.no).TabIndex = 2;
        ((System.Windows.Forms.Control)(object)this.no).Text = "Закрыть";
        ((System.Windows.Forms.Control)(object)this.no).Click += new System.EventHandler(no_Click);
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        this.txtbox.BorderColor = System.Drawing.Color.Transparent;
        this.txtbox.BorderRadius = 10;
        this.txtbox.BorderThickness = 0;
        ((System.Windows.Forms.Control)(object)this.txtbox).Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txtbox.DefaultText = "";
        this.txtbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
        this.txtbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
        this.txtbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.txtbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
        this.txtbox.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.txtbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.txtbox).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.txtbox).ForeColor = System.Drawing.Color.White;
        this.txtbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
        ((System.Windows.Forms.Control)(object)this.txtbox).Location = new System.Drawing.Point(12, 104);
        ((System.Windows.Forms.Control)(object)this.txtbox).Name = "txtbox";
        this.txtbox.PasswordChar = '\0';
        this.txtbox.PlaceholderForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
        this.txtbox.PlaceholderText = "Введите текст...";
        this.txtbox.SelectedText = "";
        ((System.Windows.Forms.Control)(object)this.txtbox).Size = new System.Drawing.Size(303, 32);
        ((System.Windows.Forms.Control)(object)this.txtbox).TabIndex = 3;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        base.ClientSize = new System.Drawing.Size(327, 179);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.txtbox);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.no);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.yes);
        base.Controls.Add(this.txt);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "GMessageBoxLeaveTextBox";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Выбор";
        base.TopMost = true;
        base.ResumeLayout(false);
    }
}
