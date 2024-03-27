# WPF Cinema Management Application

This is a group project assignment from the course **CSC13001 – Windows Programming (21KTPM2)** at VNU-HCM, University of Science.

## Project contributors
1. Nguyễn Quỳnh Hương ([qhuongng](https://github.com/qhuongng)): User pages (home, movie catalog, search results, movie details & ticket purchase, user profile).

2. Đặng Nhật Hòa ([hoadang0305](https://github.com/hoadang0305)): Admin pages (dashboard, movie management), login portal, user profile.

3. Nguyễn Thái Đan Sâm ([ntdsam2830](https://github.com/ntdsam2830)): Admin pages (dashboard, navigation bar, movie management, voucher & showtime management).

## General information
The application was developed using .NET 8.0, following the MVVM architecture. It connects to a Microsoft SQL Server database and works with the data using LINQ and Entity Framework.

The application consists of two parts:
- The **user client** allows users to browse and search for movies, filter search results, view a movie's detailed information, book tickets and view past ticket purchases. Users can browse as a guest and create an account when they need to purchase tickets.

- The **admin dashboard** allows admins to view insights on ticket sales and manage (add, update or remove items from) lists of movies, showtimes and vouchers. Admins need to sign in to access the dashboard.

## Demo
The demo video for this application is available on [YouTube](https://youtu.be/7UN9fxJTu70) (in Vietnamese).

## Build & run the application locally
**Microsoft Visual Studio** with C# and .NET development packages installed, as well as **Microsoft SQL Server** are required to build and run the project.

1. Clone this repository to your local machine and unzip the **images** folder.

2. Open the **db.sql** file, bulk replace the paths to the image files with your own paths and run the script. \
    *Example: consider this SQL script for inserting an entry into the Actor table:*
    ```
    INSERT INTO Actor (actorName, avatar, shotDes)
    VALUES (N'Tuấn Trần',
            (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\Caffeine\Desktop\cineImgData\tuấn trần.jpg', SINGLE_BLOB) AS Avatar),
		    N'Diễn viên điện ảnh, người mẫu. Nổi tiếng với các vai diễn trong phim "Bố Già", "Hồn Papa Da Con Gái", "Gái Già Lắm Chiêu V".')
    ```
    *Highlight `C:\Users\Caffeine\Desktop\cineImgData`, press **Ctrl + H** to open the replace menu, put your actual path to the **images** folder in the bottom field, and press **Alt + A** or choose **Replace all**.* 

3. Open the solution file in Visual Studio, open the NuGet Package Manager and run the scaffold script:
    ```
    Scaffold-DbContext "Data Source=<YOUR_SERVER_NAME>;Initial Catalog=cinemaManagement;User ID=<YOUR_USER_ID>;Password=<YOUR_PASSWORD>;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
    ```
4. Build and run the project.
