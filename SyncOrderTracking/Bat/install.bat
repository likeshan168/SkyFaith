%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe WindowsServiceTest.exe
Net Start TrackingService
sc config TrackingService start= auto