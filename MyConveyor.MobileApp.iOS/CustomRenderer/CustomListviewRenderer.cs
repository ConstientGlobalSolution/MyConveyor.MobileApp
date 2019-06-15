using MyConveyor.MobileApp.CustomClasses;
using MyConveyorMobileApp.iOS.CustomRenderer;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AlternatingListView), typeof(CustomListviewRenderer))]
namespace MyConveyorMobileApp.iOS.CustomRenderer
{
    public class CustomListviewRenderer : ListViewRenderer
    {
        private IDisposable offsetObserver;
        private double prevYOffset;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is AlternatingListView)
                offsetObserver = Control.AddObserver("contentOffset",
                             Foundation.NSKeyValueObservingOptions.New, HandleAction);

        }

        private void HandleAction(Foundation.NSObservedChange obj)
        {
            double effectiveY = Math.Max(Control.ContentOffset.Y, 0);
            if (!CloseTo(effectiveY, prevYOffset) && Element is AlternatingListView)
            {
                AlternatingListView myList = Element as AlternatingListView;

                myList.IsScrolling = true;
                prevYOffset = effectiveY;
            }

        }
        private static bool CloseTo(double x, double y)
        {
            return Math.Abs(x - y) < 0.1;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && offsetObserver != null)
            {
                offsetObserver.Dispose();
                offsetObserver = null;
            }

        }
    }
}