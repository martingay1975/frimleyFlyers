cls
set pathB4=%cd%
set app=phantomjs.exe runner.js

pushd c:\git\FrimleyFlyers\sitePhantomjs
rem pushd %USERPROFILE%\Google Drive\Work\Code\Web\FrimleyFlyers\sitePhantomjs
rem pushd C:\Users\slop\Google Drive\Work\Code\Web\FrimleyFlyers\sitePhantomjs

%app% ffchampionship
%app% ffchampionship2018
%app% fftrophy2018
%app% ffchampionship2017
%app% pbList
%app% index
%app% endure24

popd

rem cd %pathB4%
