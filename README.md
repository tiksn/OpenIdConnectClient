# Open ID Connect Client

A small OpenID Connect client for testing authentication flows. The app accepts
client settings such as authority, client ID, client secret, scope, and redirect
URI, then signs in, refreshes tokens, signs out, and displays returned tokens and
claims.

## Current App

The application is now implemented with .NET MAUI in `OpenIdConnectClient.Maui`.
It is wired to the shared ViewModels in `OpenIdConnectClient.ViewModels` and
targets the default MAUI platforms:

- Android
- iOS
- Mac Catalyst
- Windows

The previous WPF implementation has been removed after manual verification of
the MAUI login flow.

## Redirect URI

The default redirect URI is:

```text
oidcc://callback
```

Register this exact redirect URI in your OpenID Connect provider/client
configuration. The MAUI project includes platform callback registration for this
scheme:

- Android: `Platforms/Android/WebAuthenticationCallbackActivity.cs`
- iOS: `Platforms/iOS/Info.plist`
- Mac Catalyst: `Platforms/MacCatalyst/Info.plist`
- Windows: `Platforms/Windows/Package.appxmanifest`

## Default Demo Provider

The app opens with Duende's public demo IdentityServer configured:

```text
Authority: https://demo.duendesoftware.com
Client ID: interactive.public
Client secret: <blank>
Scope: openid profile email offline_access api
Redirect URI: oidcc://callback
```

The `interactive.public` client uses authorization code flow with PKCE and does
not require a client secret. For manual login testing, Duende documents the demo
users as `alice` / `alice` and `bob` / `bob`.

## Authentication Browser

Android, iOS, and Mac Catalyst use .NET MAUI `WebAuthenticator` for the
interactive browser flow. The Windows target uses a MAUI `WebView` fallback for
the same OIDC browser adapter because Microsoft's MAUI documentation currently
notes that `WebAuthenticator` is not working on Windows.

## Build

Build the Windows MAUI target:

```powershell
dotnet build OpenIdConnectClient.Maui\OpenIdConnectClient.Maui.csproj -f net10.0-windows10.0.19041.0
```

Build the other MAUI targets:

```powershell
dotnet build OpenIdConnectClient.Maui\OpenIdConnectClient.Maui.csproj -f net10.0-android
dotnet build OpenIdConnectClient.Maui\OpenIdConnectClient.Maui.csproj -f net10.0-ios
dotnet build OpenIdConnectClient.Maui\OpenIdConnectClient.Maui.csproj -f net10.0-maccatalyst
```

## Run On Windows

Build first, then launch the generated executable:

```powershell
dotnet build OpenIdConnectClient.Maui\OpenIdConnectClient.Maui.csproj -f net10.0-windows10.0.19041.0
.\OpenIdConnectClient.Maui\bin\Debug\net10.0-windows10.0.19041.0\win-x64\OpenIdConnectClient.Maui.exe
```

The Windows target is configured with `WindowsAppSDKSelfContained=true` so the
app can run even when the Windows App SDK runtime is not registered globally on
the machine.

## Usage

1. Start the MAUI app.
2. Enter the OpenID Connect authority, client ID, client secret, scope, and
   redirect URI.
3. Select `Log In` to authenticate through the platform browser flow.
4. Review the access token, identity token, refresh token, expiration, and
   claims. If authentication fails, review the `Error` and `Error Description`
   fields.
5. Use `Refresh`, `Auto Refresh`, and `Log Out` as needed.

## Validation

The MAUI project builds for Windows, Android, iOS, and Mac Catalyst. Manual
Windows verification has completed against the Duende demo provider with the
`interactive.public` client and the `alice` demo account.
