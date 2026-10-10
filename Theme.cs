using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// Central place for the application's colour scheme and fonts, so every
    /// form stays visually consistent (Design Consideration 1: Consistency)
    /// without repeating literal colour values across each Designer file.
    /// </summary>
    internal static class Theme
    {
        //Municipal blue / green palette
        public static readonly Color PrimaryColor = Color.FromArgb(28, 74, 112); //Header / primary buttons
        public static readonly Color AccentColor = Color.FromArgb(0, 150, 90); //success / engagement accents
        public static readonly Color BackgroundColor = Color.FromArgb(245, 247, 250);

        public static readonly Color DisabledColor = Color.FromArgb(120, 120, 120);

        public const string FontFamily = "Segoe UI";
    }
}
