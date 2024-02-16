using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            switch (platform)
            {
                case Platform.Android:
                    return ConfigureApp.Android
                        .ApkFile("../../../XamarinDemo2/XamarinDemo2.Android/bin/Release/com.companyname.xamarindemo2.apk")
                        .StartApp();
                case Platform.iOS:
                    return ConfigureApp.iOS
                        .AppBundle(
                            "/Users/adrian/workspace/XamarinDemo2/XamarinDemo2/XamarinDemo2.iOS/bin/iPhoneSimulator/Release/XamarinDemo2.iOS.app")
                        // "../../../XamarinDemo2/XamarinDemo2.iOS/bin/iPhoneSimulator/Release/XamarinDemo2.iOS.app")
                        .DeviceIdentifier("21BA36C2-39AE-4E26-ADDF-FCB7CFF2BC17")
                        .StartApp();
                default:
                    throw new Exception("Unknown platform");
            }
        }
    }
}