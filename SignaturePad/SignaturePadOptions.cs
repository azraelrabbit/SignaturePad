﻿using System.Drawing;
using System.Dynamic;
using System.Text.Json;

namespace AZSignaturePad
{
    public class SignaturePadOptions
    {
        public int LineWidth { get; set; } = 1;
        public Color StrokeStyle { get; set; } = Color.FromArgb(34, 34, 34);

        public float AspectRatio = 0;

        public string ToJSON()
        {
            dynamic json = new ExpandoObject();
            json.lineWidth = LineWidth;
            json.aspectRatio= AspectRatio;
            json.strokeStyle = $"#{StrokeStyle.R.ToString("X2")}{StrokeStyle.G.ToString("X2")}{StrokeStyle.B.ToString("X2")}";

            return JsonSerializer.Serialize(json);
        }

    }
}