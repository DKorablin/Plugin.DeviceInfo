using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Guid("654eafd4-7650-4a6f-a9e4-b38b53c0fbe2")]
[assembly: System.CLSCompliant(true)]

#if NETCOREAPP
[assembly: AssemblyMetadata("ProjectUrl", "https://dkorablin.ru/project/Default.aspx?File=97")]
#else

[assembly: AssemblyDescription("HDD device info and System Management BIOS info")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2013-2024")]
#endif

/*if $(ConfigurationName) == Release (
..\..\..\..\ILMerge.exe  "/out:$(ProjectDir)..\bin\Plugin.DeviceInfo.dll" "$(TargetDir)Plugin.DeviceInfo.dll" "$(TargetDir)DeviceIoControl.dll" "/lib:..\..\..\SAL\bin"
)*/