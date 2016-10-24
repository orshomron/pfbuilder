using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.Tooltips
{
    /// <summary>
    /// Interaction logic for BalloonContainer.xaml
    /// </summary>
    public partial class BalloonContainer : UserControl
    {
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof (string), typeof (BalloonContainer), new PropertyMetadata(default(string)));

        public string Caption
        {
            get { return (string) GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        private BalloonWindow _balloon;

        public BalloonContainer()
        {
            _balloon = null;
            InitializeComponent();
        }

        public BalloonWindow.Position Position { get; set; }

        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            if (_balloon == null)
            {
                _balloon = new BalloonWindow(this, Caption, Position);
                _balloon.Closed += BalloonClosed;
                _balloon.Show();
            }
        }

        private void BalloonClosed(object sender, EventArgs e)
        {
            _balloon = null;
        }
    }
}
