using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GitHubApp.View
{
    public class CustomListView : ListView
    {
        public static readonly BindableProperty EvenRowsColorProperty =
            BindableProperty.Create("EvenRowsColor", typeof(Color), typeof(CustomListView), Color.Transparent, propertyChanged: OnRowsColorPropertyChanged);

        public static readonly BindableProperty OddRowsColorProperty =
            BindableProperty.Create("OddRowsColor", typeof(Color), typeof(CustomListView), Color.Transparent, propertyChanged: OnRowsColorPropertyChanged);

        public Color EvenRowsColor
        {
            get => (Color)GetValue(EvenRowsColorProperty);
            set => SetValue(EvenRowsColorProperty, value);
        }

        public Color OddRowsColor
        {
            get => (Color)GetValue(OddRowsColorProperty);
            set => SetValue(OddRowsColorProperty, value);
        }

        public CustomListView()
        {
        }

        public CustomListView(ListViewCachingStrategy strategy) : base(strategy)
        {
        }

        private static void OnRowsColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Some logic to refresh the list view.
        }

        protected override void SetupContent(Cell pContent, int pIndex)
        {
            base.SetupContent(pContent, pIndex);

            if (pContent is CustomCell cell)
            {
                if (pIndex % 2 == 0)
                {
                    cell.View.BackgroundColor = this.EvenRowsColor;
                }
                else
                {
                    cell.View.BackgroundColor = this.OddRowsColor;
                }
            }
        }
    }
}
