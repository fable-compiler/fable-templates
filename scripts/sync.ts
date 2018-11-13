import * as shell from "shelljs";
import * as path from "path";

const args = process.argv.slice(2);
const dirName = args[0];
const contentDir = path.join(dirName, "Content");

const tempDir = "temp";
const templateConfigDir = ".template.config";

shell.rm("-rf", tempDir)
shell.mkdir(tempDir);
shell.cp("-R", path.join(contentDir, templateConfigDir), tempDir);
shell.rm("-rf", contentDir);
shell.mkdir(contentDir);

const sampleDir = path.join("../fable2-samples", dirName);
shell.ls(sampleDir).forEach(function (name) {
    if (!/node_modules/.test(name)) {
        shell.cp("-R", path.join(sampleDir, name), contentDir);
    }
})
shell.cp("-R", path.join(tempDir, templateConfigDir), contentDir);
