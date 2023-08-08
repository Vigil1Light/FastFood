using System;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FastFood.Droid.Effects;
using Color = Android.Graphics.Color;

[assembly: ResolutionGroupName("FastFood")]
[assembly: ExportEffect(typeof(RemoveEntryUnderline), nameof(RemoveEntryUnderline))]
namespace FastFood.Droid.Effects
{
    public class RemoveEntryUnderline : PlatformEffect
    {
        protected override void OnAttached()
        {
            var editText = this.Control as EditText;

            if (editText is null)
                throw new NotImplementedException();

            editText.SetBackgroundColor(Color.Transparent);
        }

        protected override void OnDetached()
        {
        }
    }
}