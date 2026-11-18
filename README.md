# FitApp (AVALONIA, C#) 

FitApp is a desktop fitness tracking application developed using C#, .NET 8, Avalonia UI, and XAML.
The application allows users to record physical activities, manage their personal information, and calculate fitness statistics (calories burned...) based on their activity and body data.

## Features:

- User Profile – Enter and manage personal information such as age, weight, height, and gender.
- Activity Tracking – Record walking, running, cycling, and swimming activities.
- Fitness Statistics – Calculate calories burned, basal metabolic rate (BMR), pace, speed, and other activity-related statistics.
- Data Storage – Store user profiles and activity records locally using text files.
- Activity Management – View, sort, and manage recorded activities.

## Screenshot in repository

App screenshot (FitApp.png)

## HOW TO USE ##

## Requirements

- .NET 8 SDK (required for building and running from source)
- Windows 10 / 11

## Requirements for source code
- Visual Studio 
- .NET 8 SDK
- Avalonia UI
- CommunityToolKit.MvvM

OPTION 1 : RUN FROM TERMINAL

git clone https://github.com/boznardominik/FitApp.git

cd FitApp

dotnet run

OPTION 2 : RUN IN VISUAL STUDIO

1. Clone or download the repository.
2. Open FitApp.sln in Visual Studio.
3. Make sure the .NET 8 SDK is installed.
4. Restore NuGet packages if necessary.
5. Press F5 to build and run the application.

Note: The Avalonia for Visual Studio extension is optional and provides additional XAML development tools.


## FUTURE

In the future i would like to make a mobile version of FitApp. I will write it in different language (TypeScript) or UI framework that suports GPS tracking for android.
GPS tracking would allow users to automatically collect movement data, so it won t have to be enetered manually. Txt file will also be swaped wiht a local SQL database, so storage will be more structured and scalable in future.

- local SQL base
- swap text file data storage with SQLite for more reliabe, scalable data storage.
- more activity types, beter statistics
- improve user interface
- make the project in a diferent programming language with easier GPS tracking implementation.
