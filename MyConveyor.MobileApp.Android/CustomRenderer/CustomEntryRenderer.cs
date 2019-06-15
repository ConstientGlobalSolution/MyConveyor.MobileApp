using Android.Graphics.Drawables;
using MyConveyor.MobileApp.Android.CustomRenderer;
using MyConveyor.MobileApp.CustomClasses;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace MyConveyor.MobileApp.Android.CustomRenderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetStroke(5, Color.Gray.ToAndroid());
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }

        }

    }
}