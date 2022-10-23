# RestaurantAPI

This is a simple API for a restaurant to get available reservations. It is built using Dotnet 6.

## Getting Started
You'll need first to have .NET6 installed on your machine. You can download it [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

Then clone the repository :
```sh
git clone https://github.com/LeedrisDev/RestaurantAPI
```

---

## APIs
To run the API, simply type in a terminal :
```sh
dotnet run ./Restaurants/ShinkoAPI/ShinkoAPI.csproj --configuration Release
```

---

## Client
Before everything, if you want to use the client, you'll need to defin some environment variables :
```sh
export BEGIN_DATE="01-11-2022"
export END_DATE="30-11-2022"
export NB_GUESTS=3
export IS_LUNCH="true"
export IS_DINNER="true"
```
Up to you to change the values.

Then you can run the client :
```sh
dotnet run ./Restaurants/Client/Client.csproj --configuration Release
```
