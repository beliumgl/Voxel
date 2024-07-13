using GameLynx;
using GameLynx.AccountSystem;
using GameLynx.MultiplayerAPI.Java;
using Guna.UI2.WinForms;
using Monitoring.GameLynxMC.JavaPage.javaAPI;
using Monitoring.MultiplayerAPI;
using Monitoring.ServersList;
using Monitoring.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Types;
using static Guna.UI2.WinForms.Guna2AnimateWindow;

namespace Monitoring.GameLynxMC.JavaPage;

public class AddHost : Form
{
    private IContainer components;

    private Guna2Button ot;

    private Label Ltext;

    private Guna2WinProgressIndicator prg;

    private Guna2AnimateWindow anim;

    public AddHost()
    {
        InitializeComponent();
    }

    private async void AddHost_Load(object sender, EventArgs e)
    {
        string[] args = new string[2];
        McJavaLanGameFinder f = new McJavaLanGameFinder();
        CancellationTokenSource src = new CancellationTokenSource();
        CancellationToken tkn = src.Token;
        ((Control)(object)ot).Click += async delegate
        {
            ((Control)(object)ot).Enabled = false;
            src.Cancel();
            try
            {
                f.StopSocket();
            }
            catch
            {
            }
            JavaWorld[] array = ((VoxelMC.worlds_ == null) ? (await BackendConnect.updateJavaWorldsList()) : VoxelMC.worlds_);
            JavaWorld[] worlds = array;
            Thread thread3 = new Thread((ThreadStart)delegate
            {
                Application.Run(new javaMultiplayer(worlds));
            });
            thread3.SetApartmentState(ApartmentState.STA);
            thread3.Start();
            Application.Exit();
        };
        await Task.Run(async delegate
        {
            args = await f.SearchAsync(tkn);
        }, tkn);
        if (!tkn.IsCancellationRequested)
        {
            bool isOld = false;
            if (!VoxelMC.getVoxelNetworkAdress().StartsWith(VoxelMC.networkStartsWith))
            {
                new GMessageBoxOK("Вы не подключены к сети Voxel.").ShowDialog();
                JavaWorld[] worlds = await BackendConnect.updateJavaWorldsList();
                Thread thread = new Thread((ThreadStart)delegate
                {
                    Application.Run(new javaMultiplayer(worlds));
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                Application.Exit();
            }
            else
            {
                Ltext.Text = "Отключение брандмауэра...";
                await VLAN.disFirewall();
                Ltext.Text = "Подключение к серверу...";
                ((Control)(object)ot).Visible = false;
                SettingsAddWorldScreen settingsAddWorldScreen = new SettingsAddWorldScreen();
                settingsAddWorldScreen.ShowDialog();
                if (args[1].StartsWith("0.0.0.0"))
                {
                    isOld = true;
                    args[1] = args[1].Split(':')[1];
                }
                await addWorldToDB(Convert.ToInt32(args[1]), settingsAddWorldScreen, isOld);
                JavaWorld[] worlds = await BackendConnect.updateJavaWorldsList();
                Thread thread2 = new Thread((ThreadStart)delegate
                {
                    Application.Run(new javaMultiplayer(worlds));
                });
                thread2.SetApartmentState(ApartmentState.STA);
                thread2.Start();
                Application.Exit();
            }
        }
        try
        {
            f.StopSocket();
        }
        catch
        {
        }
    }

    public async Task addWorldToDB(int port, SettingsAddWorldScreen inf, bool isOldProtocol = false)
    {
        inf.PasswordValue = (inf.IsPassword ? VoxelMC.getHashPass(inf.PasswordValue) : "");
        Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            {
                "key",
                Voxel.Properties.Settings.Default.Gtm8XVOhghwEc45gi1Y14HdUYhoSV7
            },
            {
                "ip",
                VoxelMC.algHashEncode(VoxelMC.getVoxelNetworkAdress(), Voxel.Properties.Settings.Default.DfjrKYARpE8kpfNME7uq2gp61y7Oyv)
            },
            {
                "port",
                VoxelMC.algHashEncode(port.ToString(), Voxel.Properties.Settings.Default.DfjrKYARpE8kpfNME7uq2gp61y7Oyv)
            },
            {
                "username",
                Acc.Name
            },
            {
                "userid",
                Acc.ID
            },
            {
                "ispass",
                inf.IsPassword.ToString()
            },
            { "passstr", inf.PasswordValue },
            {
                "isold",
                isOldProtocol.ToString()
            }
        };
        await BackendConnect.getBackendResponse(v.BACKEND + "/add_java_host.php", parameters);
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitoring.GameLynxMC.JavaPage.AddHost));
        this.ot = new Guna2Button();
        this.Ltext = new System.Windows.Forms.Label();
        this.prg = new Guna2WinProgressIndicator();
        this.anim = new Guna2AnimateWindow(this.components);
        base.SuspendLayout();
        ((System.Windows.Forms.Control)(object)this.ot).BackColor = System.Drawing.Color.Transparent;
        this.ot.BorderColor = System.Drawing.Color.Transparent;
        this.ot.BorderRadius = 10;
        this.ot.BorderThickness = 1;
        this.ot.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.ot.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.ot.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
        this.ot.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
        this.ot.FillColor = System.Drawing.Color.DarkSlateGray;
        ((System.Windows.Forms.Control)(object)this.ot).Font = new System.Drawing.Font("Segoe UI", 9f);
        ((System.Windows.Forms.Control)(object)this.ot).ForeColor = System.Drawing.Color.White;
        this.ot.HoverState.BorderColor = System.Drawing.Color.White;
        this.ot.HoverState.FillColor = System.Drawing.Color.FromArgb(41, 68, 68);
        ((System.Windows.Forms.Control)(object)this.ot).Location = new System.Drawing.Point(22, 24);
        ((System.Windows.Forms.Control)(object)this.ot).Name = "ot";
        ((System.Windows.Forms.Control)(object)this.ot).Size = new System.Drawing.Size(180, 45);
        ((System.Windows.Forms.Control)(object)this.ot).TabIndex = 3;
        ((System.Windows.Forms.Control)(object)this.ot).Text = "←   Отменить поиск мира";
        this.Ltext.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        this.Ltext.ForeColor = System.Drawing.Color.White;
        this.Ltext.Location = new System.Drawing.Point(291, 318);
        this.Ltext.Name = "Ltext";
        this.Ltext.Size = new System.Drawing.Size(199, 73);
        this.Ltext.TabIndex = 4;
        this.Ltext.Text = "Детект вашего мира...";
        this.Ltext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        ((Guna2ProgressIndicator)this.prg).AutoStart = true;
        ((System.Windows.Forms.Control)(object)this.prg).BackColor = System.Drawing.Color.Transparent;
        ((System.Windows.Forms.Control)(object)this.prg).Location = new System.Drawing.Point(291, 109);
        ((System.Windows.Forms.Control)(object)this.prg).Name = "prg";
        ((Guna2ProgressIndicator)this.prg).NumberOfCircles = 20;
        ((Guna2ProgressIndicator)this.prg).ProgressColor = System.Drawing.Color.White;
        ((System.Windows.Forms.Control)(object)this.prg).Size = new System.Drawing.Size(199, 194);
        ((System.Windows.Forms.Control)(object)this.prg).TabIndex = 5;
        this.anim.AnimationType = (AnimateWindowType)524288;
        this.anim.Interval = 200;
        this.anim.TargetForm = this;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(37, 61, 61);
        base.ClientSize = new System.Drawing.Size(800, 491);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.prg);
        base.Controls.Add(this.Ltext);
        base.Controls.Add((System.Windows.Forms.Control)(object)this.ot);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "AddHost";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Voxel » Добавление вашего мира.";
        base.Load += new System.EventHandler(AddHost_Load);
        base.ResumeLayout(false);
    }
}
