#!/bin/sh
cp DebugMessage.Linux/bin/Debug/netstandard2.0/DebugMessage.Core.* DebugMessage/bin/Debug/netcoreapp2.2/
dotnet DebugMessage/bin/Debug/netcoreapp2.2/DebugMessage.dll "Hello P/Invoke!"

