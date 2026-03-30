using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace frmdriverbackup
{
    public partial class Form1 : Form
    {
        private Timer loadingTimer;
        private int rotationAngle = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeLoadingAnimation();
        }

        private void InitializeLoadingAnimation()
        {
            loadingTimer = new Timer();
            loadingTimer.Interval = 50;
            loadingTimer.Tick += (s, e) =>
            {
                rotationAngle += 10;
                if (rotationAngle >= 360)
                    rotationAngle = 0;
                picIcon.Invalidate();
            };
            loadingTimer.Start();

            picIcon.Paint += (s, e) => DrawLoadingIcon(e.Graphics);
        }

        private void DrawLoadingIcon(Graphics g)
        {
            g.Clear(picIcon.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int size = 40;
            int x = (picIcon.Width - size) / 2;
            int y = (picIcon.Height - size) / 2;

            // Draw loading circle
            using (Pen pen = new Pen(Color.FromArgb(0, 120, 215), 3))
            {
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                g.TranslateTransform(picIcon.Width / 2, picIcon.Height / 2);
                g.RotateTransform(rotationAngle);
                g.TranslateTransform(-picIcon.Width / 2, -picIcon.Height / 2);

                g.DrawArc(pen, x, y, size, size, 0, 90);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Gán icon cho form
            this.Icon = IconHelper.CreateAppIcon();

            CheckAndInstallDotNet35();
        }

        private bool IsNetFramework35Installed()
        {
            try
            {
                RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5");
                if (ndpKey != null)
                {
                    object install = ndpKey.GetValue("Install");
                    if (install != null && install.ToString() == "1")
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra .NET Framework 3.5: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void CheckAndInstallDotNet35()
        {
            if (IsNetFramework35Installed())
            {
                CheckAndInstallPowerShell();
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    ".NET Framework 3.5 chưa được cài đặt.\n\nBạn có muốn cài đặt nó ngay bây giờ không?",
                    "Cài đặt .NET Framework 3.5",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    InstallDotNet35();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void InstallDotNet35()
        {
            try
            {
                this.Enabled = false;
                lblStatus.Text = "Installing .NET Framework 3.5...";
                lblStatus.Visible = true;

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c \"dism.exe /online /enable-feature /featurename:NetFx3 /All\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("Cài đặt .NET Framework 3.5 thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CheckAndInstallPowerShell();
                    }
                    else
                    {
                        MessageBox.Show("Cài đặt .NET Framework 3.5 thất bại. Mã lỗi: " + process.ExitCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = true;
                        lblStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Installation error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
                lblStatus.Visible = false;
            }
        }

        private void CheckAndInstallPowerShell()
        {
            string osVersion = System.Environment.OSVersion.VersionString;
            bool isWindows7 = osVersion.Contains("6.1");

            if (!isWindows7)
            {
                OpenForm2();
                return;
            }

            if (IsPowerShellInstalled())
            {
                OpenForm2();
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "Windows 7 requires PowerShell 5.1 for this tool to work.\n\nDo you want to download and install Windows Management Framework 5.1 now?",
                    "Install PowerShell",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    InstallPowerShellForWindows7();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private bool IsPowerShellInstalled()
        {
            try
            {
                // Kiểm tra phiên bản PowerShell
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"$PSVersionTable.PSVersion.Major\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit(5000); // Timeout 5 giây
                    string output = process.StandardOutput.ReadToEnd().Trim();

                    if (int.TryParse(output, out int version))
                    {
                        return version >= 5; // PowerShell 5.0+
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void InstallPowerShellForWindows7()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = 
                    (System.Net.SecurityProtocolType)3072 | 
                    (System.Net.SecurityProtocolType)768;

                this.Enabled = false;
                lblStatus.Text = "Downloading Windows Management Framework 5.1...";
                lblStatus.Visible = true;

                string downloadUrl = "https://download.microsoft.com/download/6/F/5/6F5FF66C-6775-42B0-86C4-47D41F2DA187/W7-KB3191566-x64.ZIP";

                if (IntPtr.Size == 4)
                {
                    downloadUrl = "https://download.microsoft.com/download/6/F/5/6F5FF66C-6775-42B0-86C4-47D41F2DA187/W7-KB3191566-x86.ZIP";
                }

                string tempPath = Path.Combine(Path.GetTempPath(), "wmf51.zip");

                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                    webClient.DownloadFileCompleted += (s, e2) =>
                    {
                        if (e2.Error != null)
                        {
                            MessageBox.Show("Lỗi tải WMF 5.1: " + e2.Error.Message + "\n\nVui lòng kiểm tra kết nối mạng.", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Enabled = true;
                            lblStatus.Visible = false;
                        }
                        else if (!e2.Cancelled)
                        {
                            ExtractAndInstallPowerShell(tempPath);
                        }
                    };

                    webClient.DownloadProgressChanged += (s, e2) =>
                    {
                        lblStatus.Text = "Tải: " + e2.ProgressPercentage + "%";
                    };

                    webClient.DownloadFileAsync(new Uri(downloadUrl), tempPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n\nVui lòng kiểm tra kết nối mạng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
                lblStatus.Visible = false;
            }
        }

        private void ExtractAndInstallPowerShell(string zipPath)
        {
            try
            {
                lblStatus.Text = "Extracting and installing...";

                string extractPath = Path.GetTempPath() + "wmf51\\";
                if (!Directory.Exists(extractPath))
                {
                    Directory.CreateDirectory(extractPath);
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = string.Format(@"-NoProfile -ExecutionPolicy Bypass -Command ""Expand-Archive -LiteralPath '{0}' -DestinationPath '{1}' -Force""", 
                        zipPath, extractPath),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    bool exited = process.WaitForExit(60000); // 60 giây timeout

                    if (!exited)
                    {
                        process.Kill();
                        throw new Exception("Timeout - giải nén vượt quá thời gian cho phép");
                    }

                    if (process.ExitCode != 0)
                    {
                        string errorOutput = process.StandardError.ReadToEnd();
                        throw new Exception("PowerShell exit code: " + process.ExitCode + "\n" + errorOutput);
                    }
                }

                string[] msuFiles = Directory.GetFiles(extractPath, "*.msu", SearchOption.AllDirectories);
                if (msuFiles.Length > 0)
                {
                    lblStatus.Text = "Đang cài đặt Windows Management Framework...";

                    ProcessStartInfo installPsi = new ProcessStartInfo
                    {
                        FileName = "wusa.exe",
                        Arguments = "\"" + msuFiles[0] + "\" /quiet /norestart",
                        UseShellExecute = true,
                        Verb = "runas"
                    };

                    using (Process installProcess = Process.Start(installPsi))
                    {
                        bool exited2 = installProcess.WaitForExit(300000); // 5 phút timeout

                        if (!exited2)
                        {
                            installProcess.Kill();
                            throw new Exception("Timeout - cài đặt vượt quá thời gian cho phép");
                        }

                        if (installProcess.ExitCode == 0 || installProcess.ExitCode == 3010)
                        {
                            MessageBox.Show("Windows Management Framework 5.1 installed successfully!\n\nYou may need to restart your machine to complete installation.", 
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OpenForm2();
                        }
                        else
                        {
                            MessageBox.Show("Installation failed. Error code: " + installProcess.ExitCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Enabled = true;
                            lblStatus.Visible = false;
                        }
                    }
                }
                else
                {
                    throw new Exception("MSU file not found in downloaded package");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Installation error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
                lblStatus.Visible = false;
            }
        }

        private void OpenForm2()
        {
            loadingTimer.Stop();
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }
    }
}
