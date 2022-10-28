# Synapse - Home Automation Demo

## Introduction
This project has been used as a support to demonstrate some of [Synapse Workflow Management System](https://github.com/serverlessworkflow/synapse) features during the 5th [Serverless Workflow](https://serverlessworkflow.io/) Workshop back in October 2022.

The [presentation recording](https://www.youtube.com/watch?v=QSqb8cYBVpg) can be found on YouTube: https://www.youtube.com/watch?v=QSqb8cYBVpg

## Get started
If you want to put your hands on the demo, the procedure is quite simple.

As you may know, Synapse runs on many platforms including Docker and Kubernetes. For ease of use, in this demo, we choose the Docker flavour.

The first, obvious step, is to clone the repository:
```
git clone https://github.com/neuroglia-io/synapse.demo.git
cd synapse.demo
```

### Running with Visual Studio 2022
For folks using Visual Studio 2022, just open up the solution file `Synapse.Demo.sln`, set the `docker-compose` project as start up project and hit the Play button.

### Running wthout Visual Studio 2022
For the others, you'll have to slightly adapt the Docker Compose configuration depending on the scenario you wish to follow. Follow the instructions one the one best suited to your needs/environment. Once you adapted the configuration, you'll simply have to start Docker Composer from the root directory `synapse.demo` with the following command:
```
docker-compose -f .\deployment\docker\docker-compose.yml up -d
```

#### With TLS
If you'd like to keep using HTTPS endpoints for the demo, you'll have to [create certificates and tell the demo app to use them](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0).

**Generate certificate for Windows using Linux containers**

Generate certificate and configure local machine:
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p { password here }
dotnet dev-certs https --trust
```
In the preceding commands, replace `{ password here }` with a password.

**Generate certificate for MacOS or Linux**

Generate certificate and configure local machine:
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p { password here }
dotnet dev-certs https --trust
```
`dotnet dev-certs https --trust` is only supported on macOS and Windows. You need to trust certificates on Linux in the way that is supported by your distribution. It is likely that you need to trust the certificate in your browser.

In the preceding commands, replace `{ password here }` with a password.

**Load the certificate**

Update `deployment/docker/docker-compose.yml` file for the `iot-demo` service:
```yaml
# ...
  iot-demo:
  # ...
    environment:
      # ...
      ASPNETCORE_Kestrel__Certificates__Default__Password: 'password'
      ASPNETCORE_Kestrel__Certificates__Default__Path: '/https/aspnetapp.pfx'
      # ...
    volumes:
      - ~/.aspnet/https:/https:ro
# ...
```
The password specified in the docker compose file must match the password used for the certificate.

#### Without TLS
An easier way to get started is to disable TLS altogether for the `iot-demo` service. To do so, update `deployment/docker/docker-compose.yml` file :
```yaml
# ...
  iot-demo:
  # ...
    environment:
      # ...
      #ASPNETCORE_URLS: 'https://+:443;http://+:80' # remove this line
      ASPNETCORE_URLS: 'http://+:80' # add this line
      # ...
    ports:
      - 8088:80
    #  - 44362:443 # remove this line
# ...
```

And, in the same file, adapt Synapse `Cloud events sink URI`:
```yaml
# ...
  synapse:
# ...
    environment:
      # ...
      - SYNAPSE_CLOUDEVENTS_SINK_URI=http://${DEMO_HOST} # replace https:// with http://
# ...
```