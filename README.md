# Synapse - Home Automation Demo

## Introduction
This project has been used as a support to demonstrate some of [Synapse Workflow Management System](https://github.com/serverlessworkflow/synapse)'s features during the 5th [Serverless Workflow](https://serverlessworkflow.io/) Workshop back in October 2022.

The [presentation recording](https://www.youtube.com/watch?v=QSqb8cYBVpg) can be found on YouTube: https://www.youtube.com/watch?v=QSqb8cYBVpg

## How to get started
If you'd like to try out the demo yourself, the procedure is quite simple.

As you may know, Synapse runs on many platforms including Docker and Kubernetes. For ease of use, in this demo, we choose the Docker flavor.

The first, obvious step, is to clone the repository:
```
git clone https://github.com/neuroglia-io/synapse.demo.git
cd synapse.demo
```

### Option 1: Running with Visual Studio 2022
For folks using Visual Studio 2022, just open up the solution file `Synapse.Demo.sln`, set the `docker-compose` project as start up project and hit the Play button.

###  Option 2: Running wthout Visual Studio 2022 (Docker Compose required)
For the others, all it takes is a single command line. Start Docker Compose from the root directory `synapse.demo` with the following command:
```
docker-compose -f .\deployment\docker\docker-compose.yml up -d
```

In both cases, you'll be able to access two websites:
- http://localhost:8088 // The IoT demo app
- http://localhost:42286 // Synapse

## Demo use-cases
For the demo, we created a fake IoT environement that can be mononitored and simulated via a simple UI. When you access the UI, http://localhost:8088, you'll be presented with the monitoring view of the system: some cards with info related to each "device". In the upper right corner you'll find the "controls" button that will toggle an overlay for simulating devices behaviors.

### Models
On a more technical aspect, all objects manipulated by this app are "[Devices](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Integration/Models/Device.cs)". They are constituted with the following properties:

- **Id** (*string*): the unique identifier of the device
- **Label** (*string*): the label displayed to the user
- **Type** (*string*): a type identifier for the device
- **Location** (*Location->string*): where the device is located
- **State** (*object*): the state object of the device
- **CreatedAt** (*DateTime*): the date and time of creation of the device
- **LastModified** (*DateTime*): the date and time of the last modification on the device
- **StateVersion** (*int*): the version of the device (used for db concurrency)

On start-up, the app will [seed](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Application/Services/DataSeeder.cs#L52) some default devices with the following [ids and types](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Common/ApplicationConstants.cs):
- thermometer / sensor.thermometer
- hydrometer / sensor.hydrometer
- heater / equipment.heater
- air-conditioning / equipment.air-conditioning
- lights-hallway / switch.light
- lights-living / switch.light
- motion-sensor-hallway / sensor.motion
- motion-sensor-living / sensor.motion
- blinds-hallway / equipment.blinds
- blinds-living / equipment.blinds

All devices, except the `thermometer` and `hydrometer`, will hold a basic state with a property `on` (nullable boolean).

e.g.:
```json
// A/C ON
{
    "id":"air-conditioning",
    "label":"A/C",
    "type":"equipment.air-conditioning",
    "location":{
        "label":"living",
        "parent":{
            "label":"indoor"
        }
    },
    "state":{
        "on": true
    },
    "createdAt":"2022-11-02T10:40:50.1432735+00:00",
    "lastModified":"2022-11-02T10:40:50.1432735+00:00",
    "stateVersion":1
}
// Heater OFF
{
    "id":"heater",
    "label":"Heater",
    "type":"equipment.heater",
    "location":{
        "label":"cellar",
        "parent":{
            "label":"indoor"
        }
    },
    "state":{

    },
    "createdAt":"2022-11-02T10:40:50.1432688+00:00",
    "lastModified":"2022-11-02T10:40:50.1432688+00:00",
    "stateVersion":1
}
```

The `thermometer` will hold a state with an measured `temperature` (nullable int) and a `desired` temperature (nullable int).

e.g.:
```json
{
    "id":"thermometer",
    "label":"Temperature",
    "type":"sensor.thermometer",
    "location":{
        "label":"indoor"
    },
    "state":{
        "temperature":16,
        "desired": 19
    },
    "createdAt":"2022-11-02T10:40:50.1418395+00:00",
    "lastModified":"2022-11-02T10:40:50.1418395+00:00",
    "stateVersion":1
}
```

The `hydrometer` state will hold a `humidity` property (nullable int).

e.g.:
```json
{
    "id":"hydrometer",
    "createdAt":"2022-11-02T10:40:50.1430997+00:00",
    "lastModified":"2022-11-02T10:40:50.1430997+00:00",
    "stateVersion":1,
    "label":"Humidity",
    "type":"sensor.hydrometer",
    "location":{
        "label":"indoor"
    },
    "state":{
        "humidity":53
    }
}
```

You can add any type of device with any state you'd like (create device command) but you'll probably have to adapt the UI code to handle the new types.

### Cloud Events
The application can ingest (sub) [commands](https://github.com/neuroglia-io/synapse.demo/tree/main/sources/core/Synapse.Demo.Integration/Commands) cloud events:
- [Create a device](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Integration/Commands/Devices/CreateDeviceCommand.cs): `com.synapse.demo/device/create/v1`
- [Update a device](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Integration/Commands/Devices/UpdateDeviceStateCommand.cs): `com.synapse.demo/device/update-state/v1`
- [Patch a device](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Integration/Commands/Devices/PatchDeviceStateCommand.cs): `com.synapse.demo/device/patch-state/v1` (known bug when sending null (or false) property values, they will be ignored instead of merged)

e.g.:
```json
{
    "id": "61bb5440-6e7b-41c4-87ed-5a2b771d51e1",
    "specversion": "1.0",
    "datacontenttype" : "application/json",
    "type": "com.synapse.demo/device/update-state/v1",
    "source": "https://anysource.com/foo",
    "data":{
        "aggregateId": "thermometer",
        "deviceId": "thermometer",
        "state": {
            "temperature": 35,
            "desired": 21
        }
    }
}
```

The application will also emit (pub) two types of cloud events, [device created](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Integration/Events/Devices/DeviceCreatedIntegrationEvent.cs) (`com.synapse.demo/device/created/v1`) and [device state changed](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/core/Synapse.Demo.Integration/Events/Devices/DeviceStateChangedIntegrationEvent.cs) (`com.synapse.demo/device/state-changed/v1`).

The cloud events will be published with the (default, defined in the [Docker Compose](https://github.com/neuroglia-io/synapse.demo/blob/main/deployment/docker/docker-compose.yml#L31) file) source `https://demo.synpase.com` and the types mentioned above.

e.g.:
```json
{
    "id":"3f6910fd-025f-4753-a291-18b352da755b",
    "source":"https://demo.synpase.com/",
    "type":"com.synapse.demo/device/state-changed/v1",
    "subject":"thermometer",
    "time":"2022-11-02T10:51:12.7284407+00:00",
    "dataSchema":"https://schema-registry.synapse.com/device/state-changed/v1",
    "data":{
        "aggregateId":"thermometer",
        "createdAt":"2022-11-02T10:51:12.7284407+00:00",
        "state":{
            "desired":19,
            "temperature":16
        }
    }
}
```

### APIs
The demo exposes multiple "APIs":

- RESTful
- OData (partially broken? doesn't handle state "object")
- WebSocket (SignalR)
- Cloud Events

RESTful & OData documentations are available at http://localhost:8088/api/doc

Cloud events can be posted on any endpoint as long as the `Content-Type` is `application/cloudevents+json`.

The WebSocket will push outgoing cloud events to the clients in an envelope `ReceiveIntegrationEventAsync`:
e.g.:
```json
{
    "type":1,
    "target":"ReceiveIntegrationEventAsync",
    "arguments":[
        {
            "id":"3f6910fd-025f-4753-a291-18b352da755b",
            "source":"https://demo.synpase.com/",
            "type":"com.synapse.demo/device/state-changed/v1",
            "subject":"thermometer",
            "time":"2022-11-02T10:51:12.7284407+00:00",
            "dataSchema":"https://schema-registry.synapse.com/device/state-changed/v1",
            "data":{
                "aggregateId":"thermometer",
                "createdAt":"2022-11-02T10:51:12.7284407+00:00",
                "state":{
                    "desired":19,
                    "temperature":16
                }
            }
        }
    ]
}
```
And clients can send cloud events via `HandleCloudEvent`.

For cloud events payloads (data) are the same than the ones used in the RESTful API but with an additionnal `aggregateId` property, holding the device id.


### Temperature management
The first use-case is about simulating temperature management, turning a heater or A/C on or off based on the desired temperature and the current temperature.
```json
{
  "id": "manage-house-temperature",
  "name": "Manage House Temperature",
  "description": "Monitors and manages the house's temperature",
  "version": "0.1.0",
  "specVersion": "0.8",
  "events": [
    {
      "name": "on-temperature-changed",
      "source": "https://demo.synpase.com",
      "type": "com.synapse.demo/device/state-changed/v1",
      "correlation": [
        {
          "contextAttributeName": "subject",
          "contextAttributeValue": "thermometer"
        }
      ]
    }
  ],
  "functions": [
    {
      "name": "update-device-state",
      "type": "rest",
	  "operation": "http://iot-demo/api/v1/doc/oas.json#UpdateDeviceState"
    }
  ],
  "states": [
    {
      "name": "On temperature changed",
      "type": "event",
      "onEvents": [
        {
          "actions": [],
          "eventRefs": [
            "on-temperature-changed"
          ],
          "eventDataFilter": {
            "toStateData": "${ .thermometer }"
          }
        }
      ],
      "transition": "Is temperature ok?"
    },
    {
      "name": "Is temperature ok?",
      "type": "switch",
      "dataConditions": [
        {
          "name": "Too cold",
          "transition": "Turn on the heater",
          "condition": "${ .thermometer.state.temperature != null and .thermometer.state.desired != null and .thermometer.state.temperature < .thermometer.state.desired }"
        },
        {
          "name": "Too hot",
          "transition": "Turn on the AC",
          "condition": "${ .thermometer.state.temperature != null and .thermometer.state.desired != null and .thermometer.state.temperature > .thermometer.state.desired }"
        },
	    {
          "name": "Perfect",
          "transition": "Turn off heater and AC",
          "condition": "${ .thermometer.state.temperature != null and .thermometer.state.desired != null and .thermometer.state.temperature == .thermometer.state.desired }"
        }
      ],
      "defaultCondition": {
        "end": true
      }
    },
    {
      "name": "Turn on the heater",
      "type": "operation",
      "actions": [
	  {
		"name": "Turn off the AC",
		"functionRef":{
			"refName": "update-device-state",
			"arguments":{
				"deviceId": "air-conditioning",
				"state": {
					"on": false
				}
			}
		}
	  },
	  {
		"name": "Turn on the heater",
		"functionRef":{
			"refName": "update-device-state",
			"arguments":{
				"deviceId": "heater",
				"state": {
					"on": true
				}
			}
		}
	  }],
      "end": true
    },
    {
      "name": "Turn on the AC",
      "type": "operation",
      "actions": [
	  {
		"name": "Turn off the heater",
		"functionRef":{
			"refName": "update-device-state",
			"arguments":{
				"deviceId": "heater",
				"state": {
					"on": false
				}
			}
		}
	  },
	  {
		"name": "Turn on the AC",
		"functionRef":{
			"refName": "update-device-state",
			"arguments":{
				"deviceId": "air-conditioning",
				"state": {
					"on": true
				}
			}
		}
	  }],
      "end": true
    },
    {
      "name": "Turn off heater and AC",
      "type": "operation",
      "actions":  [
	  {
		"name": "Turn off the heater",
		"functionRef":{
			"refName": "update-device-state",
			"arguments":{
				"deviceId": "heater",
				"state": {
					"on": false
				}
			}
		}
	  },
	  {
		"name": "Turn off the AC",
		"functionRef":{
			"refName": "update-device-state",
			"arguments":{
				"deviceId": "air-conditioning",
				"state": {
					"on": false
				}
			}
		}
	  }],
      "end": true
    }
  ]
}
```

### Lights management
The second use-case is to turn on the lights when a motion sensor is triggered and turn them off after a specified amount of time:
```json
{
  "id":"light-on-motion",
  "name":"Light on motion",
  "description":"Turns lights on when detecting motion",
  "version":"0.1.0",
  "specVersion":"0.8",
  "events":[
    {
      "name":"on-motion-detected",
      "source":"https://demo.synpase.com",
      "type":"com.synapse.demo/device/state-changed/v1",
      "correlation":[
        {
          "contextAttributeName":"subject",
          "contextAttributeValue":"motion-sensor-.*"
        }
      ]
    }
  ],
  "functions":[
    {
      "name":"get-device",
      "operation":"http://iot-demo/api/v1/doc/oas.json#GetDeviceById",
      "type":"rest"
    },
    {
      "name":"update-device-state",
      "operation":"http://iot-demo/api/v1/doc/oas.json#UpdateDeviceState",
      "type":"rest"
    }
  ],
  "states":[
    {
      "name":"On motion detected",
      "type":"event",
      "onEvents":[
        {
          "actions":[
            
          ],
          "eventRefs":[
            "on-motion-detected"
          ],
          "eventDataFilter":{
            "toStateData":"${ .event }"
          }
        }
      ],
      "transition":"Is motion active ?"
    },
    {
      "name":"Is motion active ?",
      "type":"switch",
      "dataConditions":[
        {
          "name":"Yes",
          "transition":"Turn on the lights",
          "condition":"${ .event.state.on }"
        }
      ],
      "defaultCondition":{
        "end":true
      }
    },
    {
      "name":"Turn on the lights",
      "type":"operation",
      "actions":[
        {
          "name":"Get motion sensor",
          "actionDataFilter":{
            "toStateData":"${ .motionSensor }"
          },
          "functionRef":{
            "refName":"get-device",
            "arguments":{
              "id":"${ .event.aggregateId }"
            }
          }
        },
        {
          "name":"Get lights in area",
          "actionDataFilter":{
            "toStateData":"${ .lights }"
          },
          "functionRef":{
            "refName":"get-device",
            "arguments":{
              "id":"${ .motionSensor.id | sub(\"motion-sensor\";\"lights\") }"
            }
          }
        },
        {
          "name":"Turn on the lights",
          "actionDataFilter":{
            "useResults":false
          },
          "functionRef":{
            "refName":"update-device-state",
            "arguments":{
              "deviceId":"${ .lights.id }",
              "state":{
                "on":true
              }
            }
          }
        }
      ],
      "transition":"Wait for 10 secs"
    },
    {
      "name":"Wait for 10 secs",
      "type":"sleep",
      "duration":"PT10S",
      "transition":"Turn off the lights"
    },
    {
      "name":"Turn off the lights",
      "type":"operation",
      "actions":[
        {
          "name":"Turn off the lights",
          "functionRef":{
            "refName":"update-device-state",
            "arguments":{
              "deviceId":"${ .lights.id }",
              "state":{
                "on":false
              }
            }
          }
        }
      ],
      "end":true
    }
  ]
}
```

### Power saving when away
The third use-case is to turn off the lights and lower the temperature when going out (cron scheduling recommanded but not illustrated bellow):
```json
{
  "id": "power-saver",
  "name": "Power saver",
  "description": "Saves power by shutting down lights and setting the temperature to 16 ° C everyday when leaving for work",
  "version": "0.1.1",
  "specVersion": "0.8",
  "functions": [
    {
      "name": "get-devices",
      "operation": "http://iot-demo/api/v1/doc/oas.json#GetDevices",
      "type": "rest"
    },
    {
      "name": "update-device-state",
      "operation": "http://iot-demo/api/v1/doc/oas.json#UpdateDeviceState",
      "type": "rest"
    },
    {
      "name": "patch-device-state",
      "operation": "http://iot-demo/api/v1/doc/oas.json#PatchDeviceState",
      "type": "rest"
    }
  ],
  "states": [
    {
      "name": "Get lights",
      "type": "operation",
      "actions": [
        {
          "name": "Get lights",
          "actionDataFilter": {
            "results": "${ [ .[] | select(.id | startswith(\"lights-\")) ] }",
            "toStateData": "${ .lights }"
          },
          "functionRef": {
            "refName": "get-devices"
          }
        }
      ],
      "transition": "Shutdown lights"
    },
    {
      "name": "Shutdown lights",
      "type": "foreach",
      "inputCollection": "${ .lights }",
      "actions": [
        {
          "name": "Turn off the lights",
          "functionRef": {
            "refName": "update-device-state",
            "arguments": {
              "deviceId": "${ .light.id }",
              "state": {
                "on": false
              }
            }
          }
        }
      ],
      "iterationParam": "light",
      "transition": "Set desired temperature"
    },
    {
      "name": "Set desired temperature",
      "type": "operation",
      "actions": [
        {
          "name": "Set desired temperature",
          "functionRef": {
            "refName": "patch-device-state",
            "arguments": {
			  "deviceId": "thermometer",
			  "state":{
				"desired": 16
			  }
            }
          }
        }
      ],
      "end": true
    }
  ]
}
```

## Mounting default workflow definitions
As is, when launching the demp, there will be no workflow definition in Synapse, you'll have to create them.

If you want to seed default definitions, you can mount a volume containing json workflow definitions to `/app/data/definitions`. Synapse will load them automatically.

For example, in order to load the 3 use-cases definitions above by default, update the [Docker Compose](https://github.com/neuroglia-io/synapse.demo/blob/main/deployment/docker/docker-compose.yml) file with the following instruction:
```yaml
# ...
services:
  synapse:
    image: ghcr.io/serverlessworkflow/synapse:latest # Synapse image
    # ...
    volumes:
      # ...
      - ./data/definitions:/app/data/definitions
# ...
```


## Persistence
With the provided [Docker Compose](https://github.com/neuroglia-io/synapse.demo/blob/main/deployment/docker/docker-compose.yml) config, no persistence for Synapse has been configured. Which means that when the container running Synapse is brought down, all data will be lost.

To enable peristence, you can rely on both the EventStore and Mongo plugins provided by Synapse.

More info can be found in the [Plugins sections of Synpase](https://github.com/serverlessworkflow/synapse/wiki/Plugins) wiki or in the [Synapse docker-compose sample](https://github.com/serverlessworkflow/synapse/blob/main/deployment/docker-compose/docker-compose.yml).

The demo app itself relies on volatile in memory db (repositories). There is no built-in way to enable persistence, each time you restart the application, data will be reset. You could edit the application [persistence configuration code](https://github.com/neuroglia-io/synapse.demo/blob/main/sources/infrastructure/Synapse.Demo.Persistence/Extensions/IServiceCollectionExtensions.cs) to use Neuroglia's [EventStore](https://github.com/neuroglia-io/framework/tree/main/src/Data/Neuroglia.Data.EventSourcing.EventStore) and [MongoDB](https://github.com/neuroglia-io/framework/tree/main/src/Data/Neuroglia.Data.MongoDB) repositories instead, or implement your own repositories.

## Troubleshoting
### Some bugs fixed in Synapse are still happening in my environment 
You might be using old docker image of Synapse or its worker. Run the following command to remove all Synapse related images and start Docker Compose again:
```
docker rmi -f $(docker images "ghcr.io/serverlessworkflow/*" -aq)
```

### A workflow instance stays `pending` for a long time
The first time Synapse starts a workflow instance, it pulls the worker image from the image repository. This operation can take a while depending on your network capabilities and servers load.