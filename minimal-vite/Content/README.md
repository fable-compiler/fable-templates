# Fable Minimal Vite App

This is a small Fable app project which uses [Vite](https://vitejs.dev/) so you can easily get started and add your own code progressively.

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 5.0 or higher
* [node.js](https://nodejs.org)
* An F# editor like Visual Studio, Visual Studio Code with [Ionide](http://ionide.io/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

## Building and running the app

* Navigate to directory with the package.json file
* Install dependencies: `npm install`
* Start the development server: `npm run start`

Any modification you do to the F# code will be reflected in the web page after saving.

### Note

Please note the 'start' and 'build' scripts in package.json. 
The Fable commands are configured such that the output is placed in public/. 
The Vite commands are configured (through vite.config.js) to use public/ as the 'root' directory, where the index.html is expected to be found.

## Adding SASS support

Adding SASS/SCSS support to a Vite project is trivial. Simply install `sass` (*not* `node-sass`) as a dev dependency:

    npm install -D sass

Then create a styles.scss file in src/ (*not* public/) and import it from your F# file:

    Fable.Core.JsInterop.importAll "./styles.scss"

## Project structure

```
.
├── Nuget.Config
├── README.md
├── package-lock.json
├── package.json - JS dependencies
├── dist - Ready to deploy files when you run `npm run build`
├── public
│   ├── favicon.ico - Fable favicon
│   ├── index.html - Main HTML file
│   └── robots.txt
├── vite.config.js - Vite configuration 
└── src
    ├── App.fs - F# sample code
    └── App.fsproj - F# project
```
