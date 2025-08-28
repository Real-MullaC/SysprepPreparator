Namespace Helpers

    ''' <summary>
    ''' This interface defines methods to work with the Windows Registry
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IRegistryRunner

        ''' <summary>
        ''' Runs a REG process
        ''' </summary>
        ''' <param name="CommandLine">The command line arguments to pass to the REG program</param>
        ''' <returns>The exit code of REG process</returns>
        ''' <remarks></remarks>
        Function RunRegProcess(CommandLine As String) As Integer

        ''' <summary>
        ''' Gets an appropriate representation of registry value types for REG commands
        ''' </summary>
        ''' <param name="ValueType">The registry value type</param>
        ''' <returns>The representation for REG commands</returns>
        ''' <remarks></remarks>
        Function GetRegValueTypeFromEnum(ValueType As RegistryItem.ValueType) As String

        ''' <summary>
        ''' Adds a registry item to the system
        ''' </summary>
        ''' <param name="RegItem">The new registry item</param>
        ''' <returns>The exit code of the underlying REG process call</returns>
        ''' <remarks></remarks>
        Function AddRegistryItem(RegItem As RegistryItem) As Integer

        ''' <summary>
        ''' Removes a registry item from the system
        ''' </summary>
        ''' <param name="RegPath">The absolute path to the item (key or value)</param>
        ''' <param name="DeletionArgs">Deletion arguments to pass to REG</param>
        ''' <returns>The exit code of the underlying REG process call</returns>
        ''' <remarks></remarks>
        Function RemoveRegistryItem(RegPath As String, DeletionArgs As String) As Integer

        ''' <summary>
        ''' Loads a registry hive to the system
        ''' </summary>
        ''' <param name="RegHivePath">The path of the registry hive</param>
        ''' <param name="RegMountPath">The path to mount the registry hive to</param>
        ''' <returns>The exit code of the underlying REG process call</returns>
        ''' <remarks></remarks>
        Function LoadRegistryHive(RegHivePath As String, RegMountPath As String) As Integer

        ''' <summary>
        ''' Unloads a registry hive from the system
        ''' </summary>
        ''' <param name="RegMountPath">The path of the mounted hive to unload</param>
        ''' <returns>The exit code of the underlying REG process call</returns>
        ''' <remarks></remarks>
        Function UnloadRegistryHive(RegMountPath As String) As Integer

    End Interface

End Namespace