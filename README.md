# MJML.NET
An unofficial port of [MJML](https://mjml.io/) (by [MailJet](https://www.mailjet.com/)) to [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard). This project is currently in **experimental state** and should not be used in a production environment. 

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
You can find out more about MJML 4 from the official website. 
</p>

<p align="center">
| <b><a href="https://mjml.io/">Official Website</a></b>
  | <b><a href="https://documentation.mjml.io/">Official Documentation</a></b>
  | <b><a href="https://mjml.io/getting-started-onboard">Official Oboarding</a></b>
  |
</p>

## Usage
Firstly, you'll need to reference the `MJML.NET` NuGet Package into your project. 

```cmd
PM > Install-Package MjmlDotNet -Version 1.0.0
```

Secondly, include `MJML.NET` namespace into your project.
```csharp
using MjmlDotNet;
```

Finally, the boilerplate code.
```csharp
public void Main() 
{
  // or DI
  IMjmlParser mjmlParser = new MjmlParser();

  string mjml = @"
    <mjml>
      <mj-body>
        <mj-section>
          <mj-column>
            <mj-text>
              Hello World!
            </mj-text>
          </mj-column>
        </mj-section>
      </mj-body>
    </mjml>";

  string html = mjmlParser.ParseDocument(mjmlString);
}
```
## Options
You can also specify options to the MJML parser. This can be done either at instantiation throught `defaultOptions` or when parsing an MJML document.

```csharp
public void Main() 
{
  // 1. Default Options for every MJML document
  MjmlParserOptions defaultOptions = new MjmlParserOptions() {
    Minify = true;
    Prettify = false;
  }
 
  IMjmlParser mjmlParser = new MjmlParser(defaultOptions);

  // Or 2. Override at per-document parsing
  MjmlParserOptions options = new MjmlParserOptions() {
    Minify = true;
    Prettify = false;
  }

  string html = mjmlParser.ParseDocument(mjml, options);
}
```

Currently, we've not implemented all of the same options. This is because we're still introducing components and refactoring the project. However, the currently supported options are:

| Name | Data Type | Default | Description |
| ---- | --------- | ------- | ----------- |
| Prettify | Boolean | False | Prettifies the output when enabled. |
| Minify | Boolean | True | Minifies the output when enabled. This overrides `Prettify` as this would be ideal default in production environment. |
---

## Status
`MJML.NET` tries to implement all functionality `1-2-1` with the MJML 4 project. However, due to JavaScript not being a typed language this means there has been considerate refactoring to the code to make it more aligned with C# typed requirements. 

As the project is currently still in experimental state not all MJML components have been added. Please see the below table for a list of all components and the current implementation state:

| Type | Component | Implemented | Tests | State |
| ---- | --------- | ----------- | ----- | ----- |
| Core | [mjml](https://documentation.mjml.io/#mjml) | :white_check_mark: | :x: | Awaiting Tests
| Core | [mjml-head](https://documentation.mjml.io/#mj-head) | :white_check_mark: | :x: | Awaiting Tests 
| Core | [mjml-body](https://documentation.mjml.io/#mj-body) | :white_check_mark: | :x: |  Awaiting Tests
| Core | [mjml-include](https://documentation.mjml.io/#mj-include) | :x: | :x: | Not Implemented
| Head | [mjml-attributes](https://documentation.mjml.io/#mj-attributes) | :white_check_mark: | :x: | Awaiting Tests
| Head | `mjml-class` | :x: | :x: | Not Implemented
| Head | `mjml-all` | :x: | :x: | Not Implemented
| Head | [mjml-breakpoint](https://documentation.mjml.io/#mj-breakpoint) | :white_check_mark: | :x: | Awaiting Tests
| Head | [mjml-font](https://documentation.mjml.io/#mj-font) | :white_check_mark: | :x: | Awaiting Tests
| Head | [mjml-html-attributes](https://documentation.mjml.io/#mj-html-attributes) | :x: | :x: | Not Implemented
| Head | [mjml-preview](https://documentation.mjml.io/#mj-preview) | :white_check_mark: | :x: | Awaiting Tests
| Head | [mjml-style](https://documentation.mjml.io/#mj-style) | :ballot_box_with_check: | :x: |  Partially Complete (Inline Support Required)
| Head | [mjml-title](https://documentation.mjml.io/#mj-title) | :white_check_mark: | :white_check_mark: | Complete
| Body | [mjml-accordion](https://documentation.mjml.io/#mj-accordion) | :x: | :x: | Not Implemented 
| Body | [mjml-button](https://documentation.mjml.io/#mj-button) | :white_check_mark: | :x: | Awaiting Tests 
| Body | [mjml-carousel](https://documentation.mjml.io/#mj-carousel) | :x: | :x: | Not Implemented
| Body | [mjml-column](https://documentation.mjml.io/#mj-column) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-divider](https://documentation.mjml.io/#mj-divider) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-group](https://documentation.mjml.io/#mj-group) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-hero](https://documentation.mjml.io/#mj-hero) | :white_check_mark: | :x: | Awaiting Tests 
| Body | [mjml-image](https://documentation.mjml.io/#mj-image) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-navbar](https://documentation.mjml.io/#mj-navbar) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-raw](https://documentation.mjml.io/#mj-raw) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-section](https://documentation.mjml.io/#mj-section) | :ballot_box_with_check: | :x: | Partially Complete (Background Position Bug)
| Body | [mjml-social](https://documentation.mjml.io/#mj-social) | :white_check_mark:  | :x: | Awaiting Tests
| Body | [mjml-spacer](https://documentation.mjml.io/#mj-spacer) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-table](https://documentation.mjml.io/#mj-table) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-text](https://documentation.mjml.io/#mj-text) | :white_check_mark: | :x: | Awaiting Tests
| Body | [mjml-wrapper](https://documentation.mjml.io/#mj-wrapper) | :white_check_mark: | :x: | Awaiting Tests
---
## Tests
Testing this project has been rather difficult due to the nature of the output. However, we're still trying to provide valuable tests for components which can validate correct output. For example, if we set the `<mj-title>` the outputted HTML document `<title>` should match.

Furthermore, this project will fall into the grey area of testing. Because of this, we'll rely heavily on bug reports from usage of `MJML.NET`.

## Benchmark
We've benchmarked the main components of the library to make sure we're within a suitable execution speed. We'll continue monitoring the execution speed as part of project development. We benchmark based off the default MJML templates.

### [Austin Template](https://mjml.io/try-it-live/templates/austin)
|             Method |     Mean |    Error |   StdDev |
|------------------- |---------:|---------:|---------:|
|   TryParseDocument | 37.64 ms | 2.538 ms | 7.483 ms |
|      ParseDocument | 38.77 ms | 2.549 ms | 7.515 ms |
| ParseDocumentAsync | 38.24 ms | 2.157 ms | 6.360 ms |
---

## Contribution
We really appreciate any contribution to the project to help provide a native version of MJML to C#. 

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Side Note: In your PR you should summarise your changes, bug fixes or general modifications.

## Appreciations
Once again, it's good to share some appreciation to the projects that make `MJML.NET` possible.

* [MJML](https://github.com/mjmlio/mjml)
* [AngleSharp](https://github.com/AngleSharp/AngleSharp)

## License
 
The MIT License (MIT)

Copyright (c) 2015 Liam Riddell

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
