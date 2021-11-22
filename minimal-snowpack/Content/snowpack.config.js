/** @type {import("snowpack").SnowpackUserConfig } */
module.exports = {
  exclude: [
    '**/node_modules/**/*',
    '**/obj/**/*',
    '**/bin/**/*',
    '**/*.fsproj'
  ],
  mount: {
    public: { url: '/' },
    'src': { url: '/' }
  },
  plugins: [
    [
      '@snowpack/plugin-run-script',
      {
        cmd: 'dotnet fable src',
        watch: 'dotnet fable watch src'
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
