# API Documentation

Base URL: `https://localhost:<port>/api`

Swagger UI is enabled in Development at `/swagger`.

All endpoints require authentication. Employee endpoints are restricted to Admin. Leave endpoints return only the current employee's requests unless the caller is Admin.

| Method | Endpoint | Purpose | Access |
|---|---|---|---|
| GET | `/api/employees` | List employee fields | Admin |
| GET | `/api/employees/{id}` | Get employee | Admin |
| GET | `/api/leaves` | List leave requests visible to caller | Authenticated |
| GET | `/api/leaves/{id}` | Get one visible leave request | Authenticated, ownership checked |
| POST | `/api/leaves` | Create leave request | Employee/Admin with linked employee profile |
| POST | `/api/leaves/{id}/decision` | Approve/reject pending request | Admin |

Example create request JSON:
```json
{"leaveType":"Casual","startDate":"2026-11-10T00:00:00","endDate":"2026-11-11T00:00:00","reason":"Personal appointment"}
```

Example decision JSON:
```json
{"approve":true,"comment":"Approved"}
```

The API uses the application's configured authentication. Do not expose endpoints publicly without configuring and testing an appropriate authentication scheme and HTTPS.
