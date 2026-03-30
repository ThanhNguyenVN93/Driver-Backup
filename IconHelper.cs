using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace frmdriverbackup
{
    public static class IconHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        /// <summary>
        /// Create app icon (Backup/Restore symbol)
        /// </summary>
        public static Icon CreateAppIcon()
        {
            Bitmap bitmap = new Bitmap(256, 256);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw background with blue gradient
                Rectangle bgRect = new Rectangle(0, 0, 256, 256);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    bgRect,
                    Color.FromArgb(0, 120, 215),
                    Color.FromArgb(0, 90, 158),
                    45f))
                {
                    g.FillRectangle(brush, bgRect);
                }

                // Draw hard drive icon
                // Draw rectangle representing disk
                RectangleF diskRect = new RectangleF(50, 80, 150, 100);
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(whiteBrush, diskRect);
                }

                // Draw disk border
                using (Pen pen = new Pen(Color.White, 3))
                {
                    g.DrawRectangle(pen, diskRect.X, diskRect.Y, diskRect.Width, diskRect.Height);
                }

                // Draw 2 horizontal lines (disk description)
                using (Pen pen = new Pen(Color.FromArgb(0, 120, 215), 2))
                {
                    g.DrawLine(pen, 70, 110, 220, 110);
                    g.DrawLine(pen, 70, 140, 220, 140);
                }

                // Draw down arrow (download/backup)
                PointF[] arrowDown = new PointF[]
                {
                    new PointF(128, 200),           // Arrow tip
                    new PointF(115, 185),           // Left edge
                    new PointF(121, 185),           // 
                    new PointF(121, 160),           // Left column
                    new PointF(135, 160),           // Right column
                    new PointF(135, 185),           // 
                    new PointF(141, 185)            // Right edge
                };

                using (SolidBrush arrowBrush = new SolidBrush(Color.White))
                {
                    g.FillPolygon(arrowBrush, arrowDown);
                }

                // Draw up arrow (restore) - smaller
                PointF[] arrowUp = new PointF[]
                {
                    new PointF(180, 40),            // Arrow tip
                    new PointF(167, 55),            // Left edge
                    new PointF(173, 55),            // 
                    new PointF(173, 80),            // Left column
                    new PointF(187, 80),            // Right column
                    new PointF(187, 55),            // 
                    new PointF(193, 55)             // Right edge
                };

                using (SolidBrush arrowBrush = new SolidBrush(Color.White))
                {
                    g.FillPolygon(arrowBrush, arrowUp);
                }

                // Draw glow effect around icon
                for (int i = 2; i > 0; i--)
                {
                    using (Pen glowPen = new Pen(Color.FromArgb(i * 30, 255, 255, 255), i))
                    {
                        g.DrawRectangle(glowPen, 5, 5, 246, 246);
                    }
                }
            }

            // Clone icon to safely release original handle
            IntPtr hIcon = bitmap.GetHicon();
            Icon tempIcon = Icon.FromHandle(hIcon);
            Icon result = (Icon)tempIcon.Clone();
            tempIcon.Dispose();
            DestroyIcon(hIcon);
            bitmap.Dispose();
            return result;
        }
    }
}
