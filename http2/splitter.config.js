const path = require("path");
const fableUtils = require("fable-utils");

function resolve(filePath) {
  return path.resolve(__dirname, filePath)
}

var outFile = resolve("dist/app.js");

module.exports = {
  entry: resolve("src/App.fsproj"),
  outDir: path.dirname(outFile),
  babel: fableUtils.resolveBabelOptions({
    plugins: ["transform-es2015-modules-commonjs"]
  }),
  fable: { define: ["DEBUG"] }
};