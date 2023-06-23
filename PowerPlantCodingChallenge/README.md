# Introduction

Solution for the Engie power plant coding challenge.

# Getting Started

1. Create a *Log Analytics Workspace* in *Azure* and take note of its **workspace ID** and **primary key** (under
   *Agents*)
2. Clone the repository
3. Navigate to the `src/Presentation.Api` folder
4. Open up the `appsettings.Development.json` file with your favorite text editor
5. Replace the `authenticationId` (*primary key*) and the `workspaceId` values with the values from the *Log Analytics
   Workspace* created in step 1
6. Save the file
7. Open up a terminal and navigate to the `src/Presentation.Api` folder
8. Run `dotnet run`
9. Open up a browser and navigate to `https://localhost:8888/swagger` or send a `POST` request with your favorite REST
   client (*Postman*, *Insomnia*...) to `https://localhost:8888/powerplants/merit-order`:

```
curl -X 'POST' \
  'https://localhost:8888/powerplants/merit-order' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "load": 910,
  "fuels":
  {
    "gas(euro/MWh)": 13.4,
    "kerosine(euro/MWh)": 50.8,
    "co2(euro/ton)": 20,
    "wind(%)": 60
  },
  "powerplants": [
    {
      "name": "gasfiredbig1",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    {
      "name": "gasfiredbig2",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    {
      "name": "gasfiredsomewhatsmaller",
      "type": "gasfired",
      "efficiency": 0.37,
      "pmin": 40,
      "pmax": 210
    },
    {
      "name": "tj1",
      "type": "turbojet",
      "efficiency": 0.3,
      "pmin": 0,
      "pmax": 16
    },
    {
      "name": "windpark1",
      "type": "windturbine",
      "efficiency": 1,
      "pmin": 0,
      "pmax": 150
    },
    {
      "name": "windpark2",
      "type": "windturbine",
      "efficiency": 1,
      "pmin": 0,
      "pmax": 36
    }
  ]
}'
```

# Build and Test

1. Open up a terminal and navigate to the `test/Core.Tests` folder
2. Run `dotnet test`

# Logging

Logging is done to the console and to *Azure Log Analytics*, however note that in a production environment, logging to
the console might be optional, which explains why part of the configuration is done in the
`appsettings.Development.json` file. Typically, for a production environment, an `appsettings.Production.json` file
would be created and would not contain the `Console` sink.

## Cause a failure to test logging

1. Start the application
2. Send a `POST` request to `https://localhost:8888/powerplants/merit-order` with the following payload (notice the
   `pmin` which is higher than the `pmax`):

```json
{
  "load": 910,
  "fuels": {
    "gas(euro/MWh)": 13.4,
    "kerosine(euro/MWh)": 50.8,
    "co2(euro/ton)": 20,
    "wind(%)": 60
  },
  "powerplants": [
    {
      "name": "gasfiredbig1",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 500,
      "pmax": 460
    }
  ]
}
```

3. Notice the 400 (bad request) response received
4. Open up the *Log Analytics Workspace* in *Azure* and navigate to *Logs*
5. Run the following query:

```kql
EngiePowerPlantCodingChallenge_CL 
| where LogLevel_s == "Error"
```

6. Notice the detailed exception

![Log Analytics](https://github.com/sebdesalvador/powerplant-coding-challenge/blob/master/PowerPlantCodingChallenge/doc/images/log-analytics.png?raw=true)
