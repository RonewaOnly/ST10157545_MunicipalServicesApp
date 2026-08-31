# ST10157545_MunicipalServicesApp
# Municipal Services Application — Part 1 (Report Issues)

A C# .NET Framework (v4.8) Windows Forms application built for the Municipal
Services PoE. This first submission implements the **Report Issues** module
only; **Local Events and Announcements** and **Service Request Status** are
present on the main menu but disabled, ready to be built out in Part 2.

## Requirements

- Windows 10/11
- Visual Studio 2019 or 2022 (Community edition is fine), with the
  **.NET desktop development** workload installed
- .NET Framework 4.8 Developer Pack (Visual Studio will prompt you to install
  this automatically if it is missing)

## How to compile and run

1. Extract/copy the `MunicipalServicesApp` folder to your machine.
2. Open **`MunicipalServicesApp.sln`** in Visual Studio.
3. Let Visual Studio restore/target .NET Framework 4.8 (it will prompt if the
   targeting pack isn't installed — accept the prompt to install it).
4. Set the build configuration to **Debug** (default) and press **F5**, or
   use **Build → Build Solution** (Ctrl+Shift+B) followed by **Debug → Start
   Without Debugging** (Ctrl+F5).
5. The compiled executable will be produced at:
   `MunicipalServicesApp\bin\Debug\MunicipalServicesApp.exe`

You can also build it purely with MSBuild from a Developer Command Prompt:

```
msbuild MunicipalServicesApp.sln /p:Configuration=Debug
```

## How to use the application

1. On startup, the **Main Menu** appears with three options:
   - **Report Issues** — enabled and fully functional.
   - **Local Events and Announcements** — disabled (to be implemented later).
   - **Service Request Status** — disabled (to be implemented later).
2. Click **Report Issues** to open the reporting screen.
3. Fill in:
   - **Location** — free text describing where the issue is.
   - **Category** — select from the dropdown (Sanitation, Roads and
     Potholes, Water and Utilities, Electricity, Waste Management, Parks and
     Public Spaces, Other).
   - **Description** — details of the issue, typed into the rich text box.
   - **Attach Image / Document** (optional) — opens a file dialog where you
     can select one or more images/documents to attach.
4. As you complete each section, the **progress bar and message** near the
   bottom of the form update in real time — this is the user engagement
   feature described and justified in the Task 1 research document.
5. Click **Submit Report**. If Location, Category or Description are
   missing, a message box lists exactly what still needs to be completed.
   Once submitted successfully, you'll see a confirmation with a reference
   number, and the form resets so you can report another issue.
6. Click **Back to Main Menu** at any time to return to the main menu.

## Project structure

```
MunicipalServicesApp/
├── MunicipalServicesApp.sln
└── MunicipalServicesApp/
    ├── Program.cs                     Application entry point
    ├── MainForm.cs / .Designer.cs     Main menu (3 task options)
    ├── ReportIssueForm.cs / .Designer.cs   Report Issues screen + engagement logic
    ├── Models/
    │   ├── Issue.cs                   Data model for a single reported issue
    │   └── IssueRepository.cs         List<Issue> data structure used to store reports
    ├── Properties/AssemblyInfo.cs
    ├── App.config
    └── MunicipalServicesApp.csproj
```

## Data structure

Reported issues are stored in-memory for the duration of the session in a
`List<Issue>`, held inside the static `IssueRepository` class. Each `Issue`
object stores the location, category, description, a list of attachment file
paths, a unique ID, and the date/time it was reported. This structure was
chosen so that Part 2 of the PoE (Local Events and Service Request Status)
can read from the same collection without extra wiring between forms.

## Notes

- This solution was written against .NET Framework 4.8 and was syntax- and
  compile-checked using the Mono C# compiler (`mcs`) against the
  `System.Windows.Forms` and `System.Drawing` assemblies before submission.
  Build it in Visual Studio for full designer support.
- A copy of this source code should be kept safe, as per the assignment
  brief, since it will be built upon in Part 2 and the final PoE.