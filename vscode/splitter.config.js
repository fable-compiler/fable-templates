const path = require("path");
const fableUtils = require("fable-utils");
 
function resolve(relativePath) {
    return path.join(__dirname, relativePath);
}
 
module.exports = {
  entry: resolve("extension.fsproj"),
  outDir: resolve("out"),
  babel: fableUtils.resolveBabelOptions({
    presets: [["es2015", { modules: "commonjs" }]],
    sourceMaps: true,
  }),
  fable: {
    define: ["DEBUG"]
  }
}