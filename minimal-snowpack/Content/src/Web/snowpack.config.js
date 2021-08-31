/** @type {import("snowpack").SnowpackUserConfig } */
module.exports = {
  mount: {
    public: { url: '/' },
    'src': { url: '/dist' }
  },
  plugins: [
    [
      '@snowpack/plugin-run-script',
      {
        cmd: 'dotnet fable ../App/App.fsproj -o ./src',
        watch: 'dotnet fable watch ../App/App.fsproj -o ./src'
      },
    ]
  ],
  packageOptions: {
    /* ... */
  },
  devOptions: {
    /* ... */
  },
  buildOptions: {
    /* ... */
  },
};
