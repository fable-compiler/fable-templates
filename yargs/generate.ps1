# generate the bindings with ts2fable
ts2fable node_modules/@types/node/index.d.ts src/Node.fs
ts2fable node_modules/@types/yargs/index.d.ts src/Yargs.fs