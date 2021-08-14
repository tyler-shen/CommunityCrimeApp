# CommunityCrimeApp

A small community crime search app for Calgary


## Database

Connect to my local MSSQL Express. Statistics from https://data.calgary.ca/Health-and-Safety/Community-Crime-Statistics/78gh-n26t.

Bascially just download the CSV file and import to my database.


## Data Validation

Since the data is fixed, the date and drop down list is fixed as well.

Sector is limited to "NORTH", "EAST", "NORTHEAST", "SOUTH", "NORTHWEST", "SOUTHEAST", "CENTRE", "WEST".

Category is limited to "Break & Enter - Commercial", "Commercial Robbery", "Theft FROM Vehicle", "Violence Other (Non-domestic)", "Break & Enter - Other Premises", "Street Robbery", "Theft OF Vehicle", "Assault (Non-domestic)", "Break & Enter - Dwelling".

Date is limited to 2017-01 to 2021-06.

If the data is updated, the date and drop down items may need to change.


## Dependencies

.NET 4.7.1

GMap.NET Windows Form is available as a Nuget package


## Executing program

Run the program, it displayed all records without pinning on the map defaultly.

You can choose the Sector, Category, Date range, and pin on the map or not as you want.

Pinning on the map is not recommanded if the crimes number is too large, because you will just see too many pins on the map.

You can also click the View Details button to see the details of the cases that match your filter.
