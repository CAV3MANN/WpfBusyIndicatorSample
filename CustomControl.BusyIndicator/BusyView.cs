using System.Windows;
using System.Windows.Controls;

namespace CustomControl.BusyIndicator
{
    public class BusyView : ContentControl
    {
        static BusyView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyView), new FrameworkPropertyMetadata(typeof(BusyView)));
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyView), new FrameworkPropertyMetadata()
            {
                AffectsRender = true,
                DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged,
                PropertyChangedCallback = new PropertyChangedCallback(OnBusyChanged)
            });

        public static readonly RoutedEvent IsBusyStarted = EventManager
            .RegisterRoutedEvent("IsBusyStarted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BusyView));

        public static readonly RoutedEvent IsBusyStopped = EventManager
            .RegisterRoutedEvent("IsBusyStopped", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BusyView));

        private static void OnBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newBusyValue = (bool)e.NewValue;

            var busyView = (BusyView)d;

            if (newBusyValue)
            {
                busyView.RaiseIsBusyStarted();
            }
            else
            {
                busyView.RaiseIsBusyStopped();
            }
        }

        private void RaiseIsBusyStarted()
        {
            RaiseEvent(new RoutedEventArgs(IsBusyStarted));
        }

        private void RaiseIsBusyStopped()
        {
            RaiseEvent(new RoutedEventArgs(IsBusyStopped));
        }
    }
}
