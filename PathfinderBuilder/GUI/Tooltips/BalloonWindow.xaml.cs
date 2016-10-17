using System;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Tooltips
{
    /// <summary>
    /// Interaction logic for BalloonWindow.xaml
    /// </summary>
    public partial class BalloonWindow : Window
    {
        public BalloonWindow(Control control, string caption, Position position)
        {
            InitializeComponent();

            textBlockCaption.Text = caption;

            // Compensate for the bubble point
            double captionPointMargin = PathPointLeft.Margin.Left;

            Point location = GetControlPosition(control);

            if (position == Position.Left)
            {
                PathPointRight.Visibility = Visibility.Hidden;
                Left = location.X + (control.ActualWidth / 2) - captionPointMargin;
            }
            else
            {
                PathPointLeft.Visibility = Visibility.Hidden;
                Left = location.X - Width + control.ActualWidth + (captionPointMargin / 2);
            }

            Top = location.Y + (control.ActualHeight / 2);
        }

        public enum Position
        {
            Left,
            Right
        }

        private static Point GetControlPosition(Control control)
        {
            Point locationToScreen = control.PointToScreen(new Point(0, 0));
            var source = PresentationSource.FromVisual(control);
            return source.CompositionTarget.TransformFromDevice.Transform(locationToScreen);
        }

        private void DoubleAnimationCompleted(object sender, EventArgs e)
        {
            if (!IsMouseOver)
            {
                Close();
            }
        }
    }

}
