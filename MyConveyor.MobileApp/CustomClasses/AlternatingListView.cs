using Xamarin.Forms;

namespace MyConveyor.MobileApp.CustomClasses
{
    public class AlternatingListView : ListView
    {
        public static readonly BindableProperty
            IsScrollingProperty =
                BindableProperty.Create(nameof(IsScrolling),
                typeof(bool), typeof(AlternatingListView), false);

        public bool IsScrolling
        {
            get => (bool)GetValue(IsScrollingProperty);
            set => SetValue(IsScrollingProperty, value);
        }

        public AlternatingListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
        }

        public AlternatingListView()
        {

        }

        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);

            ViewCell viewCell = content as ViewCell;
            viewCell.View.BackgroundColor = index % 2 == 0 ? Color.FromHex("bfc9cb") : Color.FromHex("dbe4e9");
        }
    }
}
