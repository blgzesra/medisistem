using System.Drawing;

namespace MediSistem
{
    public static class UITheme
    {
        public static Color Primary = Color.FromArgb(59, 130, 246);          // Mavi ton
        public static Color Danger = Color.FromArgb(239, 68, 68);           // Kırmızı ton
        public static Color Background = Color.FromArgb(243, 244, 246);     // Açık gri arka plan
        public static Color Card = Color.White;
        public static Color TextMain = Color.FromArgb(15, 23, 42);
        public static Color TextMuted = Color.FromArgb(107, 114, 128);
        public static Font Heading = new Font("Segoe UI", 20, FontStyle.Bold);
        public static Font Normal = new Font("Segoe UI", 10, FontStyle.Regular);
    }
}
