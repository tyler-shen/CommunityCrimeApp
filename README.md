# CommunityCrimeApp

A small community crime search app for Calgary


## Data

Connect to my local MSSQL Express. Statistics from https://data.calgary.ca/Health-and-Safety/Community-Crime-Statistics/78gh-n26t.

Bascially just download the CSV file and import to my database.

Program will check and update my local database everytime you open it. I assume government staffs won't modify old records, so I only check and insert new records.


## Data Validation

I also assume drop down lists are fixed as well.

Sector is limited to "NORTH", "EAST", "NORTHEAST", "SOUTH", "NORTHWEST", "SOUTHEAST", "CENTRE", "WEST".

Category is limited to "Break & Enter - Commercial", "Commercial Robbery", "Theft FROM Vehicle", "Violence Other (Non-domestic)", "Break & Enter - Other Premises", "Street Robbery", "Theft OF Vehicle", "Assault (Non-domestic)", "Break & Enter - Dwelling".

Date starts from 2017-01.


## Dependencies

.NET 4.7.1

SODA.NET is available as a Nuget package called CSM.SodaDotNet

GMap.NET Windows Form is available as a Nuget package


## Executing program

Run the program and click Start button, it will connect to local database and check update.

If it fails to connect to local database, it will stop the process. If no update or update failed, it will continue to use old data.

Software displays all records without pinning on the map defaultly.

You can choose the Sector, Category, Date range, and pin on the map or not as you want.

Pinning on the map is not recommanded if the crimes number is too large, because you will just see too many pins on the map.

You can also click the View Details button to see the details of the cases that match your filter.
