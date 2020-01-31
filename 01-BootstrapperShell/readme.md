# Manually Boostrapping Prism

This sample application shows how to do a basic bootstrap of Unity.

> **❗️ Note**: You will see a **CS0618** compiler warning in your application due to manually bootstrapping your application. Don't worry, we will show you how to fix this in a later sample.

## Takeaways

-   Understanding of how you can use the [UnityBootstrapper](https://github.com/PrismLibrary/Prism/blob/master/Source/Wpf/Prism.Unity.Wpf/Legacy/UnityBootstrapper.cs#L29) class to bootstrap a basic Prism application
-   Understanding of how to configure the default shell for a Prism application

## Try it out

### Visual Studio 2019

1.  Navigate to the **01-BootstrapperShell** folder.
1.  Open the **BootstrapperShell.sln** file in Visual Studio 2019.
1.  **Debug** the **BootstrapperShell** project.

### .NET CLI

```bash
cd '.\01-BootstrapperShell\src\BootstrapperShell\'
dotnet run
```