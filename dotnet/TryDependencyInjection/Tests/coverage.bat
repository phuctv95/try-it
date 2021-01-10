set OpenCoverVersion=4.7.922
set ReportRegenratorVersion=4.8.4
set OpenCoverResultFile=opencover-result.xml
set ReportFolder=CoverageReport

%UserProfile%\.nuget\packages\opencover\%OpenCoverVersion%\tools\OpenCover.Console.exe ^
    -target:"C:\Program Files\dotnet\dotnet.exe" ^
    -targetargs:"test" ^
    -register:user ^
    -output:%OpenCoverResultFile%

dotnet "C:\Users\puyed\.nuget\packages\reportgenerator\%ReportRegenratorVersion%\tools\netcoreapp3.0\ReportGenerator.dll" ^
    -reports:%OpenCoverResultFile% ^
    -targetdir:%ReportFolder% ^
    "-assemblyfilters:-NUnit3.TestAdapter;-Tests" ^
    "-classfilters:-System.Runtime.*;-Microsoft.CodeAnalysis.*"

%ReportFolder%\index.html
