# Fable 2 Minimal App

This template generates a simple Fable 2 app including an [Elmish](https://elmish.github.io/) counter with as little configuration as possible. If you want to see a more complex app, check `Fable2.Template.Fulma`.

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 2.0 or higher
* [node.js](https://nodejs.org) with [npm](https://www.npmjs.com/)

## Installing and running the template

In a terminal, type `dotnet new -i Fable2.Template` to install or update the template to the latest version. Then type `dotnet new fable2-minimal` to create a project with the template.

> More info [here](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new).

## Building and running the app

* Install JS dependencies: `npm install`
* Install F# dependencies: `dotnet restore src`
* Move to `src` directory to start Fable and Webpack dev server: `dotnet fable webpack-dev-server`
* After the first compilation is finished, in your browser open: http://localhost:8080/

Any modification you do to the F# code will be reflected in the web page after saving.

## Project structure

### npm

JS dependencies are declared in `package.json`, while `package-lock.json` is a lock file automatically generated.

### Webpack

[Webpack](https://webpack.js.org) is a JS bundler with extensions, like a static dev server that enables hot reloading on code changes. Fable interacts with Webpack through the `fable-loader`. Configuration for Webpack is defined in the `webpack.config.js` file.

### F#

The template only contains two F# files: the project (.fsproj) and a source file (.fs) in the `src` folder.

### Web assets

The `index.html` file and other assets like an icon can be found in the `public` folder.