const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);

    if (isDevBuild) {
        const spaExtensionsSubstring = "Starting the development server"; // DO NOT CHANGE! эту строку ждет UseReactDevelopmentServer

        const port = process.env.PORT || 3000;  // UseReactDevelopmentServer передает порт через переменную окружения
        console.log(`${spaExtensionsSubstring} at port ${port}`);

        additionalOptions = {
            devtool: "source-map",
            devServer: {
                static: false,
                historyApiFallback: true,
                hot: true,
                port,
            },
        };
    }

    return {
        mode: 'development',
        entry: './ClientApp/main.tsx',
        stats: 'errors-only',
        devtool: 'inline-source-map',
        output: {
            path: path.join(__dirname, 'wwwroot/dist'),
            filename: 'bundle.js'
        },
        devtool: 'inline-source-map',
        ...additionalOptions,
        module: {
            rules: [
                {
                    test: /\.jsx?$/,
                    exclude: /node_modules/,
                    loader: 'babel-loader'
                },
                {
                    test: /\.tsx?$/,
                    use: 'ts-loader',
                    exclude: /node_modules/,
                },
            ]
        },
        resolve: {
            extensions: ['.tsx', '.ts', '.js'],
        },
        plugins: [
            new HtmlWebpackPlugin({
                template: './Pages/index.html'
            })
        ],
        infrastructureLogging: { level: 'error' },
        stats: 'minimal',
    }
}