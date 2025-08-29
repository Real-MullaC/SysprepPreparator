# SysprepPreparator
The Sysprep preparation tool prepares computers for Sysprep generalization and for image capture.

> [!NOTE]
> This is currently in a separate repository so it can be worked on more easily. In the future, it will be merged into the DISMTools repository.

This tool is designed to be as modular as possible. Checks are performed by *Compatibility Checker Providers* (CCPs). Tasks are performed by *Preparation Tasks* (PTs).

Special Thanks to @Real-MullaC for helping with initial testing and expansion of this tool.

## How do I get started?

No releases are available yet because this is still under construction. So you'll have to build it.

**Requirements:** Visual Studio 2012 and later, .NET Framework 4.8 Developer Pack

> [!NOTE]
> JetBrains Rider is supported, but you will not be able to design forms

1. Clone the repository
2. Open the solution and restore the NuGet packages (ManagedDism)
3. Press <kbd>Ctrl</kbd> + <kbd>F5</kbd> to launch the tool without a debugger

## Contributing to the tool

To do this:

1. Fork the repo
2. Do everything listed in the previous section, but clone your fork instead
3. Make your changes and test them
4. Create a pull request

> [!NOTE]
> Bonus points will be awarded to those who **DON'T VIBE CODE**

Additional documentation for registering Compatibility Checker Providers and Preparation Tasks will be mentioned now.

## Adding Compatibility Checker Providers and Preparation Tasks

The process of adding CCPs and PTs is quite simple:

1. Create a new class in the `SysprepPreparator.Helpers.CompatChecks` or `SysprepPreparator.Helpers.PreparationTasks` namespaces. In Visual Studio, these are located in the `Helpers` folder.
2. Make the class inherit from `CompatibilityCheckProvider` or `PreparationTask`, based on what you're creating.
3. Implement the abstract members of the base class.

Example of a CCP:

```vb.net
Namespace Helpers.CompatChecks

    Public Class ExampleCCP
        Inherits CompatibilityCheckProvider

        Public Overrides Function PerformCompatibilityCheck() As Classes.CompatibilityCheckerProviderStatus
            ' Your code here
            Return Status
        End Function

    End Class

End Namespace
```

In the case of a CCP, to report a status, you have to call the `Status` property in the base class. You can do this by simply referencing `Status` in your code.

Example of a PT:

```vb.net
Namespace Helpers.PreparationTasks

    Public Class ExamplePT
        Inherits PreparationTask

        Public Overrides Function RunPreparationTask() As Boolean
            ' Your code here
            Return ... ' whatever, based on what the PT is for
        End Function

    End Class

End Namespace
```

Then, to register your CCP or PT, add it to one of these properties like this:

- For CCPs you'll have to add it to the `CompatibilityCheckerHelper` module:

```vb.net
Private CompabilityCheckerModules As New List(Of CompatibilityCheckProvider) From {
    New SetupStateCCP(),
    New ...,
    New ExampleCCP(),       ' your CCP
    ' Other CCPs...
}
```

- For PTs you'll have to add it to the `PreparationTaskHelper` module:

```vb.net
Private PreparationTaskModules As New Dictionary(Of String, PreparationTask) From {
    {"Stop Windows Explorer", New ExplorerStopperPT()},
    {"Delete Shadow Volumes", New VssAdminShadowVolumeDeletePT()},
    {"ExampleTask", New ExamplePT()},   ' your PT
    ' Other PTs...
}
```

In this dictionary, the key is the name of the task as it will appear in the UI.

For more information, please refer to the XML comments in the classes. Also make sure that your CCPs and PTs have XML comments for easier documentation.