@echo off
pushd %~dp0

SET MsBuildPath=C:\Windows\Microsoft.NET\Framework64\v4.0.30319


if exist bin goto build
mkdir bin

:Build
if "%1" == "" goto BuildDefaults

%MsBuildPath%\msbuild.exe CfCommerce.msbuild /m /nr:false /t:%* /p:Platform="Any CPU" /v:M /fl /flp:LogFile=bin\msbuild.log;Verbosity=Normal
if errorlevel 1 goto BuildFail
goto BuildSuccess

:BuildDefaults
%MsBuildPath%\msbuild.exe CfCommerce.msbuild /m /nr:false /p:Platform="Any CPU" /v:M /fl /flp:LogFile=bin\msbuild.log;Verbosity=Normal
if errorlevel 1 goto BuildFail
goto BuildSuccess

:BuildFail
echo.
echo *** BUILD FAILED ***
goto End

:BuildSuccess
echo.
echo **** BUILD SUCCESSFUL ***
goto end

:End
popd