#region

using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("StartupCreator")]
[assembly: AssemblyDescription("An application to create Windows start up script files effortlessly.")]
#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else

[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("Soenneker LLC")]
[assembly: AssemblyProduct("StartupCreator")]
[assembly: AssemblyCopyright("Copyright © Soenneker LLC 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("8edccc6f-5af5-4120-ac23-406267a6adef")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("1.1.*")]
[assembly: NeutralResourcesLanguage("en-US")]