# MediSistem

A desktop patient record tracking application built with **C# Windows Forms** on **.NET Framework 4.7.2** and a **MySQL** database. The user interface is in Turkish.

## Features

- **Login** with e-mail and password
  - Passwords are stored as salted **PBKDF2-SHA256** hashes (100,000 iterations)
  - After 5 consecutive failed attempts the login is locked for 30 seconds
- **Patient list** showing TC identity number, name, phone, diagnosis/complaint, doctor, status and last visit date
- **Add** a new patient
- **Search** patients by TC number, name or doctor (filters while typing)
- **Update** the selected patient
- **Delete** the selected patient (with confirmation)
- **Today's examinations**: lists patients whose last visit date is today
- **Print** the patient list
- **PDF export** of the patient list via the Windows "Microsoft Print to PDF" printer

## Security

What the application does:

- **Password hashing:** passwords are stored as salted **PBKDF2-SHA256** hashes (random 16-byte salt per password, 100,000 iterations, constant-time comparison). Plain-text passwords are not stored.
- **Parameterized queries:** all SQL queries that take user input use parameters (`MySqlParameter` / `AddWithValue`); user input is never concatenated into SQL.
- **Login attempt limit:** after 5 consecutive failed logins, the login is locked for 30 seconds. The counter is kept in memory for the running application and resets when the application is restarted.

What it does **not** do:

- **No user roles or authorization:** every logged-in user can view, change and delete all patient records.
- **No encryption of patient data:** patient records are stored as plain text in MySQL.
- **No foreign keys** in the database schema.

> **This is a learning/portfolio project and is not suitable for real patient data.**

## Tech Stack

| | |
|---|---|
| Language / UI | C#, Windows Forms |
| Framework | .NET Framework 4.7.2 |
| Database | MySQL (8.0 or later recommended), `utf8mb4` |
| NuGet packages | `MySql.Data` 9.5.0, `Guna.UI2.WinForms` 2.0.4.7 (+ their dependencies) |

NuGet packages are **not** included in the repository; they are restored by NuGet (see `MediSistem/packages.config`).

## Project Structure

```
MediSistem.sln
database.sql                      MySQL schema + fictitious sample data
MediSistem/
  Program.cs                      Entry point (opens LoginForm)
  LoginForm.cs                    Login, password verification, attempt limit
  MainForm.cs                     Patient list, search, delete, print, PDF
  AddPatientForm.cs               Add patient
  EditPatientForm.cs              Update patient
  MuayeneForm.cs                  Today's examinations
  DbHelper.cs                     Opens the MySQL connection
  PasswordHasher.cs               PBKDF2-SHA256 hashing and verification
  UITheme.cs                      Colors and fonts
  App.config                      Default connection string (WAMP defaults)
  App.local.config.example        Template for local credentials
  Resources/                      Images
docs/screenshots/                 Reserved for screenshots
```

## Getting Started

### Requirements

- Windows
- Visual Studio 2022 with the **.NET desktop development** workload and the **.NET Framework 4.7.2 targeting pack** (Visual Studio Installer → *Individual components*)
- A MySQL server, e.g. from WAMP

### 1. Create the database

Import `database.sql`, for example in phpMyAdmin (*Import* tab) or from the command line:

```
mysql -u root -p < database.sql
```

This creates the `medisistem_db` database with the `users` and `patients` tables and fictitious sample data.

### 2. Configure the connection

The default connection string in `MediSistem/App.config` matches a stock WAMP installation:

```
Server=localhost;Database=medisistem_db;Uid=root;Pwd=;Charset=utf8;
```

If your MySQL user or password is different, do **not** edit `App.config`. Instead:

1. Copy `MediSistem/App.local.config.example` to `MediSistem/App.local.config`
2. Put your own credentials into `App.local.config`

`App.local.config` is git-ignored and copied next to `MediSistem.exe` at build time. Its values override the defaults in `App.config`. If the file does not exist, the defaults are used.

### 3. Restore NuGet packages and run

1. Open `MediSistem.sln` in Visual Studio
2. Right-click the solution, choose **Restore NuGet Packages** (Visual Studio usually does this automatically on build)
3. Press **F5**

### Demo login

| E-mail | Password |
|---|---|
| `doctor@example.com` | `Demo1234!` |

This account is only for trying out the application locally. The application has no password-change screen (see [Known Limitations](#known-limitations)).

## Database

`database.sql` creates two tables:

- `users`: `id`, `email` (unique), `password` (PBKDF2 hash), `adsoyad`
- `patients`: `id`, `tc_no`, `adsoyad`, `telefon`, `tani`, `doktor`, `durum`, `son_ziyaret`

There are **no foreign keys** between the tables. The doctor and status of a patient are stored as plain text, not as references to other tables.

## Known Limitations

- **No appointment system.** "Randevu Bekliyor" (waiting for appointment) is only a status text, and "Today's examinations" is based on the patient's last visit date.
- **No user roles or authorization.** Every logged-in user can add, update, delete and print all patient records.
- **No foreign keys / normalization** in the database (see above).
- The login attempt limit is kept in memory and resets when the application restarts.
- No password-change screen.
- The UI is available in Turkish only.

## Sample Data and Images

- **All sample data in `database.sql` is fictitious.** The names, phone numbers and TC identity numbers do not belong to real people; the TC numbers are not valid TC numbers.
- The images `MediSistem/Resources/iç hastane resmi.jpg.png` and `MediSistem/Resources/sekreter.jpg.png` were **generated with AI**.

## Third-Party Libraries

Both libraries are restored from NuGet and are not part of this repository:

- [MySql.Data](https://www.nuget.org/packages/MySql.Data) (MySQL Connector/NET), licensed under GPLv2 with the Universal FOSS Exception
- [Guna.UI2.WinForms](https://www.nuget.org/packages/Guna.UI2.WinForms), a third-party UI component library with its own license terms; check them before commercial use

## Screenshots

Screenshots will be added to `docs/screenshots/`.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
