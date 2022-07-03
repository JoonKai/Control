using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlsTest
{
    /// <summary>
    /// Anemoscope.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Anemoscope : UserControl
    {
        public static readonly DependencyProperty ScopeColorProperty = 
            DependencyProperty.Register("ScopeProperty", typeof(Brush), typeof(Anemoscope), new FrameworkPropertyMetadata(Brushes.Green));
        public static readonly DependencyProperty WindDirectionProperty = DependencyProperty.Register("WindDirection", typeof(double), typeof(Anemoscope), 
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(WindDirectionChanged)));

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextProperty", typeof(Brush), typeof(Anemoscope), new FrameworkPropertyMetadata(Brushes.Green));


        public Brush TextBlocksColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        private static void WindDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Anemoscope control = (Anemoscope)d;
            control.RotateNiddle((double)e.OldValue, (double)e.NewValue);
        }

        private void RotateNiddle(double oldValue, double newValue)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = oldValue;
            animation.To = newValue;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            rotate.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        public Brush ScopeColor
        {
            get { return (Brush)GetValue(ScopeColorProperty); }
            set { SetValue(ScopeColorProperty, value); }
        }
        public double WindDirection
        {
            get { return (double)GetValue(WindDirectionProperty); }
            set { SetValue(WindDirectionProperty, value); }
        }
        public Anemoscope()
        {
            InitializeComponent();
        }
    }
}
