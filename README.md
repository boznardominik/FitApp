# FitApp (AVALONIA, C#) 

This is a simple activity tracking app for a small school class project. 

What app does:
- users can enter body information
- than you are able to add fitness activites, for now only (walking, running, cycling, swimming)
- date is stored simply in local text files
- it calculates simple statistics based on users body and activites information.

## Screenshot in repository

App screenshot (FitApp.png)


## HOW TO USE ##

OPTION 1 : RUN FROM TERMINAL

git clone https://github.com/boznardominik/FitApp.git
cd FitApp
dotnet run

OPTION 2 : OPEN IN VISUAL STUDIO

1. clone (download) the reposetory
2. open the project (FitApp.sln) in program Visual Studio
3. make sure .NET 8 SDK is installed 
4. install the extension Avalonia for Visual Studio
5. run the aplication by pressing F5 


## FUTURE

In the future i would like to make similar example to Fitapp as an mobile app for my diploma. I will probably have to write it in different language or UI framework that supoert GPS tracking for android.
GPS tracking would allow users to automatically collect movement data, so you won t have to always enter it manualy. I will also swap the txt file storage with SQLite, a local SQL database without server, so storage will be more 
structured and scalable in future.



- local SQL base
- swap text file data storage with SQLite for more reliabe, scalable data storage.
- more activity types, beter statisticsY
- improve user intergace
- make the project in a diferent language where it is easier to add GPS tracking
