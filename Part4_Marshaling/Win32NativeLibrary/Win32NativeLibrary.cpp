// Win32NativeLibrary.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。

#include "stdafx.h"

// 指定されたサイズの数列を生成する
extern "C" __declspec(dllexport) bool __stdcall GenerateData(uint8_t* pBuffer, uint32_t size)
{
    for (uint32_t index = 0; index < size; index++)
    {
        pBuffer[index] = static_cast<uint8_t>(index);
    }
    return true;
}
