## Table Of Contents

<details>
<summary>Details</summary>

  - [Introduction](#introduction)
  - [Description](#description)
  - [Run](#run)

</details>

## Introduction

This is a project created for Unity Developer Test Task.

You can find the Test Task [here](https://github.com/stroibot/AnalyticsServiceTestTask/blob/main/TestTask.md).

This project was completed in a little over 8 hours using following software, tools, frameworks, etc:
* Unity 2021.3 LTS
* Visual Studio 2022 + ReSharper 2022
* Zenject

The project uses all of the best industry practicies and design patterns. The code is well-structured, self-documented, scalable and consistent.

## Description

This project contains basic architecture for an App.

Lifecycle of the App is managed using a simple State Machine, which triggers `AnalyticsService.TrackEvent` to record specific app events.

`AnalyticsService` has next features:
* Batching: when you call `TrackEvent` event is not sent immediatley, but instead collected and sent only when the count of events is greater than or equal to `BatchSize`
* If you call `TrackEvent` and `AnalyticsService` already is sending events, then new event won't be sent, but instead added to the next batch to prevent any unwanted behavior
* `AnalyticsService` sends events every `CooldownBeforeSend`
* When app is closed/crashed, the `AnalyticsService` tries to send current events, if they can't be sent, then these events will be stored locally in a file
* When app launches, the `AnalyticsService` will load any events stored in a local file and send them
* Events are sent asynchronously

## Run

You can run WebGL build [here](https://stroibot.github.io/AnalyticsServiceTestTask/).

To view logs, please, use built-in developer console in your browser.
