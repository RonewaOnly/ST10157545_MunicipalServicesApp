# Municipal Services Application — Part 1 & Part 2

A C# .NET Framework (v4.8) Windows Forms application built for the Municipal
Services PoE. This submission implements:

- **Part 1 — Report Issues** (module complete)
- **Part 2 — Local Events and Announcements** (module complete)

**Service Request Status** is left for a later part of the PoE, so its button
on the main menu is still disabled.

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

### Main Menu

On startup, the **Main Menu** appears with three options:
- **Report Issues** — enabled (Part 1).
- **Local Events and Announcements** — enabled (Part 2).
- **Service Request Status** — disabled (to be implemented in a later part).

### Report Issues (Part 1)

1. Click **Report Issues** to open the reporting screen.
2. Fill in **Location**, **Category**, **Description**, and optionally
   **Attach Image / Document** (opens a file dialog, multiple files allowed).
3. As you complete each section, the **progress bar and message** update in
   real time — the user engagement feature justified in the Task 1 research
   document.
4. Click **Submit**. Missing required fields are listed in a message box;
   a successful submission shows a confirmation with a reference number and
   resets the form.
5. Click **Back to Main Menu** to return.

### Local Events and Announcements (Part 2)

1. Click **Local Events and Announcements** to open the events screen, which
   opens already showing all upcoming events, soonest first.
2. **Search** using any combination of:
   - **Category** dropdown (built from the categories actually in use).
   - **Filter by date** checkbox + date picker, for a specific day.
   - **Keyword**, matched against each event's title and description.
3. Click **Search** to apply the filters, or **Clear Filters** to go back to
   the full upcoming list.
4. **Double-click** any event in the list to view its full description and
   location in a pop-up; this also adds it to the **Recently Viewed** panel.
5. The **Recommended for You** panel updates after every category search,
   suggesting other events in whichever category you've searched most, that
   you haven't already viewed.
6. Click **Back to Main Menu** to return.

Both the Report Issues and Local Events windows are resizable (and can be
maximised) so the layout adapts to different screen sizes and resolutions,
per the brief's Responsiveness requirement.

## Project structure

```
MunicipalServicesApp/
├── MunicipalServicesApp.sln
└── MunicipalServicesApp/
    ├── Program.cs                          Application entry point
    ├── Theme.cs                            Shared colours/fonts for a consistent look
    ├── MainForm.cs / .Designer.cs          Main menu (3 task options)
    ├── ReportIssueForm.cs / .Designer.cs   Report Issues screen + engagement logic
    ├── LocalEventsForm.cs / .Designer.cs   Local Events search, display & recommendations
    ├── Models/
    │   ├── Issue.cs                        Data model for a single reported issue
    │   ├── IssueRepository.cs              List<Issue> data structure used to store reports
    │   ├── Event.cs                        Data model for a single local event
    │   ├── EventPriorityQueue.cs           Min-heap priority queue, ordered by event date
    │   └── EventRepository.cs              Event data structures, search and recommendations
    ├── Properties/AssemblyInfo.cs
    ├── App.config
    └── MunicipalServicesApp.csproj
```

## Data structures

**Part 1 — Report Issues:** reported issues are stored in-memory in a
`List<Issue>`, held inside the static `IssueRepository` class. Each `Issue`
stores the location, category, description, attachment paths, a unique ID,
and the date/time reported.

**Part 2 — Local Events:** `EventRepository` seeds ~15 realistic sample
municipal events (markets, council meetings, clinic dates, workshops, sports
fixtures) and indexes them with a deliberate data structure per job, rather
than one generic list doing everything:

| Structure | Used for |
|---|---|
| `EventPriorityQueue` (a hand-written min-heap, keyed by date) | Retrieving events in "soonest first" order without re-sorting the whole collection on every lookup — .NET Framework 4.8 has no built-in `PriorityQueue<T>`, so a small one is implemented in `EventPriorityQueue.cs` |
| `SortedDictionary<DateTime, List<Event>>` | Indexing events by date for fast date-based search |
| `Dictionary<string, List<Event>>` (hash table) | Indexing events by category for fast category-based search |
| `HashSet<string>` / `HashSet<DateTime>` | Tracking the unique categories and unique dates in use, so the search filters offer exactly what exists, with no duplicates |
| `Stack<Event>` | Recording recently viewed events (most-recent-first), shown in the "Recently Viewed" panel |
| `Dictionary<string, int>` | Tallying how often each category has been searched — this is what drives the recommendation feature |

### Recommendation feature

Every category search increments that category's count in a frequency
dictionary. The **Recommended for You** panel then takes the
highest-count category, excludes anything already in the "recently viewed"
stack, and lists the soonest remaining events in that category. This is a
simple, explainable content-based recommendation rather than a black box —
it genuinely reflects what the user has been searching for, using the
frequency dictionary and the viewed-events stack together.

## Design considerations addressed

- **Consistency** — a single `Theme` class defines the colour palette (a
  municipal blue/green scheme) and font used across all three forms.
- **Clarity** — required fields are marked with `*`; search results state
  how many events matched, or that none did.
- **User feedback** — `MessageBox` dialogs confirm validation errors and
  successful submissions; the events list gives an immediate results count.
- **Responsiveness** — all forms are resizable (not fixed-size), with
  controls anchored so the layout reflows to fit different window sizes.

## Notes

- This solution was written against .NET Framework 4.8 and was compile- and
  smoke-tested with the Mono C# compiler (`mcs`) and Mono runtime against the
  `System.Windows.Forms` and `System.Drawing` assemblies before submission —
  both the full build and a headless run of the compiled executable
  completed without errors. Build it in Visual Studio for full designer
  support.
- A copy of this source code should be kept safe, as per the assignment
  brief, since it will be built upon further in the final PoE.
