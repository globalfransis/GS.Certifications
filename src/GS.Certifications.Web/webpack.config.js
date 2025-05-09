const glob = require('glob')
const path = require('path')
var webpack = require('webpack');

//we want to find all folders in src / with a index.js inside, and convert them to outputs with the folder's name as the js file name, in the bundle folder. the folder tree has to be be respected.
//e.g.: './wwwroot/src/some/thing/index.js' generates './wwwroot/bundle/some/thing.js'

var appBasePath = './wwwroot/src/'; // where the source files located
var publicPath = '../bundle/'; // public path to modify asset urls. eg: '../bundle' => 'www.example.com/bundle/main.js'
var bundleExportPath = './wwwroot/bundle/'; // directory to export build files


//search all index.js files and take them as an entry. glob operator ** is use to look folders at any depth
var getEntries = () => {
    return glob.sync('./wwwroot/src/**/index.js').reduce((acc, path) => {
        //each acc key (entry) will be the output file name, then remove all but the folders jerarchy.
        var entry = path.replace('/index.js', '').replace('/wwwroot/src', '')
        acc[entry] = path
        return acc
    }, {})
}

const UglifyJsPlugin = require('uglifyjs-webpack-plugin')

module.exports = {
    entry: getEntries(),
    output: {
        path: path.resolve(__dirname, bundleExportPath),
        publicPath: publicPath,
        //the output file name is the key for each item in getEntries()
        filename: '[name].js'
    },
    resolve: {
        extensions: ['.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js',
            '@': path.join(__dirname, appBasePath)
        }
    },
    module: {
        loaders: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                        sass: 'vue-style-loader!css-loader!sass-loader?indentedSyntax' // <style lang="sass">
                    }
                }
            },
            {
                test: /\.scss$/,
                loader: 'style-loader!css-loader!sass-loader'
            },
            {
                test: /\.css$/,
                loader: 'style-loader!css-loader'
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2)(\?\S*)?$/,
                loader: 'file-loader'
            },
            {
                test: /\.(png|jpe?g|gif|svg)(\?\S*)?$/,
                loader: 'file-loader',
                query: {
                    name: '[name].[ext]?[hash]'
                }
            }
        ]
    },
    devtool: '#source-map', //'#eval-source-map'
}
module.exports.watch = process.env.WATCH === "true";
if (process.env.NODE_ENV === 'production') {
    module.exports.devtool = '#source-map'
    // http://vue-loader.vuejs.org/en/workflow/production.html
    module.exports.plugins = (module.exports.plugins || []).concat([
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: '"production"'
            }
        }),
        new UglifyJsPlugin({
            "uglifyOptions":
            {
                compress: {
                    warnings: false
                },
                sourceMap: true
            }
        }),
    ]);
}
else if (process.env.NODE_ENV === "dev") {
    module.exports.watch = true;
    module.exports.plugins = (module.exports.plugins || []).concat([
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: '"development"'
            }
        }),
    ]);
}