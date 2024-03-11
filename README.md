<!-- Image sourced from https://blog.1password.com/introducing-secrets-automation/ -->
<img alt="" role="img" src="https://blog.1password.com/posts/2021/secrets-automation-launch/header.svg"/>

<div align="center">
	<h1>1Password Connect SDK C#</h1>
	<p>Access your 1Password items in your dotnet applications through your self-hosted <a href="https://developer.1password.com/docs/connect">1Password Connect server</a>.</p>
	<a href="/QUICKSTART.md">
		<img alt="Get started" src="https://user-images.githubusercontent.com/45081667/226940040-16d3684b-60f4-4d95-adb2-5757a8f1bc15.png" height="37"/>
	</a>
</div>

---

The 1Password Connect SDK C# provides your dotnet applications access to the 1Password Connect API hosted on your infrastructure and leverages the power of [1Password Secrets Automation](https://1password.com/product/secrets/).

This library can be used by dotnet applications to access and manage items in 1Password vaults.

## 💾 Installation

You can install the SDK using NUGET.

```sh
dotnet add pacakge 1PasswordConnect.Sdk
```

If you want to integrate with HttpClientFactory and Microsoft.Extensions.DependencyInjection

```sh
dotnet add pacakge 1PasswordConnect.Extensions.DependencyInjection
```

## ✨ Get Started

Refer to [QUICKSTART.md](/QUICKSTART.md) for code examples on how to start using this library.

## 💙 Community & Support

-   File an [issue](https://github.com/1Password/connect-sdk-js/issues) for bugs and feature requests.
-   Join the [Developer Slack workspace](https://join.slack.com/t/1password-devs/shared_invite/zt-1halo11ps-6o9pEv96xZ3LtX_VE0fJQA).
-   Subscribe to the [Developer Newsletter](https://1password.com/dev-subscribe/).

## 🔐 Security

1Password requests you practice responsible disclosure if you discover a vulnerability. Please submit discoveries via [BugCrowd](https://bugcrowd.com/agilebits).

For information about security practices, please visit our [Security homepage](https://1password.com/security/).
