# MJML.NET
An un-official port of [MailJets MJML](https://mjml.io/) to [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard). This project is currently in **experimental state** and should not be used in a production environment. 

## Introduction

`MJML` is a markup language created by [Mailjet](https://www.mailjet.com/) and designed to reduce the pain of coding a responsive email. Its semantic syntax makes the language easy and straightforward while its rich standard components library shortens your development time and lightens your email codebase. MJML’s open-source engine takes care of translating the `MJML` you wrote into responsive HTML.

<p align="center">
  <a href="https://mjml.io" rel="nofollow">
    <img width="250" src="https://camo.githubusercontent.com/49c1d426dca03897940f39457ded0b622383efada70e7c845efadf68dfc8a73b/68747470733a2f2f6d6a6d6c2e696f2f6173736574732f696d672f6c69746d75732f6d6a6d6c62796d61696c6a65742e706e67" data-canonical-src="https://mjml.io/assets/img/litmus/mjmlbymailjet.png" style="max-width:100%;">
  </a>
</p>

<p align="center">
  <a href="https://mjml.io" target="_blank">
    <img width="75%" src="https://cloud.githubusercontent.com/assets/6558790/12450760/ee034178-bf85-11e5-9dda-98d0c8f9f8d6.png">
  </a>
</p>


<p align="center">
  | <b><a href="https://documentation.mjml.io/">Official Documentation</a></b>
  | <b><a href="https://mjml.io/getting-started-onboard">Getting Started</a></b>
  |
</p>

## Status
`MJML.NET` tries to implement all functionality `1-2-1` with the MJML 4 project. However, due to JavaScript not being a typed language this means there has been considerate refactoring to the code to make it more aligned with C# typed requirements. 

As the project is currently still in experimental state not all MJML components have been added. Please see the below table for a list of all components and the current implementation state:

| Type | Component | Implemented | Tests | State |
| ---- | --------- | ----------- | ----- | ----- |
| Core | `mjml` | :white_check_mark: | :x: | Awaiting Tests
| Core | `mjml-head` | :white_check_mark: | :x: | Awaiting Tests 
| Core | `mjml-body` | :white_check_mark: | :x: |  Awaiting Tests
| Core | `mjml-include` | :x: | :x: | Not Implemented
| Head | `mjml-attributes` | :x: | :x: | Not Implemented
| Head | `mjml-breakpoint` | :white_check_mark: | :x: | Awaiting Tests
| Head | `mjml-font` | :white_check_mark: | :x: | Awaiting Tests
| Head | `mjml-html-attributes` | :x: | :x: | Not Implemented
| Head | `mjml-preview` | :white_check_mark: | :x: | Awaiting Tests
| Head | `mjml-style` | :x: | :x: |  Partially Complete (Inline Support Required)
| Head | `mjml-title` | :white_check_mark: | :white_check_mark: | Complete
| Body | `mjml-accordion` | :x: | :x: | Not Implemented 
| Body | `mjml-button` | :white_check_mark: | :x: | Awaiting Tests 
| Body | `mjml-carousel` | :x: | :x: | Not Implemented
| Body | `mjml-column` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-divider` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-group` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-hero` | :white_check_mark: | :x: | Awaiting Tests 
| Body | `mjml-image` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-navbar` | :x: | :x: | Not Implemented 
| Body | `mjml-raw` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-section` | :x: | :x: | Partially Complete (Background Position Bug)
| Body | `mjml-social` | :white_check_mark:  | :x: | Awaiting Tests
| Body | `mjml-spacer` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-table` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-text` | :white_check_mark: | :x: | Awaiting Tests
| Body | `mjml-wrapper` | :white_check_mark: | :x: | Awaiting Tests


## Usage
