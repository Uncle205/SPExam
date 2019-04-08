//#include "Header.h"
#include <Windows.h>
#include <stdlib.h>
#include <sstream>
#include <string>
#include <tchar.h>

using namespace std;

#ifdef UNICODE
#define TSTRING wstring
#define TOSTRING wostringstream
#else
#define TSTRING string
#define TOSTRING ostringstream
#endif // UNICODE

static HHOOK    hHookId = 0;
static HWND     hMainWnd = 0;
static int      idMainWndList = 0;
static DWORD    dwHookThreadId = 0;
static HMODULE  hHookDll = 0;
static int      HookMdgId = 0;


void AddItem(LPCTSTR text)
{
  SendDlgItemMessage(hMainWnd, idMainWndList, LB_ADDSTRING, 0, (LPARAM)text);
  int cnt = SendDlgItemMessage(hMainWnd, idMainWndList, LB_GETCOUNT, 0, 0);
  if (cnt == LB_ERR) cnt = 1;
  SendDlgItemMessage(hMainWnd, idMainWndList, LB_SETCURSEL, cnt-1, 0);
}


void AddHWND(WPARAM hwnd)
{
  TOSTRING oss;
  oss << hex << (int)hwnd;
  TSTRING Message = TEXT(" ---Window Handle: 0x") + oss.str();
  AddItem(Message.c_str());
}


void AddRECT(RECT rect)
{
  TOSTRING oss;
  oss << TEXT("(") << rect.top << TEXT(",") << rect.left << TEXT(")-(") <<
    rect.bottom << TEXT(",") << rect.right << TEXT(")");
  TSTRING Message = TEXT("Window Moved/Resized: ") + oss.str();
  AddItem(Message.c_str());
}


static LRESULT CALLBACK cbtHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
  if (nCode >= 0)
  {
    CBT_CREATEWND     * cbtHookCreate;
    CBTACTIVATESTRUCT * cbtAtivate;
    RECT              * pcbtRect;
    RECT                cbtRect;
    TSTRING             Message;

    switch (nCode)
    {
      //-----------------------------------------------------------
    case HCBT_ACTIVATE:
      cbtAtivate = (CBTACTIVATESTRUCT*)lParam;
      AddItem(TEXT("Window Activated"));
      break;

      //-----------------------------------------------------------
    case HCBT_CREATEWND:
      cbtHookCreate = (CBT_CREATEWND*)lParam;
      AddItem(TEXT("Window Created"));

      Message = TEXT(" ---Window Name: ");
      if (!IsBadReadPtr(cbtHookCreate->lpcs, 1))
      {
        if (!IsBadReadPtr(cbtHookCreate->lpcs->lpszName, 1))
        {
          TCHAR tt = cbtHookCreate->lpcs->lpszName[0];
          if (tt != TCHAR(-1))
            Message = Message + cbtHookCreate->lpcs->lpszName;
          else
          {
            TCHAR tbuf[32];
            unsigned long lw = *((unsigned long*)cbtHookCreate->lpcs->lpszName);
            _ultot_s(lw, tbuf, sizeof(tbuf) / sizeof(tbuf[0]), 10);
            Message = Message + tbuf;
            _ultot_s(lw, tbuf, sizeof(tbuf) / sizeof(tbuf[0]), 16);
            Message = Message + TEXT(" (0x") + tbuf + TEXT(")");;
          }
        }
      }
      AddItem(Message.c_str());

      Message = TEXT(" ---Window Class: ");
      if (!IsBadReadPtr(cbtHookCreate->lpcs, 1))
      {
        if (!IsBadReadPtr(cbtHookCreate->lpcs->lpszClass, 1))
          Message = Message + cbtHookCreate->lpcs->lpszClass;
      }
      AddItem(Message.c_str());
      AddHWND(wParam);
      break;

      //-----------------------------------------------------------
    case HCBT_DESTROYWND:
      AddItem(TEXT("Window Destroyed"));
      break;

      //-----------------------------------------------------------
    case HCBT_MINMAX:
      switch (lParam)
      {
      case SW_HIDE:
        AddItem(TEXT("Window Hidden"));
        break;
      case SW_MAXIMIZE:
        AddItem(TEXT("Window Maximized"));
        break;
      case SW_MINIMIZE:
        AddItem(TEXT("Window Minimized"));
        break;
      case SW_RESTORE:
        AddItem(TEXT("Window Restored"));
        break;
      case SW_SHOW:
        AddItem(TEXT("Window Shown"));
        break;
      case SW_SHOWDEFAULT:
        AddItem(TEXT("Window Shown Default"));
        break;
      case SW_SHOWMINIMIZED:
        AddItem(TEXT("Window Shown Minimized"));
        break;
      case SW_SHOWMINNOACTIVE:
        AddItem(TEXT("Window Shown Minimized (Not Active)"));
        break;
      case SW_SHOWNA:
        AddItem(TEXT("Window Shown (Not Active)"));
        break;
      case SW_SHOWNOACTIVATE:
        AddItem(TEXT("Window Shown (Not Active)"));
        break;
      case SW_SHOWNORMAL:
        AddItem(TEXT("Window Shown"));
        break;
      }
      AddHWND(wParam);
      break;

      //-----------------------------------------------------------
    case HCBT_MOVESIZE:
      pcbtRect = (RECT*)lParam;
      memcpy(&cbtRect, pcbtRect, sizeof(RECT));
      AddRECT(cbtRect);
      break;
    }
  }
  return CallNextHookEx(hHookId, nCode, wParam, lParam);
}


__declspec(dllexport) HHOOK WINAPI SetCBTHook(int, HMODULE, DWORD, HWND, int);


HHOOK WINAPI SetCBTHook(int     idHook,   // WH_CBT
  HMODULE hHookDll,
  DWORD   ThreadID,
  HWND    ThisMainWindow,
  int     idListCtrl)
{
  ::hHookDll = hHookDll;     // GetModuleHandle( TEXT("hook_dll.dll") );
  ::dwHookThreadId = ThreadID;
  ::hMainWnd = ThisMainWindow;
  ::HookMdgId = idHook;
  ::idMainWndList = idListCtrl;

  hHookId = ::SetWindowsHookEx(idHook, cbtHookProc, hHookDll, ThreadID);

  if (hHookId)
  {
    MessageBox(NULL,
      TEXT("Setup CBT-hook procedure SUCCESS"),
      TEXT("Info"),
      MB_OK | MB_ICONINFORMATION);
  }
  else
  {
    MessageBox(NULL,
      TEXT("Setup CBT-hook procedure FAILED"),
      TEXT("Info"),
      MB_OK | MB_ICONERROR);
  }
  return hHookId;
}


__declspec(dllexport) void WINAPI RemoveCBTHook();

void WINAPI RemoveCBTHook()
{
  ::UnhookWindowsHookEx(hHookId);
}
