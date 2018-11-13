import * as shell from "shelljs";

const args = process.argv.slice(2);
const dirName = args[0];

shell.config.verbose = true;
shell.cd(dirName);
shell.rm("-rf", "bin", "obj");
shell.exec("dotnet pack");
shell.ls("bin/Debug").forEach(function (name) {
    if (/\.nupkg$/.test(name)) {
        shell.exec(`dotnet nuget push bin/Debug/${name} -s nuget.org -k $NUGET_KEY`)
    }
})
