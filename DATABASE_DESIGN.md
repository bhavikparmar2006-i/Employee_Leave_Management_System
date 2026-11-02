# Database Design

## Entities
- `AspNetUsers` and related Identity tables: authentication accounts, password hashes and roles.
- `Employees`: employee profile with unique email; optional one-to-one link to an Identity user.
- `LeaveRequests`: leave details linked to an employee.

## Relationships
- One Identity user can be linked to zero or one employee profile.
- One employee can have many leave requests.
- Employee deletion is restricted when leave history exists.

```mermaid
erDiagram
  APPLICATION_USER ||--o| EMPLOYEE : "linked by UserId"
  EMPLOYEE ||--o{ LEAVE_REQUEST : submits
  EMPLOYEE {
    int Id PK
    string FullName
    string Email UK
    string Department
    string Designation
    date JoiningDate
    string UserId FK
  }
  LEAVE_REQUEST {
    int Id PK
    int EmployeeId FK
    string LeaveType
    date StartDate
    date EndDate
    string Reason
    string Status
    datetime AppliedOn
    string AdminComment
  }
```
