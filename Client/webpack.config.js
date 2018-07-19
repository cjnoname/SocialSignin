const path = require('path');
const webpack = require('webpack');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;

module.exports = (env, argv) => {
  const isDevBuild = argv.mode !== 'production';
  const targetPath = isDevBuild ? path.join(__dirname, 'dist') : path.join(__dirname, '..', 'Server', 'PremierAppSign', 'wwwroot', 'dist');
  const config = {
    stats: { modules: false, performance: false },
    resolve: {
      extensions: ['.js', '.jsx', '.ts', '.tsx'],
      alias: {
        models: path.resolve(__dirname, './src/models/'),
        enums: path.resolve(__dirname, './src/enums/'),
        utils: path.resolve(__dirname, './src/utils/'),
        UI: path.resolve(__dirname, './src/UI/'),
        shared: path.resolve(__dirname, './src/shared/'),
        store: path.resolve(__dirname, './src/store')
      }
    },
    entry: {
      client: './src/index.tsx'
    },
    module: {
      rules: [
        {
          test: /\.tsx?$/, include: /src/, use: 'awesome-typescript-loader?silent=true'
        }
      ]
    },
    output: {
      filename: '[name].js',
      publicPath: 'dist/',
      path: targetPath
    },
    plugins: [
      new CheckerPlugin()
    ],
    performance: {
      hints: false
    }
  };

  if (isDevBuild) {
    config.devServer = {
      proxy: [{
        context: ["/api", "/images", "/css"],
        target: "http://localhost:5000",
        open: true,
        openPage: 'http://localhost:3000'
      }]
    };
    config.module.rules.push(
      {
        test: /\.(ts|tsx)$/,
        enforce: 'pre',
        loader: 'tslint-loader'
      }
    );
    config.plugins.push(
      new webpack.SourceMapDevToolPlugin({
        filename: '[file].map',
        moduleFilenameTemplate: path.relative(targetPath, '[resourcePath]')
      })
    );
  } else {
    config.optimization = {
      splitChunks: {
        cacheGroups: {
          commons: {
            test: /node_modules/,
            name: "vendor",
            chunks: "all"
          }
        }
      }
    };
  }

  return config;
};
