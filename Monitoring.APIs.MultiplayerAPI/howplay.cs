using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.APIs.MultiplayerAPI;

public class howplay : Form
{
    private IContainer components;

    private Label label1;

    private Label label3;

    private Label label4;

    private Label label6;

    private Label label2;

    private Label label5;

    private Label label7;

    private Label label8;

    private Label label9;

    private Label label10;

    private Guna2AnimateWindow anim;

    private Guna2Panel guna2Panel1;

    private Guna2Panel guna2Panel2;

    private Guna2Panel guna2Panel4;

    private Guna2Panel guna2Panel3;

    public howplay()
    {
        InitializeComponent();
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.APIs.MultiplayerAPI.howplay));
        this.label1 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.anim = new Guna2AnimateWindow(this.components);
        this.guna2Panel1 = new Guna2Panel();
        this.guna2Panel2 = new Guna2Panel();
        this.guna2Panel3 = new Guna2Panel();
        this.guna2Panel4 = new Guna2Panel();
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).SuspendLayout();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.Transparent;
        this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(113, 21);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(373, 36);
        this.label1.TabIndex = 0;
        this.label1.Text = "Как играть по сети Voxel.";
        this.label3.BackColor = System.Drawing.Color.Transparent;
        this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(27, 79);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(576, 125);
        this.label3.TabIndex = 2;
        this.label3.Text = resources.GetString("label3.Text");
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.label4.AutoSize = true;
        this.label4.BackColor = System.Drawing.Color.Transparent;
        this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(56, 23);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(510, 72);
        this.label4.TabIndex = 3;
        this.label4.Text = " Как хостить локальный мир или \r\nопубликовать сервер в сети Voxel.\r\n";
        this.label6.BackColor = System.Drawing.Color.Transparent;
        this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(27, 108);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(573, 106);
        this.label6.TabIndex = 5;
        this.label6.Text = resources.GetString("label6.Text");
        this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 36f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(140, 21);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(410, 64);
        this.label2.TabIndex = 6;
        this.label2.Text = "Bedrock Edition";
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 36f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label5.ForeColor = System.Drawing.Color.White;
        this.label5.Location = new System.Drawing.Point(894, 21);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(318, 64);
        this.label5.TabIndex = 7;
        this.label5.Text = "Java Edition";
        this.label7.BackColor = System.Drawing.Color.Transparent;
        this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label7.ForeColor = System.Drawing.Color.White;
        this.label7.Location = new System.Drawing.Point(33, 88);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(681, 110);
        this.label7.TabIndex = 11;
        this.label7.Text = resources.GetString("label7.Text");
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.label8.AutoSize = true;
        this.label8.BackColor = System.Drawing.Color.Transparent;
        this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label8.ForeColor = System.Drawing.Color.White;
        this.label8.Location = new System.Drawing.Point(59, 34);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(610, 36);
        this.label8.TabIndex = 10;
        this.label8.Text = " Как хостить локальный мир в сети Voxel.\r\n";
        this.label9.BackColor = System.Drawing.Color.Transparent;
        this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.label9.ForeColor = System.Drawing.Color.White;
        this.label9.Location = new System.Drawing.Point(33, 76);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(684, 137);
        this.label9.TabIndex = 9;
        this.label9.Text = resources.GetString("label9.Text");
        this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.label10.AutoSize = true;
        this.label10.BackColor = System.Drawing.Color.Transparent;
        this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.label10.ForeColor = System.Drawing.Color.White;
        this.label10.Location = new System.Drawing.Point(183, 24);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(373, 36);
        this.label10.TabIndex = 8;
        this.label10.Text = "Как играть по сети Voxel.";
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        this.guna2Panel1.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add(this.label9);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Controls.Add(this.label10);
        this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Location = new System.Drawing.Point(697, 97);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Name = "guna2Panel1";
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).Size = new System.Drawing.Size(717, 238);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).TabIndex = 12;
        this.guna2Panel2.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Controls.Add(this.label8);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Controls.Add(this.label7);
        this.guna2Panel2.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Location = new System.Drawing.Point(697, 362);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Name = "guna2Panel2";
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).Size = new System.Drawing.Size(717, 234);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).TabIndex = 13;
        this.guna2Panel3.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add(this.label1);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Controls.Add(this.label3);
        this.guna2Panel3.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Location = new System.Drawing.Point(32, 101);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Name = "guna2Panel3";
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).Size = new System.Drawing.Size(626, 234);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).TabIndex = 14;
        this.guna2Panel4.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).Controls.Add(this.label6);
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).Controls.Add(this.label4);
        this.guna2Panel4.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).Location = new System.Drawing.Point(32, 362);
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).Name = "guna2Panel4";
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).Size = new System.Drawing.Size(626, 234);
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).TabIndex = 15;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DarkSlateGray;
        base.ClientSize = new System.Drawing.Size(1452, 623);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel4);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel3);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel2);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.guna2Panel1);
        base.Controls.Add(this.label5);
        base.Controls.Add(this.label2);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "howplay";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel Multiplayer » Как пользоваться.";
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2Panel1).PerformLayout();
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2Panel2).PerformLayout();
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2Panel3).PerformLayout();
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.guna2Panel4).PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
