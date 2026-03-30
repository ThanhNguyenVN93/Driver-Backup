using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;

namespace frmdriverbackup
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Auto-export icon to file if not exists
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.ico");
            if (!File.Exists(iconPath))
            {
                try
                {
                    using (Icon icon = IconHelper.CreateAppIcon())
                    using (FileStream fs = new FileStream(iconPath, FileMode.Create))
                    {
                        icon.Save(fs);
                    }
                }
                catch { }
            }

            // Check for Admin rights
            if (!IsRunAsAdmin())
            {
                // Re-run with Admin rights
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                try
                {
                    Process.Start(processInfo);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Cannot run application with Admin rights.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static bool IsRunAsAdmin()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }
    }
}
