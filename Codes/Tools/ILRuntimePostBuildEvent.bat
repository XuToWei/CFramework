echo off
set currentDir=%~dp0
set targetDir=%1
set hotfixDllDir=%currentDir%..\..\Assets\Res\HotfixDlls\
if not exist %hotfixDllDir% ( 
	md %hotfixDllDir% 
)
copy %targetDir%Hotfix.dll %hotfixDllDir%Hotfix.dll.bytes
copy %targetDir%Hotfix.pdb %hotfixDllDir%Hotfix.pdb.bytes