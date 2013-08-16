using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DOWILL CopyCat Project Core Library")]
#if (DEBUG)
[assembly: AssemblyDescription("BitTorrent Utility(DEBUG)")]
#else
[assembly: AssemblyDescription("BitTorrent Utility(RELEASE)")]
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dowill Studio")]
[assembly: AssemblyProduct("DOWILL CopyCat Project")]
[assembly: AssemblyCopyright("Copyright ©  2009")]
[assembly: AssemblyTrademark("DOWILL Studio")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("91ae73ae-638f-49b2-b9dd-f00f1691bd85")]

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
#if(DEBUG)
[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyFileVersion("0.2009.1213.0")]
#else
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.2009.1213.0")]
#endif
