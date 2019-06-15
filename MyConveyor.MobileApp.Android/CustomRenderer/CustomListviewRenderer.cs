using Android.Content;
using Android.Widget;
using MyConveyor.MobileApp.Android.CustomRenderer;
using MyConveyor.MobileApp.CustomClasses;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AlternatingListView), typeof(CustomListviewRenderer))]

namespace MyConveyor.MobileApp.Android.CustomRenderer
{
    public class CustomListviewRenderer : ListViewRenderer
    {
        public CustomListviewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is AlternatingListView)
            {
                Control.Scroll += ControlScroll;
            }
        }

        private void ControlScroll(object sender, AbsListView.ScrollEventArgs e)
        {
            AlternatingListView myList = Element as AlternatingListView;
            myList.IsScrolling = true;
        }

    }
}