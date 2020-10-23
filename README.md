# Welcome to Azure IoT hands-on course

This course is divided into 4 parts:

- [register a device in an IoT Hub](#register-a-device-in-an-iot-hub)
- [send telemetry from a device to an IoT hub](#send-telemetry-from-a-device-to-an-iot-hub)
- [read device data with a back-end application](#read-device-data-with-a-back-end-application)
- [control a device connected to an IoT hub](#control-a-device-connected-to-an-iot-hub)

## Prerequisites

- install .NET Core SDK (https://dotnet.microsoft.com/download)
- install Git client (https://git-scm.com/downloads)
- retrieve course materials: `git clone https://github.com/christophecremon/azure-iot.git`

## Register a device in an IoT Hub

Replace the following placeholders with those given by the instructor:

- `$SubscriptionName` = ""
- `$HubName` = ""
- `$DeviceId` = "simulated-YourInitials"

Run the following commands in Azure Cloud Shell to add the IoT Hub CLI extension and to create the device identity:

1. Connect to [Azure portal](https://portal.azure.com)
2. Select the Cloud Shell button on the menu in the upper-right corner of the Azure portal, select "PowerShell" from the dropdown menu
3. Type the following commands:
    - az account set --subscription `$SubscriptionName`
    - az extension add --name azure-cli-iot-ext
    - az iot hub device-identity create --hub-name `$HubName` --device-id `$DeviceId`

## Note the following values

### Hub Connection String

- az iot hub show-connection-string --hub-name `$HubName` --output tsv

### Device Connection String

- az iot hub device-identity show-connection-string --hub-name `$HubName` --device-id `$DeviceId` --output table

### Hub Compatible Endpoint

- az iot hub show --query properties.eventHubEndpoints.events.endpoint --name `$HubName`

### Hub Compatible Path

- az iot hub show --query properties.eventHubEndpoints.events.path --name `$HubName`

### Hub Primary Key

- az iot hub policy show --name iothubowner --query primaryKey --hub-name `$HubName`

## Send telemetry from a device to an IoT hub

- launch a terminal
- move to the directory `DeviceToCloud`
- change the following lines in file `DeviceToCloud.cs`
- line 16: replace `DeviceId` with value from `$DeviceId`
- line 17: replace `DeviceConnectionString` with value from [Device Connection String](#device-connection-string)
- run "dotnet run" in the same directory
- data should be sent from your simulated device to the Cloud (aka: Device To Cloud, D2C), every second (telemetry interval = 1 second)

## Read device data with a back-end application

- launch a terminal
- move to the directory `ReadDeviceToCloudMessages`
- change the following lines in file `ReadDeviceToCloudMessages.cs`
- line 15: replace `HubCompatibleEndpoint` with value from [Hub Compatible Endpoint](#hub-compatible-endpoint)
- line 16: replace `HubCompatiblePath` with value from [Hub Compatible Path](#hub-compatible-path)
- line 17: replace `HubPrimaryKey` with value from [Hub Primary Key](#hub-primary-key)
- run "dotnet run" in the same directory
- you should be able to view the data sent from your device to the Cloud, every second by default

## Control a device connected to an IoT hub

- launch a terminal
- move to the directory `CloudToDevice`
- change the following lines in file `CloudToDevice.cs`
- line 13: replace `DeviceId` with value from `$DeviceId`
- line 14: replace `HubConnectionString` with value from [Hub Connection String](#hub-connection-string)
- run "dotnet run" in the same directory
- data should be sent from the Cloud to your simulated device (aka: Cloud To Device, C2D), changing telemetry interval from 1 second to 10 seconds (direct method: SetTelemetryInterval)