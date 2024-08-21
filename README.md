![](nuget.png)
# Plugin.Maui.UpDownControl

`Plugin.Maui.UpDownControl` provides the ability to include an up and down numeric control in your .NET MAUI application.

## Install Plugin

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.Feature.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.Feature/)

Available on [NuGet](http://www.nuget.org/packages/Plugin.Maui.UpDownControl).

Install with the dotnet CLI: `dotnet add package Plugin.Maui.UpDownControl`, or through the NuGet Package Manager in Visual Studio.

### Supported Platforms

| Platform | Minimum Version Supported |
|----------|---------------------------|
| iOS      | 11+                       |
| macOS    | 10.15+                    |
| Android  | 5.0 (API 21)              |
| Windows  | 11 and 10 version 1809+   |

## API Usage

`Plugin.Maui.UpDownControl` provides the `UpDown` class that has various properties that you can set in order to customize the UI of the control and/or customize its functionallity.

Set the `InitialValue` property in order to set the initial value from which the control will start to increment/decrement based on the `Step` property.
You can also set the `Culture` property in order to achieve localization regarding the form of the decimal and group separators etc.
Finally, you can set the `UpperLimit` and `LowerLimit` properties to further customize the behavior of the control based on your needs.

In case the upper or lower limit is reached, you will be notified via the corresponding event handlers `UpperLimitReached` and `LowerLimitReached`.
You will be notified for each value change via the `ValueChanged` event handler.

### Permissions

No need for permissions.

#### iOS

No permissions are needed for iOS.

#### Android

No permissions are needed for Android.