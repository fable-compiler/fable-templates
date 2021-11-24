# Fable Minimal Snowpack App

This is a small Fable app project which uses [Snowpack](https://www.snowpack.dev/) so you can easily get started and add your own code progressively.


## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 5.0 or higher
* [node.js](https://nodejs.org)
* An F# editor like Visual Studio, Visual Studio Code with [Ionide](http://ionide.io/) or [JetBrains Rider](https://www.jetbrains.com/rider/)


## Building and running the app

* Navigate to directory: `cd src/Web`
* Install dependencies: `npm install`
* Start the development server: `npm run dev`

Any modification you do to the F# code will be reflected in the web page after saving.


## Project structure

```
.
├── Nuget.Config
├── README.md
├── package-lock.json
├── package.json - JS dependencies
├── build - Ready to deploy files when you run `npm run build`
├── dist - Compiled JS from F# file
├── public
│   ├── favicon.ico - Fable favicon
│   ├── index.html - Main HTML file
│   └── robots.txt
├── snowpack.config.js - Snowpack configuration
└── src
    ├── App.fs - F# sample code
    └── App.fsproj - F# project
```
