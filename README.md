# Municipal Services Application — Parts 1, 2 and 3

A C# .NET Framework 4.8 Windows Forms application for the Municipal Services
PoE. It covers all three modules, plus login, registration and a staff/admin
dashboard:

- **Part 1 – Report Issues.** Every report now becomes a trackable service
  request with a ticket number, priority and status.
- **Part 2 – Local Events and Announcements.** Search and recommendations.
- **Part 3 – Service Request Status.** Citizen tracking screen and the
  staff/admin management dashboard, built on trees, a heap and a graph.

There is **no database** anywhere. All data lives in in-memory collections and
hand-written data structures and is reset each time the application closes.
Demo data is seeded on start-up.

## Requirements

- Windows 10/11
- Visual Studio 2019 or 2022 (Community is fine) with the
  **.NET desktop development** workload
- .NET Framework 4.8 Developer Pack (Visual Studio prompts for it if missing)

## How to compile and run

1. Copy/extract the `MunicipalServicesApp` folder.
2. Open **`MunicipalServicesApp.sln`** in Visual Studio.
3. Build with **Build → Build Solution** (Ctrl+Shift+B), then run with **F5**.
4. The executable is produced at `MunicipalServicesApp\bin\Debug\MunicipalServicesApp.exe`.

Command-line alternative (Developer Command Prompt):

```
msbuild MunicipalServicesApp.sln /p:Configuration=Debug
```

## Demo accounts (seeded)

| Role    | Username  | Password     |
|---------|-----------|--------------|
| Citizen | `citizen` | `Citizen@123` |
| Staff   | `staff`   | `Staff@123`   |
| Admin   | `admin`   | `Admin@123`   |

The demo citizen already owns eight demo service requests in different states
(some with staff responses), so every screen has something to show straight away.

## How to use the application

### Logging in and registering

- Enter a username and password on the login screen and press **Log In**.
- **Citizens** can create their own account with **New citizen? Create an
  account** (full name, email, username of 4+ characters, password of 6+
  characters). They are signed in straight after registering.
- **Staff and admin accounts are not self-registered.** The seeded demo
  accounts exist for marking, and a logged-in **Admin** can create further
  staff or admin accounts from the dashboard (**Create Staff Account**).
- **Log Out** on the main menu returns to the login screen.

### Main menu

| Button | Citizen | Staff / Admin |
|---|---|---|
| Report Issues | Opens the reporting screen | Disabled (citizen action) |
| Local Events and Announcements | Opens events | Opens events |
| Service Request Status | "My Service Requests" | "Manage Service Requests" dashboard |

### Report Issues (Part 1)

1. Choose a **Location** (ward) and **Category**, write a **Description**, and
   optionally attach files.
2. The progress bar and message update as each section is completed.
3. **Submit** gives you a ticket number (for example `SR-01009`) and the
   priority it was assigned. Track it under Service Request Status.

### Local Events and Announcements (Part 2)

Filter by category, date and keyword; double-click an event for its details.
The **Recommended for You** panel suggests upcoming events from the categories
you search most, skipping events you have already viewed.

### Service Request Status (Part 3) – citizen

- Lists only the signed-in citizen's own requests with date, category,
  location and status.
- **Find by ticket number** accepts `1001` or `SR-01001` (binary search tree
  lookup). A ticket that does not belong to you gives the same "not found"
  message as one that does not exist.
- Selecting a request shows its full description and any reply from the
  municipality.

### Service Request Status (Part 3) – staff and admin dashboard

**All Requests tab**
- Sort by **Newest first** (AVL tree), **Highest priority first**
  (Red-Black tree) or **Ticket number** (binary search tree), and filter by status.
- Select a request, choose a new **status**, optionally type a **response to
  the citizen**, and press **Save Update**. The citizen sees both the next time
  they open their requests.

**Urgent Queue and Categories tab**
- Top 8 most urgent open requests from the max-heap.
- Department/category hierarchy (binary tree, pre-order) with request counts.

**Ward Network and Routes tab**
- Pick a starting ward, then use **Breadth-first traversal**, **Depth-first
  traversal**, **Nearby open requests (within 2 hops)** or **Optimal service
  route (minimum spanning tree)**.

