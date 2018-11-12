import * as fs from "fs-extra";
import * as path from "path";

function createDirEmpty(dir: string) {
    fs.removeSync(dir)
    fs.mkdirSync(dir);
}

const args = process.argv.slice(2);
const dirName = args[0];
const contentDir = path.join(dirName, "Content");

const tempDir = "temp";
const templateConfigDir = ".template.config";
createDirEmpty(tempDir)
fs.copySync(path.join(contentDir, templateConfigDir), tempDir);
fs.removeSync(contentDir);
fs.copySync(path.join("../fable2-samples", dirName), contentDir, {
    filter(src, dest) {
        return !/node_modules|\.fable/.test(src);
    }
});
fs.copySync(tempDir, path.join(contentDir, templateConfigDir));
