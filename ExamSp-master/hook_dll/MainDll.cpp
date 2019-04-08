#include <Windows.h>

BOOL APIENTRY DllMain(HMODULE  hModule,
  DWORD    ul_reason_for_call,
  LPVOID   lpReserved)
{
  TCHAR * szMsgText = NULL;
  TCHAR * szMsgCaption = TEXT("DLL Load Information");

  switch (ul_reason_for_call)
  {
  case DLL_PROCESS_ATTACH:
    szMsgText = TEXT("DLL loaded and called with flag DLL_PROCESS_ATTACH");
    break;

  case DLL_THREAD_ATTACH:
    szMsgText = TEXT("DLL loaded and called with flag DLL_THREAD_ATTACH");
    break;

  case DLL_THREAD_DETACH:
    szMsgText = TEXT("DLL called with flag DLL_THREAD_DETACH");
    break;

  case DLL_PROCESS_DETACH:
    szMsgText = TEXT("DLL unloading and called with flag DLL_PROCESS_DETACH");
    break;
  }

  //MessageBox(NULL,  szMsgText, szMsgCaption, MB_OK|MB_ICONINFORMATION);

  return TRUE;
}
