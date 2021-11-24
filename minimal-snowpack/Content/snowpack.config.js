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
    'dist': { url: '/' }
  },
  plugins: [
    [
      '@snowpack/plugin-run-script',
      {
        cmd: 'dotnet fable src -o dist',
        watch: 'dotnet fable watch src -o dist'
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
