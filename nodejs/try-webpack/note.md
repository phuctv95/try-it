## Basic

Basic:
- *webpack* is a static module bundler for modern JavaScript applications.
- When webpack processes your application, it internally builds a *dependency graph* which maps every module your project needs and generates one or more *bundles*.

Concepts:
- Entry: indicates which module webpack should use to begin building out its internal dependency graph (default is `./src/index.js`)
- Output: indicates where to write the output bundles.
- Loaders: Out of the box, webpack only understands JavaScript and JSON files. Loaders allow webpack to process other types of files and convert them into valid modules that can be consumed by your application and added to the dependency graph.
- Plugins: While loaders are used to transform certain types of modules, plugins can be leveraged to perform a wider range of tasks like bundle optimization, asset management and injection of environment variables.
- Mode: By setting the mode parameter to either `development`, `production` or `none`, you can enable webpack's built-in optimizations that correspond to each environment. The default value is `production`.

Browser compatibility:
- webpack supports all browsers that are ES5-compliant (IE8 and below are not supported). webpack needs Promise for import() and require.ensure().
- If you want to support older browsers, you will need to load a polyfill before using these expressions.
