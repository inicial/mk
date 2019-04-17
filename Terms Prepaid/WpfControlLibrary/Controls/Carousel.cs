using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfControlLibrary.Controls
{
    public partial class Carousel : ListView
    {

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Init();
        }

        private void Init()
        {
            
        }

        public void SetPosition()
        {
            
            foreach (var item in Items)
            {
                FrameworkElement element = item as FrameworkElement;
                ScaleTransform scale = element.RenderTransform as ScaleTransform;
                if (scale == null)
                {
                    scale = new ScaleTransform();
                    element.RenderTransform = scale;
                }
            }
        }

    }
}
