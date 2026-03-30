using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace frmdriverbackup
{
    public partial class Form2 : Form
    {
        private string downloadUrl = "https://github.com/Chuyu-Team/Dism-Multi-language/releases/download/v10.1.1002.2/Dism++10.1.1002.1B.zip";
        private string osInfo = GetOSInfo();
        private string tempDownloadPath = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Assign icon to form
            this.Icon = IconHelper.CreateAppIcon();

            txtDownloadUrl.Text = downloadUrl;
            lblOSInfo.Text = "💻 " + osInfo;
        }

        private static string GetOSInfo()
        {
            try
            {
                // Phương pháp 1: Sử dụng WMI để lấy tên OS chính xác
                string osName = GetOSInfoFromWMI();

                if (!string.IsNullOrEmpty(osName))
                {
                    return osName;
                }

                // Fallback: Sử dụng Registry
                osName = GetOSNameFromRegistry();
                if (!string.IsNullOrEmpty(osName))
                {
                    string bitInfo = Get64BitInfo() ? "64-bit" : "32-bit";
                    return osName + " (" + bitInfo + ")";
                }

                return "Không xác định";
            }
            catch
            {
                return "Không xác định";
            }
        }

        private static string GetOSInfoFromWMI()
        {
            try
            {
                // Use Reflection to load System.Management dynamically
                Type managementSearcherType = Type.GetType("System.Management.ManagementObjectSearcher, System.Management");
                if (managementSearcherType == null)
                {
                    return ""; // System.Management not available
                }

                object searcher = Activator.CreateInstance(managementSearcherType, new object[] { "SELECT * FROM Win32_OperatingSystem" });
                object result = managementSearcherType.GetMethod("Get").Invoke(searcher, null);

                foreach (object item in (System.Collections.IEnumerable)result)
                {
                    object captionObj = item.GetType().GetProperty("Caption").GetValue(item, null);
                    object archObj = item.GetType().GetProperty("OSArchitecture").GetValue(item, null);

                    if (captionObj != null)
                    {
                        string osCaption = captionObj.ToString().Replace("Microsoft ", "");
                        string osArchitecture = archObj != null ? archObj.ToString() : (Get64BitInfo() ? "64-bit" : "32-bit");

                        return osCaption + " (" + osArchitecture + ")";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                // Debug: Ghi log lỗi
                System.Diagnostics.Debug.WriteLine("WMI Error: " + ex.Message);
                return "";
            }
        }

        private static string GetOSNameFromRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key != null)
                    {
                        // Lấy ProductName
                        object productName = key.GetValue("ProductName");
                        if (productName != null)
                        {
                            string name = productName.ToString();
                            if (!string.IsNullOrEmpty(name))
                            {
                                // Debug: Kiểm tra xem ProductName là gì
                                System.Diagnostics.Debug.WriteLine("ProductName: " + name);

                                // Nếu là Windows 10 nhưng thực tế là Windows 11, sửa
                                if (name.Contains("Windows 10") && IsWindows11())
                                {
                                    name = name.Replace("Windows 10", "Windows 11");
                                }

                                return name;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Registry Error: " + ex.Message);
            }

            return null;
        }

        private static bool IsWindows11()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key != null)
                    {
                        // Method 1: Check ReleaseId
                        object releaseId = key.GetValue("ReleaseId");
                        if (releaseId != null && releaseId.ToString().StartsWith("23"))
                        {
                            return true;
                        }

                        // Method 2: Check BuildNumber
                        object buildId = key.GetValue("CurrentBuildNumber");
                        if (buildId != null && int.TryParse(buildId.ToString(), out int build))
                        {
                            if (build >= 22000)
                            {
                                return true;
                            }
                        }

                        // Method 3: Check DisplayVersion
                        object displayVersion = key.GetValue("DisplayVersion");
                        if (displayVersion != null)
                        {
                            string version = displayVersion.ToString();
                            System.Diagnostics.Debug.WriteLine("DisplayVersion: " + version);
                            if (version.StartsWith("23") || version == "11")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Continue
            }

            return false;
        }

        private static bool Get64BitInfo()
        {
            try
            {
                return IntPtr.Size == 8;
            }
            catch
            {
                return false;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = 
                    (System.Net.SecurityProtocolType)3072 | 
                    (System.Net.SecurityProtocolType)768;

                btnDownload.Enabled = false;
                btnDownload.Text = "Đang tải...";

                string windowsDrive = Path.GetPathRoot(System.Environment.SystemDirectory);
                string downloadFolder = Path.Combine(windowsDrive, "Dism-Tools");

                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder);
                }

                tempDownloadPath = Path.Combine(downloadFolder, "Dism++10.1.1002.1B.zip");

                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                    webClient.DownloadFileCompleted += (s, e2) =>
                    {
                        if (e2.Error != null)
                        {
                            MessageBox.Show("Lỗi tải: " + e2.Error.Message + "\n\nVui lòng kiểm tra kết nối mạng và thử lại.", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnDownload.Enabled = true;
                            btnDownload.Text = "⬇ Tải xuống";
                        }
                        else if (!e2.Cancelled)
                        {
                            ExtractAndRunDism();
                        }
                    };

                    webClient.DownloadProgressChanged += (s, e2) =>
                    {
                        btnDownload.Text = "Tải: " + e2.ProgressPercentage + "%";
                    };

                    webClient.DownloadFileAsync(new Uri(txtDownloadUrl.Text), tempDownloadPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n\nVui lòng kiểm tra kết nối mạng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                btnDownload.Text = "⬇ Tải xuống";
            }
        }

        private void ExtractAndRunDism()
        {
            try
            {
                btnDownload.Text = "Extracting...";

                string extractPath = Path.GetDirectoryName(tempDownloadPath);

                ExtractZipWithPowerShell(tempDownloadPath, extractPath);

                btnDownload.Text = "Starting...";

                string exePath = FindAndRunCorrectExe(extractPath);

                if (!string.IsNullOrEmpty(exePath))
                {
                    MessageBox.Show("Downloaded and started successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Could not find exe file for your system architecture.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDownload.Enabled = true;
                    btnDownload.Text = "⬇ Download";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Extraction error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                btnDownload.Text = "⬇ Download";
            }
        }

        private void ExtractZipWithPowerShell(string zipPath, string extractPath)
        {
            try
            {
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
                    bool exited = process.WaitForExit(60000); // 60 second timeout

                    if (!exited)
                    {
                        process.Kill();
                        throw new Exception("Timeout - extraction exceeded time limit");
                    }

                    if (process.ExitCode != 0)
                    {
                        string errorOutput = process.StandardError.ReadToEnd();
                        throw new Exception("PowerShell exit code: " + process.ExitCode + "\n" + errorOutput);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ZIP extraction error: " + ex.Message);
            }
        }

        private string FindAndRunCorrectExe(string folderPath)
        {
            try
            {
                string architecture = GetSystemArchitecture();

                // Tìm file exe phù hợp
                string[] exeFiles = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories);

                // Debug: Hiển thị các file tìm được
                string foundFiles = string.Empty;
                foreach (string f in exeFiles)
                {
                    foundFiles += Path.GetFileName(f) + "\n";
                }

                // Tên file chính xác theo architecture
                string targetExeName = "";
                if (architecture == "x64")
                {
                    targetExeName = "Dism++x64.exe";
                }
                else if (architecture == "x86")
                {
                    targetExeName = "Dism++x86.exe";
                }
                else if (architecture == "arm64")
                {
                    targetExeName = "Dism++ARM64.exe";
                }
                else if (architecture == "arm")
                {
                    targetExeName = "Dism++ARM.exe";
                }

                // Tìm file theo tên chính xác
                foreach (string exePath in exeFiles)
                {
                    string fileName = Path.GetFileName(exePath);

                    if (fileName.Equals(targetExeName, StringComparison.OrdinalIgnoreCase))
                    {
                        RunExe(exePath);
                        return exePath;
                    }
                }

                // Nếu không tìm thấy file chính xác, thử pattern matching
                foreach (string exePath in exeFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(exePath).ToLower();

                    if (architecture == "x64" && (fileName.Contains("x64") || fileName.Contains("amd64")))
                    {
                        RunExe(exePath);
                        return exePath;
                    }
                    else if (architecture == "x86" && (fileName.Contains("x86") || fileName.Contains("i386")))
                    {
                        RunExe(exePath);
                        return exePath;
                    }
                    else if (architecture == "arm64" && (fileName.Contains("arm64") || fileName.Contains("aarch64")))
                    {
                        RunExe(exePath);
                        return exePath;
                    }
                    else if (architecture == "arm" && fileName.Contains("arm") && !fileName.Contains("arm64"))
                    {
                        RunExe(exePath);
                        return exePath;
                    }
                }

                // Nếu không tìm thấy file phù hợp, hiển thị lỗi chi tiết
                MessageBox.Show(
                    "Không tìm thấy file exe phù hợp.\n\n" +
                    "Kiến trúc hệ thống: " + architecture + "\n" +
                    "Tìm kiếm: " + targetExeName + "\n\n" +
                    "Các file tìm được:\n" + (string.IsNullOrEmpty(foundFiles) ? "(Không có file exe)" : foundFiles),
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm file exe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string GetSystemArchitecture()
        {
            try
            {
                // Method 1: Check PROCESSOR_ARCHITECTURE from Environment Variable
                string procArch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (!string.IsNullOrEmpty(procArch))
                {
                    procArch = procArch.ToLower();
                    if (procArch == "amd64")
                        return "x64";
                    else if (procArch == "x86")
                        return "x86";
                    else if (procArch == "arm64")
                        return "arm64";
                    else if (procArch == "arm")
                        return "arm";
                }

                // Method 2: Check from Registry
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment"))
                {
                    if (key != null)
                    {
                        object archValue = key.GetValue("PROCESSOR_ARCHITECTURE");
                        if (archValue != null)
                        {
                            string arch = archValue.ToString().ToLower();
                            if (arch == "amd64")
                                return "x64";
                            else if (arch == "x86")
                                return "x86";
                            else if (arch == "arm64")
                                return "arm64";
                            else if (arch == "arm")
                                return "arm";
                        }
                    }
                }

                // Fallback: Use IntPtr.Size
                return IntPtr.Size == 8 ? "x64" : "x86";
            }
            catch
            {
                return IntPtr.Size == 8 ? "x64" : "x86";
            }
        }

        private void RunExe(string exePath)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = true,
                    Verb = "runas" // Run with admin rights
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting application: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/sponsors");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open browser: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
