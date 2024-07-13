using GameLynx;
using GameLynx.AccountSystem;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.MyList;

public class List : Form
{
    private IContainer components;

    private Guna2Panel PanelList;

    private Label myType;

    private Label null_;

    private Label exit;

    private Guna2AnimateWindow Anim;

    private Guna2Panel loading;

    private Guna2WinProgressIndicator ind;

    public static int Y = 18;

    public static BedrockServer[] serv;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.MyList.List));
        this.PanelList = new Guna2Panel();
        this.null_ = new System.Windows.Forms.Label();
        this.myType = new System.Windows.Forms.Label();
        this.exit = new System.Windows.Forms.Label();
        this.Anim = new Guna2AnimateWindow(this.components);
        this.loading = new Guna2Panel();
        this.ind = new Guna2WinProgressIndicator();
        ((System.Windows.Forms.Control)(object)this.PanelList).SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.loading).SuspendLayout();
        base.SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.PanelList).BackColor = System.Drawing.Color.Transparent;
        this.PanelList.BorderRadius = 10;
        ((System.Windows.Forms.Control)(object)this.PanelList).Controls.Add(this.null_);
        this.PanelList.FillColor = System.Drawing.Color.FromArgb(20, 20, 20);
        ((System.Windows.Forms.Control)(object)this.PanelList).Location = new System.Drawing.Point(13, 51);
        ((System.Windows.Forms.Control)(object)this.PanelList).Name = "PanelList";
        ((System.Windows.Forms.Control)(object)this.PanelList).Size = new System.Drawing.Size(965, 554);
        ((System.Windows.Forms.Control)(object)this.PanelList).TabIndex = 0;
        this.null_.BackColor = System.Drawing.Color.Transparent;
        this.null_.Font = new System.Drawing.Font("Microsoft YaHei", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.null_.ForeColor = System.Drawing.Color.DimGray;
        this.null_.Location = new System.Drawing.Point(0, 222);
        this.null_.Name = "null_";
        this.null_.Size = new System.Drawing.Size(965, 73);
        this.null_.TabIndex = 2;
        this.null_.Text = "Ничего не обнаружено";
        this.null_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.null_.Visible = false;
        this.myType.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.myType.ForeColor = System.Drawing.Color.White;
        this.myType.Location = new System.Drawing.Point(13, 9);
        this.myType.Name = "myType";
        this.myType.Size = new System.Drawing.Size(965, 39);
        this.myType.TabIndex = 1;
        this.myType.Text = "Ваши сервера";
        this.myType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.exit.BackColor = System.Drawing.Color.Transparent;
        this.exit.Font = new System.Drawing.Font("Microsoft YaHei", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
        this.exit.ForeColor = System.Drawing.Color.DarkSlateGray;
        this.exit.Location = new System.Drawing.Point(10, 9);
        this.exit.Name = "exit";
        this.exit.Size = new System.Drawing.Size(21, 18);
        this.exit.TabIndex = 24;
        this.exit.Text = "↩";
        this.exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.exit.Click += new System.EventHandler(exit_Click);
        this.Anim.AnimationType = (AnimateWindowType)524288;
        this.Anim.Interval = 200;
        this.Anim.TargetForm = this;
        ((System.Windows.Forms.Control)(object)this.loading).Controls.Add((System.Windows.Forms.Control)(object)this.ind);
        this.loading.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.loading).Location = new System.Drawing.Point(-1, -7);
        ((System.Windows.Forms.Control)(object)this.loading).Name = "loading";
        ((System.Windows.Forms.Control)(object)this.loading).Size = new System.Drawing.Size(993, 629);
        ((System.Windows.Forms.Control)(object)this.loading).TabIndex = 3;
        ((System.Windows.Forms.Control)(object)this.loading).Visible = false;
        ((Guna2ProgressIndicator)this.ind).AutoStart = true;
        ((System.Windows.Forms.Control)(object)this.ind).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.ind).Location = new System.Drawing.Point(397, 240);
        ((System.Windows.Forms.Control)(object)this.ind).Name = "ind";
        ((Guna2ProgressIndicator)this.ind).NumberOfCircles = 24;
        ((Guna2ProgressIndicator)this.ind).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.ind).Size = new System.Drawing.Size(180, 180);
        ((System.Windows.Forms.Control)(object)this.ind).TabIndex = 0;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
        base.ClientSize = new System.Drawing.Size(990, 617);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.loading);
        base.Controls.Add(this.exit);
        base.Controls.Add(this.myType);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.PanelList);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "List";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Ваши сервера.";
        ((System.Windows.Forms.Control)(object)this.PanelList).ResumeLayout(false);
        ((System.Windows.Forms.Control)(object)this.loading).ResumeLayout(false);
        base.ResumeLayout(false);
    }

    public List(BedrockServer[] servers)
    {
        serv = servers;
        InitializeComponent();
        InitializeServers();
    }

    public async void InitializeServers()
    {
        ((Control)(object)loading).Visible = true;
        await Task.Run(delegate
        {
            int[] userWorks = Utils.getUserWorks(serv, Acc.ID);
            if (userWorks.Length != 0)
            {
                for (int i = 0; i <= userWorks.Length - 1; i++)
                {
                    BedrockServer obj = serv[userWorks[i]];
                    int number = i;
                    string iD = obj.ID;
                    byte[] picture = Convert.FromBase64String(obj.Avatar);
                    string name = obj.Name;
                    addItem(iD, number, name, picture);
                }
            }
            else
            {
                null_.Visible = true;
            }
        });
        ((Control)(object)loading).Visible = false;
    }

    public void addItem(string id, int number, string name, byte[] picture)
    {
        if (!method_0())
        {
            Guna2GradientPanel val = new Guna2GradientPanel();
            ((Control)val).BackColor = Color.Transparent;
            val.FillColor = Color.FromArgb(30, 30, 30);
            ((Control)val).Location = new Point(13, Y);
            ((Control)val).Size = new Size(917, 64);
            val.FillColor2 = Color.FromArgb(30, 30, 30);
            val.BorderColor = Color.White;
            val.BorderThickness = 1;
            Guna2GradientPanel val2 = val;
            ((Control)(object)PanelList).Controls.Add((Control)(object)val2);
            Guna2Button val3 = new Guna2Button
            {
                BorderColor = Color.IndianRed,
                BorderThickness = 1,
                CustomBorderColor = Color.DarkGray,
                FillColor = Color.Transparent
            };
            ((Control)val3).Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ((Control)val3).ForeColor = Color.IndianRed;
            ((Control)val3).Location = new Point(724, 10);
            ((Control)val3).Size = new Size(180, 45);
            ((Control)val3).Text = "Удалить";
            Guna2Button val4 = val3;
            ((Control)(object)val4).Click += delegate (object sender, EventArgs e)
            {
                del(sender, e, id);
            };
            Label value = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft YaHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204),
                ForeColor = Color.White,
                Location = new Point(75, 26),
                Size = new Size(48, 16),
                Text = name
            };
            Guna2PictureBox val5 = new Guna2PictureBox
            {
                BorderRadius = 10
            };
            ((Control)val5).Location = new Point(19, 7);
            ((Control)val5).Size = new Size(50, 50);
            ((PictureBox)val5).SizeMode = PictureBoxSizeMode.StretchImage;
            ((PictureBox)val5).Image = Image.FromStream(new MemoryStream(picture));
            Guna2PictureBox value2 = val5;
            ((Control)(object)val2).Controls.Add((Control)(object)value2);
            ((Control)(object)val2).Controls.Add(value);
            ((Control)(object)val2).Controls.Add((Control)(object)val4);
            ((Control)(object)PanelList).Controls.Add((Control)(object)val2);
            Y += 70;
        }
        else
        {
            Invoke((MethodInvoker)delegate
            {
                addItem(id, number, name, picture);
            });
        }
    }

    public async void del(object sender, EventArgs e, string id)
    {
        Guna2Button val = (Guna2Button)sender;
        ((Control)val).Text = "Удален";
        ((Control)val).Enabled = false;
        Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            {
                "key",
                Voxel.Properties.Settings.Default.Gtm8XVOhghwEc45gi1Y14HdUYhoSV7
            },
            { "value", id },
            { "table", "ServersList" },
            { "param", "ID" }
        };
        await BackendConnect.getBackendResponse(v.BACKEND + "/del_content.php", parameters);
    }

    private async void exit_Click(object sender, EventArgs e)
    {
        ((Control)(object)loading).Visible = true;
        BedrockServer[] serv = await BackendConnect.updateBedrockServersList();
        Y = 18;
        Close();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new ServersMain(serv).ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    bool method_0()
    {
        return base.InvokeRequired;
    }
}
