#include <windows.h>

//#pragma comment( lib, "../Debug/hook_dll.lib" )
//__declspec(dllimport) HHOOK WINAPI SetCBTHook( int, HMODULE, DWORD, HWND, int);
//__declspec(dllimport) void WINAPI RemoveCBTHook();


#define IDC_HOOK_LIST 4000

HINSTANCE   mhInstance = 0;
HWND        mhWnd = 0;
HWND        lvhWnd = 0;
HMODULE     hHookDLL = 0;
LPCSTR      pszRemoveCBTHook = R"(?RemoveCBTHook@@YGXXZ)";
LPCSTR      pszSetCBTHook = R"(?SetCBTHook@@YGPAUHHOOK__@@HPAUHINSTANCE__@@KPAUHWND__@@H@Z)";
HHOOK (WINAPI *pfSetCBTHook)(int, HMODULE, DWORD, HWND, int) = nullptr;
//HHOOK(WINAPI *pfSetCBTHook)(int, HMODULE, DWORD, HWND, int) = nullptr;
void (WINAPI *pfRemoveCBTHook)() = nullptr;
HHOOK       hHook = 0;
HHOOK SetCBTHook_dll();

LRESULT CALLBACK WindowProc(HWND hwnd,
  UINT message,
  WPARAM wParam,
  LPARAM lParam)
{
  switch (message)
  {
  case WM_COMMAND:
    break;
  case WM_DESTROY:
    if (pfRemoveCBTHook)
      pfRemoveCBTHook();
    PostQuitMessage(0);
    break;
  default:
    return DefWindowProc(hwnd, message, wParam, lParam);
  }
  return 0;
}


BOOL RegisterClass(TCHAR szClassName[])
{
  WNDCLASSEX wc;

  wc.hInstance = mhInstance;
  wc.lpszClassName = (LPCTSTR)szClassName;
  wc.lpfnWndProc = WindowProc;
  wc.style = CS_DBLCLKS;
  wc.cbSize = sizeof(WNDCLASSEX);
  wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
  wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);
  wc.hCursor = LoadCursor(NULL, IDC_ARROW);
  wc.lpszMenuName = NULL;
  wc.cbClsExtra = 0;
  wc.cbWndExtra = 0;
  wc.hbrBackground = (HBRUSH)COLOR_BACKGROUND;

  if (!RegisterClassEx(&wc))  return 0;
  else                          return 1;

}


int WINAPI WinMain(HINSTANCE hInstance,
  HINSTANCE hPreviousInstance,
  LPSTR lpcmdline,
  int nCmdShow)
{
  MSG messages;
  mhInstance = hInstance;
  TCHAR ClassName[] = TEXT("CBTHookClass");
  TCHAR WindowName[] = TEXT("CBTHookWindow");

  RegisterClass(ClassName);

  // Create main window
  mhWnd = CreateWindowEx(WS_EX_CONTROLPARENT,
    ClassName,
    WindowName,
    WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
    WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SIZEBOX |
    WS_VISIBLE,
    0, 0, 500, 500,
    NULL, NULL,
    hInstance, NULL);

  // Create ListBox control window
  lvhWnd = CreateWindow(TEXT("LISTBOX"),
    TEXT("CBTHookListBox"),
    WS_CHILD | WS_VISIBLE | WS_VSCROLL,
    2, 2, 490, 479,
    mhWnd, (HMENU)IDC_HOOK_LIST,
    hInstance, NULL);

  //SetCBTHook( GetCurrentThreadId(), mhWnd, IDC_HOOK_LIST );
  hHook = SetCBTHook_dll();

  while (GetMessage(&messages, NULL, 0, 0))
  {
    TranslateMessage(&messages);
    DispatchMessage(&messages);
  }
  return 0;
}


HHOOK SetCBTHook_dll()
{
  HHOOK hhook = 0;
  hHookDLL = LoadLibrary(TEXT("hook_dll.dll"));
  if (hHookDLL)
  {
    pfSetCBTHook = (HHOOK(WINAPI*)(int, HMODULE, DWORD, HWND, int))
      GetProcAddress(hHookDLL, pszSetCBTHook);
    pfRemoveCBTHook = (void(WINAPI*)())GetProcAddress(hHookDLL, pszRemoveCBTHook);

    if (pfSetCBTHook) {
      hhook = pfSetCBTHook(WH_CBT, hHookDLL, 0, mhWnd, IDC_HOOK_LIST);
      MessageBox(mhWnd,
        TEXT("Setup CBT-hook procedure SUCCESS"),
        TEXT("Info"),
        MB_OK);
    }
    else
    { // hook_dll.dll load failure
      MessageBox(mhWnd,
        TEXT("Setup CBT-hook procedure FAILED"),
        TEXT("Error"),
        MB_OK | MB_ICONERROR);
    }
  }
  else
  { // hook_dll.dll load failure
    MessageBox(mhWnd,
      TEXT("Can't load library: hook_dll.dll"),
      TEXT("Error"),
      MB_OK | MB_ICONERROR);
  }
  return hhook;
}

