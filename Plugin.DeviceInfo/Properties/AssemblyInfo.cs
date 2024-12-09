using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("654eafd4-7650-4a6f-a9e4-b38b53c0fbe2")]
[assembly: System.CLSCompliant(true)]

#if NETCOREAPP
[assembly: AssemblyMetadata("ProjectUrl", "https://dkorablin.ru/project/Default.aspx?File=97")]
#else

[assembly: AssemblyTitle("Plugin.DeviceInfo")]
[assembly: AssemblyDescription("HDD device info and System Management BIOS info")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Danila Korablin")]
[assembly: AssemblyProduct("Plugin.DeviceInfo")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2013-2024")]
#endif

/*if $(ConfigurationName) == Release (
..\..\..\..\ILMerge.exe  "/out:$(ProjectDir)..\bin\Plugin.DeviceInfo.dll" "$(TargetDir)Plugin.DeviceInfo.dll" "$(TargetDir)DeviceIoControl.dll" "/lib:..\..\..\SAL\bin"
)*/