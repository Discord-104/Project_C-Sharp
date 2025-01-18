# 🏥 **Medical Visits Management** 🏥

A Windows Forms project that allows you to manage scheduled medical visits and their history. The application enables you to:

- 📅 View scheduled and historical visits through a `DataGridView`.
- ➕ Add, ✏️ edit, and ❌ delete visits.
- 💾 Save and load visit data from CSV files.

## 🎯 **Main Features** 

### 1. **View Visits**
- Scheduled visits are displayed in the `Scheduled_Visits` table.
- The history of visits is shown in the `Visit_History` table.

### 2. **Delete Visits**
- A scheduled visit can be deleted by selecting it from the `Scheduled_Visits` table. This removes the visit from the list, `DataGridView`, and the CSV file.
- For visits in the history, you can delete them from the `Visit_History` table. Deleting from history does not alter the other saved historical entries in the CSV file.

### 3. **CSV File Management**
- Scheduled visit data is saved in `CSV\ScheduledVisits.csv`.
- Historical visit data is saved in `CSV\History.csv`.
- File operations include reading, writing, and updating after modifications or deletions.

### 4. **Confirmations & Notifications**
- A confirmation message will appear before proceeding with the deletion of any visit.
- Notifications will inform the user of the status of their operations (e.g., success or error during deletion).

## 🗂️ **Project Structure**

### Main Classes

1. **ScheduledVisit**
   - Manages the details of scheduled visits.
   - Method `ToCsvString()` to export data to CSV format.

2. **History**
   - Manages the details of past visits.
   - Method `ToCsv()` to export data to CSV format.

### Key Implemented Methods

- **`Delete_Click`**: Handles the deletion of visits from the scheduled list or history.
- **`DeleteScheduledVisitFromFile`**: Deletes a scheduled visit from the CSV file and updates the list in memory.
- **`DeleteVisitFromHistory`**: Deletes a visit from the history without affecting other saved entries.
- **`FormatDataGrid`**: Updates the layout of the tables.

## ⚙️ **Requirements**

- **Platform**: Windows.
- **Development Environment**: Microsoft Visual Studio.
- **Programming Language**: C#.
  
## ⚠️ **Important Notes**

- **CSV Backup**: It’s recommended to back up the CSV files before deleting visits to avoid accidental data loss.
- **Error Handling**: Error messages are implemented to handle file reading/writing issues.

## 📄 **License**

This project is licensed under the MIT License. See the `LICENSE` file for more details.
