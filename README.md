# MultipleFloatingActionButton
Configurable ,elastic floating action menu for Xamarin.Android

### Description
Circluar floating action menu with configurable amount of actions and bunch of animations.


Two actions |  Three actions  | Four actions
---|---|---
![two](https://github.com/bradlak/MultipleFloatingActionButton/blob/master/Media/twoFabs.gif "Two") |![three](https://github.com/bradlak/MultipleFloatingActionButton/blob/master/Media/threeFabs.gif) |![four](https://github.com/bradlak/MultipleFloatingActionButton/blob/master/Media/fourFabs.gif) |

And so on...

### Integration

**1)** Add NuGet package to your project: https://www.nuget.org/packages/bradlak.MultipleFloatingActionButton

**2)** Add ''bradlak.MultipleFloatingActionButton'' to your layout XML file and place it in the bottom right corner.

```xml
<RelativeLayout xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:app="http://schemas.android.com/apk/res-auto"
    android:layout_width="match_parent"
    android:layout_height="wrap_content">
    <LinearLayout
        android:id="@+id/<YOUR-CONTENT>"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        android:orientation="vertical"
        android:layout_alignParentTop="true"
        android:gravity="center_horizontal">
    </LinearLayout>
    <bradlak.MultipleFloatingActionButton
        android:id="@+id/multipleFab"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:layout_alignParentBottom="true"
        android:layout_alignParentRight="true" />
</RelativeLayout>
```

**3)** Find and configure newly added view.

```csharp
var multipleFab = FindViewById<MultipleFloatingActionButton>(Resource.Id.multipleFab);
```

+ Set the main fab with color, icon, click action and rotate animation.

     ```csharp
      multipleFab.SetMainButton(Resource.Color.Pink, Resource.Drawable.plus, null, true);
     ```

+ Select animation type

     ```csharp
     multipleFab.SetAnimation(MultipleFloatingActionButton.AnimationType.Explosion);
     ```

     None, Explosion, Fade animations available.

+ Add new actions

     ```csharp
     multipleFab.AddAction(Resource.Color.Yellow, Resource.Drawable.share, () =>
     {
         Toast.MakeText(this, "share clicked", ToastLength.Short).Show();
     });
     ```
	 
**4)** Done!



# License

The MIT License (MIT)

Copyright (c) 2017 Bartosz Radlak

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.