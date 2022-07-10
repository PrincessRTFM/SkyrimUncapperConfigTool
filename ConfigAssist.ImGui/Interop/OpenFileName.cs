namespace PrincessRTFM.SSEUncapConfig.Interop;

// Copyright Microsoft Corporation
// All rights reserved

// OpenFileDlg.cs

using System;
using System.Runtime.InteropServices;

/*
typedef struct tagOFN { 
  DWORD         lStructSize; 
  HWND          hwndOwner; 
  HINSTANCE     hInstance; 
  LPCTSTR       lpstrFilter; 
  LPTSTR        lpstrCustomFilter; 
  DWORD         nMaxCustFilter; 
  DWORD         nFilterIndex; 
  LPTSTR        lpstrFile; 
  DWORD         nMaxFile; 
  LPTSTR        lpstrFileTitle; 
  DWORD         nMaxFileTitle; 
  LPCTSTR       lpstrInitialDir; 
  LPCTSTR       lpstrTitle; 
  DWORD         Flags; 
  WORD          nFileOffset; 
  WORD          nFileExtension; 
  LPCTSTR       lpstrDefExt; 
  LPARAM        lCustData; 
  LPOFNHOOKPROC lpfnHook; 
  LPCTSTR       lpTemplateName; 
#if (_WIN32_WINNT >= 0x0500)
  void *        pvReserved;
  DWORD         dwReserved;
  DWORD         FlagsEx;
#endif // (_WIN32_WINNT >= 0x0500)
} OPENFILENAME, *LPOPENFILENAME; 
*/

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName {
	public int StructSize = 0;
	public IntPtr DlgOwner = IntPtr.Zero;
	public IntPtr Instance = IntPtr.Zero;

	public string Filter = null!;
	public string CustomFilter = null!;
	public int MaxCustFilter = 0;
	public int FilterIndex = 0;

	public string File = null!;
	public int MaxFile = 0;

	public string FileTitle = null!;
	public int MaxFileTitle = 0;

	public string? InitialDir = null;

	public string Title = null!;

	public int Flags = 0;
	public short FileOffset = 0;
	public short FileExtension = 0;

	public string DefExt = null!;

	public IntPtr CustData = IntPtr.Zero;
	public IntPtr Hook = IntPtr.Zero;

	public string TemplateName = null!;

	public IntPtr ReservedPtr = IntPtr.Zero;
	public int ReservedInt = 0;
	public int FlagsEx = 0;
}
