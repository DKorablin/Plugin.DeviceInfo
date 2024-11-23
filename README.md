# Int32 DeviceIoControl function wrapper and SMBIOS information
While analyzing my old C++ projects, I have found source code for project Network Administrator. One of the goals of this application was to transfer S.M.A.R.T. data to client from many other server machines through WinSock. Original author of the wrapper is Andrew I. Reshin, who has written it at 2001 with the source code in C++. At that time API to receive SMART data was undocumented at MSDN and need to be called with message:

#define DFP_RECEIVE_DRIVE_DATA	0x0007c088

For sporting interest I manage to port original C++ code to .NET without unsafe code, because it's not very interesting. Resulting source code is available at GitHub.