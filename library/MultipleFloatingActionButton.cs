using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace bradlak
{
    [Register("bradlak.MultipleFloatingActionButton")]
    public class MultipleFloatingActionButton : RelativeLayout
    {
        private FloatingActionButton mainFab;

        private RelativeLayout container;

        private List<FloatingActionButton> fabs;

        private Action onMainClick;

        private bool mainWithAnimation = false;

        private AnimationType type = AnimationType.None;

        private float initialX;

        private float initialY;

        public MultipleFloatingActionButton(
                    Context context,
                    IAttributeSet attrs)
                    : base(context, attrs)
        {
            Init(attrs);
        }

        public MultipleFloatingActionButton(Context context, IAttributeSet attrs, int defStyleAttr)
                    : base(context, attrs, defStyleAttr)
        {
            Init(attrs);
        }

        public bool IsMenuOpened { get; private set; }

        protected void Init(IAttributeSet attrs)
        {
            var inflater = LayoutInflater.From(Context);
            inflater.Inflate(Resource.Layout.MultipleFloatingActionButtonView, this);

            mainFab = FindViewById<FloatingActionButton>(Resource.Id.MainFloatingButton);

            container = FindViewById<RelativeLayout>(Resource.Id.container);

            mainFab.Click += MainFab_Click;

            fabs = new List<FloatingActionButton>();
        }

        public void SetMainButton(int colorId, int drawableId, Action onClick, bool withAnimation)
        {
            mainFab.BackgroundTintList = ColorStateList.ValueOf(new Color(ContextCompat.GetColor(Context, colorId)));
            mainFab.SetImageDrawable(ContextCompat.GetDrawable(Context, drawableId));
            onMainClick = onClick;
            mainWithAnimation = withAnimation;
        }

        public void AddAction(int colorId, int drawableId, Action onClick)
        {
            var newFab = new FloatingActionButton(Context);
            newFab.UseCompatPadding = true;
            var layoutParams = new LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            layoutParams.AddRule(LayoutRules.AlignParentBottom);
            layoutParams.AddRule(LayoutRules.AlignParentRight);
            newFab.LayoutParameters = layoutParams;
            newFab.Size = FloatingActionButton.SizeMini;
            newFab.Visibility = ViewStates.Invisible;
            newFab.BackgroundTintList = ColorStateList.ValueOf(new Color(ContextCompat.GetColor(Context, colorId)));
            newFab.SetImageDrawable(ContextCompat.GetDrawable(Context, drawableId));
            newFab.Click += (sender, e) =>
            {
                onClick?.Invoke();
            };

            container.AddView(newFab, 0);
            fabs.Add(newFab);
        }

        public void SetAnimation(AnimationType type)
        {
            this.type = type;
        }

        public void OpenMenu()
        {
            AnimateMenu(true);
        }

        public void HideMenu()
        {
            AnimateMenu(false);
        }

        private void MainFab_Click(object sender, EventArgs e)
        {
            AnimateMenu(!IsMenuOpened);
            onMainClick?.Invoke();
        }

        private void AnimateMenu(bool opening)
        {
            if (opening != IsMenuOpened)
            {
                ProcessFabs();

                if (opening)
                {
                    container.SetBackgroundColor(Color.ParseColor("#80000000"));

                    if (mainWithAnimation)
                        mainFab.Animate().Rotation(45).SetDuration(300).Start();
                }
                else
                {
                    container.SetBackgroundColor(Color.Transparent);

                    if (mainWithAnimation)
                        mainFab.Animate().Rotation(0).SetDuration(300).Start();
                }

                IsMenuOpened = opening;
            }
        }

        private void ProcessFabs()
        {
            var angle = 0.0f;

            for (int i = 0; i < fabs.Count; i++)
            {
                var offset = fabs.Count > 4 ? 0.25f : 0.1f;
                if (fabs.Count == 1) angle = 45;
                if (fabs.Count == 2) angle = i == 0 ? 20 : 70;
                var radians = angle * System.Math.PI / 180;
                var radius = fabs[i].Width + (fabs[i].Width * offset * fabs.Count);
                var tx = radius * (float)System.Math.Cos(radians);
                var ty = radius * (float)System.Math.Sin(radians);

                if (IsMenuOpened)
                {
                    tx = initialX;
                    ty = initialY;
                    HideFab(fabs[i], tx, ty, i);
                }
                else
                {
                    fabs[i].TranslationX = initialX = mainFab.Left - fabs[i].Left + mainFab.Width / 8;
                    fabs[i].TranslationY = initialY = mainFab.Top - fabs[i].Top + mainFab.Height / 8;
                    ShowFab(fabs[i], tx, ty, i);
                }

                angle += 90 / ((float)fabs.Count - 1);
            }
        }

        private void ShowFab(FloatingActionButton fab, float tx, float ty, int index)
        {
            switch (type)
            {
                case AnimationType.Explosion:

                    var endAction = new Runnable(() => fab.Clickable = true);
                    fab.Visibility = ViewStates.Visible;
                    fab.Animate().TranslationX(-tx).TranslationY(-ty).SetDuration(500).SetStartDelay(0).WithEndAction(endAction).Start();

                    break;

                case AnimationType.Fade:

                    fab.TranslationX = -tx;
                    fab.TranslationY = -ty;
                    fab.Alpha = 0;
                    fab.Visibility = ViewStates.Visible;
                    fab.Animate().Alpha(1.0f).SetDuration(800).SetStartDelay(index * 200).Start();
                    fab.Clickable = true;
                    break;

                case AnimationType.None:

                    fab.TranslationX = -tx;
                    fab.TranslationY = -ty;
                    fab.Visibility = ViewStates.Visible;
                    fab.Clickable = true;

                    break;
            }
        }

        private void HideFab(FloatingActionButton fab, float tx, float ty, int index)
        {
            switch (type)
            {
                case AnimationType.Explosion:

                    var endActionExp = new Runnable(() => fab.Visibility = ViewStates.Gone);
                    fab.Clickable = false;
                    fab.Animate().TranslationX(tx).TranslationY(ty).SetDuration(500).SetStartDelay(0).WithEndAction(endActionExp).Start();

                    break;

                case AnimationType.Fade:

                    var endActionFade = new Runnable(() =>
                    {
                        fab.TranslationX = tx;
                        fab.TranslationY = ty;
                        fab.Visibility = ViewStates.Gone;
                        fab.Alpha = 1.0f;
                    });
                    fab.Clickable = false;

                    fab.Animate().Alpha(0).SetDuration(800).SetStartDelay(index * 200).WithEndAction(endActionFade).Start();

                    break;


                case AnimationType.None:

                    fab.TranslationX = tx;
                    fab.TranslationY = ty;
                    fab.Visibility = ViewStates.Gone;
                    fab.Clickable = false;

                    break;
            }
        }

        public enum AnimationType
        {
            None,
            Explosion,
            Fade,
        }
    }
}