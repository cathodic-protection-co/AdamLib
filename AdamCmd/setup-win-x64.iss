; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "AdamCmd"
#define MyAppVersion "1.0"
#define MyAppPublisher "Cathodic Protection Co Limited"
#define MyAppURL "http://www.cathodic.co.uk/"
#define MyAppExeName "AdamCmd.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{746D444F-48B1-479B-8096-4643B7A22ABE}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\Cathodic\AdamCmd
DefaultGroupName=Adam Command Line
OutputDir=C:\Users\jordan.hemming.LAN\source\repos\AdamLib\AdamCmd\bin\Setup
OutputBaseFilename=adamcmd-setup-win-x64
Compression=lzma
SolidCompression=yes
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
ChangesEnvironment=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "C:\Users\jordan.hemming.LAN\source\repos\AdamLib\AdamCmd\bin\Release\netcoreapp2.1\publish\win-x64\AdamCmd.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\jordan.hemming.LAN\source\repos\AdamLib\AdamCmd\bin\Release\netcoreapp2.1\publish\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKCU; Subkey: "Environment"; ValueType:expandsz; ValueName: "PATH"; ValueData: "{olddata};{app}"; Flags: preservestringtype

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

