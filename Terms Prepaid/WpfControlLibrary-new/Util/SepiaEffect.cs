using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;

namespace WpfControlLibrary.Util
{
    public class SepiaEffect : ShaderEffect
    {
        private PixelShader pixelShader = new PixelShader();

        public SepiaEffect()
        {
            //pixelShader.UriSource = new System.Uri("../Shaders/Sepia.ps", UriKind.Relative);
            pixelShader.UriSource = new System.Uri(@"E:\MyProjects\terms prepaid\WpfControlLibrary\Shaders\Sepia.ps", UriKind.Absolute);
            //pixelShader.UriSource = new Uri("../Shaders/Sepia.ps", UriKind.Relative);
            this.PixelShader = pixelShader;
            this.UpdateShaderValue(InputProperty);
        }

        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input",
            typeof(SepiaEffect), 0);

        public Brush Input
        {
            get
            {
                return ((Brush)this.GetValue(InputProperty));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }
    }
}
