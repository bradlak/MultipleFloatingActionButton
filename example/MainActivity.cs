using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using bradlak;

namespace MultiFabSample
{
    [Activity(Label = "brcontrols", Theme = "@style/Theme.AppCompat.Light", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var multipleFab = FindViewById<MultipleFloatingActionButton>(Resource.Id.multipleFab);
            multipleFab.SetMainButton(Resource.Color.Pink, Resource.Drawable.plus, null, true);

            FindViewById<Button>(Resource.Id.noneButton).Click +=
                (sender, e) => multipleFab.SetAnimation(MultipleFloatingActionButton.AnimationType.None);
            FindViewById<Button>(Resource.Id.expButton).Click +=
                (sender, e) => multipleFab.SetAnimation(MultipleFloatingActionButton.AnimationType.Explosion);
            FindViewById<Button>(Resource.Id.fadeButton).Click +=
                (sender, e) => multipleFab.SetAnimation(MultipleFloatingActionButton.AnimationType.Fade);


            multipleFab.AddAction(Resource.Color.Yellow, Resource.Drawable.share, () =>
            {
                Toast.MakeText(this, "share clicked", ToastLength.Short).Show();
            });
            multipleFab.AddAction(Resource.Color.Blue, Resource.Drawable.@lock, () =>
            {
                Toast.MakeText(this, "lock clicked", ToastLength.Short).Show();
            });
            multipleFab.AddAction(Resource.Color.Green, Resource.Drawable.refresh, () =>
            {
                Toast.MakeText(this, "refresh clicked", ToastLength.Short).Show();
            });
            multipleFab.AddAction(Resource.Color.Orange, Resource.Drawable.star, () =>
            {
                Toast.MakeText(this, "star clicked", ToastLength.Short).Show();
            });
        }
    }
}

