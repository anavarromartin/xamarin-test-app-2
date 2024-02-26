Xamarin App built with the purpose of learning the technology. The purpose of the app is to generate workout plans 
using AI based on an input.

## Learnings
### Configuration management using Mobile.BuildTools
Using the Version 2 of Mobile.BuildTools, make sure there is a buildtools.json file in your project. There you need 
to populate the `appSettings` property like it is done in this project. You may also specify different `environment.
configuration` fields for different configurations.

Add those properties in json files in your project using this naming format:
`appsettings.[<Environment>.].json`. For instance:
- `appsettings.json`
- `appsettings.Release.json`

In the `appsettings` files you need to add your configuration fields in json format.
```json
{
  "apiKey": "my_cool_api_key"
}
```
If using AppCenter, you will need to add the same fields as environment variables. Mobile.BuildTools will pick them app.
### UI Testing

#### iOS testing
Install `idb`, otherwise the iOS device will not be found.

### Unit Testing
#### From command line
There is an issue with [Mobile.BuildTools](https://github.com/dansiegel/Mobile.BuildTools/issues/334) and the dotnet version. Include a `global.json` in the project to set it 
to version 7, like it is done in this project.
```json
{
  "sdk": {
    "version": "7.0.313",
    "rollForward": "latestFeature"
  }
}
```
#### Use the right NUnit and test adapter
If using NUnit3TestAdapter, make sure you use NUnit version 3, otherwise no tests will be found.
```xml
<PackageReference Include="NUnit" Version="3.12.0" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
```

### Github Actions
Mobile.BuildTools automatic versioning setting has to be turned off (or do something other than `PreferBuildNumber`),
otherwise it will set an value higher than an integer as a version and the build with fail with an error similar to 
this:
```
  /Users/runner/work/xamarin-test-app-2/xamarin-test-app-2/XamarinDemo2/XamarinDemo2.Android/obj/Release/android/AndroidManifest.xml : error XA0003: VersionCode 8024520372 is invalid. It must be an integer value. [/Users/runner/work/xamarin-test-app-2/xamarin-test-app-2/XamarinDemo2/XamarinDemo2.Android/XamarinDemo2.Android.csproj]
```

### App Center
#### Unit Testing
Use SDK version 6 in `globals.json`.

### Android builds
Add the following to the android `.csproj` in order to sign the builds and embed the assemblies into the APK for it 
to work on a real device:
```xml

<EmbedAssembliesIntoApk>true</EmbedAssembliesIntoApk>
<AndroidSupportedAbis>armeabi-v7a;x86;arm64-v8a;x86_64</AndroidSupportedAbis>
<AndroidKeyStore>true</AndroidKeyStore>
<AndroidSigningKeyStore>AIWorkoutKeystore</AndroidSigningKeyStore>
<AndroidSigningStorePass>password123</AndroidSigningStorePass>
<AndroidSigningKeyAlias>signing_key</AndroidSigningKeyAlias>
<AndroidSigningKeyPass>password123</AndroidSigningKeyPass>
```

### Other
Add MSBuildEnableWorkloadResolver=false in MSBuild properties in Toolset and Build in Rider settings. More details 
[here](https://youtrack.jetbrains.com/issue/RIDER-97292/The-SDK-Microsoft.NET.SDK.
WorkloadAutoImportPropsLocator-specified-could-not-be-found.).