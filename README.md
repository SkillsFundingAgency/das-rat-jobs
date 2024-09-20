## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# Request Apprentice Training Jobs

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status%2Fdas-rat-jobs?repoName=SkillsFundingAgency%2Fdas-rat-jobs&branchName=refs%2Fpull%2F10%2Fmerge)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=3822&repoName=SkillsFundingAgency%2Fdas-rat-jobs&branchName=refs%2Fpull%2F10%2Fmerge)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-rat-jobs&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SkillsFundingAgency_das-rat-jobs)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

This azure functions solution is part of Request Apprentice Training (RAT) project. Here we have background jobs in form of Azure functions that carry out periodical jobs like sending out notifications or expiring requests.

## How It Works

The notification job uses NServiceBus protocol to send a message per notification to the notification queue. The function connects to Employer Request Apprentice Training Outer API to get a list of standard requests per account which have recieved responses but have not been viewed by the requesting account.

The expiry job updates the database to mark a request as expired. The function connects to Employer Request Apprentice Training Outer API to update the expiry status and date for a standard request which has exceeded the configurable expiry months.

## 🚀 Installation

### Pre-Requisites
* A clone of this repository
* Storage emulator like Azurite for local config source

### Config

You can find the latest config file in [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-rat-jobs/SFA.DAS.RequestApprenticeTraining.Jobs.json). 

In the `SFA.DAS.RequestApprenticeTraining.Jobs` project, if not exist already, add local.settings.json file with following content:
```
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "ConfigNames": "SFA.DAS.RequestApprenticeTraining.Jobs",
        "EnvironmentName": "LOCAL",
        "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true",
        "ExpireEmployerRequestsTimerSchedule": "*/1 * * * *",
        "SendEmployerRequestsResponseNotificationTimerSchedule": "*/1 * * * *"
    }
}
```

## 🔗 External Dependencies

* The functions use the database defined in [das-rat-api](https://github.com/SkillsFundingAgency/das-rat-api) as primary data source.
* The functions use the Employer Request Apprentice Training Outer API defined in [das-apim-endpoints](https://github.com/SkillsFundingAgency/das-apim-endpoints/tree/master/src/EmployerRequestApprenticeTraining) to connect to the Inner API.
* The notification functions depends on [das-notifications](https://github.com/SkillsFundingAgency/das-notifications) Api to listen to the queue and forward the notification requests to Gov Notify to send out emails.

### 📦 Internal Package Dependencies
* SFA.DAS.Configuration.AzureTableStorage

## Technologies
* .Net 8.0
* Azure Functions V4
* Azure Table Storage
* NUnit
* Moq
* FluentAssertions