**Admins only:** **Create Staff Account** in the header.

## Data structures: role and contribution to efficiency

Every key (ticket number, date reported, priority) is fixed when a request is
created, and staff can only change status and response. Because of that, none
of the indexes below ever needs a delete or re-key.

| Structure | File | Keyed by | Used for |
|---|---|---|---|
| Binary tree | `Models/CategoryTree.cs` | none (fixed hierarchy) | Department/category breakdown via pre-order and in-order traversal |
| Binary search tree | `Models/BinarySearchTree.cs` | ticket number | Citizen ticket lookup, ticket-order sort |
| AVL tree | `Models/AVLTree.cs` | date reported | Newest-first list; rotations keep the height near log₂ n |
| Red-Black tree | `Models/RedBlackTree.cs` | priority × 10,000,000 + ticket | Highest-priority-first list; height ≤ 2·log₂(n+1) |
| Max-heap | `Models/RequestUrgencyHeap.cs` | priority first, age breaks ties | Top-N urgent open requests; O(1) peek, O(log n) extract |
| Graph + BFS/DFS | `Models/LocationGraph.cs` | ward name | Traversal and "nearby open requests" |
| Minimum spanning tree | `Models/LocationGraph.cs` | road distance (km) | Cheapest links connecting wards that have open requests |
| List, Dictionary, HashSet, Queue, SortedSet | various | – | Storage, category counts, distinct wards, BFS queue, Dijkstra frontier |

**Examples with the seeded data**

- *AVL:* 1,023 keys inserted in ascending order produce a tree of height 10
  (an unbalanced BST would be 1,023 deep).
- *Heap:* the top three urgent open requests are `SR-01001`, `SR-01005` and
  `SR-01008` (all Critical); the resolved waste ticket never appears.
- *Graph:* breadth-first from "Ward 1 - Central CBD" visits Sunnyside first
  (3 km), then Riverside, then Hillview; depth-first dives through Sunnyside to
  Hillview to Oakdale before backing up.
- *MST:* the seven wards with open demo requests are connected by six road
  links totalling 28 km. Distances between non-adjacent required wards are
  shortest-path distances (Dijkstra), then Prim's algorithm picks the cheapest links.

**Honest notes on the design.** The plain BST gets sequential ticket numbers,
which is its worst case (a right-leaning chain), so the AVL and Red-Black trees
carry the guarantee that matters. The MST routine favours clarity over speed;
it is more than adequate for eight wards. See the Task 3 report for details.

## Project structure

```
MunicipalServicesApp/
├── MunicipalServicesApp.sln
├── README.md
└── MunicipalServicesApp/
    ├── Program.cs                       Login loop and entry point
    ├── Theme.cs                         Shared colours and fonts
    ├── LoginForm / RegisterForm / CreateStaffAccountForm
    ├── MainForm                         Role-aware main menu
    ├── ReportIssueForm                  Part 1
    ├── LocalEventsForm                  Part 2
    ├── ServiceRequestStatusForm         Part 3 – citizen view
    ├── AdminDashboardForm               Part 3 – staff/admin view
    └── Models/
        ├── User, UserRole, PasswordHelper, UserRepository
        ├── Issue, IssueRepository, RequestStatus, RequestPriority
        ├── CategoryTree, BinarySearchTree, AVLTree, RedBlackTree
        ├── RequestUrgencyHeap, LocationGraph
        └── Event, EventPriorityQueue, EventRepository
```

## Security notes

Passwords are never stored in plain text: each account has a random 16-byte
salt and a SHA-256 hash. Login failures give one generic message. Staff and
admin accounts can only be created by an admin. This is suitable for a
coursework demo; a production system would use a slow password hash (for
example PBKDF2) and persistent storage.

## Design considerations addressed

- **Consistency:** shared `Theme` colours and fonts on every screen.
- **Responsiveness:** forms are resizable and controls are anchored/docked.
- **Feedback:** completeness indicator, clear validation messages, confirmation
  dialogs showing ticket numbers.

## Notes

- Build verification here used Mono's `mcs` compiler and a headless runtime;
  please build once in Visual Studio to confirm on Windows before submitting.
- Data is not saved between runs by design (no database is used).
