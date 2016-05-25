@ECHO OFF

REM
REM Set variables
REM
SET ScriptPath=%~dp0
REM SET Cultures=zh-CHS
SET Cultures=zh-CHS,fa-FA,up-FL
REM SET Auto=
SET Auto=/auto-translate

REM
REM Run the Localization helper for all the languages we translate.
REM
PUSHD "%ScriptPath%..\Tools\LocalizationHelper\"
LocalizationHelper.exe /cultures:%Cultures% /solution:"%ScriptPath%.." /destination:"%ScriptPath%bin/" /ignore:Tools %Auto%
POPD

REM PAUSE
