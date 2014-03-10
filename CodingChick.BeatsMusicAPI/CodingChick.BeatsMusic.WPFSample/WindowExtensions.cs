using System.Windows;

namespace CodingChick.BeatsMusic.WPFSample
{
    public static class WindowExtensions
    {
        public static void StretchToMaximum(this Window current)
        {
            current.WindowState = WindowState.Maximized;
            current.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            current.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            current.WindowStyle = WindowStyle.None;
            current.AllowsTransparency = false;
            current.ResizeMode = ResizeMode.CanResize;
        }


        public static void Navigate(this Window current, Window next)
        {
            Application.Current.MainWindow = next;
            next.Show();
            current.Close();
        }
    }
}