using Guna.UI2.WinForms;
using Monitoring;
using Monitoring.MultiplayerAPI;
using Monitoring.UI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GameLynx;

public class Settings : Form
{
    private IContainer components;

    private Guna2HtmlLabel status;

    private Label label4;

    private Guna2GradientButton leave;

    private Panel load;

    private Guna2WinProgressIndicator l;

    private Label exit;

    private Guna2AnimateWindow Anim;

    private Guna2Panel net;

    public Settings()
    {
        InitializeComponent();
        bool enabled;
        if (!(enabled = VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith)))
        {
            ((Control)(object)status).Text = ColorParser.parse("Статус подключения к сети Voxel: §cВы не в сети. §fЧтобы войти в сеть, зайдите в главное меню.");
        }
        else
        {
            ((Control)(object)status).Text = ColorParser.parse("Статус подключения к сети Voxel: §aВы в сети.");
        }
        ((Control)(object)leave).Enabled = enabled;
    }

    public async void leave_Click(object sender, EventArgs e)
    {
        load.Visible = true;
        await VLAN.GameLynxNetLeave();
        new GMessageBoxOK("Вы успешно вышли из сети Voxel Multiplayer.").ShowDialog();
        ((Control)(object)leave).Enabled = false;
        load.Visible = false;
        ((Control)(object)status).Text = ColorParser.parse("Ваш статус в сети Voxel: §cВы не в сети. §fЧтобы войти в сеть, зайдите в любой раздел игры Minecraft.");
    }

    private void exit_Click(object sender, EventArgs e)
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
        this.status = new Guna.UI2.WinForms.Guna2HtmlLabel();
        this.label4 = new System.Windows.Forms.Label();
        this.leave = new Guna.UI2.WinForms.Guna2GradientButton();
        this.load = new System.Windows.Forms.Panel();
        this.l = new Guna.UI2.WinForms.Guna2WinProgressIndicator();
        this.exit = new System.Windows.Forms.Label();
        this.Anim = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
        this.net = new Guna.UI2.WinForms.Guna2Panel();
        this.load.SuspendLayout();
        this.net.SuspendLayout();
        this.SuspendLayout();
        // 
        // status
        // 
        this.status.AutoSize = false;
        this.status.BackColor = System.Drawing.Color.Transparent;
        this.status.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.status.ForeColor = System.Drawing.Color.White;
        this.status.Location = new System.Drawing.Point(34, 73);
        this.status.Name = "status";
        this.status.Size = new System.Drawing.Size(681, 67);
        this.status.TabIndex = 23;
        this.status.Text = null;
        this.status.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label4
        // 
        this.label4.BackColor = System.Drawing.Color.Transparent;
        this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(34, 23);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(681, 38);
        this.label4.TabIndex = 22;
        this.label4.Text = "Сеть Voxel Multiplayer";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // leave
        // 
        this.leave.BorderColor = System.Drawing.Color.Transparent;
        this.leave.BorderRadius = 5;
        this.leave.BorderThickness = 1;
        this.leave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.leave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.leave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
        this.leave.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
        this.leave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
        this.leave.FillColor = System.Drawing.Color.DarkSlateGray;
        this.leave.FillColor2 = System.Drawing.Color.DarkSlateGray;
        this.leave.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.leave.ForeColor = System.Drawing.Color.White;
        this.leave.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        this.leave.HoverState.BorderColor = System.Drawing.Color.White;
        this.leave.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
        this.leave.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
        this.leave.Location = new System.Drawing.Point(34, 146);
        this.leave.Name = "leave";
        this.leave.Size = new System.Drawing.Size(681, 47);
        this.leave.TabIndex = 21;
        this.leave.Text = "Покинуть сеть";
        this.leave.Click += new System.EventHandler(this.leave_Click);
        // 
        // load
        // 
        this.load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
        this.load.Controls.Add(this.l);
        this.load.Location = new System.Drawing.Point(0, 0);
        this.load.Name = "load";
        this.load.Size = new System.Drawing.Size(800, 450);
        this.load.TabIndex = 24;
        this.load.Visible = false;
        // 
        // l
        // 
        this.l.AutoStart = true;
        this.l.BackColor = System.Drawing.Color.Transparent;
        this.l.Location = new System.Drawing.Point(284, 122);
        this.l.Name = "l";
        this.l.NumberOfCircles = 20;
        this.l.ProgressColor = System.Drawing.Color.White;
        this.l.Size = new System.Drawing.Size(202, 198);
        this.l.TabIndex = 0;
        // 
        // exit
        // 
        this.exit.AutoSize = true;
        this.exit.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.exit.ForeColor = System.Drawing.Color.DarkSlateGray;
        this.exit.Location = new System.Drawing.Point(12, 9);
        this.exit.Name = "exit";
        this.exit.Size = new System.Drawing.Size(18, 17);
        this.exit.TabIndex = 25;
        this.exit.Text = "↩";
        this.exit.Click += new System.EventHandler(this.exit_Click);
        // 
        // Anim
        // 
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        // 
        // net
        // 
        this.net.BackColor = System.Drawing.Color.Transparent;
        this.net.BorderRadius = 15;
        this.net.Controls.Add(this.leave);
        this.net.Controls.Add(this.label4);
        this.net.Controls.Add(this.status);
        this.net.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
        this.net.Location = new System.Drawing.Point(25, 29);
        this.net.Name = "net";
        this.net.Size = new System.Drawing.Size(748, 224);
        this.net.TabIndex = 26;
        // 
        // Settings
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.load);
        this.Controls.Add(this.exit);
        this.Controls.Add(this.net);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Name = "Settings";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Настройки.";
        this.load.ResumeLayout(false);
        this.net.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }
}
