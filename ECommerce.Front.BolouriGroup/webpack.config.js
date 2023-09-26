const mode = process.env.NODE_ENV || "development";

const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");

const entry = {
  bolouriGroup: "./src/js/BolouriGroupJs.js",
};

const output = {
  filename: "[name].bundle.js",
  path: __dirname + "/wwwroot/dist",
  publicPath: "/dist/",
};

const _plugins = [
  new MiniCssExtractPlugin({
    filename: "styles.css",
  }),
];
const _module = {
  rules: [
    {
      test: /.s?css$/,
      use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
    },
    {
      test: /.(jpg|jpeg|png|svg|webp)$/,
      type: "asset/resource",
      generator: {
        filename: "img/[hash][ext][query]",
      },
    },
    {
      test: /\.(woff|woff2|eot|ttf|otf)$/,
      type: "asset/resource",
      generator: {
        filename: "fonts/[hash][ext]",
      },
    },
  ],
};

module.exports = {
  entry,
  mode,
  plugins: _plugins,
  module: _module,
  output,
  optimization: {
    minimize: true,
    minimizer: [
      // For webpack@5 you can use the `...` syntax to extend existing minimizers (i.e. `terser-webpack-plugin`), uncomment the next line
      `...`,
      new CssMinimizerPlugin(),
    ],
  },
};
