// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require("path");
//#if( gitpod )
const execSync = require('child_process').execSync;
var isGitPod = process.env.GITPOD_INSTANCE_ID !== undefined;

function getDevServerUrl() {
    if (isGitPod) {
        const url = execSync('gp url 8080');
        return url.toString().trim();
    } else {
        return `http://localhost:8080`;
    }
}
//#endif

module.exports = {
    mode: "development",
    entry: "./src/App.fs.js",
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
    },
    devServer: {
        static: {
            directory: path.resolve(__dirname, "./public"),
            publicPath: "/",
        },
        port: 8080,
//#if( gitpod )
        public: getDevServerUrl(),
        host: '0.0.0.0',
        allowedHosts: ['localhost', '.gitpod.io']
//#endif
    },
    module: {
    }
}
